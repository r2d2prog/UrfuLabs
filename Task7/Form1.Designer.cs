namespace Task7
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
            this.info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1017, 571);
            this.canvas.TabIndex = 0;
            this.canvas.VSync = false;
            this.canvas.SizeChanged += new System.EventHandler(this.OnSize);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.canvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.canvas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.BackColor = System.Drawing.Color.Transparent;
            this.info.Location = new System.Drawing.Point(12, 9);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(299, 13);
            this.info.TabIndex = 1;
            this.info.Text = "Управление W,A,S,D,мышь Q-вкл\\выкл сетку ESC-выход";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 571);
            this.Controls.Add(this.info);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "House scene";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl canvas;
        private System.Windows.Forms.Label info;
    }
}

