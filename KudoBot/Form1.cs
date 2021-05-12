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
            textBox2.Text = "200";
            button1_Click(sender, e);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await PutTaskDelay(5);
            //Navigate to strava
            webView.CoreWebView2.Navigate("https://www.strava.com/dashboard");
            await PutTaskDelay(3);
            //Click on login button
            await evaluateScript("document.getElementsByClassName('btn btn-block google-button')[0].click();");
            await PutTaskDelay(5);
            progressBar1.Maximum = (Convert.ToInt32(textBox2.Text)) * 10;
            //Give Kudos up until number that the user specified
            for (int i = 0; i < (Convert.ToInt32(textBox2.Text)); i++)
            {
                //Click Kudo Button
                await evaluateScript($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].click();");
                //Focus on Kudo Button
                await evaluateScript($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].focus();");
                //Delay for one second
                await PutTaskDelay(1);
                //Update Kudo Given textbox
                textBox1.Text = (Convert.ToInt32(textBox1.Text.ToString()) + 1).ToString();
                progressBar1.PerformStep();
            }
            await PutTaskDelay(3);
            Environment.Exit(0);
        }
        public async Task evaluateScript(string functionString)
        {
            await webView.ExecuteScriptAsync(functionString);
            await PutTaskDelay(1);
        }
        public async Task PutTaskDelay(int delay)
        {
            //Multiply int by 1000 to get in miliseconds
            delay = delay * 1000;
            await Task.Delay(delay);
        }
        
    }
}
