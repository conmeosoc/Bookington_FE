using System.Security.Principal;

namespace Bookington_FE.Models.ResponseModel
{
    public class AuthLoginResponse
    {
        public AccountLoginModel result { get; set; } = new AccountLoginModel();
        public AccountProfileRead profileRead { get; set; }=new AccountProfileRead();
		public int statusCode { get; set; }
        public bool isError { get; set; }
        public string message { get; set; }
    }
    public class AccountProfileResponse
    {
		public AccountProfileRead result { get; set; } = new AccountProfileRead();
		public int statusCode { get; set; }
		public bool isError { get; set; }
		public string message { get; set; }
	}
    public class AccountLoginModel
    {
        public string userId { get; set; } = string.Empty;
        public string phonenumber { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public string sysToken { get; set; } = string.Empty;
        public int sysTokenExpires { get; set; } = 120;
    }
    public class DeleteAccountResponse
    {
        public int statusCode { get; set; }
        public bool isError { get; set; }
        public string message { get; set; }
    }
    public class AccountProfileRead
    {
        public string Phone { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public virtual AccountAvatar RefAvatarNavigation { get; set; }

        public DateTime DateOfBirth { get; set; }
    }

    public partial class AccountAvatar
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string RefImage { get; set; } = null!;
    }

}
