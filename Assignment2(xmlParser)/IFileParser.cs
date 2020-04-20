using System;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Assignment2_Parser
{
	public interface IFileParser
	{
		event EventHandler textExtracted;
		Task ExtractText(string filePath, string longNameToSearch);
	}
}
