﻿using System;
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
        private Polygon _polygon = new Polygon();

        /// <summary>
        /// Свойство кисть заливки
        /// </summary>
        public override Brush Fill => _polygon.Fill;

        /// <summary>
        /// Свойство кисть контура
        /// </summary>
        public override Brush Stroke => _polygon.Stroke;

        /// <summary>
        /// Свойство толщина линии
        /// </summary>
        public override double StrokeThickness => _polygon.StrokeThickness;

        /// <summary>
        /// Конструктор полигона
        /// </summary>
        /// <param name="x">Координата X стартовой точки</param>
        /// <param name="y">Координата Y стартовой точки</param>
        public PolygonVM(double x, double y)
        {
            _polygon.AddPoint(x, y);

            PathGeometry pathGeom = new PathGeometry();
            PolyLineSegment segment = new PolyLineSegment();
            segment.Points = new PointCollection();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(x, y);
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
            _polygon.AddPoint(x, y);
            if (Geometry != null)
            {
                PathGeometry pathGeom = (PathGeometry)Geometry;
                if (pathGeom.Figures.Count > 0)
                {
                    PathFigure figure = (PathFigure)pathGeom.Figures[0];
                    if (figure.Segments.Count > 0)
                    {
                        PolyLineSegment segment = (PolyLineSegment)figure.Segments[0];
                        segment.Points.Add(new Point(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Метод замыкания полигона
        /// </summary>
        public void Close()
        {
            if (Geometry != null)
            {
                PathGeometry pathGeom = (PathGeometry)Geometry;
                if (pathGeom.Figures.Count > 0)
                {
                    PathFigure figure = (PathFigure)pathGeom.Figures[0];
                    if (figure.Segments.Count > 0)
                    {
                        PolyLineSegment segment = (PolyLineSegment)figure.Segments[0];
                        segment.Points.Add(figure.StartPoint);
                    }

                }
            }
        }
    }
}
