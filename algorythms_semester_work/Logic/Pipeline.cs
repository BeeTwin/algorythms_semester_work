using System;
using System.Linq;
using System.Collections.Generic;

namespace algorythms_semester_work
{
    public sealed class Pipeline
    {
        public Graph Graph = new();
        public RandomType RandomType;

        private int _count;

        public Pipeline(int count = 10, RandomType randomType = RandomType.Int)
        {
            RandomType = randomType;
            _count = count;
            for (var i = 0; i < _count; i++)
                Graph.Add(new NamedNode(i == 0 ? "Water tower" : $"House #{ i }"));
        }

        public void ConnectAllNodesWithRandomWeights(double minWeight, double maxWeight)
        {
            Graph.ResetEdges();
            foreach (var firstNode in Graph.Nodes)
                foreach (var secondNode in Graph.Nodes)
                    ConnectNodes(Random(minWeight, maxWeight), firstNode, secondNode);
        }

        public void ConnectNodes(double weight, string first, string second)
            => ConnectNodes(weight, FindNode(first), FindNode(second));

        private void ConnectNodes(double weight, Node first, Node second)
        {
            if (first is not null && second is not null && first != second && !first.IsIncident(second))
                Graph.Connect(new WeightedEdge(weight, first, second));
        }

        public void DisconnectNodes(string first, string second)
            => DisconnectNodes(FindNode(first), FindNode(second));

        private void DisconnectNodes(Node first, Node second)
        {
            if (first is not null && second is not null && first != second && first.IsIncident(second))
                Graph.Disconnect(first.Edges.FirstOrDefault(edge => edge.To == second || edge.From == second));
        }

        private double Random(double minValue, double maxValue)
            => RandomType == RandomType.Int
                ? new Random().Next(
                    (int)Math.Round(minValue, MidpointRounding.ToPositiveInfinity),
                    (int)Math.Round(maxValue, MidpointRounding.ToNegativeInfinity))
                : new Random().NextDouble(minValue, maxValue);

        private Node FindNode(string name)
            => Graph.Nodes.FirstOrDefault(node => (node as NamedNode).Name == name);

        public static string GetName(int i)
            => i < 0 ? null : i == 0 ? "Water tower" : $"House #{ i }";

        public override string ToString()
            => $"Water tower and [{ _count - 1 }] houses with [{ Graph.Edges.Count }] connections.";
    }

    public enum RandomType
    {
        Double, Int
    }
}