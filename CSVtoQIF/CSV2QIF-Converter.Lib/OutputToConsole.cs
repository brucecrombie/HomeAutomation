using System.Collections.Generic;

namespace Csv2QifConverter.Lib
{
    public static partial class CSV2QIFConverter
    {
        private static void OutputToConsole(List<Transaction> trans)
        {
            foreach (var tran in trans)
            {
                if (tran.Status == null && tran.QIFDelimiter == null)
                {
                    System.Console.WriteLine($"{tran.Date.ToString("d")} {tran.Account,-4} {tran.Activity,-14} " +
                    $"{tran.Description,-40} {tran.Quantity,8} {tran.Currency,8} {tran.Commission,8} " +
                    $"{tran.Price,8} {tran.NetAmount,8} {tran.AccountNickname} {tran.Status}");
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();

            foreach (var tran in trans)
            {
                if (tran.QIFDelimiter == "^")
                {
                    System.Console.WriteLine($"{tran.QIFDate,-10} {tran.QIFAction,-10} {tran.QIFSecurity,-45} {tran.QIFPrice,11} {tran.QIFQuantity,10} " +
                $"{tran.QIFCleared} {tran.QIFUAmount,10} {tran.QIFTAmount,10} {tran.QIFCategory} {tran.QIFDelimiter}");

                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();

            foreach (var tran in trans)
            {
                if (tran.Status != null)
                    if (!tran.Status.Contains("[Duplicate]"))
                    {
                        System.Console.WriteLine($"{tran.Date.ToString("d")} {tran.Account,-4} {tran.Activity,-14} " +
                        $"{tran.Description,-40} {tran.Quantity,8} {tran.Currency,8} {tran.Commission,8} " +
                        $"{tran.Price,8} {tran.NetAmount,8} {tran.AccountNickname} {tran.Status}");
                    }
            }
        }
    }
}

