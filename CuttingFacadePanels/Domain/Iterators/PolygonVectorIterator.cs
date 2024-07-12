using System;

namespace CuttingFacadePanels
{
	public class PolygonVectorIterator : IIterator
	{
		private readonly PolygonCircleIterator _iterator;
		private int _current;

		public PolygonVectorIterator(PolygonCircleIterator iterator)
		{
			_iterator = iterator;
		}

		public object First()
		{
			throw new NotImplementedException();
		}

		public object Next()
		{
			var pointOne = (Point) _iterator.CurrentItem();
			var pointTwo = (Point) _iterator.Next();
			return new Vector(new Line(pointOne, pointTwo));
		}

		public bool IsDone()
		{
			throw new NotImplementedException();
		}

		public object CurrentItem()
		{
			throw new NotImplementedException();
		}
	}
}