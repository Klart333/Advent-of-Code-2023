using System.Text;

string[] text = File.ReadLines("13/input.txt").ToArray();
int sum = 0;

List<Formation> formations = new List<Formation>();
Formation mation = new Formation();
for (int i = 0; i < text.Length; i++)
{
	if (string.IsNullOrEmpty(text[i]))
	{
		formations.Add(mation);
		mation = new Formation();
	}
	else
	{
		mation.Lines.Add(text[i]);
	}
}
formations.Add(mation);

for (int i = 0; i < formations.Count; i++)
{
    bool done = false;
    int tries = 0;
    int notAllowedX = GetRow(formations[i]).ElementAtOrDefault(0);
    int notAllowedY = GetColumn(formations[i]).ElementAtOrDefault(0);

    for (int y = 0; y < formations[i].VerticalLength && !done; y++)
    {
        for (int x = 0; x < formations[i].Length && !done; x++)
        {
            tries++;
            StringBuilder sb = new StringBuilder(formations[i].Lines[y]);

            sb[x] = sb[x] == '#' ? '.' : '#';
            formations[i].Lines[y] = sb.ToString();

            List<int> result = GetRow(formations[i]);
            for (int k = 0; k < result.Count; k++)
            {
                if (result[k] > 0 && result[k] != notAllowedX)
                {
                    Console.WriteLine("Column " + (x + 1) + " matches");

                    sum += result[k];
                    done = true;
                    break;
                }
            }

            result = GetColumn(formations[i]);
            for (int k = 0; k < result.Count; k++)
            {
                if (result[k] > 0 && result[k] != notAllowedY)
                {
                    Console.WriteLine("Row " + (y + 1) + " matches");

                    sum += result[k];
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                sb[x] = sb[x] == '#' ? '.' : '#';
                formations[i].Lines[y] = sb.ToString();
            }
        }
    }

    //sum += GetRow(formations[i]);
    //
    //sum += GetColumn(formations[i]);

    Console.WriteLine($"{(int)((i + 1) / (float)formations.Count * 100)}%, Tries: {tries}");
}

Console.WriteLine("Sum: " + sum);

static List<int> GetRow(Formation formation)
{
    HashSet<int> result = new HashSet<int>();
    for (int y = 0; y < formation.VerticalLength - 1; y++)
    {
        bool checksOut = true;
        for (int g = y + 1; g < Math.Min(formation.VerticalLength, y * 2 + 2); g++)
        {
            string og = formation.Lines[y - (g - 1 - y)];
            string reflection = formation.Lines[g];

            if (og != reflection)
            {
                checksOut = false;
                break;
            }
        }

        if (checksOut && !result.Contains((y + 1) * 100))
        {
            result.Add((y + 1) * 100);
        }
    }

    return result.ToList();
}

static List<int> GetColumn(Formation formation)
{
    HashSet<int> result = new HashSet<int>();
    for (int x = 0; x < formation.Length - 1; x++)
    {
        bool checksOut = true;
        for (int g = x + 1; g < Math.Min(formation.Length, x * 2 + 2); g++)
        {

            string og = formation.GetVerticalLine(x - (g - 1 - x));
            string reflection = formation.GetVerticalLine(g);

            if (og != reflection)
            {
                checksOut = false;
                break;
            }
        }

        if (checksOut && !result.Contains(x + 1))
        {
            result.Add(x + 1);
        }
    }

    return result.ToList();
}

struct Formation
{
    public List<string> Lines;

	public int Length => Lines[0].Length;
	public int VerticalLength => Lines.Count;

	public string GetVerticalLine(int x)
	{
		string line = "";
		for (int h = 0; h < VerticalLength; h++)
		{
            line += Lines[h][x];
        }

		return line;
    }

	public Formation()
	{
		Lines = new List<string>();
	}
} 