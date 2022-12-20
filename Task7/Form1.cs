using System;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
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

namespace Task7
{
    public partial class Form1 : Form
    {
        bool isClose = false;
        Model house;
        Model ground;
        Model sky;
        private readonly Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000 / 60;
            timer.Tick += OnTimerEvent;
            timer.Start();
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            if (isClose)
                this.Close();
            Canvas.UpdateMouse();
            Canvas.UpdatePosition();
            Canvas.UpdateCanvas();
            sky.SetPosition(ref Canvas.EyePos);
            Canvas.Invalidate();
        }

        private void OnSize(object sender, EventArgs e)
        {
            Canvas.Size = canvas.Size;
            Canvas.UpdateCanvas();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Canvas.Render();
            ground.Draw();
            house.Draw();
            Canvas.Render(false);
            sky.Draw();
            Canvas.SwapBuffers();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Canvas.CreateCanvas(canvas);
            Cursor.Position = new Point(Left + Width / 2, Top + Height / 2);
            GL.ClearColor(Color.Cyan);
            GL.Enable(EnableCap.DepthTest);
            house = new Model("cottage.obj", "cottage.jpg");
            sky = new Model("skysphere.obj", "sky.jpg", 2f, 2f);
            ground = new Model("ground.obj", "grass.jpg", 128.0f, 128.0f);
            sky.SetPosition(ref Canvas.EyePos);
            this.WindowState = FormWindowState.Maximized;
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            ground.FreeBuffers();
            sky.FreeBuffers();
            house.FreeBuffers();
            Canvas.FreeCanvas();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Canvas.MouseLoc.X = e.Location.X;
            Canvas.MouseLoc.Y = e.Location.Y;
        }

