using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsOverlay
{
    public partial class Form1 : Form
    {
        public const string WindowName = "AssaultCube";
        public static IntPtr handle = FindWindow(null, WindowName);
        #region DllImport
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys key);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string IpWindowName);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT IpRect);
        #endregion

        public RECT rect;
        public struct RECT
        {
            public int left, top, right, bottom;
        }
        Graphics g ;
        Pen p = new Pen(Color.Red);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
           // this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            int initStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initStyle | 0x8000 | 0x20);

            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;
            backgroundWorker1.RunWorkerAsync();
            Thread show = new Thread(showWindow) { IsBackground = true };
            show.Start();
        }
        void showWindow()
        {
            while (true)
            {
                
                bool showing = true;
                if (GetAsyncKeyState(Keys.Insert) > 0 && showing == true)
                {
                    this.Hide();
                    showing = false;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawRectangle(p,20,20,200,200);
            //g.DrawString("badr", 20, 20, 200,200);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
               
                GetWindowRect(handle, out rect);
                this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
                this.Top = rect.top;
                this.Left = rect.left;
            }
        }
    }
}
