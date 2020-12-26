using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorythms_semester_work
{
    public class AlgorythmsManager
    {
        private readonly IConsoleViewer _consoleViewer;
        private bool _isWorking = true;
        private readonly Dictionary<Command, Action<double, double, double>> _commands = new Dictionary<Command, Action<double, double, double>>();
        private Pipeline _pipeline = new Pipeline();
        private int _page = 1;
        private Range _pageRange => (1000 * (_page - 1))..(_pipeline.Graph.Edges.Count < (1000 * _page) ? _pipeline.Graph.Edges.Count : (1000 * _page));

        public AlgorythmsManager(IConsoleViewer consoleViewer)
        {
            _consoleViewer = consoleViewer;
            InitializeCommands();
            StartProcessing();
        }

        private void StartProcessing()
        {
            _consoleViewer.Out(Message.Welcome, null);
            Show();
            while (_isWorking)
            {
                var input = _consoleViewer.GetInput();
                Command command;
                var args = input.Skip(1).ToArray();
                if (!Enum.TryParse(input[0], out command))
                    _consoleViewer.Out(Message.IncorrectInput, null);
                else
                {
                    var first = -1d;
                    var second = -1d;
                    var weight = -1d;
                    if (command == Command.Connect)
                        if (!(args.Length == 3
                            && double.TryParse(args[0], out first)
                            && double.TryParse(args[1], out second)
                            && double.TryParse(args[2], out weight)))
                        {
                            _consoleViewer.Out(Message.IncorrectArgs, command);
                            continue;
                        }
                        else
                        {
                            first = Math.Round(first, MidpointRounding.ToZero);
                            second = Math.Round(second, MidpointRounding.ToZero);
                            if (first == second)
                            {
                                _consoleViewer.Out(Message.IncorrectArgs, command);
                                continue;
                            }
                        }
                    if (command == Command.Random)
                        if (!(args.Length == 1 && double.TryParse(args[0], out first)))
                        {
                            first = Math.Round(first, MidpointRounding.ToZero);
                            if (first != 0 || first != 1)
                            {
                                _consoleViewer.Out(Message.IncorrectArgs, command);
                                continue;
                            }
                        }
                        else;
                    else if (command == Command.Count || command == Command.Page)
                        if (!(args.Length == 1 && double.TryParse(args[0], out first)))
                        {
                            _consoleViewer.Out(Message.IncorrectArgs, command);
                            continue;
                        }
                        else
                            first = Math.Round(first, MidpointRounding.ToZero);
                    else if (command == Command.ConnectRandom)
                        if (!(args.Length == 2 && double.TryParse(args[0], out first) && double.TryParse(args[1], out second)))
                        {
                            _consoleViewer.Out(Message.IncorrectArgs, command);
                            continue;
                        }
                    _commands[command](first, second, weight);
                }
            }
        }

        private void InitializeCommands()
        {
            _commands[Command.ConnectRandom] = (minWeight, maxWeight, _) =>
            {
                _pipeline.ConnectAllNodesWithRandomWeights(minWeight, maxWeight);
                _consoleViewer.Out(Message.RndConnectSuccess, null);
                Show();
            };

            _commands[Command.Connect] = (first, second, weight) =>
            {
                if (_pipeline.ConnectNodes(weight, Pipeline.GetName((int)first), Pipeline.GetName((int)second)))
                {
                    _consoleViewer.Out(Message.ConnectSuccess, $"{ first } and { second } with weight { weight }");
                    Show();
                }
                else
                    _consoleViewer.Out(Message.ConnectFailure, $"{first} and {second}");
            };

            _commands[Command.Prims] = (_, _, _) =>
            {
                GraphOptimizer.PrimsAlgorythm(_pipeline.Graph);
                _consoleViewer.Out(Message.PrimsSuccess, null);
                _page = 1;
                Show();
            };

            _commands[Command.Boruvkas] = (_, _, _) =>
            {
                GraphOptimizer.BoruvkasAlgorythm(_pipeline.Graph);
                _consoleViewer.Out(Message.BoruvkasSuccess, null);
                _page = 1;
                Show();
            };

            _commands[Command.Clear] = (_, _, _) =>
            {
                _pipeline = new Pipeline();
                Show();
            };

            _commands[Command.Count] = (count, _, _) =>
            {
                _pipeline = new Pipeline((int)count);
                _consoleViewer.Out(Message.CountChanged, count);
                Show();
            };

            _commands[Command.Random] = (type, _, _) =>
            {
                _consoleViewer.Out(Message.RandomChanged, type == 0 ? "doubles" : "ints");
                _pipeline.RandomType = (RandomType)type;
            };

            _commands[Command.Next] = (_, _, _) =>
            {
                if (_pipeline.Graph.Edges.Count >= _page * 1000)
                {
                    _page++;
                    _consoleViewer.Out(Message.NextPrev, $"{ _pageRange }");
                    Show();
                }
            };

            _commands[Command.Prev] = (_, _, _) =>
            {
                if (_page != 1)
                {
                    _page--;
                    _consoleViewer.Out(Message.NextPrev, $"{ _pageRange }");
                    Show();
                }
            };

            _commands[Command.Page] = (page, _, _) =>
            {
                if (page >= 1 && _pipeline.Graph.Edges.Count >= (page - 1) * 1000)
                {
                    _page = (int)page;
                    _consoleViewer.Out(Message.NextPrev, $"{ _pageRange }");
                    Show();
                }
            };
        }

        public void Show()
        {
            _consoleViewer.Show(_pipeline);
            _consoleViewer.Show(_pipeline.Graph.Edges.ToArray()[_pageRange]);
        }

        private enum Command
        {
            //algoruthms commands:
                ConnectRandom, Connect, Prims, Boruvkas,
            //console commands:
                Clear, Count, Random, Next, Prev, Page
        }
    }
}
