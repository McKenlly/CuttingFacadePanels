using System;
using System.Collections.Generic;
using System.Linq;

namespace CuttingFacadePanels
{
	public class Polygon
	{
		/// <summary>
		/// Точки полигона с учетом того что они расположены последовательно
		/// </summary>
		public List<Point> Points { get; private set; }
		public double MaxWeigth { get; }
		public double MaxHeight { get; }
		public double XMax { get; }
		public double XMin { get; }

		public int CountPoints => Points.Count;

		public Polygon(IEnumerable<Point> points)
		{
			Points = points?.ToList() ?? throw new NullReferenceException();
			if (!points.Any()) throw new NullReferenceException();
			XMax = Points.MaxBy(x => x.X).X;
			XMin = Points.MinBy(x => x.X).X;
			MaxWeigth = XMax - XMin;
			MaxHeight = Points.MaxBy(x => x.Y).Y - Points.MinBy(x => x.Y).Y;
		}
	}
}