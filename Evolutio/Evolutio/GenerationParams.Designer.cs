using System.ComponentModel;

namespace Evolutio
{
    partial class GenerationParams
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.button1 = new System.Windows.Forms.Button();
            this.tbFreq = new System.Windows.Forms.TextBox();
            this.tbPersistence = new System.Windows.Forms.TextBox();
            this.tbScale = new System.Windows.Forms.TextBox();
            this.tbBias = new System.Windows.Forms.TextBox();
            this.tbLowerBound = new System.Windows.Forms.TextBox();
            this.tbUpperBound = new System.Windows.Forms.TextBox();
            this.tbOctave = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.button1.Location = new System.Drawing.Point(17, 288);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.tbFreq.Location = new System.Drawing.Point(103, 105);
            this.tbFreq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbFreq.Name = "tbFreq";
            this.tbFreq.Size = new System.Drawing.Size(100, 23);
            this.tbFreq.TabIndex = 1;
            this.tbPersistence.Location = new System.Drawing.Point(103, 135);
            this.tbPersistence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbPersistence.Name = "tbPersistence";
            this.tbPersistence.Size = new System.Drawing.Size(100, 23);
            this.tbPersistence.TabIndex = 2;
            this.tbScale.Location = new System.Drawing.Point(103, 164);
            this.tbScale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(100, 23);
            this.tbScale.TabIndex = 3;
            this.tbBias.Location = new System.Drawing.Point(103, 193);
            this.tbBias.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbBias.Name = "tbBias";
            this.tbBias.Size = new System.Drawing.Size(100, 23);
            this.tbBias.TabIndex = 4;
            this.tbLowerBound.Location = new System.Drawing.Point(103, 222);
            this.tbLowerBound.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbLowerBound.Name = "tbLowerBound";
            this.tbLowerBound.Size = new System.Drawing.Size(100, 23);
            this.tbLowerBound.TabIndex = 5;
            this.tbUpperBound.Location = new System.Drawing.Point(103, 252);
            this.tbUpperBound.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbUpperBound.Name = "tbUpperBound";
            this.tbUpperBound.Size = new System.Drawing.Size(100, 23);
            this.tbUpperBound.TabIndex = 6;
            this.tbOctave.Location = new System.Drawing.Point(103, 72);
            this.tbOctave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbOctave.Name = "tbOctave";
            this.tbOctave.Size = new System.Drawing.Size(100, 23);
            this.tbOctave.TabIndex = 7;
            this.button2.Location = new System.Drawing.Point(103, 288);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Generate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.tb1.Location = new System.Drawing.Point(103, 12);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(100, 23);
            this.tb1.TabIndex = 9;
            this.tb2.Location = new System.Drawing.Point(103, 43);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(100, 23);
            this.tb2.TabIndex = 10;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 330);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbOctave);
            this.Controls.Add(this.tbUpperBound);
            this.Controls.Add(this.tbLowerBound);
            this.Controls.Add(this.tbBias);
            this.Controls.Add(this.tbScale);
            this.Controls.Add(this.tbPersistence);
            this.Controls.Add(this.tbFreq);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "GenerationParams";
            this.Text = "GenerationParams";
            this.Load += new System.EventHandler(this.GenerationParams_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbFreq;
        private System.Windows.Forms.TextBox tbPersistence;
        private System.Windows.Forms.TextBox tbBias;
        private System.Windows.Forms.TextBox tbLowerBound;
        private System.Windows.Forms.TextBox tbUpperBound;
        private System.Windows.Forms.TextBox tbOctave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.TextBox tbScale;
    }
}