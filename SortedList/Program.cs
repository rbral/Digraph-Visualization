using System;
using System.Collections.Generic;

namespace SortedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SortedList<Vertex<String>, int> slist = new SortedList<Vertex<string>, int>();
            slist.Add(new Vertex<string>("aa"), 1);
            slist.Add(new Vertex<string>("cc"), 2);
            slist.Add(new Vertex<string>("bb"), 3);
            foreach (Vertex<String> vertex in slist.Keys)
            {
                Console.WriteLine($"Key: {vertex} \t value {slist[vertex]}");
            }
            Console.WriteLine("bye");
            Console.ReadKey();
        }
    }
}
 
