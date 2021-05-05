using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonCRUD.Library;

namespace PersonCRUD
{
    public partial class Dashboard : Form
    {
        // create Person object
        Person person = new Person();

        // create People list
        List<Person> people = new List<Person>();

        PersonDAL personDAL = new PersonDAL();

        public Dashboard()
        {
            InitializeComponent();
        }

        // fills out data grid view
        private void FillDataGridView()
        {
            try
            {
                people = personDAL.ReadAllPeople();
                dataDashboard.DataSource = people;
                dataDashboard.Columns[0].Visible = false;

                ClearTextboxes();
                DisableDeleteButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillDataGridView_Search()
        {
            try
            {
                people = personDAL.SearchPersonFirstName(txtSearchFirstName.Text);
                dataDashboard.DataSource = people;
                dataDashboard.Columns[0].Visible = false;

                ClearTextboxes();
                DisableDeleteButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // clears textboxes in dashboard
        private void ClearTextboxes()
        {
            txtFirstName.Text = ""; 
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtSearchFirstName.Text = "";

            person.PersonID = 0;
        }

        // disables delete button
        private void DisableDeleteButton()
        {
            btnDeletePerson.Enabled = false;
        }

        // enables delete button
        private void EnableDeleteButton()
        {
            btnDeletePerson.Enabled = true;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
                DisableDeleteButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSavePerson_Click(object sender, EventArgs e)
        {
            try
            {
                if (person.PersonID != 0)
                {
                    personDAL.PersonUpdate(person.PersonID, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text);
                }
                else
                {
                    personDAL.PersonCreate(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text);
                }

                ClearTextboxes();
                DisableDeleteButton();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            try
            {
                personDAL.PersonDelete(person.PersonID);

                ClearTextboxes();
                DisableDeleteButton();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTextboxes();
                DisableDeleteButton();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchFirstName_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView_Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataDashboard_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int personID = 0;

                if(dataDashboard.CurrentRow.Index != -1)
                {
                    personID = Convert.ToInt32(dataDashboard.CurrentRow.Cells[0].Value.ToString());
                    txtFirstName.Text = dataDashboard.CurrentRow.Cells[1].Value.ToString();
                    txtLastName.Text = dataDashboard.CurrentRow.Cells[2].Value.ToString();
                    txtEmail.Text = dataDashboard.CurrentRow.Cells[3].Value.ToString();
                    txtPhone.Text = dataDashboard.CurrentRow.Cells[4].Value.ToString();

                    person.PersonID = personID;
                    person.PersonFirstName = txtFirstName.Text;
                    person.PersonLastName = txtLastName.Text;
                    person.PersonEmail = txtEmail.Text;
                    person.PersonPhone = txtPhone.Text;

                    EnableDeleteButton();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
