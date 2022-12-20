using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        private Pen pen;
        private Line line;
        private Ellipse ellipse;
        private Polygon polygon;
        public Form1()
        {
            InitializeComponent();
            Canvas.CreateCanvas(panel4);
            pen = new Pen(panel1.BackColor, trackBar1.Value);
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = comboBox3.SelectedIndex = 0;
            TrackBar[] trackBars = { trackBar1, trackBar2, trackBar3, trackBar4 };
            Label[] labels = { label3, label8, label10, label14 };
            for (uint i = 0; i < labels.Length; ++i)
                trackBars[i].DataBindings.Add(new Binding("Value", labels[i], "Text", true, DataSourceUpdateMode.OnPropertyChanged));
            line = new Line();
            ellipse = new Ellipse();
            polygon = new Polygon();
        }
        private void PanelClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((Panel)sender).BackColor = colorDialog1.Color;
                if(sender == panel1)
                    pen.Color = colorDialog1.Color;
            }
        }
        private void ActivateDrawMode(int index) 
        {
            for (int i = 0; i < 4; ++i)
                if (i != index)
                    Canvas.DrawMode[i] = false;
            Canvas.DrawMode[index] = true;
        }
        private void EraseShape(Action clear)
        {
            if (clear.Target != null && ((Line)clear.Target).IsWasDrawn)
                clear();
        }
        private void DrawShape(Action clear, int points)
        {
            EraseShape(clear);
            if (Canvas.Points.Count > points)
            {
                Canvas.ClearPoints();
                if (clear.Target.GetType() == typeof(Line))
                {
                    line = new Line(Canvas.Points.ToArray(), pen);
                    line.Draw();
                }
                else if (clear.Target.GetType() == typeof(Ellipse))
                {
                    ellipse = new Ellipse(Canvas.Points.ToArray(), new int[] { trackBar2.Value, trackBar3.Value }, panel2.BackColor, pen);
                    ellipse.Draw();
                }
                else
                {
                    polygon = new Polygon(Canvas.Points.ToArray(), trackBar4.Value, checkBox1.Checked,
                                      checkBox2.Checked & checkBox2.Enabled, panel3.BackColor, pen);
                    polygon.Draw();
                }
                Canvas.Points.Clear();
            }
        }
        private void ButtonLineClick(object sender, EventArgs e) => ButtonHandler(sender, line.Clear, new Button[] { button1, button2 },                                                                                  2, 0, "Выберите две точки на холсте");
        private void ButtonEllipseClick(object sender, EventArgs e) => ButtonHandler(sender, ellipse.Clear, new Button[] { button3, button4 },
                                                                                     1, 1, "Выберите центр эллипса на холсте");
        private void ButtonPolygonClick(object sender, EventArgs e) 
        {
            string message = checkBox1.Checked ? "Выберите базовую точку на холсте"
                                               : "Выберите не менее 3 точек на холсте и нажмите Enter для отрисовки";
            ButtonHandler(sender, polygon.Clear ,new Button[] { button5, button6 }, 
                          checkBox1.Checked ? 1 : 3, 2 + Convert.ToInt32(checkBox1.Checked), message);
        }
        private void ButtonHandler(object sender, Action clear, Button[] controls, int points, int activeMode, string message)
        {
            EraseShape(clear);
            ActivateDrawMode(activeMode);
            if (Canvas.Points.Count < points && sender == controls[0])
                textBox1.Text = message;
            else if (sender == controls[1])
                Canvas.Points.Clear();
        }
        private void CanvasDrawClick(object sender, MouseEventArgs e)
        {
            panel4.Focus();
            Canvas.AddPoint(e.Location);
            textBox1.Text = String.Format("Добалена точка с координатами {0},{1}", e.Location.X, e.Location.Y);
            if (Canvas.DrawMode[0])
                DrawShape(line.Clear, 1);
            else if (Canvas.DrawMode[1])
                DrawShape(ellipse.Clear, 0);
            else if (Canvas.DrawMode[2])
                EraseShape(polygon.Clear);
            else if (Canvas.DrawMode[3])
                DrawShape(polygon.Clear, 0);
        }
        private void ComboBoxUpdatePen(object sender, EventArgs e) 
        {
            ComboBox cb = sender as ComboBox;
            if (sender == comboBox1)
                pen.DashStyle = (DashStyle)cb.SelectedIndex;
            else if (sender == comboBox2)
                pen.StartCap = (LineCap)cb.SelectedIndex + 16;
            else
                pen.EndCap = (LineCap)cb.SelectedIndex + 16;
        }
        private void TrackBarUpdatePen(object sender, EventArgs e) => pen.Width = ((TrackBar)sender).Value;
        private void PanelPolygonUpdate(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DrawShape(polygon.Clear, 2);
        }
        private void CheckBoxUpdatePolygonStar()
        {
            Canvas.ClearPoints();
            Canvas.Points.Clear();
            bool isStarEnable = trackBar4.Value >= 3 * 2 & (trackBar4.Value & 1) == 0 & checkBox1.Checked;
            checkBox2.Enabled = isStarEnable;
        }
        private void CheckBoxUpdatePolygon(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            trackBar4.Enabled = label13.Enabled = label14.Enabled = cb.Checked;
            CheckBoxUpdatePolygonStar();
            ActivateDrawMode(2 + Convert.ToInt32(checkBox1.Checked));
        }
        private void TrackBarUpdatePolygon(object sender, EventArgs e) => CheckBoxUpdatePolygonStar();
        private void ButtonCleanAll(object sender, EventArgs e)
        {
            Pen clearPen = new Pen(Canvas.Color);
            Rectangle rect = new Rectangle(new Point(0, 0), Canvas.Size);
            Canvas.Graphic.DrawRectangle(clearPen, rect);
            Canvas.Graphic.FillRectangle(clearPen.Brush, rect);
            line.IsWasDrawn = ellipse.IsWasDrawn = polygon.IsWasDrawn = false;
            Canvas.Points.Clear();
        }
    }
    public static class Canvas
    {
        private static bool[] drawMode = { true, false, false, false };
        private static Size size;
        private static Color color;
        private static List<Point> points;
        private static Graphics graphic;
        public static void CreateCanvas(Control canvasFrom)
        {
            Canvas.size = canvasFrom.Size;
            Canvas.color = canvasFrom.BackColor;
            points = new List<Point>();
            graphic = canvasFrom.CreateGraphics();
        }
        private static void DrawMarker(Point point,Color color)
        {
            Rectangle rect = new Rectangle(new Point(point.X - 1, point.Y - 1), new Size(3, 3));
            Pen pen = new Pen(color);
            graphic.DrawEllipse(pen, rect);
            graphic.FillEllipse(pen.Brush, rect);
        }
        public static void AddPoint(Point point)
        {
            points.Add(point);
            DrawMarker(point, Color.Black);
        }
        public static void ClearPoints()
        {
            foreach(Point point in Points)
                DrawMarker(point, Color);
        }
        public static bool[] DrawMode { get { return drawMode; } }
        public static Size Size { get { return size; } }
        public static Color Color { get { return color; } }
        public static List<Point> Points { get { return points; } }
        public static Graphics Graphic { get { return graphic; } }
    }

    public class Line
    {
        private bool isWasDrawn;
        protected Pen pen;
        protected List<Point> coords;
        public Line() => isWasDrawn = false;
        public Line(Point[] points, Pen pen)
        {
            this.coords = new List<Point>(points);
            this.pen = pen.Clone() as Pen;
            isWasDrawn = false;
        }
        public virtual void Draw(bool isClear = false)
        {
            LineColor = isClear ? Canvas.Color : LineColor;
            Canvas.Graphic.DrawLines(pen, coords.ToArray());
            IsWasDrawn = !isClear;
        }
        public virtual void Clear() => Draw(true);
        public DashStyle Style { get { return pen.DashStyle; } set { pen.DashStyle = value; } }
        public LineCap StartCap { get { return pen.StartCap; } set { pen.StartCap = value; } }
        public LineCap EndCap { get { return pen.EndCap; } set { pen.EndCap = value; } }
        public Color LineColor { get { return pen.Color; } set { pen.Color = value; } }
        public float Thickness { get { return pen.Width; } set { pen.Width = value; } }
        public bool IsWasDrawn { get { return isWasDrawn; } set { isWasDrawn = value; } }
        public int Points { get { return coords.Count; } }
    }
    public class Ellipse : Line
    {
        private int radiusX;
        private int radiusY;
        private Color fillColor;
        public Ellipse() : base() { }
        public Ellipse(Point[] points, int[] radius, Color fillColor, Pen pen) : base(points, pen)
        {
            RadiusX = radius[0];
            RadiusY = radius[1];
            FillColor = fillColor;
        }
        public override void Draw(bool isClear = false)
        {
            LineColor = isClear ? Canvas.Color : LineColor;
            Rectangle rect = new Rectangle(coords[0].X - RadiusX, coords[0].Y - RadiusY, 2 * RadiusX, 2 * RadiusY);
            Canvas.Graphic.DrawEllipse(pen, rect);
            Canvas.Graphic.FillEllipse(new SolidBrush(isClear ? Canvas.Color : FillColor), rect);
            IsWasDrawn = !isClear;
            Canvas.Graphic.DrawString("Эллипс", new Font("Arial",16), new SolidBrush(isClear ? Canvas.Color : FillColor),
                                       new Point(coords[0].X - RadiusX / 2, coords[0].Y + RadiusY + 5));
        }
        public override void Clear() => Draw(true);
        public int RadiusX { get { return radiusX; } set { radiusX = value; } }
        public int RadiusY { get { return radiusY; } set { radiusY = value; } }
        public Color FillColor { get { return fillColor;  } set { fillColor = value; } }
    }
    public class Polygon : Line
    {
        const int defaultRadius = 200;
        private int sides;
        private Color fillColor;
        private Point CalculateNewPoint(Point center, double radius, double angle) => new Point((int)Math.Round(center.X + radius * Math.Cos(angle)),
                                                                                            (int)Math.Round(center.Y + radius * Math.Sin(angle)));
        public Polygon() : base() { }
        public Polygon(Point[] points,int sides, bool isRegular, bool isStar, Color fillColor, Pen pen) : base(points, pen)
        {
            Sides = sides;
            FillColor = fillColor;
            if(isRegular)
            {
                Point center = new Point(coords[0].X - defaultRadius, coords[0].Y);
                double baseAngle = 0;
                int drawPoints = isStar ? Sides / 2 : Sides;
                double sectorAngle = (2 * Math.PI) / drawPoints;
                for (uint i = 0; i < drawPoints; ++i)
                {
                    if (i != 0)
                        coords.Add(CalculateNewPoint(center, defaultRadius, baseAngle));
                    if (isStar)
                        coords.Add(CalculateNewPoint(center, defaultRadius / 3, baseAngle - sectorAngle / 2));
                    baseAngle -= sectorAngle;
                }
            }
        }
        public override void Draw(bool isClear = false)
        {
            LineColor = isClear ? Canvas.Color : LineColor;
            Point[] points = coords.ToArray();
            Canvas.Graphic.DrawPolygon(pen, points);
            Canvas.Graphic.FillPolygon(new SolidBrush(isClear ? Canvas.Color : FillColor), points);
            IsWasDrawn = !isClear;
        }
        public override void Clear() => Draw(true);
        public Color FillColor { get { return fillColor; } set { fillColor = value; } }
        public int Sides { get { return sides; } set { sides = value; } }
    }
}
