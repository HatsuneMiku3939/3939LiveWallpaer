using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace _3939LiveWallpaper
{
    public partial class LiveWallpaper : Form
    {
        public ChromiumWebBrowser browser;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public void InitBrowser()
        {
            // create CEF control and set initial URL
            Cef.Initialize(new CefSettings());

            string current_path = AppDomain.CurrentDomain.BaseDirectory;
            string start_url = "file:///" + current_path + "\\webUI\\index.html";
            browser = new ChromiumWebBrowser(start_url);

            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        public void InitTrayicon()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("ToggleSound", LiveWallpaper_OnToggleSound);
            trayMenu.MenuItems.Add("Reload", LiveWallpaper_OnReload);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", LiveWallpaper_OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "3939LiveWallpaper";
            trayIcon.Icon = _3939LiveWallpaper.Properties.Resources.AppIcon;
            // trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }

        public LiveWallpaper()
        {
            InitializeComponent();
            InitBrowser();
            InitTrayicon();
        }

        private void LiveWallpaper_Load(object sender, EventArgs e)
        {
            // Those two lines make the form a child of the WorkerW,
            // thus putting it behind the desktop icons and out of reach
            // for any user intput. The form will just be rendered, no
            // keyboard or mouse input will reach it. You would have to use
            // WH_KEYBOARD_LL and WH_MOUSE_LL hooks to capture mouse and
            // keyboard input and redirect it to the windows form manually,
            // but thats another story, to be told at a later time.
            W32.SetParent(this.Handle, _3939LiveWallpaper.Program.workerw);

            // maximize
            this.WindowState = FormWindowState.Maximized;
        }

        private void LiveWallpaper_OnReload(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void LiveWallpaper_OnToggleSound(object sender, EventArgs e)
        {
            // TODO, Implementation
        }

        private void LiveWallpaper_OnExit(object sender, EventArgs e)
        {
            // remove trayIcon
            trayIcon.Visible = false;

            // exit application
            Application.Exit();
        }
    }
}
