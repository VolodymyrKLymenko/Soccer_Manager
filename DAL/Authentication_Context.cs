using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model_Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Authentication_Context: IdentityDbContext<User>
    {
        public Authentication_Context(DbContextOptions<Authentication_Context> options): base(options)
        {
        }
    }
}
