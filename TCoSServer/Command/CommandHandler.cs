using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.Command
{

    
    class CommandHandler
    {
        private static Dictionary<String, Action<List<String>>> dictionnary = new Dictionary<string, Action<List<String>>>();

        public static void create(String s, Action<List<String>> f) 
        {
            dictionnary.Add(s, f);
        }

        public static Action<List<String>> lookup(String key)
        {
            return dictionnary[key];
        }

        public static void run(Command c)
        {
            try
            {
                var f = CommandHandler.lookup(c.Name);
                f(c.Args);
            }
            catch
            {
                Console.Out.WriteLine(c.Name + " : command not found");
            }

        }


    }
}
