using System.IO;

namespace DependencyElimination
{
    public class FileWriter
    {
        private readonly string _pathToFile;

        public FileWriter(string pathToFile)
        {
            _pathToFile = pathToFile;
            if (File.Exists(_pathToFile))
                File.Delete(_pathToFile);
        }

        public void WriteLinks(string[] links)
        {
            File.AppendAllLines(_pathToFile, links);
        }
    }
}