using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public static int score = 0;

        public Form1()
        {
            InitializeComponent();

            StartMenu sm = new StartMenu();
            this.Controls.Add(sm);
        }

        /// <summary>
        /// Will remove the current UserControl on the screen and replace it with
        /// a new one
        /// </summary>
        /// <param name="current">The UserControl to be closed</param>
        /// <param name="next">The name of the UserControl to be opened</param>
        public static void ChangeScreen(UserControl current, string next)
        {
            //f is set to the form that the current control is on
            Form f = current.FindForm();
            f.Controls.Remove(current);
            UserControl ns = null;

            ///If any screens, (UserControls), are added to the program they need to
            ///be added within this switch block as well.
            switch (next)
            {
                case "MenuScreen":
                    ns = new StartMenu();
                    break;
                case "GameScreen":
                    ns = new GameScreen();
                    break;
            }

            //centres the control on the screen
            ns.Location = new Point((f.Width - ns.Width) / 2, (f.Height - ns.Height) / 2);

            f.Controls.Add(ns);
            ns.Focus();
        }
    }
}
