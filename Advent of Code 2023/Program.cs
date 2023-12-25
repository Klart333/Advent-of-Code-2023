using System.Numerics;

string[] text = File.ReadLines("17/input.txt").ToArray();
int sum = 0;

List<List<Node>> map = new List<List<Node>>();
for (int x = 0; x < text[0].Length; x++)
{
    map.Add(new List<Node>());

    for (int y = 0; y < text.Length; y++)
    {
        map[x].Add(new Node(new Vector2(x, y), true, int.Parse(text[y][x].ToString())));
    }
}

Astar star = new Astar(map);

Stack<Node> path = star.FindPath(new Vector2(0, 0), new Vector2(text[0].Length - 1, text.Length - 1));

List<Node> list = new List<Node>(path);
for (int y = 0; y < text.Length; y++)
{
    for (int x = 0; x < text[y].Length; x++)
    {
        Console.Write(list.Exists(n => n.Position.X == x && n.Position.Y == y) ? "." : text[y][x]);
    }
    Console.WriteLine();
}

while (path.TryPop(out var value))
{
    //Console.WriteLine(value.Position);
    sum += (int)value.Weight;
}

Console.WriteLine("Sum: " + sum);

public class Node
{
    // Change this depending on what the desired size is for each element in the grid
    public static int NODE_SIZE = 1;
    public Node Parent;
    public Vector2 Position;
  
    public float Cost;
    public float Weight;
    public float F
    {
        get
        {
            return Cost;
        }
    }
    public bool Walkable;

    public Node(Vector2 pos, bool walkable, float weight = 1)
    {
        Parent = null;
        Position = pos;
        Cost = 1;
        Weight = weight;
        Walkable = walkable;
    }
}

public class Astar
{
    List<List<Node>> Grid;
    int GridRows
    {
        get
        {
            return Grid[0].Count;
        }
    }
    int GridCols
    {
        get
        {
            return Grid.Count;
        }
    }

    public Astar(List<List<Node>> grid)
    {
        Grid = grid;
    }

    public Stack<Node> FindPath(Vector2 Start, Vector2 End)
    {
        Node start = new Node(new Vector2((int)(Start.X / Node.NODE_SIZE), (int)(Start.Y / Node.NODE_SIZE)), true);
        Node end = new Node(new Vector2((int)(End.X / Node.NODE_SIZE), (int)(End.Y / Node.NODE_SIZE)), true);

        Stack<Node> Path = new Stack<Node>();
        Queue<Node> OpenList = new Queue<Node>();
        List<Node> ClosedList = new List<Node>();

        // add start node to Open List
        OpenList.Enqueue(start);
        start.Cost = 0;

        while (OpenList.TryDequeue(out var current) && !ClosedList.Exists(x => x.Position == end.Position))
        {
            ClosedList.Add(current);

            List<Node> nodes = GetPaths(new List<Node>() { current }, current, current.Parent, 3, current.Cost);
            nodes.Where(x => !OpenList.Contains(x)).ToList().ForEach(OpenList.Enqueue);
/*
            for (int x = 0; x < GridCols; x++)
            {
                for (int y = 0; y < GridRows; y++)
                {
                    if (Grid[x][y].Parent != null)
                    {
                        Console.Write(Grid[x][y].Parent.Position);
                    }
                    else
                    {
                        Console.Write("<    >");
                    }

                    Console.Write(", ");

                    *//*for (int k = 0; k < 3 - Math.Log10(Grid[x][y].Cost); k++)
                    {
                        Console.Write(" ");
                    }*//*
                }
                Console.WriteLine();
            }

            Console.ReadLine();*/
        }

        // construct path, if end was not closed return null
        if (!ClosedList.Exists(x => x.Position == end.Position))
        {
            return null;
        }

        Node target = ClosedList.Find(x => x.Position == end.Position);

        // if all good, return path
        Node temp = ClosedList[ClosedList.IndexOf(target)];
        if (temp == null) return null;
        do
        {
            Path.Push(temp);
            temp = temp.Parent;
        } while (temp != start && temp != null);
        return Path;
    }

    private List<Node> GetPaths(List<Node> path, Node current, Node parent, int depth = 3, float cost = 0)
    {
        if (depth == 0)
        {
            if (current.Cost == 1 || cost < current.Cost)
            {
                current.Cost = cost;
                /*for (int i = 1; i < path.Count; i++)
                {
                    path[i].Parent = path[i - 1];
                }*/
                current.Parent = path.First();
            }
            return [current];
        }

        List<Node> adjacencies = GetAdjacentNodes(current);
        List<Node> nodes = new List<Node>();

        foreach (Node node in adjacencies)
        {
            if (node.Position != parent?.Position)
            {
                Vector2 dir = node.Position - current.Position;

                float tripleStraightCost = 0;
                if (parent != null)
                {
                    Node straightParent = parent;
                    for (int i = 0; i < 3; i++)
                    {
                    }
                }

                cost += node.Weight + tripleStraightCost;
                path.Add(node);
                nodes.AddRange(GetPaths(path, node, current, depth - 1, cost));

                path.RemoveAt(path.Count - 1);
                cost -= node.Weight;
            }
        }

        HashSet<Node> nonOdes = new HashSet<Node>(nodes);

        return nonOdes.ToList();
    }

    private List<Node> GetAdjacentNodes(Node n)
    {
        List<Node> temp = new List<Node>();

        int row = (int)n.Position.Y;
        int col = (int)n.Position.X;

        if (row + 1 < GridRows)
        {
            temp.Add(Grid[col][row + 1]);
        }
        if (row - 1 >= 0)
        {
            temp.Add(Grid[col][row - 1]);
        }
        if (col - 1 >= 0)
        {
            temp.Add(Grid[col - 1][row]);
        }
        if (col + 1 < GridCols)
        {
            temp.Add(Grid[col + 1][row]);
        }

        return temp;
    }
}
