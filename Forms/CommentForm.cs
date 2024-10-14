using DesktopMongo.DAL;
using DesktopMongo.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DesktopMongo.Forms
{
    public partial class CommentForm : Form
    {
        string postId;
        string userIdCurrent;
        string commentId;

        public CommentForm(string postId, string commentId, string userIdCurrent)
        {
            InitializeComponent();
            this.postId = postId;
            this.userIdCurrent = userIdCurrent;
            this.commentId = commentId;
            Comment cm = PostAL.GetCommentById(postId,commentId);
            labelCommentBody.Text = cm.CommentBody;
            labelCommentLikes.Text = "Likes: " + cm.Likes;
        }

        private void buttonLikeComment_Click(object sender, EventArgs e)
        {
            PostAL.LikeComment(postId, commentId, userIdCurrent);

            labelCommentLikes.Text = "Likes: " + PostAL.GetCommentById(postId,commentId).Likes;
        }
    }
}
