using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderMailService.ViewModel;

namespace FolderMailService.Classes
{
    public class PopulateMail
    {
        private List<TextItem> textItems = new List<TextItem>();
        public string fileName { get; set; }
        public PopulateMail(string _nameFile)
        {
            fileName = _nameFile;
            ReadText();
        }
        public MailViewModel GetMailSettings()
        {
            return new MailViewModel()
            {
                SENDER = textItems.Where(x => x.Variable == "FROM").Select(x => x.Value).FirstOrDefault(),
                RECEIVERS = textItems.Where(x => x.Variable == "TO").Select(x => x.Value).FirstOrDefault().Split(';').ToList(),
                SUBJECT = textItems.Where(x => x.Variable == "SUBJECT").Select(x => x.Value).FirstOrDefault(),
                MESSAGE = textItems.Where(x => x.Variable == "MESSAGE").Select(x => x.Value).FirstOrDefault(),
                SENDERNAME = textItems.Where(x => x.Variable == "SENDERNAME").Select(x => x.Value).FirstOrDefault()
            };
        }
        private void ReadText()
        {
            int counter = 0;string line;           
            StreamReader file = new StreamReader(fileName);
            string text = File.ReadAllText(fileName);
            while ((line = file.ReadLine()) != null)
            {
                var lines = line.Split(':');
                textItems.Add(new TextItem()
                {
                    Variable = lines.FirstOrDefault().ToUpper(),
                    Value = lines.LastOrDefault()
                });
                counter++;
            }
            file.Close();
        }
    }
}
