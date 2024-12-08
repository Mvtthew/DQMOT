namespace DQMOT.Services;

public interface ICipherService
{
    public string Encrypt(string data);
    
    public string Decrypt(string data);
}