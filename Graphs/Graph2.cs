using SortedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Graphs
{
    class Graph2
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



        //----------------- code from G4G --------------------------------
        // A class to represent a graph edge 
        class EdgeG4G : IComparable<EdgeG4G>
        {
            public int src, dest, weight;

            // Comparator function used for sorting edges 
            // based on their weight 
            public int CompareTo(EdgeG4G compareEdge)
            {
                return this.weight - compareEdge.weight;
            }
        }

        // A class to represent 
        // a subset for union-find 
        public class subset
        {
            public int parent, rank;
        }

        // A utility function to find set of an element i 
        // (uses path compression technique) 
        int find(subset[] subsets, int i)
        {
            // Find root and make root as 
            // parent of i (path compression) 
            if (subsets[i].parent != i)
                subsets[i].parent
                    = find(subsets, subsets[i].parent);

            return subsets[i].parent;
        }

        // A function that does union of 
        // two sets of x and y (uses union by rank) 
        void Union(subset[] subsets, int x, int y)
        {
            int xroot = find(subsets, x);
            int yroot = find(subsets, y);

            // Attach smaller rank tree under root of 
            // high rank tree (Union by Rank) 
            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;

            // If ranks are same, then make one as root 
            // and increment its rank by one 
            else
            {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }


        public void KruskalMST(Digraph<T> digraph)
        {
            List<Edge<T>> result = new List<Edge<T>>();

            // Create a list to store all the edges
            List<Edge<T>> edges = digraph.getEdges();

            // Sort the list of all edges by weight
            edges.Sort((firstEdge, nextEdge) => firstEdge.Weight.CompareTo(nextEdge.Weight));

            int numV = Vertices.Count();
            int numE = edges.Count();

            // Allocate memory for creating numV subsets 
            subset[] subsets = new subset[numV];
            for (int i = 0; i < numV; ++i)
            {
                subsets[i] = new subset();
            }

            // Create numV subsets with single elements 
            for (int v = 0; v < numV; ++v)
            {
                subsets[v].parent = v;
                subsets[v].rank = 0;
            }

            int ix = 0;
            // Number of edges to be taken is equal to V-1 
            int ex = 0;
            while (ex < (numV - 1))
            {
                // Pick the smallest edge. And increment 
                // the index for next iteration 
                Edge<T> next_edge = new Edge<T>(null, null, 0);
                next_edge = (EdgeG4G)edges[ix++];
            }



        }



        //------------------ my code --------------------


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

        /*public List<Vertex<T>> KruskalMST(Digraph<T> digraph)
        {
            // Create a result list to store the resultant MST
            List<Edge<T>> result = new List<Edge<T>>();

            // Create a list to store all the edges
            List<Edge<T>> edges = digraph.getEdges();

            // Sort the list of all edges by weight
            edges.Sort((firstEdge, nextEdge) => firstEdge.Weight.CompareTo(nextEdge.Weight));



            foreach (Edge<T> edge in edges)
            {

            }*/
        //            Pick the smallest edge.

        //            Check if it forms a cycle with the spanning tree formed so far.
        //            If the cycle is not formed, include this edge. Else, discard it.
        //            Repeat step#2 until there are (V-1) edges in the spanning tree


        /*    Note: here's how the form1 draws the edges:
         *    so will need to pass it a dg with the ????
           private void DrawEdges_(Digraph<String> dg)
           {
               *//*
                * for each vertex in digraph,
                *    get location of start of arrow from sv
                *    for each neighbor of vertex, draw arrow to its location
                *//*
               Graphics gr = panelGraph.CreateGraphics();
               Pen pen = new Pen(EDGE_COLOR, EDGE_THICK);
               pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
               foreach (Vertex<String> vertex in dg.Vertices)
               {
                   Point src = vertex.Image.Location;
                   foreach (Vertex<String> nbr in vertex.Neighbors.Keys)
                   {
                       Point tgt = nbr.Image.Location;
                       DrawEdge(gr, pen, tgt, src);
                   }
               }
           }
           */
        /*    return null;
        }*/



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

        /*public List<Vertex<T>> kruskalMST()
        {
            List<Vertex<T>> vertices = new List<Vertex<T>>();

            List<Edge<T>> allEdges = graph.GetAllEdges();

            return null;
        // at the end, we want a list of edges to be included in the MST
        // how to represent the edges? Maybe as a tuple (sourceVertex, targetVertex)
        // so a list of these touples.
        }*/

    }
}


