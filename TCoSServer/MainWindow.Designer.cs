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
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
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
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "    PHYS_None = 0,",
            "    PHYS_Walking = 1,",
            "    PHYS_Falling = 2,",
            "    PHYS_Swimming = 3,",
            "    PHYS_Flying = 4,",
            "    PHYS_Rotating = 5,",
            "    PHYS_Projectile = 6,",
            "    PHYS_Interpolating = 7,",
            "    PHYS_MovingBrush = 8,",
            "    PHYS_Spider = 9,",
            "    PHYS_Trailer = 10,",
            "    PHYS_Ladder = 11,",
            "    PHYS_RootMotion = 12,",
            "    PHYS_Karma = 13,",
            "    PHYS_KarmaRagDoll = 14,",
            "    PHYS_Hovering = 15,",
            "    PHYS_CinMotion = 16,",
            "    PHYS_DragonFlying = 17,",
            "    PHYS_Jumping = 18,",
            "    PHYS_SitGround = 19,",
            "    PHYS_SitChair = 20,",
            "    PHYS_Submerged = 21,",
            "    PHYS_Turret = 22"});
      this.comboBox1.Location = new System.Drawing.Point(527, 97);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(121, 21);
      this.comboBox1.TabIndex = 13;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(527, 138);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(100, 20);
      this.textBox1.TabIndex = 14;
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(527, 178);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(100, 20);
      this.textBox2.TabIndex = 15;
      this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(814, 594);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.comboBox1);
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
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;

  }
}

