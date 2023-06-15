using DataAccessLayer.GlobalModels;

namespace DataAccessLayer.IServices;

public interface IMailService
{
    Task<string> Sendmail(MailClassModel model, string email, string password);

}