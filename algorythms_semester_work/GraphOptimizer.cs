using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorythms_semester_work
{
    public static class GraphOptimizer
    {
        public static void PrimsAlgorythm(Graph graph)
        {
            if (!IsGraphValid(graph))
                return;

            var sortedEdges = GetSortedEdges(graph);
            var count = graph.Nodes.Count - 1;
            graph.Reset();
            graph.Add(sortedEdges.Keys.First());

            WeightedEdge minEdge;
            for (var i = 0; i < count; i++)
            {
                minEdge = FindMinEdge(graph, sortedEdges);
                graph.Add(graph.Nodes.Contains(minEdge.From) ? minEdge.To : minEdge.From);
                graph.Connect(minEdge);
            }
        }

        public static void KruskalsAlogrythm(Graph graph)
        {

        }

        public static void BoruvkasAlgorythm(Graph graph)
        {
            if (!IsGraphValid(graph))
                return;

            var sortedEdges = GetSortedEdges(graph);
            var components = new List<Graph>() { graph };
            components.AddRange(graph.Nodes.Skip(1)
                .Select(node =>
            {
                var component = new Graph();
                component.Add(node);
                return component;
            }));
            graph.Reset();
            graph.Add(sortedEdges.Keys.First());

            Graph secondComponent;
            WeightedEdge minEdge;
            WeightedEdge newMinEdge;
            while (components.Count != 1)
            {
                minEdge = null;
                foreach (var component in components)
                {
                    newMinEdge = FindMinEdge(component, sortedEdges);
                    minEdge = Min(minEdge, newMinEdge);
                }

                secondComponent = components
                    .FirstOrDefault(x => x.Nodes.Contains(minEdge.To));
                components
                    .FirstOrDefault(x => x.Nodes.Contains(minEdge.From))
                    .Connect(secondComponent, minEdge);
                components.Remove(secondComponent);
            }
        }

        public static void ReverseDeleteAlgorythm(Graph graph)
        {

        }

        private static Dictionary<Node, List<WeightedEdge>> GetSortedEdges(Graph graph)
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

        private static WeightedEdge FindMinEdge(Graph component, Dictionary<Node, List<WeightedEdge>> sortedEdges)
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
