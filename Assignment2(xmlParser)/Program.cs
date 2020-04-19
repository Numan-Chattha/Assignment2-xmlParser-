using System;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment2_xmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var longNameToSearchFor = "Risk Factors";
            var xmlFilePath = @"../../../SEC-0000950123-09-019360.xml";
            var outputFilePath = @$"../../../Results({longNameToSearchFor}).txt";
            var result = XmlParser.ExtractTextFromXmlToFile(xmlFilePath, longNameToSearchFor,outputFilePath);
            //Console.WriteLine("Main Thread=>");
            //Console.WriteLine(result);
            Task.WaitAll(result);
        }
     
    }
}


