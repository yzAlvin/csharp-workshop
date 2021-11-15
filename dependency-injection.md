# Dependency Injection

## Credits

This doc has been adapted from the Learn Go with Tests’ book to use C# syntax. It attempts to explain the rationale behind dependency injection, providing simple examples.

**[You can find the original chapter from the book here](https://quii.gitbook.io/learn-go-with-tests/go-fundamentals/dependency-injection)**

## Intro

It is assumed that you have some understanding of inheritance, interfaces, and how to run C# code, as they will be needed for this.

There are _a lot_ of misunderstandings around dependency injection around the programming community. Hopefully, this guide will show you how

* You don't need a framework
* It does not overcomplicate your design
* It facilitates testing
* It allows you to write great, general-purpose functions.

We want to write a function that greets someone, but we are going to be testing the _actual printing_.

To get the most benefit from this open your IDE and follow along with the examples.

Just to recap, here is what that function could look like

```csharp
public void Greet(string name) 
{
	Console.WriteLine($"Hello, {name}");
}
```

But how can we test this? Calling `Console.WriteLine` prints to stdout, which is pretty hard for us to capture using the testing framework.

What we need to do is to be able to **inject** \(which is just a fancy word for pass in\) the dependency of printing.

**Our function doesn't need to care **_**where**_** or **_**how**_** the printing happens, so we should accept an **_**interface**_** rather than a concrete type.**

If we do that, we can then change the implementation to print to something we control so that we can test it. In "real life" you would inject in something that writes to stdout.

If you look at the source code of [`Console.WriteLine`](https://source.dot.net/#System.Console/System/Console.cs,5ac7c4fda643413b) you can see a way for us to hook in

```csharp
public static void WriteLine(string? value)
{
	Out.WriteLine(value);
}
```

Interesting! Under the hood `Console.WriteLine` just calls `Out.WriteLine`.

What exactly _is_ an `Out`? 

A `TextWriter`, the source [here](https://source.dot.net/#System.Private.CoreLib/TextWriter.cs,6e84a88dc2be46e3)

```csharp
public abstract partial class TextWriter : MarshalByRefObject, IDisposable, IAsyncDisposable
{
	.... // code omitted for brevity
	public virtual void WriteLine(string? value)
	{
		if (value != null)
		{
			Write(value);
		}
		Write(CoreNewLineStr);
	}
	.... // code omitted for brevity
}
```

From this we can infer that `Console.WriteLine` uses an instance of `TextWriter` (eg. I will call it `instance`), calling `instance.WriteLine` under the hood;

As you write more C# code you will find this interface popping up a lot because it's a great general purpose interface for "put this data somewhere".

So we know under the covers we're ultimately using `TextWriter` to send our greeting somewhere. Let's use this existing abstraction to make our code testable and more reusable.

## Write the test first

```csharp
[Fact]
public void TestGreet()
{
	var testWriter = new FakeWriter();
	Greet(testWriter, "Alvin");
	Assert.Equal("Hello, Alvin", testWriter.output.First());
}
```

The `FakeWriter` class (which we will define soon) implements the `TextWriter`, because it has the method `WriteLine(string str)`.

So we'll use it in our test to send in as our `TextWriter` and then we can check what was written to it after we invoke `Greet`

## Try and run the test

The test will not compile

```text
No overload for greet takes 2 arguments
FakeWriter does not contain a definition for output ... 
```

## Write the minimal amount of code for the test to run and check the failing test output

_Listen to the compiler_ and fix the problem.

```csharp
public void Greet(FakeWriter writer, string name) 
{
	writer.WriteLine($"Hello {name}");
}
```

`some error`

<!-- `Hello, Chris di_test.go:16: got '' want 'Hello, Chris'`

The test fails. Notice that the name is getting printed out, but it's going to stdout. -->

## Write enough code to make it pass

Use the writer to send the greeting to the output in our test. The type signature of `WriteLine` is string -> void, so we just need to define it but instaed of writing to stdout, we could add it to some collection.

```csharp
public class FakeWriter : TextWriter
{
	public List<string> output;
	public FakeWriter()
	{
		output = new List<string>();
	}
	public override void WriteLine(string str)
	{
		output.Add(str);
	}

	// Whatever methods we need to implement from TextWriter
}
```

The test now passes.

Run the main program again, does it do what you would expect to happen?

## Refactor

Earlier the compiler told us to pass in a `FakeWriter`. This is technically correct but not very useful.

To demonstrate this, try wiring up the `Greet` function into a C# application where we want it to print to stdout.

```csharp
public static void main()
{
	Greet(Console.Out, "Alvin");
}
```

`Cannot convert from System.IO TextWriter to FakeWriter`

As discussed earlier `Console.WriteLine` is a method in `TextWriter` which we know both `Console.Out` and `FakeWriter` implement.

If we change our code to use the more general purpose interface we can now use it in both tests and in our application.

```csharp
public void Greet(TextWriter writer, string name) 
{
	writer.WriteLine($"Hello {name}");
}
```

<!-- ## More on io.Writer

What other places can we write data to using `io.Writer`? Just how general purpose is our `Greet` function?

### The Internet

Run the following

```go
package main

import (
	"fmt"
	"io"
	"log"
	"net/http"
)

func Greet(writer io.Writer, name string) {
	fmt.Fprintf(writer, "Hello, %s", name)
}

func MyGreeterHandler(w http.ResponseWriter, r *http.Request) {
	Greet(w, "world")
}

func main() {
	log.Fatal(http.ListenAndServe(":5000", http.HandlerFunc(MyGreeterHandler)))
}
```

Run the program and go to [http://localhost:5000](http://localhost:5000). You'll see your greeting function being used.

HTTP servers will be covered in a later chapter so don't worry too much about the details.

When you write an HTTP handler, you are given an `http.ResponseWriter` and the `http.Request` that was used to make the request. When you implement your server you _write_ your response using the writer.

You can probably guess that `http.ResponseWriter` also implements `io.Writer` so this is why we could re-use our `Greet` function inside our handler. -->

## Extensions

* Make the greet method print "Bye, Bob!", if the name passed in is Bob. Make sure your tests cover this.

* Make the greet method print something else after Hello __name__, cover it with a test and run the program to see it works in both places.

* Try doing the same but with `Console.ReadLine()`, perhaps the `Greet` method could take in a name from `ReadLine()`.

## Wrapping up

Our first round of code was not easy to test because it wrote data to somewhere we couldn't control.

_Motivated by our tests_ we refactored the code so we could control _where_ the data was written by **injecting a dependency** which allowed us to:

* **Test our code** If you can't test a function _easily_, it's usually because of dependencies hard-wired into a function _or_ global state. If you have a global database connection pool for instance that is used by some kind of service layer, it is likely going to be difficult to test and they will be slow to run. DI will motivate you to inject in a database dependency \(via an interface\) which you can then mock out with something you can control in your tests.
* **Separate our concerns**, decoupling _where the data goes_ from _how to generate it_. If you ever feel like a method/function has too many responsibilities \(generating data _and_ writing to a db? handling HTTP requests _and_ doing domain level logic?\) DI is probably going to be the tool you need.
* **Allow our code to be re-used in different contexts** The first "new" context our code can be used in is inside tests. But further on if someone wants to try something new with your function they can inject their own dependencies.

### What about mocking? I hear you need that for DI and also it's evil

Mocking will be covered in detail later \(and it's not evil\). You use mocking to replace real things you inject with a pretend version that you can control and inspect in your tests. In our case though, the standard library had something ready for us to use.

### The C# standard library is really good, take time to study it

By having some familiarity with the `TextWriter` interface we are able to use it in our test as our `TextWriter` and then we can use other `TextWriter`s from the standard library to use our function in a command line app or in web server.

The more familiar you are with the standard library the more you'll see these general purpose interfaces which you can then re-use in your own code to make your software reusable in a number of contexts.
