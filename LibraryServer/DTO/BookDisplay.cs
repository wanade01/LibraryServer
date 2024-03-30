namespace LibraryServer.DTO
{
    public class BookDisplay
    { 
        public required string BookTitle { get; set; }
        public required string BookAuthor { get; set; }
        public required string BookPublisher { get; set; }
        public required string BookPublishYear { get; set; }
        public required string BookISBN10 { get; set; }
        public required string BookISBN13 { get; set; }
        public required string BookGenre { get; set; }

    }
}
