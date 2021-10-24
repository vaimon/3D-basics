using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Dbasics
{
    class Point
    {
        int x, y, z;

        public Point(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Z { get => z; set => z = value; }
    }

    class Line
    {
        Point start,end;

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
    }

    class Face
    {
        List<Line> edges;

        public Face()
        {
            edges = new List<Line>();
        }

        public Face(IEnumerable<Line> edges) : this()
        {
            this.edges.AddRange(edges);
        }

        public Face addEdge(Line edge)
        {
            edges.Add(edge);
            return this;
        }
        public Face addEdges(IEnumerable<Line> edges)
        {
            this.edges.AddRange(edges);
            return this;
        }

        public List<Line> Edges { get => edges; }
    }

    class Shape
    {
        List<Face> faces;

        public Shape()
        {
            faces = new List<Face>();
        }

        public Shape(IEnumerable<Face> faces) : this()
        {
            this.faces.AddRange(faces);
        }

        public Shape addFace(Face face)
        {
            faces.Add(face);
            return this;
        }
        public Shape addFaces(IEnumerable<Face> faces)
        {
            this.faces.AddRange(faces);
            return this;
        }

        public List<Face> Faces { get => faces; }
    }

    class ShapeGetter
    {
        public static Shape getTetrahedron()
        {
            Shape res = new Shape();
            Face face = new Face().addEdge(new Line(new Point(0,0,0), new Point(0,0,0)));
            return res;
        }
    }
}
