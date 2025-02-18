namespace JrApi.Presentation.Api.Routes;

public static class ApiRoutes
{
    public static class Health
    {
        public const string HEALTH = "/health";   
        public const string DASHBOARD = "/dashboard";    
    }
    public static class Users
    {
        public const string GET_BY_ID = "user/{userId:guid}";
        public const string GET_PAGED = "user";
        public const string CREATE = "user";
        public const string UPDATE = "user/{userId:guid}";
        public const string DELETE = "user/{userId:guid}";
    }
}