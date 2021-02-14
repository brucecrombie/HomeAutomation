
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
            int daysOld = 365;
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
                    csvFileName = filePath + "\\" + csvFileName;
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the account nickname");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    EJNickName = System.Console.ReadLine();
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the maximum days before now");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    daysOld = int.Parse(System.Console.ReadLine());
                    break;

                case 1: // Assumes the full filename including path has been passed as an argument.
                    csvFileName = args[0];
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the account nickname");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    EJNickName = System.Console.ReadLine();
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the maximum days before now");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    daysOld = int.Parse(System.Console.ReadLine());
                    break;

                case 2: // Assume the full filename including path and the nickname has been passed as arguments.
                    csvFileName = args[0];
                    EJNickName = args[1];
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine($"Enter the maximum days before now");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    daysOld = int.Parse(System.Console.ReadLine());
                    break;

                case 3: // Assume the full filename including path, the nickname, and days old have been passed as arguments.
                    csvFileName = args[0];
                    EJNickName = args[1];
                    daysOld = int.Parse(args[2]);
                    break;

                default:
                    break;
            }

            #endregion----------------------------- Data Input -----------------------------------
            #region--------------------------- Input Data Validation -----------------------------

            // Input error checking.
            csvFile = new FileInfo(csvFileName);

            // 1st check to see if the file is valid.
            // If the file doesn't exist then just exit with an error message.
            if (!csvFile.Exists)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file [{csvFile.FullName}] does not exist. Program teminated.");
                return;
            }

            // 2nd check for CVS file extension.
            if (csvFile.Extension.ToUpper() != ".CSV")
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file extension for [{csvFile.FullName}] is not CVS. Program teminated.");
                return;
            }

            // 3rd check that the mapping file for the Edward Jones securities to Quicken exists.
            Ej2QickenSecurites = new FileInfo($"{csvFile.DirectoryName}\\{securitiesNameMapFileName}");
            if (!Ej2QickenSecurites.Exists)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The file [{Ej2QickenSecurites}] was expected to exist in the same folder as");
                System.Console.WriteLine($"[{csvFile}]. It is required to mapp Edward Jones security names to Quicken security names. Program teminated.");
                return;
            }

            // 4th check that a non empty nickname for the account has been entered.
            if (String.IsNullOrEmpty(EJNickName))
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"A nickname is required. Program teminated.");
                return;
            }

            // th check that days old has a meaningfull value - default to 365.
            if (daysOld <= 0)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Days old can not be less than or equal to zero - default value of 365 was used.");
                daysOld = 365;
                return;
            }

            #endregion------------------------ Input Data Validation -----------------------------

            qifFile = new FileInfo($"{csvFile.FullName[0..^3]}QIF");
            rejectedTransFile = new FileInfo($"{csvFile.Directory}\\REJECTED-{csvFile.Name[0..^3]}CSV");

            string EJAccount = "Cash";
            decimal quantityLimit = 2000;
            decimal priceLimit = 3000;
            Decimal transLimit = 20000;
            DateTime startDate = DateTime.Now - new TimeSpan(daysOld, 0, 0, 0);

            CSV2QIFConverter.Convert(csvFile, qifFile, rejectedTransFile, Ej2QickenSecurites,
                                     EJAccount, EJNickName, quantityLimit, priceLimit, transLimit,startDate);
        }
    }
}
