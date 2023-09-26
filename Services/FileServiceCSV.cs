using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Logging;

namespace ApplicationTemplate.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileServiceCSV : IFileService
{
    //private readonly ILogger<IFileService> _logger;

    //public FileServiceCSV(ILogger<IFileService> logger)
    //{
    //    _logger = logger;
    //}

    public string FileName { get; set; }

    public void Read(string filename)
    {
        FileName = filename;

        //_logger.Log(LogLevel.Information, "Reading");
        //Console.WriteLine("*** I am reading");

        if (File.Exists(filename))
        {
            StreamReader sr = new StreamReader(filename);

            //Remove first line (headers) from output.
            sr.ReadLine();

            //Ask user what year they are interested in.
            var year = ValidateHelper.GetYear("\nEnter the release year of " +
                "the movie(s) you are interested in.");

            var validLines = new List<string>();

            while (sr.EndOfStream != true)
            {
                var line = sr.ReadLine();
                if (line.Contains("(" + year + ")"))
                {
                    validLines.Add(line);
                }
                else
                {
                    continue;
                }
            }
            if (validLines.Count != 0)
            {
                foreach (string line in validLines)
                {
                    TextFieldParser parser = new TextFieldParser(new StringReader(line));

                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    string[] elements;

                    while (!parser.EndOfData)
                    {
                        elements = parser.ReadFields();
                        Console.WriteLine($"Movie ID: {elements[0]}");
                        Console.WriteLine($"Title (Relaese Year): {elements[1]}");
                        Console.Write($"Genre(s): ");
                        if (elements[2].Contains("|"))
                        {
                            var genres = elements[2].Split("|");
                            for (int i = 0; i < genres.Length; i++)
                            {
                                if (i != (genres.Length - 1))
                                {
                                    Console.Write(genres[i] + ", ");
                                }
                                else
                                {
                                    Console.Write(genres[i]);
                                }
                            }

                        }
                        else
                        {
                            Console.Write(elements[2]);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    parser.Close();
                }
            }
            else
            {
                Console.WriteLine("\nThere are no entries which match your criteria.\n");
            }

        }
        else
        {
            Console.WriteLine("File does not exist.\n");
        }
    }

    public void Write(string filename)
    {
        FileName = filename;
        //_logger.Log(LogLevel.Information, "Writing");
        //Console.WriteLine("*** I am writing");
    }
}
