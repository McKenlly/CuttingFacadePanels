using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CuttingFacadePanels
{
	public static class VectorExtensions
	{
		/// <summary>
		/// пересечение двух векторов
		/// </summary>
		public static Point GetCommonPoint(this Vector vector1, Vector vector2)
		{
			var y = -(vector1.C * vector2.B - vector1.B * vector2.C) / (vector1.A * vector2.B - vector2.A * vector1.B);
			var x = -(vector2.C * vector1.A - vector2.A * vector1.C) / (vector1.A * vector2.B - vector2.A * vector1.B);
			return new Point(x, y);
		}

		/// <summary>
		/// Если матрица коэффициэнтов равна нулю, то векторы параллельны друг другу
		/// </summary>
		public static bool IsCollinear(this Vector vector1, Vector vector2)
		{
			return vector1.A*vector2.B - vector1.B*vector2.A == 0;
		}

		public static bool IsPositiveRouteByX(this Vector vector)
		{
			return vector.A > 0;
		}

		public static bool IsPositiveRouteByY(this Vector vector)
		{
			return vector.B > 0;
		}
	}
}