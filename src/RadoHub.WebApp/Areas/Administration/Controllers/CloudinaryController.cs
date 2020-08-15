﻿using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Contracts;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    public class CloudinaryController : AdministrationControllerBase
    {
        private readonly ICloudinaryService cloudinaryService;

        public CloudinaryController(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult GenerateSignature()
        {
            var timestamp = Request.Query["data[timestamp]"];
            var source = Request.Query["data[source]"];

            string signature = this.cloudinaryService.GenerateSignature(timestamp, source);
            return Ok(signature);
        }
    }
}