using System;
using System.Collections.Generic;
using System.IO;


namespace Csv2QifConverter.Lib
{
    public static partial class CSV2QIFConverter
    {

        public static void Convert(
            FileInfo csvFile,
            FileInfo qifFile,
            FileInfo rejectedTransFile,
            FileInfo Ej2QickenSecurites,
            string EJAccount,
            string EJNickName,
            decimal QuantityLimit,
            decimal PriceLimit,
            decimal TransactionLimit,
            DateTime startDate

            )
        {
            bool debug = false;

            if (debug)
            {
                Console.WriteLine($"csvFile:\t\t{csvFile}");
                Console.WriteLine($"qifFile:\t\t{qifFile}");
                Console.WriteLine($"rejectedTransFile:\t{rejectedTransFile}");
                Console.WriteLine($"Ej2QickenSecurites:\t{Ej2QickenSecurites}");
                Console.WriteLine($"EJAccount:\t\t{EJAccount}");
                Console.WriteLine($"EJNickName:\t\t{EJNickName}");
                Console.WriteLine($"QuantityLimit:\t\t{QuantityLimit}");
                Console.WriteLine($"PriceLimit:\t\t{PriceLimit}");
                Console.WriteLine($"TransactionLimit:\t{TransactionLimit}");
            }

            // list of activities that Converter can process
            List<string> Activities = new List<string>
            {
                "Buy",                  // Buy a security with cash in the account
                "Bought",               // Same thing as buy
                "Sell",                 // Sell a security
                "Sold",                 // Same thing as sell
                "Dividend",             // Dividend paid into the account
                "Reinvested Dist",      // Reinvestment of a distribution paid by a fund
                "Reinvest",             // Additional shares distributed instead of cash distribution
                "Other",                // Exception - except with matching "Reinvest" activity
                "Fee",                  // Investment fees
                "Tax Withheld",         // Tax on investment fees
                "Distribution",
                "Cash Other",
                "Cash In Lieu"          // Excepton
            };

            // read the CSV Name Map file into dictionary
            Dictionary<string, string> SecuritiesMap = CreateNameMapDictionaryFromCSVFile(Ej2QickenSecurites);
            System.Console.WriteLine("Creation of dictionary of mappings between Edward Jones and Quicken was sucessfull!");

            // read the CSV Transaction file into the transaction list
            List<Transaction> trans = CreateTransactionListFromCSVFile(csvFile);
            // Output to console
            if (debug)
            {
                System.Console.WriteLine("Creation of a transaction list from the input csv file was sucessfull!");
                OutputToConsole(trans, "Input");
            }

            // Validate the Transaction list and flag invalid records status field with a message
            ValidateTransactions(trans, Activities, EJAccount, EJNickName, QuantityLimit, PriceLimit, TransactionLimit, startDate);
            if (debug)
            {
                System.Console.WriteLine("Validation of the csv file was sucessfull!");
                OutputToConsole(trans, "Validation");
            }

            PopulateQIFFields(trans, SecuritiesMap);
            if (debug)
            {
                System.Console.WriteLine("Population of QIF fields was sucessfull!");
                OutputToConsole(trans, "QifFields");
            }

            // Output to files
            using (TextWriter writer = new StreamWriter(qifFile.FullName))
            {
                writer.WriteLine("!Type:Invst");
                foreach (var tran in trans)
                {
                    if (tran.Status == null)
                    {
                        writer.WriteLine(tran.QIFDate);
                        writer.WriteLine(tran.QIFAction);
                        switch (tran.QIFAction)
                        {
                            case "NDiv":
                                writer.WriteLine(tran.QIFSecurity);
                                writer.WriteLine(tran.QIFCleared);
                                writer.WriteLine(tran.QIFUAmount);
                                writer.WriteLine(tran.QIFTAmount);
                                writer.WriteLine(tran.QIFDelimiter);
                                break;

                            case "NReinvDiv":
                                writer.WriteLine(tran.QIFSecurity);
                                writer.WriteLine(tran.QIFPrice);
                                writer.WriteLine(tran.QIFQuantity);
                                writer.WriteLine(tran.QIFCleared);
                                writer.WriteLine(tran.QIFUAmount);
                                writer.WriteLine(tran.QIFTAmount);
                                writer.WriteLine(tran.QIFDelimiter);
                                break;

                            case "NBuy":
                                writer.WriteLine(tran.QIFSecurity);
                                writer.WriteLine(tran.QIFPrice);
                                writer.WriteLine(tran.QIFQuantity);
                                writer.WriteLine(tran.QIFCleared);
                                writer.WriteLine(tran.QIFUAmount);
                                writer.WriteLine(tran.QIFTAmount);
                                writer.WriteLine(tran.QIFDelimiter);
                                break;

                            case "NSell":
                                writer.WriteLine(tran.QIFSecurity);
                                writer.WriteLine(tran.QIFPrice);
                                writer.WriteLine(tran.QIFQuantity);
                                writer.WriteLine(tran.QIFCleared);
                                writer.WriteLine(tran.QIFUAmount);
                                writer.WriteLine(tran.QIFTAmount);
                                writer.WriteLine(tran.QIFDelimiter);
                                break;

                            case "NCash":
                                writer.WriteLine(tran.QIFCleared);
                                writer.WriteLine(tran.QIFUAmount);
                                writer.WriteLine(tran.QIFTAmount);
                                writer.WriteLine(tran.QIFCategory);
                                writer.WriteLine(tran.QIFDelimiter);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            if (debug)
            {
                System.Console.WriteLine("Writing to Qif File was sucessfull!");
                OutputToConsole(trans, "QifWrite");
            }

            using (TextWriter writer = new StreamWriter(rejectedTransFile.FullName))
            {
                writer.WriteLine("DATE,ACCOUNT,ACTIVITY,DESCRIPTION,QUANTITY,CURRENCY,COMMISSION,PRICE,NET AMOUNT,ACCOUNT NICKNAME");
                foreach (var tran in trans)
                {
                    if (tran.Status != null)
                    {
                        if (!tran.Status.Contains("[Duplicate]"))
                        {
                            writer.WriteLine(value: $"{tran.Date:d},{tran.Account},{tran.Activity}," +
                                $"{tran.Description},{tran.Quantity},{tran.Currency:C},{tran.Commission:C}," +
                                $"{tran.Price:C},{tran.NetAmount:$######0.00},{tran.AccountNickname},{tran.Status}");
                        }
                    }
                }
            }
            if (debug)
            {
                System.Console.WriteLine("Writing to Excluded file was sucessfull!");
                OutputToConsole(trans, "ExcludedWrite");
            }
        }

        #region -------------------------------- Private Methods --------------------------------------
        private static Dictionary<string, string> CreateNameMapDictionaryFromCSVFile(FileInfo SecuritiesNameMapFile)
        {
            // read the Name Map file into dictionary
            Dictionary<string, string> SecuritiesMap = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(SecuritiesNameMapFile.FullName))
            {
                if (!reader.EndOfStream)
                // skip over the header line
                {
                    reader.ReadLine();
                }
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    SecuritiesMap.Add(values[0], values[1]);
                }
            }
            return SecuritiesMap;
        }

        private static List<Transaction> CreateTransactionListFromCSVFile(FileInfo CsvFile)
        {
            List<Transaction> trans = new List<Transaction>();

            List<string> csvLines = CreateListOfLinesFromCSVFile(CsvFile);

            // Break up the csvLines list into fields and put into trans
            foreach (var line in csvLines)
            {
                string[] fields = line.Split(',');
                Transaction tr = new Transaction
                {
                    Date = DateTime.Parse(fields[0]),                    // the date is in the first field - form is M/d/yy
                    Account = fields[1],                                 // the account name is not used
                    Activity = fields[2],                                // The activity is translated to an QIF action
                    Description = fields[3],                             // The Secutity name is within the descripiotn
                    Quantity = decimal.Parse(fields[4]),                 // Quantity
                    Currency = fields[5],                                // The currency is not used it is always CAD
                    Commission = fields[6].Substring(0, 1) == "-" ?      // The commission string is converted to a signed decimal - not used
                    -decimal.Parse(fields[6].TrimStart('-', '$')) :
                    decimal.Parse(fields[6].TrimStart('$')),
                    Price = fields[7].Substring(0, 1) == "-" ?           // The price is converted to a signed decimal
                    -decimal.Parse(fields[7].TrimStart('-', '$')) :
                    decimal.Parse(fields[7].TrimStart('$')),
                    NetAmount = fields[8].Substring(0, 1) == "-" ?       // The NetAmount is converted to a signed decimal
                    -decimal.Parse(fields[8].TrimStart('-', '$')) :
                    decimal.Parse(fields[8].TrimStart('$')),
                    AccountNickname = fields[9]                          // The account nickname is not used
                };

                trans.Add(tr);
            }
            // Sort the transactions into assending date order
            trans.Sort((x, y) => x.Date.CompareTo(y.Date));

            return trans;
        }

        private static List<string> CreateListOfLinesFromCSVFile(FileInfo CsvFile)
        {
            // Read in the lines from the CSV Transaction file
            List<string> csvLines = new List<string>();
            using (StreamReader reader = new StreamReader(CsvFile.FullName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    csvLines.Add(line);
                }
            }

            // Delete the header from the csvLines list
            csvLines.RemoveRange(0, 2);
            // Delete the footer from the csvLines list
            csvLines.RemoveRange(csvLines.Count - 4, 4);
            return csvLines;
        }

        private static void ValidateTransactions(List<Transaction> trans,
                                                 List<string> Activities,
                                                 string EJAccount,
                                                 string EJNickName,
                                                 decimal QuantityLimit,
                                                 decimal PriceLimit,
                                                 decimal TansactionLimit,
                                                 DateTime startDate
                                                 )
        {
            // Validate the transaction records and set unreadable transacitons status field with a message.
            foreach (var tran in trans)
            {
                // date - should be no older that one year and can not be a future date
                if (tran.Date < startDate || tran.Date > DateTime.Now)
                { tran.Status += "[Bad Date] "; }

                // Account - not really used but it should match the input account name
                if (tran.Account != EJAccount)
                { tran.Status += "[Bad Account] "; }

                // Activity - must be on the list of known activities
                if (!Activities.Contains(tran.Activity))
                { tran.Status += "[Unknown Activity] "; }

                // Quantity - reject records that have to large a transaction quantity
                if (tran.Quantity < -QuantityLimit || tran.Quantity > QuantityLimit)
                { tran.Status += "[Qty out of bounds] "; }

                // Currency - should always be CAD for Canadian
                if (tran.Currency != "CAD")
                { tran.Status += "[Not Candadian currency] "; }

                //  Commission - should always be zero
                if (tran.Commission != 0)
                { tran.Status += "[Incorrect commission] "; }

                // Price - the price can not be negative or greater than the price limit
                if (tran.Price < 0 || tran.Price > PriceLimit)
                { tran.Status += "[Price out of range ]"; }

                // Net Amount - the value of the transaction can not be greater than the transaction limit
                if (tran.NetAmount < -TansactionLimit || tran.NetAmount > TansactionLimit)
                { tran.Status += "[Net Amount out of range] "; }

                // NickName - not really used but it should match the input nickname
                if (tran.AccountNickname != EJNickName)
                { tran.Status += "[Bad Nickname] "; }
            }
        }

        #endregion ----------------------------- Private Methods --------------------------------------
    }
}
