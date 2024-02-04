namespace SkiaCarForms
{
    partial class FormMain
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
            skglControl = new SkiaSharp.Views.Desktop.SKGLControl();
            fileSystemWatcher1 = new FileSystemWatcher();
            BtnReset = new Button();
            ChkDashboard = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // skglControl
            // 
            skglControl.BackColor = Color.Black;
            skglControl.Location = new Point(251, 13);
            skglControl.Margin = new Padding(5, 5, 5, 5);
            skglControl.Name = "skglControl";
            skglControl.Size = new Size(213, 520);
            skglControl.TabIndex = 0;
            skglControl.VSync = true;
            skglControl.PaintSurface += skglControl_PaintSurface;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // BtnReset
            // 
            BtnReset.FlatStyle = FlatStyle.Flat;
            BtnReset.Location = new Point(624, 464);
            BtnReset.Margin = new Padding(3, 4, 3, 4);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(144, 36);
            BtnReset.TabIndex = 1;
            BtnReset.Text = "Reiniciar";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // ChkDashboard
            // 
            ChkDashboard.AutoSize = true;
            ChkDashboard.Location = new Point(624, 508);
            ChkDashboard.Margin = new Padding(3, 4, 3, 4);
            ChkDashboard.Name = "ChkDashboard";
            ChkDashboard.Size = new Size(157, 24);
            ChkDashboard.TabIndex = 2;
            ChkDashboard.Text = "Mostrar dashboard";
            ChkDashboard.UseVisualStyleBackColor = true;
            ChkDashboard.CheckedChanged += ChkDashboard_CheckedChanged;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(ChkDashboard);
            Controls.Add(BtnReset);
            Controls.Add(skglControl);
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl skglControl;
        private FileSystemWatcher fileSystemWatcher1;
        private Button BtnReset;
        private CheckBox ChkDashboard;
    }
}
