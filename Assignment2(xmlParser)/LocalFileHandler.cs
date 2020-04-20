using System;
using System.IO;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Assignment2_Parser
{

    public class LocalFileHandler:IOutputHandler
    {
        private static readonly Object LockObj = new Object();
        private static LocalFileHandler instance;
        public string OutputFilePath { get; set; }
        public LocalFileHandler(IFileParser parser,string outputFilePath)
        {
            parser.textExtracted += HandleOutput;
            this.OutputFilePath = outputFilePath;
        }


        public void HandleOutput(Object sender, EventArgs e)
	    {
            if (e is StringEventArgs) { 
                //Console.WriteLine( "EventArgs=>>>>>>"+((StringEventArgs)e).Data);
                File.AppendAllLines(this.OutputFilePath, new[] { "\n" + ((StringEventArgs)e).Data });
            }
	    }

    }
}
