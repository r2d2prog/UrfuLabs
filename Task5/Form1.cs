using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void OnLoad(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lFileName.Text = openFileDialog1.FileName;
                sourceBitmap.SizeMode = PictureBoxSizeMode.StretchImage;
                sourceBitmap.Load(lFileName.Text);
                Bitmap bitmap = new Bitmap(sourceBitmap.Image,sourceBitmap.Size);
                for(int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        if (y == 0)
                        {
                            string xLabel = x.ToString();
                            sourceData.Columns.Add(xLabel, "Колонка "+ xLabel);
                        }
                        if (x == 0)
                        {
                            sourceData.Rows.Add();
                            sourceData.Rows[y].HeaderCell.Value = "Ряд " + y.ToString();
                        }
                        Color color = bitmap.GetPixel(x, y);
                        int brightness = (int)Math.Round(color.GetBrightness() * 255);
                        string cString = color.R.ToString() + "," + color.G.ToString() + "," + 
                                         color.B.ToString() + "," + color.A.ToString() + "," + brightness.ToString();
                        sourceData[x, y].Value = cString;
                    }
                }
            }
        }

        private void OnReset(object sender, MouseEventArgs e)
        {
            if(sourceBitmap.Image != null)
            {
                sourceData.RowCount = sourceData.ColumnCount = 0;
                sourceBitmap.Image = null;
                destBitmap.Image = null;
            }
        }

        private void OnChange(object sender, EventArgs e)
        {
            if (sourceBitmap.Image != null)
            {
                Bitmap bitmap = new Bitmap(sourceBitmap.Image, sourceBitmap.Size);
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        int color  = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                        bitmap.SetPixel(x, y, Color.FromArgb(pixel.A,color,color,color));
                    }
                }
                destBitmap.Image = bitmap;
            }
        }
    }
}
