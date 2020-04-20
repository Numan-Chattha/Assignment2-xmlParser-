using System;

namespace Assignment2_Parser
{
    public class StringEventArgs : EventArgs
    {
        private readonly string data;

        public StringEventArgs(string test)
        {
            data = test;
        }

        public string Data
        {
            get { return data; }
        }
    }
}