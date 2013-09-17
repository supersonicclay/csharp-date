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
using NUnit.Framework;

namespace CSharpDate.Test
{
	[TestFixture]
	public class DateTests
	{
		[Test]
		public void CanConstructDefault()
		{
			Date d = new Date();

			Assert.AreEqual(Date.MinValue.Year, d.Year);
			Assert.AreEqual(Date.MinValue.Month, d.Month);
			Assert.AreEqual(Date.MinValue.Day, d.Day);
			Assert.AreEqual(Date.MinValue.DayOfWeek, d.DayOfWeek);
			Assert.AreEqual(Date.MinValue.DayOfYear, d.DayOfYear);
		}

		[Test]
		public void CanConstruct()
		{
			Date d = new Date(2013, 4, 5);

			Assert.AreEqual(2013, d.Year);
			Assert.AreEqual(4, d.Month);
			Assert.AreEqual(5, d.Day);
			Assert.AreEqual(DayOfWeek.Friday, d.DayOfWeek);
			Assert.AreEqual(31 + 28 + 31 + 5, d.DayOfYear);
		}

		[Test]
		public void CanGetDateFromDateTime()
		{
			DateTime dt = new DateTime(2013, 4, 5, 6, 7, 8);
			Date d = dt.ToDate();

			Assert.AreEqual(dt.Year, d.Year);
			Assert.AreEqual(dt.Month, d.Month);
			Assert.AreEqual(dt.Day, d.Day);
		}

		[Test]
		public void CanAddYears()
		{
			Date d1 = new Date(2013, 1, 30);
			Date d2 = d1.AddYears(3);

			Assert.AreEqual(2016, d2.Year);
			Assert.AreEqual(1, d2.Month);
			Assert.AreEqual(30, d2.Day);
		}

		[Test]
		public void CanAddMonths()
		{
			Date d1 = new Date(2013, 2, 12);
			Date d2 = d1.AddMonths(4);

			Assert.AreEqual(2013, d2.Year);
			Assert.AreEqual(6, d2.Month);
			Assert.AreEqual(12, d2.Day);
		}

		[Test]
		public void CanAddDays()
		{
			Date d1 = new Date(2012, 2, 29);
			Date d2 = d1.AddDays(1);

			Assert.AreEqual(2012, d2.Year);
			Assert.AreEqual(3, d2.Month);
			Assert.AreEqual(1, d2.Day);
		}

		[Test]
		public void CanAddTimeSpanWithoutTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 + TimeSpan.FromDays(3);

			Assert.AreEqual(2013, d2.Year);
			Assert.AreEqual(4, d2.Month);
			Assert.AreEqual(8, d2.Day);
		}

		[Test]
		public void CanAddTimeSpanWithTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 + new TimeSpan(3, 4, 5, 6);

