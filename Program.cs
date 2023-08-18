using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeAss
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Instantiate the classes that implement the interfaces
            IGitRepositoryManager gitRepoManager = new CodeAss();
            IGPTSeedingService gptSeedingService = new CodeAss();

            Application.Run(new MainForm(gitRepoManager, gptSeedingService));
        }
    }
}
