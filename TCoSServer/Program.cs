using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace TCoSServer
{
  public static class DataType
  {
    public const int BYTE = 1;
    public const int WORD = 2;
    public const int DWORD = 4;
    public const int QWORD = 8;
  }

  static class Program
  {

    /// <summary>
    /// Point d'entrée principal de l'application.
    /// </summary>
    [STAThread]
    static void Main ()
    {
      Application.EnableVisualStyles ();
      Application.SetCompatibleTextRenderingDefault (false);
      Login.LoginSession.initMessageHandlers ();

      
      var thread = new Thread(CommandThread);
      // allow UI with ApartmentState.STA though [STAThread] above should give that to you
      thread.TrySetApartmentState(ApartmentState.STA);
      thread.Start(); 

      Command.CommandHandler.create("jambon", 
          (l) => Console.Out.WriteLine("Je print jambon"));

      Application.Run (new MainWindow());
    }

    private static void CommandThread()
    {
        Application.Run(new Command.SimpleCommandWindow());
    }

  }
}
