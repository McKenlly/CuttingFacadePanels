using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingFacadePanels.GetAmountScraps;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuttingFacadePanels
{
	[Route("api/polygon")]
	[ApiController]
	[Produces("application/json")]
	public class PolygonController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PolygonController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("get-panels")]
		[ProducesResponseType(typeof(GetLengthPanelsResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetLengthPanels([FromQuery(Name = "query")] string queryJson)
		{
			try
			{
				var points = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Point>>(queryJson);
				if (points == null || !points.Any())
					return BadRequest();

				var response = await _mediator.Send(new GetLengthPanelsQuery() { Points = points});
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet("get-square")]
		[ProducesResponseType(typeof(GetSquareResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetSquare([FromQuery(Name = "query")] string queryJson)
		{
			//Прибиндить кверю к модельке
			try
			{
				var points = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Point>>(queryJson);
				if (points == null || !points.Any())
					return BadRequest();

				var response = await _mediator.Send(new GetSquareQuery() { Points = points});
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet("get-amount-scraps")]
		[ProducesResponseType(typeof(GetSquareResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAmountOfScraps([FromQuery(Name = "query")] string queryJson)
		{
			try
			{
				var points = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Point>>(queryJson);
				if (points == null || !points.Any())
					return BadRequest();

				var response = await _mediator.Send(new GetAmountScrapsQuery() { Points = points});
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}	
}