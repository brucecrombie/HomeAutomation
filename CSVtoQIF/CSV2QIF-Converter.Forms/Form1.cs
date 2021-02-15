using Csv2QifConverter.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV2QIF_Converter.Forms
{
    public partial class MainForm : Form
    {
        FileInfo csvFile;
        FileInfo Ej2QickenSecurites;
        FileInfo qifFile;
        FileInfo rejectedTransFile;

        const string securitiesNameMapFileName = @"EdwardJonesToQuickenSecuritiesNameMap.csv";

        public MainForm()
        {
            string filePath = "";
            string csvFileName = "";
            string EJNickName = "";
            int daysOld = 365;

            InitializeComponent();
            EarliestDateTimePicker.Value = DateTime.Now - new TimeSpan(365, 0, 0, 0);


        }

        private void OpenFileDialogButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            csvFile = new FileInfo(openFileDialog.FileName);
            Ej2QickenSecurites = new FileInfo($"{csvFile.DirectoryName}\\{securitiesNameMapFileName}");

            CsvFileTextBox.Text = csvFile.FullName;
            SecuritiesMappingFileTextBox.Text = Ej2QickenSecurites.FullName;

            // 1st check to see if the file is valid.
            if (!csvFile.Exists)
            {
                ErrorLabel.Text= $"The file [{csvFile.FullName}] does not exist.";
                ConvertButton.Enabled = false;
            }

            // 2nd check for CVS file extension.
            else if (csvFile.Extension.ToUpper() != ".CSV")
            {
                ErrorLabel.Text = $"The file extension for [{csvFile.FullName}] is not CVS.";
                ConvertButton.Enabled = false;
            }

            // 3rd check that the mapping file for the Edward Jones securities to Quicken exists.
            else if (!Ej2QickenSecurites.Exists)
            {
                ErrorLabel.Text = $"The file [{Ej2QickenSecurites}] was expected to exist in the same folder as [{csvFile}].\n" + 
                   $"It is required to map Edward Jones security names to Quicken security names. Program teminated.";
                ConvertButton.Enabled = false;
            }

            // 4th check that the earliest date is meaningful. The default is one year ago.
            else if (!Ej2QickenSecurites.Exists)
            {
                ErrorLabel.Text = $"The file [{Ej2QickenSecurites}] was expected to exist in the same folder as [{csvFile}].\n" +
                   $"It is required to map Edward Jones security names to Quicken security names. Program teminated.";
                ConvertButton.Enabled = false;
            }


            else
            {
                ErrorLabel.Text = "";
                ConvertButton.Enabled = true;
            }


        }




    private void ConvertButton_Click(object sender, EventArgs e)
        {
            qifFile = new FileInfo($"{csvFile.FullName[0..^3]}QIF");
            rejectedTransFile = new FileInfo($"{csvFile.Directory}\\REJECTED-{csvFile.Name[0..^3]}CSV");

            string EJAccount = "Cash";
            decimal quantityLimit = 2000;
            decimal priceLimit = 3000;
            Decimal transLimit = 20000;

            OutputTextBox.Text += $"Starting {csvFile.FullName}...\r\n";
            CSV2QIFConverter.Convert(csvFile, qifFile, rejectedTransFile, Ej2QickenSecurites,
                                     EJAccount, NicknameComboBox.Text, quantityLimit, priceLimit, transLimit, EarliestDateTimePicker.Value);
            OutputTextBox.Text += $"Completed conversion of {csvFile.FullName}.\r\n";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NicknameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
