namespace Bookington_FE.Models.ResponseModel
{
    public class CourtReportReply
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; } = null!;

        public virtual ICollection<CourtReportResponse> CourtReports { get; } = new List<CourtReportResponse>();
    }
}
