using System.ComponentModel.DataAnnotations;

namespace Bookington_FE.Models.ResponseModel
{
    public class CourtReportResponse
    {
        public List<CourtReportModel> result { get; set; } = new List<CourtReportModel>();
        public List<CourtReportResponseWriteDTO> response { get; set; } = new List<CourtReportResponseWriteDTO>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }

    public class CourtReportModel
    {
        public string Id { get; set; }

        public string RefCourt { get; set; }

        public string CourtName { get; set; } 

        public string ReporterId { get; set; }

        public string ReporterPhone { get; set; } 

        public string ReporterName { get; set; } 

        public string Content { get; set; }

        public bool IsResponded { get; set; }

        public bool IsBan { get; set; } = false;

    }
	public class CourtReportResponseWriteDTO
	{
		[Required]
		public string CourtReportId { get; set; } = null!;

		[Required]
		public string Content { get; set; } = null!;

		public bool IsBanned { get; set; } = false;

		public int Duration { get; set; } = 0;
	}
}
