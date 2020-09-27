namespace EfCore.LinqExtensions.Tests.Entities {
  public class Document {
		public int Id { get; set; }
		public string ContentType { get; set; }
		public byte[] FileBytes { get; set; }
		public string FileName { get; set; }
		public string Author { get; set; }
	}
}
