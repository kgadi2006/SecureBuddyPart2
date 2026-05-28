using System.Collections;

namespace SecureBuddyPart2
{
    internal class chat_bot
    {
        private ArrayList reply;
        private ArrayList ignore;

        public chat_bot(ArrayList reply, ArrayList ignore)
        {
            this.reply = reply;
            this.ignore = ignore;
        }
    }
}