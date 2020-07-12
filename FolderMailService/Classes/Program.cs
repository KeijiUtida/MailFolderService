using FolderMailService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FolderMailService
{
    class Program
    {
        static void Main(string[] args)
        {
                var exitCode = HostFactory.Run(x =>
                {
                    x.Service<FileWhatcher>(s =>
                    {
                        s.ConstructUsing(fileWatcher => new FileWhatcher());
                        s.WhenStarted(fileWatcher => fileWatcher.Start());
                        s.WhenStopped(fileWatcher => fileWatcher.Stop());
                    });
                    x.RunAsLocalSystem();
                    x.SetServiceName("FolderMailService");
                    x.SetDisplayName("FolderMail Service");
                    x.SetDescription("This service sends email from a text file in a specific folder.");
                });
                int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
                Environment.ExitCode = exitCodeValue;
            }
        
    }
}
