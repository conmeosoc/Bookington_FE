namespace Bookington_FE.Models.ResponseModel
{
	public class SubCourtAllModel
	{
		public List<SubCourtDetails> SubCourtDetails { get; set; } = new List<SubCourtDetails>();
		
		public CourtModel courtParent { get; set; } = new CourtModel();
	}
	public class SubCourtDetails
	{
		public SubcourtModel subcourtModel { get; set; } = new SubcourtModel();
        public Dictionary<string, List<SlotModel>> GroupSlotByTime { get; set; } = new Dictionary<string, List<SlotModel>>();
		public Dictionary<string, List<BookingModel>> GroupBookingByTime { get; set; } = new Dictionary<string, List<BookingModel>>();
		public List<BookingModel> bookingModel { get; set; } = new List<BookingModel>();
        public List<SlotModel> slotModel { get; set; }	= new List<SlotModel> { };
    }
}
