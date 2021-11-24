public class IOClass
{
	void WritingExample()
	{
		Console.WriteLine("Hi");
		Console.Write("Hi");
	}

	void ReadingExample()
	{
		var name = Console.ReadLine();
		Console.WriteLine($"Hello {name}");
	}
}