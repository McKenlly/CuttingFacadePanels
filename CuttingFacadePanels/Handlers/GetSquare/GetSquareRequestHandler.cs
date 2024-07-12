using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace CuttingFacadePanels
{
	public class GetSquareRequestHandler : IRequestHandler<GetSquareQuery, GetSquareResponse>
	{
		private readonly IValidator<Polygon> _validator;

		public GetSquareRequestHandler(IValidator<Polygon> validator)
		{
			_validator = validator;
		}

		public Task<GetSquareResponse> Handle(GetSquareQuery query, CancellationToken cancellationToken)
		{
			var polygon = new Polygon(query.Points);
			if (!_validator.Validate(polygon).IsValid)
			{
				return Task.FromResult(new GetSquareResponse() { IsSuccess = false });
				
			}
			return Task.FromResult(new GetSquareResponse() { Square = polygon.GetSquare(), IsSuccess = true});
		}
	}
}