using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorythms_semester_work
{
    public interface IConsoleViewer
    {
        public string[] GetInput();

        public void Out(Message message, object args);

        public void Show(Edge[] edges);

        public void Show(Pipeline pipeline);
    }

    public enum Message
    {
        RndConnectSuccess, ConnectSuccess, PrimsSuccess, BoruvkasSuccess, 
        IncorrectInput, IncorrectArgs, Welcome, CountChanged, RandomChanged, 
        NextPrev, ConnectFailure
    }
}
