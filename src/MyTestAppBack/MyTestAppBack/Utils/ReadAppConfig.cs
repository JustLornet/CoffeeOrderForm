using MyTestAppBack.DataAccess.Utils;

namespace MyTestAppBack.Utils
{
    internal static class ReadAppConfig
    {
        internal static DbType GetCurrentDb(out string connectionString)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appConfig.json").Build();

                // полчение данных из конфига
                string currentDb = config.GetValue<string>("DbInfo:currentDb");
                connectionString = config.GetValue<string>($"DbInfo:connectionStrings:{currentDb}");
            
                // парсинг типа бд
                var dbType = DbTypeParser.Parse(currentDb);

                return dbType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
