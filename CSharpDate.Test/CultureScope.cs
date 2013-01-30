using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace CSharpDate.Test
{
	public class CultureScope : IDisposable
	{
		private CultureInfo _before;

		public CultureScope(CultureInfo culture)
		{
			this._before = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = culture;
		}

		public void Dispose()
		{
			Thread.CurrentThread.CurrentCulture = _before;
		}
	}
}
