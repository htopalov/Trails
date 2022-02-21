namespace Trails.Models.Event
{
    public class UnapprovedEventModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public UnapprovedEventDetailsModel DetailsModel { get; set; }
    }
}
