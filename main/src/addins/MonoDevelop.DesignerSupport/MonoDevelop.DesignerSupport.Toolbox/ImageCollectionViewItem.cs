using System;
using AppKit;
using CoreGraphics;

namespace MonoDevelop.DesignerSupport.Toolbox
{
	public class ImageCollectionViewItem : NSCollectionViewItem
	{
		internal const string Name = "ImageViewItem";
		ImageContentCollectionViewItem view;

		public NSImage Image {
			get => view.Image;
			set => view.Image = value;
		}

		public override void LoadView ()
		{
			View = view = new ImageContentCollectionViewItem (Name);
		}

		public ImageCollectionViewItem (IntPtr handle) : base (handle)
		{

		}
	}

	class ImageContentCollectionViewItem : NSView
	{
		readonly NSImageView image;

		public NSImage Image {
			get => image.Image;
			set => image.Image = value;
		}

		public override void DrawRect (CGRect dirtyRect)
		{
			base.DrawRect (dirtyRect);
		}

		public ImageContentCollectionViewItem (string identifier)
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			image = new NSImageView () { TranslatesAutoresizingMaskIntoConstraints = false };
			AddSubview (image);
			image.TopAnchor.ConstraintEqualToAnchor (TopAnchor, 0).Active = true;
			image.LeftAnchor.ConstraintEqualToAnchor (LeftAnchor, 0).Active = true;
			image.RightAnchor.ConstraintEqualToAnchor (RightAnchor, 0).Active = true;
			image.BottomAnchor.ConstraintEqualToAnchor (BottomAnchor, 0).Active = true;
			Identifier = identifier;
		}
	}
}