        private void OnKeyDown(object sender, KeyEventArgs e) => Canvas.UpdateKeys(e.KeyCode, ref isClose, true);
        private void OnKeyUp(object sender, KeyEventArgs e) => Canvas.UpdateKeys(e.KeyCode, ref isClose, false);

    }
    public static class Canvas
    {
        private readonly static bool[] keyStates = new bool[4];
        private static PolygonMode mode = PolygonMode.Line;
        private static GLControl canvas;
        private static Point mouseLoc;
        private static Size size;
        private static Vector3 eyePos;
        private static Vector3 front;
        private static Point lastMouse;
        private static Vector2 yawPitch;
        private static Matrix4 view;
        private static Matrix4 projection;
        private static bool firstMouse = true;
        private static int modelProg;
        private static int modelVShader;
        private static int modelFShader;
        private static int skyProg;
        private static int skyVShader;
        private static int skyFShader;

        private static void LoadShader(ref int shader, string source, ShaderType type)
        {
            if (File.Exists(source))
            {
                shader = GL.CreateShader(type);
                string data;
                using (StreamReader reader = new StreamReader(source))
                    data = reader.ReadToEnd();
                GL.ShaderSource(shader, data);
                GL.CompileShader(shader);
            }
        }

        private static void CreateProgram(ref int progShader, ref int vShader, ref int fShader, string vSource, string fSource)
        {
            progShader = GL.CreateProgram();
            LoadShader(ref vShader, "shaders/" + vSource, ShaderType.VertexShader);
            LoadShader(ref fShader, "shaders/" + fSource, ShaderType.FragmentShader);
            GL.AttachShader(progShader, vShader);
            GL.AttachShader(progShader, fShader);
            GL.LinkProgram(progShader);
        }

        private static float Rad(float angle) => angle * (float)Math.PI / 180;

        public static void SetMatrixUniform(ref Matrix4 matrix, string name, bool isModelProg = true)
        {
            int location = GL.GetUniformLocation(isModelProg ? modelProg : skyProg, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }
        public static void CreateCanvas(GLControl control)
        {
            canvas = control;
            eyePos = new Vector3(0.0f, 4.0f, 30.0f);
            front = -Vector3.UnitZ;
            Size = control.Size;
            CreateProgram(ref modelProg, ref modelVShader, ref modelFShader, "vertex.vs", "fragment.fs");
            CreateProgram(ref skyProg, ref skyVShader, ref skyFShader, "skyvertex.vs", "skyfragment.fs");
            UpdateCanvas();
        }
        public static void SwapBuffers() => canvas.SwapBuffers();
        public static void Invalidate() => canvas.Invalidate();
        public static void UpdateMouse()
        {
            const float mouseSens = 1.0f;
            if (firstMouse)
            {
                yawPitch.X = 90.0f;
                lastMouse.X = mouseLoc.X;
                lastMouse.Y = mouseLoc.Y;
                firstMouse = false;
            }
            if (mouseLoc.X != lastMouse.X || mouseLoc.Y != lastMouse.Y)
            {
                float xOffset = lastMouse.X - mouseLoc.X;
                float yOffset = lastMouse.Y - mouseLoc.Y;
                lastMouse.X = mouseLoc.X;
                lastMouse.Y = mouseLoc.Y;
                xOffset *= mouseSens;
                yOffset *= mouseSens;
                yawPitch.X += xOffset;
                yawPitch.Y += yOffset;
                yawPitch.Y = yawPitch.Y > 89.0f ? 89.0f : yawPitch.Y;
                yawPitch.Y = yawPitch.Y < -89.0f ? -89.0f : yawPitch.Y;
                front.X = (float)Math.Cos(Rad(yawPitch.X)) * (float)Math.Cos(Rad(yawPitch.Y));
                front.Y = (float)Math.Sin(Rad(yawPitch.Y));
                front.Z = -(float)Math.Sin(Rad(yawPitch.X)) * (float)Math.Cos(Rad(yawPitch.Y));
                front.Normalize();
            }
        }

        public static void UpdateKeys(Keys key, ref bool isClose, bool isKeyDown = true)
        {
            if (key == Keys.W)
                keyStates[0] = isKeyDown;
            else if (key == Keys.S)
                keyStates[1] = isKeyDown;
            else if (key == Keys.D)
                keyStates[2] = isKeyDown;
            else if (key == Keys.A)
                keyStates[3] = isKeyDown;
            else if (key == Keys.Q && isKeyDown)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, mode);
                mode = mode == PolygonMode.Line ? PolygonMode.Fill : PolygonMode.Line;
            }
            else if (key == Keys.Escape && isKeyDown)
                isClose = true;
        }

        public static void UpdatePosition()
        {
            const float cameraSpeed = 0.25f;
            if (keyStates[0] || keyStates[1])
            {
                Vector3 pos = new Vector3(front.X, 0.0f, front.Z);
                pos *= cameraSpeed * (1 - Math.Abs(front.Y));
                eyePos = keyStates[0] ? eyePos + pos : eyePos - pos;

            }
            if (keyStates[2] || keyStates[3])
            {
                Vector3 offset = Vector3.Cross(front, Vector3.UnitY);
                offset.Normalize();
                offset *= cameraSpeed * (1 - Math.Abs(front.Y));
                eyePos = keyStates[2] ? eyePos + offset : eyePos - offset;
            }
        }

        public static void UpdateCanvas()
        {
            GL.Viewport(0, 0, Size.Width, Size.Height);
            projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, (float)Canvas.Size.Width / Canvas.Size.Height, 0.1f, 200.0f);
            view = Matrix4.LookAt(eyePos, front + eyePos, Vector3.UnitY);
        }

        public static void Render(bool isModelProg = true)
        {
            int prog = isModelProg ? modelProg : skyProg;
            GL.UseProgram(prog);
            SetMatrixUniform(ref view, "view", isModelProg);
            SetMatrixUniform(ref projection, "projection", isModelProg);
        }
        public static void FreeCanvas()
        {
            GL.DeleteShader(modelVShader);
            GL.DeleteShader(modelFShader);
            GL.DeleteShader(skyVShader);
            GL.DeleteShader(skyFShader);
            GL.DetachShader(modelProg, modelVShader);
            GL.DetachShader(modelProg, modelFShader);
            GL.DeleteProgram(modelProg);
        }
        public static Size Size { get { return size; } set { size = value; } }
        public static ref Point MouseLoc { get { return ref mouseLoc; } }

        public static ref Vector3 EyePos { get { return ref eyePos; } }
    }

    public class Model
    {
        private Matrix4 model;
        private readonly float[] points;
        private readonly uint[] indices;
        private int texture;
        private int vao;
        private int vbo;
        private int ebo;

        private List<List<float[]>> InitParamsList()
        {
            List<List<float[]>> data = new List<List<float[]>>();
            for (int i = 0; i < 3; ++i)
                data.Add(new List<float[]>());//vertices,uvs,normals
            return data;
        }

        private int StartsWith(ref string line)
        {
            int pos = line.IndexOf(' ');
            if (line != null)
            {
                string[] commands = { "v", "vt", "vn", "f" };
                for (int i = 0; i < commands.Length; ++i)
                    if (line.StartsWith(commands[i]) && commands[i].Length == pos)
                        return i;
            }
            return -1;
        }

        private void WriteCoords(List<float[]> list, ref string line, ref int index, bool flipV = true, float tileU = 1.0f, float tileV = 1.0f)
        {
            string[] lineCoords = line.Split(' ');
            float[] coords = new float[lineCoords.Length - 1];
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            for (int i = 1; i < lineCoords.Length; ++i)
            {
                float coord = float.Parse(lineCoords[i], NumberStyles.Any, ci);
                if (index != 1)
                    coords[i - 1] = coord;
                else
                    coords[i - 1] = i == 2 && flipV ? (1 - coord) * tileV : coord * (i == 1 ? tileU : tileV);
            }
            list.Add(coords);
        }

        private void ParseFace(List<List<float[]>> data, Hashtable faces, List<uint> indices,
                               List<float[]> points, ref string line)
        {
            string[] lineCoords = line.Split(' ');
            for (int i = 1; i < lineCoords.Length; ++i)
            {
                if (!faces.Contains(lineCoords[i]))
                {
                    float[] point = new float[8];
                    string[] coords = lineCoords[i].Split('/');
                    int offset = 0;
                    for (int j = 0; j < 3; ++j)
                    {
                        int ind = Int32.Parse(coords[j]);
                        Array.Copy(data[j][ind - 1], 0, point, offset, j == 1 ? 2 : 3);
                        offset += j == 1 ? 2 : 3;
                    }
                    points.Add(point);
                    faces[lineCoords[i]] = (uint)points.Count - 1;
                }
                indices.Add((uint)faces[lineCoords[i]]);
                if (i > 3)
                {
                    indices.Add((uint)faces[lineCoords[1]]);
                    indices.Add((uint)faces[lineCoords[i - 1]]);
                }
            }
        }

        private void LoadTexture(string fileName)
        {
            if (File.Exists(fileName))
            {
                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                Bitmap bitmap = new Bitmap(fileName);
                BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                byte[] bytes = new byte[data.Height * data.Stride];
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                bitmap.UnlockBits(data);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, bitmap.Width, bitmap.Height,
                              0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, bytes);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
        }

        public Model(string fileName, string texture, float tileU = 1.0f, float tileV = 1.0f)
        {
            string fullPath = "models/" + fileName;
            if (File.Exists(fullPath))
            {
                model = Matrix4.Identity;
                List<List<float[]>> data = InitParamsList();
                List<uint> indices = new List<uint>();
                List<float[]> points = new List<float[]>();
                Hashtable faces = new Hashtable();
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        int index = StartsWith(ref line);
                        if (index == -1)
                            continue;
                        if (index < 3)
                            WriteCoords(data[index], ref line, ref index, true, tileU, tileV);
                        else
                            ParseFace(data, faces, indices, points, ref line);
                    }
                }
                this.points = points.SelectMany(inner => inner).ToArray();
                int ab = this.points.Length;
                this.indices = indices.ToArray();
                vao = GL.GenVertexArray();
                vbo = GL.GenBuffer();
                ebo = GL.GenBuffer();
                GL.BindVertexArray(vao);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * this.points.Length), this.points, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                GL.BufferData<uint>(BufferTarget.ElementArrayBuffer, new IntPtr(this.indices.Length * sizeof(uint)), this.indices, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
                GL.EnableVertexAttribArray(2);
                LoadTexture("textures/" + texture);
            }
        }
        public void SetPosition(ref Vector3 position) => model = Matrix4.CreateTranslation(position);

        public void Draw(bool isModelProg = true)
        {
            if (vao != 0 && vbo != 0 && ebo != 0)
            {
                Canvas.SetMatrixUniform(ref model, "model", isModelProg);
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.BindVertexArray(vao);
                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }
        }

        public void FreeBuffers()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteTexture(texture);
            vao = vbo = ebo = 0;
        }
    }
}
