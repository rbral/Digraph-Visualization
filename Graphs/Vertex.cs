using SortedList;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs
{
    class Vertex<T> : IComparable<Vertex<T>> where T : IComparable<T>
    {
        private T info;

        T Info { get; set; }
        public Label Image { get; set; }
        public SortedList<Vertex<T>, double> Neighbors { get; } 
        // my addition:---------------
        //public List<Edge<T>> Edges { get; }
        //----------------------------
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

        //public  int CompareTo(Vertex<T> other)
        //{
        //    String thisStr = Info.ToString();
        //    String otheStr = other.Info.ToString();
        //    return thisStr.CompareTo(otheStr);
        //    //return this.Info.CompareTo(other.Info);
        //}
        //public int CompareTo([AllowNull] Vertex<T> other)
        //{
        //    return this.Info.CompareTo(other.Info);
        //}

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
        public void AddNeighbor(Vertex<T> tgt, double wgt)
        {
            Neighbors.Add(tgt, wgt);
            OutDegree += 1;
            tgt.InDegree += 1;
        }

        public int CompareTo(Vertex<T> other)
        {
            return this.Info.CompareTo(other.Info);
        }
    }
}
