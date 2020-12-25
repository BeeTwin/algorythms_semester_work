using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace algorythms_semester_work
{
    public class ConsoleViewer : IConsoleViewer
    {
        private readonly Dictionary<Message, Func<object, string>> _messages = new Dictionary<Message, Func<object, string>>();

        public ConsoleViewer()
        {
            InitializeMessages();
            DrawBoxes();
        }

        private void DrawBoxes()
        {
            var line = new string('═', WindowWidth - 2);
            var emptyLine = new string(' ', WindowWidth - 2);
            Out('╔' + line + '╗', 0, 0);
            Out('║' + emptyLine + '║', 0, 1);
            Out('╠' + line + '╣', 0, 2);
            Out('║' + emptyLine + '║', 0, 3);
            Out('╚' + line + '╝', 0, 4);
        }

        public string[] GetInput()
        {
            Out("$: ", 2, 3);
            SetCursorPosition(5, 3);
            string input;
            if ((input = ReadLine()) != null)
                ClearBoxes();
            return input.Split(' ');
        }

        public void Out(Message message, object args)
            => Out("#: " + _messages[message](args), 2, 1);

        private void Out(string message, int x, int y)
        {
            try
            {
                SetCursorPosition(x, y);
                Write(message);
            }
            catch
            {
                Clear();
                WriteLine("Ah... Sempai... That's too big for me...");
            }
        }

        private void ClearBoxes()
        {
            var emptyLine = new string(' ', WindowWidth - 2);
            Out(emptyLine, 1, 1);
            Out(emptyLine, 1, 3);
        }

        public void Show(Edge[] edges)
        {
            var i = 7;
            SetCursorPosition(0, i);

            Out(new string(' ', 1001 * WindowWidth), 0, i);

            SetCursorPosition(3, i++);
            foreach(var edge in edges)
               Out(edge.ToString(), 3, i++);
            Out(". . .", 3, i);
            SetCursorPosition(0, 0);
        }

        public void Show(Pipeline pipeline)
        {
            Out(new string(' ', WindowWidth), 0, 6);
            Out(pipeline.ToString(), 3, 6);
        }

        private void InitializeMessages()
        {
            _messages[Message.BoruvkasSuccess] = (message)
                => $"Graph optimized by Boruvka`s algorythm.";
            _messages[Message.ConnectedSuccess] = (message)
                => $"Nodes { message } connected successfully.";
            _messages[Message.IncorrectArgs] = (message)
                => $"Incorrect arguments. Try in form { message } [args].";
            _messages[Message.IncorrectInput] = (message)
                => $"Incorrect input. Try again.";
            _messages[Message.PrimsSuccess] = (message)
                => $"Graph optimized by Prim`s algorythm.";
            _messages[Message.RndConnectedSuccess] = (message)
                => $"Graph connected by random weights.";
            _messages[Message.Welcome] = (message)
                => $"Welcome to console graph optimizer.";
            _messages[Message.CountChanged] = (message)
                => $"Count is now { message }.";
            _messages[Message.RandomChanged] = (message)
                => $"Weights is now { message }.";
            _messages[Message.NextPrev] = (message)
                => $"Edges { message }.";
        }
    }
}
