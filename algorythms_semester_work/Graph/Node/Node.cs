using System.Collections.Generic;
using System.Linq;

namespace algorythms_semester_work
{
    public class Node
    {
        public readonly HashSet<Edge> Edges = new();

        public bool IsIncident(Node node)
            => Edges.Where(x => x.IsIncident(node)).Any();
    }
}