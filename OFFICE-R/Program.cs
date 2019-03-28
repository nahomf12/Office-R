using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OFFICE_R.Stores;
using OFFICE_R.Models;
using System.IO;

namespace OFFICE_R
{ 
    static class Program
    {
        public static Database database = new Database();
        public static SecurityStore securityStore = new SecurityStore();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                database.CreateTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
        }
    }
}
