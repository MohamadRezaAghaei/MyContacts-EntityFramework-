using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyContacts
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
            
        }

        private void BindGrid()
        {
            using (Contact_DBEntities db=new Contact_DBEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.DataSource = db.MyContacts.ToList();
                
            }
            
        }

        private void dgContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddorEdit frm = new frmAddorEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow!=null)
            {
                string Name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string Family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string FullName = Name + " " + Family;
                if (MessageBox.Show($" آیا از حذف {FullName} مطمئن هستید؟","توجه",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int conTactID = Convert.ToInt32(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    using (Contact_DBEntities db= new Contact_DBEntities())
                    {
                        MyContact contact = db.MyContacts.Single(c=>c.ContactID==conTactID);
                        db.MyContacts.Remove(contact);
                        db.SaveChanges();


                    }
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا یک شخص را از لیست انتخاب نمایید");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow!=null)
            {
                int contactiD = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                frmAddorEdit frm = new frmAddorEdit();
                frm.contactid = contactiD;
                if (frm.ShowDialog()==DialogResult.OK)
                {
                    BindGrid();
                }

            }
            else
            {
                MessageBox.Show("لطفا یک شخص را از لیست انتخاب نمایید");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (Contact_DBEntities db =new Contact_DBEntities())
            {
                dgContacts.DataSource = db.MyContacts.Where(f =>
                    f.Name.Contains(txtSearch.Text) || f.Family.Contains(txtSearch.Text)).ToList();
            }

            
            
        }
    }
}
