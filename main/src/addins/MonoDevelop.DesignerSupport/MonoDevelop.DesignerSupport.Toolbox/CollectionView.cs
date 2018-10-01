using System;
using System.Collections.Generic;
using AppKit;
using CoreGraphics;
using Foundation;

namespace MonoDevelop.DesignerSupport.Toolbox
{
	[Register ("CollectionView")]
	public class CollectionView : NSCollectionView
	{
		CollectionDataSource dataSource;
		CollectionViewDelegateFlowLayout collectionViewDelegate;
		NSCollectionViewFlowLayout flowLayout;

		List<CollectionHeaderItem> items = new List<CollectionHeaderItem> ();

		//public override NSView MakeSupplementaryView (NSString elementKind, string identifier, NSIndexPath indexPath)
		//{
		//	var item = MakeItem (identifier, indexPath) as HeaderCollectionViewItem;
		//	var selectedItem = items[(int)indexPath.Section];
		//	item.Label = selectedItem.Label;
		//	item.View.TranslatesAutoresizingMaskIntoConstraints = false;
		//	return item.View;
		//}

		// Called when created from unmanaged code
		//public CollectionView (IntPtr handle) : base (handle)
		//{
		//	Initialize ();
		//}

		public CollectionView ()
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		//[Export ("initWithCoder:")]
		//public CollectionView (NSCoder coder) : base (coder)
		//{
		//	Initialize ();
		//}

		// Shared initialization code
		void Initialize ()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			RegisterClassForItem (typeof (HeaderCollectionViewItem), HeaderCollectionViewItem.Name);
			RegisterClassForItem (typeof (LabelCollectionViewItem), LabelCollectionViewItem.Name);
			RegisterClassForItem (typeof (ImageCollectionViewItem), ImageCollectionViewItem.Name);

			flowLayout = new NSCollectionViewFlowLayout ();
			flowLayout.HeaderReferenceSize = new CGSize (100, 30);
			flowLayout.SectionInset = new NSEdgeInsets (top: 10.0f, left: 20.0f, bottom: 10.0f, right: 20.0f);
			flowLayout.MinimumInteritemSpacing = 20.0f;
			flowLayout.MinimumLineSpacing = 20.0f;

			CollectionViewLayout = flowLayout;
			Delegate = collectionViewDelegate = new CollectionViewDelegateFlowLayout ();
			DataSource = dataSource = new CollectionDataSource (items);
			Selectable = true;
			AllowsMultipleSelection = true;
			AllowsEmptySelection = true;
		}

		public bool IsCompactMode { get; private set; }
		public bool IsImageMode { get; private set; }

		public void CompactMode ()
		{
			if (IsCompactMode) {
				flowLayout.HeaderReferenceSize = new CGSize (100, 30);
			} else {
				flowLayout.HeaderReferenceSize = CGSize.Empty;
			}
			IsCompactMode = !IsCompactMode;
			collectionViewDelegate.IsOnlyImage = dataSource.IsOnlyImage = false;
			ReloadData ();
		}

		public void ImageMode ()
		{
			if (IsImageMode) {
				flowLayout.HeaderReferenceSize = new CGSize (100, 30);
			} else {
				flowLayout.HeaderReferenceSize = CGSize.Empty;
			}
		
			collectionViewDelegate.IsOnlyImage = dataSource.IsOnlyImage = true;
			ReloadData ();
		}

		public bool IsOnlyImage {
			get => dataSource.IsOnlyImage;
			set {
				if (value == dataSource.IsOnlyImage) {
					return;
				}
				collectionViewDelegate.IsOnlyImage = dataSource.IsOnlyImage = value;
			}
		}

		public string CustomMessage { get; internal set; }

		public void SetData (List<CollectionHeaderItem> items)
		{
			this.items.Clear ();
			this.items.AddRange (items);
		}

	}
	public class CollectionHeaderItem
	{
		public string Label { get; set; }
		public List<CollectionItem> Items = new List<CollectionItem> ();
	}

	public class CollectionItem
	{
		public string Label { get; set; }
		public NSImage Image { get; set; }
	}

	class CollectionViewDelegateFlowLayout : NSCollectionViewDelegateFlowLayout
	{
		internal int ItemRightMargin = 10;
		public bool IsOnlyImage { get; set; }

		public CollectionViewDelegateFlowLayout ()
		{
		}

		public override CGSize SizeForItem (NSCollectionView collectionView, NSCollectionViewLayout collectionViewLayout, NSIndexPath indexPath)
		{
			if (IsOnlyImage) {
				return new CGSize (50, 50);
			}
			return new CGSize (collectionView.Frame.Width - ItemRightMargin, 50);
		}

		public override NSSet ShouldDeselectItems (NSCollectionView collectionView, NSSet indexPaths)
		{
			return indexPaths;
		}
		public override NSSet ShouldSelectItems (NSCollectionView collectionView, NSSet indexPaths)
		{
			return indexPaths;
		}
	}

	class CollectionDataSource : NSCollectionViewDataSource
	{
		public bool IsOnlyImage { get; set; }

		List<CollectionHeaderItem> items;
		public CollectionDataSource (List<CollectionHeaderItem> items)
		{
			this.items = items;
		}

		public override NSCollectionViewItem GetItem (NSCollectionView collectionView, NSIndexPath indexPath)
		{
			var item = collectionView.MakeItem (IsOnlyImage ? ImageCollectionViewItem.Name : LabelCollectionViewItem.Name, indexPath);
			if (item is LabelCollectionViewItem itmView) {
				var selectedItem = items[(int)indexPath.Section].Items[(int)indexPath.Item];
				itmView.Label = selectedItem.Label;
				return itmView;
			}

			if (item is ImageCollectionViewItem imgView) {
				var selectedItem = items[(int)indexPath.Section].Items[(int)indexPath.Item];
				imgView.Image = selectedItem.Image;
			}
			return item;
		}

		//public override NSView GetView (NSCollectionView collectionView, NSString kind, NSIndexPath indexPath)
		//{
		//	return collectionView.MakeSupplementaryView (kind, HeaderCollectionViewItem.Name, indexPath);
		//}

		public override nint GetNumberofItems (NSCollectionView collectionView, nint section)
		{

			return items[(int)section].Items.Count;
		}

		public override nint GetNumberOfSections (NSCollectionView collectionView)
		{
			return items.Count;
		}
	}
}
