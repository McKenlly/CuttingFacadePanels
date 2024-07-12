using System;

namespace CuttingFacadePanels
{
	public record Vector
	{
		public double A { get; }
		public double B { get; }
		public double C { get; }
		
		public Line Line { get; }

		public Vector(Line line)
		{
			Line = line;
			A = line.EndPoint.X - line.BeginPoint.X;
			B = line.BeginPoint.Y - line.EndPoint.Y;
			C = - B *  line.EndPoint.X - A * line.EndPoint.Y;
		}

		public double Length => Math.Sqrt(A*A + B*B);
	}
}