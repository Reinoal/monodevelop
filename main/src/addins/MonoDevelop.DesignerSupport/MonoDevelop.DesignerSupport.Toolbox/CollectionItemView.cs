using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace MonoDevelop.DesignerSupport.Toolbox
{

	public interface ITextView
	{
		string Label { get; set; }
	}

	public class LabelCollectionViewItem : NSCollectionViewItem
	{
		internal const string Name = "LabelViewItem";
		ImageLabelContentCollectionViewItem view;

		public string Label {
			get => view.Label;
			set => view.Label = value;
		}

		public override void LoadView ()
		{
			View = view = new ImageLabelContentCollectionViewItem (Name);
		}

		public LabelCollectionViewItem (IntPtr handle) : base (handle)
		{

		}
	}

	public class HeaderCollectionViewItem : NSCollectionViewItem, ITextView
	{
		internal const string Name = "HeaderViewItem";
		ImageLabelContentCollectionViewItem view;

		public string Label {
			get => view.Label;
			set => view.Label = value;
		}

		public override void LoadView ()
		{
			View = view = new ImageLabelContentCollectionViewItem (Name);
		}

		public HeaderCollectionViewItem (IntPtr handle) : base (handle)
		{

		}
	}

	class ImageLabelContentCollectionViewItem : NSView, ITextView
	{
		readonly NSTextField label;

		public string Label {
			get => label.StringValue;
			set => label.StringValue = value;
		}

		public override void DrawRect (CGRect dirtyRect)
		{
			base.DrawRect (dirtyRect);
		}

		public ImageLabelContentCollectionViewItem (string identifier)
		{
			TranslatesAutoresizingMaskIntoConstraints = false;

			label = new NSTextField ();
			AddSubview (label);
			label.CenterYAnchor.ConstraintEqualToAnchor (CenterYAnchor).Active = true;
			label.LeftAnchor.ConstraintEqualToAnchor (LeftAnchor, 20).Active = true;
			Identifier = identifier;
		}
	}
}
