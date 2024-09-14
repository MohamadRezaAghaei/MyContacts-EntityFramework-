using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyContacts
{
    public partial class frmAddorEdit : Form
    {
        Contact_DBEntities db=new Contact_DBEntities();
        public int contactid = 0;
        public frmAddorEdit()
        {
            InitializeComponent();
            
        }

        private void frmAddorEdit_Load(object sender, EventArgs e)
        {
            if (contactid==0)
            {
                this.Text = "افزودن شخص جدید";
            }
            else
            {
                this.Text = "ویرایش شخص";
                MyContact contact= db.MyContacts.Find(contactid);
                txtName.Text = contact.Name;
                txtFamily.Text = contact.Family;
                txtMobile.Text = contact.Mobile;
                txtAge.Text = contact.Age.ToString();
                txtEmail.Text = contact.Name;
                txtAddress.Text =contact.Address;
                btnSubmit.Text = "ویرایش";


            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        bool ValidateInputs()
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("لطفا نام را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFamily.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("لطفا موبایل را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAge.Value ==0)
            {
                MessageBox.Show("لطفا سن را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("لطفا ایمیل را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (contactid == 0)
                {
                    MyContact contact = new MyContact()
                    {
                        Name = txtName.Text,
                        Family = txtFamily.Text,
                        Mobile = txtMobile.Text,
                        Age = (int)txtAge.Value,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text
                        
                    };
                    db.MyContacts.Add(contact);
                }
                else
                {
                    var contact= db.MyContacts.Find(contactid);
                    contact.Name=txtName.Text;
                    contact.Family=txtFamily.Text;
                    contact.Mobile=txtMobile.Text;
                    contact.Age = (int)txtAge.Value;
                    contact.Email=txtEmail.Text;
                    contact.Address=txtAddress.Text;

                }

                db.SaveChanges();
                    MessageBox.Show("عملیات با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    

            }
        }
    }
}
