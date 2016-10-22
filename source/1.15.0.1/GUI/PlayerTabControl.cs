﻿using System.Drawing;
using System.Windows.Forms;

namespace SevenDaysProfileEditor.GUI {

    /// <summary>
    /// Holds all the tabs that represent a ttp each.
    /// </summary>
    internal class PlayerTabControl : TabControl {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="mainWindow">Main window of the program</param>
        public PlayerTabControl(MainWindow mainWindow) {
            Dock = DockStyle.Fill;
            DrawMode = TabDrawMode.OwnerDrawFixed;

            DrawItem += (sender, e) => {
                e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 13, e.Bounds.Top + 1);
                e.Graphics.DrawString(TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
                e.DrawFocusRectangle();
            };

            MouseDown += (sender, e) => {
                for (int i = 0; i < TabPages.Count; i++) {
                    Rectangle r = GetTabRect(i);
                    Rectangle closeButton = new Rectangle(r.Right - 13, r.Top + 1, 11, 10);
                    if (closeButton.Contains(e.Location)) {
                        foreach (PlayerTab tab in TabPages) {
                            if (TabPages.IndexOf(tab) == i) {
                                mainWindow.mainMenu.mainMenuActions.Close(tab);
                            }
                        }
                    }
                }
            };

            ControlAdded += (sender, e) => {
                mainWindow.mainMenu.UpdateMenus(GetTabCount());
            };

            ControlRemoved += (sender, e) => {
                // GetTabCount() - 1 since tab count isn't updated right away for some reason.
                mainWindow.mainMenu.UpdateMenus(GetTabCount() - 1);
            };
        }

        /// <summary>
        /// Adds a tab.
        /// </summary>
        /// <param name="tab">Tab to add</param>
        public void AddTab(PlayerTab tab) {
            Controls.Add(tab);
        }

        /// <summary>
        /// Gets the currently selected tab
        /// </summary>
        /// <returns>Currently selected tab</returns>
        public PlayerTab GetSelectedTab() {
            return (PlayerTab)SelectedTab;
        }

        /// <summary>
        /// Gets the tab count
        /// </summary>
        /// <returns>Tab count</returns>
        public int GetTabCount() {
            return TabPages.Count;
        }
    }
}