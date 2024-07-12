using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace CuttingFacadePanels
{
	public class GetLengthPanelsRequestHandler : IRequestHandler<GetLengthPanelsQuery, GetLengthPanelsResponse>
	{
		private readonly ICutService _cutService;
		private readonly ICalculateAmountPanelsService _calculateAmountPanelsService;
		private readonly IValidator<Polygon> _validator;

		public GetLengthPanelsRequestHandler(
			ICutService cutService,
			ICalculateAmountPanelsService calculateAmountPanelsService,
			IValidator<Polygon> validator)
		{
			_cutService = cutService;
			_calculateAmountPanelsService = calculateAmountPanelsService;
			_validator = validator;
		}

		public Task<GetLengthPanelsResponse> Handle(GetLengthPanelsQuery query, CancellationToken cancellationToken)
		{
			var polygon = new Polygon(query.Points);
			if (!_validator.Validate(polygon).IsValid)
				return Task.FromResult(new GetLengthPanelsResponse() { IsSuccess = false });

			//получили список отрезков в последовательном порядке, от начала до конца
			var lines = _cutService.Cut(polygon).ToList();
			var result = new List<Line>();
			double maxX, maxY, minX, minY;
			// ищем вершины в промежутках
			for (var i = 0; i < lines.Count; i++)
			{
				// то есть панель покрывает по оси х многоугольник больше ожидаемого
				if (i == 0 && polygon.Points.Any(x => x.X < lines[i].BeginPoint.X))
				{
					var beginVertex = polygon.Points.Where(x => x.X < lines[i].BeginPoint.X).MaxBy(x => x.Y);

					minY = Math.Min(lines[i].EndPoint.Y, beginVertex.Y);
					maxY = Math.Max(lines[i].BeginPoint.Y, beginVertex.Y);
					maxX = lines[i].EndPoint.X;
					minX = beginVertex.X;
					result.Add(new Line(new Point(minX, minY), new Point(maxX, maxY)));

					continue;
				}
				// то есть панель покрывает по оси х многоугольник больше ожидаемого
				if (i == lines.Count - 1 && polygon.Points.Any(x => x.X > lines[i].BeginPoint.X))
				{
					var endVertex = polygon.Points.Where(x => x.X > lines[i].BeginPoint.X).MaxBy(x => x.Y);

					minY = Math.Min(lines[i].EndPoint.Y, endVertex.Y);
					maxY = Math.Max(lines[i].BeginPoint.Y, endVertex.Y);
					maxX = endVertex.X;
					minX = lines[i].EndPoint.X; // без разницы, вертикальный отрезок
					result.Add(new Line(new Point(minX, minY), new Point(maxX, maxY)));

					break;
				}

				if (i == lines.Count - 1) break;

				var line1 = lines[i];
				var line2 = lines[i + 1];
				//endPoint это нижние точки у панелей, то есть нужны минимумы
				var polygonPoints = polygon.Points.Where(x => x.X > line1.BeginPoint.X && x.X < line2.BeginPoint.X);
				// if (polygonPoints.Any())
				//Можно было обойтись поиском персечения между точкой и прямой
				minY = Math.Min(Math.Min(polygonPoints.MinBy(x => x.Y)?.Y ?? line1.EndPoint.Y, line1.EndPoint.Y), line2.EndPoint.Y);
				maxY = Math.Max(Math.Max(polygonPoints.MaxBy(x => x.Y)?.Y ?? line1.BeginPoint.Y, line1.BeginPoint.Y), line2.BeginPoint.Y);
				maxX = line2.BeginPoint.X;
				minX = line1.EndPoint.X;
				result.Add(new Line(new Point(minX, minY), new Point(maxX, maxY)));
				
			}
			return Task.FromResult(new GetLengthPanelsResponse()
			{
				PanelsLength = result.Select(x => x.Length),
				AmountPanels = _calculateAmountPanelsService.Calculate(polygon),
				IsSuccess = true
			});
		}
	}
}