			Assert.AreEqual(2013, d2.Year);
			Assert.AreEqual(4, d2.Month);
			Assert.AreEqual(8, d2.Day);
		}

		[Test]
		public void CanSubtractTimeSpanWithoutTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 - TimeSpan.FromDays(3);

			Assert.AreEqual(2013, d2.Year);
			Assert.AreEqual(4, d2.Month);
			Assert.AreEqual(2, d2.Day);
		}

		[Test]
		public void CanSubtractTimeSpanWithTime()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = d1 - new TimeSpan(3, 4, 5, 6);

			Assert.AreEqual(2013, d2.Year);
			Assert.AreEqual(4, d2.Month);
			Assert.AreEqual(1, d2.Day);
		}

		[Test]
		public void CanSubtractDates()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = new Date(2013, 4, 7);
			TimeSpan ts = d2 - d1;

			Assert.AreEqual(2, ts.Days);
			Assert.AreEqual(0, ts.Hours);
			Assert.AreEqual(0, ts.Minutes);
			Assert.AreEqual(0, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		[Test]
		public void CanCompareDates()
		{
			Date d1 = new Date(2013, 4, 5);
			Date d2 = new Date(2013, 4, 5);
			Date d3 = new Date(2014, 4, 8);

			Assert.IsTrue(d1 == d2);
			Assert.IsTrue(d1 != d3);
			Assert.IsTrue(d1 <= d2);
			Assert.IsTrue(d1 >= d2);
			Assert.IsTrue(d1 < d1.AddDays(3));
			Assert.IsTrue(d1 < d1.AddMonths(4));
			Assert.IsTrue(d1 < d1.AddYears(5));
			Assert.IsTrue(d1 <= d1.AddDays(3));
			Assert.IsTrue(d1 <= d1.AddMonths(4));
			Assert.IsTrue(d1 <= d1.AddYears(5));
			Assert.IsTrue(d1 > d1.AddDays(-3));
			Assert.IsTrue(d1 > d1.AddMonths(-4));
			Assert.IsTrue(d1 > d1.AddYears(-5));
			Assert.IsTrue(d1 >= d1.AddDays(-3));
			Assert.IsTrue(d1 >= d1.AddMonths(-4));
			Assert.IsTrue(d1 >= d1.AddYears(-5));
		}

		[Test]
		public void CanImplicitCastToDateTime()
		{
			Date d = new Date(2013, 4, 5);
			DateTime dt = d;

			Assert.AreEqual(d.Year, dt.Year);
			Assert.AreEqual(d.Month, dt.Month);
			Assert.AreEqual(d.Day, dt.Day);
			Assert.AreEqual(d.DayOfWeek, dt.DayOfWeek);
			Assert.AreEqual(d.DayOfYear, dt.DayOfYear);
			Assert.AreEqual(0, dt.Hour);
			Assert.AreEqual(0, dt.Minute);
			Assert.AreEqual(0, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		[Test]
		public void CanExplicitCastToDateTime()
		{
			DateTime dt = new DateTime(2000, 1, 2, 3, 4, 5);
			Date d = (Date)dt;

			Assert.AreEqual(dt.Year, d.Year);
			Assert.AreEqual(dt.Month, d.Month);
			Assert.AreEqual(dt.Day, d.Day);
		}

		[Test]
		public void CanToShortString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToShortString();

				StringAssert.Contains("2013", s);
				StringAssert.Contains("4", s);
				StringAssert.Contains("5", s);
			}
		}

		[Test]
		public void CanToLongString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToLongString();

				StringAssert.Contains("2013", s);
				StringAssert.Contains("April", s);
				StringAssert.Contains("5", s);
			}
		}

		[Test]
		public void CanToString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToShortString();

				StringAssert.Contains("2013", s);
				StringAssert.Contains("4", s);
				StringAssert.Contains("5", s);
			}
		}

		[Test]
		public void CanParse()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = Date.Parse("2013-04-05");

				Assert.AreEqual(2013, d.Year);
				Assert.AreEqual(4, d.Month);
				Assert.AreEqual(5, d.Day);
				Assert.AreEqual(DayOfWeek.Friday, d.DayOfWeek);
				Assert.AreEqual(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Test]
		public void CanParseWithTime()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = Date.Parse("2013-04-05 6:07:08 PM");

				Assert.AreEqual(2013, d.Year);
				Assert.AreEqual(4, d.Month);
				Assert.AreEqual(5, d.Day);
				Assert.AreEqual(DayOfWeek.Friday, d.DayOfWeek);
				Assert.AreEqual(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void CantParseInvalidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = Date.Parse("abc");
			}
		}

		[Test]
		public void CanTryParseValidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d;
				bool successs = Date.TryParse("2013-04-05", out d);

				Assert.IsTrue(successs);
				Assert.AreEqual(2013, d.Year);
				Assert.AreEqual(4, d.Month);
				Assert.AreEqual(5, d.Day);
				Assert.AreEqual(DayOfWeek.Friday, d.DayOfWeek);
				Assert.AreEqual(31 + 28 + 31 + 5, d.DayOfYear);
			}
		}

		[Test]
		public void CanTryParseInalidString()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d;
				bool success = Date.TryParse("abc", out d);

				Assert.IsFalse(success);
			}
		}

		[Test]
		public void CanPresentRoundTripPattern()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s1 = d.ToString("O");
				string s2 = d.ToString("o");

				Assert.AreEqual(s1, s2);
				Assert.AreEqual("2013-04-05", s1);
				Assert.AreEqual("2013-04-05", s2);
			}
		}

		[Test]
		public void CanPresentSortablePattern()
		{
			using (new CultureScope(CultureInfo.InvariantCulture))
			{
				Date d = new Date(2013, 4, 5);
				string s = d.ToString("s");

				Assert.AreEqual("2013-04-05", s);
			}
		}
	}
}
