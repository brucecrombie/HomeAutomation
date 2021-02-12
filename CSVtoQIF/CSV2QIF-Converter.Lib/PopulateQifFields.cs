using System;
using System.Collections.Generic;

namespace Csv2QifConverter.Lib
{
    public static partial class CSV2QIFConverter
    {
        private static void PopulateQIFFields(List<Transaction> trans, Dictionary<string, string> SecuritiesMap)
        {
            // Build QIF transactions
            foreach (var tran in trans)
            {
                tran.QIFDate = $"D{tran.Date.Month}/{tran.Date.Day,2}'" + tran.Date.ToString("yy");

                switch (tran.Activity)
                {
                    case "Dividend":
                        PopulateDividend(SecuritiesMap, tran);
                        break;

                    case "Reinvested Dist":
                        PopulateReinvestDist(SecuritiesMap, tran);
                        break;

                    case "Buy":
                    case "Bought":
                        PopulateBuy(SecuritiesMap, tran);
                        break;

                    case "Sell":
                    case "Sold":
                        PopulateSell(SecuritiesMap, tran);
                        break;

                    case "Fee":
                        PopulateFee(tran);
                        break;

                    case "Tax Withheld":
                        PopulateTaxWithheld(tran);
                        break;

                    // Franklin Bisset, Capital Group special case
                    case "Reinvest":
                        PopulateReinvest(trans, SecuritiesMap, tran);
                        break;

                    default:
                        tran.Status += "[Unknown activity type] ";
                        break;
                }
            }
        }

        private static void PopulateDividend(Dictionary<string, string> SecuritiesMap, Transaction tran)
        {
            tran.QIFAction = "NDiv";
            foreach (var entry in SecuritiesMap)
            {
                if (tran.Description.StartsWith(entry.Key))
                {
                    tran.QIFSecurity = $"Y{entry.Value}";
                    break;
                }
            }
            if (tran.QIFSecurity != null)
            {
                tran.QIFCleared = "C";
                tran.QIFUAmount = $"U{tran.NetAmount:#,##0.00}";
                tran.QIFTAmount = $"T{tran.NetAmount:#,##0.00}";
                tran.QIFDelimiter = "^";
            }
            else
            {
                tran.Status += "[Unknown Secutrity Name] ";
            }
        }

        private static void PopulateReinvestDist(Dictionary<string, string> SecuritiesMap, Transaction tran)
        {
            tran.QIFAction = "NReinvDiv";
            foreach (var entry in SecuritiesMap)
            {
                if (tran.Description.StartsWith(entry.Key))
                {
                    tran.QIFSecurity = $"Y{entry.Value}";
                    break;
                }
            }
            if (tran.QIFSecurity != null)
            {

                tran.QIFCleared = "C";
                try
                {
                    decimal amount = decimal.Parse(tran.Description.Substring(tran.Description.IndexOf('$')),
                        System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowDecimalPoint);
                    tran.QIFPrice = $"I{amount / tran.Quantity:0.######}";
                    tran.QIFQuantity = $"Q{tran.Quantity:0.######}";
                    tran.QIFUAmount = $"U{amount:#,##0.00}";
                    tran.QIFTAmount = $"T{amount:#,##0.00}";
                    tran.QIFDelimiter = "^";
                }
                catch (FormatException)
                {
                    tran.Status += "[Unable to find value in description] ";
                }
            }
            else
            {
                tran.Status += "[Unknown Secutrity Name] ";
            }
        }

