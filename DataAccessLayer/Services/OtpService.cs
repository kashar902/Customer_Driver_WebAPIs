using DataAccessLayer.GlobalAndCommon;
using DataAccessLayer.IServices;

namespace DataAccessLayer.Services;

public class OtpService: IOtpService
{
    public int GenerateOTP()
    {
        Random random = new Random();
        GlobalVariables.Otp = random.Next(1000, 10000);

        Timer timer = new Timer(_ =>
        {
            GlobalVariables.Otp = 0;
        }, null, TimeSpan.FromMinutes(2), Timeout.InfiniteTimeSpan);

        return GlobalVariables.Otp;
    }

    public bool ValidateOTP(int otp) =>
        GlobalVariables.Otp.Equals(otp);
}