using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


            private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var item = listView1.SelectedItems[0].Text;
                txtbxselecteditem.Text = item;
 }
        }

        private void btnindir_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count<=0)
            {
                MessageBox.Show("Please select file for download");
            }
            else
            {
                System.IO.Directory.CreateDirectory(txtbxselecteditem.Text + @"\yeni_klasor");

                if (System.IO.File.Exists(textBox1.Text+ "\\"+ txtbxselecteditem.Text) == false)
                {
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {


                        try
                        {

                  
                        using (WebClient wc = new WebClient())
                        {
                            wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                            wc.DownloadFile(
                                // Param1 = Link of file
                                new System.Uri("http://kurulum.fsdyazilim.com/FsdOrderv21/" + item.Text),
                                // Param2 = Path to save
                                textBox1.Text + "\\" + item.Text
                            ); 
                        
                            }

                            listBox1.Items.Add(item.Text);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);

                        }
                        //MessageBox.Show("Dosya başarıyla indirildi ✓ ");
                    }
                }

                else
                {
                    MessageBox.Show("indirme başlatılamadı , dosya zaten mevcut");
                }
            }
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblperc.Text = "%"+ e.ProgressPercentage.ToString();
        }

        private void btnlist_Click(object sender, EventArgs e)
        {


            string uri = "http://kurulum.fsdyazilim.com/FsdOrderv21/OrderFileList.txt";
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            //Regex regex = new Regex("\r\n|\r|\n");

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string[] result = reader.ReadToEnd().Split(new Char[] {'\r','\n' });

                //MatchCollection matches = regex.Matches(result);
                //if (matches.Count == 0)
                //{

                //    return;
                //}

                //foreach (Match match in matches)
                ////{
                //    if (!match.Success) { continue; }
                //    string name = match.Groups["name"].Value;
                
                foreach (string item in result)
                {
                    if (item.Trim().Length > 0     ) 
                    {
                           listView1.Items.Add(item);


                    }
   
                }
         
                //}


            }

            btndownload.Enabled = true;
            lblperc.Enabled = true;
            label1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
    }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

