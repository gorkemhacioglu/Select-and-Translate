using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Keys = System.Windows.Forms.Keys;
using Microsoft.Playwright;
using System.Runtime.InteropServices;
using Microsoft.Toolkit.Uwp.Notifications;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using System.Collections.Generic;

namespace Select_And_Translate
{
    public partial class MainForm : Form
    {
        private IKeyboardMouseEvents _mGlobalHook;

        private Point _fromPoint;
        private Point _toPoint;

        private bool _isEnabled;
        
        private Graphics _graphic;

        private readonly Dictionary<string, string> _fromLanguageDictionary = new Dictionary<string, string>();

        private readonly Dictionary<string, string> _toLanguageDictionary = new Dictionary<string, string>();

        public MainForm()
        {
            new ToastContentBuilder()
                .AddText("Initializing the translator...")
                .Show();

            Microsoft.Playwright.Program.Main(new[] { "install" });

            InitializeComponent();

            PrepareUi();

            new ToastContentBuilder()
                .AddText("Translator is running in background...")
                .Show();
        }

        private void PrepareUi()
        {
            _fromLanguageDictionary.Add("English", "en");
            _fromLanguageDictionary.Add("Turkish", "tr");
            _fromLanguageDictionary.Add("Spanish", "es");
            _fromLanguageDictionary.Add("German", "de");
            _fromLanguageDictionary.Add("Italian", "it");
            _fromLanguageDictionary.Add("Russian", "ru");
            _fromLanguageDictionary.Add("French", "fr");

            foreach (var item in _fromLanguageDictionary)
            {
                _toLanguageDictionary.Add(item.Key, item.Value);
            }

            _fromLanguageDictionary.Add("Detect Language", "auto");

            fromLanguage.ValueMember = "Value";
            fromLanguage.DisplayMember = "Key";
            fromLanguage.DataSource = new System.Windows.Forms.BindingSource(_fromLanguageDictionary, null);
            fromLanguage.SelectedIndex = 0;

            toLanguage.ValueMember = "Value";
            toLanguage.DisplayMember = "Key";
            toLanguage.DataSource = new System.Windows.Forms.BindingSource(_toLanguageDictionary, null);
            toLanguage.SelectedIndex = 1;

            pictureBox1_Click(null, null);
        }

        private void Subscribe()
        {
            _mGlobalHook = Hook.GlobalEvents();

            _mGlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            _mGlobalHook.MouseUpExt += GlobalHookMouseUpExt;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            _fromPoint = Cursor.Position;
        }

        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {
            _toPoint = Cursor.Position;

            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            _graphic = Graphics.FromHdc(desktopPtr);

            var brush = Brushes.Yellow;

            var pen = new Pen(brush);

            _graphic.DrawRectangle(pen,
                new Rectangle(_fromPoint.X, _fromPoint.Y, _toPoint.X - _fromPoint.X, _toPoint.Y - _fromPoint.Y));

            ReleaseDC(IntPtr.Zero, desktopPtr);

            try
            {
                if (_toPoint.X - _fromPoint.X > 10 || _toPoint.Y - _fromPoint.Y > 10)
                {
                    Bitmap bitmap = new Bitmap(_toPoint.X - _fromPoint.X, _toPoint.Y - _fromPoint.Y);

                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(_fromPoint.X, _fromPoint.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);

                        bitmap = ProcessImage(bitmap);

                        bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\subtitle.jpeg",
                            ImageFormat.Jpeg);

                        DoMouseClick();

                        using (var engine = new TesseractEngine(@"./3rdPartyFiles/tessdata", "eng", EngineMode.Default))
                        {
                            using (var img = Pix.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory +
                                                              @"\Resources\subtitle.jpeg"))
                            {
                                using (var page = engine.Process(img))
                                {
                                    var result = page.GetText();

                                    if (!String.IsNullOrEmpty(result))
                                        Translate(result);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                GlobalHookMouseUpExt(null, null);
            }
        }

        private Bitmap ProcessImage(Bitmap image)
        {
            Bitmap returnMap = new Bitmap(image.Width, image.Height,
                PixelFormat.Format32bppArgb);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0,
                    image.Width, image.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                    returnMap.Width, returnMap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            int a = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        a = (imagePointer1[0] + imagePointer1[1] +
                             imagePointer1[2]) / 3;
                        imagePointer2[0] = (byte)a;
                        imagePointer2[1] = (byte)a;
                        imagePointer2[2] = (byte)a;
                        imagePointer2[3] = imagePointer1[3];

                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }

                    imagePointer1 += bitmapData1.Stride -
                                     (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride -
                                     (bitmapData1.Width * 4);
                }
            }

            returnMap.UnlockBits(bitmapData2);
            image.UnlockBits(bitmapData1);
            return returnMap;
        }

        private void Translate(string selected)
        {
            try
            {
                var url = $"https://translate.google.com/?sl=" + fromLanguage.SelectedValue + "&tl=" + toLanguage.SelectedValue + "&text=" + HttpUtility.UrlEncode(selected) + "&op=translate";

                var pw = Playwright.CreateAsync().GetAwaiter().GetResult();

                var browser = pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = true })
                    .GetAwaiter().GetResult();

                var page = browser.NewPageAsync().GetAwaiter().GetResult();
                page.GotoAsync(url).GetAwaiter().GetResult();

                var translate =
                    page.Locator(
                            "xpath=/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[2]/div[8]/div/div[1]")
                        .InnerTextAsync().GetAwaiter().GetResult();
                browser.DisposeAsync().GetAwaiter().GetResult();

                MessageBox.Show(translate, "Translation is:");
                
                    /*new ToastContentBuilder()
                    .AddText(translate)
                    .Show();*/

                _graphic.Clear(Color.Transparent);
                _graphic.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Unsubscribe()
        {
            _mGlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            _mGlobalHook.KeyPress -= GlobalHookKeyPress;

            _mGlobalHook.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _isEnabled = !_isEnabled;

            if (_isEnabled)
            {
                pictureBoxOnOff.BackgroundImage =
                    Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Icons\\on.png");

                Subscribe();
            }
            else
            {
                pictureBoxOnOff.BackgroundImage =
                    Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Icons\\off.png");

                Unsubscribe();
            }
        }

        private void DoMouseClick()
        {
            var x = _toPoint.X;
            var y = _toPoint.Y;
            mouse_event(MouseEventfLeftdown | MouseEventfLeftup, x, y, 0, 0);
        }

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        private static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        private const int MouseEventfLeftdown = 0x02;
        private const int MouseEventfLeftup = 0x04;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private void radioManuelMode_CheckedChanged(object sender, EventArgs e)
        {
            radioPreDefinedSelection.Checked = !radioManuelMode.Checked;
        }

        private void radioPreDefinedSelection_CheckedChanged(object sender, EventArgs e)
        {
            radioManuelMode.Checked = !radioPreDefinedSelection.Checked;

            MessageBox.Show("Define the subtitle area");
        }
    }
}