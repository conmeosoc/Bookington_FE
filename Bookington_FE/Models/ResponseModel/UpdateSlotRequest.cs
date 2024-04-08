namespace Bookington_FE.Models.ResponseModel
{
    public class UpdateSlotRequest
    {
        public string Id { get; set; } = string.Empty;
        public double Price { get; set;} = 0;
        public bool IsActive { get; set; } = false;
    }
}
