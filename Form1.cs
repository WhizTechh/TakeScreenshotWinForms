using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeScreenshotWinForms
{
    public partial class Form1 : Form
    {
        private readonly string directoryPath = @"C:\your\path\here\";
        private readonly string filename = $"Screenshot_{DateTime.Now:yyyyMMddHHmmss}.png";
        private readonly static double scalefactor = 1; //Windows zoom scale factor (1 = default)

        private static Rectangle screenBounds = Screen.AllScreens[0].Bounds; //Entire screen
        //private static Rectangle screenBounds = Screen.AllScreens[0].WorkingArea; //Current window
        private readonly static Bitmap bit = new Bitmap(MakeScaleNumberEven((int)(screenBounds.Width*scalefactor)), MakeScaleNumberEven((int)(screenBounds.Height * scalefactor)));

        readonly Graphics graphic = Graphics.FromImage(bit);

        public Form1()
        {
            InitializeComponent();
        }

        private void captureScreenshotBtn_Click(object sender, EventArgs e)
        {
            TakeScreenshotWinForms();
        }

        static int MakeScaleNumberEven(int value) {
            return (value % 2 == 0) ? value : value + 1;
        }

        async void TakeScreenshotWinForms()
        {
            try
            {
                Visible = false; //Hide form
                await Task.Delay(1500);

                float dpiX = (graphic.DpiX / 96.0f) * (float)scalefactor;
                float dpiY = (graphic.DpiY / 96.0f) * (float)scalefactor;

                Point p1 = new Point(0, 0);
                Point p2 = new Point(0, 0);

                graphic.CopyFromScreen(p1,p2, new Size((int)(screenBounds.Width*dpiX), (int)(screenBounds.Height * dpiY)));
                previewBox.Image = bit;
                Visible = true; //Show form
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void saveScreenshotBtn_Click(object sender, EventArgs e)
        {
            bit.Save(directoryPath+filename); //Save image to the path
        }
    }
}
