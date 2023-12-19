string[] text = File.ReadLines("7/input.txt").ToArray();
int sum = 0;

Dictionary<char, int> CardValues = new Dictionary<char, int>()
{
	{ 'A', 15 },
	{ 'K', 14 },
	{ 'Q', 13 },
	{ 'T', 12 },
	{ '9', 10 },
	{ '8', 9 },
	{ '7', 8 },
	{ '6', 7 },
	{ '5', 6 },
	{ '4', 5 },
	{ '3', 4 },
	{ '2', 3 },
	{ 'J', 2 },
};

List<(string, int)> hands = text.Select((x) => (x.Substring(0, 5), int.Parse(x.Substring(6)))).ToList();
hands.Sort((x, y) =>
{
	int xNum = EvaluateHand(x.Item1);
	int yNum = EvaluateHand(y.Item1);
	if (xNum != yNum)
	{
		return xNum.CompareTo(yNum);
	}

	for (int i = 0; i < 5; i++)
	{
		if (CardValues[x.Item1[i]] != CardValues[y.Item1[i]])
		{
			return CardValues[x.Item1[i]].CompareTo(CardValues[y.Item1[i]]);
        }
	}
	return 0;
});

for (int i = 0; i < hands.Count; i++)
{
	Console.WriteLine(hands[i].Item1 + " " + hands[i].Item2);
	sum += hands[i].Item2 * (i + 1);
}

Console.WriteLine("Sum: " + sum);


int EvaluateHand(string cards)
{
	if (cards.Contains('J'))
	{
		int best = -1;
		foreach (char cardChar in CardValues.Keys)
		{
			if (cardChar == 'J')
			{
				continue;
			}

            int val = EvaluateHand(cards.Replace('J', cardChar));

			if (val > best)
			{
				best = val;
			}
        }

		return best;
	}

    Dictionary<char, int> cardAmounts = new Dictionary<char, int>();

	for (int i = 0; i < cards.Length; i++)
	{
		if (cardAmounts.ContainsKey(cards[i]))
		{
			cardAmounts[cards[i]]++;
		}
		else
		{
			cardAmounts.Add(cards[i], 1);
		}
	}

	bool hasThree = false;
	bool hasTwo = false;
	foreach (var kvp in cardAmounts)
	{
        if (kvp.Value >= 4)
        {
            return kvp.Value;
        }

		if (kvp.Value == 3)
		{
			hasThree = true;
		}

		if (kvp.Value == 2)
		{
			if (hasTwo)
			{
				return 1;
			}

			hasTwo = true;
		}
    }

	if (hasTwo && hasThree)
	{
		return 3;
	}

	if (hasThree)
	{
		return 2;
	}

	if (hasTwo)
	{
		return 0;
	}

	return -1;
}