using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Dbasics
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        bool isInteractiveMode = false;
        int shiftx=0;
        int shifty=0;
        int shiftz=0;

        public Form1()
        {
            InitializeComponent();
            selectShape.SelectedIndex = 0;
            selectAxis.SelectedIndex = 0;
            selectMirrorAxis.SelectedIndex = 0;
            selectRollAxis.SelectedIndex = 0;
            g = canvas.CreateGraphics();

            // Здесь мы задаём Декартову систему координат на канвасе
            g.ScaleTransform(1.0F, -1.0F);
            g.TranslateTransform(0.0F, -(float)canvas.Height);

            // А здесь задаём точку начала координат
            Point.worldCenter = new PointF(canvas.Width / 2, canvas.Height / 2);
            setFlags();
        }
        void setFlags(bool interactiveMode = false)
        {
            isInteractiveMode = interactiveMode;
            selectAxis.Enabled = interactiveMode;
            buttonRotate.Enabled = interactiveMode;
            buttonScale.Enabled = interactiveMode;
            buttonShift.Enabled = interactiveMode;
            rbAxonometric.Enabled = interactiveMode;
            rbPerspective.Enabled = interactiveMode;
            rbIsometric.Enabled = interactiveMode;
            btnShowAxis.Enabled = interactiveMode;
            textAngle.Enabled = interactiveMode;
            textScaleX.Enabled = interactiveMode;
            textScaleY.Enabled = interactiveMode;
            textScaleZ.Enabled = interactiveMode;
            textShiftX.Enabled = interactiveMode;
            textShiftY.Enabled = interactiveMode;
            textShiftZ.Enabled = interactiveMode;
            rbAxonometric.Enabled = interactiveMode;
            selectMirrorAxis.Enabled = interactiveMode;
            rbWorldCenter.Enabled = interactiveMode;
            rbCenter.Enabled = interactiveMode;
            buttonMirror.Enabled = interactiveMode;
            buttonRoll.Enabled = interactiveMode;
            selectRollAxis.Enabled = interactiveMode;
            buttonRotateAroundLine.Enabled = interactiveMode;
            textX1.Enabled = interactiveMode;
            textX2.Enabled = interactiveMode;
            textY1.Enabled = interactiveMode;
            textY2.Enabled = interactiveMode;
            textZ1.Enabled = interactiveMode;
            textZ2.Enabled = interactiveMode;
            textAngleForLineRotation.Enabled = interactiveMode;
            textBoxAngleRotCenter.Enabled = interactiveMode;

            buttonShape.Text = interactiveMode ? "Очистить" : "Нарисовать";
            selectShape.Enabled = !interactiveMode;
        }

        private void comboBoxShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectShape.SelectedIndex)
            {
                case 0: currentShapeType = ShapeType.TETRAHEDRON; break;
                case 1: currentShapeType = ShapeType.HEXAHEDRON; break;
                case 2: currentShapeType = ShapeType.OCTAHEDRON; break;
                case 3: currentShapeType = ShapeType.ICOSAHEDRON; break;
                case 4: currentShapeType = ShapeType.DODECAHEDRON; break;
                default: throw new Exception("Фигурки всё сломали :(");
            }
        }

        private void selectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectAxis.SelectedIndex)
            {
                case 0: currentAxis = AxisType.X; break;
                case 1: currentAxis = AxisType.Y; break;
                case 2: currentAxis = AxisType.Z; break;
                default: throw new Exception("Оси всё сломали :(");
            }
        }

        private void rbPerspective_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPerspective.Checked)
            {
                Point.projection = ProjectionType.PERSPECTIVE;
                redraw();
            }
        }

        private void rbIsometric_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIsometric.Checked)
            {
                Point.projection = ProjectionType.ISOMETRIC;
                redraw();
            }
        }

        private void rbAxonometric_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAxonometric.Checked)
            {
                Point.projection = ProjectionType.TRIMETRIC;
                redraw();
            }
        }

        private void textScaleX_TextChanged(object sender, EventArgs e)
        {
            if(textScaleX.Text == "")
            {
                textScaleX.Text = "1";
            }
        }

        private void textScaleY_TextChanged(object sender, EventArgs e)
        {
            if (textScaleY.Text == "")
            {
                textScaleY.Text = "1";
            }
        }

        private void textScaleZ_TextChanged(object sender, EventArgs e)
        {
            if (textScaleZ.Text == "")
            {
                textScaleZ.Text = "1";
            }
        }

        private void textShiftX_TextChanged(object sender, EventArgs e)
        {
            if (textShiftX.Text == "")
            {
                textShiftX.Text = "0";
            }
        }

        private void textShiftY_TextChanged(object sender, EventArgs e)
        {
            if (textShiftY.Text == "")
            {
                textShiftY.Text = "0";
            }
        }

        private void textShiftZ_TextChanged(object sender, EventArgs e)
        {
            if (textShiftZ.Text == "")
            {
                textShiftZ.Text = "0";
            }
        }
        
        private void rbWorldCenter_CheckedChanged(object sender, EventArgs e)
        {
            isScaleModeWorldCenter = rbWorldCenter.Checked;
        }

        private void buttonMirror_Click(object sender, EventArgs e)
        {
            reflectionAboutTheAxis(ref currentShape, currentMirrorAxis);
            redraw();
        }

        private void selectMirrorAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectMirrorAxis.SelectedIndex)
            {
                case 0: currentMirrorAxis = AxisType.X; break;
                case 1: currentMirrorAxis = AxisType.Y; break;
                case 2: currentMirrorAxis = AxisType.Z; break;
                default: throw new Exception("Зеркальные оси всё сломали :(");
            }
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            rotationThroughTheCenter(ref currentShape, currentRollAxis, int.Parse(textBoxAngleRotCenter.Text));
            redraw();
        }

        private void selectRollAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectRollAxis.SelectedIndex)
            {
                case 0: currentRollAxis = AxisType.X; break;
                case 1: currentRollAxis = AxisType.Y; break;
                case 2: currentRollAxis = AxisType.Z; break;
                default: throw new Exception("Вращающиеся оси всё сломали :(");
            }
        }
        
        private void buttonRotateAroundLine_Click(object sender, EventArgs e)
        {
            // TODO
            int angle = int.Parse(textAngleForLineRotation.Text);
            Point p1 = new Point(int.Parse(textX1.Text), int.Parse(textY1.Text), int.Parse(textZ1.Text));
            Point p2 = new Point(int.Parse(textX2.Text), int.Parse(textY2.Text), int.Parse(textZ2.Text));
            if (p1.Z ==0 && p1.X==0&&p1.Y==0&& (p2.Z != 0 ||p2.Y ==0 || p2.X==0) )
              
            {
                Point tmp = p1;
                p1 = p2;
                p2 = tmp;
            }
            if (p2.Z == 0 && p2.X == 0 && p2.Y == 0 && (p1.Z != 0 || p1.Y == 0 || p1.X == 0))

            {
                Point tmp = p1;
                p1 = p2;
                p2 = tmp;
            }

            rotate_around_line(ref currentShape, angle,p1,p2);
            int A = p1.Y - p2.Y;//общее уравнение прямой, проходящей через заданные точки
            int B = p2.X - p1.X;//вектор нормали 
            int C = p1.X * p2.Y - p2.X *p1.Y;
            Point p3 = new Point(p2.X - p1.X,  p2.Y- p1.Y,  p2.Z - p1.Z);
          // возможно, что все проще
            //redraw();
            shift(ref currentShape, p1.X-shiftx, p1.Y-shifty, p1.Z-shiftz);
            shiftx = p1.X;
            shifty = p1.Y;
            shiftz = p1.Z;
            redraw();
        }

        
    }
}