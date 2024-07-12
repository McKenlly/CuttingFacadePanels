using FluentValidation;

namespace CuttingFacadePanels.Validators
{
	public class PolygonValidator : AbstractValidator<Polygon>
	{
		public PolygonValidator()
		{
			RuleFor(x => x.Points)
				.NotNull()
				.NotEmpty();
			//не менее трех точек
			RuleFor(x => x.CountPoints)
				.GreaterThan(2);
			//высота не должна превышать константы
			RuleForEach(x => x.Points)
				.Where(x => x.Y <= Constants.HeightPanel);
		}
	}
}