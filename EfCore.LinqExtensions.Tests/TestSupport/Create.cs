using EfCore.LinqExtensions.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCore.LinqExtensions.Tests.TestSupport {
  public static class Create {

    public static TestDbContext InMemoryDbContext() {
      var options = new DbContextOptionsBuilder<TestDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

      return new TestDbContext(options);
    }

    public static Document Document() {
      return new Document {
        ContentType = "text/plain",
        FileName = "Lyrics sheet",
        FileBytes = new byte[] { 77, 195, 164, 32, 97, 106, 97, 110, 32, 107, 111, 107, 111, 32, 121, 195, 182, 110, 44, 32, 78, 105, 105, 110, 32, 107, 117, 105, 110, 32, 82, 111, 121, 32, 79, 114, 98, 105, 115, 111, 110 },
        Author = "Stig"
      };
    }

  }
}
