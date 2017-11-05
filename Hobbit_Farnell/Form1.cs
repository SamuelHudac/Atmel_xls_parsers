using CsvHelper;
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
using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Hobbit_Farnell;

namespace Hobbit_Farnell
{
    public partial class Form1 : Form
    {
        PriceListController _priceListController = new PriceListController();

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.NumCount1.Value == 0 && this.NumCount2.Value == 0 && this.NumCount3.Value == 0 && this.NumCount4.Value == 0)
                {
                    MessageBox.Show("You have to fill at least one field (per series)", "Cannot generate file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (this.NumDpsPerOneSeries.Value == 0)
                {
                    MessageBox.Show("You have to fill field how much dps is a one series","Cannot generate file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!string.IsNullOrEmpty(this.directoryPath.Text))
                {
                    Cursor = Cursors.WaitCursor;
                    var reader = new StreamReader(File.OpenRead(this.directoryPath.Text.ToString()));

                    // Insert code to read the stream here.
                    _priceListController.ParseAltiumExcel(this.directoryPath.Text, (int)this.NumCount1.Value, (int)this.NumCount2.Value, (int)this.NumCount3.Value, (int)this.NumCount4.Value, (int)this.NumDpsPerOneSeries.Value);
                    Cursor = Cursors.Arrow;

                    MessageBox.Show("Price list was generated", "Price list generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not generate file. Original error: " + ex.Message, "Cannot generate" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        this.directoryPath.Text = openFileDialog1.FileName.ToString();
                        this.GenerateFile.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
