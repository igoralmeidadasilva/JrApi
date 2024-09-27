namespace JrApi.Presentation.Routes;

public static class ApiRoutes
{
    public static class Health
    {
        public const string HEALTH = "/health";   
        public const string DASHBOARD = "/dashboard";    
    }
    public static class Users
    {
        public const string GET_BY_ID = "users/{userId:guid}";
        public const string GET_ALL = "users";
        public const string INSERT = "users";
        public const string UPDATE = "users/{userId:guid}";
        public const string DELETE = "users/{userId:guid}";
    }
}
