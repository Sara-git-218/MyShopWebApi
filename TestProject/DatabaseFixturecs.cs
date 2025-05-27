using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{

public class DatabaseFixture : IDisposable
    {
        public _326059268_ShopApiContext DbContext { get; private set; }

        public DatabaseFixture()
        {
            //string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;";
            string connectionString = "Server=SRV2\\PUPILS;Database=326059268_TestDb;Trusted_Connection=True;;Integrated Security=True;TrustServerCertificate=True";
            //optionsBuilder.UseSqlServer(connectionString);
            var options = new DbContextOptionsBuilder<_326059268_ShopApiContext>()
                .UseSqlServer(connectionString) // אפשר לשנות ל־UseSqlite לפי צורך
                .Options;

            DbContext = new _326059268_ShopApiContext(options);

            // ודא שהמסד נוצר
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
            //DbContext.Database.EnsureDeleted();

        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }


}

