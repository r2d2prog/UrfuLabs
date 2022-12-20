using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void WriteData(Func<double,double> fun,int index,float range = 1, float freq = 1,
                                float start = -10, float end = 10,float step = 1.0f)
        {
            chart.Series[index].Points.Clear();
            while (start < end)
            {
                double y = range*fun(freq*start);
                chart.Series[index].Points.AddXY(start, y);
                start += step;
            }
        }
        
        private void OnLoad(object sender, EventArgs e)
        {
            Func<double, double> fun = (p) =>
            {
                double offset = p > 0 ? -0.01 : 0.01;
                return Math.Abs(p - Math.PI / 2) < 0.0001f ? Math.Tan(p+offset): Math.Tan(p);
            };
            WriteData(Math.Sin, 0);
            WriteData(Math.Cos, 1);
        }

        private void OnCheckChange(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int index = cb == sinCheck ? 0 : 1;
            chart.Series[index].Enabled = cb.Checked;
        }

        private void OnUpdateChart(object sender, EventArgs e)
        {
            float value;
            TextBox txt = sender as TextBox;
            if (float.TryParse(txt.Text,out value))
            {
                TextBox[] tb = { textBox1, textBox2, textBox3, textBox4 };
                float[] values = new float[4];
                for (int i = 0; i < 4; ++i)
                {
                    if (!float.TryParse(tb[i].Text, out value))
                        return;
                    values[i] = value;
                }
                WriteData(Math.Sin, 0, values[2], values[3], values[0], values[1]);
                WriteData(Math.Cos, 1, values[2], values[3], values[0], values[1]);
            }
        }
    }
}
