namespace LibraryServer.Data
{
    public class PatronCSV
    {
        public string PatronFName { get; set; } = null!;
        public string PatronLName { get; set; } = null!;
        public string PatronAddress { get; set; } = null!;
        public int PatronCheckedBookID { get; set; }
        public DateOnly PatronCheckedDate { get; set; }
        public DateOnly PatronCheckedDueDate { get; set; }
        public decimal PatronCheckedOverdueAmt { get; set; }
    }
}
