namespace ApplicationTemplate.Services;

/// <summary>
///     This service interface only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public interface IFileService
{
    public string FileName { get; set; }

    void Read(string filename);
    void Write(string filename);

}
