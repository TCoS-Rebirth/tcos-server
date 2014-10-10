using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCoSPackageExplorer
{
  public partial class Form1 : Form
  {
    public Form1 ()
    {
      InitializeComponent ();
    }

    private void Form1_Load (object sender, EventArgs e)
    {
      
    }

    private void openFileDialog1_FileOk (object sender, CancelEventArgs e)
    {

    }

    private void button1_Click (object sender, EventArgs e)
    {
      DialogResult result = openFileDialog1.ShowDialog ();
      if (result == DialogResult.OK)
      {
        Console.WriteLine ("Opened file: {0}", openFileDialog1.FileName);
        FileInfo info = new FileInfo(openFileDialog1.FileName);
        long size = info.Length;
        using (var file = MemoryMappedFile.CreateFromFile (openFileDialog1.FileName, FileMode.Open))
        {
          using (var view = file.CreateViewAccessor (0, size))
          {
          }
        }
      }
    }
  }
}
