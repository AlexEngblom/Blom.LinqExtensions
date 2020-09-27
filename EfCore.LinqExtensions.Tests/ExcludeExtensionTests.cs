using EfCore.LinqExtensions.Tests.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EfCore.LinqExtensions.Tests {

  [TestClass]
  public class ExcludeExtensionTests {

    private TestDbContext _db;

    [TestInitialize]
    public void InitializeTests() {
      _db = Create.InMemoryDbContext();
    }

    [TestMethod]
    public void Exclude_SingleMember() {
      var doc = _db.AddDocument();

      var result = _db.Documents.Exclude(x => x.FileBytes).FirstOrDefault();
      Assert.AreEqual(doc.Id, result.Id, "Id");
      Assert.AreEqual(doc.ContentType, result.ContentType, "ContentType");
      Assert.AreEqual(doc.FileName, result.FileName, "FileName");
      Assert.AreEqual(doc.Author, result.Author, "Author");
      Assert.IsNull(result.FileBytes, "FileBytes");
    }

    [TestMethod]
    public void Exclude_MultipleMembers() {
      var doc = _db.AddDocument();

      var result = _db.Documents.Exclude(x => x.Author, x => x.ContentType).FirstOrDefault();
      Assert.AreEqual(doc.Id, result.Id, "Id");
      Assert.AreEqual(doc.FileName, result.FileName, "FileName");
      Assert.AreEqual(doc.FileBytes.Length, result.FileBytes.Length, "FileBytes");
      Assert.IsNull(result.Author, "Author");
      Assert.IsNull(result.ContentType, "ContentType");
    }

    [TestMethod]
    public void Exclude_InSequence() {
      var doc = _db.AddDocument();

      var result = _db.Documents.Exclude(x => x.FileBytes).Exclude(x => x.ContentType).FirstOrDefault();
      Assert.AreEqual(doc.Id, result.Id, "Id");
      Assert.AreEqual(doc.FileName, result.FileName, "FileName");
      Assert.AreEqual(doc.Author, result.Author, "Author");
      Assert.IsNull(result.ContentType, "ContentType");
      Assert.IsNull(result.FileBytes, "FileBytes");
    }

  }
}
