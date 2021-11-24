public class ControlFlowClass
{
	void ifExample()
	{
		if (1 == 1)
		{
			Console.WriteLine("True!");
		}
	}

	void ifElseExample()
	{
		var name = "Alvin";
		if (name == "Alvin")
		{
			Console.WriteLine("Hi");
		}
		else
		{
			Console.WriteLine("Go away");
		}
	}

	void ifElseIfElseExample()
	{
		var name = "Alvin";
		if (name == "Alvin")
		{
			Console.WriteLine("Hi");
		}
		else if (name == "Bob")
		{
			Console.WriteLine("Heey");
		}
		else
		{
			Console.WriteLine("Go away");
		}
	}

	void switchExample()
	{
		var fruit = "Apple";
		switch (fruit)
		{
			case "Apple":
				Console.WriteLine("Red");
				break;
			case "Lemon":
				Console.WriteLine("Yellow");
				break;
			case "Lime":
			case "Watermelon":
			case "Pear":
				Console.WriteLine("Green");
				break;
			default:
				Console.WriteLine("Unknown!");
				break;
		}
	}

	string switchExpressionExample()
	{
		var fruit = "Apple";
		var color = fruit switch
		{
			"Apple" => "Red",
			"Lime" or "Pear" => "Green",
			_ => "Unknown"
		};
		return color;
	}

	void ForLoopExample()
	{
		for (int i = 0; i < 10; i++)
		{
			Console.WriteLine(i);
		}
	}

	void ForEachLoopExample()
	{
		var fruits = new string[] { "Apple", "Orange", "Mango" };
		foreach (var fruit in fruits)
		{
			Console.WriteLine(fruit);
		}
	}

	void WhileLoopExample()
	{
		var i = 0;
		while (i < 10)
		{
			Console.WriteLine(i);
			i++;
		}
	}

	void DoWhileLoopExample()
	{
		var i = 0;
		do
		{
			Console.WriteLine(i);
		} while (i < 0);
	}
}
