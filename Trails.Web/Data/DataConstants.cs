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
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const int CountryNameMaxLength = 50;
        }

        public class Beacon
        {
            public const int ImeiLength = 15;
            public const int SimCardNumberLength = 13;
            public const int DescriptionMaxLength = 100;
        }
    }
}
