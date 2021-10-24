using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Dbasics
{
    public enum ShapeType { TETRAHEDRON, HEXAHEDRON, OCTAHEDRON, ICOSAHEDRON, DODECAHEDRON }

    public partial class Form1
    {
        bool isAxisVisible = false;
        ShapeType currentShapeType;
        Shape currentShape;
        Graphics g;

        private void btnShowAxis_Click(object sender, EventArgs e)
        {
            isAxisVisible = !isAxisVisible;
            g.Clear(Color.White);
            if (isAxisVisible)
            {
                drawAxis();
            }
            drawShape(currentShape);
            btnShowAxis.Text = isAxisVisible ? "Скрыть точки и оси" : "Показать точки и оси";
        }
        private void buttonShape_Click(object sender, EventArgs e)
        {
            if (isInteractiveMode)
            {
                setFlags(false);
                g.Clear(Color.White);
            }
            else
            {
                currentShape = ShapeGetter.getShape(currentShapeType);
                redraw();
                setFlags(true);
            }
            
        }

        void drawShape(Shape shape)
        {
            foreach(var face in shape.Faces)
            {
                drawFace(face);
            }
        }

        void drawFace(Face face)
        {
            foreach(var line in face.Edges)
            {
                drawLine(line, new Pen(Color.Black, 3));
            }
        }

        void drawLine(Line line, Pen pen)
        {
            g.DrawLine(pen, line.Start.to2D(), line.End.to2D());
        }

        void drawAxis()
        {
            Line axisX = new Line(new Point(0, 0, 0), new Point(300, 0, 0));
            Line axisY = new Line(new Point(0, 0, 0), new Point(0, 300, 0));
            Line axisZ = new Line(new Point(0, 0, 0), new Point(0, 0, 300));
            drawLine(axisX, new Pen(Color.DarkRed, 4));
            drawLine(axisY, new Pen(Color.DarkBlue, 4));
            drawLine(axisZ, new Pen(Color.DarkGreen, 4));

            g.ScaleTransform(1.0F, -1.0F);
            g.TranslateTransform(0.0F, -(float)canvas.Height);
            foreach (var face in currentShape.Faces)
            {
                foreach(var line in face.Edges)
                {
                    g.DrawString($"  ({line.Start.X}, {line.Start.Y}, {line.Start.Z})", new Font("Arial", 12, FontStyle.Italic), new SolidBrush(Color.DarkBlue), line.Start.to2D().X, canvas.Height - line.Start.to2D().Y);
                    g.DrawString($"  ({line.End.X}, {line.End.Y}, {line.End.Z})", new Font("Arial", 12, FontStyle.Italic), new SolidBrush(Color.DarkBlue), line.End.to2D().X, canvas.Height - line.End.to2D().Y);
                }
            }
            g.ScaleTransform(1.0F, -1.0F);
            g.TranslateTransform(0.0F, -(float)canvas.Height);
        }

        void redraw()
        {
            g.Clear(Color.White);
            
            drawShape(currentShape);
            if (isAxisVisible)
            {
                drawAxis();
            }
        }

        
    }
}
