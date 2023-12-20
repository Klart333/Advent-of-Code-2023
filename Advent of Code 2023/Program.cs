using System.Text.RegularExpressions;

int possibilities = 0;
string[] text = File.ReadLines("12/input.txt").ToArray();
double sum = 0;
Dictionary<string, List<int>> sequenceMemo = new Dictionary<string, List<int>>();
var cache = new Dictionary<string, long>();

Regex regex = new Regex(Regex.Escape("?"));

for (int i = 0; i < 1; i++)
{
    string unkown = text[i].Split(' ')[0];
    string longUnkown = unkown + '?' + unkown + '?' + unkown + '?' + unkown + '?' + unkown;
    List<int> facit = text[i].Split(' ')[1].Split(',').Select(x => int.Parse(x)).ToList();
    List<int> longFacit = new List<int>(facit);
    longFacit.AddRange(facit);
    longFacit.AddRange(facit);
    longFacit.AddRange(facit);
    longFacit.AddRange(facit);

    sum += GeneratePossibilities(longUnkown, longFacit);
}

Console.WriteLine("Sum: " + sum);
Console.WriteLine("Possibilities Explored: " + possibilities);
return;

int GeneratePossibilities(string input, List<int> facit) // I should be more intelligently creating the possibilities and ending branches when they no longer fullfill the facit, it needs to generate to the very end and then evaluate the entire string. Not good. Should also use memoization (caching) as there is a lot of overlapping calculations, where two branches divert then overlap later. Should probably generate the possibilities laterally and only using the relevant part of the input, then you can check if it matches as you go and later branches can use cached results.
{
    possibilities++;

    if (input.Contains('?'))
    {
        return GeneratePossibilities(regex.Replace(input, ".", 2), facit) + GeneratePossibilities(regex.Replace(input, "#", 2), facit);
    }

    if (EvaluateSequence(input).SequenceEqual(facit))
    {
        Console.WriteLine(input);

        return 1;
    }

    return 0;
}

List<int> EvaluateSequence(string input)
{
    if (sequenceMemo.ContainsKey(input))
    {
        return sequenceMemo[input];
    }

    List<int> result = new List<int>();
    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] != '#')
        {
            continue;
        }

        if (i > 0 && input[i - 1] == '#')
        {
            result[result.Count - 1]++;
        }
        else
        {
            result.Add(1);
        }
    }
    
    sequenceMemo.Add(input, result);
    return result;
}