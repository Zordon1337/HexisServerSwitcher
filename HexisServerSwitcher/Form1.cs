using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexisServerSwitcher
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IRC_SERVER.Text = HostsUtils.GetCurrentValue("irc.dienay.net");
            WEB_SITE.Text = HostsUtils.GetCurrentValue("hexis.dienay.net");
            SCORE_SERVER.Text = HostsUtils.GetCurrentValue("hscore.dienay.net");
            GAME_SERVER.Text = HostsUtils.GetCurrentValue("hnet.dienay.net");
            CRASH_REPORT.Text = HostsUtils.GetCurrentValue("hcrash.dienay.net");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://zrd.ovh");
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            HostsUtils.SetValue("irc.dienay.net", IRC_SERVER.Text);
            HostsUtils.SetValue("hexis.dienay.net", WEB_SITE.Text);
            HostsUtils.SetValue("hscore.dienay.net", SCORE_SERVER.Text);
            HostsUtils.SetValue("hnet.dienay.net", GAME_SERVER.Text);
            HostsUtils.SetValue("hcrash.dienay.net", CRASH_REPORT.Text);
        }
    }
    public static class HostsUtils
    {
        private static string _hosts = "C:\\Windows\\System32\\drivers\\etc\\hosts";
        public static string GetCurrentValue(string key)
        {
            var lines = System.IO.File.ReadAllLines(_hosts);
            foreach (var line in lines)
            {
                if (line.Contains(" "))
                {
                    var key1 = line.Split(' ')[0];
                    var value1 = line.Split(' ')[1];
                    if(key1 == value1)
                    {
                        if (key1.Equals(key))
                            return "default";
                    } else
                    {
                        if (key1.Equals(key))
                            return value1;
                    }
                }
            }
            return key;
        }
        public static void SetValue(string key, string value)
        {

            if (GetCurrentValue(key) == key) // aka not found
            {

                var file = System.IO.File.ReadAllText(_hosts);
                file += $"\n{key} {value}";
                System.IO.File.WriteAllText(_hosts, file);
            }
            else
            {
                var lines = System.IO.File.ReadAllLines(_hosts);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(" "))
                    {
                        var key1 = lines[i].Split(' ')[0];
                        var value1 = lines[i].Split(' ')[1];
                        if (key1.Equals(key))
                        {
                            lines[i] = $"{key} {value}";
                        }
                    }
                }
                System.IO.File.WriteAllLines(_hosts, lines);
            }
        }
    }
}
