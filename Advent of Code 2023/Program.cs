using System.Numerics;

string[] text = File.ReadLines("16/input.txt").ToArray();
int sum = 0;

int best = 0;
for (int x = -1; x <= text[0].Length; x++)
{
    for (int y = -1; y <= text[0].Length; y++)
    {
        if (x >= 0 && x < text[0].Length && y >= 0 && y < text[0].Length)
        {
            continue;
        }

        Vector2 index = new Vector2(x, y);
        Console.WriteLine("Index: " + index);
        Calculate(index, new Vector2(1, 0));
        Calculate(index, new Vector2(-1, 0));
        Calculate(index, new Vector2(0, 1));
        Calculate(index, new Vector2(0, -1));
    }
}

sum = best;
Console.WriteLine("Sum: " + sum);

int Calculate(Vector2 inputIndex, Vector2 inputDirection)
{
    List<Light> lights = [new Light(inputDirection, inputIndex)];
    HashSet<(Vector2, Vector2)> spawnedLightsSet = new HashSet<(Vector2, Vector2)>();
    bool evaluated = false;
    int[,] energizeArray = new int[text[0].Length, text.Length];
    int lastEnergizedCount = -1;
    int iteration = 0;
    while (!evaluated)
    {
        for (int i = lights.Count - 1; i >= 0; i--)
        {
            lights[i].Move();
            if (lights[i].Index.X < 0 || lights[i].Index.X >= text[0].Length || lights[i].Index.Y < 0 || lights[i].Index.Y >= text.Length)
            {
                lights.RemoveAt(i);
                continue;
            }

            char symbol = text[(int)lights[i].Index.Y][(int)lights[i].Index.X];
            energizeArray[(int)lights[i].Index.X, (int)lights[i].Index.Y] = 1;

            switch (symbol)
            {
                case '.':
                    continue;

                case '|':
                    if (lights[i].Direction.X == 0)
                    {
                        continue;
                    }
                    else
                    {
                        lights[i].SetDirection(new Vector2(0, 1));

                        Light light = new Light(new Vector2(0, -1), lights[i].Index);
                        if (!spawnedLightsSet.Contains((new Vector2(0, -1), lights[i].Index)))
                        {
                            lights.Add(light);
                            spawnedLightsSet.Add((new Vector2(0, -1), lights[i].Index));
                        }
                    }
                    break;

                case '-':
                    if (lights[i].Direction.Y == 0)
                    {
                        continue;
                    }
                    else
                    {
                        lights[i].SetDirection(new Vector2(1, 0));

                        Light light = new Light(new Vector2(-1, 0), lights[i].Index);
                        if (!spawnedLightsSet.Contains((new Vector2(-1, 0), lights[i].Index)))
                        {
                            lights.Add(light);
                            spawnedLightsSet.Add((new Vector2(-1, 0), lights[i].Index));
                        }
                    }
                    break;

                case '/':
                    lights[i].SetDirection(new Vector2(-lights[i].Direction.Y, -lights[i].Direction.X));
                    break;

                case '\\':
                    lights[i].SetDirection(new Vector2(lights[i].Direction.Y, lights[i].Direction.X));
                    break;

                default:
                    break;
            }
        }

        if (iteration++ % 100 == 99)
        {
            int count = 0;
            foreach (var item in energizeArray)
            {
                if (item == 1)
                {
                    count++;
                }
            }

            if (lastEnergizedCount == count)
            {
                Console.WriteLine("Count: " + count);

                evaluated = true;

                if (count > best)
                {
                    best = count;
                }

                return count;
            }

            lastEnergizedCount = count;
        }

        /*Console.WriteLine("Lights: " + lights.Count);
        for (int y = 0; y < text.Length; y++)
        {
            for (int x = 0; x < text[y].Length; x++)
            {
                if (lights.Exists(light => light.Index.X == x && light.Index.Y == y))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(text[y][x]);
                }
            }

            Console.WriteLine();
        }
        Console.ReadLine();*/
    }

    return lastEnergizedCount;
}

class Light
{
    public Vector2 Direction;
    public Vector2 Index;

    public void Move()
    {
        Index.X += Direction.X;
        Index.Y += Direction.Y;
    }

    public void SetDirection(Vector2 dir)
    {
        Direction = dir;
    }

    public Light(Vector2 direction, Vector2 index)
    {
        Direction = direction;
        Index = index;
    }
}
