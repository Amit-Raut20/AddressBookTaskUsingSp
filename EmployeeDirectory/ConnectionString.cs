namespace EmployeeDirectory
{
    public class ConnectionString
    {

        private static IConfiguration ?Configuration { get; set; }

        public static string GetConnectionString()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            //Configuration = builder.Build();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return Configuration.GetConnectionString("MyDbConnection");
        }
    }
}
