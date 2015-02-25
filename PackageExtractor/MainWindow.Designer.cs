namespace PackageExtractor
{
    partial class MainWindow
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
            this.btnDoExtract = new System.Windows.Forms.Button();
            this.txtMapFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSBPath = new System.Windows.Forms.TextBox();
            this.lblMapFilename = new System.Windows.Forms.Label();
            this.checkBoxAllMaps = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilenameSuffix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.checkBoxLocalSave = new System.Windows.Forms.CheckBox();
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDoExtract
            // 
            this.btnDoExtract.Location = new System.Drawing.Point(152, 383);
            this.btnDoExtract.Name = "btnDoExtract";
            this.btnDoExtract.Size = new System.Drawing.Size(75, 23);
            this.btnDoExtract.TabIndex = 0;
            this.btnDoExtract.Text = "Extract!";
            this.btnDoExtract.UseVisualStyleBackColor = true;
            this.btnDoExtract.Click += new System.EventHandler(this.btnDoExtract_Click);
            // 
            // txtMapFile
            // 
            this.txtMapFile.Location = new System.Drawing.Point(102, 95);
            this.txtMapFile.Name = "txtMapFile";
            this.txtMapFile.Size = new System.Drawing.Size(100, 20);
            this.txtMapFile.TabIndex = 1;
            this.txtMapFile.Text = "PT_Hawksmouth";
            this.txtMapFile.TextChanged += new System.EventHandler(this.txtFileAppend_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSearchString);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSBPath);
            this.groupBox1.Controls.Add(this.lblMapFilename);
            this.groupBox1.Controls.Add(this.txtMapFile);
            this.groupBox1.Controls.Add(this.checkBoxAllMaps);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 229);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "TCOS path";
            // 
            // txtSBPath
            // 
            this.txtSBPath.Location = new System.Drawing.Point(72, 19);
            this.txtSBPath.Multiline = true;
            this.txtSBPath.Name = "txtSBPath";
            this.txtSBPath.Size = new System.Drawing.Size(130, 50);
            this.txtSBPath.TabIndex = 4;
            this.txtSBPath.Text = "C:\\Game Files (SSD)\\The Chronicles of Spellborn";
            this.txtSBPath.TextChanged += new System.EventHandler(this.txtSBPath_TextChanged);
            // 
            // lblMapFilename
            // 
            this.lblMapFilename.AutoSize = true;
            this.lblMapFilename.Location = new System.Drawing.Point(26, 98);
            this.lblMapFilename.Name = "lblMapFilename";
            this.lblMapFilename.Size = new System.Drawing.Size(70, 13);
            this.lblMapFilename.TabIndex = 2;
            this.lblMapFilename.Text = "Map filename";
            this.lblMapFilename.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBoxAllMaps
            // 
            this.checkBoxAllMaps.AutoSize = true;
            this.checkBoxAllMaps.Location = new System.Drawing.Point(9, 75);
            this.checkBoxAllMaps.Name = "checkBoxAllMaps";
            this.checkBoxAllMaps.Size = new System.Drawing.Size(65, 17);
            this.checkBoxAllMaps.TabIndex = 3;
            this.checkBoxAllMaps.Text = "All maps";
            this.checkBoxAllMaps.UseVisualStyleBackColor = true;
            this.checkBoxAllMaps.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxLocalSave);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSavePath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtFilenameSuffix);
            this.groupBox2.Location = new System.Drawing.Point(9, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 130);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output file configuration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filename suffix";
            // 
            // txtFilenameSuffix
            // 
            this.txtFilenameSuffix.Location = new System.Drawing.Point(88, 22);
            this.txtFilenameSuffix.Name = "txtFilenameSuffix";
            this.txtFilenameSuffix.Size = new System.Drawing.Size(100, 20);
            this.txtFilenameSuffix.TabIndex = 0;
            this.txtFilenameSuffix.Text = ".leveldata";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Save path";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Enabled = false;
            this.txtSavePath.Location = new System.Drawing.Point(76, 71);
            this.txtSavePath.Multiline = true;
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(130, 50);
            this.txtSavePath.TabIndex = 4;
            this.txtSavePath.Text = "C:\\Game Files (SSD)\\The Chronicles of Spellborn\\data\\";
            this.txtSavePath.TextChanged += new System.EventHandler(this.txtSavePath_TextChanged);
            // 
            // checkBoxLocalSave
            // 
            this.checkBoxLocalSave.AutoSize = true;
            this.checkBoxLocalSave.Enabled = false;
            this.checkBoxLocalSave.Location = new System.Drawing.Point(6, 48);
            this.checkBoxLocalSave.Name = "checkBoxLocalSave";
            this.checkBoxLocalSave.Size = new System.Drawing.Size(117, 17);
            this.checkBoxLocalSave.TabIndex = 6;
            this.checkBoxLocalSave.Text = "Save to local folder";
            this.checkBoxLocalSave.UseVisualStyleBackColor = true;
            this.checkBoxLocalSave.CheckedChanged += new System.EventHandler(this.checkBoxLocalSave_CheckedChanged);
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(102, 138);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(100, 20);
            this.txtSearchString.TabIndex = 6;
            this.txtSearchString.Text = "SBWorldPortal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Search string";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 418);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDoExtract);
            this.Name = "MainWindow";
            this.Text = "TCoS Package Extractor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDoExtract;
        private System.Windows.Forms.TextBox txtMapFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMapFilename;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxAllMaps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilenameSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSBPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.CheckBox checkBoxLocalSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchString;
    }
}