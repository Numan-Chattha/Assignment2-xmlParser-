using System;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Assignment2_Parser
{
	public interface IFileParser
	{
		Task<string> ExtractText(string filePath, string longNameToSearch);
	}
}
