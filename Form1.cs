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

namespace FileManager
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string path_to_copy;
        int papki;
        string now_path;
        bool now_select_disk;
        public Form1()
        {
            InitializeComponent();
        }

        private void go(string where)
        {
            treeView1.Nodes.Clear();
            now_path = where;
            metroTextBox1.Text = now_path;
            string[] folders = Directory.GetDirectories(@where);
            string[] files = Directory.GetFiles(@where);
            papki = 0;
            foreach(string foldername in folders)
            {
                treeView1.Nodes.Add(foldername);
                papki++;
            }
            foreach (string filename in files)
                treeView1.Nodes.Add(filename);
        }

        private void disks()
        {
            now_select_disk = true;
            papki = 0;
            now_path = "";
            metroTextBox1.Text = now_path;
            treeView1.Nodes.Clear();
            DriveInfo[] names = DriveInfo.GetDrives();
            foreach (DriveInfo name in names)
            {
                treeView1.Nodes.Add(name.Name);
                papki++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            now_select_disk = true;
            disks();

        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode.Index < papki && !now_select_disk)
            {
                go(treeView1.SelectedNode.Text.ToString());
            } else if (now_select_disk)
            {
                disks();
                now_select_disk = false;
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (now_path == @"C:\" || now_path=="") disks();
            else if (now_path.LastIndexOf('\\')==2) go(now_path.Substring(0,3));
            else go(now_path.Substring(0, now_path.LastIndexOf('\\')));
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            treeView1.Select();
            if(treeView1.SelectedNode.Index>=papki)
            path_to_copy = treeView1.SelectedNode.Text.ToString();
            metroLabel1.Text = "Копируется файл " + path_to_copy.Substring(path_to_copy.LastIndexOf('\\') + 1);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(now_path +'\\'+ path_to_copy.Substring(path_to_copy.LastIndexOf('\\') + 1)))
            {
                File.Copy(path_to_copy, now_path+'\\'+path_to_copy.Substring(path_to_copy.LastIndexOf('\\') + 1), true);
                metroLabel1.Text = "Файл успешно скопирован!";
                go(now_path);
            }
            else metroLabel1.Text = "Произошла ошибка при копировании файла!";
            
        }
    }
}
