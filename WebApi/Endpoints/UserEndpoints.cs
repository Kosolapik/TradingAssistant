namespace TradingAssistant.WebApi.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/user").WithTags("User endpoints");
        }
    }
}
