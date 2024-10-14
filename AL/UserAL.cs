using DesktopMongo.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace DesktopMongo.DAL
{
    public class UserAL
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase database = client.GetDatabase("sn");
        static IMongoCollection<User> users = database.GetCollection<User>("users");

        public static string CheckLogin(string email, string password)
        {
            var foundUser = users.Find(x => x.Email == email).FirstOrDefault();

            if (foundUser == null)
            {
                throw new Exception("Not correct email");
            }
            else if (foundUser.Password == password)
            {
                return foundUser.Id;
            }
            else
            {
                throw new Exception("Not correct password");
            }
        }

        public static string FindUserProfile(string name)
        {
            var valueArr = name.Split(' ');
            var foundUser = users.Find(x => (x.FirstName == valueArr[0] && x.LastName == valueArr[1])).FirstOrDefault(); ;

            if (foundUser == null)
            {
                throw new Exception("No user with this name");
            }
            else
            {
                return foundUser.Id;
            }
        }

        public static User GetUserById(string userIdCurrent)
        {
            return users.Find(x => x.Id == userIdCurrent).FirstOrDefault();
        }

        public static bool IfFollower(string userIdCurrent, string userIdProfile)
        {
            return (users.Find(x => x.Id == userIdCurrent).FirstOrDefault().FollowPeople.Contains(userIdProfile)) ? true : false;
        }

        public static void FollowUser(string userIdCurrent, string userIdProfile)
        {
            var followArray = users.Find(x => x.Id == userIdCurrent).Project("{_id:0,followPeople:1}").FirstOrDefault().First().Value.AsBsonArray;
            if (IfFollower(userIdCurrent, userIdCurrent)) { }
            else if (IfFollower(userIdCurrent, userIdProfile))
                followArray.Remove(userIdProfile);
            else
                followArray.Add(userIdProfile);

            var filter = Builders<User>.Filter.Eq(x => x.Id, userIdCurrent);
            var update = Builders<User>.Update.Set("followPeople", followArray);
            users.UpdateOne(filter, update);
        
        }

    }
}
