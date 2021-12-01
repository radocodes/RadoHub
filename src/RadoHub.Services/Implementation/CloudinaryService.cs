using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using RadoHub.Data.Models.AppConfigModels;
using RadoHub.Services.Contracts;
using System.Collections.Generic;

namespace RadoHub.Services.Implementation
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryConfig cloudinaryConfig;
        private readonly Cloudinary cloudinary;
        private readonly Account cloudinaryAccount;

        public CloudinaryService(IOptions<CloudinaryConfig> cloudinaryConfigAccessor)
        {
            this.cloudinaryConfig = cloudinaryConfigAccessor.Value;

            this.cloudinaryAccount = new Account(
                this.cloudinaryConfig.CloudName,
                this.cloudinaryConfig.ApiKey,
                this.cloudinaryConfig.ApiSecret);

            this.cloudinary = new Cloudinary(this.cloudinaryAccount);
        }

        public Cloudinary GetCloudinaryInstance()
        {
            return this.cloudinary;
        }

        public Account CloudinaryAccount()
        {
            return this.cloudinaryAccount;
        }

        public string GenerateSignature(string timestamp, string source, string folder)
        {
            string SignedUploadPreset = "ml_default";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("folder", folder);
            parameters.Add("source", source);
            parameters.Add("timestamp", timestamp);
            parameters.Add("upload_preset", SignedUploadPreset);

            string signature = cloudinary.Api.SignParameters(parameters);
            return signature;
        }
    }
}
