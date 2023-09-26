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

        //if file doesn't exist add header.
        if (!File.Exists(filename))
        {
            StreamWriter sw_header = new StreamWriter(filename, true);
            sw_header.WriteLine("movieId,title,genres");
            sw_header.Close();
        }
        //find highest value movieID
        StreamReader sr = new StreamReader(filename, true);

        //skip header
        sr.ReadLine();

        //loop through and find highest value movieID.
        int maxID = 0;
        while (sr.EndOfStream != true)
        {
            var lineID = Int32.Parse(sr.ReadLine().Split(",")[0]);
            if (lineID > maxID) { maxID = lineID; }
        }

        StreamWriter sw = new StreamWriter(filename, true);
        var genres = new List<string>();

        Console.WriteLine("Enter the movie title:");
        var movieTitle = Console.ReadLine();

        Console.WriteLine("Enter the movie release year:");
        var movieYear = Console.ReadLine();

        Console.WriteLine("Enter a genre for the movie:");
        var movieGenre = Console.ReadLine();

        genres.Add(movieGenre);

        string anotherGenre;

        do
        {
            Console.WriteLine($"Would you like to add another genre for movie \"{movieTitle}\" (Y/N)?");
            anotherGenre = Console.ReadLine().ToUpper();
            if (anotherGenre == "Y")
            {
                Console.WriteLine("Enter a genre for the movie:");
                movieGenre = Console.ReadLine();
                genres.Add(movieGenre);

            }
            else if (anotherGenre == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please enter either 'Y' for yes or 'N' for no.");
            }
        } while (anotherGenre != "N");

        if (genres.Count != 1)
        {
            movieGenre = null;
            for (int i = 0; i < genres.Count; i++)
            {
                if (i != genres.Count - 1)
                {
                    movieGenre += genres[i] + "|";
                }
                else
                {
                    movieGenre += genres[i];
                }
            }
        }

        sw.WriteLine($"{maxID + 1},{movieTitle} ({movieYear}),{movieGenre}");

        sw.Close();
    }

}
