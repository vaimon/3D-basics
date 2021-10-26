using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Dbasics
{
    /// <summary>
    /// Тип координатной прямой (для поворотов)
    /// </summary>
    public enum AxisType { X, Y, Z };
    
    public partial class Form1
    {
        public AxisType currentAxis;
        public AxisType currentMirrorAxis;
        public AxisType currentRollAxis;

        public bool isScaleModeWorldCenter = true; 
        private void buttonScale_Click(object sender, EventArgs e)
        {
            if (isScaleModeWorldCenter)
            {
                scale(ref currentShape, int.Parse(textScaleX.Text), int.Parse(textScaleY.Text), int.Parse(textScaleZ.Text));
                redraw();
            }
            else
            {
                // TODO: scale from shape center 
            }
            
        }
        private void buttonRotate_Click(object sender, EventArgs e)
        {
            rotate(ref currentShape, currentAxis, int.Parse(textAngle.Text));
            redraw();
        }

        private void buttonShift_Click(object sender, EventArgs e)
        {
            shift(ref currentShape, int.Parse(textShiftX.Text), int.Parse(textShiftY.Text), int.Parse(textShiftZ.Text));
            redraw();
        }

        /// <summary>
        /// Сдвинуть фигуру на заданные расстояния
        /// </summary>
        /// <param name="shape">Фигура</param>
        /// <param name="dx">Сдвиг по оси X</param>
        /// <param name="dy">Сдвиг по оси Y</param>
        /// <param name="dz">Сдвиг по оси Z</param>
        void shift(ref Shape shape, int dx, int dy, int dz)
        {
            Matrix shift = new Matrix(4, 4).fill(1, 0, 0, dx, 0, 1, 0, dy, 0, 0, 1, dz, 0, 0, 0, 1);
            shape.transformPoints((ref Point p) =>
            {
                var res = shift * new Matrix(4, 1).fill(p.X, p.Y, p.Z, 1);
                p = new Point((int)res[0, 0], (int)res[1, 0], (int)res[2, 0]);
            });
        }

        /// <summary>
        /// Растянуть фигуру на заданные коэффициенты
        /// </summary>
        /// <param name="shape">Фигура</param>
        /// <param name="cx">Растяжение по оси X</param>
        /// <param name="cy">Растяжение по оси Y</param>
        /// <param name="cz">Растяжение по оси Z</param>
        void scale(ref Shape shape, int cx, int cy, int cz)
        {
            Matrix scale = new Matrix(4, 4).fill(cx, 0, 0, 0, 0, cy, 0, 0, 0, 0, cz, 0, 0, 0, 0, 1);
            shape.transformPoints((ref Point p) =>
            {
                var res = scale * new Matrix(4, 1).fill(p.X, p.Y, p.Z, 1);
                p = new Point((int)res[0, 0], (int)res[1, 0], (int)res[2, 0]);
            });
        }

        /// <summary>
        /// Повернуть фигуру на заданный угол вокруг заданной оси
        /// </summary>
        /// <param name="shape">Фигура</param>
        /// <param name="type">Ось, вокруг которой поворачиваем</param>
        /// <param name="angle">Угол поворота в градусах</param>
        void rotate(ref Shape shape, AxisType type, int angle)
        {
            Matrix rotation = new Matrix(0,0);
            switch (type)
            {
                case AxisType.X:
                    rotation = new Matrix(4, 4).fill(1, 0, 0, 0, 0, Math.Cos(ShapeGetter.degreesToRadians(angle)), -Math.Sin(ShapeGetter.degreesToRadians(angle)), 0, 0, Math.Sin(ShapeGetter.degreesToRadians(angle)), Math.Cos(ShapeGetter.degreesToRadians(angle)), 0, 0, 0, 0, 1);
                    break;
                case AxisType.Y:
                    rotation = new Matrix(4, 4).fill(Math.Cos(ShapeGetter.degreesToRadians(angle)), 0, Math.Sin(ShapeGetter.degreesToRadians(angle)), 0, 0, 1 ,0 ,0, -Math.Sin(ShapeGetter.degreesToRadians(angle)), 0, Math.Cos(ShapeGetter.degreesToRadians(angle)), 0, 0, 0, 0, 1);
                    break;
                case AxisType.Z:
                    rotation = new Matrix(4, 4).fill(Math.Cos(ShapeGetter.degreesToRadians(angle)), -Math.Sin(ShapeGetter.degreesToRadians(angle)), 0, 0, Math.Sin(ShapeGetter.degreesToRadians(angle)), Math.Cos(ShapeGetter.degreesToRadians(angle)), 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                    break;
            }
           
            shape.transformPoints((ref Point p) =>
            {
                var res = rotation * new Matrix(4, 1).fill(p.X, p.Y, p.Z, 1);
                p = new Point((int)res[0, 0], (int)res[1, 0], (int)res[2, 0]);
            });
        }


        // Отражение относительно выбранной координатной плоскости
        void reflectionAboutTheAxis(ref Shape shape, AxisType axis)
        {
            Matrix reflectionMatrix;
            switch (axis)
            {
                case AxisType.X: // XY                 
                    reflectionMatrix = new Matrix(4, 4).fill(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1);
                    break;
                case AxisType.Y: // XZ
                    reflectionMatrix = new Matrix(4, 4).fill(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                    break;
                case AxisType.Z: // YZ                 
                    reflectionMatrix = new Matrix(4, 4).fill(-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                    break;
                default:
                    throw new Exception("Зеркальные оси всё сломали :(");
            }

            shape.transformPoints((ref Point p) =>
            {
                var res = reflectionMatrix * new Matrix(4, 1).fill(p.X, p.Y, p.Z, 1);
                p = new Point((int)res[0, 0], (int)res[1, 0], (int)res[2, 0]);
            });
        }
    }
}
