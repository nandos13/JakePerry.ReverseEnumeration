# JakePerry.ReverseEnumeration

[![NuGet version (JakePerry.ReverseEnumeration)](https://img.shields.io/nuget/v/JakePerry.ReverseEnumeration.svg?style=flat-square)](https://www.nuget.org/packages/JakePerry.ReverseEnumeration/)

A simple package that allows a List&lt;T> to be enumerated in reverse order.

### Usage

Everything in this package lives in the `JakePerry` namespace, so make sure you include it.
```
using JakePerry;
```

The easiest way to reverse a list is to simply use the `InReverseOrder()` extension method.
Most commonly, this is used directly in a `foreach` loop
```
foreach (var obj in list.InReverseOrder())
```

You can also cast the struct returned by `InReverseOrder()` to an `IEnumerable` or `IEnumerable<T>` as appropriate if you wish to pass the reversed collection to another method that accepts either of these interface types as an argument.
```
var list = new List<string>() { "a", "b", "c" };

PrintValues(list.InReverseOrder());

//...

void PrintValues(IEnumerable<string> values)
{
    foreach (var val in values)
        Console.WriteLine(val);
}
```
The above code snippet produces the output
>*c*<br/>
>*b*<br/>
>*a*
