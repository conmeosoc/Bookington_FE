namespace Bookington_FE.Models.ResponseModel
{
    public class SubcourtResquest
    {
        public string parentCourtId { get; set; } 
        public string name { get; set; } 
        public int courtTypeId { get; set; } 
        public bool isActive { get; set; } 
        public bool isDeleted { get; set; }
        
    }
}
