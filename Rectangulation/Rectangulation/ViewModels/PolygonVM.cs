using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Rectangulation
{
    class PolygonVM : BaseShapeVM
    {
        /// <summary>
        /// Полигон
        /// </summary>
        public Polygon Polygon { get; }

        /// <summary>
        /// Свойство кисть заливки
        /// </summary>
        public override Brush Fill => Polygon.Fill;

        /// <summary>
        /// Свойство кисть контура
        /// </summary>
        public override Brush Stroke => Polygon.Stroke;

        /// <summary>
        /// Свойство толщина линии
        /// </summary>
        public override double StrokeThickness => Polygon.StrokeThickness;

        /// <summary>
        /// Конструктор полигона
        /// </summary>
        /// <param name="x">Координата X стартовой точки</param>
        /// <param name="y">Координата Y стартовой точки</param>
        public PolygonVM(double x, double y)
        {
            Polygon = new Polygon();
            Polygon.AddPoint(x, y);

            var pathGeom = new PathGeometry();
            var segment = new PolyLineSegment {Points = new PointCollection()};
            var figure = new PathFigure {StartPoint = new Point(x, y)};
            figure.Segments.Add(segment);
            pathGeom.Figures.Add(figure);
            Geometry = pathGeom;
        }

        /// <summary>
        /// Метод добавления точки в полигон
        /// </summary>
        /// <param name="x">Координата X точки</param>
        /// <param name="y">Координата Y точки</param>
        public void AddPoint(double x, double y)
        {
            Polygon.AddPoint(x, y);
            var figure = GetFigure();
            if (figure.Segments.Count <= 0)
                return;
            var segment = (PolyLineSegment)figure.Segments[0];
            segment.Points.Add(new Point(x, y));
        }

        /// <summary>
        /// Метод замыкания полигона
        /// </summary>
        public void Close()
        {
            var figure = GetFigure();
            if (figure.Segments.Count <= 0)
                return;
            var segment = (PolyLineSegment)figure.Segments[0];
            segment?.Points.Add(figure.StartPoint);
        }

        /// <summary>
        /// Метод получения фигуры
        /// </summary>
        /// <returns>ссылку на фигуру</returns>
        private PathFigure GetFigure()
        {
            if (Geometry == null)
                return null;
            var pathGeom = (PathGeometry)Geometry;
            if (pathGeom.Figures.Count <= 0)
                return null;
            return (PathFigure)pathGeom.Figures[0];
        }
    }
}