        private static void PopulateBuy(Dictionary<string, string> SecuritiesMap, Transaction tran)
        {
            tran.QIFAction = "NBuy";
            foreach (var entry in SecuritiesMap)
            {
                if (tran.Description.StartsWith(entry.Key))
                {
                    tran.QIFSecurity = $"Y{entry.Value}";
                    break;
                }
            }
            if (tran.QIFSecurity != null)
            {
                tran.QIFCleared = "C";
                tran.QIFPrice = $"I{-tran.NetAmount / tran.Quantity:#,##0.######}";
                tran.QIFQuantity = $"Q{tran.Quantity:#,##0.######}";
                tran.QIFUAmount = $"U{-tran.NetAmount:#,##0.00}";
                tran.QIFTAmount = $"T{-tran.NetAmount:#,##0.00}";
                tran.QIFDelimiter = "^";
            }
            else
            {
                tran.Status += "[Unknown Secutrity Name] ";
            }
        }

        private static void PopulateSell(Dictionary<string, string> SecuritiesMap, Transaction tran)
        {
            tran.QIFAction = "NSell";
            foreach (var entry in SecuritiesMap)
            {
                if (tran.Description.StartsWith(entry.Key))
                {
                    tran.QIFSecurity = $"Y{entry.Value}";
                    break;
                }
            }
            if (tran.QIFSecurity != null)
            {
                tran.QIFCleared = "C";
                tran.QIFPrice = $"I{tran.NetAmount / -tran.Quantity:#,##0.######}";
                tran.QIFQuantity = $"Q{-tran.Quantity:#,##0.######}";
                tran.QIFUAmount = $"U{tran.NetAmount:#,##0.00}";
                tran.QIFTAmount = $"T{tran.NetAmount:#,##0.00}";
                tran.QIFDelimiter = "^";
            }
            else
            {
                tran.Status += "[Unknown Secutrity Name] ";
            }
        }

        private static void PopulateFee(Transaction tran)
        {
            tran.QIFAction = "NCash";
            tran.QIFCleared = "C";
            tran.QIFUAmount = $"U{tran.NetAmount}";
            tran.QIFTAmount = $"T{tran.NetAmount}";
            tran.QIFCategory = $"LInvestment Fee";
            tran.QIFDelimiter = "^";
        }

        private static void PopulateTaxWithheld(Transaction tran)
        {
            tran.QIFAction = "NCash";
            tran.QIFCleared = "C";
            tran.QIFUAmount = $"U{tran.NetAmount}";
            tran.QIFTAmount = $"T{tran.NetAmount}";
            tran.QIFCategory = $"LTax";
            tran.QIFDelimiter = "^";
        }

        private static void PopulateReinvest(List<Transaction> trans, Dictionary<string, string> SecuritiesMap, Transaction tran)
        {
            string matchingTranKey = string.Empty;
            tran.QIFAction = "NReinvDiv";
            foreach (var entry in SecuritiesMap)
            {
                if (tran.Description.StartsWith(entry.Key))
                {
                    tran.QIFSecurity = $"Y{entry.Value}";
                    matchingTranKey = entry.Key;
                    break;
                }
            }

            if (tran.QIFSecurity == "YFranklin Bissett Core Plus Bond Fund O(110)"
                || tran.QIFSecurity == "YCapital Group Can Core Plus Fixed Income Series O(801)")
            {
                tran.QIFCleared = "C";
                tran.QIFPrice = $"I{-tran.NetAmount / tran.Quantity:0.######}";
                tran.QIFQuantity = $"Q{tran.Quantity:0.######}";
                tran.QIFUAmount = $"U{-tran.NetAmount:#,##0.00}";
                tran.QIFTAmount = $"T{-tran.NetAmount:#,##0.00}";
                tran.QIFDelimiter = "^";

                // find the matching 'other' transaction
                Transaction otherTran = trans.Find(x => x.Activity == "Other" &&
                x.Description.StartsWith(matchingTranKey) &&
                x.NetAmount == -tran.NetAmount);
                if (otherTran != null)
                {
                    otherTran.Status = "[Duplicate] ";
                }
                else
                {
                    tran.Status += "[Unable to find partner transaction] ";
                }
            }
            else
            {
                tran.Status += "[Unknown Secutrity Name] ";
            }
        }
    }
}