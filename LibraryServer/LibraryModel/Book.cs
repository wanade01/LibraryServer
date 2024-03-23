using System;
using System.Collections.Generic;

namespace LibraryServer.LibraryModel;

public partial class Book
{
    public int BookId { get; set; }

    public string BookIsbn13 { get; set; } = null!;

    public string BookIsbn10 { get; set; } = null!;

    public string BookTitle { get; set; } = null!;

    public string BookAuthor { get; set; } = null!;

    public string BookPublisher { get; set; } = null!;

    public short BookPublishYear { get; set; }

    public string BookGenre { get; set; } = null!;

    public virtual ICollection<Patron> Patrons { get; set; } = new List<Patron>();
}
