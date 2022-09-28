using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;

namespace regedit_steam
{
    class Program
    {
        public static string login;
        public static string name;


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_Min = 2;
        const int SW_Max = 3;
        const int SW_Norm = 4;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        static void Main(string[] args)
        {
            RegistryKey UserKey = Registry.Users;
            int a =  0;
            string[] mas = UserKey.GetSubKeyNames();
            string[] atmas = new string[mas.Length];
            string line = ""; int j = 0; 
            while (a < mas.Length)
            {
                line = mas[a];
                j = line.Length - 1;
                for(; j > 0; j--)
                {
                    if(line[j] != '-')
                    {
                        atmas[a] += line[j];
                    }
                    else
                    {
                        j = 0;
                    }
                }
                a++;
            }
            a = 0;
            while (a < mas.Length)
            {
                Console.Write(mas[a] + " - ");
                Console.WriteLine(atmas[a]);
                a++;
            }
            int id = 0;
            a = 0;
            while (a < atmas.Length)
            {
                if(atmas[a] == "1001")
                {
                    id = a;
                }
                a++;
            }
            Console.WriteLine("ID line: " + id);

            RegistryKey User = UserKey.OpenSubKey(mas[id], true);
            RegistryKey Software = User.OpenSubKey("SOFTWARE", true);
            RegistryKey Valve = Software.OpenSubKey("Valve", true);
            RegistryKey Steam = Valve.OpenSubKey("Steam", true);

            login = Steam.GetValue("AutoLoginUser").ToString();
            name = Steam.GetValue("LastGameNameUsed").ToString();

            Console.WriteLine("");
            Console.WriteLine("Steam Login: " + login);
            Console.WriteLine("Steam Name: " + name);

            RegistryKey CurUser = Registry.CurrentUser;
            RegistryKey SoftWare = CurUser.OpenSubKey("SOFTWARE", true);
            RegistryKey SC = SoftWare.OpenSubKey("SteamControl", true);


            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);


            GUI gui = new GUI();
            gui.ShowDialog();
            // SettingsWIN settings = new SettingsWIN();
            // settings.ShowDialog();
            ShowWindow(handle, SW_SHOW);
            System.Threading.Thread.Sleep(500);
            Environment.Exit(0);
            Console.ReadKey();
        }
    }
}
