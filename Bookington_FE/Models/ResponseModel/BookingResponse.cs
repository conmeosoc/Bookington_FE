namespace Bookington_FE.Models.ResponseModel
{
    public class BookingResponse
    {
        public List<BookingModel> result { get; set; } = new List<BookingModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }


    public class BookingModel
    {
        public string? Id { get; set; }

        public string? SubCourtName { get; set; }

        public string? TimeSlot { get; set; }

        public string? Customer { get; set; }

        public string? Phone { get; set; }

        public DateTime? BookAt { get; set; }

        public double? Price { get; set; }

    }
}
