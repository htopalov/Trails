namespace Trails.Web.Areas.Identity.Pages.Account.Contracts
{
    public interface IUserInputModel
    {
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string CountryName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
