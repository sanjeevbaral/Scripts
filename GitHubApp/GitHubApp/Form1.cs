using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHubApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //https://sanjeevbaral:sbaral271685@github.com/sanjeevbaral/Scripts.git
            txtGithub.Text = "https://sanjeevbaral:sbaral271685@github.com/sanjeevbaral/Scripts.git";
            txtLocal.Text = "C:\\GitTest";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          

            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = txtLocal.Text,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                RedirectStandardError=true,
                RedirectStandardOutput=true,
                RedirectStandardInput = true,
                UseShellExecute = false
            };
           
            var process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.WriteLine("cd " + txtLocal.Text);        

            if(Directory.Exists(txtLocal.Text + "\\Clone\\Scripts"))
            {
                process.StandardInput.WriteLine("cd Clone\\Scripts");
                process.StandardInput.WriteLine("git pull --hard " + txtGithub.Text);
            }
            else
            {
                process.StandardInput.WriteLine("md Clone");
                process.StandardInput.WriteLine("cd Clone");

                process.StandardInput.WriteLine("git clone " + txtGithub.Text);
            }
           
            

            process.StandardInput.WriteLine("exit");
            
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();


        }

        private void setAttributesNormal(DirectoryInfo dir)
        {
            foreach (var subDir in dir.GetDirectories())
                setAttributesNormal(subDir);
            foreach (var file in dir.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
            }
        }

        private void EmptyFolder(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
            {
                EmptyFolder(subfolder);
            }
        }
    }
}
