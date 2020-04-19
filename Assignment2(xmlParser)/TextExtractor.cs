using System;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace Assignment2_Parser
{
	public class TextExtractor
	{
        readonly IFileParser parser;
        readonly IOutputHandler outputHandler;
		public TextExtractor(IFileParser parser,IOutputHandler outputHandler)
		{
			this.parser = parser;
			this.outputHandler = outputHandler;
		}

        /// <summary>
        /// Extracts text from a Source File (using IFileParser and IOutputHandler provided by Caller )based on a long-name attribute
        /// </summary>
        /// <param name="filePath">Path to the source File.</param>
        /// <param name="longNameToSearchFor"> Long-name that we are supposed to search in file.</param>
        /// <param name="outputFilePath">Path to the Destination text File.</param>
        public async Task ExtractTextFromFile(string filePath, string longNameToSearchFor, string outputFilePath)
        {

            var extractedText = await this.parser.ExtractText(filePath, longNameToSearchFor);
            if (!string.IsNullOrEmpty(extractedText))
            {
                this.outputHandler.HandleOutput(outputFilePath, extractedText);
                Console.WriteLine($"Extracted Text Saved to File -> {outputFilePath}");
            }
            else
            {
                Console.WriteLine($"long-name=\"{longNameToSearchFor}\" not found in file={filePath}.");
            }

        }
    }

}
