
namespace So2Baladna.Core.Dto
{
    public class EmailDto
    {

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailDto(string to,string from,string subject,string content) { 

            this.To = to;
            this.From = from;
            this.Subject = subject;
            this.Content = content;
        }
    }
}
