using System;
using Terminal.Gui;

namespace TerminalWizard
{
    public class Hostname
    {
        public string Title { get; set; }

        public FrameView LoadView()
        {
            var container = new FrameView("Host name")
            {
                Height = Dim.Fill(),
                Width = Dim.Fill(),
                Id = "frmHostname"
            };

            var _textView = new TextField("Test2")
            {
                X = 10,
                Y = 10,
                Width = Dim.Fill() - 25,
                Id = "txthostname"
            };

            container.Add(_textView);

            return container;
        }
    }
}