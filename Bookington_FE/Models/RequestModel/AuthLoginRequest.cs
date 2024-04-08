namespace Bookington_FE.Models.RequestModel
{
    public class AuthLoginRequest
    {
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UpdateAccountRequest
    {
        public string fullName { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
    }
    public class UpdateCourtRequest
    {
		public string OwnerId { get; set; } = string.Empty;

		public string DistrictId { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public TimeSpan OpenAt { get; set; } = TimeSpan.Zero;

		public TimeSpan CloseAt { get; set; } = TimeSpan.Zero;
	}

    public class UpdateRoleRequest
    {
        public string RoleId { get; set; }= string.Empty;
    }
    public class UpdateNotificationRequest
    {
        public string NotiId { get;set; } = string.Empty;
        public bool IsRead { get; set; } = false;

    }

    public class CreateCourtRequest
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? DistrictName { get; set; }

        public string? ProvinceName { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }





        public TimeSpan? OpenAt { get; set; }

        public TimeSpan? CloseAt { get; set; }

        public bool IsActive { get; set; }

        public IFormFile CourtPictures { get; set; }
    }
}
