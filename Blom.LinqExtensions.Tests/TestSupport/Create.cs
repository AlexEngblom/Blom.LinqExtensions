﻿using Blom.LinqExtensions.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Blom.LinqExtensions.Tests.TestSupport {
  public static class Create {

    private static ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

    /// <summary>
    /// Used to validate query translation while we wait for https://blog.oneunicorn.com/2020/01/12/toquerystring/ being published
    /// </summary>
    public static TestDbContext RealDbContext() {
      var options = new DbContextOptionsBuilder<TestDbContext>()
        .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=testi;Trusted_Connection=True;")
        .UseLoggerFactory(loggerFactory)
        .Options;

      var database = new TestDbContext(options);
      database.Database.EnsureDeleted();
      database.Database.EnsureCreated();

      return database;
    }

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
