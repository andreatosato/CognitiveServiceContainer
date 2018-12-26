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
    class Program
    {
        const string CONSUMER_KEY = "4uRwC3v66Mjx9aCpDTto62gwx";
        const string CONSUMER_SECRET = "6EZBg5NRulnC8UozjsPhvN9vkl0BkjvSX422W6twTIZyhBlBVF";
        const string ACCESS_TOKEN = "3092075422-XoTTuFkzdr1DMvKhGUdq0FSFO0UgZZhmZRj68o9";
        const string ACCESS_TOKEN_SECRET = "NQdPjdnfmJjRuAvutrfxIp6CAVf6ivYCWw00alvPeRlbX"; 
        static async Task Main(string[] args)
        {
            string jsonContent = await File.ReadAllTextAsync($@"{Environment.CurrentDirectory}\appsettings.json");
            AppSettings settings = JsonSerializer.ConvertJsonTo<AppSettings>(jsonContent);

            //CristianoRonaldo(settings.Twitter);
            //Ibra(settings.Twitter);
            Insigne(settings.Twitter);


            //Juventus(settings.Twitter);
            RealMadrid(settings.Twitter);
            Milan(settings.Twitter);
            Napoli(settings.Twitter);
        }


        public static void CristianoRonaldo(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@Cristiano OR #CR7", "CristianoRonaldo");            
            db.SaveData(result, CollectionType.Player);
        }

        public static void Insigne(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@Lor_Insigne OR #Insigne", "Insigne");
            db.SaveData(result, CollectionType.Player);
        }

        public static void Ibra(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@Ibra_official OR #Zlatan OR #Ibrahimovic", "Ibra");
            db.SaveData(result, CollectionType.Player);
        }

        public static void Juventus(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@juventusfc OR #Juve OR #Juventus", "Juventus");
            db.SaveData(result, CollectionType.Team);
        }

        public static void RealMadrid(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@realmadrid OR #RealMadrid", "RealMadrid");
            db.SaveData(result, CollectionType.Team);
        }

        public static void Milan(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@acmilan OR #Milan OR #ACMilan", "Milan");
            db.SaveData(result, CollectionType.Team);
        }

        public static void Napoli(TwitterSettings settings)
        {
            TweeterService service = new TweeterService(settings);
            TweeterDatabase db = new TweeterDatabase();
            var result = service.SearchAll("@sscnapoli OR #ForzaNapoliSempre OR @en_sscnapoli", "Napoli");
            db.SaveData(result, CollectionType.Team);
        }
    }
}
