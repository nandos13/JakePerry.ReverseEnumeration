# JakePerry.ReverseEnumeration

[![NuGet version (JakePerry.ReverseEnumeration)](https://img.shields.io/nuget/v/JakePerry.ReverseEnumeration.svg?style=flat-square)](https://www.nuget.org/packages/JakePerry.ReverseEnumeration/)

A simple package that makes enumerating collections in reverse order easy.


### Purpose

Manually enumerating a collection in reverse order can feel tedious and can be prone to index errors (forget the `-1` in the initializer section or the `>=` in the condition and you'll run into an `ArgumentOutOfRangeException` or miss the first element).
A `foreach` loop is cleaner and convenient.
```cs
// Traditional reverse for-loop
for (int i = list.Count - 1; i >= 0; i--)
{
    var obj = list[i];
}

// New foreach loop
foreach (var obj in list.Reversed()) { ... }
```


### Advantages

#### Immutability
Methods such as [Array.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.array.reverse) and [List<T>.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.reverse) mutate the original collection which is not always ideal. The `ReverseEnumerator<T>` does *not* mutate the original collection.

This package also offers the option to maintain list immutability during reverse-enumeration with the use of the `List<T>.ReversedImmutable` extension method. When this method is used, an `InvalidOperationException` will be thrown if the list is modified during enumeration, matching the behavior of the default `List<T>.Enumerator` type.
More info [in the *Usage/List<T> and mutability* section below](#listt-and-mutability).

#### Allocations
The LINQ [Enumerable.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.reverse) extension method:
- boxes the source enumerable if it's a struct
- allocates a new enumerable object
- clones the entire source collection before iteration

The reverse enumerator doesn't allocate any additional memory. The package takes advantage of a compiler optimization by directly returning a value-type enumerator in the `GetEnumerator()` methods.


### Usage

The code lives in the `JakePerry` namespace, so make sure you include it.
```cs
using JakePerry;
```

Any collection implementing the `IList<T>` or `IReadOnlyList<T>` interface can be enumerated in reverse order via the `Reversed()` extension method. `ArraySegment<T>` is also supported.

Most commonly, this is used directly in a `foreach` loop
```cs
foreach (var obj in list.Reversed()) { ... }
```
The returned struct implements `IEnumerable`, `IEnumerable<T>`, `IReadOnlyCollection<T>` and `IReadOnlyList<T>`, so it can also be passed to any other methods accepting these types as required, at the cost of boxing the object.

#### List&lt;T&gt; and mutability
As mentioned above, it's possible to guarantee immutability for the `List<T>` type during reverse-enumeration, just as regular enumeration does. The `ReversedImmutable()` extension method uses a special enumerator
that performs the additional checks required & throws an `InvalidOperationException` if it's modified during enumeration. Note that this additional overhead is costly ([see benchmarks below](#benchmarks))
```cs
var list = new List<string>() { "a", "b", "c" };

// Using the Reversed method, the list can be modified as required
foreach (var obj in list.Reversed()) { ... }

// Using the ReversedImmutable method, modification during enumeration will cause an InvalidOperationException
foreach (var obj in list.ReversedImmutable()) { ... }
```

### Benchmarks

###### List enumeration benchmarks
List size: 16
```md
|                         Method |      Mean |    Error |   StdDev | Allocated |
|------------------------------- |----------:|---------:|---------:|----------:|
|                        ForLoop |  12.34 ns | 0.044 ns | 0.041 ns |         - |
|           Standard_ForeachLoop |  61.47 ns | 0.919 ns | 0.860 ns |         - |
|           Reversed_ForeachLoop |  83.78 ns | 0.246 ns | 0.218 ns |         - |
| Immutable_Reversed_ForeachLoop | 269.17 ns | 1.537 ns | 1.200 ns |         - |
```

List size: 4096
```md
|                         Method |      Mean |     Error |    StdDev | Allocated |
|------------------------------- |----------:|----------:|----------:|----------:|
|                        ForLoop |  2.618 us | 0.0089 us | 0.0079 us |         - |
|           Standard_ForeachLoop | 10.657 us | 0.0562 us | 0.0439 us |         - |
|           Reversed_ForeachLoop | 16.939 us | 0.0753 us | 0.0704 us |         - |
| Immutable_Reversed_ForeachLoop | 57.818 us | 0.3556 us | 0.3152 us |         - |
```

###### ArraySegment enumeration benchmarks
ArraySegment count: 16
```md
|               Method |      Mean |     Error |    StdDev |    Median | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
|              ForLoop |  7.625 ns | 0.1795 ns | 0.3237 ns |  7.466 ns |         - |
| Reversed_ForeachLoop | 60.111 ns | 1.2133 ns | 2.0272 ns | 59.453 ns |         - |
```

ArraySegment count: 4096
```md
|               Method |      Mean |     Error |    StdDev | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|
|              ForLoop |  1.332 us | 0.0211 us | 0.0234 us |         - |
| Reversed_ForeachLoop | 11.178 us | 0.2232 us | 0.3909 us |         - |
```
