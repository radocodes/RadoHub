using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RadoHub.Data.Models;

namespace RadoHub.WebApp.Data
{
    public class RadoHubDbContext : IdentityDbContext<RadoHubUser>
    {
        public RadoHubDbContext(DbContextOptions<RadoHubDbContext> options)
            : base(options)
        {
        }
    }
}
