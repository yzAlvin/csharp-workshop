// Data Types
// int		4 bytes 				Integers from -2,147,483,648 to 2,147,483,647
// long 	8 bytes 				Integers from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
// float 	4 bytes 				Floating point numbers with 6 to 7 decimal digits
// double 	8 bytes 				Floating point numbers with 15 decimal digits
// bool 	1 bit 					true or false
// char 	2 bytes 				Single character/letter, surrounded by single quotes
// string 	2 bytes per character 	Sequence of characters, surrounded by double quotes
public class DataTypesClass
{
	void DataTypes()
	{
		int myInt = 1;
		long myLong = 1;

		// float myFloat = 1.23; error!
		float myFloat = 1.23F;
		double myDouble = 1.23;

		bool myBool = true;

		// char myChar = "A"; error
		char myChar = 'A';
		string myString = "A";
		// string myString = 'A'; error

		// Usually you can stick with int and double and be fine.

		// You can also use 'var' instead of the type to initialise variables:

		var aInt = 1;
		var aLong = -1; // look at the implicit typing, it's not long!!
		var aFloat = 1.23F;
		var aDouble = 1.23;
		var aBool = true;
		var aChar = 'A';
		var aString = "A";
	}
}