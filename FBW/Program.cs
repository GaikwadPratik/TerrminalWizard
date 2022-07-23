using TerminalWizard.FBW.Steps;
using Terminal.Gui;
using System.Collections.Generic;
using System;

namespace TerminalWizard.FBW
{

  public interface IFBWStep
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

    private Wizard wizard = null;
    private MenuBar menu = null;
    private Dictionary<string, IFBWStep> configurationSteps = null;

    private HiveConfig initialConfig = new HiveConfig();

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

      menu = GenerateMenuBar();

      wizard = new Wizard(title: "First Boot Wizard")
      {
        X = 0,
        Y = 0,
        Width = Dim.Fill(),
        Height = Dim.Fill(),
        ColorScheme = Colors.Dialog,
      };

      //THIS IS STILL NOT WORKING
      wizard.Modal = false;

      ConfigureWizardInstance();

      LoadConfigurationSteps();

      Win.Add(views: new Terminal.Gui.View[] { menu, wizard });
    }

    #region ConfigureWizardInstance
    private void ConfigureWizardInstance()
    {
      // var stepIndex = 0;

      // wizard.MovingBack += (args) =>
      // {
      //   if (stepIndex > 0)
      //   {
      //     stepIndex--;
      //   }
      // };

      // wizard.MovingNext += (args) =>
      // {
      //   var instance = lstInstance[stepIndex];
      //   if (instance != null)
      //   {
      //     var result = instance.ReadValues(hiveConfig: ref inConfig);
      //     if (!result)
      //     {
      //       args.Cancel = true;
      //     }
      //   }
      //   if (stepIndex < lstInstance.Count)
      //   {
      //     stepIndex++;
      //   }
      // };

      wizard.StepChanging += (args) =>
      {
        if (args.OldStep == null) {
          return;
        }
        var oldStepTitle = args.OldStep.Title.ToString();
        if (!configurationSteps.TryGetValue(key: oldStepTitle, value: out var stepInstance))
        {
          throw new TypeLoadException(message: $"Unable to load step instance for - {oldStepTitle}");
        }
        if (stepInstance == null)
        {
          throw new NullReferenceException(message: oldStepTitle);
        }
        args.Cancel = !stepInstance.ReadValues(hiveConfig: ref initialConfig);
      };

      wizard.Finished += (args) =>
      {
        // var instance = lstInstance[stepIndex];
        // if (instance != null)
        // {
        //   var result = instance.ReadValues(hiveConfig: ref inConfig);
        //   if (!result)
        //   {
        //     args.Cancel = true;
        //     return;
        //   }
        // }
        Application.RequestStop(wizard);
        //Win.Remove(view: wizard);
      };
    }
    #endregion ConfigureWizardInstance

    #region GenerateMenuBar
    private MenuBar GenerateMenuBar()
    {
      return new MenuBar(
        menus: new MenuBarItem[]
        {
          new MenuBarItem (
            title: "_File",
            children: new MenuItem []
            {
              new MenuItem (
                title: "_Quit",
                help: "",
                action: () =>
                {
                  Application.RequestStop(wizard);
                }
              ),
              new MenuItem (
                title: "Re_boot Server",
                help: "",
                action: () =>
                {
                  MessageBox.Query (
                    title: "Wizaard",
                    message: "Are you sure you want to reboot the server start over?",
                    buttons: new NStack.ustring[] { "Ok", "Cancel" }
                  );
                }
              ),
              new MenuItem (
                title: "_Shutdown Server",
                help: "",
                action: () =>
                {
                  MessageBox.Query (
                    title: "Wizaard",
                    message: "Are you sure you want to cancel setup and shutdown?",
                    buttons: new NStack.ustring[] { "Ok", "Cancel" }
                  );
                }
              ),
            }
          )
        }
      );
    }
    #endregion GenerateMenuBar

    #region LoadConfigurationSteps
    private void LoadConfigurationSteps()
    {
      var eulaStep = new EulaStep();
      var hostnameStep = new HostnameStep();
      var passwordStep = new PasswordStep();

      configurationSteps = new Dictionary<string, IFBWStep>()
      {
        { eulaStep.Title, eulaStep },
        { hostnameStep.Title, hostnameStep },
        { passwordStep.Title, passwordStep },
      };

      foreach (var step in configurationSteps)
      {
        if (string.IsNullOrWhiteSpace(step.Key))
        {
          throw new MissingMemberException("Title for the step is missing");
        }
        Console.WriteLine($"Loading - {step.Key}");
        wizard.AddStep(newStep: step.Value.LoadView());
      }
    }
    #endregion LoadConfigurationSteps
  }
}