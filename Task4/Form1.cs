using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leafs
{
    public partial class Form1 : Form
    {
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
        }
        private float CheckCoord(Func<float, float, float> fun, float a, float b) => fun(a, b);

        private void CheckPoint(float[] dim,float coord)
        {
            dim[0] = CheckCoord(Math.Min, dim[0], coord);
            dim[1] = CheckCoord(Math.Max, dim[1], coord);
        }

        private void CheckRectangle(RectangleF rect, float[] dimX, float[] dimY)
        {
            CheckPoint(dimX, rect.X);
            CheckPoint(dimY, rect.Y);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            g.Clear(canvas.BackColor);
            Pen pen = new Pen(Color.Red);
            Point cCanvas = new Point(canvas.Width >> 1, canvas.Height >> 1);
            float[] dimXimage = { 100, -100 };
            float[] dimYimage = { 100, -100 };
            float a = aT.Value * 0.1f, b = bT.Value * 0.1f, c = cT.Value * 0.1f, d = dT.Value * 0.1f;
            int height = recT.Value;
            List<RectangleF> points = new List<RectangleF>();
            RectangleF rectangle = new RectangleF(1, 0, 1, 1);
            Action<RectangleF, int> DrawLeafs = null;
            DrawLeafs = (point, h) =>
            {
                points.Add(rectangle);
                CheckRectangle(point, dimXimage, dimYimage);
                if (h > 0)
                {
                    rectangle.X = a * point.X + b * point.Y;
                    rectangle.Y = b * point.X - a * point.Y;
                    DrawLeafs(rectangle, h - 1);
                    rectangle.X = c * (point.X - xT.Value * 0.1f) - d * point.Y + xT.Value * 0.1f;
                    rectangle.Y = d * (point.X - xT.Value * 0.1f) + c * point.Y;
                    DrawLeafs(rectangle, h - 1);
                }
            };
            DrawLeafs(rectangle, height);
            float fx = canvas.Width / (dimXimage[1] - dimXimage[0]);
            float fy = canvas.Height / (dimYimage[1] - dimYimage[0]);
            float f = (fx < fy ? fx : fy) * 0.8f;
            float[] cImage = { (dimXimage[0] + dimXimage[1]) / 2, (dimYimage[0] + dimYimage[1]) / 2 };
            for (int i = 0; i < points.Count; ++i)
            {
                rectangle.X = cCanvas.X + f * (points[i].X - cImage[0]);
                rectangle.Y = cCanvas.Y + f * (points[i].Y - cImage[1]);
                g.DrawRectangle(pen, rectangle.X, rectangle.Y, 1, 1);
            }
        }

        private void OnChange(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            Label[] labels = { aV, bV, cV, dV, xV };
            TrackBar[] tbs = { aT, bT, cT, dT, xT };
            Func<int> SearchIndex = () =>
            {
                for (int i = 0; i < 5; ++i)
                    if (tb == tbs[i])
                        return i;
                return -1;
            };
            int index = SearchIndex();
            if (index != -1)
                labels[index].Text = (tbs[index].Value * 0.1f).ToString();
            else
                recV.Text = recT.Value.ToString();
        }

        private void OnDraw(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
