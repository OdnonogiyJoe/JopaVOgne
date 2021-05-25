using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDisappearanceTrick.Entities;

namespace TheDisappearanceTrick
{
    class DB : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<LostSoul> LostSouls { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductList> ProductLists { get; set; }
        public DbSet<Workshop> Workshops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //https://hh.ru/vacancy/44172512 //https://hh.ru/vacancy/44332778 //https://github.com/OdnonogiyJoe/MnogoBykav/files/6535539/default.docx
        {
            var sb = new SqlConnectionStringBuilder();
            sb.DataSource = @"DESKTOP-G7E40EF";
            sb.InitialCatalog = "Vsplil";
            sb.IntegratedSecurity = true;
            optionsBuilder.UseSqlServer(sb.ToString());
            base.OnConfiguring(optionsBuilder);
        }

        private DB()
        {
            Database.EnsureCreated();
        }

        static DB db;
        public static DB GetDb()
        {
            if (db == null)
                db = new DB();
            return db;
        }
    }
}
