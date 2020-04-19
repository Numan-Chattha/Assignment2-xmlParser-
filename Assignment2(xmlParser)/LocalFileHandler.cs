using System;

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
        private LocalFileHandler()
        {

        }
        public static LocalFileHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (LockObj)
                    {
                        if (instance == null)
                        {
                            instance = new LocalFileHandler();
                        }
                    }
                }
                return instance;
            }
        }

        public void HandleOutput(string outputFilePath, string text)
	    {
		    //Console.WriteLine($"path = > {outputFilePath}, text length ={text.Length}Bytes");
		    System.IO.File.WriteAllText(outputFilePath, text);
	    }
    }
}
