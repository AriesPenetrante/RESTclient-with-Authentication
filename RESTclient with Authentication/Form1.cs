using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RestClientName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private void Output(string strOutputText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strOutputText + Environment.NewLine);
                outputTextBox.Text = outputTextBox.Text + strOutputText + Environment.NewLine;
                outputTextBox.SelectionStart = outputTextBox.TextLength;
                outputTextBox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {

            RESTClient rClient = new RESTClient();

            rClient.endPoint = urlTextBox.Text;
            ///start autentication. Input the credentials.
			rClient.authTech = autheticationTechnique.RollYourOwn;
            rClient.authType = authenticationType.Basic;
            rClient.userName = usernameTextBox.Text;
            rClient.userPassword = passwordTextBox.Text;

            ///end authentication




            Output("RESTClient Object created.");

            string strJSON = string.Empty;

            strJSON = rClient.makeRequest();

            Output(strJSON);




        }






        private void writetofile()


        {

            ///Start of Writing 
            ///make output file at C:\Output

            string filePath = @"C:\Output\Output.txt";
            string content = outputTextBox.Text;

            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(content);
            }




            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            string readfile = File.ReadAllText(dialog.FileName);

            outputTextBox.Text = readfile;




            ///end of writing




        }

        private void copytofileBtn_Click(object sender, EventArgs e)
        {
            writetofile();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            outputTextBox.Text = string.Empty;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
