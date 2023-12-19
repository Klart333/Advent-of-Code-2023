string[] text = File.ReadLines("8/input.txt").ToArray();
double sum = 0;
string directions = text[0];

Dictionary<string, (string, string)> map = new Dictionary<string, (string, string)>();
List<string> nodes = new List<string>();
for (int i = 2; i < text.Length; i++)
{
    map.Add(text[i].Substring(0, 3), (text[i].Substring(7, 3), text[i].Substring(12, 3)));

    if (text[i][2] == 'A')
    {
        nodes.Add(text[i].Substring(0, 3));
    }
}

List<int> amounts = new List<int>();
for (int i = 0; i < nodes.Count; i++)
{
    int directionIndex = 0;
    int count = 0;

    while (nodes[i][2] != 'Z')
    {
        count++;
        char direction = directions[directionIndex++ % directions.Length];
        if (direction == 'L')
        {
            nodes[i] = map[nodes[i]].Item1;
        }
        else
        {
            nodes[i] = map[nodes[i]].Item2;
        }
    }

    amounts.Add(count);
}

sum = 1;
amounts.ForEach(amount => sum *= amount);
amounts.ForEach(amount => Console.Write(amount + ", "));
Console.WriteLine();

Console.WriteLine("Sum: " + sum);
// DO ONLINE CALCULATOR FOR LAST STEP, FIND LCM
