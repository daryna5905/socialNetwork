using System;
using System.Windows.Forms;
using DesktopMongo.DAL;
using DesktopMongo.Entities;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using DesktopMongo.Forms;

namespace DesktopMongo
{
    public partial class PostF : Form
    {
        string postId;
        string userIdCurrent;

        public PostF(string postId, string userIdCurrent)
        {
            this.postId = postId;
            this.userIdCurrent = userIdCurrent;
            InitializeComponent();
        }

        private void PostF_Load(object sender, EventArgs e)
        {
            Post p = PostAL.GetPostById(postId);
            labelTitle.Text = p.Title;
            labelBodyPost.Text = p.Body;
            labelLikes.Text = "Likes: " + p.LikesPost.ToString();
        }

        private void buttonLikes_Click(object sender, EventArgs e)
        {
            PostAL.LikePost(postId, userIdCurrent);

            labelLikes.Text = "Likes: " + PostAL.GetPostById(postId).LikesPost.ToString();
        }

        private void buttonComments_Click(object sender, EventArgs e)
        {
            using (Comments cm = new Comments(postId, userIdCurrent))
            {
                cm.ShowDialog();
            }
        }

    }
}
