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
            var _mainWindow = new Window("Hive Appliance Console")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var menu = new MenuBar(new MenuBarItem [] 
            {
                new MenuBarItem("_Menu", new MenuItem [] 
                {
                    new MenuItem ("_Quit", "", () => 
                    { 
                        if (Quit ()) 
                        {
                            Application.RequestStop(); 
                        }
                    })
                })
            });

            var _formWindow = new FrameView("")
            {
                Height = Dim.Percent(99)
            };

            var _eula = new Eula();
            var _hostname = new Hostname();

            var _childFormContainer = _eula.LoadView(); 
            var _childFormContainer1 = _hostname.LoadView();
            _formWindow.Add(_childFormContainer);

            var _btnNext = new Button("Next") 
            { 
                Id = "btnNext",
                X = Pos.Center() + 4,
                Y = Pos.Bottom(_formWindow),
                Clicked = () => {
                    _formWindow.RemoveAll();
                    _formWindow.Add(_childFormContainer1);
                }
            };
            var _btnPrevious = new Button("Previous")
            {
                Id = "btnPrevious",
                X = Pos.Center() - 10,
                Y = Pos.Bottom(_formWindow)
            };

            _mainWindow.Add(_formWindow, _btnPrevious, _btnNext);

            top.Add(
                menu,
                _mainWindow
            );
            Application.Run();
        }

        static bool Quit ()
        {
            var n = MessageBox.Query (50, 7, "Quit setup", "Are you sure you want to quit setup process?", "Yes", "No");
            return n.Equals(0);
        }
    }
}

