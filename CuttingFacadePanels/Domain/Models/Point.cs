using System;

namespace CuttingFacadePanels
{
	public class Point
	{
		public double X { get; }
		public double Y { get; }

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public bool Equals(Point p)
		{
			if (p == null) return false;
			return Math.Abs(p.X - this.X) < Constants.Epsilon && Math.Abs(p.Y - this.Y) < Constants.Epsilon;
		}

	}
}