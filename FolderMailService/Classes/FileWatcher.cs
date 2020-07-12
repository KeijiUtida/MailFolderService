using System;
using System.IO;
using System.Configuration;
using FolderMailService.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace FolderMailService.Classes
{
    public class FileWhatcher
    {
        public FileSystemWatcher watcher = new FileSystemWatcher();
        private string lastRead = "";
        public FileWhatcher(string folder)
        {
            watcher.Path = ConfigurationManager.AppSettings["FolderWatcherPath"];
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                  | NotifyFilters.LastWrite
                                  | NotifyFilters.FileName
                                  | NotifyFilters.DirectoryName;
            watcher.Filter = "*.mail";
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
        }     
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.FullPath != lastRead)
            {
                Console.WriteLine("The file" + e.Name + " has been changed.");
                MailViewModel mail = new MailViewModel();
                var a = new PopulateMail(e.FullPath);
                mail = a.GetMailSettings();
                lastRead = e.FullPath;
                SendEmail sendmail = new SendEmail(mail);
                sendmail.SendMail();
            }
        }
        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

    }
}
