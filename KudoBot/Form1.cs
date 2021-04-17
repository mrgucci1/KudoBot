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
            string[] comments = { "just chilly", "mega chilly", "DALQ STRENGTH", "COOOOOOOOOOOKIE", "good job :D" };
            var rand = new Random();
            //Give Kudos up until number that the user specified
            for (int i = 0; i < (Convert.ToInt32(textBox2.Text)); i++)
            {
                //Focus on Kudo Button
                await evaluateScript($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].focus();");
                //Click Kudo Button
                await evaluateScript($@"document.getElementsByClassName('btn btn-icon btn-icon-only btn-kudo btn-xs js-add-kudo')[0].click();");
                //If a kudo dialog shows up, click the x
                await evaluateScript("document.getElementsByClassName('btn btn-unstyled btn-close')[0].click();");
                //Loop Through activities and find ones with names we want to comment on
                /*for (int j = 0; j < Convert.ToInt32(textBox2.Text); j++)
                {
                    var returnVar = await webView.ExecuteScriptAsync($"document.getElementsByClassName('entry-owner')[{j}].innerText.toString()");
                    if(returnVar.ToString().Contains("Austen Dalquist") || returnVar.ToString().Contains("Kirsten Dalquist"))
                    {
                        //Click on comment button
                        await evaluateScript($"document.getElementsByClassName('btn btn-comment btn-icon btn-icon-only btn-xs empty')[{j}].click();");
                        await evaluateScript($"document.getElementsByClassName('form-control')[{7+j}].innerText = '{comments[rand.Next(0, 5)]}'");
                        //await evaluateScript($"document.getElementsByClassName('text-footnote btn btn-default btn-sm')[{j}].click();");
                    }
                }*/
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
        }
        public async Task PutTaskDelay(int delay)
        {
            //Multiply int by 1000 to get in miliseconds
            delay = delay * 1000;
            await Task.Delay(delay);
        }
        
    }
}
