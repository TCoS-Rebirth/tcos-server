namespace TCoSServer
{
  partial class MainWindow
  {
    /// <summary>
    /// Variable nécessaire au concepteur.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Nettoyage des ressources utilisées.
    /// </summary>
    /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
    protected override void Dispose (bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose ();
      }
      base.Dispose (disposing);
    }

    #region Code généré par le Concepteur Windows Form

    /// <summary>
    /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent ()
    {
            this.txtLoginLogs = new System.Windows.Forms.RichTextBox();
            this.lblLoginLogs = new System.Windows.Forms.Label();
            this.btnStartLogin = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.txtLoginIP = new System.Windows.Forms.TextBox();
            this.txtLoginPort = new System.Windows.Forms.TextBox();
            this.txtGameWorldPort = new System.Windows.Forms.TextBox();
            this.txtGameWorldIP = new System.Windows.Forms.TextBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtGameWorldLogs = new System.Windows.Forms.RichTextBox();
            this.lblGameWorldLogs = new System.Windows.Forms.Label();
            this.chkToWorld = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTelePawn = new System.Windows.Forms.Button();
            this.txtTelePawnRot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTelePawnRel = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTelePawnLocZ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTelePawnLocY = new System.Windows.Forms.TextBox();
            this.txtTelePawnLocX = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIERel = new System.Windows.Forms.TextBox();
            this.btnIE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLevelObject = new System.Windows.Forms.TextBox();
            this.txtBitfield = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCollision = new System.Windows.Forms.TextBox();
            this.txtIELocZ = new System.Windows.Forms.TextBox();
            this.txtIsEnabled = new System.Windows.Forms.TextBox();
            this.txtIELocY = new System.Windows.Forms.TextBox();
            this.txtIERot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIsHidden = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIELocX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrintCharIDs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLoginLogs
            // 
            this.txtLoginLogs.Location = new System.Drawing.Point(24, 276);
            this.txtLoginLogs.Name = "txtLoginLogs";
            this.txtLoginLogs.Size = new System.Drawing.Size(191, 294);
            this.txtLoginLogs.TabIndex = 0;
            this.txtLoginLogs.Text = "Not implemented yet. (logs are in console)";
            // 
            // lblLoginLogs
            // 
            this.lblLoginLogs.AutoSize = true;
            this.lblLoginLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginLogs.Location = new System.Drawing.Point(61, 239);
            this.lblLoginLogs.Name = "lblLoginLogs";
            this.lblLoginLogs.Size = new System.Drawing.Size(103, 24);
            this.lblLoginLogs.TabIndex = 1;
            this.lblLoginLogs.Text = "Login Logs";
            // 
            // btnStartLogin
            // 
            this.btnStartLogin.Location = new System.Drawing.Point(62, 97);
            this.btnStartLogin.Name = "btnStartLogin";
            this.btnStartLogin.Size = new System.Drawing.Size(127, 27);
            this.btnStartLogin.TabIndex = 2;
            this.btnStartLogin.Text = "Start Login Server";
            this.btnStartLogin.UseVisualStyleBackColor = true;
            this.btnStartLogin.Click += new System.EventHandler(this.btnStartLogin_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(195, 97);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(124, 27);
            this.btnStartGame.TabIndex = 3;
            this.btnStartGame.Text = "Start Game Server";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // txtLoginIP
            // 
            this.txtLoginIP.Location = new System.Drawing.Point(65, 45);
            this.txtLoginIP.Name = "txtLoginIP";
            this.txtLoginIP.Size = new System.Drawing.Size(124, 20);
            this.txtLoginIP.TabIndex = 4;
            this.txtLoginIP.Text = "127.0.0.1";
            this.txtLoginIP.TextChanged += new System.EventHandler(this.txtLoginIP_TextChanged);
            // 
            // txtLoginPort
            // 
            this.txtLoginPort.Location = new System.Drawing.Point(65, 71);
            this.txtLoginPort.Name = "txtLoginPort";
            this.txtLoginPort.Size = new System.Drawing.Size(124, 20);
            this.txtLoginPort.TabIndex = 5;
            this.txtLoginPort.Text = "4242";
            this.txtLoginPort.TextChanged += new System.EventHandler(this.txtLoginPort_TextChanged);
            // 
            // txtGameWorldPort
            // 
            this.txtGameWorldPort.Location = new System.Drawing.Point(195, 70);
            this.txtGameWorldPort.Name = "txtGameWorldPort";
            this.txtGameWorldPort.Size = new System.Drawing.Size(124, 20);
            this.txtGameWorldPort.TabIndex = 6;
            this.txtGameWorldPort.Text = "4343";
            this.txtGameWorldPort.TextChanged += new System.EventHandler(this.txtGameWorldPort_TextChanged);
            // 
            // txtGameWorldIP
            // 
            this.txtGameWorldIP.Location = new System.Drawing.Point(195, 44);
            this.txtGameWorldIP.Name = "txtGameWorldIP";
            this.txtGameWorldIP.Size = new System.Drawing.Size(124, 20);
            this.txtGameWorldIP.TabIndex = 7;
            this.txtGameWorldIP.Text = "127.0.0.1";
            this.txtGameWorldIP.TextChanged += new System.EventHandler(this.txtGameWorldIP_TextChanged);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 52);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(17, 13);
            this.lblIp.TabIndex = 8;
            this.lblIp.Text = "IP";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 78);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 9;
            this.lblPort.Text = "Port";
            // 
            // txtGameWorldLogs
            // 
            this.txtGameWorldLogs.Location = new System.Drawing.Point(246, 276);
            this.txtGameWorldLogs.Name = "txtGameWorldLogs";
            this.txtGameWorldLogs.Size = new System.Drawing.Size(191, 294);
            this.txtGameWorldLogs.TabIndex = 10;
            this.txtGameWorldLogs.Text = "Not implemented yet. (logs are in console)";
            // 
            // lblGameWorldLogs
            // 
            this.lblGameWorldLogs.AutoSize = true;
            this.lblGameWorldLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameWorldLogs.Location = new System.Drawing.Point(262, 239);
            this.lblGameWorldLogs.Name = "lblGameWorldLogs";
            this.lblGameWorldLogs.Size = new System.Drawing.Size(162, 24);
            this.lblGameWorldLogs.TabIndex = 11;
            this.lblGameWorldLogs.Text = "Game World Logs";
            // 
            // chkToWorld
            // 
            this.chkToWorld.AutoSize = true;
            this.chkToWorld.Location = new System.Drawing.Point(335, 74);
            this.chkToWorld.Name = "chkToWorld";
            this.chkToWorld.Size = new System.Drawing.Size(130, 17);
            this.chkToWorld.TabIndex = 12;
            this.chkToWorld.Text = "Connect direct to map";
            this.chkToWorld.UseVisualStyleBackColor = true;
            this.chkToWorld.CheckedChanged += new System.EventHandler(this.chkToWorld_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTelePawn);
            this.groupBox1.Controls.Add(this.txtTelePawnRot);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTelePawnRel);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtTelePawnLocZ);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTelePawnLocY);
            this.groupBox1.Controls.Add(this.txtTelePawnLocX);
            this.groupBox1.Location = new System.Drawing.Point(584, 278);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 127);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Teleport Pawn To (Relevance)";
            // 
            // btnTelePawn
            // 
            this.btnTelePawn.Location = new System.Drawing.Point(153, 98);
            this.btnTelePawn.Name = "btnTelePawn";
            this.btnTelePawn.Size = new System.Drawing.Size(59, 23);
            this.btnTelePawn.TabIndex = 63;
            this.btnTelePawn.Text = "Teleport!";
            this.btnTelePawn.UseVisualStyleBackColor = true;
            this.btnTelePawn.Click += new System.EventHandler(this.btnTelePawn_Click_1);
            // 
            // txtTelePawnRot
            // 
            this.txtTelePawnRot.Location = new System.Drawing.Point(112, 72);
            this.txtTelePawnRot.Name = "txtTelePawnRot";
            this.txtTelePawnRot.Size = new System.Drawing.Size(100, 20);
            this.txtTelePawnRot.TabIndex = 61;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 62;
            this.label11.Text = "Rotation";
            // 
            // txtTelePawnRel
            // 
            this.txtTelePawnRel.Location = new System.Drawing.Point(112, 22);
            this.txtTelePawnRel.Name = "txtTelePawnRel";
            this.txtTelePawnRel.Size = new System.Drawing.Size(100, 20);
            this.txtTelePawnRel.TabIndex = 59;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "RelevanceID";
            // 
            // txtTelePawnLocZ
            // 
            this.txtTelePawnLocZ.Location = new System.Drawing.Point(166, 46);
            this.txtTelePawnLocZ.Name = "txtTelePawnLocZ";
            this.txtTelePawnLocZ.Size = new System.Drawing.Size(47, 20);
            this.txtTelePawnLocZ.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 55;
            this.label9.Text = "Location";
            // 
            // txtTelePawnLocY
            // 
            this.txtTelePawnLocY.Location = new System.Drawing.Point(113, 46);
            this.txtTelePawnLocY.Name = "txtTelePawnLocY";
            this.txtTelePawnLocY.Size = new System.Drawing.Size(47, 20);
            this.txtTelePawnLocY.TabIndex = 57;
            this.txtTelePawnLocY.TextChanged += new System.EventHandler(this.txtTelePawnLocY_TextChanged);
            // 
            // txtTelePawnLocX
            // 
            this.txtTelePawnLocX.Location = new System.Drawing.Point(60, 46);
            this.txtTelePawnLocX.Name = "txtTelePawnLocX";
            this.txtTelePawnLocX.Size = new System.Drawing.Size(47, 20);
            this.txtTelePawnLocX.TabIndex = 56;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIERel);
            this.groupBox2.Controls.Add(this.btnIE);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtLevelObject);
            this.groupBox2.Controls.Add(this.txtBitfield);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCollision);
            this.groupBox2.Controls.Add(this.txtIELocZ);
            this.groupBox2.Controls.Add(this.txtIsEnabled);
            this.groupBox2.Controls.Add(this.txtIELocY);
            this.groupBox2.Controls.Add(this.txtIERot);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtIsHidden);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtIELocX);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(584, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 260);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Interactive Element";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtIERel
            // 
            this.txtIERel.Location = new System.Drawing.Point(112, 22);
            this.txtIERel.Name = "txtIERel";
            this.txtIERel.Size = new System.Drawing.Size(100, 20);
            this.txtIERel.TabIndex = 36;
            // 
            // btnIE
            // 
            this.btnIE.Location = new System.Drawing.Point(133, 230);
            this.btnIE.Name = "btnIE";
            this.btnIE.Size = new System.Drawing.Size(80, 23);
            this.btnIE.TabIndex = 51;
            this.btnIE.Text = "Send packet";
            this.btnIE.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "RelevanceID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "LevelObjectID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "RspwnTimerBtfld";
            // 
            // txtLevelObject
            // 
            this.txtLevelObject.Location = new System.Drawing.Point(112, 48);
            this.txtLevelObject.Name = "txtLevelObject";
            this.txtLevelObject.Size = new System.Drawing.Size(100, 20);
            this.txtLevelObject.TabIndex = 37;
            // 
            // txtBitfield
            // 
            this.txtBitfield.Location = new System.Drawing.Point(113, 204);
            this.txtBitfield.Name = "txtBitfield";
            this.txtBitfield.Size = new System.Drawing.Size(100, 20);
            this.txtBitfield.TabIndex = 44;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "CollisionType";
            // 
            // txtCollision
            // 
            this.txtCollision.Location = new System.Drawing.Point(113, 178);
            this.txtCollision.Name = "txtCollision";
            this.txtCollision.Size = new System.Drawing.Size(100, 20);
            this.txtCollision.TabIndex = 43;
            // 
            // txtIELocZ
            // 
            this.txtIELocZ.Location = new System.Drawing.Point(165, 126);
            this.txtIELocZ.Name = "txtIELocZ";
            this.txtIELocZ.Size = new System.Drawing.Size(47, 20);
            this.txtIELocZ.TabIndex = 54;
            // 
            // txtIsEnabled
            // 
            this.txtIsEnabled.Location = new System.Drawing.Point(113, 74);
            this.txtIsEnabled.Name = "txtIsEnabled";
            this.txtIsEnabled.Size = new System.Drawing.Size(100, 20);
            this.txtIsEnabled.TabIndex = 40;
            // 
            // txtIELocY
            // 
            this.txtIELocY.Location = new System.Drawing.Point(112, 126);
            this.txtIELocY.Name = "txtIELocY";
            this.txtIELocY.Size = new System.Drawing.Size(47, 20);
            this.txtIELocY.TabIndex = 53;
            // 
            // txtIERot
            // 
            this.txtIERot.Location = new System.Drawing.Point(113, 152);
            this.txtIERot.Name = "txtIERot";
            this.txtIERot.Size = new System.Drawing.Size(100, 20);
            this.txtIERot.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Rotation";
            // 
            // txtIsHidden
            // 
            this.txtIsHidden.Location = new System.Drawing.Point(112, 100);
            this.txtIsHidden.Name = "txtIsHidden";
            this.txtIsHidden.Size = new System.Drawing.Size(100, 20);
            this.txtIsHidden.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Location";
            // 
            // txtIELocX
            // 
            this.txtIELocX.Location = new System.Drawing.Point(59, 126);
            this.txtIELocX.Name = "txtIELocX";
            this.txtIELocX.Size = new System.Drawing.Size(47, 20);
            this.txtIELocX.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "isHidden";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "IsEnabled";
            // 
            // btnPrintCharIDs
            // 
            this.btnPrintCharIDs.Location = new System.Drawing.Point(719, 411);
            this.btnPrintCharIDs.Name = "btnPrintCharIDs";
            this.btnPrintCharIDs.Size = new System.Drawing.Size(83, 23);
            this.btnPrintCharIDs.TabIndex = 52;
            this.btnPrintCharIDs.Text = "Print Char IDs";
            this.btnPrintCharIDs.UseVisualStyleBackColor = true;
            this.btnPrintCharIDs.Click += new System.EventHandler(this.btnPrintCharIDs_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 594);
            this.Controls.Add(this.btnPrintCharIDs);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkToWorld);
            this.Controls.Add(this.lblGameWorldLogs);
            this.Controls.Add(this.txtGameWorldLogs);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.txtGameWorldIP);
            this.Controls.Add(this.txtGameWorldPort);
            this.Controls.Add(this.txtLoginPort);
            this.Controls.Add(this.txtLoginIP);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnStartLogin);
            this.Controls.Add(this.lblLoginLogs);
            this.Controls.Add(this.txtLoginLogs);
            this.Name = "MainWindow";
            this.Text = "The Chronicles of Spellborn Servers";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RichTextBox txtLoginLogs;
    private System.Windows.Forms.Label lblLoginLogs;
    private System.Windows.Forms.Button btnStartLogin;
    private System.Windows.Forms.Button btnStartGame;
    private System.Windows.Forms.TextBox txtLoginIP;
    private System.Windows.Forms.TextBox txtLoginPort;
    private System.Windows.Forms.TextBox txtGameWorldPort;
    private System.Windows.Forms.TextBox txtGameWorldIP;
    private System.Windows.Forms.Label lblIp;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.RichTextBox txtGameWorldLogs;
    private System.Windows.Forms.Label lblGameWorldLogs;
    private System.Windows.Forms.CheckBox chkToWorld;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtIERel;
    private System.Windows.Forms.Button btnIE;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtLevelObject;
    private System.Windows.Forms.TextBox txtBitfield;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtCollision;
    private System.Windows.Forms.TextBox txtIELocZ;
    private System.Windows.Forms.TextBox txtIsEnabled;
    private System.Windows.Forms.TextBox txtIELocY;
    private System.Windows.Forms.TextBox txtIERot;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtIsHidden;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtIELocX;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnTelePawn;
    private System.Windows.Forms.TextBox txtTelePawnRot;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtTelePawnRel;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtTelePawnLocZ;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtTelePawnLocY;
    private System.Windows.Forms.TextBox txtTelePawnLocX;
    private System.Windows.Forms.Button btnPrintCharIDs;

  }
}

