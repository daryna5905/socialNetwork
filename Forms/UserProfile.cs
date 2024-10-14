using DesktopMongo.DAL;
using DesktopMongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DesktopMongo.Forms
{
    public partial class UserProfile : Form
    {
        string userIdProfile;
        string userIdCurrent;

        public UserProfile(string userIdProfile, string userIdCurrent)
        {
            InitializeComponent();
            this.userIdProfile = userIdProfile;
            this.userIdCurrent = userIdCurrent;
            buttonFriends.Text = (UserAL.IfFollower(userIdCurrent, userIdProfile)) ? "You are following" : "Follow";
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            List<Post> postsList = PostAL.GetUserPosts(userIdProfile);

            List<PostF> listItems = new List<PostF>();

            foreach (var item in postsList)
            {
                listItems.Add(new PostF(item.Id, userIdCurrent));
                var last = listItems.Last();
                last.TopLevel = false;
                flowLayoutPanel1.Controls.Add(last);
                last.Show();
            }

            User u = UserAL.GetUserById(userIdProfile);
            labelAllName.Text = u.FirstName + u.LastName;
            labelEmail.Text = u.Email;
            foreach (var item in u.Hobbies)
            {
                labelHobbies.Text += item +", ";
            }
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            StartPage st = new StartPage(userIdCurrent);
            st.Show();
            this.Hide();
        }

        private void buttonFriends_Click(object sender, EventArgs e)
        {
            UserAL.FollowUser(userIdCurrent, userIdProfile);

            buttonFriends.Text = (UserAL.IfFollower(userIdCurrent, userIdProfile)) ? "You are following" : "Follow";
        }
    }
}
