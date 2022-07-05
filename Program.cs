using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace TerminalWizard
{
  class Program
  {
    private static Toplevel top = null;
    private static Scenario runningScenario = null;
    private static MenuBar menu = null;

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      Application.Init();

      var scenarios = Scenario.GetDerivedClasses<Scenario>().OrderBy(t => Scenario.ScenarioMetadata.GetName(t)).ToList();

      menu = new MenuBar(menus: new MenuBarItem[]{
        new MenuBarItem (title: "_File", children: new MenuItem [] {
					new MenuItem (title: "_Quit", help: "", action: () => Application.RequestStop(), canExecute: null, parent: null, shortcut: Key.Null)
				})
      });

      top = Application.Top;
      top.Add(menu);

      var scenarioNameToLoad = "FirstBootWizard";
      var itemIndex = scenarios.FindIndex(t => Scenario.ScenarioMetadata.GetName(t).Equals(scenarioNameToLoad, StringComparison.OrdinalIgnoreCase));
      runningScenario = (Scenario)Activator.CreateInstance(scenarios[itemIndex]);
      if (runningScenario == null) {
        Console.WriteLine($"Unable to load scenario - {scenarioNameToLoad}");
        return;
      }
      runningScenario.Init(top, Colors.Dialog);
      runningScenario.Setup();
      runningScenario.Run();
      Application.Shutdown();
    }
  }
}

