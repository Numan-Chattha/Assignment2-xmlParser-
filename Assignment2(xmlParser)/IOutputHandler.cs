using System;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Assignment2_Parser
{
	public interface IOutputHandler
	{
		void HandleOutput(string fileName, string  outputText);
	}
}
