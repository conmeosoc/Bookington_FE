namespace Bookington_FE.Models.ResponseModel
{
    public class SubcourtResponse
    {
        public List<SubcourtModel> result { get; set; } = new List<SubcourtModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
    
    public class SubcourtModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = null!;

        public string ParentCourtId { get; set; } = null!;

        public string CourtTypeId { get; set; } = null!;

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public int SlotDuration { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
    }
}
