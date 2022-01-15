namespace Trails.Web.Data
{
    public class DataConstants
    {
        public class Common
        {
            public const int NameMaxLength = 100;
        }
        public class Event
        {
            public const int DescriptionMaxLength = 3000;
        }

        public class User
        {
            public const int UsernameMaxLength = 50;
            public const int UsernameMinLength = 5;
            public const int FirstNameMaxLength = 50;
            public const int FirstNameMinLength = 2;
            public const int LastNameMaxLength = 50;
            public const int LastNameMinLength = 2;
            public const int CountryNameMaxLength = 50;
            public const int CountryNameMinLength = 2;
            public const int MinAge = 16;
            public const int MaxAge = 100;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 50;
            public const int PhoneNumberMaxLength = 20;
            public const int PhoneNumberMinLength = 9;
            public const int ValidMaleGender = 0;
            public const int ValidFemaleGender = 1;
        }

        public class Beacon
        {
            public const int ImeiLength = 15;
            public const int SimCardNumberLength = 13;
            public const int DescriptionMaxLength = 100;
        }
    }
}
