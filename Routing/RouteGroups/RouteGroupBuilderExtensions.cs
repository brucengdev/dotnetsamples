namespace RouteGroups
{
    public static class RouteGroupBuilderExtensions
    {
        public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder builder)
        {
            builder.Map("/", GetAllTodos);
            return builder;
        }

        public static async Task<List<string>> GetAllTodos()
        {
            return new List<string> { "Public task A", "Public task B", "Public task C"};
        }
    }
}
