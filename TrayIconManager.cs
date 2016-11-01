using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EDEngineer.Utils;

namespace EDEngineer
{
    public static class TrayIconManager
    {
        public static IDisposable Init(EventHandler showHandler,
            EventHandler quitHandler,
            EventHandler configureShortcutHandler)
        {
            var menu = BuildContextMenu(showHandler, quitHandler, configureShortcutHandler);

            var icon = new NotifyIcon
            {
                Icon = new Icon(Path.GetFullPath("Resources/Images/elite-dangerous-clean.ico")),
                Visible = true,
                Text = "ED - Engineer",
                ContextMenu = menu
            };

            return Disposable.Create(() =>
            {
                icon.Visible = false;
                icon.Icon = null;
                icon.Dispose();
            });
        }

        private static ContextMenu BuildContextMenu(EventHandler showHandler, EventHandler quitHandler, EventHandler configureShortcutHandler)
        {
            var showItem = new MenuItem()
            {
                Text = "Show"
            };
            showItem.Click += showHandler;

            var setShortCutItem = new MenuItem()
            {
                Text = "Set Shortcut"
            };
            setShortCutItem.Click += configureShortcutHandler;
            var quitItem = new MenuItem()
            {
                Text = "Quit",
            };
            quitItem.Click += quitHandler;

            var menu = new ContextMenu();
            menu.MenuItems.Add(showItem);
            menu.MenuItems.Add(setShortCutItem);
            menu.MenuItems.Add("-");
            menu.MenuItems.Add(quitItem);
            return menu;
        }
    }
}