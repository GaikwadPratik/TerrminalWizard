using TerminalWizard.FBW.Steps;
using Terminal.Gui;
using System.Collections.Generic;

namespace TerminalWizard.FBW
{

  public interface IFBWSteps
  {
    Wizard.WizardStep LoadView();
    bool ReadValues(ref HiveConfig hiveConfig);
  }

  public class HiveConfig
  {
    public string Hostname { get; set; }
    public string Password { get; set; }
  }


  [ScenarioMetadata(Name: "FirstBootWizard", Description: "Steps for initial configuration")]
  [ScenarioCategory(Name: "FBW")]
  public class FirstBootWizard : Scenario
  {
    public override void Setup()
    {
      if (Top != null)
      {
        void Top_Loaded()
        {
          Top.Loaded -= Top_Loaded;
        }
        Top.Loaded += Top_Loaded;
      }

      var inConfig = new HiveConfig();
      var wizard = new Wizard(title: "First Boot Wizard")
      {
        X = 0,
        Y = 0,
        Width = Dim.Fill(),
        Height = Dim.Fill(),
        ColorScheme = Colors.Dialog
      };

      var eulaStep = new EulaStep();
      var hostnameStep = new HostnameStep();
      var passwordStep = new PasswordStep();

      var lstInstance = new List<IFBWSteps>()
      {
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
        if (stepIndex > 0)
        {
          stepIndex--;
        }
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
        if (stepIndex < lstInstance.Count)
        {
          stepIndex++;
        }
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
            return;
          }
          Application.RequestStop(wizard);
        }
      };

      Win.Add(view: wizard);
    }
  }
}