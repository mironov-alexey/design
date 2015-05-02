using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly Lazy<ICommand[]> _commands;
        private readonly TextWriter _writer;

        public HelpCommand(Lazy<ICommand[]> commands, TextWriter writer)
        {
            _commands = commands;
            _writer = writer;
        }
        public override void Execute()
        {
            foreach (var command in _commands.Value)
            {
                _writer.WriteLine(command.Name);
            }
        }
    }
}
