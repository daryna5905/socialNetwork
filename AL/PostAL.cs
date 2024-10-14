using DesktopMongo.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopMongo.DAL
{
    public class PostAL
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase database = client.GetDatabase("sn");
        static IMongoCollection<Post> posts = database.GetCollection<Post>("posts");

        public static List<Post> GetSortedPosts()
        {
            return posts.Aggregate().SortByDescending(x => x.CreatedTime).ToList();
        }

        public static Post GetPostById(string postId)
        {
            return posts.Find(y => y.Id == postId).FirstOrDefault();
        }

        public static Comment GetCommentById(string postId, string commentId)
        {
            return GetPostById(postId).Comments.Where(x => x.Id == commentId).FirstOrDefault();
        }

        public static List<Post> GetUserPosts(string userId)
        {
            return posts.Find(y => y.UserIdPost == userId).ToList();
        }

        public static string AddComment(string postId, Comment comment)
        {
            comment.Id = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Post>.Filter.Eq(x => x.Id, postId);
            var update = Builders<Post>.Update.Push(y => y.Comments, comment);
            posts.FindOneAndUpdate(filter,update);
            return comment.Id;
        }

        public static string AddPost(Post post)
        {
            posts.InsertOneAsync(post).Wait();
            return post.Id;
        }

        public static void LikePost(string postId, string userIdCurrent)
        {
            var likesUsersArray = posts.Find(x => x.Id == postId).Project("{likesUsersPost:1}").FirstOrDefault().GetValue("likesUsersPost").AsBsonArray;

            bool likesIncDec;
            if (likesUsersArray.Contains(userIdCurrent))
            {
                likesUsersArray.Remove(userIdCurrent);
                likesIncDec = false;
            }
            else
            {
                likesUsersArray.Add(userIdCurrent);
                likesIncDec = true;
            }

            var filter = Builders<Post>.Filter.Eq(x=>x.Id, postId);
            var update = Builders<Post>.Update.Set("likesUsersPost", likesUsersArray).Inc("likesPost", (likesIncDec) ? 1 : -1);
            posts.UpdateOne(filter, update);
        }

        public static void LikeComment(string postId, string commentId, string userIdCurrent)
        {
            var filter = Builders<Post>.Filter.ElemMatch(x => x.Comments, Builders<Comment>.Filter.Eq(t => t.Id, commentId));
            var likesUsersArray = posts.Find(filter).Project(y => y.Comments.Select(t => t.LikesUsers)).First().First();
            
            bool likesIncDec;
            if (likesUsersArray.Contains(userIdCurrent))
            {
                likesUsersArray.Remove(userIdCurrent); likesIncDec = false;
            }
            else
            {
                likesUsersArray.Add(userIdCurrent); likesIncDec = true;
            }
            
            var update = Builders<Post>.Update.Set("comments.$[].likesUsers", likesUsersArray).Inc("comments.$[].likesComment", (likesIncDec) ? 1 : -1);
            posts.UpdateOne(filter, update);
        }

    }
}
