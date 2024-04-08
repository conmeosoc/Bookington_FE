using System.ComponentModel.DataAnnotations;

namespace Bookington_FE.Models.ResponseModel
{
    public class UserReportResponse
    {
        public List<UserReportModel> result { get; set; } = new List<UserReportModel>();
        public List<UserReportResponseWriteDTO> response { get; set; } = new List<UserReportResponseWriteDTO>();

        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }

    public class UserReportModel
    {
        public string Id { get; set; }

        public string RefUser { get; set; }

        public string RefUserName { get; set; }

        public string ReporterId { get; set; }

        public string ReporterCourtName { get; set; }

        public string Content { get; set; }

        public bool IsResponded { get; set; }

        public bool IsBan { get; set; }
    }
    public class UserReportResponseWriteDTO
    {
        [Required]
        public string UserReportId { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public bool IsBanned { get; set; } = false;

        public int Duration { get; set; } = 0;
    }
}
