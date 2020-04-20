using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment2_Parser
{
    class XmlParser : IFileParser
    {

        private static readonly Object LockObj = new Object();
        private static XmlParser instance;
        public event EventHandler textExtracted;
        private XmlParser()
        {

        }
        internal virtual void OnLineExtraction(string eventData)
        {
            textExtracted?.Invoke(this, new StringEventArgs(eventData));
        }
        public static XmlParser Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (LockObj)
                    {
                        if (instance == null)
                        {
                            instance = new XmlParser();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Extracts text from an XML File based on a long-name attribute
        /// </summary>
        /// <param name="xmlFilePath">Path to the source XML File.</param>
        /// <param name="longNameToSearch"> Long-name that we are supposed to search in file.</param>
        public async Task ExtractText(string filePath, string longNameToSearch)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (longNameToSearch is null)
            {
                throw new ArgumentNullException(nameof(longNameToSearch));
            }

            XmlReaderSettings settings = new XmlReaderSettings() { IgnoreWhitespace = false };
            using var reader = XmlReader.Create(filePath, settings);
            var result =await Task.Run(()=>this.ExtractTextFromXmlReader(reader, longNameToSearch));
            if (result)
            {
                Console.WriteLine("Text Extracted Successfully!");
            }
        }

        private bool ExtractTextFromXmlReader(XmlReader reader,string longNameToSearch)
        {
            reader.MoveToContent();
            //reader.ReadToDescendant("Section");//Content
            var riskFactorTagFound = false;
            var endSearch = false;
            var skippedATag = false;
            var textFound = false;
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
                            if (!string.IsNullOrEmpty(reader.Value)) {
                                this.OnLineExtraction(reader.Value);
                                textFound = true;
                            }
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
            return textFound;
        }
    }
}
