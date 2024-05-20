/*using SortedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Graphs
{
    class Graph
    {
    }
    class Digraph<T> where T : IComparable<T>
    {
        // make list of all vertices:
        public List<Vertex<T>> Vertices = new List<Vertex<T>>();

        public void AddVertex(Vertex<T> v)
        {
            Vertices.Add(v);
        }

        // make a list of all edges:
        public List<Edge<T>> getEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();

            foreach (Vertex<T> currVertex in Vertices)
            {
                foreach (KeyValuePair<Vertex<T>, double> neighbor in currVertex.Neighbors)
                {
                    Vertex<T> target = neighbor.Key;
                    double weight = neighbor.Value;
                    edges.Add(new Edge<T>(currVertex, target, weight));
                }
            }
            return edges;
        }

        public List<Vertex<T>> KruskalMST(Digraph<T> DiGraph)
        {

            // Create a list to store all the edges
            List<Edge<T>> edges = DiGraph.getEdges();

            // Sort the list of all edges by weight
            edges.Sort((firstEdge, nextEdge) => firstEdge.Weight.CompareTo(nextEdge.Weight));

//            Pick the smallest edge.
            
//            Check if it forms a cycle with the spanning tree formed so far.
//            If the cycle is not formed, include this edge. Else, discard it.
//            Repeat step#2 until there are (V-1) edges in the spanning tree




            return null;
        }



            public List<Vertex<T>> TopologicalSort()
        {
            List<Vertex<T>> sorted = new List<Vertex<T>>();
            Queue<Vertex<T>> zeros = new Queue<Vertex<T>>();

            // Dictionary to track in-degrees of vertices
            Dictionary<Vertex<T>, int> inDegreesDict = new Dictionary<Vertex<T>, int>();

            // Initialize in-degrees for each vertex
            foreach (var vertex in Vertices)
            {
                inDegreesDict[vertex] = 0;
            }

            // Calculate in-degrees for each vertex
            foreach (var vertex in Vertices)
            {
                foreach (var neighbor in vertex.Neighbors.Keys)
                {
                    inDegreesDict[neighbor]++;
                }
            }

            // Initialize the queue: enqueue vertices with in-degree zero
            foreach (var vertex in Vertices)
            {
                if (inDegreesDict[vertex] == 0)
                {
                    zeros.Enqueue(vertex);
                }
            }

            // Perform topological sorting
            while (zeros.Count > 0)
            {
                var vertex = zeros.Dequeue();
                sorted.Add(vertex);
                foreach (var neighbor in vertex.Neighbors.Keys)
                {
                    inDegreesDict[neighbor]--; // Decrement in-degree of neighbors
                    if (inDegreesDict[neighbor] == 0)
                    {
                        zeros.Enqueue(neighbor);
                    }
                }
            }

            return sorted;
        }

        *//*public List<Vertex<T>> kruskalMST()
        {
            List<Vertex<T>> vertices = new List<Vertex<T>>();

            List<Edge<T>> allEdges = graph.GetAllEdges();

            return null;
        // at the end, we want a list of edges to be included in the MST
        // how to represent the edges? Maybe as a tuple (sourceVertex, targetVertex)
        // so a list of these touples.
        }*//*

    }
}


*/