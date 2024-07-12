using MediatR;

namespace CuttingFacadePanels
{
	public class GetSquareQuery : PolygonPointsQuery, IRequest<GetSquareResponse>
	{
	}
}