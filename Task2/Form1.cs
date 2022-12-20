using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Task2
{
    public partial class Form1 : Form
    {
        private Polygon polygon;
        private Vector2 rotPoint;
        private Timer timer;
        private float x;
        private float xOffset;
        public Form1()
        {
            InitializeComponent();
            funChoice.SelectedIndex = 0;
            timer = new Timer();
            timer.Interval = 1000 / 60;
            timer.Tick += OnTimerEvent;
            x = 0;
            xOffset = 1;
        }
        private bool CheckCoord(int halfDim, float coord, bool isX = true)
        {
            int size = isX ? polygon.Size.Width >> 1 : polygon.Size.Height >> 1;
            size = xOffset > 0 || funChoice.SelectedIndex == 2 ? size : -size; 
            return halfDim < Math.Abs(coord + size);
        }
        private void OnTimerEvent(object sender, EventArgs e)
        {
            Func<float, float>[] actions = new Func<float, float>[]
            {
                (x) => (float)(100*Math.Sin(x/30)),
                (x) => x == 0 ? (int)(1/(x - 1) * Canvas.Size.Height): (int)(1/(float)x * Canvas.Size.Height),
                (x) =>  x*x/50
            };
            float y = actions[funChoice.SelectedIndex](x);
            xOffset = CheckCoord(Canvas.Size.Width >> 1, x) 
                 || ( CheckCoord(Canvas.Size.Height >> 1, y, false) && funChoice.SelectedIndex != 1) ? -xOffset : xOffset;
            x += xOffset;
            polygon.Position = new Vector3(x, y, 0);
            UpdatePolygon();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.Cyan);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Canvas.Render();
            polygon.Draw();
            canvas.SwapBuffers();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            Canvas.Size = canvas.Size;
            Canvas.UpdateCanvas();
        }
        private void OnLoad(object sender, EventArgs e)
        {
            Canvas.CreateCanvas(canvas);
            polygon = new Polygon(Color.DarkMagenta);
        }

        private void UpdatePolygon()
        {
            string[] coords = coordsText.Text.Split(',');
            Vector3 origin = new Vector3(Int32.Parse(coords[0]), Int32.Parse(coords[1]),0.0f);
            Vector3 scale = new Vector3(float.Parse(xScaleValue.Text), float.Parse(yScaleValue.Text), 0.0f);
            int angle = isClockWise.Checked ? -anglesValues.Value : anglesValues.Value;
            polygon.Update(scale, angle,origin);
            canvas.Invalidate();
        }

        private void AngleChanged(object sender, EventArgs e)
        {
            int value = isClockWise.Checked ?  -anglesValues.Value: anglesValues.Value;
            angleValue.Text = value.ToString() + "°";
            UpdatePolygon();
        }

        private void FacesChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            faceValue.Text = tb.Value.ToString();
        }

        private void OnDrawPolygon(object sender, EventArgs e)
        {
            if (sender == circle)
                polygon = new Polygon(Color.Red, 80);
            else if(sender == square)
                polygon = new Polygon(Color.DarkMagenta);
            else
                polygon = new Polygon(Color.Orange,faces.Value);
            UpdatePolygon();
            canvas.Invalidate();
        }

        private void SelectRotationPoint(object sender, MouseEventArgs e)
        {
            if (!aroundSelf.Checked)
            {
                Vector2 point = Canvas.ConvertScreenToWorld(e.Location);
                rotPoint = point;
                messages.Text = String.Format("Выбрана точка вращения с координатами {0},{1}", e.X, e.Y);
                coordsText.Text = String.Format("{0},{1}", point.X, point.Y);
            }
        }
        private void ChangeScaleValue(TrackBar sender,Label label, TrackBar slaveTb)
        {
            label.Text = ((float)(sender.Value * 0.1)).ToString();
            int dim = sender == xScales ? polygon.DefaultSize.Width : polygon.DefaultSize.Height;
            dim  = (int)((float)sender.Value * 0.1f * dim);
            polygon.Size = sender == xScales ? new Size(dim,polygon.Size.Height) 
                                             : new Size(polygon.Size.Width, dim);
            if (uniformScale.Checked && slaveTb.Value != sender.Value)
                slaveTb.Value = sender.Value;
        }

        private void ScaleValueChanged(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            if (sender == xScales)
                ChangeScaleValue(tb, xScaleValue, yScales);
            else
                ChangeScaleValue(tb, yScaleValue, xScales);
            UpdatePolygon();
        }

        private void ScaleUniformChecked(object sender, EventArgs e)
        {
            xScaleValue.Text = yScaleValue.Text;
            xScales.Value = yScales.Value;
        }

        private void AnimatePolygon(object sender, EventArgs e)
        {
            if (animate.Checked)
            {
                timer.Start();
            }
            else
                timer.Stop();
        }

        private void AroundCheckBox(object sender, EventArgs e)
        {
            if (aroundSelf.Checked)
                coordsText.Text = "0,0";
            else
                messages.Text = "Выберите на холсте точку вращения";
        }
        private void CloseForm(object sender, FormClosedEventArgs e) => Canvas.FreeCanvas();
    }
    public class Polygon
    {
        private Vector4 color;
        private Size defaultSize;
        private Size size;
        private int sides;
        private int vao;
        private int vbo;
        private Vector3 position;
        private Vector3[] points;
        private Matrix4 model;
        private float GetCoord(Func<double, double> trigFun, double angle,float radius ) => radius*(float)trigFun(angle);
        public Polygon(Color color, int sides = 4) 
        {
            Color = new Vector4(color.R / (float)255,color.G / (float)255,color.B / (float)255,color.A / (float)255);
            Sides = sides;
            position = Vector3.Zero;
            model = Matrix4.Identity;
            List<Vector3> array = new List<Vector3>();
            double baseAngle = Math.PI / 4;
            double sectorAngle = 2 * Math.PI / sides;
            DefaultSize = Size = new Size(Canvas.Size.Width >> 2, Canvas.Size.Height >> 2);
            float radius = Math.Min(Size.Width, Size.Height);
            for (int i = 0; i < sides; ++i, baseAngle+=sectorAngle)
            {
                Vector3 vector = new Vector3(GetCoord(Math.Cos, baseAngle, radius), GetCoord(Math.Sin, baseAngle, radius),0.0f);
                array.AddRange(i - 2 > 0 ? new Vector3[] { array[array.Count - (i - 2 == 1 ? 1 : 2)], vector, array[0]} : new Vector3[] { vector });
            }
            points = array.ToArray();
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * 3 * points.Length),points, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }
        public void Update(Vector3 scale,int angle,Vector3 origin)
        {
            Matrix4 s = Matrix4.CreateScale(scale);
            Matrix4 t = Matrix4.CreateTranslation(position);
            Matrix4 r = Matrix4.CreateRotationZ(angle * (float)Math.PI / 180);
            if(origin == Vector3.Zero)
                Model = s * r * t;
            else
            {
                Matrix4 so = Matrix4.CreateTranslation(-origin);
                Matrix4 sb = Matrix4.CreateTranslation(origin);
                Model = so * s * r * t * sb;
            }
        }
        public void Draw()
        {
            Canvas.SetMatrixUniform(ref model, "model");
            int location = GL.GetUniformLocation(Canvas.Program, "color");
            GL.Uniform4(location, Color);
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, points.Length);
        }
        ~Polygon()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }
        public Vector4 Color { get { return color; } set { color = value; } }
        public Vector3 Position { get { return position; } set { position = value; } }
        public Size Size { get { return size; } set { size = value; } }
        public Size DefaultSize { get { return defaultSize; } set { defaultSize = value; } }
        public int Sides { get { return sides; } set { sides = value ; } }
        public Matrix4 Model { get { return model; } set { model = value; } }
    }
    public static class Canvas
    {
        private static Matrix4 view;
        private static Matrix4 projection;
        private static int program;
        private static int vertexShader;
        private static int fragmentShader;
        private static Size size;
        private static void LoadShader(string source,ShaderType type)
        {
            if (File.Exists(source))
            {
                int shader = GL.CreateShader(type);
                string data;
                using (StreamReader reader = new StreamReader(source))
                    data = reader.ReadToEnd();
                GL.ShaderSource(shader, data);
                GL.CompileShader(shader);
                if (type == ShaderType.VertexShader)
                    vertexShader = shader;
                else
                    fragmentShader = shader;
            }
        }
        private static int CreateProgram()
        {
            program =  GL.CreateProgram();
            LoadShader("shaders/vertex.vs", ShaderType.VertexShader);
            LoadShader("shaders/fragment.fs", ShaderType.FragmentShader);
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            return 0;
        }
        public static void SetMatrixUniform(ref Matrix4 matrix, string name)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }
        public static void CreateCanvas(GLControl control)
        { 
            Size = control.Size;
            CreateProgram();
            view = Matrix4.LookAt(Vector3.UnitZ, -Vector3.UnitZ,Vector3.UnitY);
            UpdateCanvas();
        }
        public static void UpdateCanvas()
        {
            GL.Viewport(0, 0, Size.Width, Size.Height);
            projection = Matrix4.CreateOrthographic(Size.Width, Size.Height, 0.1f, 50.0f);
        }
        public static void Render()
        {
            GL.UseProgram(program);
            SetMatrixUniform(ref view, "view");
            SetMatrixUniform(ref projection, "projection");
        }
        public static Vector2 ConvertScreenToWorld(Point point) => new Vector2(point.X - Size.Width / 2, Size.Height / 2 - point.Y);
        public static void FreeCanvas()
        {
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteProgram(program);
        }
        public static int Program { get { return program; } set { program = value; } }
        public static Size Size { get { return size; } set { size = value; } }
    }
}
