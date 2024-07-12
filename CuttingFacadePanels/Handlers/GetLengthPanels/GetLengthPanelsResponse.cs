using System.Collections.Generic;
using System.Linq;

namespace CuttingFacadePanels
{
	public class GetLengthPanelsResponse : PolygonResponse
	{
		public IEnumerable<double> PanelsLength { get; set; }
		public int AmountPanels { get; set; }
		public int Count => PanelsLength?.Count() ?? 0;
		public double SquareSum => PanelsLength?.Sum() ?? 0;
	}
}