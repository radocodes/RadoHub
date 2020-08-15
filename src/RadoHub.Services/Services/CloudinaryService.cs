﻿using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using RadoHub.Data.Models;
using RadoHub.Services.Contracts;
using System.Collections.Generic;

namespace RadoHub.Services.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryConfigs cloudinaryConfigs;
        private readonly Cloudinary cloudinary;
        private readonly Account cloudinaryAccount;

        public CloudinaryService(IOptions<CloudinaryConfigs> cloudinaryConfigsAccessor)
        {
            this.cloudinaryConfigs = cloudinaryConfigsAccessor.Value;

            this.cloudinaryAccount = new Account(
                this.cloudinaryConfigs.CloudName,
                this.cloudinaryConfigs.ApiKey,
                this.cloudinaryConfigs.ApiSecret);

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

        public string GenerateSignature(string timestamp, string source)
        {
            string SignedUploadPreset = "ml_default";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("source", source);
            parameters.Add("timestamp", timestamp);
            parameters.Add("upload_preset", SignedUploadPreset);

            string signature = cloudinary.Api.SignParameters(parameters);
            return signature;
        }
    }
}
