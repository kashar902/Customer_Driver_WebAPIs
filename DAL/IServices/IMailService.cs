using DAL.GlobalModels;

namespace DAL.IServices;

public interface IMailService
{
    Task<string> Sendmail(MailClassModel model, string email, string password);

}