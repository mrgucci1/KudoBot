using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace KudoBot
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "1000";
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            //Navigate to strava
            webView.CoreWebView2.Navigate("https://www.strava.com/dashboard");
            await PutTaskDelay(5);
            progressBar1.Maximum = (Convert.ToInt32(textBox2.Text)) * 10;
            //Give Kudos up until number that the user specified
            for (int i = 0;i<(Convert.ToInt32(textBox2.Text));i++)
            {
                webView.Focus();
                var functionString = string.Format($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].focus();");
                await webView.ExecuteScriptAsync(functionString);
                functionString = string.Format($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].click();");
                await webView.ExecuteScriptAsync(functionString);
                await PutTaskDelay(1);
                textBox1.Text = (Convert.ToInt32(textBox1.Text.ToString()) + 1).ToString();
                //Every 10 Kudos, press space to go to botton of page
                if(i%10==0)
                {
                    //if not first case
                    if(i!=0)
                    {
                        //get webview window in forground to press space
                        Process p = Process.GetProcessesByName("msedgewebview2").FirstOrDefault();
                        if (p != null)
                        {
                            IntPtr h = p.MainWindowHandle;
                            SetForegroundWindow(h);
                            SendKeys.SendWait(" ");
                            await PutTaskDelay(1);
                            SendKeys.SendWait(" ");
                        }
                    }     
                }
                progressBar1.PerformStep();
            }  
        }
        public async Task PutTaskDelay(int delay)
        {
            //Multiply int by 1000 to get in miliseconds
            delay = delay * 1000;
            await Task.Delay(delay);
        }
        
    }
}
