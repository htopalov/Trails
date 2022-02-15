namespace Trails.Data.DomainModels
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public byte[] DataBytes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatorId { get; set; }
        public User Creator { get; set; }

    
    }
}
