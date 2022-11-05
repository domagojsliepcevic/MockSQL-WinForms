using MockSQL.Data;
using MockSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockSQL.Forms
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
            cbDatabases.DataSource = new List<Database>(RepositoryFactory.GetRepository().GetDatabases());
        }

        private void UserInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
      
            string query = tbQuery.SelectedText.Trim();
            if(string.IsNullOrWhiteSpace(query)) query = tbQuery.Text;
            if(string.IsNullOrWhiteSpace(query)) return;

            try
            {
                var dataset = RepositoryFactory.GetRepository().CreateDataSet((Database)cbDatabases.SelectedItem, query);
                if (dataset.Tables.Count > 0)
                {
                    
                        dgResults.DataSource = dataset.Tables[0];
                        tabRMWindow.SelectedTab = tabRMWindow.TabPages[nameof(tabResults)];
                 

                }
                else
                {
                    tbMessages.Text = "Query executed successfully!";
                    tabRMWindow.SelectedTab = tabRMWindow.TabPages[nameof(tabMessages)];

                }
            }
            catch (Exception ex)
            {

                tbMessages.Text = ex.Message;
                tabRMWindow.SelectedTab = tabRMWindow.TabPages[nameof(tabMessages)];
            }
        }
    }
}
