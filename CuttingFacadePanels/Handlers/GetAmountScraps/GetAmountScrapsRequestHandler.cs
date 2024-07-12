using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CuttingFacadePanels.GetAmountScraps
{
	public class GetAmountScrapsRequestHandler : IRequestHandler<GetAmountScrapsQuery, GetAmountScrapsResponse>
	{
		private readonly IMediator _mediator;

		public GetAmountScrapsRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<GetAmountScrapsResponse> Handle(GetAmountScrapsQuery request, CancellationToken cancellationToken)
		{
			var squarePolygonResult = await _mediator.Send(new GetSquareQuery() { Points = request.Points }, cancellationToken); 
			var lengthPanelsResult = await _mediator.Send(new GetLengthPanelsQuery() { Points = request.Points }, cancellationToken);

			if (squarePolygonResult.IsSuccess && lengthPanelsResult.IsSuccess)
				return new GetAmountScrapsResponse() { Sum = lengthPanelsResult.SquareSum - squarePolygonResult.Square, IsSuccess = true };

			return new GetAmountScrapsResponse() { IsSuccess = false };
		}
	}
}