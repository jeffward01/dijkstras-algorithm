using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstras.services
{
    public class Dijkstra
    {
        Dictionary<char, Dictionary<char, int>> verticies = new Dictionary<char, Dictionary<char, int>>();


        //add the name to the and edge values to our verticies array
        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            verticies[name] = edges;
        }


        /// <summary>
        /// Finds the shortest path betweet two nodes.  'A' and 'H' for example
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public List<char> shortest_path(char start, char finish)
        {
            var previous = new Dictionary<char, char>(); //Holder varible to hold our previous node
            var distances = new Dictionary<char, int>(); //The distance beween our two nodes
            var nodes = new List<char>(); //List of nodes (this is gathered from our verticies
            var priorityQueue = new PriorityQueue();


            List<char> path = null;

            //Iterate over each of the verticies from the vertexes that were added.
            foreach (var vertex in verticies)
            {
                //If vertex is the start distance for our mapping, for example 'A'
                if (vertex.Key == start)
                {
                    //Set current disance from start node to 0.  0 because we are standing on it
                    distances[vertex.Key] = 0;
                   // priorityQueue.EnQueue(0, vertex.Value);  //Turned off
                }
                else
                {
                    //If NOT our start node, then set the distance for our unvisted node to infinity (we dont know how far, we havent visited it yet)
                    distances[vertex.Key] = int.MaxValue;
               //     priorityQueue.EnQueue(int.MaxValue, vertex.Value); //turned off
                }
                //Add each vertex to our list of nodes.  This is our 'web of possible destiations'
                nodes.Add(vertex.Key);
            }

            // while(!priorityQueue.IsEmpty())  //If we wanted to use PriorityQueue
            while (nodes.Count != 0)
            {
                //Sort the nodes from shortest value to largest.  We know this because we saved it in our dictionary values of char and int
                nodes.Sort((x, y) => distances[x] - distances[y]);

                //get the smallest (this is our current position)
                var smallest = nodes[0];
                nodes.Remove(smallest);

                /*
                 * var smallest = priorityQueue.DeQueue();
                 */

                //If smallest == finish
                if (smallest == finish)
                {
                    //Create new path, this is the path between 
                    path = new List<char>();

                    //While the smallest is one of the nodes we have already been to
                    while (previous.ContainsKey(smallest))
                    {
                        //Add thesmallest to the path
                        path.Add(smallest);
                        //Get the smallest node
                        smallest = previous[smallest];
                    }

                    break;
                }

                //If unnkown, break becuase it is not adjacent
                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                //Get all adjacent nodest to our current node, the smallest node.
                foreach (var neighbor in verticies[smallest])
                {
                    //this is the potential closest nodse, its equal to our current location, and then the first neigbor
                    var alt = distances[smallest] + neighbor.Value;

                    //If our potential node is closer than the neighhboring node, its a match!
                    if (alt < distances[neighbor.Key])
                    {
                        //Add 'potential node' (we now know this is the closest node to our start) to our distances dictionart
                        distances[neighbor.Key] = alt;

                        //Set the previous destination to our current (now previous) smallest node, and move onto the next smallest node
                        previous[neighbor.Key] = smallest;

                        //priorityQueue.EnQueue(alt, ConvertToDictionary(neighbor));  //Turned off

                    }
                }
            }
            return path;
        }

        public Dictionary<char, int> ConvertToDictionary(KeyValuePair<char, int> keyValuePair)
        {
            var list = new List<KeyValuePair<char, int>> { keyValuePair };
            return list.ToDictionary(x => x.Key, x => x.Value);
        }

        //Get count of number of vertices in dictionary
        public int GetNumberOfVerticies()
        {
            return verticies.Count;
        }


        public class PriorityQueue
        {
            Dictionary<int, Dictionary<char, int>> nodes = new Dictionary<int, Dictionary<char, int>>();

            public void EnQueue(int priority, Dictionary<char, int> key)
            {
                nodes.Add(priority, key);
                Sort();
            }

            public void Sort()
            {
                var sorted = nodes.OrderByDescending(x => x.Key);
                nodes = sorted.ToDictionary(x => x.Key, x => x.Value);
            }

            public Dictionary<int, Dictionary<char, int>> DeQueue()
            {
                var smallestNode = nodes.FirstOrDefault();
                var key = smallestNode.Key;
                var value = smallestNode.Value;

                Dictionary<int, Dictionary<char, int>> returnObj = new Dictionary<int, Dictionary<char, int>>();
                returnObj.Add(key, value);
                return returnObj;
            }

            public bool IsEmpty()
            {
                if (nodes.Count >= 1)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
