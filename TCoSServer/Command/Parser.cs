using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.Command
{
    class Parser
    {

        public static Command Parse (String line)
        {
            var arr = line.Split(' ');

            var l = arr.Aggregate(new List<String>(),
                (a, b) => { a.Add(b); return a; });

            if (l.Any())
                throw new ArgumentException();

            return new Command(l);
        }



    }

}
