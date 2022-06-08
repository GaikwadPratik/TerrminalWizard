using System.Collections.Generic;
using NStack;

namespace Terminal.Gui {
	/// <summary>
    /// Provides a step-based Wizard.
	/// </summary>
	/// <remarks>
	/// </remarks>
	public class Wizard : Dialog {
        /// <summary>
        /// One step for the Wizard.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public class WizardStep : View {
            public ustring Title;
            public View ControlPane = new View();
            public TextView HelpPane = new TextView();

            /// <summary>
            /// Initializes a new instance of the <see cref="Wizard"/> class using <see cref="LayoutStyle.Computed"/> positioning.
            /// </summary>
            /// <param name="title">Title for the Step. Will be appended to the containing Wizard's title as 
            // "Wizard Title - Wizard Step Title" when this step is active.</param>
            /// <remarks>
            /// </remarks>
            public WizardStep (ustring title) {
                Title = title;
                Height = Dim.Fill(2); // for button frame
                Width = Dim.Fill();

                ControlPane.ColorScheme = Colors.Dialog;
                ControlPane.Height = Dim.Fill();
                ControlPane.Width = Dim.Percent(50);

                HelpPane.ColorScheme = Colors.Dialog;
                HelpPane.X = Pos.Right(ControlPane) + 1;
                HelpPane.Height = Dim.Fill();
                HelpPane.Width = Dim.Fill();

                this.Add(ControlPane);

                var separator = new LineView(Graphs.Orientation.Vertical);
                separator.X = Pos.Right(ControlPane);
                separator.Height = Dim.Fill();
                this.Add(separator);

                this.Add(HelpPane);
            }
        }

        public List<WizardStep> Steps = new List<WizardStep>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Wizard"/> class using <see cref="LayoutStyle.Computed"/> positioning.
		/// </summary>
		/// <param name="title">Title for the Wizard.</param>
		/// <param name="buttons">Optional buttons for the bottom of the Wizard.</param>
		/// <remarks>
		/// The Wizard will be vertically and horizontally centered in the container and the size will be 85% of the container. 
		/// After initialization use <c>X</c>, <c>Y</c>, <c>Width</c>, and <c>Height</c> to override this with a location or size.
		/// </remarks>
		public Wizard (ustring title, params Button [] buttons) : base (title: title, width: 0, height: 0, buttons: buttons) {
            if (buttons.Length > 0) { 
                buttons[buttons.Length-1].IsDefault = true;
            }
        }

    }
}