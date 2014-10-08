using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.Command
{
    class CommandScheme <T>
    {

        Func<List<String>> f;

        public CommandScheme (Func<List<String>> f)
        {
            this.f = f;    
        }


    }


}
