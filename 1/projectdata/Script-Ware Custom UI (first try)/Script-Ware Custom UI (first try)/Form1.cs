using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using WebSocketSharp;
using WebSocketSharp.Server;

namespace Script_Ware_Custom_UI__first_try_
{
    // some Button Functions are skidded from Grepper...

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        public WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7891");

        private void Form1_Load(object sender, EventArgs e)
        {

            wssv.AddWebSocketService<Echo>("/Executor");
            wssv.Start();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|lua files (*.lua)|*.lua";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            this.fastColoredTextBox1.Text = fileContent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fastColoredTextBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|lua files (*.lua)|*.lua";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, this.fastColoredTextBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            wssv.WebSocketServices["/Executor"].Sessions.Broadcast(fileContent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wssv.WebSocketServices["/Executor"].Sessions.Broadcast(this.fastColoredTextBox1.Text);
        }
    }
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send("rconsoleprint('Script-Ware Connected...')");
        }
    }
}
