string[] text = File.ReadLines("14/input.txt").ToArray();
int sum = 0;
int[,] map = new int[text.Length, text[0].Length];

for (int i = 0; i < text.Length; i++)
{
    for (int g = 0; g < text[0].Length; g++)
    {
        map[g, i] = text[i][g] == '.' ? 0 : text[i][g] == '#' ? 1 : 2;
    }
}

int interval = 0;
int[,] copy = new int[text.Length, text[0].Length];
int prettySureItsDone = 0;
int last = 0;
bool done = false;
for (int i = 0; i < 10000000 && !done; i++)
{
    Cycle();

    if (i >= 1000)
    {
        /*sum = 0;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 2)
                {
                    sum += map.GetLength(1) - y;
                }
            }
        }

        Console.WriteLine($"Sum: {sum}");
        Console.ReadLine();*/

        if (i == 1000)
        {
            copy = map.Clone() as int[,];
        }
        else
        {
            bool checksOut = true;
            for (int x = 0; x < map.GetLength(0) && checksOut; x++)
            {
                for (int y = 0; y < map.GetLength(1) && checksOut; y++)
                {
                    if (copy[x,y] != map[x, y])
                    {
                        checksOut = false;
                    }
                }
            }

            if (checksOut)
            {
                if (i - last == interval)
                {
                    

                    prettySureItsDone++;

                    if (prettySureItsDone > 10)
                    {
                        int diff = (1000000000 - i) % interval;
                        Console.WriteLine("diff: " + diff);
                        for (int k = 0; k < diff + interval - 1; k++)
                        {
                            Cycle();
                        }
                        done = true;
                        break;
                    }
                }
                else
                {
                    prettySureItsDone = 0;
                }

                interval = i - last;
                last = i;
            }
        }
    }
/*
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            Console.Write(map[x, y] == 0 ? "." : map[x, y] == 1 ? "#" : "O");
        }
        Console.WriteLine();
    }
    Console.ReadLine();
*/
    if (i % 100000 == 500)
    {
        Console.WriteLine($"{((i + 1.0f) / 1000000000.0f) * 100}%");
    }
}
void Cycle()
{
    RollNorth(map);
    RollWest(map);
    RollSouth(map);
    RollEast(map);
}
sum = 0;
for (int x = 0; x < map.GetLength(0); x++)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        if (map[x, y] == 2)
        {
            sum += map.GetLength(1) - y;
        }
    }
}

Console.WriteLine($"Sum: {sum}");

void RollNorth(int[,] map)
{
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] != 2)
            {
                continue;
            }

            bool moved = false;
            int index = y;
            for (int k = y - 1; k >= 0; k--)
            {
                if (map[x, k] == 0)
                {
                    index = k;
                    moved = true;
                }
                else
                {
                    break;
                }
            }

            if (moved)
            {
                map[x, y] = 0;
                map[x, index] = 2;
            }
        }
    }
}

void RollWest(int[,] map)
{
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] != 2)
            {
                continue;
            }

            bool moved = false;
            int index = x;
            for (int k = x - 1; k >= 0; k--)
            {
                if (map[k, y] == 0)
                {
                    index = k;
                    moved = true;
                }
                else
                {
                    break;
                }
            }

            if (moved)
            {
                map[x, y] = 0;
                map[index, y] = 2;
            }
        }
    }
}

void RollSouth(int[,] map)
{
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = map.GetLength(1) - 1; y >= 0; y--)
        {
            if (map[x, y] != 2)
            {
                continue;
            }

            bool moved = false;
            int index = y;
            for (int k = y + 1; k < map.GetLength(1); k++)
            {
                if (map[x, k] == 0)
                {
                    index = k;
                    moved = true;
                }
                else
                {
                    break;
                }
            }

            if (moved)
            {
                map[x, y] = 0;
                map[x, index] = 2;
            }
        }
    }
}

void RollEast(int[,] map)
{
    for (int x = map.GetLength(0) - 1; x >= 0; x--)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] != 2)
            {
                continue;
            }

            bool moved = false;
            int index = x;
            for (int k = x + 1; k < map.GetLength(0); k++)
            {
                if (map[k, y] == 0)
                {
                    index = k;
                    moved = true;
                }
                else
                {
                    break;
                }
            }

            if (moved)
            {
                map[x, y] = 0;
                map[index, y] = 2;
            }
        }
    }
}