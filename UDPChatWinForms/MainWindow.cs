using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UDPChatWinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            StartApp();
        }

        bool done;

        UdpClient udpClnt = new UdpClient();
        Socket soc = new Socket(AddressFamily.InterNetwork,
        SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint endPnt;


        bool chatConnected = false;
        bool receiveData = false; //- Control bool for receive loop
        string chatName = ""; //- Our name
        string chatSendStr = "255.255.255.255"; //- Broadcast or server IP
        int chatPort = 8520;
        bool connecting = false; //- Our we in process of connecting?

        private void StartApp()
        {
            chatField.Enabled = false;
        }

        private void App_Closing(object sender, EventArgs e)
        {
            ChatDisconnect("User Initiated");
        }

        private void ChatDisconnect(string reason)
        {
            if(!chatConnected) { /* Trying to disconnect, when we were not even connected. */ return; }

            chatConnected = false;
            SendMessage($"User disconnected. Reason: {reason}");
            
            //- Maybe should wait here for a second to receive any final messages?

            receiveData = false;
            udpClnt.Close();
        }

        private void ChatConnect()
        { 
            receiveData = true;
            chatConnected = true;
            chatName = nameTBox.Text;
            if (chatName.ToLower().Contains("username1")) { chatName = "User"+DateTime.Now.Millisecond; nameTBox.Text = chatName; }

            AddText($"Connecting...");
            Task t = new Task(() => { ReceiveMsgs(); });
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMsgUI();
        }

        private void SendMsgUI()
        {
            //- UI Method for Send Message Event

            string msg = MsgBox.Text.Trim();
            MsgBox.Text = "";

            //- Check Message to Send First
            if (String.IsNullOrEmpty(msg) || msg.Length < 2) { MessageBox.Show("Cannot send empty messages.","Cannot Send"); return; }

            if (msg.ToLower().StartsWith("/disconnect")) { ChatDisconnect("User Initiated"); }
            else if (msg.ToLower().StartsWith("/connect")) { ChatConnect(); }
            else
            {
                if (!chatConnected)
                {
                    var r = MessageBox.Show("You're not connected. Attempt connection now?", "You're Not Connected!", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        //- User clicked yes, we shall attempt to connect.
                        ChatConnect();


                    }
                    else { /* User chose not to connect. */ return; }

                }

                SendMessage(msg);
                MsgBox.Text = "";
            }
        }

        private void SendMessage(string message)
        {
            try
            {

                byte[] data = Encoding.ASCII.GetBytes(nameTBox.Text + ": " + message);
                AddText($"[SYSTEM] Sending {data.Length} bytes.");
                udpClnt.Send(data, data.Length, chatSendStr, chatPort);
            }
            catch (Exception ex) { AddText($"\nError During Send Function:\n{ex.ToString()}\n"); }

        }

        private void ReceiveMsgs()
        {
            byte[] buffer = new byte[256];

            UdpClient udpClnt = new UdpClient();

            endPnt = new IPEndPoint(IPAddress.Any, chatPort);

            udpClnt.ExclusiveAddressUse = false;
            udpClnt.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClnt.Client.Bind(endPnt);
            AddText($"Binding to {endPnt.ToString()}.");
            connecting = true;
            HashSet<string> names = new HashSet<string>();


            /* Windows Firewall Prompt Here */

            int errors = 0;
            var from = new IPEndPoint(0, 0);
            while (receiveData)
            {
                try
                {
                    int bytesReady = udpClnt.Available;

                    while (bytesReady >0)
                    {
  
                            AddText($"[SYSTEM] Receiving {bytesReady} from buffer.");
                            try
                            {
                                var buff = new byte[bytesReady];
                                buff = udpClnt.Receive(ref from);
                                string strData = Encoding.ASCII.GetString(buff, 0, buff.Length);
                                AddText(strData);
                            }
                            catch (Exception ex) { errors++; AddText($"\nError During Outer Receive Function:\n{ex.ToString()}\n"); }
                        bytesReady = 0;
                    }

                    if (connecting) { if (errors == 0) { AddText("[SYSTEM} Connected!"); connecting = false; } }

                    /* Buffers are clear, can wait here for continue loop perpetually. */
                    Thread.Sleep(1000); //- Sleeps for 1 second, and performs loop check again.
                }
                catch (Exception ex) { errors++; AddText($"\nError During Inner Receive Function:\n{ex.ToString()}\n"); }
            }
        }


        private void AddText (string str)
        {

            chatField.Invoke((MethodInvoker)delegate { chatField.Text += DateTime.Now.ToString() + ": " + str + "\r\n"; });
        }

        /*private void StartListener()
        {

            udpClnt = new UdpClient(8520);

            udpClnt.Client.Bind(new IPEndPoint(IPAddress.Any, 8520));
            soc.Connect(new IPEndPoint(IPAddress.Broadcast, 8520));
            udpClnt.Connect(endPnt);

            SendMessage($"Connected {DateTime.Now.ToString()}");

            try
            {
                while (!done)
                {
                byte[] data = UDPclient.Receive(ref iep);
                richTextBox1.AppendText(Encoding.ASCII.GetString(data, 0, data.Length) + "\r\n");
                ReceiveMsgs();
                }
            }
            catch (Exception ex)
            {
                
                AddText(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            receiver = new Thread(new ThreadStart(StartListener));
            receiver.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            done = true;

            //udpClnt.Close();
            //receiver.Abort();
        }*/
    }
}
