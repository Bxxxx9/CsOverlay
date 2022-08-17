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
        #region DllImport
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys keys);
        public const string WINDOW_NAME = "AssaultCube";
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        #endregion

        public static RECT rect;
        public struct RECT
        {
            public int left, top, right, bottom;
        }
        IntPtr handle = FindWindow(null,WINDOW_NAME);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CheckForIllegalCrossThreadCalls = false;
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            int intstyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.handle, -20, intstyle | 0x8000 | 0x20);
            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;
            backgroundWorker1.RunWorkerAsync();
            
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                GetWindowRect(handle, out rect);
                this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
                this.Left = rect.left;
                this.Top = rect.top;
                Thread.Sleep(10);
                
            }
        }
    }
}
