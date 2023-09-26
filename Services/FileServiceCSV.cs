using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationTemplate.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileServiceCSV : IFileService
{
    private readonly ILogger<IFileService> _logger;

    public FileServiceCSV(ILogger<IFileService> logger)
    {
        _logger = logger;
    }

    public string FileName { get; set; }

    public string LineParser(string line)
    {
        
    }

    public void Read(string filename)
    {
        FileName = filename;

        _logger.Log(LogLevel.Information, "Reading");
        Console.WriteLine("*** I am reading");

        if (File.Exists(filename))
        {
            StreamReader sr = new StreamReader(filename);

            //Remove first line (headers) from output.
            sr.ReadLine();

            //Ask user what year they are interested in.
            var year = ValidateHelper.GetYear("Enter the release year of " +
                "the movie(s) you are interested in.");

            var validLines = new List<string>();

            while (sr.EndOfStream != true)
            {
                var line = sr.ReadLine();
                if (line.Contains("(" + year + ")"))
                {
                    validLines.Add(line);
                    //Console.WriteLine($"{line}");
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
                    Console.WriteLine(line);
                }
                Console.WriteLine("\n");
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
        _logger.Log(LogLevel.Information, "Writing");
        Console.WriteLine("*** I am writing");
    }
}
