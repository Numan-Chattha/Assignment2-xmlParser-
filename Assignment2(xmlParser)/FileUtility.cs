using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public static class FileUtility
{
	
	public static void SaveTextToFile(string outputFilePath, string text)
	{
		//Console.WriteLine($"path = > {outputFilePath}, text length ={text.Length}Bytes");
		System.IO.File.WriteAllText(outputFilePath, text);
	}
}
