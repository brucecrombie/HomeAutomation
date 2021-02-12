
using Csv2QifConverter.Lib;
using System;
using System.IO;

namespace CSV2QIF_Converter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "";
            string csvFileName = "";
            string EJNickName = "";
            FileInfo csvFile;
            FileInfo Ej2QickenSecurites;
            FileInfo qifFile;
            FileInfo rejectedTransFile;

            const string securitiesNameMapFileName = @"EdwardJonesToQuickenSecuritiesNameMap.csv";
            #region -------------------------------- Data Input -----------------------------------
            int argsLenth = args.Length;

            switch (argsLenth)
            {
                case 0: // No args are passed. Input statements for path, filename, and nickname
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the path to the CSV file:");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    filePath = System.Console.ReadLine();
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the CSV file name");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    csvFileName = System.Console.ReadLine();
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the account nickname");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    EJNickName = System.Console.ReadLine();
                    break;

                case 1:
                    break;

                default:
                    break;
            }

            #endregion----------------------------- Data Input -----------------------------------
            #region--------------------------- Input Data Validation -----------------------------

            // Input error checking.
            // 1st check to see if the file path is valid.
            // If the path doesn't exist then just exit with an error message.
            if (!Directory.Exists(filePath))
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file path [{filePath}] does not exist. Program teminated.");
                return;
            }

            // 2nd check to see if the file name is valid.
            csvFile = new FileInfo(filePath + "\\" + csvFileName);
            if (!csvFile.Exists)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file [{csvFile.FullName}] does not exist. Program teminated.");
                return;
            }

            // 3rd check for CVS file extension.
            if (csvFile.Extension != "CVS")
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file extension for [{csvFile.FullName}] is not CVS. Program teminated.");
                return;
            }

            // 4th check that the mapping file for the Edward Jones securities to Quicken exists.
            Ej2QickenSecurites = new FileInfo($"{csvFile.DirectoryName}\\{securitiesNameMapFileName}");
            if (!Ej2QickenSecurites.Exists)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file [{Ej2QickenSecurites}] was expected to exist in the same folder as");
                System.Console.WriteLine($"[{csvFile}]. It is required to mapp Edward Jones security names to Quicken security names. Program teminated.");
                return;
            }

            // 5th check that a non empty nickname for the account has been entered.
            if (String.IsNullOrEmpty(EJNickName))
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"A nickname is required. Program teminated.");
                return;
            }

            #endregion------------------------ Input Data Validation -----------------------------

            qifFile = new FileInfo($"{csvFile.FullName[0..^3]}QIF");
            rejectedTransFile = new FileInfo($"{csvFile.Directory}\\EJECTED-{csvFileName[0..^3]}CSV");

            string EJAccount = "Cash";
            decimal quantityLimit = 2000;
            decimal priceLimit = 3000;
            Decimal transLimit = 20000;

            CSV2QIFConverter.Convert(csvFile, qifFile, rejectedTransFile, Ej2QickenSecurites,
                                     EJAccount, EJNickName, quantityLimit, priceLimit, transLimit);
        }
    }
}
