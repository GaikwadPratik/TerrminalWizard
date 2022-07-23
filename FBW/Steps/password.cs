using Terminal.Gui;

namespace TerminalWizard.FBW.Steps
{
  public class PasswordStep : IFBWStep {
    public string Title { get; } = "PasswordStep";
    protected TextField txtPassword = null;
    protected string newPassword = "";
    protected string confirmPassword = "";
    public Wizard.WizardStep LoadView()
    {
      var container = new Wizard.WizardStep(title: Title);
      container.HelpText = "Admin User Password Help \n Set the password for the admin user, this is the administrative account used to login to the web administrative.";
      var lblPassword = new Label(text: "New Password: ")
      {
        Id = "lblPassword",
        X = 1,
        AutoSize = true
      };

      container.Add(view: lblPassword);

      txtPassword = new TextField(text: "")
      {
        X = Pos.Right(view: lblPassword),
        Y = Pos.Top(view: lblPassword),
        Width = Dim.Percent(35),
        Id = "txtPassword"
      };

      txtPassword.Enter += (args) =>
      {
        if (newPassword.Length > 0)
        {
          txtPassword.Text = newPassword;
        }
      };

      txtPassword.Leave += (args) =>
      {
        newPassword = txtPassword.Text.ToString();
        if (newPassword.Length > 0)
        {
          txtPassword.Text = "******";
        }
      };

      container.Add(txtPassword);

      var lblConfirmPassword = new Label(text: "Confirm Password: ")
      {
        Id = "lblConfirmPassword",
        X = 1,
        Y = Pos.Bottom(view: lblPassword) + 1,
        AutoSize = true
      };
      container.Add(view: lblConfirmPassword);

      var txtConfirmPassword = new TextField("")
      {
        X = Pos.Right(view: lblConfirmPassword),
        Y = Pos.Top(view: lblConfirmPassword),
        Width = Dim.Percent(35),
        Id = "txtConfirmPassword"
      };

      txtConfirmPassword.Enter += (args) =>
      {
        if (confirmPassword.Length > 0)
        {
          txtConfirmPassword.Text = confirmPassword;
        }
      };

      txtConfirmPassword.Leave += (args) =>
      {
        confirmPassword = txtConfirmPassword.Text.ToString();
        if (confirmPassword.Length > 0)
        {
          txtConfirmPassword.Text = "******";
        }
      };

      container.Add(view: txtConfirmPassword);

      return container;
    }

    public bool ReadValues(ref HiveConfig hiveConfig)
    {
      if (newPassword.Length == 0)
      {
        MessageBox.ErrorQuery(title: "Validation Error", message: "New password can not be blank", buttons: new NStack.ustring[] { "Ok" });
        return false;
      }
      if (confirmPassword.Length == 0)
      {
        MessageBox.ErrorQuery(title: "Validation Error", message: "Confirm password can not be blank", buttons: new NStack.ustring[] { "Ok" });
        return false;
      }
      if (newPassword != confirmPassword)
      {
        MessageBox.ErrorQuery(title: "Validation Error", message: "New password and confirm password do not match", buttons: new NStack.ustring[] { "Ok" });
        return false;
      }
      hiveConfig.Password = newPassword;
      return true;
    }
  }
}