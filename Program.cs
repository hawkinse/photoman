using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Photoman
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Enable visual styles. Needed for fancy XP/Vista/7 UI Elements
            Application.EnableVisualStyles();

            //Use native text rendering
            Application.SetCompatibleTextRenderingDefault(false);

            Global.DebugOutput = new Photoman.DebugConsole();
            Global.DebugOutput.Show();

            //Write to log that we're starting, version number, etc.
            Global.WriteToLog("Photoman v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(), true, true);
            Global.WriteToLog("Copyright © Elliot Hawkins 2016\n\n", true, true);

            //Test out ram statistics functions
            Global.WriteToLog("         Total Physical Ram: " + Global.GetTotalMemory().ToString(), true, true);
            Global.WriteToLog("         Free  Physical Ram: " + Global.GetFreeMemory().ToString(), true, true);
            Global.WriteToLog(" Used Physical Memory Total: " + Global.GetUsedMemoryBySys().ToString() + "\n", true, true);
                       
            //Create our controller

            Controller.Controller controller = new Photoman.Controller.Controller();
            
            //Create a UI and pass controller to it
            UserInterface.TestGUI_Controller UI = new Photoman.UserInterface.TestGUI_Controller(controller);
                        
            //Stop application exit until UI quits
            Application.Run();
            
            Global.WriteToLog("Application exited cleanly", true);
            Global.SaveLog();
        }
    }
}
