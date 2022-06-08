using System;
using Terminal.Gui;

namespace TerminalWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Application.Init();
            var top = Application.Top;

            //Create top level window to show
            var mainWindow = new Window(title: "Hive Appliance Console")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var menu = new MenuBar(menus: new MenuBarItem [] 
            {
                new MenuBarItem(title: "_Menu", children: new MenuItem [] 
                {
                    new MenuItem (title: "_Quit", help: "", action: () => 
                    { 
                        if (Quit()) 
                        {
                            top.Running = false;
                        }
                    })
                })
            });

            var backBtn = new Button("Back");
            var nextBtn = new Button("Next");
            nextBtn.IsDefault = true;
            var wizard = new Wizard("Demo Wizard", new Button[]{ backBtn, nextBtn });
            wizard.Steps.Add(new Wizard.WizardStep("First Step"));
            wizard.Steps[0].HelpPane.Text = "This is the help text for the First Step.";
            var editLbl = new Label() { Text = "Edit: ", AutoSize = true, X = 1, Y = 10 };

            var edit = new TextField() {
                Text = "hello world",
                X = Pos.Right(editLbl),
                Y = Pos.Top(editLbl),
                Width = 40,
            };
            wizard.Steps[0].ControlPane.Add(editLbl, edit);

            wizard.Add(wizard.Steps[0]);
            mainWindow.Add(wizard);

            top.Add(
                menu,
                mainWindow
            );
            Application.Run();
        }

        static bool Quit()
        {
            var n = MessageBox.Query (50, 7, "Quit setup", "Are you sure you want to quit setup process?", "Yes", "No");
            return n.Equals(0);
        }
    }
}

