using Terminal.Gui;

namespace TerminalWizard.FBW.Steps
{
  public class HostnameStep : IFBWStep {
    public string Title { get; } = "Set Host Name";
    protected TextField txtHostname = null;

    public Wizard.WizardStep LoadView() {
      var container = new Wizard.WizardStep(title: Title);
      var lblHostname = new Label(text: "Hostname: ") {
        Id = "lblHostname"
      };
      container.Add(view: lblHostname);

      txtHostname = new TextField("Hive 1") {
        X = Pos.Right(view: lblHostname),
        Y = Pos.Top(view: lblHostname),
        Width = Dim.Percent(35),
        Id = "txtHostname"
      };
      container.HelpText = "Hostname Help \nEnter the hostname for the appliance.";

      container.Add(view: txtHostname);

      return container;
    }

    public bool ReadValues(ref HiveConfig hiveConfig)
    {
      if (txtHostname.Text == null || txtHostname.Text.Length == 0) {
        MessageBox.ErrorQuery(title: "Validation Error", message: "Hostname can not be blank", buttons: new NStack.ustring[] { "Ok" });
        return false;
      }
      hiveConfig.Hostname = txtHostname.Text.ToString();
      return true;
    }

  }
}