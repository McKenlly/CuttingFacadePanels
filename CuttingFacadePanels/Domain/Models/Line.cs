using System;

namespace CuttingFacadePanels
{
	public class Line
	{
		public Point BeginPoint { get; }
		public Point EndPoint { get; }

		public Line(Point beginPoint, Point endPoint)
		{
			BeginPoint = beginPoint;
			EndPoint = endPoint;
		}

		public double Length => Math.Sqrt(Math.Pow(BeginPoint.X-EndPoint.X, 2) + Math.Pow(BeginPoint.Y-EndPoint.Y, 2));
	}
}