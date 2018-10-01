/* 
 * Toolbox.cs - A toolbox widget
 * 
 * Authors: 
 *  Michael Hutchinson <m.j.hutchinson@gmail.com>
 *  
 * Copyright (C) 2005 Michael Hutchinson
 *
 * This sourcecode is licenced under The MIT License:
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit
 * persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
 * NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Design;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Components.AtkCocoaHelper;
using MonoDevelop.Components.Commands;
using MonoDevelop.Components.Docking;
using MonoDevelop.Ide;
using MonoDevelop.Components;
using AppKit;
using Xwt;
using System.Drawing;
using Xwt.Drawing;
using System.Threading.Tasks;

namespace MonoDevelop.DesignerSupport.Toolbox
{

	public class ScrollContainerView : NSScrollView
	{
		public ScrollContainerView (NSView content)
		{
			HasVerticalScroller = true;
			HasHorizontalScroller = false;
			TranslatesAutoresizingMaskIntoConstraints = false;
			DocumentView = content;
		}
	}

	public class Toolbox : NSView, IPropertyPadProvider, IToolboxConfiguration
	{
		ToolboxService toolboxService;

		ItemToolboxNode selectedNode;
		//Hashtable expandedCategories = new Hashtable ();
		CollectionView toolboxWidget;
		//ScrollContainerView scrolledWindow;

		ToggleButton catToggleButton;
		ToggleButton compactModeToggleButton;
		readonly NSSearchField filterEntry;

		MonoDevelop.Ide.Gui.PadFontChanger fontChanger;

		IPadWindow container;
		Dictionary<string, int> categoryPriorities = new Dictionary<string, int> ();

		ImagePressedButton toolboxAddButton;

		Xwt.Drawing.Image groupByCategoryImage;

		int margin = 5;

		List<CollectionHeaderItem> items = new List<CollectionHeaderItem> ();

		public Toolbox (ToolboxService toolboxService, IPadWindow container)
		{
			this.toolboxService = toolboxService;
			this.container = container;

			#region Toolbar
			//DockItemToolbar toolbar = container.GetToolbar (DockPositionType.Top);
			groupByCategoryImage = ImageService.GetIcon (Ide.Gui.Stock.GroupByCategory, Gtk.IconSize.Menu);
			var compactImage = ImageService.GetIcon ("md-compact-display", Gtk.IconSize.Menu);
			var addImage = ImageService.GetIcon (Ide.Gui.Stock.Add, Gtk.IconSize.Menu);

			var verticalStackView = NativeViewHelper.CreateVerticalStackView (10);
			AddSubview (verticalStackView);

			verticalStackView.TopAnchor.ConstraintEqualToAnchor (TopAnchor, 0).Active = true;
			verticalStackView.LeftAnchor.ConstraintEqualToAnchor (LeftAnchor, margin).Active = true;
			verticalStackView.RightAnchor.ConstraintEqualToAnchor (RightAnchor, -margin).Active = true;

			//Horizontal container
			var hotizontalToolbar = NativeViewHelper.CreateHorizontalStackView (3);
			verticalStackView.AddArrangedSubview (hotizontalToolbar);

			filterEntry = new NSSearchField ();
			filterEntry.TranslatesAutoresizingMaskIntoConstraints = false;
			filterEntry.AccessibilityTitle = GettextCatalog.GetString ("Search Toolbox");
			filterEntry.AccessibilityLabel = GettextCatalog.GetString ("Enter a term to search for it in the toolbox");

			hotizontalToolbar.AddArrangedSubview (filterEntry);

			filterEntry.SetContentHuggingPriorityForOrientation (250, NSLayoutConstraintOrientation.Horizontal);
			filterEntry.SetContentCompressionResistancePriority (250, NSLayoutConstraintOrientation.Horizontal);

			filterEntry.Changed += (s, e) => {
				RefreshData ();
			};

			//filter.

			//filterEntry.Ready = true;
			//filterEntry.HasFrame = true;
			//filterEntry.WidthRequest = 150;
			//filterEntry.Changed += new EventHandler (filterTextChanged);
			//filterEntry.Show ();
			//filterEntry.Accessible.Name = "Toolbox.SearchEntry";
			//filterEntry.Accessible.SetLabel (GettextCatalog.GetString ("Search Toolbox"));
			//filterEntry.Accessible.Description = GettextCatalog.GetString ("Enter a term to search for it in the toolbox");

			//toolbar. (filterEntry, true);
			catToggleButton = new ToggleButton (groupByCategoryImage);
			catToggleButton.ToolTip = GettextCatalog.GetString ("Show categories");

			//catToggleButton.AccessibilityTitle Accessible.Name = "Toolbox.ShowCategories";
			catToggleButton.AccessibilityTitle = GettextCatalog.GetString ("Show Categories");
			catToggleButton.AccessibilityHelp = GettextCatalog.GetString ("Toggle to show categories");
			hotizontalToolbar.AddArrangedSubview (catToggleButton);
			catToggleButton.WidthAnchor.ConstraintEqualToConstant (30).Active = true;

			catToggleButton.Toggled += toggleCategorisation;

			compactModeToggleButton = new ToggleButton (compactImage);
			compactModeToggleButton.ToolTip = GettextCatalog.GetString ("Use compact display");
			compactModeToggleButton.AccessibilityTitle = GettextCatalog.GetString ("Compact Layout");
			compactModeToggleButton.AccessibilityHelp = GettextCatalog.GetString ("Toggle for toolbox to use compact layout");
			hotizontalToolbar.AddArrangedSubview (compactModeToggleButton);
			compactModeToggleButton.WidthAnchor.ConstraintEqualToConstant (30).Active = true;

			compactModeToggleButton.Toggled += ToggleCompactMode;

			toolboxAddButton = new ImagePressedButton (addImage);
			toolboxAddButton.ToolTip = GettextCatalog.GetString ("Add toolbox items");
			toolboxAddButton.AccessibilityTitle = GettextCatalog.GetString ("Add");
			toolboxAddButton.AccessibilityHelp = GettextCatalog.GetString ("Add toolbox items");

			hotizontalToolbar.AddArrangedSubview (toolboxAddButton);
			toolboxAddButton.WidthAnchor.ConstraintEqualToConstant (30).Active = true;

			toolboxAddButton.Activated += toolboxAddButton_Clicked;

			#endregion
			defaultImage = addImage.ToNative ();
		
			toolboxWidget = new CollectionView ();
			var scrollContainer = new ScrollContainerView (toolboxWidget);

			//toolboxWidget.AccessibilityTitle = GettextCatalog.GetString ("Toolbox Items");
			//toolboxWidget.AccessibilityHelp = GettextCatalog.GetString ("The toolbox items");
			//toolboxWidget.AddColumn (new NSTableColumn ("col") { Title = "Toolbox Items" });

			verticalStackView.AddArrangedSubview (scrollContainer);
			scrollContainer.HeightAnchor.ConstraintEqualToConstant (200).Active = true;
			toolboxWidget.HeightAnchor.ConstraintEqualToConstant (300).Active = true;
			//GenerateData ();

			//var scrollContainer = new ScrollContainerView (toolboxWidget);

			//toolboxWidget.HeightAnchor.ConstraintEqualToConstant (200).Active = true;

			//toolboxWidget.SelectionChanged += delegate {
			//	if (toolboxWidget.SelectedItem is ToolboxTableViewItem itm) {
			//		selectedNode = itm.Node;
			//	} else {
			//		selectedNode = null;
			//	}
			//	//toolboxService.SelectItem (selectedNode);
			//};
			//this.toolboxWidget.DragBegin += delegate(object sender, Gtk.DragBeginArgs e) {

			//	if (this.toolboxWidget.SelectedItem != null) {
			//		this.toolboxWidget.HideTooltipWindow ();
			//		toolboxService.DragSelectedItem (this.toolboxWidget, e.Context);
			//	}
			//};
			//this.toolboxWidget.ActivateSelectedItem += delegate {
			//	toolboxService.UseSelectedItem ();
			//};

			//fontChanger = new MonoDevelop.Ide.Gui.PadFontChanger (toolboxWidget, toolboxWidget.SetCustomFont, toolboxWidget.QueueResize);

			//this.toolboxWidget.DoPopupMenu = ShowPopup;
			//scrolledWindow = new MonoDevelop.Components.CompactScrolledWindow ();
			//base.PackEnd (scrolledWindow, true, true, 0);
			//base.FocusChain = new Gtk.Widget [] { scrolledWindow };

			//Initialise self
			//scrolledWindow.ShadowType = ShadowType.None;
			//scrolledWindow.VscrollbarPolicy = PolicyType.Automatic;
			//scrolledWindow.HscrollbarPolicy = PolicyType.Never;
			//scrolledWindow.WidthRequest = 150;
			//scrolledWindow.Add (this.toolboxWidget);

			//update view when toolbox service updated
			//toolboxService.ToolboxContentsChanged += (sender, e) => Refresh ();
			//toolboxService.ToolboxConsumerChanged += (sender, e) => Refresh ();

			GenerateData ();

			//set initial state
			//this.toolboxWidget.ShowCategories = catToggleButton.IsToggled = true;
			compactModeToggleButton.IsToggled = MonoDevelop.Core.PropertyService.Get ("ToolboxIsInCompactMode", false);
			//this.toolboxWidget.IsListMode  = !compactModeToggleButton.IsToggled;
			//this.ShowAll ();
		}
		NSImage defaultImage;
	
		void GenerateData ()
		{
			//Data = new List<TableViewItem> () {
			//	new TableViewItem () { Image =  groupByCategoryImage, Label = "fdeeeeesdasdsadsddddsasadassddas" },
			//	new TableViewItem ()  { Image =  groupByCategoryImage, Label = "12" },
			//	new TableViewItem ()  { Image =  groupByCategoryImage, Label = "3" }
			//};

			var header = new CollectionHeaderItem () { Label = "header1" };
			header.Items.Add (new CollectionItem () { Label = "1", Image = defaultImage });
			header.Items.Add (new CollectionItem () { Label = "2", Image = defaultImage });
			header.Items.Add (new CollectionItem () { Label = "3", Image = defaultImage });
			items.Add (header);

			header = new CollectionHeaderItem () { Label = "header2" };
			header.Items.Add (new CollectionItem () { Label = "12", Image = defaultImage });
			header.Items.Add (new CollectionItem () { Label = "22", Image = defaultImage });
			header.Items.Add (new CollectionItem () { Label = "33", Image = defaultImage });
			items.Add (header);


			RefreshData ();
		}

		void RefreshData ()
		{
			var filteredItems = items;

			if (!string.IsNullOrEmpty (filterEntry.StringValue)) {
				filteredItems = items.Where (h => h.Label.Contains (filterEntry.StringValue)).ToList ();
			}

			toolboxWidget.SetData (filteredItems);
		}


		#region Toolbar event handlers

		void ToggleCompactMode (object sender, EventArgs e)
		{
			toolboxWidget.CompactMode ();
			//this.toolboxWidget.IsListMode = !compactModeToggleButton.Active;
			//MonoDevelop.Core.PropertyService.Set ("ToolboxIsInCompactMode", compactModeToggleButton.Active);

			//if (compactModeToggleButton.Active) {
			//	compactModeToggleButton.Accessible.SetLabel (GettextCatalog.GetString ("Full Layout"));
			//	compactModeToggleButton.Accessible.Description = GettextCatalog.GetString ("Toggle for toolbox to use full layout");
			//} else {
			//	compactModeToggleButton.Accessible.SetLabel (GettextCatalog.GetString ("Compact Layout"));
			//	compactModeToggleButton.Accessible.Description = GettextCatalog.GetString ("Toggle for toolbox to use compact layout");
			//}
		}

		void toggleCategorisation (object sender, EventArgs e)
		{
			toolboxWidget.ImageMode ();
			//this.toolboxWidget.ShowCategories = catToggleButton.Active;
			//if (catToggleButton.Active) {
			//	catToggleButton.Accessible.SetLabel (GettextCatalog.GetString ("Hide Categories"));
			//	catToggleButton.Accessible.Description = GettextCatalog.GetString ("Toggle to hide toolbox categories");
			//} else {
			//	catToggleButton.Accessible.SetLabel (GettextCatalog.GetString ("Show Categories"));
			//	catToggleButton.Accessible.Description = GettextCatalog.GetString ("Toggle to show toolbox categories");
			//}
		}
		
		void filterTextChanged (object sender, EventArgs e)
		{
			refilter ();
		}

		void refilter ()
		{
			//foreach (ToolboxWidgetCategory cat in toolboxWidget.Categories) {
			//	bool hasVisibleChild = false;
			//	foreach (ToolboxWidgetItem child in cat.Items) {
			//		child.IsVisible = ((ItemToolboxNode)child.Tag).Filter (filterEntry.StringValue);
			//		hasVisibleChild |= child.IsVisible;
			//	}
			//	cat.IsVisible = hasVisibleChild;
			//}
			//toolboxWidget.QueueDraw ();
			//toolboxWidget.QueueResize ();
		}
		
		async void toolboxAddButton_Clicked (object sender, EventArgs e)
		{
			await toolboxService.AddUserItems ();
		}
		
		void ShowPopup (Gdk.EventButton evt)
		{
			if (!AllowEditingComponents)
				return;
			CommandEntrySet eset = IdeApp.CommandService.CreateCommandEntrySet ("/MonoDevelop/DesignerSupport/ToolboxItemContextMenu");
			if (evt != null) {
				//IdeApp.CommandService.ShowContextMenu (toolboxWidget, evt, eset, this);
			} else {
				//IdeApp.CommandService.ShowContextMenu (toolboxWidget, (int) Frame.Left, (int)Frame.Top, eset, this);
			}
		}

		[CommandHandler (MonoDevelop.Ide.Commands.EditCommands.Delete)]
		internal void OnDeleteItem ()
		{
			if (MessageService.Confirm (GettextCatalog.GetString ("Are you sure you want to remove the selected Item?"), AlertButton.Delete))
				toolboxService.RemoveUserItem (selectedNode);
		}

		[CommandUpdateHandler (MonoDevelop.Ide.Commands.EditCommands.Delete)]
		internal void OnUpdateDeleteItem (CommandInfo info)
		{
			// Hack manually filter out gtk# widgets & container since they cannot be re added
			// because they're missing the toolbox attributes.
			info.Enabled = selectedNode != null
				&& (selectedNode.ItemDomain != GtkWidgetDomain
				    || (selectedNode.Category != "Widgets" && selectedNode.Category != "Container"));
		}
		
		static readonly string GtkWidgetDomain = GettextCatalog.GetString ("GTK# Widgets");

		#endregion

		#region GUI population

	

		Dictionary<string, ToolboxWidgetCategory> categories = new Dictionary<string, ToolboxWidgetCategory> ();
		void AddItems (IEnumerable<ItemToolboxNode> nodes)
		{
			foreach (var itbn in nodes) {
				var newItem = new ToolboxWidgetItem (itbn);


				if (!categories.ContainsKey (itbn.Category)) {
					var cat = new ToolboxWidgetCategory (itbn.Category);
					int prio;
					if (!categoryPriorities.TryGetValue (itbn.Category, out prio))
						prio = -1;
					cat.Priority = prio;
					categories[itbn.Category] = cat;
				}
				if (newItem.Text != null)
					categories[itbn.Category].Add (newItem);
			}
		}


		public void Refresh ()
		{
			// GUI assert here is to catch Bug 434065 - Exception while going to the editor
			//Runtime.AssertMainThread ();
			
			if (toolboxService.Initializing) {
				toolboxWidget.CustomMessage = GettextCatalog.GetString ("Initializing...");
				return;
			}
			
			ConfigureToolbar ();
			
			toolboxWidget.CustomMessage = null;
			
			categories.Clear ();

			//Data.Clear ();

			AddItems (toolboxService.GetCurrentToolboxItems ());
			RefreshData ();

			//////Drag.SourceUnset (toolboxWidget);
			//toolboxWidget.ClearCategories ();

			//var cats = categories.Values.ToList ();
			//cats.Sort ((a,b) => a.Priority != b.Priority ? a.Priority.CompareTo (b.Priority) : a.Text.CompareTo (b.Text));
			//cats.Reverse ();
			//foreach (ToolboxWidgetCategory category in cats) {
			//	category.IsExpanded = true;
			//	toolboxWidget.AddCategory (category);
			//}
			//toolboxWidget.QueueResize ();
			//Gtk.TargetEntry[] targetTable = toolboxService.GetCurrentDragTargetTable ();
			//if (targetTable != null)
			//	//Drag.SourceSet (toolboxWidget, Gdk.ModifierType.Button1Mask, targetTable, Gdk.DragAction.Copy | Gdk.DragAction.Move);
			//compactModeToggleButton.Hidden = !toolboxWidget.CanIconizeToolboxCategories;
			//refilter ();
		}
		
		void ConfigureToolbar ()
		{
			// Default configuration
			categoryPriorities.Clear ();
			toolboxAddButton.Visible = true;
			
			toolboxService.Customize (container, this);
		}

		protected override void Dispose (bool disposing)
		{
			if (fontChanger != null) {
				fontChanger.Dispose ();
				fontChanger = null;
			}
			base.Dispose (disposing);
		}

		#endregion
		
		#region IPropertyPadProvider
		
		object IPropertyPadProvider.GetActiveComponent ()
		{
			return selectedNode;
		}

		object IPropertyPadProvider.GetProvider ()
		{
			return selectedNode;
		}

		void IPropertyPadProvider.OnEndEditing (object obj)
		{
		}

		void IPropertyPadProvider.OnChanged (object obj)
		{
		}
		
		#endregion

		#region IToolboxConfiguration implementation
		public void SetCategoryPriority (string category, int priority)
		{
			categoryPriorities[category] = priority;
		}

		public bool AllowEditingComponents {
			get {
				return toolboxAddButton.Visible;
			}
			set {
				toolboxAddButton.Visible = value;
			}
		}
		#endregion
	}
}
