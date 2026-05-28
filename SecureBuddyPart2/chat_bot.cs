using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SecureBuddyPart2
{//start of namespace
    public class AI_Check
    {//start of class
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();

        public AI_Check(ArrayList replies, ArrayList ignores)
        {//start of contsructor
            reply = replies;
            ignore = ignores;
        }//end of constructor

        public string ai_check(string questions, string username)
        {
            if (string.IsNullOrWhiteSpace(questions))
            {
                return "Please enter a valid question.";
            }

            string[] words = questions.ToLower().Split(
                new char[] { ' ', ',', '.', '?', '!', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            bool found = false;

            string message = string.Empty;

            Random indexer = new Random();

            List<string> per_word = new List<string>();

            List<string> answers_found = new List<string>();

            foreach (string word in words)
            {
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue;

                per_word.Clear();

                bool wordFound = false;

                foreach (string answer in reply)
                {
                    if (answer.ToLower().Contains(word))
                    {
                        wordFound = true;

                        per_word.Add(answer);
                    }
                }

                if (wordFound && per_word.Count > 0)
                {
                    found = true;

                    int indexing = indexer.Next(0, per_word.Count);

                    answers_found.Add(per_word[indexing]);
                }
            }

            if (found && answers_found.Count > 0)
            {
                answers_found = answers_found.Distinct().ToList();

                foreach (string per_answer in answers_found)
                {
                    message += per_answer + "\n";
                }

                return message.TrimEnd('\n');
            }
            else
            {
                string[] fallbackMessages =
                {
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite get that. Try asking about cyber security topics.",
                    "Hmm, I'm not sure how to respond to that.",
                    "Please ask about cyber security, technology or programming.",
                    "I do not have information on that topic yet."
                };

                Random random = new Random();

                return fallbackMessages[random.Next(fallbackMessages.Length)];
            }
        }
    }//end of class

}//end of namespace