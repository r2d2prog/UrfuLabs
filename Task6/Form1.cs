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

namespace Task6
{
    public partial class Form1 : Form
    {
        private Button lastPainter;
        private readonly Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000 / 60;
            timer.Tick += OnTimerEvent;
        }
        private void OnTimerEvent(object sender, EventArgs e)
        {
            Transforms.Rotation.Y += 1;
            Transforms.Rotation.X += 1;
            if (Transforms.Rotation.Y >= 360)
                Transforms.Rotation.Y = 0;
            if(Transforms.Rotation.X >= 360)
                Transforms.Rotation.X = 0;
            Geometric.UpdateModel();
            Canvas.Invalidate();
        }

        private void OnSavePainter(object sender, EventArgs e) => lastPainter = sender as Button;

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (Canvas.IsLoaded)
            {
                GL.ClearColor(Color.Cyan);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Canvas.Render();
                Geometric.Draw();
                Canvas.SwapBuffers();
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Transforms.CreateTransforms();
            Canvas.CreateCanvas(canvas);
            GL.Enable(EnableCap.DepthTest);
            Button[] buttons = {polBtn, sqrBtn, crcBtn, pyrBtn, trpBtn, octBtn, prlBtn, cylBtn, 
                                hlxBtn, sphBtn, cnsBtn, torBtn, tetBtn, hexBtn, ikoBtn, dodBtn };
            for (int i = 0; i < buttons.Length; ++i)
                buttons[i].Click += new EventHandler(OnSavePainter);
        }

        private void OnResize(object sender, EventArgs e)
        {
            Canvas.Size = canvas.Size;
            Canvas.UpdateCanvas();
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            Geometric.FreeBuffers();
            Canvas.FreeCanvas();
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (rotateCheck.Checked)
                timer.Start();
            else
                timer.Stop();
        }

        private void OnWireframe(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            canvas.Invalidate();
        }

        private int ConvertToSliderValue(ref Vector3 vec, Transforms.Transform transform, Transforms.Axis axis)
        {
            switch (transform)
            {
                case Transforms.Transform.Scale:
                    return (int)((axis == Transforms.Axis.X ? vec.X : axis == Transforms.Axis.Y ? vec.Y : vec.Z) * 10);
                default:
                    return (int)(axis == Transforms.Axis.X ? vec.X : axis == Transforms.Axis.Y ? vec.Y : vec.Z);
            }
        }

        private int GetMinMax(Transforms.Transform transform, Transforms.Axis axis, bool isMax = true)
        {
            switch (transform)
            {
                case Transforms.Transform.Scale:
                    return isMax ? 100 : -100;
                case Transforms.Transform.Rotation:
                    return isMax ? 360 : 0;
                default:
                    if (axis == Transforms.Axis.Z)
                        return isMax ? 10 : -50;
                    else 
                        return isMax ? 30 : -30;
            }
        }

        private void OnReset(object sender, EventArgs e)
        {
            Transforms.ResetTransforms();
            Vector3 vec = SclBut.Checked ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3();
            Transforms.Transform transform = SclBut.Checked ? Transforms.Transform.Scale : RotBut.Checked ?
                                                    Transforms.Transform.Rotation : Transforms.Transform.Translation;
            int value = ConvertToSliderValue(ref vec, transform, Transforms.Axis.X);
            Button temp = lastPainter;
            lastPainter = null;
            X.Value = Y.Value = Z.Value = value;
            lastPainter = temp;
            Geometric.UpdateModel();
            Canvas.Invalidate();
        }

        private void OnStateSliderChange(ref Vector3 vec, Transforms.Transform transform)
        {
            X.Maximum = GetMinMax(transform, Transforms.Axis.X);
            Y.Maximum = GetMinMax(transform, Transforms.Axis.Y);
            Z.Maximum = GetMinMax(transform, Transforms.Axis.Z);
            X.Minimum = GetMinMax(transform, Transforms.Axis.X, false);
            Y.Minimum = GetMinMax(transform, Transforms.Axis.Y, false);
            Z.Minimum = GetMinMax(transform, Transforms.Axis.Z, false);
            X.Value = ConvertToSliderValue(ref vec, transform, Transforms.Axis.X);
            Y.Value = ConvertToSliderValue(ref vec, transform, Transforms.Axis.Y);
            Z.Value = ConvertToSliderValue(ref vec, transform, Transforms.Axis.Z);
        }

