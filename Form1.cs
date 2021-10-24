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
        public Form1()
        {
            InitializeComponent();
            selectShape.SelectedIndex = 0;
            selectAxis.SelectedIndex = 0;
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
            btnShowAxis.Enabled = interactiveMode;
            textAngle.Enabled = interactiveMode;
            textScaleX.Enabled = interactiveMode;
            textScaleY.Enabled = interactiveMode;
            textScaleZ.Enabled = interactiveMode;
            textShiftX.Enabled = interactiveMode;
            textShiftY.Enabled = interactiveMode;
            textShiftZ.Enabled = interactiveMode;
            rbAxonometric.Enabled = interactiveMode;

            buttonShape.Text = interactiveMode ? "Очистить" : "Нарисовать";
            selectShape.Enabled = !interactiveMode;
        }

        private void comboBoxShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectShape.SelectedIndex)
            {
                case 0: currentShape = ShapeType.TETRAHEDRON; break;
                case 1: currentShape = ShapeType.HEXAHEDRON; break;
                case 2: currentShape = ShapeType.OCTAHEDRON; break;
                case 3: currentShape = ShapeType.ICOSAHEDRON; break;
                case 4: currentShape = ShapeType.DODECAHEDRON; break;
                default: throw new Exception("Фигурки всё сломали :(");
            }
        }

        private void selectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectShape.SelectedIndex)
            {
                case 0: currentAxis = AxisType.X; break;
                case 1: currentAxis = AxisType.Y; break;
                case 2: currentAxis = AxisType.Z; break;
                default: throw new Exception("Оси всё сломали :(");
            }
        }

        private void rbPerspective_CheckedChanged(object sender, EventArgs e)
        {
            isPerspective = rbPerspective.Checked;
        }
    }
}