using System.Collections.Generic;

namespace FolderMailService.ViewModel
{
    public class MailViewModel
    {
        public string SENDERNAME { get; set; }
        public string SENDER { get; set; }
        public List<string> RECEIVERS { get; set; }
        public string SUBJECT { get; set; }
        public string MESSAGE { get; set; }        
    }
}
