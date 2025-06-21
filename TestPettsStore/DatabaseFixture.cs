using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
   
        public class DatabaseFixture : IDisposable
        {
            public PettsStoreContext Context { get; private set; }

            public DatabaseFixture()
            {
                // Set up the test database connection and initialize the context
                var options = new DbContextOptionsBuilder<PettsStoreContext>()
                    //.UseSqlServer("Data Source=srv2\\pupils;Initial Catalog=328277439_PettsStoreTest;Integrated Security=True;Trust Server Certificate=True")
                    .UseSqlServer("Server=srv2\\pupils;Database=328277439_PettsStoreTest;Trusted_Connection=True;TrustServerCertificate=True")
                    .Options;
                Context = new PettsStoreContext(options);
                Context.Database.EnsureCreated();// create the data base
            }

            public void Dispose()
            {
                // Clean up the test database after all tests are completed
                Context.Database.EnsureDeleted();
                Context.Dispose();
            }
        }
    }
