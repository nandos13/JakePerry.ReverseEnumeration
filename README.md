# JakePerry.ReverseEnumeration

[![NuGet version (JakePerry.ReverseEnumeration)](https://img.shields.io/nuget/v/JakePerry.ReverseEnumeration.svg?style=flat-square)](https://www.nuget.org/packages/JakePerry.ReverseEnumeration/)

A simple package that allows a List&lt;T> to be enumerated in reverse order.

### Usage

Make sure you include the required namespace
```
using JakePerry;
```
then simply use the `InReverseOrder()` extension method when enumerating
```
foreach (var obj in list.InReverseOrder())
```
