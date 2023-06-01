namespace DAL.GlobalModels;

public class MailClassModel
{
    public List<string> ToMailIds { get; set;} = new List<string>();
    public string Subject { get; set; } =   string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool  IsHtmlBody { get; set; }  = true;
    public List<string> Attachments { get; set; } = new List<string>();
}