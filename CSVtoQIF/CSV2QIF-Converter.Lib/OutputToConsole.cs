using System;
using System.Collections.Generic;

namespace Csv2QifConverter.Lib
{
    public static partial class CSV2QIFConverter
    {
        private static void OutputToConsole(List<Transaction> trans, string stage)
        {
            Console.WindowWidth = 196;

            switch (stage)
            {
                case "Input":
                    Console.WriteLine("  DATE   ACCT    ACTIVITY                   DESCRIPTION                                                                                             QTY   CURR   COMM      PRICE NET AMNT NICKNAME");
                    Console.WriteLine("======== ==== =============== ================================================================================================================== ======== ==== ======== ======== ======== ========");
                    foreach (var tran in trans)
                    {
                        if (tran.Status == null && tran.QIFDelimiter == null)
                        {
                            Console.WriteLine($"{tran.Date,-8:d} {tran.Account,-4} {tran.Activity,-15} " +
                            $"{(tran.Description.Length > 114 ? tran.Description.Substring(0, 114) : tran.Description),-114} {tran.Quantity,8} {tran.Currency,4} {tran.Commission,8} " +
                            $"{tran.Price,8} {tran.NetAmount,8} {tran.AccountNickname} {tran.Status}");
                        }
                    }
                    Console.WriteLine();
                    break;

                case "Validation":
                    Console.WriteLine("  DATE   ACCT    ACTIVITY                   DESCRIPTION                                                                          QTY   CURR   COMM    PRICE   NET AMNT   NICK     STATUS        ");
                    Console.WriteLine("======== ==== =============== =============================================================================================== ======== ==== ======== ======== ======== ======= =================");
                    foreach (var tran in trans)
                    {
                        if (tran.Status != null)
                        {
                            Console.WriteLine($"{tran.Date,-8:d} {tran.Account,-4} {tran.Activity,-15} " +
                            $"{(tran.Description.Length > 95 ? tran.Description.Substring(0, 95) : tran.Description),-95} {tran.Quantity,8} {tran.Currency,4} {tran.Commission,8} " +
                            $"{tran.Price,8} {tran.NetAmount,8} {tran.AccountNickname,-7} {tran.Status,-15}");
                        }
                    }
                    Console.WriteLine();
                    break;

                case "QifFields":
                    Console.WriteLine("  DATE      ACTIVITY                   SECURITY                                            PRICE       QTY     C  AMOUNT-T     AMOUNT-U    CATEGORY                DELIM");
                    Console.WriteLine("========== ========== ================================================================= =========== ========== = =========== =========== ========================= =====");
                    foreach (var tran in trans)
                    {
                        if (tran.QIFDelimiter == "^")
                        {
                            System.Console.WriteLine($"{tran.QIFDate,-10} {tran.QIFAction,-10} {tran.QIFSecurity,-65} {tran.QIFPrice,11} {tran.QIFQuantity,10} " +
                            $"{tran.QIFCleared} {tran.QIFUAmount,10} {tran.QIFTAmount,10} {tran.QIFCategory,-20} {tran.QIFDelimiter}");
                        }
                    }
                    Console.WriteLine();
                    break;

                case "QifWrite":
                    Console.WriteLine("  DATE      ACTIVITY                   SECURITY                                            PRICE       QTY     C  AMOUNT-T     AMOUNT-U    CATEGORY                DELIM");
                    Console.WriteLine("========== ========== ================================================================= =========== ========== = =========== =========== ========================= =====");
                    foreach (var tran in trans)
                    {
                        if (tran.Status == null)
                        {
                            Console.WriteLine($"{tran.QIFDate,-10} {tran.QIFAction,-10} {tran.QIFSecurity,-65} {tran.QIFPrice,11} {tran.QIFQuantity,10} " +
                            $"{tran.QIFCleared} {tran.QIFUAmount,10} {tran.QIFTAmount,10} {tran.QIFCategory,-20} {tran.QIFDelimiter}");
                        }
                    }
                    Console.WriteLine();
                    break;

                case "ExcludedWrite":
                    Console.WriteLine("  DATE      ACTIVITY                   DESCRIPTION                                                                QTY        PRICE       NET AMNT   NICK         STATUS");
                    Console.WriteLine("            /ACTION                    /SECURITY                                                                                         /U AMNT    /T AMNT");
                    Console.WriteLine("========== =============== ===================================================================================== ========== =========== ========== ========== ==================================");
                    foreach (var tran in trans)
                    {
                        if (tran.Status != null)
                        {
                            Console.WriteLine($"{tran.Date,-10:d} {tran.Activity,-15} {(tran.Description.Length > 85 ? tran.Description.Substring(0, 82)+"..." : tran.Description),-85} " +
                            $"{tran.Quantity,10} {tran.Price,11} {tran.NetAmount,10} " +
                            $"{tran.AccountNickname,10} {tran.Status,-30}");
                            Console.WriteLine($"{tran.QIFDate,-10} {tran.QIFAction,-15} {tran.QIFSecurity,-85} " +
                            $"{tran.QIFQuantity,10} {tran.QIFPrice,11} {tran.QIFUAmount,10} {tran.QIFTAmount,10} " +
                            $"{tran.QIFCategory,-30} ");
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}

