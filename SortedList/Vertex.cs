using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedList
{
    class Vertex<T> :  IComparable<Vertex<T>> where T : IComparable<T>
    {
        T Info { get; set; }
        SortedList<Vertex<T>, double> Neighbors = null;
        int InDegree { get; set; }
        int OutDegree { get; set; }

        public Vertex(T info)
        {
            Info = info;
            Neighbors = new SortedList<Vertex<T>, double>();
            InDegree = 0;
            OutDegree = 0;
        }


        public override String ToString() => Info.ToString();

     
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is Vertex<T>)
            {
                Vertex<T> other = (Vertex<T>)obj;
                isEqual = this.Info.Equals(other.Info);
            }
            return isEqual;
        }
        public void AddNeighbor(Vertex<T> target, double weight)
        {
            Neighbors.Add(target, weight);
            OutDegree += 1;
            target.InDegree += 1;
        }

        public int CompareTo([AllowNull] Vertex<T> other)
        {
            return this.Info.CompareTo(other.Info);
        }

        /*public double getWeightBetEachNeighbor(Vertex<T> neighbor)
        {
            if (Neighbors.ContainsKey(neighbor))
            {
                // Return the weight of the edge between the current vertex and the neighbor
                return Neighbors[neighbor];
            }
            else
            {
                // If the neighbor doesn't exist, return a default value (e.g., -1) or throw an exception
                throw new ArgumentException("Neighbor vertex not found.");
            }
        }*/
    }
}
