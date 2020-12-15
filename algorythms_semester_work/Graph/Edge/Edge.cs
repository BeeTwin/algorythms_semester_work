using System;

namespace algorythms_semester_work
{
    public class Edge
    {
        public readonly Node From; 
        public readonly Node To; 

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public bool IsIncident(Node node) 
            => From == node || To == node;

        public Node OtherNode(Node node) 
            => IsIncident(node) ? From == node ? To : From : throw new ArgumentException();

        public override string ToString() 
            => $"From \"{ From }\" \tto \"{ To }\"";
    }
}