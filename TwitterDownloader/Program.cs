using FluentScheduler;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TwitterDatabase;
using TwitterDatabase.Tweeter;
using Tweetinvi;

namespace TwitterDownloader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string jsonContent = await File.ReadAllTextAsync($@"{Environment.CurrentDirectory}/appsettings.json");
            AppSettings settings = Tweetinvi.JsonSerializer.ConvertJsonTo<AppSettings>(jsonContent);
            string conn = settings.ConnectionString;
            var registry = new Registry();

            registry.Schedule(async () =>
            {
                try
                {
                    Task t1 = SaveTweets(settings, "@Cristiano OR #CR7", "CristianoRonaldo", await GetServiceData(conn).GetMinId("CristianoRonaldo"));
                    Task t2 = SaveTweets(settings, "@Lor_Insigne OR #Insigne", "Insigne", await GetServiceData(conn).GetMinId("Insigne"));
                    Task t3 = SaveTweets(settings, "@Ibra_official OR #Zlatan OR #Ibrahimovic", "Ibra", await GetServiceData(conn).GetMinId("Ibra"));
                    Task t4 = SaveTweets(settings, "@juventusfc OR #Juve OR #Juventus", "Juventus", await GetServiceData(conn).GetMinId("Juventus"));
                    Task t5 = SaveTweets(settings, "@realmadrid OR #RealMadrid", "RealMadrid", await GetServiceData(conn).GetMinId("RealMadrid"));
                    Task t6 = SaveTweets(settings, "@acmilan OR #Milan OR #ACMilan", "Milan", await GetServiceData(conn).GetMinId("Milan"));
                    Task t7 = SaveTweets(settings, "@sscnapoli OR #ForzaNapoliSempre OR @en_sscnapoli", "Napoli", await GetServiceData(conn).GetMinId("Napoli"));
                    Task t8 = SaveTweets(settings, "@paulpogba OR #Pogba OR #PaulPogba", "Pogba", await GetServiceData(conn).GetMinId("Pogba"));
                    Task t9 = SaveTweets(settings, "@OfficialRadja OR #Nainggolan OR #Radja", "Nainggolan", await GetServiceData(conn).GetMinId("Nainggolan"));
                    Task t10 = SaveTweets(settings, "@G_Higuain OR #Higuain", "Higuain", await GetServiceData(conn).GetMinId("Higuain"));
                    Task t11 = SaveTweets(settings, "@FinallyMario OR #Balotelli", "Higuain", await GetServiceData(conn).GetMinId("Higuain"));

                    await Task.WhenAll(new[] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11 });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }).NonReentrant().ToRunNow().AndEvery(15).Minutes();

            JobManager.Initialize(registry);

            JobManager.JobException += info => Console.WriteLine("An error just happened with a scheduled job: " + info.Exception);
            Console.ReadLine();
        }

        private static TweeterContext GetDb(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TweeterContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new TweeterContext(optionsBuilder.Options);
        }

        private static TweeterServiceData GetServiceData(string connectionString)
        {
            return new TweeterServiceData(GetDb(connectionString));
        }

        public static async Task SaveTweets(AppSettings settings, string searchQuery, string keyword, long? maxIdStored = null)
        {
            TweeterService service = new TweeterService(settings.Twitter);
            await service.SearchAll(searchQuery, keyword, new TweeterServiceData(GetDb(settings.ConnectionString)), maxIdStored);
        }
    }
}
