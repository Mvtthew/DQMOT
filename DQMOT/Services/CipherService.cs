using Microsoft.AspNetCore.DataProtection;

namespace DQMOT.Services;

public class CipherService : ICipherService
{
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly IConfiguration Configuration;
    private readonly string encryptionKey;
    
    public CipherService(
        IDataProtectionProvider dataProtectionProvider,
        IConfiguration configuration)
    {
        _dataProtectionProvider = dataProtectionProvider;
        Configuration = configuration;
        
        encryptionKey = Configuration["Encryption:Key"] ?? throw new ArgumentNullException(nameof(Configuration));
    }
    
    public string Encrypt(string data)
    {
        var protector = _dataProtectionProvider.CreateProtector(encryptionKey);
        return protector.Protect(data);
    }
    
    public string Decrypt(string data)
    {
        var protector = _dataProtectionProvider.CreateProtector(encryptionKey);
        return protector.Unprotect(data);
    }
}