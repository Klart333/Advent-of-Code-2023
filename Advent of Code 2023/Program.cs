using System.Numerics;

string[] text = File.ReadLines("11/input.txt").ToArray();
double sum = 0;

List<int> rowsToExpand = new List<int>();
List<int> columnsToExpand = new List<int>();
for (int y = 0; y < text.Length; y++)
{
	if (!text[y].Contains('#'))
	{
		rowsToExpand.Add(y);
	}
}

for (int x = 0; x < text[0].Length; x++)
{
	bool galaxy = false;
	for (int y = 0; y < text.Length; y++)
	{
		if (text[y][x] == '#')
		{
			galaxy = true;
			break;
		}
	}

	if (!galaxy)
	{
		columnsToExpand.Add(x);
	}
}

List<Vector2> galaxies = new List<Vector2>();

for (int y = 0; y < text.Length; y++)
{
	for (int x = 0; x < text[y].Length; x++)
	{
		if (text[y][x] == '#')
		{
			galaxies.Add(new Vector2(x, y));
		}
	}
}

for (int i = 0; i < galaxies.Count; i++)
{
	for (int g = i + 1; g < galaxies.Count; g++)
	{
		int xDiff = (int)galaxies[g].X - (int)galaxies[i].X;
		int yDiff = (int)galaxies[g].Y - (int)galaxies[i].Y;

		int xDistance = 0;
		int yDistance = 0;
		for (int x = 1; x <= Math.Abs(xDiff); x++)
		{
			int index = (int)galaxies[i].X + x * Math.Sign(xDiff);
			if (columnsToExpand.Contains(index))
			{
				xDistance += 1000000;
			}
			else
			{
				xDistance += 1;
			}
		}

		for (int y = 1; y <= Math.Abs(yDiff); y++)
		{
            int index = (int)galaxies[i].Y + y * Math.Sign(yDiff);
            if (rowsToExpand.Contains(index))
            {
                yDistance += 1000000;
            }
            else
            {
                yDistance += 1;
            }
        }

		sum += xDistance + yDistance;
	}
}

Console.WriteLine("Sum: " + sum); 