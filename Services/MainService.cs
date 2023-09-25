using System;

namespace ApplicationTemplate.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IFileService _fileService;
    public MainService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void Invoke()
    {
        var filename = "movies.csv";
        string choice;
        do
        {
            Console.WriteLine("1. Display all movies");
            Console.WriteLine("2. Add movie");
            Console.WriteLine("3. Exit\n");
            choice = Console.ReadLine();

            // Logic would need to exist to validate inputs and data prior to writing to the file
            // You would need to decide where this logic would reside.
            // Is it part of the FileService or some other service?
            if (choice == "1")
            {
                _fileService.Read(filename);
            }
            else if (choice == "2")
            {
                _fileService.Write(filename);
            }
            else
            {
                Console.WriteLine("\nPlease select a valid option.\n");
            }
        }
        while (choice != "3");
    }
}
