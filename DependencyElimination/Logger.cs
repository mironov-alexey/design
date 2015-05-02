using System.IO;

namespace DependencyElimination
{
    public class Logger
    {
        private readonly TextWriter _output;

        public Logger(TextWriter output)
        {
            _output = output;
        }

        public void Log(string message)
        {
            _output.WriteLine(message);
        }
    }
}