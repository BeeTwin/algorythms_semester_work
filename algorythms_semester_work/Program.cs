using algorythms_semester_work;
using System;
using System.Collections.Generic;
using System.Linq;

var pipeline = new Pipeline(5);
pipeline.ConnectAllNodesWithRandomWeights(1, 10);

var g = new Graph();
var a = new NamedNode("a");
var b = new NamedNode("b");
var c = new NamedNode("c");
var d = new NamedNode("d");
g.Add(a);
g.Add(a);
g.Add(a, b);
g.Connect(new Edge(a, b), new Edge(a, c), new Edge(b, c));
var b1 = g.Contains(a);
var b2 = g.Contains(a, b, c, d);
var b3 = g.Contains(new List<Node>().ToArray());
g.Remove(d, b, c);
var b4 = g.Contains(d);
g.Remove(a);
var b5 = g.Contains(d, a, b);
g.Remove(a);


//GraphOptimizer.PrimsAlgorythm(pipeline.Graph);
//GraphOptimizer.KruskalsAlogrythm(pipeline.Graph);
GraphOptimizer.BoruvkasAlgorythm(pipeline.Graph);
//GraphOptimizer.ReverseDeleteAlgorythm(pipeline.Graph);

return;