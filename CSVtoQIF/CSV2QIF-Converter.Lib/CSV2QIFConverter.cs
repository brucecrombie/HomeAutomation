using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            decimal TransactionLimit
            )
        {
            System.Console.WriteLine($"csvFile: {csvFile}");
            System.Console.WriteLine($"qifFile: {qifFile}");
            System.Console.WriteLine($"rejectedTransFile: {rejectedTransFile}");
            System.Console.WriteLine($"Ej2QickenSecurites: {Ej2QickenSecurites}");
            System.Console.WriteLine($"EJAccount: {EJAccount}");
            System.Console.WriteLine($"EJNickName: {Ej2QickenSecurites}");
            System.Console.WriteLine($"QuantityLimit: {EJNickName}");
            System.Console.WriteLine($"PriceLimit: {PriceLimit}");
            System.Console.WriteLine($"TransactionLimit: {TransactionLimit}");




        }
    }
}
