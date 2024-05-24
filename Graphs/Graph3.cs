using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Graphs
{
    class Graph
    {
    }
    class Digraph<T> where T : IComparable<T>
    {
        public List<Vertex<T>> Vertices = new List<Vertex<T>>();

        public void AddVertex(Vertex<T> v)
        {
            Vertices.Add(v);
        }

        // classes for Kruskal MST: ------------------------
        // A class to represent 
        // a subset for union-find 
        public class Subset
        {
            public int parent { get; set; }
            public int rank { get; set; }
        }

        // A utility function to find set of an element i 
        // (uses path compression technique) 
        int find(Subset[] subsets, int i)
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
        void Union(Subset[] subsets, int x, int y)
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


        public Digraph<T> createDigraphFromEdges(Digraph<T> originalDigraph, List<Edge<T>> edgesToIncludeList)
        {
            Digraph<T> newGraph = new Digraph<T>();
            Dictionary<Vertex<T>, Vertex<T>> vertexMap = new Dictionary<Vertex<T>, Vertex<T>>();

            // Add vertices to the new graph
            foreach (var vertex in originalDigraph.Vertices)
            {
                var newVertex = new Vertex<T>(vertex.Info);
                newGraph.AddVertex(newVertex);
                vertexMap[vertex] = newVertex;
            }

            // Add edges to the new graph
            foreach (var edge in edgesToIncludeList)
            {
                if (vertexMap.ContainsKey(edge.Source) && vertexMap.ContainsKey(edge.Target))
                {
                    var newSource = vertexMap[edge.Source];
                    var newTarget = vertexMap[edge.Target];
                    newSource.AddNeighbor(newTarget, edge.Weight);
                }
                else
                {
                    Console.WriteLine("Edge skipped: one of the vertices not found in the original graph.");
                }
            }

            return newGraph;


            /*
             so we have a list of edges to include in drawing of mst.
            edges have: 
                public Vertex<T> Source { get; }
                public Vertex<T> Target { get; }
                public double Weight { get; }
            we need to make a digraph where there are the same vertices
            but for each vertex, it will have less neighbors. 
            the only neighbors it will have are those included in the edges list
            a vertex has: 
                Info = info;
                Neighbors = new SortedList<Vertex<T>, double>();
                InDegree = 0;
                OutDegree = 0;
            so here's how to do it:
            make a copy of the digraph
            loop through the edges TO EXCLUDE list
                take source of curr edge
                    find matching vertex to that src already in copydigraph
                    loop through neighbors list of vertex (correcponding to target in edge)
                    if find an edge that is          
             */

            /*Digraph<T> result = new Digraph<T>();
            List<Vertex<T>> newVertices = result.Vertices;
            //List<Vertex<T>> NewVertices = new List<Vertex<T>>();


            Vertex<T> currSource;
            Vertex<T> currTarget;
            double currWeight;

            foreach (Edge<T> currEdge in edgesToIncludeList)
            {
                currSource = currEdge.Source; 
                currTarget = currEdge.Target;
                currWeight = currEdge.Weight;
                currSource.AddNeighbor(currTarget, currWeight);
                newVertices.Add(currSource);                              
            }

            return result;*/
        }

        // new attempt chat gpt
        // Kruskal's MST algorithm
        public List<Edge<T>> KruskalMSTEdges()
        {
            List<Edge<T>> resultEdges = new List<Edge<T>>();
            List<Edge<T>> edges = getEdges();
            edges.Sort();

            int numV = Vertices.Count;
            Subset[] subsets = new Subset[numV];
            for (int v = 0; v < numV; ++v)
                subsets[v] = new Subset { parent = v, rank = 0 };

            int e = 0; // Number of edges in MST
            int i = 0; // Index for sorted edges

            while (e < numV - 1 && i < edges.Count)
            {
                Edge<T> nextEdge = edges[i++];
                int x = find(subsets, Vertices.IndexOf(nextEdge.Source));
                int y = find(subsets, Vertices.IndexOf(nextEdge.Target));

                if (x != y)
                {
                    resultEdges.Add(nextEdge);
                    Union(subsets, x, y);
                    e++;
                }
            }

            return resultEdges;
        }


        /*        public List<Edge<T>> KruskalMSTEdges()
                {
                    // remove all the neighbors from the digraph
                    // that will not be included in the MST. 
                    // then call display on that digraph


                    // get number of Vertices and edges: 
                    int numV = Vertices.Count();
                    List<Edge<T>> edges = getEdges();
                    int numE = edges.Count();

                    // NEW: this will store the resultant mst:
                    //Digraph<T> result = new Digraph<T>();

                    // OLD:
                    // This will store the 
                    // resultant MST 
                    //EdgeG[] result = new EdgeG[numV];
                    List<Edge<T>> edgesToInclude = new List<Edge<T>>();

                    List<Edge<T>> edgesToExclude = new List<Edge<T>>();

                    // An index variable, used for result[] 
                    int e = 0;

                    // An index variable, used for sorted edges 
                    int i = 0;

                    // Sort all the edges in non-decreasing 
                    // order of their weight. 
                    edges.Sort((firstEdge, nextEdge) => firstEdge.Weight.CompareTo(nextEdge.Weight));

                    // Allocate memory for creating V subsets 
                    Subset[] subsets = new Subset[numV];
                    for (i = 0; i < numV; ++i)
                        subsets[i] = new Subset();

                    // Create V subsets with single elements 
                    for (int v = 0; v < numV; ++v)
                    {
                        subsets[v].parent = v;
                        subsets[v].rank = 0;
                    }
                    i = 0;
                    int x, y;
                    // Number of edges to be taken is equal to V-1 
                    while (e < (numV - 1) && i < edges.Count)
                    {
                        foreach (Edge<T> edge in edges)
                        {
                            x = find(subsets, Vertices.IndexOf(edge.Source));
                            y = find(subsets, Vertices.IndexOf(edge.Target));
                            if (x != y)
                            {
                                edgesToInclude.Add(edge);
                                //edgesToInclude[e++] = next_edge;
                                Union(subsets, x, y);
                            }
                            else
                            {
                                // remove this edge from the digraph
                                // so add to edges to EXclude list:
                                //edgesToExclude[e++] = next_edge;
                                edgesToExclude.Add(edge);

                            }
                        }
                        // Pick the smallest edge. And increment 
                        // the index for next iteration 
                        //EdgeG next_edge = new EdgeG();
                        //next_edge = edges[i++];
                        //Edge<T> next_edge = edges[i++];

                        // Convert Vertex to index (this may require a mapping from Vertex<T> to index)
                        //int x = find(subsets, Vertices.IndexOf(next_edge.Source));
                        //int y = find(subsets, Vertices.IndexOf(next_edge.Target));
                        //int x = find(subsets, next_edge.src);
                        //int y = find(subsets, next_edge.dest);

                        // If including this edge doesn't cause cycle, 
                        // include it in result and increment the index 
                        // of result for next edge 
                        */
        /*if (x != y)
                        {
                            edgesToInclude.Add(next_edge);
                            //edgesToInclude[e++] = next_edge;
                            Union(subsets, x, y);
                        }
                        else
                        {
                            // remove this edge from the digraph
                            // so add to edges to EXclude list:
                            //edgesToExclude[e++] = next_edge;
                            edgesToExclude.Add(next_edge);

                        }*//*

                    }
                    return edgesToInclude;
                }
        */


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


        // new: Prim's MST:


    }
}