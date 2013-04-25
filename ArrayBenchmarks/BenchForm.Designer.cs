namespace ArrayBenchmarks
{
    partial class BenchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.START = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // START
            // 
            this.START.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.START.Location = new System.Drawing.Point(348, 619);
            this.START.Name = "START";
            this.START.Size = new System.Drawing.Size(97, 36);
            this.START.TabIndex = 18;
            this.START.Text = "ПУСК";
            this.START.UseVisualStyleBackColor = true;
            this.START.Click += new System.EventHandler(this.START_Click_1);
            // 
            // BenchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 667);
            this.Controls.Add(this.START);
            this.Name = "BenchForm";
            this.ShowInTaskbar = false;
            this.Text = "Тест производительности методов сортировки";
            this.Load += new System.EventHandler(this.BenchForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button START;
    }
}

