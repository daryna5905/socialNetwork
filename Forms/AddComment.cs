using DesktopMongo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopMongo.Forms
{
    public partial class AddComment : Form
    {
        public Comment newComment;
        public AddComment()
        {
            InitializeComponent();
        }
        private void buttonAddPost_Click(object sender, EventArgs e)
        {
            newComment = new Comment { CommentBody = textBoxBody.Text, Likes = 0, LikesUsers = new List<string>() };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
