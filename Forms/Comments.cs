using DesktopMongo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DesktopMongo.Forms
{
    public partial class Comments : Form
    {
        string postId;
        string userIdCurrent;

        public Comments(string postId, string userIdCurrent)
        {
            this.postId = postId;
            this.userIdCurrent = userIdCurrent;
            InitializeComponent();
        }

        private void Comments_Load(object sender, EventArgs e)
        {
            var comments = PostAL.GetPostById(postId).Comments;

            List<CommentForm> listItems = new List<CommentForm>();

            foreach (var item in comments) 
            {
                listItems.Add(new CommentForm(postId, item.Id, userIdCurrent));
                var last = listItems.Last();
                last.TopLevel = false;
                flowLayoutPanelComments.Controls.Add(last);
                last.Show();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddComment_Click(object sender, EventArgs e)
        {
            using (AddComment f = new AddComment())
            {
                var temp = f.ShowDialog();
                if (temp == DialogResult.OK)
                {
                    var newComment = f.newComment;
                    PostAL.AddComment(postId,newComment);
                    CommentForm c = new CommentForm(postId, newComment.Id,userIdCurrent);
                    c.TopLevel = false;
                    flowLayoutPanelComments.Controls.Add(c);
                    c.Show();
                }
            }
        }
    }
}
