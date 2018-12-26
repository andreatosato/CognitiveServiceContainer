using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterDatabase.Tweeter
{
    public class TweeterDatabase : IDisposable
    {
        private LiteDatabase db;
        public TweeterDatabase()
        {
            db = new LiteDatabase(@"Filename=E:\Progetti\CognitiveServiceContainer\Database\twDatabase.db");
            BsonMapper.Global.Entity<TweeterResult>().Id(x => x.Id);
        }

        public void SaveData(IEnumerable<TweeterResult> result, CollectionType collectionType)
        {
            var dbCollection = db.GetCollection<TweeterResult>(Enum.GetName(typeof(CollectionType), collectionType));
            foreach (var item in result)
            {
                dbCollection.Upsert(item);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }

    public enum CollectionType
    {
        Player,
        Team
    }
}
