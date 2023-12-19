string[] text = File.ReadLines("9/input.txt").ToArray();
int sum = 0;

for (int i = 0; i < text.Length; i++)
{
    List<int> nums = text[i].Split(' ').Select(x => int.Parse(x)).ToList();
    List<List<int>> underlists = new List<List<int>>() { nums };

    while (GetUnderThing(underlists[underlists.Count - 1]) != null)
    {
        underlists.Add(GetUnderThing(underlists[underlists.Count - 1]));
    }
    underlists.Reverse();

    int last = 0;
    for (int g = 0; g < underlists.Count; g++)
    {
        last = underlists[g][0] - last;
    }

    Console.WriteLine("Adding: " + last);
    sum += last;
}


Console.WriteLine("Sum: " + sum);

 
List<int> GetUnderThing(List<int> list)
{
    List<int> underThing = new List<int>();
    bool onlyZeros = true;
    for (int i = 1; i < list.Count; i++)
    {
        int diff = list[i] - list[i - 1];
        underThing.Add(diff);

        if (diff != 0)
        {
            onlyZeros = false;
        }
    }

    if (onlyZeros)
    {
        return null;
    }

    return underThing;
}