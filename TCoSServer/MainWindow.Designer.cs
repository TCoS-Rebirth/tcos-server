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
      this.txtRelevance = new System.Windows.Forms.TextBox();
      this.txtLevelObject = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtIsEnabled = new System.Windows.Forms.TextBox();
      this.txtIsHidden = new System.Windows.Forms.TextBox();
      this.txtLocation = new System.Windows.Forms.TextBox();
      this.txtRotation = new System.Windows.Forms.TextBox();
      this.txtCollision = new System.Windows.Forms.TextBox();
      this.txtBitfield = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.btnSendPacket = new System.Windows.Forms.Button();
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
      // txtRelevance
      // 
      this.txtRelevance.Location = new System.Drawing.Point(575, 160);
      this.txtRelevance.Name = "txtRelevance";
      this.txtRelevance.Size = new System.Drawing.Size(100, 20);
      this.txtRelevance.TabIndex = 13;
      // 
      // txtLevelObject
      // 
      this.txtLevelObject.Location = new System.Drawing.Point(575, 205);
      this.txtLevelObject.Name = "txtLevelObject";
      this.txtLevelObject.Size = new System.Drawing.Size(100, 20);
      this.txtLevelObject.TabIndex = 14;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(584, 141);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(70, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "RelevanceID";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(584, 189);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(75, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "LevelObjectID";
      // 
      // txtIsEnabled
      // 
      this.txtIsEnabled.Location = new System.Drawing.Point(575, 253);
      this.txtIsEnabled.Name = "txtIsEnabled";
      this.txtIsEnabled.Size = new System.Drawing.Size(100, 20);
      this.txtIsEnabled.TabIndex = 17;
      // 
      // txtIsHidden
      // 
      this.txtIsHidden.Location = new System.Drawing.Point(575, 299);
      this.txtIsHidden.Name = "txtIsHidden";
      this.txtIsHidden.Size = new System.Drawing.Size(100, 20);
      this.txtIsHidden.TabIndex = 18;
      // 
      // txtLocation
      // 
      this.txtLocation.Location = new System.Drawing.Point(575, 350);
      this.txtLocation.Name = "txtLocation";
      this.txtLocation.Size = new System.Drawing.Size(100, 20);
      this.txtLocation.TabIndex = 19;
      // 
      // txtRotation
      // 
      this.txtRotation.Location = new System.Drawing.Point(575, 394);
      this.txtRotation.Name = "txtRotation";
      this.txtRotation.Size = new System.Drawing.Size(100, 20);
      this.txtRotation.TabIndex = 20;
      // 
      // txtCollision
      // 
      this.txtCollision.Location = new System.Drawing.Point(575, 446);
      this.txtCollision.Name = "txtCollision";
      this.txtCollision.Size = new System.Drawing.Size(100, 20);
      this.txtCollision.TabIndex = 21;
      // 
      // txtBitfield
      // 
      this.txtBitfield.Location = new System.Drawing.Point(575, 489);
      this.txtBitfield.Name = "txtBitfield";
      this.txtBitfield.Size = new System.Drawing.Size(100, 20);
      this.txtBitfield.TabIndex = 22;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(584, 237);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(54, 13);
      this.label3.TabIndex = 23;
      this.label3.Text = "IsEnabled";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(584, 283);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 13);
      this.label4.TabIndex = 24;
      this.label4.Text = "isHidden";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(584, 334);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(48, 13);
      this.label5.TabIndex = 25;
      this.label5.Text = "Location";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(584, 378);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(47, 13);
      this.label6.TabIndex = 26;
      this.label6.Text = "Rotation";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(584, 417);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(69, 13);
      this.label7.TabIndex = 27;
      this.label7.Text = "CollisionType";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(584, 473);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(38, 13);
      this.label8.TabIndex = 28;
      this.label8.Text = "Bitfield";
      // 
      // btnSendPacket
      // 
      this.btnSendPacket.Location = new System.Drawing.Point(556, 515);
      this.btnSendPacket.Name = "btnSendPacket";
      this.btnSendPacket.Size = new System.Drawing.Size(142, 23);
      this.btnSendPacket.TabIndex = 29;
      this.btnSendPacket.Text = "Send packet";
      this.btnSendPacket.UseVisualStyleBackColor = true;
      this.btnSendPacket.Click += new System.EventHandler(this.btnSendPacket_Click);
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(814, 594);
      this.Controls.Add(this.btnSendPacket);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtBitfield);
      this.Controls.Add(this.txtCollision);
      this.Controls.Add(this.txtRotation);
      this.Controls.Add(this.txtLocation);
      this.Controls.Add(this.txtIsHidden);
      this.Controls.Add(this.txtIsEnabled);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtLevelObject);
      this.Controls.Add(this.txtRelevance);
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
    private System.Windows.Forms.TextBox txtRelevance;
    private System.Windows.Forms.TextBox txtLevelObject;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtIsEnabled;
    private System.Windows.Forms.TextBox txtIsHidden;
    private System.Windows.Forms.TextBox txtLocation;
    private System.Windows.Forms.TextBox txtRotation;
    private System.Windows.Forms.TextBox txtCollision;
    private System.Windows.Forms.TextBox txtBitfield;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Button btnSendPacket;

  }
}

