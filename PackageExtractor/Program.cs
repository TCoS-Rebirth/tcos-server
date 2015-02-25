using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace PackageExtractor
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

      Application.Run (new MainWindow ());
      
    }
  }
}
