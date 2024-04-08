namespace Bookington_FE.Models.ResponseModel
{
	public class CourtResponse
	{
		public List<CourtModel> result { get; set; } = new List<CourtModel>();
		public Pagination pagination { get; set; } = new Pagination();
        public int statusCode { get; set; }
		public bool isError { get; set; } = false;
		public string message { get; set; } = string.Empty;
	}
	public class Pagination
	{
		public int totalCount { get; set; } = 0;
		public int maxPageSize { get; set; } = 0;
		public int currentPage { get; set; } = 1;
		public int totalPages { get; set; } = 0;
		public bool hasNext { get; set; } = false;
		public bool hasPrevious { get; set; } = false;
    }

    public class CourtModel
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string OwnerId { get; set; } = null!;

		public string DistrictName { get; set; } = null!;
		public string ProvinceName { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string? Address { get; set; }

		public string? Description { get; set; }

		public TimeSpan OpenAt { get; set; }

		public TimeSpan CloseAt { get; set; }

		public DateTime CreateAt { get; set; } = DateTime.Now;

		public bool IsActive { get; set; } = false;

		public bool IsDeleted { get; set; } = false;

	}
}
