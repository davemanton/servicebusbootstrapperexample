using System.IO;

namespace ServiceBusBootstrapper
{
    public interface IFileReader
    {
        string ReadFileAsString(string filePath);
    }

    public class FileReader : IFileReader
    {
        private readonly string _workingDirectory;

        public FileReader(string workingDirectory)
        {
            _workingDirectory = workingDirectory;
        }

        public string ReadFileAsString(string fileName)
        {
            var path = Path.Combine(_workingDirectory, fileName);

            return File.ReadAllText(path);
        }
    }
}