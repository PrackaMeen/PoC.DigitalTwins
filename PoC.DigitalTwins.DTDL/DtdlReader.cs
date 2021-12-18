namespace PoC.DigitalTwins.DTDL;
using Microsoft.Azure.DigitalTwins.Parser;
using System.Text;

public class DtdlReader
{
    protected static StreamReader GetStreamReader(string fullPath)
    {
        return new StreamReader(fullPath, new FileStreamOptions()
        {
            Mode = FileMode.Open,
            Share = FileShare.Read,
            Access = FileAccess.Read,
        });
    }

    public static async Task<string> LoadDtdlBy(string fullPath)
    {
        var serializedModel = String.Empty;
        using (var sr = GetStreamReader(fullPath))
        {
            serializedModel = await sr.ReadToEndAsync();
        }

        return serializedModel ?? String.Empty;
    }

    public static IEnumerable<string> GetAllJsonFilesPathsFromDirectory(string directoryFullPath)
    {
        List<string> allFilePaths = new();

        DirectoryInfo directoryInfo = new(directoryFullPath);
        if (directoryInfo.Exists)
        {
            foreach (var fullFilePath in directoryInfo.EnumerateFiles("*.json", SearchOption.AllDirectories))
            {
                allFilePaths.Add(fullFilePath.FullName);
            }
        }

        if (allFilePaths.Count < 1)
        {
            throw new ArgumentException("Folder specified by directoryFullPath does not exist or do not contains any DTDL files (*.json)...");
        }

        return allFilePaths;
    }

    public static async Task<IEnumerable<string>> LoadAllDtdlsFrom(string rootFolderFullPath)
    {
        List<string> serializedDtdls = new();

        foreach (var fullFilePath in GetAllJsonFilesPathsFromDirectory(rootFolderFullPath))
        {
            var serializedDtdl = await LoadDtdlBy(fullFilePath);
            serializedDtdls.Add(serializedDtdl);
        }

        return serializedDtdls;
    }

    protected static async Task<string> LoadDtdlByName(string dtdlName)
    {
        var relativePath = Path.Combine("Examples", dtdlName);
        var fullPath = Path.GetFullPath($"./{relativePath}.json");
        string serializedDtdl = await DtdlReader.LoadDtdlBy(fullPath);
        return serializedDtdl;
    }
}
