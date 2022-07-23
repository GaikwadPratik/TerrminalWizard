using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;
using TerminalWizard.FBW;

namespace TerminalWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            var fbw = new FirstBootWizard(Application.Top);

            // When Wizard runs as non-modal, the Wizard title is not displayed
            // Here, we add a centered label below the menu bar and above the
            // Wizard to give the user some context.
            Application.Top.Add(new Label("Hive Appliance Setup Wizard") { 
                ColorScheme = Colors.Base, 
                Y = 1, Width = Dim.Fill(),
                TextAlignment = TextAlignment.Centered
            });

            fbw.Y = 2; // Room for the Title label

            Application.Top.Add(fbw.GenerateMenuBar());
            Application.Top.Add(fbw);

            Application.Run();
            Application.Shutdown();
        }
    }
}

