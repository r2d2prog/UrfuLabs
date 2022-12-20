namespace Leafs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.recT = new System.Windows.Forms.TrackBar();
            this.recV = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.xT = new System.Windows.Forms.TrackBar();
            this.xV = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dT = new System.Windows.Forms.TrackBar();
            this.dV = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cT = new System.Windows.Forms.TrackBar();
            this.cV = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bT = new System.Windows.Forms.TrackBar();
            this.bV = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.aT = new System.Windows.Forms.TrackBar();
            this.aV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aT)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.75F));
            this.tableLayoutPanel1.Controls.Add(this.canvas, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 542);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(213, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(618, 536);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.recT);
            this.groupBox1.Controls.Add(this.recV);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.xT);
            this.groupBox1.Controls.Add(this.xV);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dT);
            this.groupBox1.Controls.Add(this.dV);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cT);
            this.groupBox1.Controls.Add(this.cV);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.bT);
            this.groupBox1.Controls.Add(this.bV);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.aT);
            this.groupBox1.Controls.Add(this.aV);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 536);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление:";
            // 
            // recT
            // 
            this.recT.Location = new System.Drawing.Point(39, 480);
            this.recT.Maximum = 14;
            this.recT.Minimum = 1;
            this.recT.Name = "recT";
            this.recT.Size = new System.Drawing.Size(104, 45);
            this.recT.TabIndex = 18;
            this.recT.Value = 12;
            this.recT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // recV
            // 
            this.recV.AutoSize = true;
            this.recV.Location = new System.Drawing.Point(108, 452);
            this.recV.Name = "recV";
            this.recV.Size = new System.Drawing.Size(19, 13);
            this.recV.TabIndex = 17;
            this.recV.Text = "12";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 452);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Глубина рекурсии:";
            // 
            // xT
            // 
            this.xT.Location = new System.Drawing.Point(39, 404);
            this.xT.Name = "xT";
            this.xT.Size = new System.Drawing.Size(104, 45);
            this.xT.TabIndex = 15;
            this.xT.Value = 10;
            this.xT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // xV
            // 
            this.xV.AutoSize = true;
            this.xV.Location = new System.Drawing.Point(108, 376);
            this.xV.Name = "xV";
            this.xV.Size = new System.Drawing.Size(22, 13);
            this.xV.TabIndex = 14;
            this.xV.Text = "1,0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(81, 376);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "x0:";
            // 
            // dT
            // 
            this.dT.Location = new System.Drawing.Point(39, 328);
            this.dT.Minimum = 1;
            this.dT.Name = "dT";
            this.dT.Size = new System.Drawing.Size(104, 45);
            this.dT.TabIndex = 12;
            this.dT.Value = 3;
            this.dT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // dV
            // 
            this.dV.AutoSize = true;
            this.dV.Location = new System.Drawing.Point(108, 300);
            this.dV.Name = "dV";
            this.dV.Size = new System.Drawing.Size(22, 13);
            this.dV.TabIndex = 11;
            this.dV.Text = "0,3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 300);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Коэф. d:";
            // 
            // cT
            // 
            this.cT.Location = new System.Drawing.Point(39, 252);
            this.cT.Minimum = 1;
            this.cT.Name = "cT";
            this.cT.Size = new System.Drawing.Size(104, 45);
            this.cT.TabIndex = 9;
            this.cT.Value = 5;
            this.cT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // cV
            // 
            this.cV.AutoSize = true;
            this.cV.Location = new System.Drawing.Point(108, 224);
            this.cV.Name = "cV";
            this.cV.Size = new System.Drawing.Size(22, 13);
            this.cV.TabIndex = 8;
            this.cV.Text = "0,5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Коэф. c:";
            // 
            // bT
            // 
            this.bT.Location = new System.Drawing.Point(39, 176);
            this.bT.Minimum = 1;
            this.bT.Name = "bT";
            this.bT.Size = new System.Drawing.Size(104, 45);
            this.bT.TabIndex = 6;
            this.bT.Value = 3;
            this.bT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // bV
            // 
            this.bV.AutoSize = true;
            this.bV.Location = new System.Drawing.Point(108, 148);
            this.bV.Name = "bV";
            this.bV.Size = new System.Drawing.Size(22, 13);
            this.bV.TabIndex = 5;
            this.bV.Text = "0,3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Коэф. b:";
            // 
            // aT
            // 
            this.aT.Location = new System.Drawing.Point(39, 100);
            this.aT.Minimum = 1;
            this.aT.Name = "aT";
            this.aT.Size = new System.Drawing.Size(104, 45);
            this.aT.TabIndex = 3;
            this.aT.Value = 5;
            this.aT.ValueChanged += new System.EventHandler(this.OnChange);
            // 
            // aV
            // 
            this.aV.AutoSize = true;
            this.aV.Location = new System.Drawing.Point(108, 72);
            this.aV.Name = "aV";
            this.aV.Size = new System.Drawing.Size(22, 13);
            this.aV.TabIndex = 2;
            this.aV.Text = "0,5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Коэф. a:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Рисовать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnDraw);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 542);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Рисование листьев";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar xT;
        private System.Windows.Forms.Label xV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar dT;
        private System.Windows.Forms.Label dV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar cT;
        private System.Windows.Forms.Label cV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar bT;
        private System.Windows.Forms.Label bV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar aT;
        private System.Windows.Forms.Label aV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar recT;
        private System.Windows.Forms.Label recV;
        private System.Windows.Forms.Label label12;
    }
}

