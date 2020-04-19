using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment2_xmlParser
{
    class XmlParser
    {

        public static async Task ExtractTextFromXmlToFile(string xmlFilePath, string longNameToSearch,string outputFilePath) {
            var extractedText = await XmlParser.ExtractTextFromXml(xmlFilePath, longNameToSearch);
            if (!string.IsNullOrEmpty(extractedText))
            {
                FileUtility.SaveTextToFile(outputFilePath, extractedText);
                Console.WriteLine($"Extracted Text Saved to File -> {outputFilePath}");
            }
            else {
                Console.WriteLine($"long-name=\"{longNameToSearch}\" not found in file={xmlFilePath}.");
            }
        }
        private static async Task<string> ExtractTextFromXml(string xmlFilePath, string longNameToSearch)
        {
            if (xmlFilePath is null)
            {
                throw new ArgumentNullException(nameof(xmlFilePath));
            }

            if (longNameToSearch is null)
            {
                throw new ArgumentNullException(nameof(longNameToSearch));
            }

            XmlReaderSettings settings = new XmlReaderSettings() { IgnoreWhitespace = false };
            using var reader = XmlReader.Create(xmlFilePath, settings);
            var result =await Task.Run(()=>XmlParser.ExtractTextFromXmlReader(reader, longNameToSearch));
            //Console.WriteLine("From Method");
            //Console.WriteLine(result);
            return result.ToString();
        }

        private static StringBuilder ExtractTextFromXmlReader(XmlReader reader,string longNameToSearch)
        {
            StringBuilder result = new StringBuilder();
            reader.MoveToContent();
            //reader.ReadToDescendant("Section");//Content
            var riskFactorTagFound = false;
            var endSearch = false;
            var skippedATag = false;
            while (!reader.EOF && !endSearch)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        var longName = reader.GetAttribute("long-name");
                        if (reader.Name != "FilingToolDoc" && reader.Name != "Content" && longName != "Content" && longName != longNameToSearch)
                        {
                            if (!riskFactorTagFound)
                            {
                                //Console.WriteLine($"Skipping Childs for  {reader.Name} with long-name = {longName}");
                                reader.Skip();
                                skippedATag = true;
                                break;
                            }
                            break;
                        }
                        else if (longName == longNameToSearch)
                        {
                            riskFactorTagFound = true;
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        if (riskFactorTagFound)
                        {
                            result.Append(reader.Value);
                            //Console.WriteLine("Text :-" + reader.Value);
                        }
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        if (reader.Name == "Section" && riskFactorTagFound)
                        {
                            endSearch = true;
                        }
                        break;
                }
                if (!skippedATag)
                {
                    reader.Read();
                }
                else
                {
                    skippedATag = false;
                }
            }
            return result;
        }
    }
}
