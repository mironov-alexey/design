using System;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using FakeItEasy.Configuration;
using Ninject;

namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
        }

        static void Main(string[] args)
        {
//            args = new[] {"timer", "2000"};
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<ICommand>().To<HelpCommand>();
            container.Bind<TextWriter>().ToConstant(Console.Out);
            container.Bind<CommandLineArgs>().ToMethod(c => new CommandLineArgs(args));
//            container.Bind<CommandLineArgs>().ToSelf().WithConstructorArgument(typeof(string[]), args);
//            container.Bind<CommandLineArgs>().ToConstant(new CommandLineArgs(args));
            var program = container.Get<Program>();
            program.Run();
        }

        public void Run()
        {
            if (arguments.Command == null)
            {
                Console.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                Console.WriteLine("Sorry. Unknown command {0}", arguments.Command);
            else
                command.Execute();
        }
    }
}
