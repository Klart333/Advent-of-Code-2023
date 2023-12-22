string[] text = File.ReadLines("15/input.txt").ToArray();
int sum = 0;

List<string> codes = text[0].Split(',').ToList();
Dictionary<int, List<(string, char)>> boxes = new Dictionary<int, List<(string, char)>>();

for (int i = 0; i < codes.Count; i++)
{
    bool isDash = !int.TryParse(codes[i][^1].ToString(), out _);
    string input = codes[i].Substring(0, codes[i].Length - (isDash ? 1 : 2));
    int hash = Hash(input);

    if (isDash)
    {
        if (boxes.TryGetValue(hash, out var value))
        {
            for (int g = 0; g < value.Count; g++)
            {
                if (value[g].Item1 == input)
                {
                    value.RemoveAt(g);
                    break;
                }
            }
        }
    }
    else
    {
        if (boxes.TryGetValue(hash, out var value))
        {
            bool added = false;
            for (int g = 0; g < value.Count; g++)
            {
                if (value[g].Item1 == input)
                {
                    value[g] = (input, codes[i][^1]);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                value.Add((input, codes[i][^1]));
            }
        }
        else
        {
            boxes.Add(hash, new List<(string, char)>() { (input, codes[i][^1]) });
        }
    }
}

for (int i = 0; i < 256; i++)
{
    if (boxes.TryGetValue(i, out var value))
    {
        for (int g = 0; g < value.Count; g++)
        {
            Console.WriteLine($"{value[g].Item1}: {i + 1} * {g + 1} * {int.Parse(value[g].Item2.ToString())}");
            sum += (i + 1) * (g + 1) * int.Parse(value[g].Item2.ToString());
        }
    }
}

Console.WriteLine("Sum: " + sum);
    
int Hash(string s)
{
    int value = 0;

    for (int i = 0; i < s.Length; i++)
    {
        value += s[i];
        value *= 17;
        value = value % 256;
    }

    return value;
}