using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace regedit_steam
{
    public partial class SettingsWIN : Form
    {
        public SettingsWIN()
        {
            InitializeComponent();

            this.Location = Cursor.Position;
            textBox1.Text = Settings.Default.path;
            trackBar1.Value = (int)(Settings.Default.Opacity * 100);
        }
        //GUI gui = new GUI();

        int x;
        int y;
        bool track = false;

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.Opacity = (double)trackBar1.Value / 100;
            this.Opacity = (double)trackBar1.Value / 100;
          
            
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void SettingsWIN_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
        }

        private void SettingsWIN_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void SettingsWIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void SettingsWIN_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
        }

        private void SettingsWIN_Activated(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
        }

        private void SettingsWIN_MouseDown(object sender, MouseEventArgs e)
        {
            track = true;
            x = this.Location.X - Cursor.Position.X;
            y = this.Location.Y - Cursor.Position.Y;
        }

        private void SettingsWIN_MouseMove(object sender, MouseEventArgs e)
        {
            Point a = new Point(x, y);
            if (track == true)
            {
                this.Location = new Point(Cursor.Position.X + x, Cursor.Position.Y + y);
            }
        }

        private void SettingsWIN_MouseUp(object sender, MouseEventArgs e)
        {
            track = false;
        }

        private void aX_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Tag = "Выбери путь к библиотеке стим";
            re:
            folderBrowserDialog.ShowDialog();
            if (GetLastFolder(folderBrowserDialog.SelectedPath) == "common")
            {
                Settings.Default.path = folderBrowserDialog.SelectedPath;
                textBox1.Text = Settings.Default.path;
                
            }
            else
            {
                MessageBox.Show("Требеутся выбрать папку common с содержанием ВСЕХ установленых игр из стим!", "Не верный путь!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto re;
            }
        }

        private string GetLastFolder(string path)
        {
            string folder = "";
            for(int i = path.Length-6; i < path.Length; i++)
            {
                folder += path[i];
            }
            return folder;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            this.Hide();
            GUI gui = new GUI();
            gui.update();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Default.path = "null";
            textBox1.Text = "null";
            Settings.Default.Opacity = 1;
            Settings.Default.Save();
        }
    }
}
