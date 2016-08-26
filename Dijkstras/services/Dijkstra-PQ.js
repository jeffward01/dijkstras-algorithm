
//This is the PriorityQueue Data Structure to help with load times
function PriorityQueue() {
    this.nodes = [];
    
    //Adds node to nodes, and sort so they are in smallest => greatest.
    this.enqueue = function (priority, key) {
        this.nodes.push({ key: key, priority: priority });
        this.sort();
    }

    //Returns first node in queue, this is the smallest
    this.dequeue = function () {
        return this.nodes.shift().key;
    }

    //Sort by priority, with lowest first
    this.sort = function() {
        this.nodes.sort(function(a, b) {
            return a.priority - b.priority;
        });
    }
    //Return true if empty
    this.isEmpty = function() {
        return !this.nodes.length;
    }
}


function Graph() {
    var INFINITY = 1 / 0;
    this.verticies = {};

    this.addVertex = function(name, edges) {
        this.verticies[name] = edges;
    }

    this.shorest_path = function(start, finish) {
        var nodes = new PriorityQueue();
        var distances = {};
        var previous = {};
        var path = [];

        var smallest, vertex, neighbor, alt;

        for (vertex in this.verticies) {
            if (vertex === start) {
                distances[vertex] = 0;
                nodes.enqueue(0, vertex);
            } else {
                distances[vertex] = INFINITY;
                nodes.enqueue(INFINITY, vertex);
            }

            previous[vertex] = null;
        }

        while (!nodes.isEmpty()) {
            smallest = nodes.dequeue();

            if (smallest === finish) {
                path;
                while (previous[smallest]) {
                    path.push(smallest);
                    smallest = previous[smallest];
                }
                break;
            }

            if (!smallest || distances[smallest] === INFINITY) {
                continue;
            }

            for (neighbor in this.verticies[smallest]) {
                alt = distances[smallest] = this.verticies[smallest][neighbor];

                if (alt < distances[neighbor]) {
                    distances[neighbor] = alt;
                    previous[neighbor] = smallest;

                    nodes.enqueue(alt, neighbor);
                }
            }
        }
        return path;
    }
    

    //Implementation
    var g = new Graph();
    g.addVertex('A', { B: 7, C: 8 });
    g.addVertex('B', { A: 7, F: 2 });
    g.addVertex('C', { A: 8, F: 6, G: 4 });
    g.addVertex('D', { F: 8 });
    g.addVertex('E', { H: 1 });
    g.addVertex('F', { B: 2, C: 6, D: 8, G: 9, H: 3 });
    g.addVertex('G', { C: 4, F: 9 });
    g.addVertex('H', { E: 1, F: 3 });

    // Log test, with the addition of reversing the path and prepending the first node so it's more readable
    console.log(g.shortestPath('A', 'H').concat(['A']).reverse());

}