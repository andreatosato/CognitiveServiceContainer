using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    if (alreadyExist != null) // Nothing to do
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

        public async Task<long?> GetMinId(string searchKey)
        {
            var tweet = await db.Tweets.FirstOrDefaultAsync(x => x.SearchKey == searchKey);
            if (tweet != null)
                return tweet.Id;
            return null;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}