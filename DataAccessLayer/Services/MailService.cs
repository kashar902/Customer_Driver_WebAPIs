using System.Net;
using System.Net.Mail;
using DataAccessLayer.GlobalModels;
using DataAccessLayer.IServices;

namespace DataAccessLayer.Services;

public class MailService : IMailService
{
    public async Task<string> Sendmail(MailClassModel model, string email, string password)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(email);
                model.ToMailIds.ForEach(oMailId =>
                {
                    mail.To.Add(oMailId);
                });
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                mail.IsBodyHtml = model.IsHtmlBody;
                model.Attachments.ForEach(x =>
                {
                    mail.Attachments.Add(new Attachment(x));
                });

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(email, password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                    return "Mail Sent Successfully!";
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}