using System;
using System.Collections.Generic;
using System.Linq;

namespace algorythms_semester_work
{
    public class Graph
    {
        public HashSet<Node> Nodes = new();
        public HashSet<Edge> Edges = new();

        #region Add Node
        public void Add(params Node[] nodes)
            => AddAll(nodes);

        private void AddAll(IEnumerable<Node> nodes)
            => nodes.ForEach(node => Nodes.Add(node));
        #endregion

        #region Remove Node
        public void Remove(params Node[] nodes)
            => RemoveAll(nodes);

        private void RemoveAll(IEnumerable<Node> nodes)
            => nodes.ForEach(node =>
            {
                Nodes.Remove(node);
                DisconnectAll(node.Edges);
            });
        #endregion

        #region Contains Node
        public bool Contains(params Node[] nodes)
            => ContainsAll(nodes);

        private bool ContainsAll(IEnumerable<Node> nodes)
        {
            var result = nodes.Any();
            nodes.ForEach(node => result &= Nodes.Contains(node));
            return result;
        }
        #endregion

        #region Connect Edge
        public void Connect(params Edge[] edges)
            => ConnectAll(edges);

        private void ConnectAll(IEnumerable<Edge> edges)
            => edges.ForEach(edge =>
            {
                CheckNode(edge.From);
                CheckNode(edge.To);
                edge.From.Edges.Add(edge);
                edge.To.Edges.Add(edge);
                Edges.Add(edge);
            });
        #endregion

        #region Connect Graph with Edge
        public void Connect(Graph graph, params Edge[] edges)
            => ConnectAll(graph, edges);

        private void ConnectAll(Graph graph, IEnumerable<Edge> edges)
        {
            foreach(var edge in edges)
                try { CheckNode(edge.From); graph.CheckNode(edge.To); } 
                catch { CheckNode(edge.To); graph.CheckNode(edge.From); }

            AddAll(graph.Nodes);

            graph.Edges.ForEach(edge => Edges.Add(edge));
            ConnectAll(edges);

            graph.Copy(this);
        }
        #endregion

        #region Copy Graph
        public void Copy(Graph graph)
        {
            Nodes = graph.Nodes;
            Edges = graph.Edges;
        }
        #endregion

        #region Disconnect Edge
        public void Disconnect(params Edge[] edges)
            => DisconnectAll(edges);

        private void DisconnectAll(IEnumerable<Edge> edges)
        {
            Edge edge;
            while (edges.Any())
            {
                edge = edges.First();
                edge.From.Edges.Remove(edge);
                edge.To.Edges.Remove(edge);
                Edges.Remove(edge);
            }
        }
        #endregion

        #region Reset
        public void ResetEdges()
        {
            Edge edge;
            while (Edges.Any())
            {
                edge = Edges.First();
                edge.From.Edges.Remove(edge);
                edge.To.Edges.Remove(edge);
                Edges.Remove(edge);
            }
        }

        public void Reset()
        {
            ResetEdges();
            Nodes.Clear();
        }
        #endregion

        #region Check
        private Edge CheckEdge(Edge edge)
            => !Edges.Contains(edge) 
                ? throw new ArgumentException($"Graph doesn`t contain the [ { edge } ] edge.")
                : edge;

        private Node CheckNode(Node node)
            => !Nodes.Contains(node)
                ? throw new ArgumentException($"Graph doesn`t contain the [ { node } ] node.")
                : node;
        #endregion

        public override string ToString()
            => $"[{ Nodes.Count }] nodes and [{ Edges.Count }] edges.";
    }
}