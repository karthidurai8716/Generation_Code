using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GenerationServer.Models;

namespace GenerationServer
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            :base(options) { }

        public DbSet<DiscountCodes> DiscountCodes { get; set; }


    }

}