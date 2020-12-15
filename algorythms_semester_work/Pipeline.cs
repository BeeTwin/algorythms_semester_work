using System;
using System.Collections.Generic;

namespace algorythms_semester_work
{
    public sealed class Pipeline
    {
        public Graph Graph = new();
        public RandomType RandomType;

        private int _count;

        public Pipeline(int count, RandomType randomType = RandomType.Int)
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

        public void ConnectNodes(double weight, Node first, Node second)
        {
            if (first != second && !first.IsIncident(second))
                Graph.Connect(new WeightedEdge(weight, first, second));
        }

        private double Random(double minValue, double maxValue)
            => RandomType == RandomType.Int
                ? new Random().Next(
                    (int)Math.Round(minValue, MidpointRounding.ToPositiveInfinity),
                    (int)Math.Round(maxValue, MidpointRounding.ToNegativeInfinity))
                : new Random().NextDouble(minValue, maxValue);

        public override string ToString()
            => $"Water tower and [{ _count - 1 }] houses with [{ Graph.Edges.Count }] connections.";
    }

    public enum RandomType
    {
        Double, Int
    }
}