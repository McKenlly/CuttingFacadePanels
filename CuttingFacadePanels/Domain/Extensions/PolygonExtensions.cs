using System;

namespace CuttingFacadePanels
{
	public static class PolygonExtensions
	{
		public static double GetSquare(this Polygon polygon)
		{
			double res = 0;
			var point = polygon.Points[0];
			for (int i = 0; i < polygon.CountPoints - 1; i++)
			{
				res += 0.5 * Math.Abs((polygon.Points[i].X + polygon.Points[i + 1].X) *
				                      (polygon.Points[i].Y - polygon.Points[i + 1].Y));
			}
			return res;
		}
	}
}