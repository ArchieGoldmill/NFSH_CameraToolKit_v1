using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CameraController
{
	static class Program
	{

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

#if !DEBUG
			try
			{
#endif
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
#if !DEBUG
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);

				Environment.Exit(0);
			}
#endif
		}
	}
}
