
namespace CSV2QIF_Converter.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CsvFileTextBox = new System.Windows.Forms.TextBox();
            this.CsvFileLabel = new System.Windows.Forms.Label();
            this.OpenFileDialogButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.NicknameLabel = new System.Windows.Forms.Label();
            this.NicknameComboBox = new System.Windows.Forms.ComboBox();
            this.SecuritiesMappingFileLabel = new System.Windows.Forms.Label();
            this.SecuritiesMappingFileTextBox = new System.Windows.Forms.TextBox();
            this.EarliestDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.EarliestDateLabel = new System.Windows.Forms.Label();
            this.VerboseCheckBox = new System.Windows.Forms.CheckBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "CSV";
            this.openFileDialog.Multiselect = true;
            // 
            // CsvFileTextBox
            // 
            this.CsvFileTextBox.Location = new System.Drawing.Point(122, 42);
            this.CsvFileTextBox.Name = "CsvFileTextBox";
            this.CsvFileTextBox.Size = new System.Drawing.Size(825, 31);
            this.CsvFileTextBox.TabIndex = 0;
            // 
            // CsvFileLabel
            // 
            this.CsvFileLabel.AutoSize = true;
            this.CsvFileLabel.Location = new System.Drawing.Point(7, 45);
            this.CsvFileLabel.Name = "CsvFileLabel";
            this.CsvFileLabel.Size = new System.Drawing.Size(84, 25);
            this.CsvFileLabel.TabIndex = 1;
            this.CsvFileLabel.Text = "CSV File: ";
            // 
            // OpenFileDialogButton
            // 
            this.OpenFileDialogButton.Location = new System.Drawing.Point(953, 41);
            this.OpenFileDialogButton.Name = "OpenFileDialogButton";
            this.OpenFileDialogButton.Size = new System.Drawing.Size(45, 34);
            this.OpenFileDialogButton.TabIndex = 2;
            this.OpenFileDialogButton.Text = "...";
            this.OpenFileDialogButton.UseVisualStyleBackColor = true;
            this.OpenFileDialogButton.Click += new System.EventHandler(this.OpenFileDialogButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Enabled = false;
            this.ConvertButton.Location = new System.Drawing.Point(1343, 130);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(112, 34);
            this.ConvertButton.TabIndex = 3;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(122, 132);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(1203, 33);
            this.ErrorLabel.TabIndex = 4;
            // 
            // NicknameLabel
            // 
            this.NicknameLabel.AutoSize = true;
            this.NicknameLabel.Location = new System.Drawing.Point(1038, 46);
            this.NicknameLabel.Name = "NicknameLabel";
            this.NicknameLabel.Size = new System.Drawing.Size(99, 25);
            this.NicknameLabel.TabIndex = 6;
            this.NicknameLabel.Text = "Nickname: ";
            // 
            // NicknameComboBox
            // 
            this.NicknameComboBox.FormattingEnabled = true;
            this.NicknameComboBox.Items.AddRange(new object[] {
            "Meghan - RESP +",
            "Stephanie - RESP +",
            "Charlene-Bruce Joint",
            "Bruce - TFSA",
            "Charlene TFSA",
            "Bruce - RRSP",
            "Charlene RRSP",
            "MSBC"});
            this.NicknameComboBox.Location = new System.Drawing.Point(1143, 42);
            this.NicknameComboBox.Name = "NicknameComboBox";
            this.NicknameComboBox.Size = new System.Drawing.Size(312, 33);
            this.NicknameComboBox.TabIndex = 7;
            this.NicknameComboBox.SelectedIndexChanged += new System.EventHandler(this.NicknameComboBox_SelectedIndexChanged);
            // 
            // SecuritiesMappingFileLabel
            // 
            this.SecuritiesMappingFileLabel.AutoSize = true;
            this.SecuritiesMappingFileLabel.Location = new System.Drawing.Point(7, 90);
            this.SecuritiesMappingFileLabel.Name = "SecuritiesMappingFileLabel";
            this.SecuritiesMappingFileLabel.Size = new System.Drawing.Size(198, 25);
            this.SecuritiesMappingFileLabel.TabIndex = 9;
            this.SecuritiesMappingFileLabel.Text = "Securities Mapping File:";
            // 
            // SecuritiesMappingFileTextBox
            // 
            this.SecuritiesMappingFileTextBox.Enabled = false;
            this.SecuritiesMappingFileTextBox.Location = new System.Drawing.Point(227, 87);
            this.SecuritiesMappingFileTextBox.Name = "SecuritiesMappingFileTextBox";
            this.SecuritiesMappingFileTextBox.Size = new System.Drawing.Size(720, 31);
            this.SecuritiesMappingFileTextBox.TabIndex = 8;
            // 
            // EarliestDateTimePicker
            // 
            this.EarliestDateTimePicker.Location = new System.Drawing.Point(1143, 87);
            this.EarliestDateTimePicker.Name = "EarliestDateTimePicker";
            this.EarliestDateTimePicker.Size = new System.Drawing.Size(312, 31);
            this.EarliestDateTimePicker.TabIndex = 10;
            // 
            // EarliestDateLabel
            // 
            this.EarliestDateLabel.AutoSize = true;
            this.EarliestDateLabel.Location = new System.Drawing.Point(1024, 90);
            this.EarliestDateLabel.Name = "EarliestDateLabel";
            this.EarliestDateLabel.Size = new System.Drawing.Size(113, 25);
            this.EarliestDateLabel.TabIndex = 11;
            this.EarliestDateLabel.Text = "Earliest Date:";
            this.EarliestDateLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // VerboseCheckBox
            // 
            this.VerboseCheckBox.AutoSize = true;
            this.VerboseCheckBox.Location = new System.Drawing.Point(12, 130);
            this.VerboseCheckBox.Name = "VerboseCheckBox";
            this.VerboseCheckBox.Size = new System.Drawing.Size(102, 29);
            this.VerboseCheckBox.TabIndex = 12;
            this.VerboseCheckBox.Text = "Verbose";
            this.VerboseCheckBox.UseVisualStyleBackColor = true;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.CausesValidation = false;
            this.OutputTextBox.Enabled = false;
            this.OutputTextBox.Location = new System.Drawing.Point(12, 178);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(1443, 260);
            this.OutputTextBox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1467, 450);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.VerboseCheckBox);
            this.Controls.Add(this.EarliestDateLabel);
            this.Controls.Add(this.EarliestDateTimePicker);
            this.Controls.Add(this.SecuritiesMappingFileLabel);
            this.Controls.Add(this.SecuritiesMappingFileTextBox);
            this.Controls.Add(this.NicknameComboBox);
            this.Controls.Add(this.NicknameLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.OpenFileDialogButton);
            this.Controls.Add(this.CsvFileLabel);
            this.Controls.Add(this.CsvFileTextBox);
            this.Name = "MainForm";
            this.Text = "Edward Jones CSV to Quicken QIF Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox CsvFileTextBox;
        private System.Windows.Forms.Label CsvFileLabel;
        private System.Windows.Forms.Button OpenFileDialogButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label NicknameLabel;
        private System.Windows.Forms.ComboBox NicknameComboBox;
        private System.Windows.Forms.Label SecuritiesMappingFileLabel;
        private System.Windows.Forms.TextBox SecuritiesMappingFileTextBox;
        private System.Windows.Forms.DateTimePicker EarliestDateTimePicker;
        private System.Windows.Forms.Label EarliestDateLabel;
        private System.Windows.Forms.CheckBox VerboseCheckBox;
        private System.Windows.Forms.TextBox OutputTextBox;
    }
}

