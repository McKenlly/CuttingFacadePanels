using System;
using System.Collections.Generic;
using System.Linq;

namespace CuttingFacadePanels
{
	public static class PointExtensions
	{
		/// <summary>
		/// Если точка принадлежит отрезку то сумма длинн отрезков будет равна
		/// </summary>
		public static bool IsPointOnVectorExist(this Point point, Vector vector)
		{
			var vector1 = new Vector(new Line(point, vector.Line.BeginPoint));
			var vector2 = new Vector(new Line(point, vector.Line.EndPoint));
			return Math.Abs(vector1.Length + vector2.Length - vector.Length) < Constants.Epsilon;
		}

		/// <summary>
		/// Сортировка массива точек по Х
		/// </summary>
		public static List<Point> SortByX(this IEnumerable<Point> arr)
		{
			return arr.OrderBy(a => a.X).ToList();
		}
	}
}