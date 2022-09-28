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
using Microsoft.Win32;

namespace regedit_steam
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();

            dataGridView1.Controls.Add(pictureBox3);
            //dataGridView2.Controls.Add(pictureBox3);
            pictureBox3.Location = new Point(290, 0);
            pictureBox3.BackColor = Color.Transparent;
            update();
            login.Text = "login: " + Program.login + " name: " + Program.name;
            
            this.Opacity = Settings.Default.Opacity;
            //timer1.Start();

            if (!System.IO.File.Exists(@"C:\ProgramData\SteamControl\config.sc"))
            {

                // var fs = new System.IO.FileStream(@"C:\123.txt", System.IO.FileMode.Open);
                //  var sr = new System.IO.StreamReader(fs, Encoding.Default);
                // string result = sr.ReadLine();
                System.IO.File.Create(@"C:\ProgramData\SteamControl\config.sc");
                
            }
            else
            {
                string[] NewFile = File.ReadAllLines(@"C:\ProgramData\SteamControl\config.sc");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                foreach (string str in NewFile)
                {
                    openFileDialog.FileName = str;
                    Icon ic = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                    dataGridView2.Rows.Add(ic, openFileDialog.SafeFileName, openFileDialog.FileName);
                }
            }


            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Console.WriteLine("Exit");
             

        }
        bool track = false;
        int x;
        int y;

        bool status = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            track = true;
            x = this.Location.X - Cursor.Position.X;
            y = this.Location.Y - Cursor.Position.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            Point a = new Point(x, y);
            if (track == true)
            {
                this.Location = new Point(Cursor.Position.X + x, Cursor.Position.Y + y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            track = false;
        }


        bool[] objs = { false, false, false, false, false, false, false, false, false, false };

        private void GUI_Load(object sender, EventArgs e)
        {
           
    
  
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void закрытьСоединениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Console.WriteLine("Exit");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void toolline_Click(object sender, EventArgs e)
        {

        }
        SettingsWIN settings2 = new SettingsWIN();
        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            if (status == false)
            {
                
                Settings.Default.Opacity = this.Opacity;
                settings2.Show();
                update();
                status = true;

            }
            else
            {
                status = false;
                //settings2.Hide();
            }
            //this.Opacity = Settings.Default.Opacity;
        }

        public void acceptSettings()
        {
            
        }

        private void GUI_Activated(object sender, EventArgs e)
        {
            this.Opacity = Settings.Default.Opacity;
        }

        private void object0_Click(object sender, EventArgs e)
        {

        }



        private void picture_object0_Click(object sender, EventArgs e)
        {
           
            
        }

        private void picture_object0_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        string[] folders = new string[10000];
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        public void update()
        {
            DirectoryInfo dir = new DirectoryInfo(Settings.Default.path);
            int i = 0;
            try
            {
                foreach (var item in dir.GetDirectories())
                {
                    folders[i] = item.Name;
                    i++;
                }
            }
            catch
            { }
            //string ar = GetExeParh(Settings.Default.path + @"\" + folders[1]);
            Console.WriteLine("Count folders: " + i);
            for (int m = 0; m < i; m++)
            {
                try
                {
                    Icon ic = Icon.ExtractAssociatedIcon(GetExeParh(Settings.Default.path + @"\" + folders[m]));
                    dataGridView1.Rows.Add(ic, folders[m], GetExeParh(Settings.Default.path + @"\" + folders[m]));
                }
                catch
                { }
            }
        }

        private string GetExeParh(string path)
        {
            string[] second = Directory.GetFiles(path);
            string exe = "";
            for(int i = 0; i < second.Length; i++)
            {
                string sim = "";
                for(int j = second[i].Length - 3; j < second[i].Length; j++)
                {
                    string arg = second[i];
                    sim += arg[j];
                }
                if(sim == "exe")
                {
                    exe = second[i];
                    break;
                }
            }
            return exe;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string path = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            this.Enabled = false;
            System.Diagnostics.Process.Start(path);
            this.Enabled = true;

        }
         
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите файл запуска игры";
            DialogResult dg = openFileDialog.ShowDialog();
            if(dg == DialogResult.OK)
            {
                try
                {
                    bool accept = true;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if(dataGridView2.Rows[i].Cells[2].Value.ToString() == openFileDialog.FileName)
                        {
                            accept = false;
                        }
                    }
                    if (accept == true)
                    {
                        Icon ic = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                        dataGridView2.Rows.Add(ic, openFileDialog.SafeFileName, openFileDialog.FileName);
                        
                        StreamReader streamReader = new StreamReader(@"C:\ProgramData\SteamControl\config.sc");
                        string str = streamReader.ReadToEnd();
                        streamReader.Close();
                        StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");
                        streamWriter.WriteLine(str + openFileDialog.FileName);
                        streamWriter.Close();
                    }
                    else
                    {
                        MessageBox.Show("Игра " + openFileDialog.SafeFileName + ", уже добавлена в список!", "Повтор игры", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                { }
            }
        }

        private void обновитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update();
        }

        private void dataGridView2_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string[] games = File.ReadAllLines(@"C:\ProgramData\SteamControl\config.sc");    
            try
            {
                string del_game = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                if (MessageBox.Show("Вы точно хотите удалить игру " + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");
                    
                    for (int i = 0; i < games.Length; i++)
                    {
                        if(games[i] != del_game)
                        {
                            streamWriter.WriteLine(games[i]);
                        }

                    }
                    int a = dataGridView2.CurrentRow.Index;
                    dataGridView2.Rows.Remove(dataGridView2.Rows[a]);
                    streamWriter.Close();
                }
            }
            catch
            { MessageBox.Show("Выберите игру из вашей библиотеке!", "Ошибка выбора", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void открытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите файл запуска игры";
            DialogResult dg = openFileDialog.ShowDialog();
            if (dg == DialogResult.OK)
            {
                try
                {
                    bool accept = true;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[2].Value.ToString() == openFileDialog.FileName)
                        {
                            accept = false;
                        }
                    }
                    if (accept == true)
                    {
                        Icon ic = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                        dataGridView2.Rows.Add(ic, openFileDialog.SafeFileName, openFileDialog.FileName);

                        StreamReader streamReader = new StreamReader(@"C:\ProgramData\SteamControl\config.sc");
                        string str = streamReader.ReadToEnd();
                        streamReader.Close();
                        StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");
                        streamWriter.WriteLine(str + openFileDialog.FileName);
                        streamWriter.Close();
                    }
                    else
                    {
                        MessageBox.Show("Игра " + openFileDialog.SafeFileName + ", уже добавлена в список!", "Повтор игры", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                { }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string[] games = File.ReadAllLines(@"C:\ProgramData\SteamControl\config.sc");
            try
            {
                string del_game = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                if (MessageBox.Show("Вы точно хотите удалить игру " + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");

                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i] != del_game)
                        {
                            streamWriter.WriteLine(games[i]);
                        }

                    }
                    int a = dataGridView2.CurrentRow.Index;
                    dataGridView2.Rows.Remove(dataGridView2.Rows[a]);
                    streamWriter.Close();
                }
            }
            catch
            { MessageBox.Show("Выберите игру из вашей библиотеке!", "Ошибка выбора", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //pictureBox3.Visible = true;
            try
            {
                string path = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                this.Enabled = false;
                
                
                System.Diagnostics.Process.Start(path);
                System.Threading.Thread.Sleep(1500);
                pictureBox3.Visible = false;
                this.Enabled = true;
            }
            catch { }
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //pictureBox3.Visible = true;
            try
            {
                string path = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                this.Enabled = false;
                
                
                System.Diagnostics.Process.Start(path);
                System.Threading.Thread.Sleep(1500);
                pictureBox3.Visible = false;
                this.Enabled = true;
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //pictureBox3.Visible = true;
            try
            {
                string path = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                this.Enabled = false;
               
                
                System.Diagnostics.Process.Start(path);
                System.Threading.Thread.Sleep(1500);
                pictureBox3.Visible = false;
                this.Enabled = true;
            }
            catch { }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string[] games = File.ReadAllLines(@"C:\ProgramData\SteamControl\config.sc");
            try
            {
                string del_game = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                if (MessageBox.Show("Вы точно хотите удалить игру " + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "?", "Ошибка выбора", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");

                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i] != del_game)
                        {
                            streamWriter.WriteLine(games[i]);
                        }

                    }
                    int a = dataGridView2.CurrentRow.Index;
                    dataGridView2.Rows.Remove(dataGridView2.Rows[a]);
                    streamWriter.Close();
                }
            }
            catch
            { MessageBox.Show("Выберите игру из вашей библиотеке!", "Ошибка выбора", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите файл запуска игры";
            DialogResult dg = openFileDialog.ShowDialog();
            if (dg == DialogResult.OK)
            {
                try
                {
                    bool accept = true;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[2].Value.ToString() == openFileDialog.FileName)
                        {
                            accept = false;
                        }
                    }
                    if (accept == true)
                    {
                        Icon ic = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                        dataGridView2.Rows.Add(ic, openFileDialog.SafeFileName, openFileDialog.FileName);

                        StreamReader streamReader = new StreamReader(@"C:\ProgramData\SteamControl\config.sc");
                        string str = streamReader.ReadToEnd();
                        streamReader.Close();
                        StreamWriter streamWriter = new StreamWriter(@"C:\ProgramData\SteamControl\config.sc");
                        streamWriter.WriteLine(str + openFileDialog.FileName);
                        streamWriter.Close();
                    }
                    else
                    {
                        MessageBox.Show("Игра " + openFileDialog.SafeFileName + ", уже добавлена в список!", "Повтор игры", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                { }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
