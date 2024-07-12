using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CuttingFacadePanels
{
	public class GetLengthPanelsQuery : PolygonPointsQuery, IRequest<GetLengthPanelsResponse>
	{
	}
}