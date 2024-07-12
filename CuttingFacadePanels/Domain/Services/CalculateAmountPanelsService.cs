using System;

namespace CuttingFacadePanels
{
	public class CalculateAmountPanelsService : ICalculateAmountPanelsService
	{
		public int Calculate(Polygon polygon) => (int)Math.Ceiling(polygon.MaxWeigth / Constants.WidthPanel);
	}
}