using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace Patterns.Api
{
    class Program
    {
        static int Main(string[] args)
        {
         var exitCode = HostFactory.Run(c =>
            {
                c.Service<Service>(service =>
                {
                    ServiceConfigurator<Service> s = service;
                    s.ConstructUsing(() => new Service());
                    s.WhenStarted(a => a.Start());
                    s.WhenStopped(a => a.Stop());
                });
            });

            return (int)exitCode;

        }
    }

    class Service
    {
        private IDisposable app;
        public void Start()
        {
            app = WebApp.Start("http://localhost:8095/");
        }
        public void Stop()
        {
            app.Dispose();
        }

    }



}
