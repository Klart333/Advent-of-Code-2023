using System.Numerics;

string[] text = File.ReadLines("10/input.txt").ToArray();
int sum = 0;

Dictionary<char, (Vector2, Vector2)> pipes = new Dictionary<char, (Vector2, Vector2)>()
{
    { '|', (new Vector2(0, 1), new Vector2(0, -1)) }, 
    { '-', (new Vector2(1, 0), new Vector2(-1, 0)) }, 
    { 'L', (new Vector2(0, -1), new Vector2(1, 0)) }, 
    { 'J', (new Vector2(0, -1), new Vector2(-1, 0)) }, 
    { '7', (new Vector2(0, 1), new Vector2(-1, 0)) }, 
    { 'F', (new Vector2(0, 1), new Vector2(1, 0)) }, 
};


LinkedList<Vector2> path = new LinkedList<Vector2>();
Vector2 pathIndex = new Vector2();
for (int i = 0; i < text.Length; i++)
{
    if (text[i].Contains('S'))
    {
        pathIndex = new Vector2(text[i].IndexOf('S'), i);
        Console.WriteLine("Found Start at: " + pathIndex);
        break;
    }
}

path.AddFirst(pathIndex);
pathIndex.Y += 1;
while (text[(int)pathIndex.Y][(int)pathIndex.X] != 'S')
{
    //Console.WriteLine("Path Index: " + pathIndex + ", char: " + text[(int)pathIndex.Y][(int)pathIndex.X]);

    char pipe = text[(int)pathIndex.Y][(int)pathIndex.X];
    Vector2 connection1 = new Vector2(pathIndex.X + pipes[pipe].Item1.X, pathIndex.Y + pipes[pipe].Item1.Y);
    Vector2 connection2 = new Vector2(pathIndex.X + pipes[pipe].Item2.X, pathIndex.Y + pipes[pipe].Item2.Y);
    
    if (path.First.Value.X != connection1.X || path.First.Value.Y != connection1.Y)
    {
        path.AddFirst(pathIndex);
        pathIndex = connection1;
        continue;
    }

    path.AddFirst(pathIndex);
    pathIndex = connection2;
}

for (int i = 0; i < text.Length; i++)
{
    for (int j = 0; j < text[i].Length; j++)
    {
        if (text[i][j] == '.' || !path.Contains(new Vector2(j, i)))
        {
            if (IsEnclosed(i, j))
            {
                Console.WriteLine(j + ", " + i);
                sum += 1;
            }
        }
    }
}



Console.WriteLine("Sum: " + sum);

bool IsEnclosed(int yPos, int xPos)
{
    int wallCount = 0;
    LinkedListNode<Vector2>? last = null;
    for (int x = 1; x <= xPos; x++)
    {
        var index = new Vector2(xPos - x, yPos);
        if (path.Contains(index))
        {
            var tile = path.Find(index);
            if (last != null)
            {
                if (last.Previous == tile || last.Next == tile || (tile == path.Last && last == path.First) || (last == path.First && tile == path.Last))
                {
                    bool isPrevious = true;
                    if (last.Previous == null || last.Previous.Value.Y == yPos)
                    {
                        isPrevious = false;
                    }

                    Vector2 entryDirection = (isPrevious ? last.Previous.Value : last.Next.Value) - last.Value;
                    int y = (int)last.Value.Y;
                    int count = 0;
                    while (last.Value.Y == y && (isPrevious ? last.Next != null : last.Previous != null))
                    {
                        last = isPrevious ? last.Next : last.Previous;
                        count++;
                    }
                    x += Math.Max(count - 2, 0);
                    Vector2 exitDirection = last.Value - (isPrevious ? last.Previous.Value : last.Next.Value);

                    if (entryDirection == exitDirection)
                    {
                        wallCount--;
                    }
                    continue;
                }
            }

            last = tile;
            wallCount++;
        }
    }

    return wallCount % 2 != 0;
}
