﻿//using SortedList;
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
        public T info;

        public T Info { get; set; }
        public Label Image { get; set; }
        public SortedList<Vertex<T>, double> Neighbors { get; } 
        
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
