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
                Height = Dim.Percent(100),
                Width = Dim.Percent(100)
            };

            var _textView = new TextField("Test2")
            {
                X = 10,
                Y = 10
            };

            container.Add(_textView);

            return container;
        }
    }
}