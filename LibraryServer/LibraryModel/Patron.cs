using System;
using System.Collections.Generic;

namespace LibraryServer.LibraryModel;

public partial class Patron
{
    public int PatronId { get; set; }

    public string PatronFname { get; set; } = null!;

    public string PatronLname { get; set; } = null!;

    public string PatronAddress { get; set; } = null!;

    public int? PatronCheckedBookId { get; set; }

    public DateOnly? PatronCheckedDate { get; set; }

    public DateOnly? PatronCheckedDueDate { get; set; }

    public decimal PatronCheckedOverdueAmt { get; set; }

    public virtual Book? PatronCheckedBook { get; set; }
}
