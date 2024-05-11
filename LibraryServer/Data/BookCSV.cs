namespace LibraryServer.Data
{
    public class BookCSV
    {
        public string BookIsbn13 { get; set; } = null!;
        public string BookIsbn10 { get; set; } = null!;
        public string BookTitle { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookPublisher { get; set; } = null!;
        public short BookPublishYear { get; set; }
        public string BookGenre { get; set; } = null!;

    }
}
