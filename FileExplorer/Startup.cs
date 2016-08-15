using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(FileExplorer.Startup))]

namespace FileExplorer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            
        }
    }
}
