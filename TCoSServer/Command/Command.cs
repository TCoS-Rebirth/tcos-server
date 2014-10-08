using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.Command
{
    class Command
    {

        public List<String> Args{set;get;}
        public String Name{set;get;}

        public Command()
        {
            this.Args = new List<String>();
        }

        public Command(List<String> args)
        {
            this.Name = args.First();
            this.Args = args;
        }

        override public String ToString ()
        {
            return Args.Aggregate("", 
                (a, b) => { return a + "'" + b + "' "; } );
        }

   
    }
}
