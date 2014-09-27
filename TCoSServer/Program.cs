using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

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

      Application.Run (new MainWindow ());
    }
  }
}
