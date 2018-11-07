using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Hotkeys;
namespace MontaroKBrouter2
{
    public partial class Form1 : Form
    {

        private Hotkeys.GlobalHotkey ghk;


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;



        public Form1()
        {
            InitializeComponent();
            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Keys key = Keys.A;
            if ((Keys)System.Enum.Parse(typeof(Keys), textBox1.Text) != key)
            {
                try{ ghk.Unregister(); }
                catch { }

            }
            key = (Keys)System.Enum.Parse(typeof(Keys), textBox1.Text);
            
            ghk = new Hotkeys.GlobalHotkey(Constants.NOMOD, key , this);
            if (ghk.Register())
            {
                MessageBox.Show("Registered hotkey to: " + key.ToString());
                label2.Text = key.ToString();
            }
        }

        private void HandleHotkey()
        {
            //When Hotkey is clicked
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            

        }

        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(ghk.Unregister())
            {
                MessageBox.Show("Complete: unregister");
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

        }

       
    }
}
