using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using TCoSServer.Common;

namespace TCoSServer
{
  public partial class MainWindow : Form
  {
    private Thread loginServerThread;
    private Thread gameServerThread;
    private Login.LoginServer loginServer;
    private GameServer.GameServer gameServer;

    private bool loginIpFilled = false;
    private bool loginPortFilled = false;
    private bool gameWorldIpFilled = false;
    private bool gameWorldPortFilled = false;

    //DEBUG REMOVE ME
    public static byte PhysicMode = 0;
    public static Int32 UnkownValue = 0;
    public static byte FrozenFlags = 0;
 
    public MainWindow ()
    {
      InitializeComponent ();
    }

    private void MainWindow_FormClosed (object sender, EventArgs e)
    {
      if (loginServer != null)
      {
        loginServer.StopListening ();
        loginServerThread.Join (10);
      }
      if (gameServer != null)
      {
        gameServer.StopServer ();
        gameServerThread.Join (10);
      }
    }

    private void btnStartLogin_Click (object sender, EventArgs e)
    { 
      loginServer = new Login.LoginServer (txtLoginIP.Text, txtLoginPort.Text);
      if (!loginServer.isValid)
      {
        MessageBox.Show ("Error: Address and/or port is invalid");
        return;
      }

      loginServerThread = new Thread (new ThreadStart (loginServer.StartListening));
      loginServerThread.Name = "LoginServer";
      loginServerThread.Start ();
      btnStartLogin.Enabled = false;
      btnStartLogin.Text = "Login server started";
    }

    private void btnStartGame_Click (object sender, EventArgs e)
    {
      gameServer = new GameServer.GameServer (txtGameWorldIP.Text, txtGameWorldPort.Text);
      if (!gameServer.isValid)
      {
        MessageBox.Show ("Error: Address and/or port is invalid");
        return;
      }

      gameServerThread = new Thread (new ThreadStart (gameServer.StartServer));
      gameServerThread.Name = "GameServer";
      gameServerThread.Start ();
      btnStartGame.Enabled = false;
      btnStartGame.Text = "Game server started";
    }

    private void txtLoginIP_TextChanged (object sender, EventArgs e)
    {
      if (txtLoginIP.Text.Length > 0)
        loginIpFilled = true;
      else
        loginIpFilled = false;

      switchLoginButton ();
    }

    private void txtLoginPort_TextChanged (object sender, EventArgs e)
    {
      if (txtLoginPort.Text.Length > 0)
        loginPortFilled = true;
      else
        loginPortFilled = false;

      switchLoginButton ();
    }

    private void switchLoginButton ()
    {
      if (loginIpFilled && loginPortFilled)
        btnStartLogin.Enabled = true;
      else
        btnStartLogin.Enabled = false;
    }

    private void switchGameButton ()
    {
      if (gameWorldIpFilled && gameWorldPortFilled)
        btnStartGame.Enabled = true;
      else
        btnStartGame.Enabled = false;
    }

    private void MainWindow_Load (object sender, EventArgs e)
    {

    }

    private void txtGameWorldIP_TextChanged (object sender, EventArgs e)
    {
      if (txtGameWorldIP.Text.Length > 0)
        gameWorldIpFilled = true;
      else
        gameWorldIpFilled = false;

      switchGameButton ();
    }

    private void txtGameWorldPort_TextChanged (object sender, EventArgs e)
    {
      if (txtGameWorldPort.Text.Length > 0)
        gameWorldPortFilled = true;
      else
        gameWorldPortFilled = false;

      switchGameButton ();
    }

    private void chkToWorld_CheckedChanged (object sender, EventArgs e)
    {
      GameServer.GameServer.BypassCharacterScreen = !GameServer.GameServer.BypassCharacterScreen;
    }

    private void comboBox1_SelectedIndexChanged (object sender, EventArgs e)
    {
      PhysicMode = (byte)comboBox1.SelectedIndex;
    }

    private void textBox1_TextChanged (object sender, EventArgs e)
    {
      UnkownValue = Int32.Parse (textBox1.Text);
    }

    private void textBox2_TextChanged (object sender, EventArgs e)
    {
      FrozenFlags = Byte.Parse(textBox2.Text);
    }
  }
}
