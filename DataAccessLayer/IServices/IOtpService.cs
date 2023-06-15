namespace DataAccessLayer.IServices;

public interface IOtpService
{
    int GenerateOTP();

    bool ValidateOTP(int otp);
}