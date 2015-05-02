using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentTask
{
    public class Behavior
    {
        private readonly List<Action> actions;

        public Behavior()
        {
            actions = new List<Action>();
        }
        public Behavior(IEnumerable<Action> actions)
        {
            this.actions = actions.ToList();
        }

        public Behavior Say(string message)
        {
//            Console.WriteLine(message);
            actions.Add(() => Console.WriteLine(message));
            return this;
        }

        public Behavior UntilKeyPressed(Action<Behavior> inner)
        {
//            Wait(inner);
            actions.Add(() => { Wait(inner);});
            return this;
        }

        private static void Wait(Action<Behavior> inner)
        {
            while (!Console.KeyAvailable)
            {
                var behaviour = new Behavior();
                inner(behaviour);
                behaviour.Execute();
                Thread.Sleep(500);
            }
            Console.ReadKey();
        }

        public Behavior Jump(JumpHeight height)
        {
//            Console.WriteLine("Jump: " + height);
            actions.Add(() => Console.WriteLine("Jump: " + height));
            return this;
        }

        public Behavior Delay(TimeSpan timeSpan)
        {
//            Thread.Sleep(timeSpan);
            actions.Add(() => Thread.Sleep(timeSpan));
            return this;
        }

        public void Execute()
        {
            foreach (var action in actions)
            {
                action();
            }
        }
    }
}
