using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Rectangulation.Commands
{
    class LeftClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            var canvas = (Canvas)values[0];
            var vmodel = (MainWindowVM)values[1];
            var mousePos = Mouse.GetPosition(canvas);
            if ((vmodel.CurrentPolygon != null) || (!TrySelectRectangle(mousePos.X, mousePos.Y, vmodel)))
                AddPointToPolygon(mousePos.X, mousePos.Y, vmodel);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Метод добавления точки в полигон
        /// </summary>
        /// <param name="x">Координата X точки</param>
        /// <param name="y">Координата Y точки</param>
        private void AddPointToPolygon(double x, double y, MainWindowVM vmodel)
        {
            if (vmodel.CurrentPolygon == null)
            {
                vmodel.CurrentPolygon = new PolygonVM(x, y);
                vmodel.Polygons.Add(vmodel.CurrentPolygon);
            }
            else
                vmodel.CurrentPolygon.AddPoint(x, y);
            vmodel.OnPropertyChanged();
            vmodel.SelectedRectangle = null;
        }

        /// <summary>
        /// Метод выделения прямоугольника
        /// </summary>
        /// <param name="x">Координата X нажатия левой кнопки мыши</param>
        /// <param name="y">Координата Y нажатия левой кнопки мыши</param>
        /// <param name="vmodel">Экземляр MainWindowVM</param>
        private bool TrySelectRectangle(double x, double y, MainWindowVM vmodel)
        {
            foreach (var rectangle in vmodel.Rectangles)
            {
                if (rectangle.HitToBorder(x, y))
                {
                    vmodel.SelectedRectangle = null;
                    vmodel.SelectedRectangle = rectangle;
                    return true;
                }
            }
            return false;
        }

    }
}
