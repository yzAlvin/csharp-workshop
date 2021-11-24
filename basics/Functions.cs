public class FunctionsClass
{
	void FunctionsExample()
	{
		Console.WriteLine("I am a function");
	}

	void ParameterExample(string name)
	{
		Console.WriteLine($"Hello {name}");
	}

	string CapitalizeName(string name)
	{
		return name.ToUpper();
	}

	string RepeatName(string name, int repetitions)
	{
		var result = name;
		for (int i = 0; i < repetitions; i++)
		{
			result += name;
		}
		return result;
	}

	int WordCount(string sentence)
	{
		return sentence.Split().Length;
	}
}