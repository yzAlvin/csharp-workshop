// List<T> 					Represents a list of objects that can be accessed by index. Provides methods to search, sort, and modify lists.
// Dictionary<TKey,TValue> 	Represents a collection of key/value pairs that are organized based on the key.
// Queue<T> 				Represents a first in, first out (FIFO) collection of objects.
// Stack<T> 				Represents a last in, first out (LIFO) collection of objects.
// SortedList<TKey,TValue> 	Represents a collection of key/value pairs that are sorted by key based on the associated IComparer<T> implementation.
public class CollectionsClass
{
	void ArrayExample()
	{
		var myArray = new int[3]; // an empty array with 3 spaces
		myArray[0] = 1; // add 1 to the array at index 0
		var anotherAray = new string[] { "Alvin", "Bob", "Jeff" }; // an array with items already in it
	}

	void ListExample()
	{
		var myList = new List<string>();
		myList.Add("Alvin");
		foreach (var item in myList)
		{
			Console.WriteLine(item);
		}

		var anotherList = new List<int> { 1, 2, 3 };
	}

	void DictionaryExample()
	{
		var myDictionary = new Dictionary<string, int>();
		myDictionary.Add("Alvin", 1);
		myDictionary.Add("Bob", 200);
		var cat = myDictionary["Cat"]; // will throw an error, cat does not exist!
		Console.WriteLine(myDictionary["Alvin"]);

		var cities = new Dictionary<string, string>()
		{
			{"Australia", "Melbourne"},
			{"UK", "London"},
			{"USA", "New York"},
			{"India", "New Delhi"}
		};
	}
}