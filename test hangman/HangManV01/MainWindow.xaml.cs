﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HangManV01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentWord;
        private string currentHint;

        private int failedAttempts = 0;
        private List<string> imagePaths;

        public MainWindow()
        {
            InitializeComponent();
            LoadWordFromExcel();
        }

        private void LoadWordFromExcel()
        {
            string excelFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Data - Excel", "Words.xlsx");

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(excelFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;  

            int rowCount = xlRange.Rows.Count;

            Random random = new Random();  // selects random word from excel and the hint exactly next to the selected word
            int row = random.Next(1, rowCount + 1);

            currentWord = xlRange.Cells[row, 1].Value2.ToString();
            currentHint = xlRange.Cells[row, 2].Value2.ToString();

            WordForGuess.Text = string.Join(" ", currentWord.Select(c => '_')); // replaces letters with _
            Hint.Text = currentHint;

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menuWindow = new Menu();
            menuWindow.Show();
            this.Close();
        }
        private void DisableAllLetterButtons()       // after the game is finished disables all letters so player can't guess anymore
        {
            foreach (var child in LettersGrid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;
            string selectedLetter = button.Content.ToString();

            if (currentWord.IndexOf(selectedLetter, StringComparison.OrdinalIgnoreCase) >= 0) // if player guess correct letter replace _ with the letter
            {
                RevealLetter(selectedLetter);

                if (!WordForGuess.Text.Contains("_"))
                {
                    MessageBox.Show("CONGRATULATION YOU WON!", "Výhra", MessageBoxButton.OK, MessageBoxImage.Information); //appears when player guesses corectly whole word
                    DisableAllLetterButtons();
                }
            }
            else
            {
                failedAttempts++;

                if (failedAttempts <= 5) 
                {
                    UpdateHangmanImage(failedAttempts);
                }
                if (failedAttempts == 5)
                {
                    MessageBox.Show("YOU LOST! CORRECT WORD WAS: " + currentWord, "Prohra", MessageBoxButton.OK, MessageBoxImage.Warning); //appears when player spends all his attempts
                    DisableAllLetterButtons();
                }
            }

            button.IsEnabled = false;
        }


        private void RevealLetter(string selectedLetter)
        {
            var wordArray = WordForGuess.Text.Split(' ').Select((ch, index) =>
            currentWord[index].ToString().Equals(selectedLetter, StringComparison.OrdinalIgnoreCase) ?
            currentWord[index].ToString() : ch.ToString()).ToArray();

            WordForGuess.Text = string.Join(" ", wordArray);
        }



        private void UpdateHangmanImage(int attemptNumber) // after every failed attempt show new stage immage of hangman
        {
            string imagePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", $"hangman{attemptNumber}.bmp");
            HangmanImage.Source = new BitmapImage(new Uri(imagePath));
        }
    }
}

