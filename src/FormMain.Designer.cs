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
            btnExport = new Button();
            btnImport = new Button();
            txtMutate = new TextBox();
            btnPlusMutate = new Button();
            btnMinusMutate = new Button();
            btnRandomReset = new Button();
            btnMinusCountRays = new Button();
            btnPlusCountRays = new Button();
            txtCountRays = new TextBox();
            lblCountRays = new Label();
            btnDriveMode = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // skglControl
            // 
            skglControl.BackColor = Color.Black;
            skglControl.Location = new Point(35, 11);
            skglControl.Margin = new Padding(4);
            skglControl.Name = "skglControl";
            skglControl.Size = new Size(242, 538);
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
            BtnReset.Location = new Point(429, 524);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(72, 27);
            BtnReset.TabIndex = 1;
            BtnReset.Text = "Entrenar";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // ChkDashboard
            // 
            ChkDashboard.AutoSize = true;
            ChkDashboard.Location = new Point(316, 464);
            ChkDashboard.Name = "ChkDashboard";
            ChkDashboard.Size = new Size(83, 19);
            ChkDashboard.TabIndex = 2;
            ChkDashboard.Text = "Dashboard";
            ChkDashboard.UseVisualStyleBackColor = true;
            ChkDashboard.Visible = false;
            ChkDashboard.CheckedChanged += ChkDashboard_CheckedChanged;
            // 
            // skglNetworkControl
            // 
            skglNetworkControl.BackColor = Color.Black;
            skglNetworkControl.Location = new Point(316, 11);
            skglNetworkControl.Margin = new Padding(4, 3, 4, 3);
            skglNetworkControl.Name = "skglNetworkControl";
            skglNetworkControl.Size = new Size(286, 417);
            skglNetworkControl.TabIndex = 3;
            skglNetworkControl.VSync = true;
            skglNetworkControl.PaintSurface += skglNetworkControl_PaintSurface;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(316, 524);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 4;
            btnSave.Text = "Guardar mejor coche";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnExport
            // 
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(457, 489);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(68, 27);
            btnExport.TabIndex = 5;
            btnExport.Text = "Exportar";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Location = new Point(531, 489);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(71, 27);
            btnImport.TabIndex = 6;
            btnImport.Text = "Importar";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // txtMutate
            // 
            txtMutate.BorderStyle = BorderStyle.FixedSingle;
            txtMutate.Location = new Point(507, 526);
            txtMutate.Name = "txtMutate";
            txtMutate.ReadOnly = true;
            txtMutate.Size = new Size(39, 23);
            txtMutate.TabIndex = 7;
            txtMutate.TextAlign = HorizontalAlignment.Center;
            // 
            // btnPlusMutate
            // 
            btnPlusMutate.FlatStyle = FlatStyle.Flat;
            btnPlusMutate.Location = new Point(550, 526);
            btnPlusMutate.Name = "btnPlusMutate";
            btnPlusMutate.Size = new Size(23, 23);
            btnPlusMutate.TabIndex = 8;
            btnPlusMutate.Text = "+";
            btnPlusMutate.UseVisualStyleBackColor = true;
            btnPlusMutate.Click += btnPlusMutate_Click;
            // 
            // btnMinusMutate
            // 
            btnMinusMutate.FlatStyle = FlatStyle.Flat;
            btnMinusMutate.Location = new Point(579, 526);
            btnMinusMutate.Name = "btnMinusMutate";
            btnMinusMutate.Size = new Size(23, 23);
            btnMinusMutate.TabIndex = 9;
            btnMinusMutate.Text = "-";
            btnMinusMutate.UseVisualStyleBackColor = true;
            btnMinusMutate.Click += btnMinusMutate_Click;
            // 
            // btnRandomReset
            // 
            btnRandomReset.FlatStyle = FlatStyle.Flat;
            btnRandomReset.Location = new Point(316, 489);
            btnRandomReset.Name = "btnRandomReset";
            btnRandomReset.Size = new Size(135, 27);
            btnRandomReset.TabIndex = 10;
            btnRandomReset.Text = "Iniciar desde cero";
            btnRandomReset.UseVisualStyleBackColor = true;
            btnRandomReset.Click += btnRandomReset_Click;
            // 
            // btnMinusCountRays
            // 
            btnMinusCountRays.FlatStyle = FlatStyle.Flat;
            btnMinusCountRays.Location = new Point(579, 434);
            btnMinusCountRays.Name = "btnMinusCountRays";
            btnMinusCountRays.Size = new Size(23, 23);
            btnMinusCountRays.TabIndex = 13;
            btnMinusCountRays.Text = "-";
            btnMinusCountRays.UseVisualStyleBackColor = true;
            btnMinusCountRays.Click += btnMinusCountRays_Click;
            // 
            // btnPlusCountRays
            // 
            btnPlusCountRays.FlatStyle = FlatStyle.Flat;
            btnPlusCountRays.Location = new Point(550, 434);
            btnPlusCountRays.Name = "btnPlusCountRays";
            btnPlusCountRays.Size = new Size(23, 23);
            btnPlusCountRays.TabIndex = 12;
            btnPlusCountRays.Text = "+";
            btnPlusCountRays.UseVisualStyleBackColor = true;
            btnPlusCountRays.Click += btnPlusCountRays_Click;
            // 
            // txtCountRays
            // 
            txtCountRays.BorderStyle = BorderStyle.FixedSingle;
            txtCountRays.Location = new Point(507, 434);
            txtCountRays.Name = "txtCountRays";
            txtCountRays.ReadOnly = true;
            txtCountRays.Size = new Size(39, 23);
            txtCountRays.TabIndex = 11;
            txtCountRays.TextAlign = HorizontalAlignment.Center;
            // 
            // lblCountRays
            // 
            lblCountRays.AutoSize = true;
            lblCountRays.Location = new Point(448, 438);
            lblCountRays.Name = "lblCountRays";
            lblCountRays.Size = new Size(53, 15);
            lblCountRays.TabIndex = 14;
            lblCountRays.Text = "Sensores";
            // 
            // btnDriveMode
            // 
            btnDriveMode.FlatStyle = FlatStyle.Flat;
            btnDriveMode.Location = new Point(316, 432);
            btnDriveMode.Name = "btnDriveMode";
            btnDriveMode.Size = new Size(107, 27);
            btnDriveMode.TabIndex = 15;
            btnDriveMode.Text = "Modo Conducir";
            btnDriveMode.UseVisualStyleBackColor = true;
            btnDriveMode.Click += btnDriveMode_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(615, 561);
            Controls.Add(btnDriveMode);
            Controls.Add(lblCountRays);
            Controls.Add(btnMinusCountRays);
            Controls.Add(btnPlusCountRays);
            Controls.Add(txtCountRays);
            Controls.Add(btnRandomReset);
            Controls.Add(btnMinusMutate);
            Controls.Add(btnPlusMutate);
            Controls.Add(txtMutate);
            Controls.Add(btnImport);
            Controls.Add(btnExport);
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
        private Button btnExport;
        private Button btnImport;
        private Button btnMinusMutate;
        private Button btnPlusMutate;
        private TextBox txtMutate;
        private Button btnRandomReset;
        private Label lblCountRays;
        private Button btnMinusCountRays;
        private Button btnPlusCountRays;
        private TextBox txtCountRays;
        private Button btnDriveMode;
    }
}
