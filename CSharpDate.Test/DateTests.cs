/*
Copyright 2013 Clay Anderson

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Globalization;
using Xunit;

namespace CSharpDate.Test
{
	public class DateTests
	{
		[Fact]
		public void CanConstructDefault()
		{
			Date d = new Date();

			Assert.Equal(Date.MinValue.Year, d.Year);
			Assert.Equal(Date.MinValue.Month, d.Month);
			Assert.Equal(Date.MinValue.Day, d.Day);
			Assert.Equal(Date.MinValue.DayOfWeek, d.DayOfWeek);
			Assert.Equal(Date.MinValue.DayOfYear, d.DayOfYear);
		}

		[Fact]
		public void CanConstruct()
		{
			Date d = new Date(2013, 4, 5);

			Assert.Equal(2013, d.Year);
			Assert.Equal(4, d.Month);
			Assert.Equal(5, d.Day);
			Assert.Equal(DayOfWeek.Friday, d.DayOfWeek);
			Assert.Equal(31 + 28 + 31 + 5, d.DayOfYear);
			Assert.Equal(635007168000000000, d.Ticks);
		}

    [Fact]
    public void CanConstructFromDateTime()
    {
      Date d1 = new Date(new DateTime(2001, 2, 3, 4, 5, 6, 7).AddTicks(8));
      Date d2 = new Date(2001, 2, 3);
      Assert.Equal(d1, d2);
      Assert.True(d1.Equals(d2));
    }

    [Fact]
    public void ToDateRemovesAllTimePortion()
    {
      Date d1 = new DateTime(2001, 2, 3, 4, 5, 6, 7).AddTicks(8).ToDate();
      Date d2 = new Date(2001, 2, 3);
      Assert.Equal(d1, d2);
      Assert.True(d1.Equals(d2));
    }

		[Fact]
		public void CanGetDateFromDateTime()
		{
			DateTime dt = new DateTime(2013, 4, 5, 6, 7, 8);
			Date d = dt.ToDate();

			Assert.Equal(dt.Year, d.Year);
			Assert.Equal(dt.Month, d.Month);
			Assert.Equal(dt.Day, d.Day);
		}

		[Fact]
		public void CanAddYears()
		{
			Date d1 = new Date(2013, 1, 30);
			Date d2 = d1.AddYears(3);

			Assert.Equal(2016, d2.Year);
			Assert.Equal(1, d2.Month);
			Assert.Equal(30, d2.Day);
		}

		[Fact]
		public void CanAddMonths()
		{
			Date d1 = new Date(2013, 2, 12);
			Date d2 = d1.AddMonths(4);

			Assert.Equal(2013, d2.Year);
			Assert.Equal(6, d2.Month);
			Assert.Equal(12, d2.Day);
		}

		[Fact]
		public void CanAddDays()
		{
			Date d1 = new Date(2012, 2, 29);
			Date d2 = d1.AddDays(1);

			Assert.Equal(2012, d2.Year);
			Assert.Equal(3, d2.Month);
			Assert.Equal(1, d2.Day);
		}

		[Fact]
		public void CanAddTimeSpanWithoutTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 + TimeSpan.FromDays(3);

			Assert.Equal(2013, d2.Year);
			Assert.Equal(4, d2.Month);
			Assert.Equal(8, d2.Day);
		}

		[Fact]
		public void CanAddTimeSpanWithTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 + new TimeSpan(3, 4, 5, 6);

			Assert.Equal(2013, d2.Year);
			Assert.Equal(4, d2.Month);
			Assert.Equal(8, d2.Day);
		}

		[Fact]
		public void CanSubtractTimeSpanWithoutTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 - TimeSpan.FromDays(3);

			Assert.Equal(2013, d2.Year);
			Assert.Equal(4, d2.Month);
			Assert.Equal(2, d2.Day);
		}

		[Fact]
		public void CanSubtractTimeSpanWithTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 - new TimeSpan(3, 4, 5, 6);

			Assert.Equal(2013, d2.Year);
			Assert.Equal(4, d2.Month);
			Assert.Equal(1, d2.Day);
		}

		[Fact]
		public void CanSubtractDates()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = new Date(2013, 4, 7);
			TimeSpan ts = d2 - d1;

			Assert.Equal(2, ts.Days);
			Assert.Equal(0, ts.Hours);
			Assert.Equal(0, ts.Minutes);
			Assert.Equal(0, ts.Seconds);
			Assert.Equal(0, ts.Milliseconds);
		}

		[Fact]
		public void CanCompareDates()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = new Date(2013, 4, 5);
			Date d3 = new Date(2014, 4, 8);

			Assert.True(d1 == d2);
			Assert.True(d1 != d3);
			Assert.True(d1 <= d2);
			Assert.True(d1 >= d2);
			Assert.True(d1 < d1.AddDays(3));
			Assert.True(d1 < d1.AddMonths(4));
			Assert.True(d1 < d1.AddYears(5));
			Assert.True(d1 <= d1.AddDays(3));
			Assert.True(d1 <= d1.AddMonths(4));
			Assert.True(d1 <= d1.AddYears(5));
			Assert.True(d1 > d1.AddDays(-3));
			Assert.True(d1 > d1.AddMonths(-4));
			Assert.True(d1 > d1.AddYears(-5));
			Assert.True(d1 >= d1.AddDays(-3));
			Assert.True(d1 >= d1.AddMonths(-4));
			Assert.True(d1 >= d1.AddYears(-5));
		}

		[Fact]
		public void CanImplicitCastToDateTime()
		{
			Date d = new Date(2013, 4, 5);
			DateTime dt = d;

			Assert.Equal(d.Year, dt.Year);
			Assert.Equal(d.Month, dt.Month);
			Assert.Equal(d.Day, dt.Day);
			Assert.Equal(d.DayOfWeek, dt.DayOfWeek);
			Assert.Equal(d.DayOfYear, dt.DayOfYear);
			Assert.Equal(0, dt.Hour);
			Assert.Equal(0, dt.Minute);
			Assert.Equal(0, dt.Second);
			Assert.Equal(0, dt.Millisecond);
		}

		[Fact]
		public void CanExplicitCastToDateTime()
		{
			DateTime dt = new DateTime(2000, 1, 2, 3, 4, 5);
			Date d = (Date)dt;

			Assert.Equal(dt.Year, d.Year);
			Assert.Equal(dt.Month, d.Month);
			Assert.Equal(dt.Day, d.Day);
		}

		[Fact]
		public void CanToShortString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToShortString();

				Assert.Equal("04/05/2013", s);
			}
		}

		[Fact]
		public void CanToLongString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToLongString();

				Assert.Equal("Friday, 05 April 2013", s);
			}
		}

		[Fact]
		public void CanToString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToString();

				Assert.Equal("04/05/2013", s);
			}
		}

		[Fact]
		public void CanParse()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = Date.Parse("2013-04-05");

				Assert.Equal(2013, d.Year);
				Assert.Equal(4, d.Month);
				Assert.Equal(5, d.Day);
				Assert.Equal(DayOfWeek.Friday, d.DayOfWeek);
				Assert.Equal(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Fact]
		public void CanParseWithTime()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = Date.Parse("2013-04-05 6:07:08 PM");

				Assert.Equal(2013, d.Year);
				Assert.Equal(4, d.Month);
				Assert.Equal(5, d.Day);
				Assert.Equal(DayOfWeek.Friday, d.DayOfWeek);
				Assert.Equal(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Fact]
		public void CantParseInvalidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Assert.Throws<FormatException>(() => Date.Parse("abc"));
			}
		}

		[Fact]
		public void CanTryParseValidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d;
				bool successs = Date.TryParse("2013-04-05", out d);

				Assert.True(successs);
				Assert.Equal(2013, d.Year);
				Assert.Equal(4, d.Month);
				Assert.Equal(5, d.Day);
				Assert.Equal(DayOfWeek.Friday, d.DayOfWeek);
				Assert.Equal(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Fact]
		public void CanTryParseInalidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d;
				bool success = Date.TryParse("abc", out d);

				Assert.False(success);
			}
		}

		[Fact]
		public void CanPresentRoundTripPattern()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s1 = d.ToString("O");
				string s2 = d.ToString("o");

				Assert.Equal(s1, s2);
				Assert.Equal("2013-04-05", s1);
				Assert.Equal("2013-04-05", s2);
			}
		}

		[Fact]
		public void CanPresentSortablePattern()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToString("s");

				Assert.Equal("2013-04-05", s);
			}
		}
	}
}
