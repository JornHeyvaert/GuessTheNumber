using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace GuessTheNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int numberToGuess = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (!rbEasy.IsChecked.Value && !rbMedium.IsChecked.Value && !rbHard.IsChecked.Value && !rbVeryHard.IsChecked.Value)
            {
                lblMessage.Text = "You must choose a difficulty!!!";
                btnValidate.IsEnabled = false;
                txtGuessNumber.Text = string.Empty;
                lblGeneratedNumber.Content = string.Empty;
            }
            else
            {
                btnNewGame.IsEnabled = false;
                btnValidate.IsEnabled = true;
                txtGuessNumber.Text = string.Empty;
                lblMessage.Text = string.Empty;
                lblGeneratedNumber.Content = string.Empty;
                EnableDifficulty(false);
                numberToGuess = GenerateNumber();
            }
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGuessNumber.Text))
            {
                lblMessage.Text = (Convert.ToInt32(txtGuessNumber.Text) == numberToGuess) ? "You Win!!!" : "You Lose!!!";
                lblGeneratedNumber.Content = numberToGuess.ToString();
                btnValidate.IsEnabled = false;
                btnNewGame.IsEnabled = true;
                ResetDifficulty();
                EnableDifficulty(true);
            }
            else
            {
                lblMessage.Text = "You must input a number to validate!!!";
            }
        }

        private void ResetDifficulty()
        {
            rbEasy.IsChecked = false;
            rbMedium.IsChecked = false;
            rbHard.IsChecked = false;
            rbVeryHard.IsChecked = false;
        }

        private void EnableDifficulty(bool isActive)
        {
            rbEasy.IsEnabled = isActive;
            rbMedium.IsEnabled = isActive;
            rbHard.IsEnabled = isActive;
            rbVeryHard.IsEnabled = isActive;
        }

        #region Generate number to guess

        private int GenerateNumber()
        {
            Random generator = new Random();
            if (rbEasy.IsChecked.Value)
            {
                return generator.Next(1, 10);
            }
            else if (rbMedium.IsChecked.Value)
            {
                return generator.Next(1, 100);
            }
            else if (rbHard.IsChecked.Value)
            {
                return generator.Next(1, 1000);
            }
            else if (rbVeryHard.IsChecked.Value)
            {
                return generator.Next(1, 10000);
            }
            return 0;
        }

        #endregion Generate number to guess

        #region Allow only numbers in textbox

        private void txtGuessNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        #endregion Allow only numbers in textbox
    }
}