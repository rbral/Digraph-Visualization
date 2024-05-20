using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs
{
    public partial class Form1 : Form
    {
        private const int CENTER = 220;     // distance from the edge of the panel
        private const int DFC = 160;        // Distance from Center of the vertex
        private const int LABEL_SIZE = 18;  // size of the displayed ertex labels
        private const int WEE = 2;          // wee bit compensation
        //private const int HLB = LABEL_SIZE / 2;
        private const int EDGE_THICK = 3;
        private readonly Brush EDGE_COLOR = Brushes.Red;


        Digraph<String> digraph = new Digraph<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var server = tbServer.Text;
            var database = tbDatabase.Text;
            var graph = tbGraph.Text;
            digraph = LoadDigraph(server, database, graph);
            Display(digraph);
        }

        private Digraph<String> LoadDigraph(String server, String database, String graph)
        {
            Digraph<String> digraph = new Digraph<string>();
            String strConnect = $"Server={server};Database={database};Trusted_Connection=True;";
            SqlConnection sqlCon;
            try
            {
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();
                SortedList<int, Vertex<String>> vertices = FetchVertices(sqlCon, graph);
                digraph = FetchEdges(vertices, sqlCon, graph);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return digraph;
        }

        private SortedList<int, Vertex<String>> FetchVertices(SqlConnection sqlCon, String graph)
        {
            SortedList<int, Vertex<String>> vertices = new SortedList<int, Vertex<string>>();
            /* set up a call to dynamic SQL query: */
            SqlCommand sqlCmd = new SqlCommand(
                        "SELECT Vertex_ID, Info FROM Vertex WHERE Graph = '" + graph + "'",
                        sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            /* execute */
            sqlCmd.ExecuteNonQuery();

            /* get the data returned */
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataSet dtVertices = new DataSet();
            da.Fill(dtVertices, "Vertices");
            // using verts = dtVertices.Tables["Vertices"].Rows;
            int nrVertices = dtVertices.Tables["Vertices"].Rows.Count;
            for (int vix = 0; vix < nrVertices; ++vix)
            {
                int id = (int)dtVertices.Tables["Vertices"].Rows[vix][0];
                string vx = dtVertices.Tables["Vertices"].Rows[vix][1].ToString();
                vertices.Add(id, new Vertex<String>(vx));
            }
            return vertices;
        }

        private Digraph<String> FetchEdges(SortedList<int, Vertex<String>> vertices, SqlConnection sqlCon, String graph)
        {
            Digraph<String> digraph = new Digraph<string>();
            /* set up a call to dynamic SQL query: */
            SqlCommand sqlCmd = new SqlCommand(
                        "SELECT Edge_ID, SrcVertex, TgtVertex, Weight FROM Edge WHERE Graph =  '" + graph + "'",
                        sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            try
            {
                /* execute */
                sqlCmd.ExecuteNonQuery();

                /* get the data returned */
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataSet dtEdges = new DataSet();
                da.Fill(dtEdges, "Edges");
                int nrEdges = dtEdges.Tables["Edges"].Rows.Count;
                for (int eix = 0; eix < nrEdges; ++eix)
                {
                    int srcVix = (int)dtEdges.Tables["Edges"].Rows[eix][1];
                    int tgtVix = (int)dtEdges.Tables["Edges"].Rows[eix][2];
                    double wgt = dtEdges.Tables["Edges"].Rows[eix][3]== DBNull.Value
                        ? 0.0
                        : (double)dtEdges.Tables["Edges"].Rows[eix][3];
                    if (vertices.ContainsKey(srcVix))
                    {
                        Vertex<String> srcVertex = vertices[srcVix];
                        if (vertices.ContainsKey(tgtVix))
                        {
                            Vertex<String> tgtVertex = vertices[tgtVix];
                            srcVertex.AddNeighbor(tgtVertex, wgt);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "FetchEdges error");
            }
            foreach (int key in vertices.Keys)
            {
                digraph.AddVertex(vertices[key]);
            }
            return digraph;
        }

        private void Display(Digraph<String> digraph)
        {
            int nrVertices = digraph.Vertices.Count;
            panelGraph.Controls.Clear();
            panelGraph.Invalidate();
            for (int vix = 0; vix < nrVertices; ++vix)
            {
                Label picVertex = new Label();
                panelGraph.Controls.Add(picVertex);
                picVertex.Text = digraph.Vertices[vix].ToString();
                picVertex.Location = GetLocation(vix, nrVertices);
                picVertex.Width = picVertex.Height = LABEL_SIZE;
                picVertex.BorderStyle = BorderStyle.FixedSingle;
                picVertex.BackColor = Color.Transparent;
                digraph.Vertices[vix].Image = picVertex;
            }
            DrawEdges_(digraph);

        }

        private Point GetLocation(int ordinal, int outOf)
        {
            double angle = 2.0 * Math.PI / outOf * ordinal;
            int xCoord = (int)Math.Floor(CENTER + DFC * Math.Cos(angle));
            int yCoord = (int)Math.Floor(CENTER - DFC * Math.Sin(angle));
            Point point = new Point(xCoord, yCoord);
            return point;
        }

        private void DrawEdges(Digraph<String> dg)
        {
            /*
             * for each vertex in digraph,
             *    get location of start of arrow from sv
             *    for each neighbor of vertex, draw arrow to its location
             */
            Graphics gr = panelGraph.CreateGraphics();
            Pen pen = new Pen(Brushes.Red, 3);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            foreach (Vertex<String> vertex in dg.Vertices)
            {
                Point src = vertex.Image.Location;
                foreach (Vertex<String> nbr in vertex.Neighbors.Keys)
                {
                    Point tgt = nbr.Image.Location;
                    gr.DrawLine(pen, tgt, src);
                }
            }

        }

        private void DrawEdges_(Digraph<String> dg)
        {
            /*
             * for each vertex in digraph,
             *    get location of start of arrow from sv
             *    for each neighbor of vertex, draw arrow to its location
             */
            Graphics gr = panelGraph.CreateGraphics();
            Pen pen = new Pen(EDGE_COLOR, EDGE_THICK);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            foreach (Vertex<String> vertex in dg.Vertices)
            {
                Point src = vertex.Image.Location;
                foreach (Vertex<String> nbr in vertex.Neighbors.Keys)
                {
                    Point tgt = nbr.Image.Location;
                    DrawEdge(gr, pen, tgt, src);
                }
            }
        }

        /*
         *  ┎─┒  ┎─┒ ┎─┒
         *  ┖─┚  ┖─┚ ┖─┚
         *     ╲ |  ╱
         *  ┎─┒━┎─┒━┎─┒
         *  ┖─┚ ┖─┚ ┖─┚
         *     ╱|  ╲
         *  ┎─┒ ┎─┒ ┎─┒
         *  ┖─┚ ┖─┚ ┖─┚ 
         */
        private void DrawEdge(Graphics gr, Pen pen, Point tgt, Point src)
        {
            int xt = tgt.X;
            int yt = tgt.Y;
            int xs = src.X;
            int ys = src.Y;
            if (xt == xs)
            {
                if (yt > ys)
                    gr.DrawLine(pen, xt, yt, xs, ys + LABEL_SIZE);
                else
                    gr.DrawLine(pen, xt, yt + LABEL_SIZE, xs, ys);
            }
            else if (xt < xs)
            {
                if (yt > ys)
                    gr.DrawLine(pen, xt + LABEL_SIZE, yt, xs, ys + LABEL_SIZE);
                else if (yt == ys)
                    gr.DrawLine(pen, xt + LABEL_SIZE, yt, xs, ys);
                else // yt < ys
                    gr.DrawLine(pen, xt + LABEL_SIZE, yt + LABEL_SIZE, xs, ys);
            }
            else // xt > xs
            {
                if (yt > ys)
                    gr.DrawLine(pen, xt, yt, xs + LABEL_SIZE, ys + LABEL_SIZE);
                else if (yt == ys)
                    gr.DrawLine(pen, xt, yt, xs + LABEL_SIZE, ys);
                else // yt < ys
                    gr.DrawLine(pen, xt, yt + LABEL_SIZE, xs + LABEL_SIZE, ys);
            }
        }

        private void topSortButton_Click(object sender, EventArgs e)
        {
            List<Vertex<String>> sortedVertices = digraph.TopologicalSort();
            string result = string.Join(", ", sortedVertices.Select(v => v.ToString()));
            MessageBox.Show("Topological Sort Result: \n" + result);


        }

        private void dijkstraButton_Click(object sender, EventArgs e)
        {

        }


        private void prillsButton_Click(object sender, EventArgs e)
        {

        }

        private void kruskalsButton_Click(object sender, EventArgs e)
        {

        }
    }

}