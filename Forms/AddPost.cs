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
    public partial class AddPost : Form
    {
        public Post newPost;
        public AddPost()
        {
            InitializeComponent();
        }

        private void buttonAddPost_Click(object sender, EventArgs e)
        {
            newPost = new Post { Title = textBoxTitle.Text, Body = textBoxBody.Text, CreatedTime = DateTime.Now, LikesPost = 0, Comments = new List<Comment>(), LikesUsers = new List<string>()};
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
