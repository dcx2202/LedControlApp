using System;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace LedControlApp
{
    public partial class Form1 : Form
    {
        private const int cGrip = 16;
        private const int cCaption = 56;
        private Color selectedColor;
        private SerialPort port;
        private Button selectedTab;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            selectedColor = Color.FromArgb(255, 255, 255);
            setSelectedTab(tab1);
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
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("C#" + selectedColor.R + "#" + selectedColor.G + "#" + selectedColor.B + "#!");
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

        private void OnTab1Click(object sender, EventArgs e)
        {
            setSelectedTab(tab1);
        }

        private void OnTab2Click(object sender, EventArgs e)
        {
            setSelectedTab(tab2);
        }

        private void OnTab3Click(object sender, EventArgs e)
        {
            setSelectedTab(tab3);
        }

        private void OnTab4Click(object sender, EventArgs e)
        {
            setSelectedTab(tab4);
        }

        private void OnTab5Click(object sender, EventArgs e)
        {
            setSelectedTab(tab5);
        }

        private void setSelectedTab(Button newtab)
        {
            if (selectedTab == null)
                selectedTab = tab1;

            if (newtab == tab1)
                ledstripPanel.Visible = false;
            else
                ledstripPanel.Visible = true;

            selectedTab.BackColor = Color.FromArgb(40, 43, 43);
            selectedTab = newtab;
            selectedTab.BackColor = Color.FromArgb(46, 49, 49);
        }

        private void OnBreathingButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("B####!");
        }

        private void OnFlashButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("F####!");
        }

        private void OnWipeButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("D####!");
        }

        private void OnLightningButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("G####!");
        }

        private void OnSpeedButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("E####!");
        }

        private void OnClearButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("K####!");
        }

        private void OnRainbowButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("R####!");
        }

        private void OnRandomButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("A####!");
        }

        private void OnReactiveButtonClick(object sender, EventArgs e)
        {
            if (port == null)
            {
                port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }

            port.Write("I####!");
        }

        private void OnGrayScaleClick(object sender, EventArgs e)
        {
            selectedColor = ColorUnderCursor.CursorColor.Get();
        }

        #region ledstrip led buttons
        private void led1Button_Hover(object sender, EventArgs e)
        {
            led1Button.BackColor = selectedColor;
        }

        private void led2Button_Hover(object sender, EventArgs e)
        {
            led2Button.BackColor = selectedColor;
        }

        private void led3Button_Hover(object sender, EventArgs e)
        {
            led3Button.BackColor = selectedColor;
        }

        private void led4Button_Hover(object sender, EventArgs e)
        {
            led4Button.BackColor = selectedColor;
        }

        private void led5Button_Hover(object sender, EventArgs e)
        {
            led5Button.BackColor = selectedColor;
        }

        private void led6Button_Hover(object sender, EventArgs e)
        {
            led6Button.BackColor = selectedColor;
        }

        private void led7Button_Hover(object sender, EventArgs e)
        {
            led7Button.BackColor = selectedColor;
        }

        private void led8Button_Hover(object sender, EventArgs e)
        {
            led8Button.BackColor = selectedColor;
        }

        private void led9Button_Hover(object sender, EventArgs e)
        {
            led9Button.BackColor = selectedColor;
        }

        private void led10Button_Hover(object sender, EventArgs e)
        {
            led10Button.BackColor = selectedColor;
        }

        private void led11Button_Hover(object sender, EventArgs e)
        {
            led11Button.BackColor = selectedColor;
        }

        private void led12Button_Hover(object sender, EventArgs e)
        {
            led12Button.BackColor = selectedColor;
        }

        private void led13Button_Hover(object sender, EventArgs e)
        {
            led13Button.BackColor = selectedColor;
        }

        private void led14Button_Hover(object sender, EventArgs e)
        {
            led14Button.BackColor = selectedColor;
        }

        private void led15Button_Hover(object sender, EventArgs e)
        {
            led15Button.BackColor = selectedColor;
        }

        private void led16Button_Hover(object sender, EventArgs e)
        {
            led16Button.BackColor = selectedColor;
        }

        private void led17Button_Hover(object sender, EventArgs e)
        {
            led17Button.BackColor = selectedColor;
        }

        private void led18Button_Hover(object sender, EventArgs e)
        {
            led18Button.BackColor = selectedColor;
        }

        private void led19Button_Hover(object sender, EventArgs e)
        {
            led19Button.BackColor = selectedColor;
        }

        private void led20Button_Hover(object sender, EventArgs e)
        {
            led20Button.BackColor = selectedColor;
        }

        private void led21Button_Hover(object sender, EventArgs e)
        {
            led21Button.BackColor = selectedColor;
        }

        private void led22Button_Hover(object sender, EventArgs e)
        {
            led22Button.BackColor = selectedColor;
        }

        private void led23Button_Hover(object sender, EventArgs e)
        {
            led23Button.BackColor = selectedColor;
        }
        #endregion
    }
}
