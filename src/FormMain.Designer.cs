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
            skglNetworkControl = new SkiaSharp.Views.Desktop.SKGLControl();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // skglControl
            // 
            skglControl.BackColor = Color.Black;
            skglControl.Location = new Point(60, 11);
            skglControl.Margin = new Padding(4);
            skglControl.Name = "skglControl";
            skglControl.Size = new Size(200, 538);
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
            BtnReset.Location = new Point(325, 522);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(126, 27);
            BtnReset.TabIndex = 1;
            BtnReset.Text = "Reiniciar";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // ChkDashboard
            // 
            ChkDashboard.AutoSize = true;
            ChkDashboard.Location = new Point(457, 527);
            ChkDashboard.Name = "ChkDashboard";
            ChkDashboard.Size = new Size(126, 19);
            ChkDashboard.TabIndex = 2;
            ChkDashboard.Text = "Mostrar dashboard";
            ChkDashboard.UseVisualStyleBackColor = true;
            ChkDashboard.CheckedChanged += ChkDashboard_CheckedChanged;
            // 
            // skglNetworkControl
            // 
            skglNetworkControl.BackColor = Color.Black;
            skglNetworkControl.Location = new Point(325, 11);
            skglNetworkControl.Margin = new Padding(4, 3, 4, 3);
            skglNetworkControl.Name = "skglNetworkControl";
            skglNetworkControl.Size = new Size(277, 468);
            skglNetworkControl.TabIndex = 3;
            skglNetworkControl.VSync = true;
            skglNetworkControl.PaintSurface += skglNetworkControl_PaintSurface;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(325, 489);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(277, 27);
            btnSave.TabIndex = 4;
            btnSave.Text = "Guardar NNA mejor coche";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(615, 561);
            Controls.Add(btnSave);
            Controls.Add(skglNetworkControl);
            Controls.Add(ChkDashboard);
            Controls.Add(BtnReset);
            Controls.Add(skglControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormMain";
            Text = "Skia Neuronal Network";
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
        private SkiaSharp.Views.Desktop.SKGLControl skglNetworkControl;
        private Button btnSave;
    }
}
