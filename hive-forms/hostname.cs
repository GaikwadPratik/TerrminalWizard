using Terminal.Gui;

namespace TerminalWizard
{
  public class Hostname : HiveView
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
      if (txtHostname == null)
      {
        return false;
      }
      test.Hostname = txtHostname.Text.ToString();
      return true;
    }
  }
}