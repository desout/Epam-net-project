namespace DAL.models
{
    public class Layout
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public string LayoutName { get; set; }
        public string Description { get; set; }
    }
}