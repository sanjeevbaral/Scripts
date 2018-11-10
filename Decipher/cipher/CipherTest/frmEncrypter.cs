using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace CipherTest
{
    public partial class frmEncrypter : Form
    {
        bool Encrypted = false;
        SqlConnection con;
        public frmEncrypter()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = txtConnectionString.Text;
                con.Open();
                con.Close();
                MessageBox.Show("Connection successful");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEncryptDecrypt_Click(object sender, EventArgs e)
        {
            if(!Encrypted)
            {
                string cipherText = CipherSuite.Cipher.Encrypt(txtConnectionString.Text, txtCipherPhrase.Text);
                txtConnectionString.Text = cipherText;
                btnEncryptDecrypt.Text = "Decrypt";
                Encrypted = true;
            }
            else
            {
                string clearText = CipherSuite.Cipher.Decrypt(txtConnectionString.Text, txtCipherPhrase.Text);
                txtConnectionString.Text = clearText;
                btnEncryptDecrypt.Text = "Encrypt";
                Encrypted = false;

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "connection.inf";
            dlg.ShowDialog();
            if(dlg.FileName!="")
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);
                writer.WriteLine(txtConnectionString.Text);
                writer.Flush();
                writer.Close();
            }
           


        }
    }
}
