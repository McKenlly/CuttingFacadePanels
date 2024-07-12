using System.Collections.Generic;

namespace CuttingFacadePanels
{
	public interface ICutService
	{
		/// <summary>
		/// Находим отрезки разрезов многоугольника
		/// </summary>
		public IEnumerable<Line> Cut(Polygon polygon);
	}
}