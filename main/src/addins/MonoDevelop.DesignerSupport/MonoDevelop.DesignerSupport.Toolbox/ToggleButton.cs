using System;

using AppKit;
using CoreGraphics;
using Foundation;

namespace MonoDevelop.DesignerSupport.Toolbox
{
	class ImagePressedButton : NSButton
	{
		bool isPressed;
		public ImagePressedButton (Xwt.Drawing.Image unpressed)
		{
			Bordered = true;
			BezelStyle = NSBezelStyle.Rounded;
			Title = "";
			//SetButtonType(NSButtonType.);
			//ShowsBorderOnlyWhileMouseInside = true;
			Image = unpressed.ToNative ();
			ImageScaling = NSImageScale.AxesIndependently;
			TranslatesAutoresizingMaskIntoConstraints = false;
		}

		public bool Visible {
			get => !Hidden;
			set => Hidden = !value;
		  }

		//public override void MouseDown(NSEvent theEvent)
		//{
		//	isPressed = true;
		//	base.MouseDown(theEvent);
		//	isPressed = false;
		//}

		//public override void DrawRect(CGRect dirtyRect)
		//{
		//	base.DrawRect(dirtyRect);

		//	var context = NSGraphicsContext.CurrentContext;
		//	context.SaveGraphicsState();
		//	//if (isPressed) {

		//	//} else {
		//	//	ImageUnpressed?.Draw(Bounds);
		//	//}
		//	tmp?.Draw(Bounds);
		//	context.RestoreGraphicsState();
		//}
	}

	class ToggleButton : NSButton
	{
		public event EventHandler Toggled;

		public override void MouseDown (NSEvent theEvent)
		{
			base.MouseDown (theEvent);
		}

		public override void PerformClick (NSObject sender)
		{
			base.PerformClick (sender);
		}

		public bool IsToggled {
			get => State == NSCellStateValue.On;
			set {
				if (IsToggled == value) {
					return;
				}
				State = value ? NSCellStateValue.On : NSCellStateValue.Off;
				Toggled?.Invoke (this, EventArgs.Empty);
			}
		}

		public ToggleButton (Xwt.Drawing.Image imageToggled)
		{
			Bordered = true;
			BezelStyle = NSBezelStyle.Rounded;
			Title = "";
			SetButtonType (NSButtonType.OnOff);
			//ShowsBorderOnlyWhileMouseInside = true;
			Image = imageToggled.ToNative ();
			ImageScaling = NSImageScale.AxesIndependently;
			TranslatesAutoresizingMaskIntoConstraints = false;
		}

		//public override void DrawRect (CGRect dirtyRect)
		//{
		//	base.DrawRect (dirtyRect);

		//	// General Declarations
		//	var context = NSGraphicsContext.CurrentContext;
		//	// Image Declarations
		//	// Rectangle Drawing
		//	context.SaveGraphicsState ();
		//	//if (isToggled) {

		//	//} else {
		//	//	ImageUntoggled?.Draw (Bounds);
		//	//}

		//	tmp?.Draw(Bounds);

		//	context.RestoreGraphicsState ();
		//}
	}
}
