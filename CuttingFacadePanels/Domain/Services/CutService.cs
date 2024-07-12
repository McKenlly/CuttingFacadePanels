using System;
using System.Collections.Generic;
using System.Linq;

namespace CuttingFacadePanels
{
	public class CutService : ICutService
	{
		private readonly ICalculateAmountPanelsService _calculateAmountPanelsService;

		public CutService(ICalculateAmountPanelsService calculateAmountPanelsService)
		{
			_calculateAmountPanelsService = calculateAmountPanelsService;
		}

		public IEnumerable<Line> Cut(Polygon polygon)
		{
			var n = _calculateAmountPanelsService.Calculate(polygon);

			//Если прямоугольник полностью покрывает многоугольник, то выводим максимальную высоту многоугольника
			if (n == 1)
			{
				yield break;
			}

			var dotArray = new List<Point>();
			var polygonIterator = new PolygonCircleIterator(polygon);
			var vectorIterator = new PolygonVectorIterator(polygonIterator);

			//Костыль, проматываем вершины в поисках стартовой, от которой будем отталкиваться. Она будет иметь минимальное значение X
			while (((Point)polygonIterator.CurrentItem()).X != polygon.XMin)
			{
				polygonIterator.Next();
			}

			var vector = (Vector)vectorIterator.Next();
			var iteratorByX = vector.Line.BeginPoint.X;

			//ищем пересечения панелей с многоугольником. Их будет 2n в любом случае даже, если одна панель будет частично закрывать многоугольник
			for (int i = 0; i < n * 2; i++)
			{
				//чтобы знать в какую сторону смещаться. право или влево
				iteratorByX += vector.IsPositiveRouteByX() ? Constants.WidthPanel : -Constants.WidthPanel;
					
				var panelVector = new Vector(new Line(new Point(iteratorByX, 0), new Point(iteratorByX, 1000000))); // вертикальный вектор
				var dot = vector.GetCommonPoint(panelVector);

				//меняем отрезок многоугольника, если вышли за текущий и отматываемся на шаг назад
				if (!dot.IsPointOnVectorExist(vector))
				{
					iteratorByX += vector.IsPositiveRouteByX() ? -Constants.WidthPanel : +Constants.WidthPanel;
					vector = (Vector) vectorIterator.Next();
					i--;
					continue;
				}

				dotArray.Add(dot);
			}

			//могут быть повторения, случай когда панели имеют покрытие по Х выходящее за границу многоугольника, сортируем по Х
			dotArray = dotArray.Distinct().SortByX();

			//содержатся верхние и нижние точки. Находим отрезки
			for (int i = 0; i < dotArray.Count; i++)
			{
				if (i == dotArray.Count - 1)
				{
					if (Math.Abs(dotArray[i - 1].X - dotArray[i].X) > Constants.Epsilon)
					{
						yield return new Line(dotArray[i], dotArray[i]);
					}
					yield break;
				}
				if (Math.Abs(dotArray[i].X - dotArray[i + 1].X) < Constants.Epsilon)
				{
					i++;
					yield return new Line(dotArray[i], dotArray[i+1]);
				}
				else
				{
					//конечная точка пересечения, которая совпадает с вершиной
					yield return new Line(dotArray[i], dotArray[i]);
				}

				//TODO: еще не конец, определить, входит ли вершина в прямоугольник, если не входит, то расширить значение до размера вершины
				// var y1 = Math.Min(dotArray[2*n-i-2].Y, dotArray[2*n-1-i].Y);
				// var y2 = Math.Max(dotArray[i].Y, dotArray[i+1].Y);;
				//т.к. вертикальные, то X переменная не нужна
				// var resultVector = new Vector(new Line(new Point(0, y1), new Point(0, y2)));
				// yield return resultVector.Length; 
			}
		}
	}
}