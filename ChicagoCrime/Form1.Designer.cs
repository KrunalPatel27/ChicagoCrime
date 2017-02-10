namespace ChicagoCrime
{
  partial class Form1
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
            this.cmdByYear = new System.Windows.Forms.Button();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.Arrest = new System.Windows.Forms.Button();
            this.ByCrime = new System.Windows.Forms.Button();
            this.ByArea = new System.Windows.Forms.Button();
            this.Chicago = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdByYear
            // 
            this.cmdByYear.Location = new System.Drawing.Point(12, 12);
            this.cmdByYear.Name = "cmdByYear";
            this.cmdByYear.Size = new System.Drawing.Size(95, 62);
            this.cmdByYear.TabIndex = 0;
            this.cmdByYear.Text = "By Year";
            this.cmdByYear.UseVisualStyleBackColor = true;
            this.cmdByYear.Click += new System.EventHandler(this.cmdByYear_Click);
            // 
            // graphPanel
            // 
            this.graphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel.BackColor = System.Drawing.Color.White;
            this.graphPanel.Location = new System.Drawing.Point(123, 12);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(769, 454);
            this.graphPanel.TabIndex = 1;
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.BackColor = System.Drawing.SystemColors.Control;
            this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(123, 474);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(769, 44);
            this.txtFilename.TabIndex = 2;
            this.txtFilename.Text = "Crimes.csv";
            // 
            // Arrest
            // 
            this.Arrest.Location = new System.Drawing.Point(12, 96);
            this.Arrest.Name = "cmdArrest";
            this.Arrest.Size = new System.Drawing.Size(95, 60);
            this.Arrest.TabIndex = 0;
            this.Arrest.Text = "Arrest%";
            this.Arrest.UseVisualStyleBackColor = true;
            this.Arrest.Click += new System.EventHandler(this.button1_Click);
            // 
            // ByCrime
            // 
            this.ByCrime.Location = new System.Drawing.Point(12, 178);
            this.ByCrime.Name = "ByCrime";
            this.ByCrime.Size = new System.Drawing.Size(95, 56);
            this.ByCrime.TabIndex = 3;
            this.ByCrime.Text = "By Crime";
            this.ByCrime.UseVisualStyleBackColor = true;
            this.ByCrime.Click += new System.EventHandler(this.button2_Click);
            // 
            // ByArea
            // 
            this.ByArea.Location = new System.Drawing.Point(12, 297);
            this.ByArea.Name = "cmdByArea";
            this.ByArea.Size = new System.Drawing.Size(95, 48);
            this.ByArea.TabIndex = 4;
            this.ByArea.Text = "By Area";
            this.ByArea.UseVisualStyleBackColor = true;
            this.ByArea.Click += new System.EventHandler(this.button3_Click);
            // 
            // Chicago
            // 
            this.Chicago.Location = new System.Drawing.Point(12, 412);
            this.Chicago.Name = "cmdChicago";
            this.Chicago.Size = new System.Drawing.Size(95, 53);
            this.Chicago.TabIndex = 5;
            this.Chicago.Text = "Chicago";
            this.Chicago.UseVisualStyleBackColor = true;
            this.Chicago.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 240);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 51);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 358);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(95, 51);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(22F, 44F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 508);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Chicago);
            this.Controls.Add(this.ByArea);
            this.Controls.Add(this.ByCrime);
            this.Controls.Add(this.Arrest);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.graphPanel);
            this.Controls.Add(this.cmdByYear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chicago Crime Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdByYear;
    private System.Windows.Forms.Panel graphPanel;
    private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button Arrest;
        private System.Windows.Forms.Button ByCrime;
        private System.Windows.Forms.Button ByArea;
        private System.Windows.Forms.Button Chicago;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

