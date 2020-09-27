using Blom.LinqExtensions.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blom.LinqExtensions.Tests.TestSupport {
  public class TestDbContext : DbContext {
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    public DbSet<Document> Documents { get; set; }

    public Document AddDocument() {
      var doc = Create.Document();
      Documents.Add(doc);
      SaveChanges();
      Entry(doc).State = EntityState.Detached;
      return doc;
    }
  }
}
