using System;
using System.Threading.Tasks;
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

            var formWindow = new FrameView(title: "")
            {
                Height = Dim.Percent(99)
            };

            var eula = new Eula();
            var hostname = new Hostname();

            var eulaContainer = eula.LoadView(); 
            var hostnameContainer = hostname.LoadView();
            formWindow.Add(eulaContainer);

            var btnNext = new Button(text: "Next") 
            { 
                Id = "btnNext",
                X = Pos.Center() + 4,
                Y = Pos.Bottom(formWindow)
            };
            btnNext.Clicked += () => 
            {
                Application.RequestStop();
                formWindow.RemoveAll();
                formWindow.Add(hostnameContainer);
            };
            var btnPrevious = new Button("Previous")
            {
                Id = "btnPrevious",
                X = Pos.Center() - 10,
                Y = Pos.Bottom(formWindow),
            };
            btnPrevious.Clicked += () => 
            {
                Application.RequestStop();
                formWindow.RemoveAll();
                formWindow.Add(eulaContainer);
            };

            mainWindow.Add(formWindow, btnPrevious, btnNext);

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

