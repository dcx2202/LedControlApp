using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedControlApp
{
    public partial class Form1 : Form
    {
        private const int cGrip = 16;
        private const int cCaption = 56;
        private Color selectedColor;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            selectedColor = Color.FromArgb(255, 255, 255);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(26, 24, 24)), rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void OnCloseButton(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnMinimizeButton(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void OnOkButton(object sender, EventArgs e)
        {

        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //selectedColor = ColorUnderCursor.CursorColor.Get();
            }
        }

        private void OnColorWheel(object sender, EventArgs e)
        {
            selectedColor = ColorUnderCursor.CursorColor.Get();
        }
    }
}
