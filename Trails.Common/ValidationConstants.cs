namespace Trails.Common
{
    public static class ValidationConstants
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 5;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 3000;
        public const int UsernameMaxLength = 50;
        public const int UsernameMinLength = 5;
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 2;
        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 2;
        public const int CountryNameMaxLength = 50;
        public const int CountryNameMinLength = 2;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 50;
        public const int SimCardNumberLength = 13;
        public const int EventNameMinLength = 5;
        public const int EventNameMaxLength = 50;
        public const int RouteNameMinLength = 5;
        public const int RouteNameMaxLength = 100;
        public const int EventMinLength = 1;
        public const double RouteMinLength = 0.0;
        public const double MinAltitude = 0.0;
        public const double MaxAltitude = double.MaxValue;
        public const double MinSpeed = 0.0;
        public const double MaxSpeed = double.MaxValue;
        public const int FullnameMinLength = 2;
        public const int FullnameMaxLength = 50;
        public const int MessageMinLength = 20;
        public const int MessageMaxLength = 2000;

        public const string PhonePattern = @"(\+359|0)[0-9]{9}";
        public const string ImeiPattern = @"[0-9]{15}";
    }
}
