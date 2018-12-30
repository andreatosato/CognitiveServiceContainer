using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TwitterDatabase.Tweeter;

namespace TwitterDatabase
{
    public class Program
    {       
        public static async Task Main(string[] args)
        {
            string jsonContent = await File.ReadAllTextAsync($@"{Environment.CurrentDirectory}/appsettings.json");
            AppSettings settings = JsonSerializer.ConvertJsonTo<AppSettings>(jsonContent);

            await CristianoRonaldo(settings);
            //Ibra(settings.Twitter);
            //Insigne(settings);


            //Juventus(settings.Twitter);
            //RealMadrid(settings);
            //Milan(settings);
            //Napoli(settings);
        }

        private static TweeterContext GetDb(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TweeterContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new TweeterContext(optionsBuilder.Options);
        }


        public static async Task CristianoRonaldo(AppSettings settings)
        {
            TweeterService service = new TweeterService(settings.Twitter);
            TweeterContext db = GetDb(settings.ConnectionString);
            await service.SearchAll("@Cristiano OR #CR7", "CristianoRonaldo", new TweeterServiceData(db));
            //TweeterServiceData tweeterServiceData = new TweeterServiceData(db);
            //await tweeterServiceData.SaveCollection(result);
        }
        /*
        public static void Insigne(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@Lor_Insigne OR #Insigne", "Insigne");
            db.SaveData(result, CollectionType.Player);
        }

        public static void Ibra(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@Ibra_official OR #Zlatan OR #Ibrahimovic", "Ibra");
            db.SaveData(result, CollectionType.Player);
        }

        public static void Juventus(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@juventusfc OR #Juve OR #Juventus", "Juventus");
            db.SaveData(result, CollectionType.Team);
        }

        public static void RealMadrid(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@realmadrid OR #RealMadrid", "RealMadrid");
            db.SaveData(result, CollectionType.Team);
        }

        public static void Milan(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@acmilan OR #Milan OR #ACMilan", "Milan");
            db.SaveData(result, CollectionType.Team);
        }

        public static void Napoli(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterServiceData db = new TweeterServiceData();
            var result = service.SearchAll("@sscnapoli OR #ForzaNapoliSempre OR @en_sscnapoli", "Napoli");
            db.SaveData(result, CollectionType.Team);
        }
        */
    }
}
