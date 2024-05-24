using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Graphs
{
    class Edge<T> : IComparable<Edge<T>> where T : IComparable<T>
    {
        public Vertex<T> Source { get; }
        public Vertex<T> Target { get; }
        public double Weight { get; }

        public Edge(Vertex<T> source, Vertex<T> target, double weight)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }

       

        public int CompareTo(Edge<T> other)
        {
            return this.Weight.CompareTo(other.Weight);
        }


        
    }
}
