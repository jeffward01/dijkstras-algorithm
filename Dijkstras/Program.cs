using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dijkstras.services;

namespace Dijkstras
{
    class Program
    {
        static void Main(string[] args)
        {

            Dijkstra mapMaker = new Dijkstra();

            char start = 'H';
            char finish = 'A';
           

            //This is all of our vertexes.  Possible destinations.
            mapMaker.add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
            mapMaker.add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
            mapMaker.add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
            mapMaker.add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
            mapMaker.add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
            mapMaker.add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
            mapMaker.add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
            mapMaker.add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });

            List<char> path = new List<char>();
            path = mapMaker.shortest_path(start, finish);

            path.Reverse();


            Console.WriteLine("Start Node: " + start);
            Console.WriteLine("First Nearest Node:");
            foreach (var node in path)
            {
                Console.WriteLine(node);
            }
            Console.ReadLine();

        }
    }
}
