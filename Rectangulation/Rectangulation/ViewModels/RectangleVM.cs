using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Rectangulation
{
    public class RectangleVM : BaseShapeVM
    {
        /// <summary>
        /// Прямоугольник
        /// </summary>
        private Rectangle Rectangle { get; }

        /// <summary>
        /// Имя прямоугольника в списке
        /// </summary>
        public string Label => "Прямоугольник " + Rectangle.Id.ToString();

        /// <summary>
        /// Свойство, использующиеся для выбора в списке
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Свойство кисть заливки
        /// </summary>
        public override Brush Fill => Rectangle.Fill;

        /// <summary>
        /// Свойство кисть контура
        /// </summary>
        public override Brush Stroke => Rectangle.Stroke;

        /// <summary>
        /// Свойство толщина линии
        /// </summary>
        public override double StrokeThickness => 1;

        /// <summary>
        /// Свойство, использующиеся для выделения цветом прямоугольника
        /// </summary>
        private bool _selected;
        
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                if (_selected)
                {
                    Rectangle.Fill = Brushes.Transparent;
                    Rectangle.Stroke = Brushes.Red;
                }
                else
                {
                    Rectangle.Fill = Brushes.Black;
                    Rectangle.Stroke = Brushes.Black;
                }
                OnPropertyChanged("Fill");
                OnPropertyChanged("Stroke");
            }
        }

        /// <summary>
        /// Конструктор RectangleVM
        /// </summary>
        /// <param name="x">Координата X левого правого угла прямоугольника</param>
        /// <param name="y">Координата Y левого правого угла прямоугольника</param>
        public RectangleVM(Rectangle rectangle)
        {
            Rectangle = rectangle;
            Geometry = new RectangleGeometry(Rectangle.Rect);
        }

        /// <summary>
        /// Метод попадания в границы прямоугольника
        /// </summary>
        /// <param name="x">Координата X нажатия левой кнопки мыши</param>
        /// <param name="y">Координата Y нажатия левой кнопки мыши</param>
        /// <returns>Возвращает true, если клик рядом с границей</returns>
        public bool HitToBorder(double x, double y)
        {
            return Rectangle.HitToBorder(x, y);
        }

        public void Move(double x, double y)
        {
            Rectangle.Y = y;
            Rectangle.X = x;
            Geometry = new RectangleGeometry(Rectangle.Rect);
        }
    }
}
