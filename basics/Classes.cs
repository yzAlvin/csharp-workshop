public class Person
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int Age { get; set; }
	public Address address;

	public Person(string firstName, string lastName, int age, Address address)
	{
		FirstName = firstName;
		LastName = lastName;
		Age = age;
		this.address = address;
	}
}

public class Address
{
	public int Number { get; set; }
	public string Street { get; set; }

	public Address(int number, string street)
	{
		Number = number;
		Street = street;
	}
}