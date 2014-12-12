using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageExtractor
{
  class Program
  {
    
     static void Main (string[] args)
    {
      Package pack = new Package (@"G:\TCoS0.9\data\environment\maps\PT_Hawksmouth.sbw");
      pack.ReadLocalizedStrings (@"G:\TCoS0.9\data\static\descriptions.s");
      pack.Load (@"G:\TCoS0.9\data\scripts\client\SBGame");
     
      Console.ReadKey ();
    }

  }
}
