using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Booru_Pareser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Booru booru;

        void fillList(List<string> pic_list)
        {
            foreach (var i in pic_list)
            {
                if (i != "")
                {
                    listView1.Items.Add(new ListViewItem(new string[] { i, (listView1.Items.Count + 1).ToString() }));
                    listView1.Items[listView1.Items.Count-1].Checked = true;
                }

            }
        }
        void switch_class(int i)
        {
            switch(i)
            {
                case 1:
                    booru = new Danbooru(textBox1.Text);
                    break;
                case 2:
                    booru = new Danbooru_API(textBox1.Text);
                    break;
                case 3:
                    booru = new Gelbooru(textBox1.Text);
                    break;
                case 4:
                    booru = new Gelbooru_API(textBox1.Text);
                    break;
                case 5:
                    booru = new Konachan(textBox1.Text);
                    break;
                case 6:
                    booru = new Konachan_API(textBox1.Text);
                    break;
                case 7:
                    booru = new Yande_re(textBox1.Text);
                    break;
                case 8:
                    booru = new Yande_re_API(textBox1.Text);
                    break;
                case 9:
                    booru = new Zerochan(textBox1.Text);
                    break;
                case 10:
                    booru = new Anime_picture(textBox1.Text);
                    break;
                default:
                    booru = new Danbooru(textBox1.Text);
                    break;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            switch_class(comboBox1.SelectedIndex + 1);
            if (booru.error != true)
            {
                listView1.Items.Clear();
                if (checkBox1.Checked == false)
                {
                    fillList(booru.getPics());
                }
                else
                {
                    fillList(booru.getAll(new List<string>()));
                }
                label2.Text = "Total page: " + booru.total_page;
                label3.Text = "Current page: " + booru.current_page;
            }
            else { MessageBox.Show("This tag don't exist!"); }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = listView1.Items[listView1.SelectedItems[0].Index].Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (booru.current_page <= booru.total_page)
            {
                booru.current_page++;
                fillList(booru.getPics());
                label3.Text = "Current page: " + booru.current_page;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                WebClient download = new WebClient();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked == true)
                    {
                        download.DownloadFile(listView1.Items[i].Text, textBox2.Text + listView1.Items[i].Text.Substring(listView1.Items[i].Text.LastIndexOf('/')));
                        listView1.Items[i].BackColor = Color.Green;
                    }
                    else listView1.Items[i].BackColor = Color.Orange;
                }
            }
            else { MessageBox.Show("Enter the path to the directory!"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                new WebClient().DownloadFile(pictureBox1.ImageLocation, saveFileDialog1.FileName);
            }
            saveFileDialog1.FileName = "";
        }
    }
}
