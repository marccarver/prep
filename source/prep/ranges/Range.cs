using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prep.ranges
{
	public class Range<T> where T : IComparable<T>
	{
		public static IHaveStartInclusive<T> StartingFrom(T start)
		{
			var rangeBuilder = new RangeBuilder<T> { Start = start };
			return rangeBuilder;
		}

		public static IHaveEndInclusive<T> EndingAt(T end)
		{
			var rangeBuilder = new RangeBuilder<T>();
			rangeBuilder.EndingAt(end);
			return rangeBuilder;
		}
	}


	public class RangeBuilder<T> : IHaveStartInclusive<T>, IHaveEndInclusive<T>, IHaveStart<T>, IHaveEnd<T>
		where T : IComparable<T>
	{
		public T Start { get; set; }
		public T End { get; set; }

		private bool isExclusiveStart;
		private bool isExclusiveEnd;

		public bool contains(T value)
		{
			// TODO use isExclusive
			return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
		}

		public IHaveEndInclusive<T> EndingAt(T value)
		{
			End = value;
			return this;
		}


		IHaveStart<T> IHaveStartInclusive<T>.Exclusive()
		{
			isExclusiveStart = true;
			return this;
		}

		IHaveEnd<T> IHaveEndInclusive<T>.Exclusive()
		{
			isExclusiveEnd = true;
			return this;
		}

	}

	public interface IHaveStartInclusive<T> : IHaveStart<T>, IContainValues<T> where T : IComparable<T>
	{
		IHaveStart<T> Exclusive();
	}

	public interface IHaveStart<T> : IContainValues<T> where T : IComparable<T>
	{
		IHaveEndInclusive<T> EndingAt(T value);
	}


	public interface IHaveEndInclusive<T> : IHaveEnd<T>, IContainValues<T> where T : IComparable<T>
	{
		IHaveEnd<T> Exclusive();
	}

	public interface IHaveEnd<T> : IContainValues<T> where T : IComparable<T>
	{
	}

	
	// Test
	public class UseIt
	{
		public static void use()
		{

			var result = Range<int>.StartingFrom(1).EndingAt(7).Exclusive().contains(5);

			Range<int>.StartingFrom(1).Exclusive().contains(5);

			Range<int>.EndingAt(4).contains(5);
		}
	}
}
