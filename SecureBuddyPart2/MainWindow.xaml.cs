using demo;
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

        AI_Check ai;

        Interest_handler interests;

        Clean_input clean;

        public MainWindow()
        {
            InitializeComponent();

            new respond(reply, ignore);

            ai = new AI_Check(reply, ignore);

            interests = new Interest_handler();

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
        {
            username =
                check_name.submit_name(usernames_input, chats);

            username_grid.Visibility = Visibility.Hidden;

            chat_grid.Visibility = Visibility.Visible;
        }

        private void send(object sender, RoutedEventArgs e)
        {
            string rawQuestion =
                question.Text.ToString().Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
                error_method("SecureBuddy", "Please enter a question.");

                return;
            }

            string questions =
                clean.RemoveSpecialCharacters(rawQuestion);

            error_method(username, rawQuestion);

            if (questions.Contains("interested"))
            {
                string[] words = questions.Split(' ');

                string interest_message =
                    interests.save_interest(
                        words,
                        ignore,
                        username);

                error_method("SecureBuddy", interest_message);
            }

            auto_show_interest();

            string response =
                ai.ai_check(questions, username);

            error_method("SecureBuddy", response);

            question.Clear();
        }

        private void error_method(string v1, string v2)
        {
            chats.Items.Add(v1 + ": " + v2);

        }

        private void auto_show_interest()
        {
            if (counting == 3)
            {
                string reminder =
                    interests.reminder(username);

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

