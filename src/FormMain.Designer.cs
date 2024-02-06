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
            btnMinusSecondLevel = new Button();
            btnPlusSecondLevel = new Button();
            txtSecondLevel = new TextBox();
            lblSecondLevel = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
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
            BtnReset.Location = new Point(694, 178);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(105, 48);
            BtnReset.TabIndex = 1;
            BtnReset.Text = "Entrenar (mutar pesos)";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // ChkDashboard
            // 
            ChkDashboard.AutoSize = true;
            ChkDashboard.Location = new Point(694, 496);
            ChkDashboard.Name = "ChkDashboard";
            ChkDashboard.Size = new Size(135, 19);
            ChkDashboard.TabIndex = 2;
            ChkDashboard.Text = "Visualizar Dashboard";
            ChkDashboard.UseVisualStyleBackColor = true;
            ChkDashboard.CheckedChanged += ChkDashboard_CheckedChanged;
            // 
            // skglNetworkControl
            // 
            skglNetworkControl.BackColor = Color.Black;
            skglNetworkControl.Location = new Point(316, 11);
            skglNetworkControl.Margin = new Padding(4, 3, 4, 3);
            skglNetworkControl.Name = "skglNetworkControl";
            skglNetworkControl.Size = new Size(371, 538);
            skglNetworkControl.TabIndex = 3;
            skglNetworkControl.VSync = true;
            skglNetworkControl.PaintSurface += skglNetworkControl_PaintSurface;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(805, 178);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 48);
            btnSave.TabIndex = 4;
            btnSave.Text = "Guardar mejor coche";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnExport
            // 
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(694, 298);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(211, 27);
            btnExport.TabIndex = 5;
            btnExport.Text = "Exportar mejor coche actual";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Location = new Point(694, 265);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(211, 27);
            btnImport.TabIndex = 6;
            btnImport.Text = "Importar modelo";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // txtMutate
            // 
            txtMutate.BorderStyle = BorderStyle.FixedSingle;
            txtMutate.Location = new Point(763, 127);
            txtMutate.Name = "txtMutate";
            txtMutate.ReadOnly = true;
            txtMutate.Size = new Size(39, 23);
            txtMutate.TabIndex = 7;
            txtMutate.TextAlign = HorizontalAlignment.Center;
            // 
            // btnPlusMutate
            // 
            btnPlusMutate.FlatStyle = FlatStyle.Flat;
            btnPlusMutate.Location = new Point(806, 127);
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
            btnMinusMutate.Location = new Point(835, 127);
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
            btnRandomReset.Location = new Point(694, 232);
            btnRandomReset.Name = "btnRandomReset";
            btnRandomReset.Size = new Size(211, 27);
            btnRandomReset.TabIndex = 10;
            btnRandomReset.Text = "Modo Automático (reinicia modelo)";
            btnRandomReset.UseVisualStyleBackColor = true;
            btnRandomReset.Click += btnRandomReset_Click;
            // 
            // btnMinusCountRays
            // 
            btnMinusCountRays.FlatStyle = FlatStyle.Flat;
            btnMinusCountRays.Location = new Point(835, 39);
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
            btnPlusCountRays.Location = new Point(806, 39);
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
            txtCountRays.Location = new Point(763, 39);
            txtCountRays.Name = "txtCountRays";
            txtCountRays.ReadOnly = true;
            txtCountRays.Size = new Size(39, 23);
            txtCountRays.TabIndex = 11;
            txtCountRays.TextAlign = HorizontalAlignment.Center;
            // 
            // lblCountRays
            // 
            lblCountRays.AutoSize = true;
            lblCountRays.Location = new Point(704, 43);
            lblCountRays.Name = "lblCountRays";
            lblCountRays.Size = new Size(53, 15);
            lblCountRays.TabIndex = 14;
            lblCountRays.Text = "Sensores";
            // 
            // btnDriveMode
            // 
            btnDriveMode.FlatStyle = FlatStyle.Flat;
            btnDriveMode.Location = new Point(694, 521);
            btnDriveMode.Name = "btnDriveMode";
            btnDriveMode.Size = new Size(208, 27);
            btnDriveMode.TabIndex = 15;
            btnDriveMode.Text = "Modo Conducir";
            btnDriveMode.UseVisualStyleBackColor = true;
            btnDriveMode.Click += btnDriveMode_Click;
            // 
            // btnMinusSecondLevel
            // 
            btnMinusSecondLevel.FlatStyle = FlatStyle.Flat;
            btnMinusSecondLevel.Location = new Point(835, 83);
            btnMinusSecondLevel.Name = "btnMinusSecondLevel";
            btnMinusSecondLevel.Size = new Size(23, 23);
            btnMinusSecondLevel.TabIndex = 18;
            btnMinusSecondLevel.Text = "-";
            btnMinusSecondLevel.UseVisualStyleBackColor = true;
            btnMinusSecondLevel.Click += btnMinusSecondLevel_Click;
            // 
            // btnPlusSecondLevel
            // 
            btnPlusSecondLevel.FlatStyle = FlatStyle.Flat;
            btnPlusSecondLevel.Location = new Point(806, 83);
            btnPlusSecondLevel.Name = "btnPlusSecondLevel";
            btnPlusSecondLevel.Size = new Size(23, 23);
            btnPlusSecondLevel.TabIndex = 17;
            btnPlusSecondLevel.Text = "+";
            btnPlusSecondLevel.UseVisualStyleBackColor = true;
            btnPlusSecondLevel.Click += btnPlusSecondLevel_Click;
            // 
            // txtSecondLevel
            // 
            txtSecondLevel.BorderStyle = BorderStyle.FixedSingle;
            txtSecondLevel.Location = new Point(763, 83);
            txtSecondLevel.Name = "txtSecondLevel";
            txtSecondLevel.ReadOnly = true;
            txtSecondLevel.Size = new Size(39, 23);
            txtSecondLevel.TabIndex = 16;
            txtSecondLevel.TextAlign = HorizontalAlignment.Center;
            // 
            // lblSecondLevel
            // 
            lblSecondLevel.AutoSize = true;
            lblSecondLevel.Location = new Point(696, 87);
            lblSecondLevel.Name = "lblSecondLevel";
            lblSecondLevel.Size = new Size(61, 15);
            lblSecondLevel.TabIndex = 19;
            lblSecondLevel.Text = "Neuronas:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(763, 65);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 20;
            label1.Text = "Capa oculta:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(763, 21);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 21;
            label2.Text = "Capa entrada:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(763, 109);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 22;
            label3.Text = "Tasa aprendizaje:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(721, 131);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 23;
            label4.Text = "Valor:";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(916, 561);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblSecondLevel);
            Controls.Add(btnMinusSecondLevel);
            Controls.Add(btnPlusSecondLevel);
            Controls.Add(txtSecondLevel);
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
        private Button btnMinusSecondLevel;
        private Button btnPlusSecondLevel;
        private TextBox txtSecondLevel;
        private Label lblSecondLevel;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
