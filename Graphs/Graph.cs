using System;
using System.Collections.Generic;

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

        // Topological Sort--------------------------------------------------------------------------------
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


        // to make a list of all edges:
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


        // classes for Kruskal's MST: ----------------------------------------------------------------------------------------
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


        // Prim's MST algorithm: -----------------------------------------------------------------------------------------
        public Digraph<T> createPrimMST_new(Digraph<T> originalDigraph)
        {
            Digraph<T> primsMST = new Digraph<T>();
            int numV = originalDigraph.Vertices.Count;

            if (numV == 0)
                return primsMST;

            var parent = new Dictionary<Vertex<T>, Vertex<T>>();
            var key = new Dictionary<Vertex<T>, double>();
            var inMST = new Dictionary<Vertex<T>, bool>();

            // Initialize all keys as infinite and parent as null
            foreach (var vertex in originalDigraph.Vertices)
            {
                key[vertex] = double.MaxValue;
                parent[vertex] = null;
                inMST[vertex] = false;
            }

            // Helper function to find the vertex with the minimum key value
            Vertex<T> minKeyVertex()
            {
                double minValue = double.MaxValue;
                Vertex<T> minVertex = null;

                foreach (var vertex in key.Keys)
                {
                    if (!inMST[vertex] && key[vertex] < minValue)
                    {
                        minValue = key[vertex];
                        minVertex = vertex;
                    }
                }
                return minVertex;
            }

            // Start with the first vertex
            key[originalDigraph.Vertices[0]] = 0;

            // Process all vertices
            for (int count = 0; count < numV; count++)
            {
                var u = minKeyVertex();
                if (u == null)
                    break;

                inMST[u] = true;

                // Update key value and parent for adjacent vertices of the picked vertex
                foreach (var neighbor in u.Neighbors)
                {
                    var v = neighbor.Key;
                    var weight = neighbor.Value;

                    if (!inMST[v] && weight < key[v])
                    {
                        key[v] = weight;
                        parent[v] = u;
                    }
                }

                // Treat the graph as undirected by checking the reverse edges
                foreach (var vertex in originalDigraph.Vertices)
                {
                    if (!vertex.Equals(u) && vertex.Neighbors.ContainsKey(u))
                    {
                        var weight = vertex.Neighbors[u];
                        if (!inMST[vertex] && weight < key[vertex])
                        {
                            key[vertex] = weight;
                            parent[vertex] = u;
                        }
                    }
                }
            }

            // Add vertices to the MST
            foreach (var vertex in originalDigraph.Vertices)
            {
                primsMST.AddVertex(new Vertex<T>(vertex.Info));
            }

            // Add edges to the MST
            foreach (var vertex in originalDigraph.Vertices)
            {
                if (parent[vertex] != null) // meaning: this vertex has been connected to the MST through another vertex
                {
                    var srcVertex = primsMST.Vertices.Find(v => v.Info.CompareTo(parent[vertex].Info) == 0);
                    var tgtVertex = primsMST.Vertices.Find(v => v.Info.CompareTo(vertex.Info) == 0);
                    if (srcVertex != null && tgtVertex != null)
                    {
                        srcVertex.AddNeighbor(tgtVertex, key[vertex]);
                        // Ensure to add both directions to treat the graph as undirected
                        tgtVertex.AddNeighbor(srcVertex, key[vertex]);
                    }
                }
            }

            return primsMST;
        }


    }
}