namespace cslibs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnUIBackTask = new Button();
            pbUIBackTask = new ProgressBar();
            tbUIBackTask = new TextBox();
            SuspendLayout();
            // 
            // btnUIBackTask
            // 
            btnUIBackTask.Location = new Point(12, 28);
            btnUIBackTask.Name = "btnUIBackTask";
            btnUIBackTask.Size = new Size(143, 23);
            btnUIBackTask.TabIndex = 0;
            btnUIBackTask.Text = "UIBackTaskのテスト";
            btnUIBackTask.UseVisualStyleBackColor = true;
            btnUIBackTask.Click += btnTestUIBackTask_Click;
            // 
            // pbUIBackTask
            // 
            pbUIBackTask.Location = new Point(161, 28);
            pbUIBackTask.Name = "pbUIBackTask";
            pbUIBackTask.Size = new Size(205, 23);
            pbUIBackTask.TabIndex = 1;
            // 
            // tbUIBackTask
            // 
            tbUIBackTask.Location = new Point(12, 57);
            tbUIBackTask.Multiline = true;
            tbUIBackTask.Name = "tbUIBackTask";
            tbUIBackTask.ScrollBars = ScrollBars.Vertical;
            tbUIBackTask.Size = new Size(354, 152);
            tbUIBackTask.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tbUIBackTask);
            Controls.Add(pbUIBackTask);
            Controls.Add(btnUIBackTask);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUIBackTask;
        private ProgressBar pbUIBackTask;
        private TextBox tbUIBackTask;
    }
}
