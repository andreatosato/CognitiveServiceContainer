
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TwitterDatabase.Tweeter
//{
//    public class TweeterDatabase : IDisposable
//    {
//        private LiteDatabase db;
//        public TweeterDatabase()
//        {
//            db = new LiteDatabase(@"Filename=E:\Progetti\CognitiveServiceContainer\Database\twDatabase.db");
//        }

//        public void SaveData(IEnumerable<TweeterResult> result, CollectionType collectionType)
//        {
//            var dbCollection = db.GetCollection<TweeterResult>(Enum.GetName(typeof(CollectionType), collectionType));
//            foreach (var item in result)
//            {
//                dbCollection.Upsert(item);
//            }
//        }

//        public void Dispose()
//        {
//            db.Dispose();
//        }
//    }

//    public enum CollectionType
//    {
//        Player,
//        Team
//    }
//}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDatabase.Tweeter
{
    public class TweeterServiceData : IDisposable
    {
        private TweeterContext db;
        public TweeterServiceData(TweeterContext database)
        {
            db = database;
        }

        public async Task SaveCollection(List<TweeterResult> results)
        {
            foreach (var item in results)
            {
                try
                {
                    var alreadyExist = await db.Tweets.FindAsync(item.Id);
                    if (alreadyExist != null)
                    {
                        //db.Entry<TweeterResult>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //db.Tweets.Attach(item);
                    }
                    else
                    {
                        db.Tweets.Add(item);
                    }

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}