using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace TerminalWizard
{
  public interface HiveView
  {
    Wizard.WizardStep LoadView();
    bool ReadValues(ref HiveConfig hiveConfig);
  }

  public class HiveConfig
  {
    public string Hostname { get; set; }
    public string Password { get; set; }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      Application.Init();

      var menu = new MenuBar(menus: new MenuBarItem [] 
      {
          new MenuBarItem(title: "_Menu", children: new MenuItem [] 
          {
              new MenuItem (title: "_Quit", help: "", action: () => 
              { 
                  if (Quit()) 
                  {
                      Application.Top.Running = false;
                  }
              }),
              new MenuItem(title: "_Restart", help: "", action: () =>
              {

              })
          })
      });

      var inConfig = new HiveConfig();
      var wizard = new Wizard("Hive Appliance Console");

      var eulaStep = new Eula();
      var hostnameStep = new Hostname();
      var passwordStep = new Password();
      //TODO: Can we use LinkedList instead?
      var lstInstance = new List<HiveView>()
      {
        //Keep this list as the step to be shown in FBW
        eulaStep,
        hostnameStep,
        passwordStep
      };
      foreach (var step in lstInstance)
      {
        wizard.AddStep(newStep: step.LoadView());
      }
      var stepIndex = 0;
      
      wizard.MovingBack += (args) =>
      {
        stepIndex--;
      };

      wizard.MovingNext += (args) =>
      {
        var instance = lstInstance[stepIndex];
        if (instance != null)
        {
          var result = instance.ReadValues(hiveConfig: ref inConfig);
          if (!result)
          {
            args.Cancel = true;
          }
        }
        stepIndex++;
      };

      wizard.Finished += (args) =>
      {
        var instance = lstInstance[stepIndex];
        if (instance != null)
        {
          var result = instance.ReadValues(hiveConfig: ref inConfig);
          if (!result)
          {
            args.Cancel = true;
          }
        }
      };

      Application.Run(view: wizard);
    }
    static bool Quit()
    {
        var n = MessageBox.Query (width: 50, height: 7, title: "Quit first boot wizard", message: "Are you sure you want to quit setup process?", buttons: new NStack.ustring[] { "Yes", "No" });
        return n.Equals(0);
    }
  }
}

