﻿using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;  // Přidáváme alias pro Excel
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text;

namespace HangManV01
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            LoadWordsIntoTextBox();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) //setting for adding hint and word to excel
        {
            if (string.IsNullOrWhiteSpace(Word.Text))
            {
                MessageBox.Show("Prosím, zadejte slovo do pole 'Word'.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Hint.Text))
            {
                MessageBox.Show("Prosím, zadejte nápovědu do pole 'Hint'.");
                return;
            }

            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel není správně nainstalován!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx"); // path for excel 
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                }
                else
                {
                    excelWorkbook = excelApp.Workbooks.Add();
                    excelWorkbook.SaveAs(workbookPath);
                }

                excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                int rowIndex = 1;

                while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null) 
                {
                    rowIndex++;
                }

                excelWorksheet.Cells[rowIndex, 1] = Word.Text; // excel setting for saving hint and word next to each other
                excelWorksheet.Cells[rowIndex, 2] = Hint.Text;

                excelWorkbook.Save();

                LoadWordsIntoTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo k chybě: " + ex.Message);
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close();
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel není správně nainstalován!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx");
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    bool wordFound = false;

                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        if (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString().Trim() == Delete.Text.Trim())
                        {
                            wordFound = true;
                            break;
                        }
                        rowIndex++;
                    }

                    if (wordFound)
                    {
                        Excel.Range rowRange = (Excel.Range)excelWorksheet.Rows[rowIndex]; // setting for deleting words from database - if the word is deleted the hint is deleted too.
                        rowRange.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        excelWorkbook.Save();
                    }
                    else
                    {
                        MessageBox.Show("Slovo nebylo nalezeno v Excelu.");
                    }
                }
                else
                {
                    MessageBox.Show("Excel soubor neexistuje!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo k chybě: " + ex.Message);
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false, Type.Missing, Type.Missing);
                LoadWordsIntoTextBox();
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        private void LoadWordsIntoTextBox()
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel není správně nainstalován!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx");
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    StringBuilder sb = new StringBuilder();

                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)  // Loop through the cells in the worksheet until an empty cell is encountered.
                    {
                        string word = ((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString();
                        string hint = ((Excel.Range)excelWorksheet.Cells[rowIndex, 2]).Value2.ToString();

                        sb.AppendLine(word + " + " + hint);
                        rowIndex++;
                    }

                    TextBoxWordsPreview.Text = sb.ToString();
                }
                else
                {
                    MessageBox.Show("Excel soubor neexistuje!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo k chybě: " + ex.Message);
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false, Type.Missing, Type.Missing);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        private void Delete_GotFocus(object sender, RoutedEventArgs e) // Clear the Delete TextBox when it receives focus.
        {
            Delete.Text = string.Empty;
        }
        private void Word_GotFocus(object sender, RoutedEventArgs e) // Clear the Word TextBox when it receives focus.
        {
            Word.Text = string.Empty;
        }

        private void Hint_GotFocus(object sender, RoutedEventArgs e) // Clear the Hint TextBox when it receives focus.
        {
            Hint.Text = string.Empty;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) // Closes the current window and shows the owner window if it is of type Menu
        {
            this.Close();

            if (this.Owner != null && this.Owner is Menu)
            {
                this.Owner.Show();
            }
        }
    }
}
