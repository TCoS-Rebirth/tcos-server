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
using TCoSServer.GameServer.Network.Packets;

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

    private void btnIE_Rel (object sender, EventArgs e)
    {
      s2c_interactivelevelelement_add packet = new s2c_interactivelevelelement_add ();
      packet.RelevanceID = Int32.Parse(txtIERel.Text);
      packet.NetRotation = new GameServer.Network.Structures.FVector ();
      packet.NetLocation = new GameServer.Network.Structures.FVector ();
      packet.NetRotation.Y = Int32.Parse(txtIERot.Text); //Rotate on Y-axis only
      packet.NetLocation.X = Int32.Parse(txtIELocX.Text);
      packet.NetLocation.Y = Int32.Parse(txtIELocY.Text);
      packet.NetLocation.Z = Int32.Parse(txtIELocZ.Text);
      packet.LevelObjectID = Int32.Parse (txtLevelObject.Text);
      packet.IsEnabledBitfield = Int32.Parse (txtIsEnabled.Text);
      packet.IsHidden = Int32.Parse (txtIsHidden.Text);
      packet.CollisionType = Byte.Parse (txtCollision.Text);
      packet.ActivatedRespawnTimerBitfield = Int32.Parse (txtBitfield.Text);
      gameServer.DebugSendMessageToEveryone (packet.Generate ());
    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {

    }

    private void txtTelePawnLocY_TextChanged(object sender, EventArgs e)
    {

    }


    private void btnPrintCharIDs_Click(object sender, EventArgs e)
    {
       // if (gameServer.getNumPlayers()) 
        int numP = gameServer.getNumPlayers();
        Console.WriteLine(numP + " players connected");
        if (gameServer.getNumPlayers() != 0) { gameServer.PrintPlayerIDsList();}
    }

    private void btnTelePawn_Click_1(object sender, EventArgs e)
    {
        s2r_game_pawn_sv2clrel_teleportto packet = new s2r_game_pawn_sv2clrel_teleportto();
        packet.RelevanceID = Int32.Parse(txtTelePawnRel.Text);
        packet.NewLocation = new GameServer.Network.Structures.FVector();
        packet.NewRotation = new GameServer.Network.Structures.FRotator();
        packet.NewRotation.Yaw = Int32.Parse(txtTelePawnRot.Text); //Rotate on Y-axis only
        packet.NewLocation.X = Int32.Parse(txtTelePawnLocX.Text);
        packet.NewLocation.Y = Int32.Parse(txtTelePawnLocY.Text);
        packet.NewLocation.Z = Int32.Parse(txtTelePawnLocZ.Text);
        gameServer.DebugSendMessageToEveryone(packet.Generate());
    }


  }
}
