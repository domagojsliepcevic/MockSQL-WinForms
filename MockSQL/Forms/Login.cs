using MockSQL.Data;
using MockSQL.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockSQL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                RepositoryFactory.GetRepository().LogIn(
                        tbServer.Text.Trim(),
                        tbUsername.Text.Trim(),
                        tbPassword.Text.Trim()
                        );

                new UserInterface().Show();

                Hide();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }
    }
    
}
