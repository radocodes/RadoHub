using CloudinaryDotNet;

namespace RadoHub.Services.Contracts
{
    public interface ICloudinaryService
    {
        public Account CloudinaryAccount();

        public string GenerateSignature(string timestamp, string source);
    }
}
