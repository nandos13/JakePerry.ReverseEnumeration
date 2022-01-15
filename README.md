# JakePerry.ReverseEnumeration

[![NuGet version (JakePerry.ReverseEnumeration)](https://img.shields.io/nuget/v/JakePerry.ReverseEnumeration.svg?style=flat-square)](https://www.nuget.org/packages/JakePerry.ReverseEnumeration/)

A simple package that makes enumerating collections in reverse order easy.


### Purpose

Manually enumerating a collection in reverse order can feel tedious and can be prone to index errors (forget the `-1` in the initializer section or the `>=` in the condition and you'll run into an `ArgumentOutOfRangeException` or miss the first element).
A `foreach` loop is cleaner and convenient.
```
// Traditional reverse for-loop
for (int i = list.Count - 1; i >= 0; i--) { ... }

// New foreach loop
foreach (var obj in list.InReverseOrder()) { ... }
```

### Advantages

#### Immutability
Methods such as [Array.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.array.reverse) and [List<T>.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.reverse) mutate the original collection which is not always ideal. The `ReverseEnumerator<T>` does *not* mutate the original collection.

#### Allocations
The LINQ [Enumerable.Reverse](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.reverse) extension method:
- boxes the source enumerable if it's a struct
- allocates a new enumerable object
- clones the entire source collection before iteration

The reverse enumerator doesn't allocate any additional memory

#### Modification during enumeration
Modifying a `List<T>` while it is being enumerated in a `foreach` loop will result in an `InvalidOperationException` being thrown. The reverse enumerator is able to maintain this logic (but can optionally be disabled, more info [in the Usage section](#Usage)).

### Usage

The code lives in the `JakePerry` namespace, so make sure you include it.
```
using JakePerry;
```

Any collection implementing the `IList<T>` or `IReadOnlyList<T>` interface can be enumerated in reverse order via the `InReverseOrder()` extension method.
Most commonly, this is used directly in a `foreach` loop
```
foreach (var obj in list.InReverseOrder()) { ... }
```
The returned struct implements `IEnumerable`, `IEnumerable<T>`, `IReadOnlyCollection<T>` and `IReadOnlyList<T>`, so it can also be passed to any other methods accepting these types as required.

As mentioned above, reverse-enumerating a `List<T>` will maintain default logic that throws an exception if the collection is modified. This is achieved by using a different enumerator specifically for lists.
If required, the special-case list enumerator can be converted to a standard reverse enumerator via the `WithoutModifiedChecks` method or with an explicit cast:
```
var list = new List<string>() { "a", "b", "c" };

// Modifying the list in this loop will throw an InvalidOperationException
foreach (var obj in list.InReverseOrder()) { }

// WithoutModifiedChecks converts the enumerable to allow it to be modified in the loop
foreach (var obj in list.InReverseOrder().WithoutModifiedChecks()) { ... }

// We can also explicitly cast the enumerable to ReverseEnumerable<T>
foreach (var obj in (ReverseEnumerable<T>)list.InReverseOrder()) { ... }
```
