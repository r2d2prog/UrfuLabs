namespace Task2
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
            this.canvas = new OpenTK.GLControl();
            this.generalLayout = new System.Windows.Forms.TableLayoutPanel();
            this.controls = new System.Windows.Forms.GroupBox();
            this.polygons = new System.Windows.Forms.GroupBox();
            this.funChoice = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.animate = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uniformScale = new System.Windows.Forms.CheckBox();
            this.yScaleValue = new System.Windows.Forms.Label();
            this.xScaleValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.yScales = new System.Windows.Forms.TrackBar();
            this.xScales = new System.Windows.Forms.TrackBar();
            this.rotation = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.aroundSelf = new System.Windows.Forms.CheckBox();
            this.coordsText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.isClockWise = new System.Windows.Forms.CheckBox();
            this.angleValue = new System.Windows.Forms.Label();
            this.anglesValues = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.faces = new System.Windows.Forms.TrackBar();
            this.faceValue = new System.Windows.Forms.Label();
            this.facesLabel = new System.Windows.Forms.Label();
            this.circle = new System.Windows.Forms.Button();
            this.regular = new System.Windows.Forms.Button();
            this.square = new System.Windows.Forms.Button();
            this.slaveLayout = new System.Windows.Forms.TableLayoutPanel();
            this.messagesBox = new System.Windows.Forms.GroupBox();
            this.messages = new System.Windows.Forms.TextBox();
            this.generalLayout.SuspendLayout();
            this.controls.SuspendLayout();
            this.polygons.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yScales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xScales)).BeginInit();
            this.rotation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anglesValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faces)).BeginInit();
            this.slaveLayout.SuspendLayout();
            this.messagesBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(3, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(637, 496);
            this.canvas.TabIndex = 0;
            this.canvas.VSync = false;
            this.canvas.Load += new System.EventHandler(this.OnLoad);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectRotationPoint);
            this.canvas.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // generalLayout
            // 
            this.generalLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generalLayout.ColumnCount = 2;
            this.generalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.generalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.generalLayout.Controls.Add(this.controls, 0, 0);
            this.generalLayout.Controls.Add(this.slaveLayout, 1, 0);
            this.generalLayout.Location = new System.Drawing.Point(10, 10);
            this.generalLayout.Name = "generalLayout";
            this.generalLayout.RowCount = 1;
            this.generalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.generalLayout.Size = new System.Drawing.Size(927, 608);
            this.generalLayout.TabIndex = 1;
            // 
            // controls
            // 
            this.controls.Controls.Add(this.polygons);
            this.controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controls.Location = new System.Drawing.Point(3, 3);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(272, 602);
            this.controls.TabIndex = 2;
            this.controls.TabStop = false;
            this.controls.Text = "Управление:";
            // 
            // polygons
            // 
            this.polygons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.polygons.Controls.Add(this.funChoice);
            this.polygons.Controls.Add(this.label8);
            this.polygons.Controls.Add(this.animate);
            this.polygons.Controls.Add(this.groupBox2);
            this.polygons.Controls.Add(this.rotation);
            this.polygons.Controls.Add(this.faces);
            this.polygons.Controls.Add(this.faceValue);
            this.polygons.Controls.Add(this.facesLabel);
            this.polygons.Controls.Add(this.circle);
            this.polygons.Controls.Add(this.regular);
            this.polygons.Controls.Add(this.square);
            this.polygons.Location = new System.Drawing.Point(10, 20);
            this.polygons.Name = "polygons";
            this.polygons.Size = new System.Drawing.Size(250, 570);
            this.polygons.TabIndex = 0;
            this.polygons.TabStop = false;
            this.polygons.Text = "Многоугольники:";
            // 
            // funChoice
            // 
            this.funChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.funChoice.FormattingEnabled = true;
            this.funChoice.Items.AddRange(new object[] {
            "y = 100*sin (x/30)",
            "y = 1/x",
            "y = 1/50*x^2"});
            this.funChoice.Location = new System.Drawing.Point(140, 535);
            this.funChoice.Name = "funChoice";
            this.funChoice.Size = new System.Drawing.Size(97, 21);
            this.funChoice.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(159, 519);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Функция:";
            // 
            // animate
            // 
            this.animate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animate.AutoSize = true;
            this.animate.Location = new System.Drawing.Point(12, 530);
            this.animate.Name = "animate";
            this.animate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.animate.Size = new System.Drawing.Size(94, 17);
            this.animate.TabIndex = 8;
            this.animate.Text = "Анимировать";
            this.animate.UseVisualStyleBackColor = true;
            this.animate.Click += new System.EventHandler(this.AnimatePolygon);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.uniformScale);
            this.groupBox2.Controls.Add(this.yScaleValue);
            this.groupBox2.Controls.Add(this.xScaleValue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.yScales);
            this.groupBox2.Controls.Add(this.xScales);
            this.groupBox2.Location = new System.Drawing.Point(6, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 134);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Масштабирование:";
            // 
            // uniformScale
            // 
            this.uniformScale.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.uniformScale.AutoSize = true;
            this.uniformScale.Location = new System.Drawing.Point(75, 31);
            this.uniformScale.Name = "uniformScale";
            this.uniformScale.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.uniformScale.Size = new System.Drawing.Size(82, 17);
            this.uniformScale.TabIndex = 14;
            this.uniformScale.Text = "Одинаково";
            this.uniformScale.UseVisualStyleBackColor = true;
            this.uniformScale.Click += new System.EventHandler(this.ScaleUniformChecked);
            // 
            // yScaleValue
            // 
            this.yScaleValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yScaleValue.AutoSize = true;
            this.yScaleValue.Location = new System.Drawing.Point(187, 59);
            this.yScaleValue.Name = "yScaleValue";
            this.yScaleValue.Size = new System.Drawing.Size(13, 13);
            this.yScaleValue.TabIndex = 13;
            this.yScaleValue.Text = "1";
            // 
            // xScaleValue
            // 
            this.xScaleValue.AutoSize = true;
            this.xScaleValue.Location = new System.Drawing.Point(71, 59);
            this.xScaleValue.Name = "xScaleValue";
            this.xScaleValue.Size = new System.Drawing.Size(13, 13);
            this.xScaleValue.TabIndex = 12;
            this.xScaleValue.Text = "1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "X:";
            // 
            // yScales
            // 
            this.yScales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yScales.Location = new System.Drawing.Point(128, 81);
            this.yScales.Maximum = 80;
            this.yScales.Minimum = 1;
            this.yScales.Name = "yScales";
            this.yScales.Size = new System.Drawing.Size(104, 45);
            this.yScales.TabIndex = 9;
            this.yScales.Value = 10;
            this.yScales.ValueChanged += new System.EventHandler(this.ScaleValueChanged);
            // 
            // xScales
            // 
            this.xScales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.xScales.Location = new System.Drawing.Point(6, 81);
            this.xScales.Maximum = 80;
            this.xScales.Minimum = 1;
            this.xScales.Name = "xScales";
            this.xScales.Size = new System.Drawing.Size(104, 45);
            this.xScales.TabIndex = 8;
            this.xScales.Value = 10;
            this.xScales.ValueChanged += new System.EventHandler(this.ScaleValueChanged);
            // 
            // rotation
            // 
            this.rotation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotation.Controls.Add(this.groupBox1);
            this.rotation.Controls.Add(this.isClockWise);
            this.rotation.Controls.Add(this.angleValue);
            this.rotation.Controls.Add(this.anglesValues);
            this.rotation.Controls.Add(this.label4);
            this.rotation.Location = new System.Drawing.Point(5, 160);
            this.rotation.Name = "rotation";
            this.rotation.Size = new System.Drawing.Size(238, 207);
            this.rotation.TabIndex = 6;
            this.rotation.TabStop = false;
            this.rotation.Text = "Вращение:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.aroundSelf);
            this.groupBox1.Controls.Add(this.coordsText);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 121);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Точка вращения:";
            // 
            // aroundSelf
            // 
            this.aroundSelf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.aroundSelf.AutoSize = true;
            this.aroundSelf.Checked = true;
            this.aroundSelf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aroundSelf.Location = new System.Drawing.Point(54, 32);
            this.aroundSelf.Name = "aroundSelf";
            this.aroundSelf.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.aroundSelf.Size = new System.Drawing.Size(115, 17);
            this.aroundSelf.TabIndex = 13;
            this.aroundSelf.Text = "Вокруг своей оси";
            this.aroundSelf.UseVisualStyleBackColor = true;
            this.aroundSelf.Click += new System.EventHandler(this.AroundCheckBox);
            // 
            // coordsText
            // 
            this.coordsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coordsText.Location = new System.Drawing.Point(116, 75);
            this.coordsText.Name = "coordsText";
            this.coordsText.ReadOnly = true;
            this.coordsText.Size = new System.Drawing.Size(100, 20);
            this.coordsText.TabIndex = 12;
            this.coordsText.Text = "0,0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Координаты:";
            // 
            // isClockWise
            // 
            this.isClockWise.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isClockWise.AutoSize = true;
            this.isClockWise.Location = new System.Drawing.Point(6, 41);
            this.isClockWise.Name = "isClockWise";
            this.isClockWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isClockWise.Size = new System.Drawing.Size(84, 17);
            this.isClockWise.TabIndex = 9;
            this.isClockWise.Text = "По часовой";
            this.isClockWise.UseVisualStyleBackColor = true;
            // 
            // angleValue
            // 
            this.angleValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.angleValue.AutoSize = true;
            this.angleValue.Location = new System.Drawing.Point(192, 16);
            this.angleValue.Name = "angleValue";
            this.angleValue.Size = new System.Drawing.Size(17, 13);
            this.angleValue.TabIndex = 8;
            this.angleValue.Text = "0°";
            // 
            // anglesValues
            // 
            this.anglesValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.anglesValues.Location = new System.Drawing.Point(122, 41);
            this.anglesValues.Maximum = 360;
            this.anglesValues.Name = "anglesValues";
            this.anglesValues.Size = new System.Drawing.Size(104, 45);
            this.anglesValues.TabIndex = 7;
            this.anglesValues.ValueChanged += new System.EventHandler(this.AngleChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Угол:";
            // 
            // faces
            // 
            this.faces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.faces.Location = new System.Drawing.Point(127, 74);
            this.faces.Maximum = 80;
            this.faces.Minimum = 3;
            this.faces.Name = "faces";
            this.faces.Size = new System.Drawing.Size(104, 45);
            this.faces.TabIndex = 5;
            this.faces.Value = 3;
            this.faces.ValueChanged += new System.EventHandler(this.FacesChanged);
            // 
            // faceValue
            // 
            this.faceValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.faceValue.AutoSize = true;
            this.faceValue.Location = new System.Drawing.Point(197, 58);
            this.faceValue.Name = "faceValue";
            this.faceValue.Size = new System.Drawing.Size(13, 13);
            this.faceValue.TabIndex = 4;
            this.faceValue.Text = "3";
            // 
            // facesLabel
            // 
            this.facesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.facesLabel.AutoSize = true;
            this.facesLabel.Location = new System.Drawing.Point(145, 58);
            this.facesLabel.Name = "facesLabel";
            this.facesLabel.Size = new System.Drawing.Size(46, 13);
            this.facesLabel.TabIndex = 3;
            this.facesLabel.Text = "Граней:";
            // 
            // circle
            // 
            this.circle.Location = new System.Drawing.Point(5, 120);
            this.circle.Name = "circle";
            this.circle.Size = new System.Drawing.Size(90, 30);
            this.circle.TabIndex = 2;
            this.circle.Text = "Круг";
            this.circle.UseVisualStyleBackColor = true;
            this.circle.Click += new System.EventHandler(this.OnDrawPolygon);
            // 
            // regular
            // 
            this.regular.Location = new System.Drawing.Point(5, 70);
            this.regular.Name = "regular";
            this.regular.Size = new System.Drawing.Size(90, 30);
            this.regular.TabIndex = 1;
            this.regular.Text = "Правильный";
            this.regular.UseVisualStyleBackColor = true;
            this.regular.Click += new System.EventHandler(this.OnDrawPolygon);
            // 
            // square
            // 
            this.square.Location = new System.Drawing.Point(5, 20);
            this.square.Name = "square";
            this.square.Size = new System.Drawing.Size(90, 30);
            this.square.TabIndex = 0;
            this.square.Text = "Квадрат";
            this.square.UseVisualStyleBackColor = true;
            this.square.Click += new System.EventHandler(this.OnDrawPolygon);
            // 
            // slaveLayout
            // 
            this.slaveLayout.ColumnCount = 1;
            this.slaveLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.slaveLayout.Controls.Add(this.canvas, 0, 0);
            this.slaveLayout.Controls.Add(this.messagesBox, 0, 1);
            this.slaveLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slaveLayout.Location = new System.Drawing.Point(281, 3);
            this.slaveLayout.Name = "slaveLayout";
            this.slaveLayout.RowCount = 2;
            this.slaveLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.slaveLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.slaveLayout.Size = new System.Drawing.Size(643, 602);
            this.slaveLayout.TabIndex = 0;
            // 
            // messagesBox
            // 
            this.messagesBox.Controls.Add(this.messages);
            this.messagesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesBox.Location = new System.Drawing.Point(3, 505);
            this.messagesBox.Name = "messagesBox";
            this.messagesBox.Size = new System.Drawing.Size(637, 94);
            this.messagesBox.TabIndex = 1;
            this.messagesBox.TabStop = false;
            this.messagesBox.Text = "Сообщения:";
            // 
            // messages
            // 
            this.messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messages.Location = new System.Drawing.Point(3, 16);
            this.messages.Multiline = true;
            this.messages.Name = "messages";
            this.messages.ReadOnly = true;
            this.messages.Size = new System.Drawing.Size(631, 75);
            this.messages.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 631);
            this.Controls.Add(this.generalLayout);
            this.MinimumSize = new System.Drawing.Size(965, 670);
            this.Name = "Form1";
            this.Text = "Рисование простых форм через OpenGL";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseForm);
            this.generalLayout.ResumeLayout(false);
            this.controls.ResumeLayout(false);
            this.polygons.ResumeLayout(false);
            this.polygons.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yScales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xScales)).EndInit();
            this.rotation.ResumeLayout(false);
            this.rotation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anglesValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faces)).EndInit();
            this.slaveLayout.ResumeLayout(false);
            this.messagesBox.ResumeLayout(false);
            this.messagesBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl canvas;
        private System.Windows.Forms.TableLayoutPanel generalLayout;
        private System.Windows.Forms.GroupBox controls;
        private System.Windows.Forms.GroupBox polygons;
        private System.Windows.Forms.TableLayoutPanel slaveLayout;
        private System.Windows.Forms.TrackBar faces;
        private System.Windows.Forms.Label faceValue;
        private System.Windows.Forms.Label facesLabel;
        private System.Windows.Forms.Button circle;
        private System.Windows.Forms.Button regular;
        private System.Windows.Forms.Button square;
        private System.Windows.Forms.TextBox messages;
        private System.Windows.Forms.GroupBox rotation;
        private System.Windows.Forms.CheckBox isClockWise;
        private System.Windows.Forms.Label angleValue;
        private System.Windows.Forms.TrackBar anglesValues;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox coordsText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox messagesBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox funChoice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox animate;
        private System.Windows.Forms.CheckBox uniformScale;
        private System.Windows.Forms.Label yScaleValue;
        private System.Windows.Forms.Label xScaleValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar yScales;
        private System.Windows.Forms.TrackBar xScales;
        private System.Windows.Forms.CheckBox aroundSelf;
    }
}

