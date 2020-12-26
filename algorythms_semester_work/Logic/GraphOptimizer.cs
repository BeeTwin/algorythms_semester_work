using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorythms_semester_work
{
    public static class GraphOptimizer
    {
        //Операции, выполняемые за константное время, не указаны.

        public static void PrimsAlgorythm(Graph graph) //O(|V| * |E|log|E| + |E| + |V| * |V| * |E|) = 
        {                                              //= O(|V| * |E| * (log|E| + |V|))
            if (!IsGraphValid(graph))
                return;

            var sortedEdges = GetSortedEdges(graph); //O(|V| * |E|log|E|)
            var count = graph.Nodes.Count - 1;
            graph.Reset(); //O(|E|)
            graph.Add(sortedEdges.Keys.First());

            WeightedEdge minEdge;
            for (var i = 0; i < count; i++) //O(|V|)
            { 
                minEdge = FindMinEdge(graph, sortedEdges); //O(|V| * |E|)
                graph.Add(graph.Nodes.Contains(minEdge.From) ? minEdge.To : minEdge.From);
                graph.Connect(minEdge);
            }
        }

        public static void BoruvkasAlgorythm(Graph graph) //O(|V| * |E|log|E| + |V| + |E| + |V| * |V| * |V| * |E|) = 
        {                                                 //= O(|V| * |E| * (log|E| + |V| * |V|)
            if (!IsGraphValid(graph))
                return;

            var sortedEdges = GetSortedEdges(graph); //O(|V| * |E|log|E|)
            var components = new List<Graph>() { graph };
            components.AddRange(graph.Nodes.Skip(1) //O(|V|)
                .Select(node =>
            {
                var component = new Graph();
                component.Add(node);
                return component;
            }));
            graph.Reset(); //O(|E|)
            graph.Add(sortedEdges.Keys.First());

            Graph secondComponent;
            WeightedEdge minEdge;
            WeightedEdge newMinEdge;
            while (components.Count != 1) // O(|V|)
            {
                minEdge = null;
                foreach (var component in components) //O(|V|)
                {
                    newMinEdge = FindMinEdge(component, sortedEdges); //O(|V| * |E|)
                    minEdge = Min(minEdge, newMinEdge);
                }

                secondComponent = components
                    .FirstOrDefault(x => x.Nodes.Contains(minEdge.To));
                components
                    .FirstOrDefault(x => x.Nodes.Contains(minEdge.From))
                    .Connect(secondComponent, minEdge);
                components.Remove(secondComponent);
            }
            graph.Copy(components.FirstOrDefault());
        }

        public static void ReverseDeleteAlgorythm(Graph graph)
        {
            throw new NotImplementedException();
        }

        public static void KruskalsAlogrythm(Graph graph)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Node, List<WeightedEdge>> GetSortedEdges(Graph graph) //O(|V| * |E|log|E|)
            => graph.Nodes.ToDictionary(
                    x => x,
                    x => Sorter.QuickSort(x.Edges.Cast<WeightedEdge>()));

        private static WeightedEdge Min(WeightedEdge minEdge, WeightedEdge newMinEdge)
        {
            if (newMinEdge is not null)
                if (minEdge is null || minEdge > newMinEdge)
                    minEdge = newMinEdge;
            return minEdge;
        }

        private static WeightedEdge FindMinEdge(Graph component, Dictionary<Node, List<WeightedEdge>> sortedEdges) //O(|V| * |E|)
        {
            WeightedEdge minEdge = null;
            foreach (var node in component.Nodes)
                foreach (var edge in sortedEdges[node])
                    if (!component.Nodes.Contains(edge.OtherNode(node)))
                    {
                        minEdge = Min(minEdge, edge);
                        break;
                    }
            return minEdge;
        }

        private static bool IsGraphValid(Graph graph)
            => graph.Edges.Count >= graph.Nodes.Count;
    }
}
