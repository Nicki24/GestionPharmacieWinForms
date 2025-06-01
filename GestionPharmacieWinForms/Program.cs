using System;
using System.Windows.Forms;

namespace GestionPharmacieWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin()); // Lancer FormDashboard au lieu de Form1
        }
    }
}