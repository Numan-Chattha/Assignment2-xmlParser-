using System;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment2_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var longNameToSearchFor = "Risk Factors";
            var inputFilePath = @"../../../SEC-0000950123-09-019360.xml";
            var outputFilePath = @$"../../../Results({longNameToSearchFor}).txt";
            //get Singleton instance of LocalFileHandler to store file on Local Disk
            IOutputHandler outputHandler = LocalFileHandler.Instance;
            //get Singleton instance of XmlParser to parse XML File
            IFileParser parser = XmlParser.Instance;



            //prepare Object of the class that will handle program flow and provide dependencies
            TextExtractor extractor = new TextExtractor(parser, outputHandler);
            //start the text extraction process Asynchronously
            var task = extractor.ExtractTextFromFile(inputFilePath, longNameToSearchFor, outputFilePath);
            
            //do some other stuff Asynchronously

            //wait for above task to complete so that console window stays open untill that task has displayed its output message
            Task.WaitAll(task);
        }

    }
}


