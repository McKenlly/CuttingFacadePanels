namespace CuttingFacadePanels
{
	public class PolygonCircleIterator : IIterator
	{
		private readonly Polygon _polygon;
		private int _current;

		public PolygonCircleIterator(Polygon polygon)
		{
			_polygon = polygon;
		}

		public object First()
		{
			return _polygon.Points[0];
		}

		/// <summary>
		/// Цикличный обход точек, так как первая точка совпадает с конечной
		/// </summary>
		/// <returns></returns>
		public object Next()
		{
			_current++;
  
			if (_current < _polygon.CountPoints)
			{
				return _polygon.Points[_current];
			}
  
			return _polygon.Points[0];
		}

		public bool IsDone()
		{
			return _current >= _polygon.CountPoints;
		}

		public object CurrentItem()
		{
			if (IsDone()) _current = 0;
			return _polygon.Points[_current];
		}
	}
}