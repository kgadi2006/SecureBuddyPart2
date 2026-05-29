using SecureBuddyPart2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace SecureBuddyPart2
{
    //start of namespace
    public partial class MainWindow : Window
    {//start of class
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();

        user_name check_name = new user_name();

        string username = string.Empty;

        int counting = 0;

        chat_bot ai;

        Interest_handler interestsHandler;

        Clean_input clean;
        private string tempName;

        public MainWindow()
        {
            InitializeComponent();

            new respond(reply, ignore);

            ai = new chat_bot(reply, ignore);

            interestsHandler = new Interest_handler();

            clean = new Clean_input();

            voice_greeting greet = new voice_greeting();

            greet.greet();
        }

        private void proceed(object sender, RoutedEventArgs e)
        {
            home_grid.Visibility = Visibility.Hidden;

            username_grid.Visibility = Visibility.Visible;
        }

        private void submit_name(object sender, RoutedEventArgs e)
        {//start of submit name method
            username = check_name.submit_name(usernames_input, chats);
            if (string.IsNullOrWhiteSpace(username))

            {//start of if statement

                return;
            }//end of if statement
            
            username_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible;
        }//end of submit name method

        private void send(object sender, RoutedEventArgs e)
        {
            string rawQuestion = question.Text.ToString().Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
               
                error_method("SecureBuddy", "Please enter a question.");

                return;
            }

            string questions = clean.RemoveSpecialCharacters(rawQuestion);

            error_method(username, rawQuestion);

            if (questions.Contains("interested"))
            {
                string[] words = questions.Split(' ');

                string interest_message =
                    interestsHandler.save_interest(
                        words,
                        ignore,
                        username);

                error_method("SecureBuddy", interest_message);
            }

            auto_show_interest();

            string response = ai.ai_check(questions, username);

            error_method("SecureBuddy", response);

            question.Clear();
        }

        private void error_method(string name, string message)
        {
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5),
                BorderThickness = new Thickness(1)
            };

            if (name.ToLower().Contains("securebuddy"))
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                messageBorder.BorderBrush = Brushes.Black;
            }
            else
            {
                messageBorder.Background = Brushes.Black;
                messageBorder.BorderBrush = Brushes.White;
            }

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Cyan
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.White
            });

            messageBorder.Child = messageText;

            chats.Items.Add(messageBorder);
        }

        private void auto_show_interest()
        {
            if (counting == 3)
            {
                string reminder =
                    interestsHandler.reminder(username);

                if (!string.IsNullOrWhiteSpace(reminder))
                {
                    error_method("SecureBuddy", reminder);
                }

                counting = 0;
            }
            else
            {
                counting += 1;
            }
        }
    }//end of class
}//end of namespace

