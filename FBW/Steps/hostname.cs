using Terminal.Gui;

namespace TerminalWizard.FBW.Steps
{
  public class HostnameStep : IFBWSteps
  {
    protected TextField txtHostname = null;
    public Wizard.WizardStep LoadView()
    {
      var container = new Wizard.WizardStep(title: "Host name");
      var lblHostname = new Label(text: "Set Hostname: ")
      {
        Id = "lblHostname"
      };
      container.Controls.Add(view: lblHostname);

      txtHostname = new TextField("Hive 1")
      {
        X = Pos.Right(view: lblHostname),
        Y = Pos.Top(view: lblHostname),
        Width = Dim.Percent(35),
        Id = "txtHostname"
      };
      container.HelpText = "Hostname Help \n Enter the hostname for the appliance.";

      container.Controls.Add(view: txtHostname);

      return container;
    }

    public bool ReadValues(ref HiveConfig test)
    {
      if (txtHostname.Text == null || txtHostname.Text.Length == 0)
      {
        MessageBox.ErrorQuery(title: "Validation Error", message: "Hostname can not be blank", buttons: new NStack.ustring[] { "Ok" });
        return false;
      }
      test.Hostname = txtHostname.Text.ToString();
      return true;
    }
  }
}