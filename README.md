csharp-date
===========

Implementation of the missing C# Date struct.

Instructions
-------------------------
Include as a [single file](https://github.com/claycephus/csharp-date/blob/master/CSharpDate/Date.cs).

Example
-------------------------
```C#
Date today = Date.Today;
Date yesterday = Date.Today.AddDays(-1);
Date independenceDay = Date.Parse("2013-07-04");

independenceDay.ToLongString();    // "Thursday, July 4, 2013"
independenceDay.ToShortString();   // "7/4/2013"
independenceDay.ToString();        // "7/4/2013"
independenceDay.ToString("s");     // "2013-07-04"
int july = independenceDay.Month;  // 7
```

Licensed under [MIT](http://opensource.org/licenses/MIT).
