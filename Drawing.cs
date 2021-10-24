using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Dbasics
{
    public enum ShapeType { TETRAHEDRON, HEXAHEDRON, OCTAHEDRON, ICOSAHEDRON, DODECAHEDRON }

    public partial class Form1
    {

        bool isPerspective = true;
        bool isAxisVisible = true;
        ShapeType currentShape;

        private void btnShowAxis_Click(object sender, EventArgs e)
        {

        }
        private void buttonShape_Click(object sender, EventArgs e)
        {
            setFlags(true);
        }
    }
}
