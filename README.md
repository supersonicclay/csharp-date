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
Date piDay = Date.Parse("2015-03-14");

piDay.ToLongString();    // "Saturday, March 14, 2015"
piDay.ToShortString();   // "3/14/2015"
piDay.ToString();        // "3/14/2015"
piDay.ToString("s");     // "2015-03-14"
int march = piDay.Month;  // 3
```

Licensed under [MIT](http://opensource.org/licenses/MIT).
