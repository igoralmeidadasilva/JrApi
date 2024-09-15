namespace JrApi.Domain;

public static class Constants
{
    public static class Constraints
    {
        public static class User
        {
            public const int FIRST_NAME_MAX_SIZE = 20;
            public const int LAST_NAME_MAX_SIZE = 40;
            public const int EMAIL_MAX_SIZE = 60;
            public const int PASSWORD_MIN_SIZE = 8;
            public const int PASSWORD_MAX_SIZE = 128;
            public const string PASSWORD_FORMAT= "(?=.*[@#$%^&+=])";
            public const int STREET_MAX_SIZE = 60;
            public const int CITY_MAX_SIZE = 60;
            public const int DISTRICT_MAX_SIZE = 60;
            public const int STATE_MAX_SIZE = 60;
            public const int COUNTRY_MAX_SIZE = 60;
            public const int ZIP_CODE_SIZE = 9;
            public const string ZIP_CODE_FORMAT = "^\\d{2}\\d{3}[-]\\d{3}$";
        }
    }
}
