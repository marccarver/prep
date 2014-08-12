using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prep.ranges
{
	public class Range<T> where T : IComparable<T>
	{
		public static IHaveStart<T> StartingFrom(T start)
		{
			var rangeBuilder = new RangeBuilder<T> { Start = start };
			return rangeBuilder;
		}

		public static IHaveEnd<T> EndingAt(T end)
		{
			var rangeBuilder = new RangeBuilder<T>();
			rangeBuilder.EndingAt(end);
			return rangeBuilder;
		}
	}


	public class RangeBuilder<T> : IContainValues<T>, IHaveStart<T>, IHaveEnd<T>, IHaveStartExclusive<T>, IHaveEndExclusive<T>
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

		public IHaveEnd<T> EndingAt(T value)
		{
			End = value;
			return this;
		}


		IHaveStartExclusive<T> IHaveStart<T>.Exclusive()
		{
			isExclusiveStart = true;
			return this;
		}

		IHaveEndExclusive<T> IHaveEnd<T>.Exclusive()
		{
			isExclusiveEnd = true;
			return this;
		}

	}

	public interface IHaveStart<T> : IContainValues<T> where T : IComparable<T>
	{
		IHaveEnd<T> EndingAt(T value);
		IHaveStartExclusive<T> Exclusive();

	}

	public interface IHaveStartExclusive<T> : IContainValues<T> where T : IComparable<T>
	{
		IHaveEnd<T> EndingAt(T value);
	}


	public interface IHaveEnd<T> : IContainValues<T> where T : IComparable<T>
	{
		IHaveEndExclusive<T> Exclusive();
	}

	public interface IHaveEndExclusive<T> : IContainValues<T> where T : IComparable<T>
	{
	}

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