        private void OnVectorChange(ref Vector3 vec, Transforms.Transform transform)
        {
            XAxisValue.Text = transform == Transforms.Transform.Rotation ? vec.X.ToString() + "°" : vec.X.ToString();
            YAxisValue.Text = transform == Transforms.Transform.Rotation ? vec.Y.ToString() + "°" : vec.Y.ToString();
            ZAxisValue.Text = transform == Transforms.Transform.Rotation ? vec.Z.ToString() + "°" : vec.Z.ToString();
            OnStateSliderChange(ref vec, transform);
        }

        private void OnCheckChange(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb == RotBut)
                OnVectorChange(ref Transforms.Rotation, Transforms.Transform.Rotation);
            else if (rb == SclBut)
                OnVectorChange(ref Transforms.Scale, Transforms.Transform.Scale);
            else
                OnVectorChange(ref Transforms.Translation, Transforms.Transform.Translation);
        }

        private void OnChange(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            Label label = tb == X ? XAxisValue : tb == Y ? YAxisValue : ZAxisValue;
            if (RotBut.Checked)
            {
                if (tb == X)
                    Transforms.Rotation.X = tb.Value;
                else if (tb == Y)
                    Transforms.Rotation.Y = tb.Value;
                else
                    Transforms.Rotation.Z = tb.Value;

            }
            else if (SclBut.Checked)
            {
                if (tb == X)
                    Transforms.Scale.X = tb.Value * 0.1f;
                else if (tb == Y)
                    Transforms.Scale.Y = tb.Value * 0.1f;
                else
                    Transforms.Scale.Z = tb.Value * 0.1f;
            }
            else
            {
                if (tb == X)
                    Transforms.Translation.X = tb.Value;
                else if (tb == Y)
                    Transforms.Translation.Y = tb.Value;
                else
                    Transforms.Translation.Z = tb.Value;
            }
            label.Text = SclBut.Checked ? (tb.Value * 0.1f).ToString("0.0")
                                : RotBut.Checked ? tb.Value.ToString() + "°" : tb.Value.ToString();
            if (lastPainter != null)
            {
                Geometric.UpdateModel();
                Canvas.Invalidate();
            }
        }
        private void OnSqrDraw(object sender, EventArgs e) => Geometric.Square();

        private void OnCrcDraw(object sender, EventArgs e) => Geometric.Circle();

        private void OnPolDraw(object sender, EventArgs e) => Geometric.Polygon(sides.Value);

        private void OnOctDraw(object sender, EventArgs e) => Geometric.Octahedron(sideTrack.Value);

        private void OnTetDraw(object sender, EventArgs e) => Geometric.Tetrahedron(sideTrack2.Value);

        private void OnDodDraw(object sender, EventArgs e) => Geometric.Dodecahedron(sideTrack2.Value);

        private void OnIkoDraw(object sender, EventArgs e) => Geometric.Ikosahedron(sideTrack2.Value);

        private void OnTorDraw(object sender, EventArgs e) => Geometric.Torus(radTrack3.Value, segTrack.Value, segTrack.Value);


        private void OnHexDraw(object sender, EventArgs e) => Geometric.Hexahedron(sideTrack2.Value);

        private void OnHlx(object sender, EventArgs e) => Geometric.Helix(radTrack3.Value, segTrack.Value, segTrack.Value, coilTrack.Value);

        private void OnSph(object sender, EventArgs e) => Geometric.Sphere(radTrack3.Value,segTrack.Value, segTrack.Value);

        private void OnCns(object sender, EventArgs e) => Geometric.Conus(radTrack3.Value, segTrack.Value);

        private void OnPrl(object sender, EventArgs e) => Geometric.Parallelepiped(width.Value, height.Value, depth.Value);

        private void OnPyr(object sender, EventArgs e) => Geometric.Pyramid(sideTrack.Value);

        private void OnTrp(object sender, EventArgs e) => Geometric.TruncatedPyramid(sideTrack.Value);

        private void OnCyl(object sender, EventArgs e) => Geometric.Cylinder(radTrack2.Value, radTrack.Value, segTrack.Value);

        private void OnSlide(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            TrackBar[] tbs = {sides ,width, height, depth, sideTrack, segTrack ,radTrack, radTrack2, coilTrack ,radTrack3, sideTrack2 };
            Label[] lbs = {pValue ,wValue, hValue, dValue, sValue, segValue ,rValue, rValue2, coilValue, rValue3, sValue2 };
            for (int i = 0; i < tbs.Length; ++i)
            {
                if (trackBar == tbs[i])
                {
                    lbs[i].Text = trackBar.Value.ToString();
                    if ((lastPainter != null && lastPainter.TabIndex != 0 && trackBar.TabIndex % lastPainter.TabIndex == 0))
                        lastPainter.PerformClick();
                    else if (trackBar.Tag != null && lastPainter != null && trackBar.Tag == lastPainter.Tag)
                        lastPainter.PerformClick();
                    break;
                }
            }
        }
    }
    public static class Geometric
    {
        private static int vao;
        private static int vbo;
        private static Vector3[] points;
        private static Matrix4 model;
        public enum Plane
        {
            XY,
            XZ
        }
        public enum Direction
        {
            CCW,
            CW
        }
        private static Vector3[] InitData(int pointsCount, int baseSides)
        {
            if (vao != 0 && vbo != 0)
                FreeBuffers();
            UpdateModel();
            points = new Vector3[pointsCount];
            return baseSides == 0 ? null : new Vector3[baseSides];
        }

        private static void CreateIkoGeometry(float side, int vertices, Action<Vector3, Vector3, Vector3> fillAnotherContour = null)
        {
            Vector3[] contour = InitData(vertices, 10);
            float radius = 0.0f, baseHeight = 0.0f, capHeight = 0.0f;
            GetSizes(side, ref radius, ref baseHeight, ref capHeight);
            Vector3 offset = new Vector3(0.0f, baseHeight, 0.0f);
            CreateContour(contour, 5, 5 * (float)Math.PI / 4, radius, 0, Plane.XZ, offset);
            offset.Y = -offset.Y;
            CreateContour(contour, 5, 5 * (float)Math.PI / 4 + 36 * (float)Math.PI / 180, radius, 5, Plane.XZ, offset);
            offset.Y = baseHeight - offset.Y;
            FillContour(contour, 0, 5, 30, 90, Direction.CCW, offset, fillAnotherContour);
            offset.Y = -offset.Y;
            FillContour(contour, 5, 5, 45, 105, Direction.CW, offset, fillAnotherContour);
            CreateSegment(contour, 5, 0, 5, 0, 60, true, fillAnotherContour);
        }

        private static double GetCoord(Func<double, double> trigFun, double angle, double radius) => radius * trigFun(angle);
        private static Vector3 CalculateCenter(Vector3 a, Vector3 b, Vector3 c)
        {
            float averX = (a.X + b.X + c.X) / 3;
            float averY = (a.Y + b.Y + c.Y) / 3;
            float averZ = (a.Z + b.Z + c.Z) / 3;
            return new Vector3(averX, averY, averZ);
        }

        private static Vector3 CalculateNormal(Vector3 origin, Vector3 a, Vector3 b)
        {
            Vector3 normal = Vector3.Cross(a - origin, b - origin);
            float dir = normal.X * origin.X + normal.Y * origin.Y + normal.Z * origin.Z;
            if (dir < 0)
                normal = -normal;
            return normal;
        }

        private static void WriteNormals(Vector3[] points, Vector3 normal, int destOffset, int count)
        {
            for (int i = 0; count > 0; --count, ++i)
                points[destOffset + i] = normal;
        }

        private static void FillContour(Vector3[] contour, int cReadConst, int vertices,
                                        int vWrite, int nWrite, Direction dir = Direction.CCW,
                                        Object vertex = null, Action<Vector3, Vector3, Vector3> fillAnotherContour = null)
        {
            int triangles = vertex == null ? vertices - 2 : vertices;
            Vector3 top = vertex == null ? new Vector3() : (Vector3)vertex;
            for (int i = 0, cRead = cReadConst; i < triangles; ++i, vWrite += 3, ++cRead)
            {
                if (vertex != null)
                {
                    Vector3 a = dir == Direction.CCW ? contour[cRead] : i == triangles - 1 ? contour[cReadConst] : contour[cRead + 1];
                    Vector3 b = dir == Direction.CCW ? i == triangles - 1 ? contour[cReadConst] : contour[cRead + 1] : contour[cRead];
                    if (fillAnotherContour != null)
                        fillAnotherContour(top, a, b);
                    else
                    {
                        points[vWrite] = top;
                        points[vWrite + 1] = a;
                        points[vWrite + 2] = b;
                        Vector3 normal = CalculateNormal(points[vWrite], points[vWrite + 1], points[vWrite + 2]);
                        WriteNormals(points, normal, nWrite, 3);
                        nWrite += 3;
                    }
                }
                else
                {
                    points[vWrite] = contour[cReadConst];
                    if (i == 0)
                    {
                        points[vWrite + 2] = dir == Direction.CCW ? contour[++cRead] : contour[cRead += 2];
                        points[vWrite + 1] = dir == Direction.CCW ? contour[++cRead] : contour[cRead - 1];
                        continue;
                    }
                    points[vWrite + 1] = dir == Direction.CCW ? contour[cRead] : contour[cRead - 1];
                    points[vWrite + 2] = dir == Direction.CCW ? contour[cRead - 1] : contour[cRead];
                }
            }
            if (vertex == null)
            {
                Vector3 normal = CalculateNormal(points[vWrite - 3], points[vWrite - 2], points[vWrite - 1]);
                WriteNormals(points, normal, nWrite, triangles * 3);
            }
        }

        private static void CreateSegment(Vector3[] contour, int sides, int cLeft, 
                                            int cRight, int vWrite, int nWrite,
                                            bool splitNormals = false, Action<Vector3, Vector3, Vector3> fillAnotherContour = null)
        {
            for (int i = 0, left = cLeft, right = cRight; i < sides; ++i, ++left, ++right, vWrite += 6, nWrite += 6)
            {
                Vector3 conditionLeft = i == sides - 1 ? contour[cLeft] : contour[left + 1];
                Vector3 conditionRight = i == sides - 1 ? contour[cRight] : contour[right + 1];
                if (fillAnotherContour != null)
                {
                    fillAnotherContour(contour[left], contour[right], conditionLeft);
                    fillAnotherContour(contour[right], conditionRight, conditionLeft);
                }
                else
                {
                    points[vWrite] = contour[left];
                    points[vWrite + 1] = contour[right];
                    points[vWrite + 2] = conditionLeft;
                    points[vWrite + 3] = contour[right];
                    points[vWrite + 4] = conditionRight;
                    points[vWrite + 5] = conditionLeft;
                    Vector3 normal = Vector3.Cross(points[vWrite + 1] - points[vWrite], points[vWrite + 2] - points[vWrite]);
                    WriteNormals(points, normal, nWrite, splitNormals ? 3 : 6);
                    if (splitNormals)
                    {
                        normal = Vector3.Cross(points[vWrite + 4] - points[vWrite + 3], points[vWrite + 5] - points[vWrite + 3]);
                        WriteNormals(points, normal, nWrite + 3, 3);
                    }
                }
            }
        }

        private static void CreatePoint(ref Vector3 point, float angle, float radius, 
                                        Plane plane = Plane.XZ, Object position = null,float rotAngle = 0.0f)
        {
            point.X = (float)(GetCoord(Math.Cos, angle, radius) * Math.Cos(rotAngle));
            point.Y = plane == Plane.XY ? (float)GetCoord(Math.Sin, angle, radius) : 0.0f;
            point.Z = plane == Plane.XZ ? (float)GetCoord(Math.Sin, angle, -radius)
                                        : (float)GetCoord(Math.Cos, angle, -radius) * (float)Math.Sin(rotAngle);
            if(position != null)
            {
                Vector3 translate = (Vector3)position;
                point.X += translate.X;
                point.Y += translate.Y;
                point.Z += translate.Z;
            }
        }

        private static void CreateContour(Vector3[] contour, float w, float h, float z)
        {
            for (int i = 0; i < 2; ++i, z = -z)
            {
                int offset = i * 4;
                contour[offset + 0] = new Vector3(w, -h, z);
                contour[offset + 1] = new Vector3(w, h, z);
                contour[offset + 2] = new Vector3(-w, h, z);
                contour[offset + 3] = new Vector3(-w, -h, z);
            }
        }

        private static void CreateContour(Vector3[] contour, int vertices, float initAngle,
                                           float radius, int cWrite, Plane createPlane = Plane.XZ, 
                                           Object position = null, float rotAngle = 0.0f)
        {
            float sectorAngle = (float)(2 * Math.PI / vertices);
            Vector3 point = new Vector3();
            for(int i = 0; i < vertices; ++i, initAngle += sectorAngle)
            {
                CreatePoint(ref point, initAngle, radius, createPlane, position, rotAngle);
                contour[cWrite + i] = point;
            }
        }

        private static void CreateBuffers(int normalOffset)
        {
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * 3 * points.Length), points, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            if (normalOffset != 0)
            {
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, normalOffset * 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
            }
        }


        public static void FreeBuffers()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            vao = vbo = 0;
        }

        private static void GetSizes(float side, ref float radius, ref float baseHeight, ref float capHeight)
        {
            radius = side / (2 * (float)Math.Sin(36 * Math.PI / 180));
            baseHeight = (float)Math.Sqrt(3) * side / 4;
            capHeight = (float)Math.Sqrt(side * side - radius * radius);
        }

        public static void UpdateModel()
        {
            Matrix4.CreateScale(ref Transforms.Scale, out model);
            Vector3 rotation = Transforms.ConvertToRad();
            model *= Matrix4.CreateRotationX(rotation.X);
            model *= Matrix4.CreateRotationY(rotation.Y);
            model *= Matrix4.CreateRotationZ(rotation.Z);
            model *= Matrix4.CreateTranslation(Transforms.Translation);
        }

        public static void Draw()
        {
            if (vao != 0 && vbo != 0)
            {
                Canvas.SetMatrixUniform(ref model, "model");
                int location = GL.GetUniformLocation(Canvas.Program, "color");
                Color color = Color.LightGray;
                GL.Uniform3(location, (float)color.R / 255, (float)color.G / 255, (float)color.B / 255);
                GL.BindVertexArray(vao);
                GL.DrawArrays(PrimitiveType.Triangles, 0, points.Length);
            }
        }

        public static void Square()
        {
            Vector3[] contour = InitData(12, 4);
            CreateContour(contour, 4, (float)Math.PI / 4, 5, 0, Plane.XY);
            FillContour(contour, 0, 4, 0, 6,Direction.CW);
            CreateBuffers(6);
            Canvas.Invalidate();
        }

        public static void Circle()
        {
            Vector3[] contour = InitData(180, 32);
            CreateContour(contour, 32, 0, 5, 0, Plane.XY);
            FillContour(contour, 0, 32, 0, 90,Direction.CW);
            CreateBuffers(90);
            Canvas.Invalidate();
        }

        public static void Polygon(int sides)
        {
            int vertices = (sides - 2) * 3;
            Vector3[] contour = InitData(vertices * 2, sides);
            CreateContour(contour, sides, 0, 5, 0, Plane.XY);
            FillContour(contour, 0, sides, 0, vertices,Direction.CW);
            CreateBuffers(vertices);
            Canvas.Invalidate();
        }

        public static void Octahedron(float side)
        {
            Vector3[] contour = InitData(48, 4);
            Vector3 offset = new Vector3(0.0f , (float)Math.Sqrt(2) * side / 2, 0.0f);
            CreateContour(contour, 4, 0, offset.Y, 0);
            FillContour(contour, 0, 4, 0, 24, Direction.CCW, offset);
            offset.Y = -offset.Y;
            FillContour(contour, 0, 4, 12, 36, Direction.CW, offset);
            CreateBuffers(24);
            Canvas.Invalidate();
        }

        public static void Tetrahedron(float side)
        {
            Vector3[] contour = InitData(24, 3);
            float radius = side / (float)Math.Sqrt(3);
            Vector3 offset = new Vector3(0.0f, -side * (float)Math.Sqrt((float)2 / 3) / 2, 0.0f);
            CreateContour(contour, 3, 0, radius, 0,Plane.XZ,offset);
            FillContour(contour, 0, 3, 0, 12,Direction.CW);
            offset.Y = -offset.Y;
            FillContour(contour, 0, 3, 3, 15, Direction.CCW, offset);
            CreateBuffers(12);
            Canvas.Invalidate();
        }

        public static void Dodecahedron(float side)
        {
            Vector3[] contour = new Vector3[20];
            int i = 0;
            Action<Vector3, Vector3, Vector3> action = (a, b, c) => contour[i++] = CalculateCenter(a, b, c);
            CreateIkoGeometry(side, 216, action);
            Vector3[] hex = new Vector3[5];
            for(int hexes = 0, cen = 1, up = 0, dwn = 0; hexes < 10; ++hexes, ++cen)
            {
                hex[0] = contour[10 + cen - 1];
                hex[1] = contour[10 + cen % 10];
                hex[2] = contour[10 + (cen + 1) % 10];
                hex[3] = (hexes & 1) == 0 ? contour[(up + 1) % 5] : contour[5 + (dwn + 1) % 5];
                hex[4] = (hexes & 1) == 0 ? contour[up++] : contour[5 + dwn++];
                FillContour(hex, 0, 5, hexes * 9, 108 + hexes * 9);
            }
            FillContour(contour, 0, 5, 10 * 9, 108 + 10 * 9);
            FillContour(contour, 5, 5, 10 * 9 + 9, 108 + 10 * 9 + 9);
            CreateBuffers(108);
            Canvas.Invalidate();
        }

        public static void Ikosahedron(float side)
        {
            CreateIkoGeometry(side, 120, null);
            CreateBuffers(60);
            Canvas.Invalidate();
        }

        public static void Hexahedron(float side)
        {
            Vector3[] contour = InitData(72, 8);
            float radius = (float)Math.Sqrt(2) / 2 * side;
            float baseAngle = 5 * (float)Math.PI / 4;
            Vector3 offset = new Vector3(0.0f, side / 2, 0.0f);
            CreateContour(contour, 4, baseAngle, radius, 0, Plane.XZ, offset);
            FillContour(contour, 0, 4, 0, 36);
            offset.Y = -offset.Y;
            CreateContour(contour, 4, baseAngle, radius, 4, Plane.XZ, offset);
            FillContour(contour, 4, 4, 6, 42);
            CreateSegment(contour, 4, 0, 4, 12, 48);
            CreateBuffers(36);
            Canvas.Invalidate();
        }

        public static void Torus(float radius, int segments = 16, int pointPerSeg = 16)
        {
            int vertPerSeg = pointPerSeg * 3 + pointPerSeg * 3;
            int vertices = segments * vertPerSeg;
            int contourCount = segments * pointPerSeg;
            Vector3[] contour = InitData(vertices * 2, contourCount);
            float segmentRadius = radius / 4;
            float baseAngle = 0;
            float sectorAngle = (float)(2 * Math.PI / segments);
            Vector3 offset = new Vector3();
            for (int i = 0; i <= segments; ++i, baseAngle += sectorAngle)
            {
                offset.X = (float)GetCoord(Math.Cos, baseAngle, radius);
                offset.Z = (float)GetCoord(Math.Sin, baseAngle, -radius);
                if (i != segments)
                    CreateContour(contour, pointPerSeg, 0.0f, segmentRadius, i * pointPerSeg,Plane.XY, offset, baseAngle);
                if (i > 0)
                {
                    int left = (i - 1) * pointPerSeg;
                    int right = left + pointPerSeg;
                    int vWrite = (i - 1) * vertPerSeg;
                    int nWrite = vertices + (i - 1) * vertPerSeg;
                    if (i != segments)
                        CreateSegment(contour, pointPerSeg, left, right, vWrite, nWrite);
                    else
                        CreateSegment(contour, pointPerSeg, left, 0, vWrite, nWrite);
                }
            }
            CreateBuffers(vertices);
            Canvas.Invalidate();
        }
        
        public static void Helix(float radius, int segCount = 16, int pointPerSeg = 16, int coilCount = 4)
        {
            int vertPerSeg = pointPerSeg * 3 + pointPerSeg * 3;
            int maxVertices = segCount * vertPerSeg;
            int vertices = maxVertices * coilCount - vertPerSeg;
            int capVertices = (pointPerSeg - 2) * 3 * 2;
            int maxPoints = pointPerSeg * segCount;
            int contourCount = maxPoints * coilCount;
            int totalVertices = vertices * 2 + capVertices * 2;
            Vector3[] contour = InitData(totalVertices, contourCount);
            float segmentRadius = radius / 4;
            float baseAngle = 0;
            float yOffset = segmentRadius * 2 / (segCount + 1) + 0.05f;
            float yOffsetBegin = -(yOffset * segCount * coilCount / 2);
            float step = yOffset * segCount;
            float sectorAngle = (float)(2 * Math.PI / segCount);
            Vector3 offset = new Vector3();
            for (int i = 0; i < coilCount; ++i)
            {
                for (int j = 0; j < segCount; ++j, baseAngle += sectorAngle)
                {
                    offset.X = (float)GetCoord(Math.Cos, baseAngle, radius);
                    offset.Y = yOffsetBegin + i * step +  j * yOffset;
                    offset.Z = (float)GetCoord(Math.Sin, baseAngle, -radius);
                    int right = i * maxPoints + j * pointPerSeg;
                    CreateContour(contour, pointPerSeg, 0.0f, segmentRadius, right, Plane.XY, offset, baseAngle);
                    if (j > 0 || i > 0)
                    {
                        int left = right - pointPerSeg;
                        int vWrite = i * maxVertices + (j-1) * vertPerSeg;
                        int nWrite = vertices + capVertices + vWrite;
                        CreateSegment(contour, pointPerSeg, left, right, vWrite, nWrite);
                    }
                }
            }
            FillContour(contour, 0, pointPerSeg, vertices, totalVertices - capVertices, Direction.CW);
            FillContour(contour, contourCount - pointPerSeg, pointPerSeg, 
                       vertices + capVertices / 2, totalVertices - capVertices / 2, Direction.CCW);
            CreateBuffers(vertices + capVertices);
            Canvas.Invalidate();
        }

        public static void Sphere(float radius, int segments = 16, int pointPerSeg = 16)
        {
            segments += 2;
            int trnPerSegment = pointPerSeg * 2;
            int vPerSegment = trnPerSegment * 3;
            int trnPolus = pointPerSeg * 2;
            int totalVertex = (trnPerSegment * (segments - 2) + trnPolus) * 3;
            int contourCount = pointPerSeg * (segments - 2);
            float baseAngle = 0;
            float sectorAngle = (float)Math.PI / (segments - 2);
            Vector3[] contour = InitData(totalVertex * 2, contourCount);
            Vector3 offset = new Vector3();
            Vector3[] p = new Vector3[2];
            for(int i = 0, j = 0; i < segments; ++i, baseAngle+=sectorAngle)
            {
                offset.Y = (float)GetCoord(Math.Cos, baseAngle, radius);
                float z = (float)GetCoord(Math.Sin, baseAngle, radius);
                if (i != 0 && i != segments - 1)
                {
                    CreateContour(contour, pointPerSeg, (float)-Math.PI / 2, z, (i - 1) * pointPerSeg, Plane.XZ, offset);
                    if (i > 1)
                        CreateSegment(contour, pointPerSeg, (i - 2) * pointPerSeg, (i - 1) * pointPerSeg,
                                     (i - 2) * vPerSegment, totalVertex + (i - 2) * vPerSegment);
                }
                else
                    p[j++] = new Vector3(0.0f, offset.Y, z);
            }
            FillContour(contour, 0, pointPerSeg, totalVertex - trnPolus * 3, totalVertex * 2 - trnPolus * 3,Direction.CCW, p[0]);
            FillContour(contour, contourCount - pointPerSeg, pointPerSeg, totalVertex - (trnPolus / 2)  * 3,
                        totalVertex * 2 - (trnPolus / 2) * 3, Direction.CCW,p[1]);
            CreateBuffers(totalVertex);
            Canvas.Invalidate();
        }

        public static void Conus(float radius, int segments = 16)
        {
            int cVertices = segments * 3;
            int bVertices = (segments - 2) * 3;
            int totalVertices = cVertices + bVertices;
            Vector3[] contour = InitData(totalVertices * 2, 32);
            Vector3 top = new Vector3(0, -radius, 0);
            CreateContour(contour, segments, 0, radius, 0,Plane.XZ,top);
            top.Y = -top.Y;
            FillContour(contour, 0, segments, 0, totalVertices, Direction.CCW, top);
            FillContour(contour, 0, segments, cVertices, totalVertices + cVertices, Direction.CCW);
            CreateBuffers(totalVertices);
            Canvas.Invalidate();
        }

        public static void Parallelepiped(int width, int height, int depth)
        {
            Vector3[] contour = InitData(72, 8);
            CreateContour(contour, (float)width/2, (float)height/2, (float)depth/2);
            FillContour(contour, 0, 4, 0, 36);
            FillContour(contour, 4, 4, 6, 42,Direction.CW);
            CreateSegment(contour, 4, 0, 4, 12, 48);
            CreateBuffers(36);
            Canvas.Invalidate();
        }

        public static void Pyramid(float side)
        {
            Vector3[] contour = InitData(36, 4);
            Vector3 offset = new Vector3(0.0f, -(float)Math.Sqrt(2) * side / 4, 0.0f);
            CreateContour(contour, 4, 0, -offset.Y * 2 , 0, Plane.XZ, offset);
            offset.Y = -offset.Y;
            FillContour(contour, 0, 4, 0, 18, Direction.CCW, offset);  
            FillContour(contour, 0, 4, 12, 30);
            CreateBuffers(18);
            Canvas.Invalidate();
        }

        public static void TruncatedPyramid(float side, float k = 0.5f)
        {
            float bigRadius = (float)Math.Sqrt(2) * side / 2;
            float smallRadius = (float)Math.Sqrt(2) * (side * (1 - k)) / 2;
            Vector3[] contour = InitData(72, 8);
            Vector3 offset = new Vector3(0.0f, bigRadius * k / 2, 0.0f);
            CreateContour(contour, 4, 0, smallRadius, 0, Plane.XZ, offset);
            offset.Y = -offset.Y;
            CreateContour(contour, 4, 0, bigRadius, 4, Plane.XZ, offset);
            FillContour(contour, 0, 4, 0, 36);
            FillContour(contour, 4, 4, 6, 42, Direction.CW);
            CreateSegment(contour, 4, 0, 4, 12, 48);
            CreateBuffers(36);
            Canvas.Invalidate();
        }

        public static void Cylinder(float upRadius, float dwnRadius, int segments = 32, int height = 5)
        {
            int capVertices = (segments - 2) * 6;
            int baseVertices = segments * 6;
            Vector3[] contour = InitData((capVertices + baseVertices) * 2, segments * 2);
            Vector3 offset = new Vector3(0.0f, height / 2, 0.0f);
            CreateContour(contour, segments, 0, upRadius, 0, Plane.XZ, offset);
            offset.Y = -offset.Y;
            CreateContour(contour, segments, 0, dwnRadius, segments, Plane.XZ, offset);
            FillContour(contour, 0, segments, 0, baseVertices + capVertices);
            FillContour(contour, segments, segments, capVertices / 2, baseVertices + capVertices + capVertices / 2, Direction.CW);
            CreateSegment(contour, segments, 0, segments, capVertices, (capVertices + baseVertices) * 2 - baseVertices);
            CreateBuffers(capVertices + baseVertices);
            Canvas.Invalidate();
        }
    }

    public static class Transforms
    {
        private static Vector3 scale;
        private static Vector3 rotation;
        private static Vector3 translation;
        public enum Axis
        {
            X,
            Y,
            Z
        }
        public enum Transform
        {
            Scale,
            Rotation,
            Translation
        }
        private static void SetValue(ref Vector3 vec, Axis axis, float value)
        {
            switch (axis)
            {
                case Axis.X: vec.X = value; break;
                case Axis.Y: vec.Y = value; break;
                default: vec.Z = value; break;
            }
        }
        public static void ResetTransforms()
        {
            scale.X = scale.Y = scale.Z = 1.0f;
            rotation.X = rotation.Y = rotation.Z = 0.0f;
            translation.X = translation.Y = translation.Z = 0.0f;
        }
        public static void CreateTransforms()
        {
            scale = new Vector3(1.0f, 1.0f, 1.0f);
            rotation = new Vector3(0.0f, 0.0f, 0.0f);
            translation = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public static Vector3 ConvertToRad() => new Vector3(rotation.X * (float)Math.PI / 180, rotation.Y * (float)Math.PI / 180, 
                                                                                               rotation.Z * (float)Math.PI / 180);
        public static void SetTranform(Transform transform, Axis axis, float value)
        {
            if (transform == Transform.Scale)
                SetValue(ref scale, axis, value);
            else if (transform == Transform.Translation)
                SetValue(ref translation, axis, value);
            else
                SetValue(ref rotation, axis, value);
        }
        public static ref Vector3 Scale { get { return ref scale; } }
        public static ref Vector3 Rotation { get { return ref rotation; } }
        public static ref Vector3 Translation { get { return ref translation; } }
    }


    public static class Canvas
    {
        private static bool isLoaded;
        private static GLControl canvas;
        private static Size size;
        private static Vector3 eyePos;
        private static Matrix4 view;
        private static Matrix4 projection;
        private static int program;
        private static int vertexShader;
        private static int fragmentShader;
        private static void LoadShader(string source, ShaderType type)
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
            program = GL.CreateProgram();
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
            canvas = control;
            eyePos = new Vector3(0.0f, 0.0f, 10.0f);
            Size = control.Size;
            CreateProgram();
            view = Matrix4.LookAt(eyePos, -Vector3.UnitZ, Vector3.UnitY);
            isLoaded = true;
        }
        public static void SwapBuffers() => canvas.SwapBuffers();
        public static void Invalidate() => canvas.Invalidate();
        public static void UpdateCanvas()
        {
            GL.Viewport(0, 0, Size.Width, Size.Height);
            projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, (float)Canvas.Size.Width / Canvas.Size.Height, 0.1f, 50.0f);
        }
        public static void Render()
        {
            GL.UseProgram(program);
            SetMatrixUniform(ref view, "view");
            SetMatrixUniform(ref projection, "projection");
            int location = GL.GetUniformLocation(Canvas.Program, "eyePos");
            GL.Uniform3(location, ref eyePos);
        }
        public static void FreeCanvas()
        {
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteProgram(program);
        }
        public static int Program { get { return program; } }
        public static Size Size { get { return size; } set { size = value; } }
        public static bool IsLoaded { get { return isLoaded; } set { isLoaded = value; } }
    }
}
