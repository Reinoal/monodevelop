//
// Styles.cs
//
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
//
// Copyright (c) 2012 Xamarin Inc
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using MonoDevelop.Components;
using Xwt.Drawing;
using System.Security.Policy;

#if MAC
using AppKit;
#endif

namespace MonoDevelop.Ide.Gui
{
	public static class Styles
	{
		public static event EventHandler Changed;

		public static Color BackgroundColor { get; internal set; }        // must be the bg color from Gtkrc
		public static Color BaseBackgroundColor { get; internal set; }    // must be the base color from Gtkrc
		public static Color BaseForegroundColor { get; internal set; }    // must be the text color from Gtkrc
		public static Color BaseSelectionBackgroundColor { get; internal set; }
		public static Color BaseSelectionTextColor { get; internal set; }
		public static Color BaseIconColor { get; internal set; }
		
		public static Pango.FontDescription DefaultFont { get; internal set; }
		public static string DefaultFontName { get; internal set; }
		public static double FontScale12 = 1; // 12pt
		public static double FontScale11 = 0.917; // 11pt = default font size * 0.917

		public static Color ThinSplitterColor { get; internal set; }
		public static Color SeparatorColor { get; internal set; }
		public static Color PrimaryBackgroundColor { get; internal set; }
		public static Color SecondaryBackgroundLighterColor { get; internal set; }
		public static Color SecondaryBackgroundDarkerColor { get; internal set; }
		public static Color DimTextColor { get; internal set; }
		public static Color StatusInformationBackgroundColor { get; internal set; }
		public static Color StatusInformationTextColor { get; internal set; }
		public static Color StatusWarningBackgroundColor { get; internal set; }
		public static Color StatusWarningTextColor { get; internal set; }
		public static Color StatusErrorBackgroundColor { get; internal set; }
		public static Color StatusErrorTextColor { get; internal set; }

		// Document tab bar

		public static Color TabBarBackgroundColor { get; internal set; }
		public static Color TabBarActiveTextColor { get; internal set; }
		public static Color TabBarNotifyTextColor { get; internal set; }

		public static Color TabBarActiveGradientStartColor { get; internal set; }
		public static Color TabBarActiveGradientEndColor { get; internal set; }
		public static Color TabBarGradientStartColor { get; internal set; }
		public static Color TabBarGradientEndColor { get; internal set; }
		public static Color TabBarGradientShadowColor { get; internal set; }
		public static Color TabBarGlowGradientStartColor { get; internal set; }
		public static Color TabBarGlowGradientEndColor { get; internal set; }
		public static Color TabBarHoverActiveTextColor { get; internal set; }
		public static Color TabBarInactiveTextColor { get; internal set; }
		public static Color TabBarHoverInactiveTextColor { get; internal set; }
		public static Color TabBarInnerBorderColor { get; internal set; }
		public static Color TabBarInactiveGradientStartColor { get; internal set; }
		public static Color TabBarInactiveGradientEndColor { get; internal set; }

		public static Color BreadcrumbGradientStartColor { get; internal set; }
		public static Color BreadcrumbBackgroundColor { get; internal set; }
		public static Color BreadcrumbGradientEndColor { get; internal set; }
		public static Color BreadcrumbBorderColor { get; internal set; }
		public static Color BreadcrumbInnerBorderColor { get; internal set; }
		public static Color BreadcrumbTextColor { get; internal set; }
		public static Color BreadcrumbButtonBorderColor { get; internal set; }
		public static Color BreadcrumbButtonFillColor { get; internal set; }
		public static Color BreadcrumbBottomBorderColor { get; internal set; }

		public static readonly bool BreadcrumbInvertedIcons = false;
		public static readonly bool BreadcrumbGreyscaleIcons = false;

		// Document Subview Tabs

		public static Color SubTabBarBackgroundGradientTopColor { get; internal set; }
		public static Color SubTabBarBackgroundGradientStartColor { get; internal set; }
		public static Color SubTabBarBackgroundGradientEndColor { get; internal set; }
		public static Color SubTabBarTextColor { get; internal set; }
		public static Color SubTabBarActiveGradientStartColor { get; internal set; }
		public static Color SubTabBarActiveGradientTopColor { get; internal set; }
		public static Color SubTabBarActiveGradientEndColor { get; internal set; }
		public static Color SubTabBarActiveTextColor { get; internal set; }
		public static Color SubTabBarHoverGradientStartColor { get; internal set; }
		public static Color SubTabBarHoverGradientEndColor { get; internal set; }
		public static Color SubTabBarSeparatorColor { get; internal set; }

		// Dock pads

		public static Color DockTabBarGradientTop { get; internal set; }
		public static Color DockTabBarGradientStart { get; internal set; }
		public static Color DockTabBarGradientEnd { get; internal set; }
		public static Color DockTabBarShadowGradientStart { get; internal set; }
		public static Color DockTabBarShadowGradientEnd { get; internal set; }

		public static Color PadBackground { get; internal set; }
		public static Color InactivePadBackground { get; internal set; }
		public static Color PadLabelColor { get; internal set; }
		public static Color InactivePadLabelColor { get; internal set; }
		public static Color DockFrameBackground { get; internal set; }
		public static Color DockSeparatorColor { get; internal set; }
		public static Color DockBarBackground { get; internal set; }
		public static Color DockBarPrelightColor { get; internal set; }
		public static Color DockBarLabelColor { get; internal set; }

		public static Color BrowserPadBackground { get; internal set; }
		public static Color InactiveBrowserPadBackground { get; internal set; }

		public static Color PadCategoryBackgroundColor { get; internal set; }
		public static Color PadCategoryBorderColor { get; internal set; }
		public static Color PadCategoryLabelColor { get; internal set; }

		public static Color PropertyPadLabelBackgroundColor { get; internal set; }
		public static Color PropertyPadDividerColor { get; internal set; }

		// Status area

		public static Color WidgetBorderColor { get; internal set; }

		public static Color StatusBarBorderColor { get; internal set; }

		public static Color StatusBarFill1Color { get; internal set; }
		public static Color StatusBarFill2Color { get; internal set; }
		public static Color StatusBarFill3Color { get; internal set; }
		public static Color StatusBarFill4Color { get; internal set; }

		public static Color StatusBarErrorColor { get; internal set; }

		public static Color StatusBarInnerColor { get; internal set; }
		public static Color StatusBarShadowColor1 { get; internal set; }
		public static Color StatusBarShadowColor2 { get; internal set; }
		public static Color StatusBarTextColor { get; internal set; }
		public static Color StatusBarProgressBackgroundColor { get; internal set; }
		public static Color StatusBarProgressOutlineColor { get; internal set; }

		public static readonly Pango.FontDescription StatusFont = Pango.FontDescription.FromString ("Normal");

		public static int StatusFontPixelHeight { get { return (int)(11 * PixelScale); } }
		public static int ProgressBarHeight { get { return (int)(18 * PixelScale); } }
		public static int ProgressBarInnerPadding { get { return (int)(4 * PixelScale); } }
		public static int ProgressBarOuterPadding { get { return (int)(4 * PixelScale); } }

		static double? pixelScale = null;

		static double PixelScale {
			get {
				if (!pixelScale.HasValue)
					pixelScale = GtkWorkarounds.GetPixelScale ();
				return (double)pixelScale;
			}
		}

		// Toolbar

		public static Color ToolbarBottomBorderColor { get; internal set; }

		// Code Completion

		public static readonly int TooltipInfoSpacing;

		// Popover Windows

		public static class PopoverWindow
		{
			public static readonly int PagerTriangleSize = 6;
			public static readonly int PagerHeight = 16;
			public static readonly double DefaultFontScale = 0.917; // 12pt default font size * 0.917 = 11pt

			public static Color DefaultBackgroundColor { get; internal set; }
			public static Color ErrorBackgroundColor { get; internal set; }
			public static Color WarningBackgroundColor { get; internal set; }
			public static Color InformationBackgroundColor { get; internal set; }

			public static Color DefaultTextColor { get; internal set; }
			public static Color ErrorTextColor { get; internal set; }
			public static Color WarningTextColor { get; internal set; }
			public static Color InformationTextColor { get; internal set; }

			public static Color ShadowColor { get; internal set; }

			public static class ParamaterWindows
			{
				public static Color GradientStartColor { get; internal set; }
				public static Color GradientEndColor { get; internal set; }
			}
		}

		// Code Completion

		public static class CodeCompletion
		{
			public static Color BackgroundColor { get; internal set; }
			public static Color TextColor { get; internal set; }
			public static Color HighlightColor { get; internal set; }
			public static Color SelectionBackgroundColor { get; internal set; }
			public static Color SelectionBackgroundInactiveColor { get; internal set; }
			public static Color SelectionTextColor { get; internal set; }
			public static Color SelectionHighlightColor { get; internal set; }
		}

		// Global Search

		public static class GlobalSearch
		{
			public static Color HeaderTextColor { get; internal set; }
			public static Color SeparatorLineColor { get; internal set; }
			public static Color HeaderBackgroundColor { get; internal set; }
			public static Color BackgroundColor { get; internal set; }
			public static Color SelectionBackgroundColor { get; internal set; }
			public static Color ResultTextColor { get; internal set; }
			public static Color ResultDescriptionTextColor { get; internal set; }
		}

		// New Project Dialog

		public static class NewProjectDialog
		{
			public static Color BannerBackgroundColor { get; internal set; }
			public static Color BannerLineColor { get; internal set; }
			public static Color BannerForegroundColor { get; internal set; }
			public static Color CategoriesBackgroundColor { get; internal set; }
			public static Color TemplateListBackgroundColor { get; internal set; }
			public static Color TemplateBackgroundColor { get; internal set; }
			public static Color TemplateSectionSeparatorColor { get; internal set; }
			public static Color TemplateLanguageButtonBackground { get; internal set; }
			public static Color TemplateLanguageButtonTriangle { get; internal set; }
			public static Color ProjectConfigurationLeftHandBackgroundColor { get; internal set; }
			public static Color ProjectConfigurationRightHandBackgroundColor { get; internal set; }
			public static Color ProjectConfigurationPreviewLabelColor { get; internal set; }
			public static Color ProjectConfigurationSeparatorColor { get; internal set; }
		}

		// Editor

		public static class Editor
		{
			public static Color SmartTagMarkerColorLight { get; internal set; }
			public static Color SmartTagMarkerColorDark { get; internal set; }
		}

		// Helper methods

		internal static Color Shift (Color color, double factor)
		{
			return new Color (color.Red * factor, color.Green * factor, color.Blue * factor, color.Alpha);
		}

		internal static Color MidColor (double factor)
		{
			return BaseBackgroundColor.BlendWith (BaseForegroundColor, factor);
		}

		internal static Color ReduceLight (Color color, double factor)
		{
			color.Light *= factor;
			return color;
		}

		internal static Color IncreaseLight (Color color, double factor)
		{
			color.Light += (1 - color.Light) * factor;
			return color;
		}

		public static string ColorGetHex (Color color, bool withAlpha = false)
		{
			if (withAlpha) {
				return String.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", (byte)(color.Red * 255), (byte)(color.Green * 255),
				                     (byte)(color.Blue * 255), (byte)(color.Alpha * 255));
			} else {
				return String.Format("#{0:x2}{1:x2}{2:x2}", (byte)(color.Red * 255), (byte)(color.Green * 255),
				                     (byte)(color.Blue * 255));
			}
		}

		static Styles ()
		{
			if (Core.Platform.IsWindows)
				TooltipInfoSpacing = 0;
			else
				TooltipInfoSpacing = -5;
		}

		internal static void LoadStyle ()
		{
			var defaultStyle = Gtk.Rc.GetStyle (IdeApp.Workbench.RootWindow);

			BackgroundColor = defaultStyle.Background (Gtk.StateType.Normal).ToXwtColor ();	// must be the bg color from Gtkrc
			BaseBackgroundColor = defaultStyle.Base (Gtk.StateType.Normal).ToXwtColor ();	// must be the base color from Gtkrc
			BaseForegroundColor = defaultStyle.Foreground (Gtk.StateType.Normal).ToXwtColor ();	// must be the text color from Gtkrc
			BaseSelectionBackgroundColor = defaultStyle.Base (Gtk.StateType.Selected).ToXwtColor ();
			BaseSelectionTextColor = defaultStyle.Text (Gtk.StateType.Selected).ToXwtColor ();

			DefaultFont = defaultStyle.FontDescription.Copy ();
			DefaultFontName = DefaultFont.ToString ();

			if (IdeApp.Preferences.UserInterfaceSkin == Skin.Light)
				LoadLightStyle ();
			else
				LoadDarkStyle ();

			// Shared colors

			DockBarLabelColor = BaseIconColor;
			PadCategoryBorderColor = SeparatorColor;
			PadCategoryLabelColor = BaseForegroundColor;
			PadCategoryBackgroundColor = SecondaryBackgroundLighterColor;

			CodeCompletion.SelectionBackgroundColor = BaseSelectionBackgroundColor;
			CodeCompletion.SelectionTextColor = BaseSelectionTextColor;

			GlobalSearch.BackgroundColor = PrimaryBackgroundColor;
			GlobalSearch.HeaderBackgroundColor = SecondaryBackgroundLighterColor;
			GlobalSearch.HeaderTextColor = DimTextColor;
			GlobalSearch.SeparatorLineColor = SeparatorColor;
			GlobalSearch.SelectionBackgroundColor = BaseSelectionBackgroundColor;
			GlobalSearch.ResultTextColor = BaseForegroundColor;
			GlobalSearch.ResultDescriptionTextColor = DimTextColor;

			NewProjectDialog.TemplateBackgroundColor = PrimaryBackgroundColor;
			NewProjectDialog.TemplateLanguageButtonTriangle = BaseIconColor;
			NewProjectDialog.ProjectConfigurationPreviewLabelColor = BaseForegroundColor;
			NewProjectDialog.CategoriesBackgroundColor = SecondaryBackgroundDarkerColor;
			NewProjectDialog.ProjectConfigurationLeftHandBackgroundColor = SecondaryBackgroundDarkerColor;
			NewProjectDialog.ProjectConfigurationRightHandBackgroundColor = PrimaryBackgroundColor;

			PopoverWindow.InformationBackgroundColor = StatusInformationBackgroundColor;
			PopoverWindow.InformationTextColor = StatusInformationTextColor;
			PopoverWindow.WarningBackgroundColor = StatusWarningBackgroundColor;
			PopoverWindow.WarningTextColor = StatusWarningTextColor;
			PopoverWindow.ErrorBackgroundColor = StatusErrorBackgroundColor;
			PopoverWindow.ErrorTextColor = StatusErrorTextColor;

			PropertyPadLabelBackgroundColor = PrimaryBackgroundColor;

			Editor.SmartTagMarkerColorLight = Color.FromName ("#ff70fe").WithAlpha (.5);
			Editor.SmartTagMarkerColorDark = Color.FromName ("#ffffff").WithAlpha (.5);

			if (Changed != null)
				Changed (null, EventArgs.Empty);
		}

		internal static void LoadLightStyle ()
		{
			BaseIconColor = Color.FromName ("#575757");
			ThinSplitterColor = Color.FromName ("#dadada");
			SeparatorColor = Color.FromName ("#f2f2f4");
			PrimaryBackgroundColor = Color.FromName ("#ffffff");
			SecondaryBackgroundDarkerColor = Color.FromName ("#e7eaee");
			SecondaryBackgroundLighterColor = Color.FromName ("#f9f9fb");
			DimTextColor = Color.FromName ("#aaaaaa");

			// these colors need to match colors from status icons
			StatusInformationBackgroundColor = Color.FromName ("#87b6f0");
			StatusInformationTextColor = Color.FromName ("#ffffff");
			StatusWarningBackgroundColor = Color.FromName ("#f1c40f");
			StatusWarningTextColor = Color.FromName ("#ffffff");
			StatusErrorBackgroundColor = Color.FromName ("#f56d4f");
			StatusErrorTextColor = Color.FromName ("#ffffff");

			// Document tabs
			// TODO: Will be new with roslyn-dark-skin-tabs

			TabBarBackgroundColor = Color.FromName ("#bfbfbf"); // TODO
			TabBarActiveTextColor = Color.FromName ("#505050"); // TODO
			TabBarNotifyTextColor = Color.FromName ("#0000ff"); // TODO
			TabBarActiveGradientStartColor = Shift (TabBarBackgroundColor, 0.92); // TODO
			TabBarActiveGradientEndColor = TabBarBackgroundColor; // TODO
			TabBarGradientStartColor = Shift (TabBarBackgroundColor, 1.02); // TODO
			TabBarGradientEndColor = TabBarBackgroundColor; // TODO
			TabBarGradientShadowColor = Shift (TabBarBackgroundColor, 0.8); // TODO
			TabBarGlowGradientStartColor = Color.FromName ("#ffffff").WithAlpha (.4); // TODO
			TabBarGlowGradientEndColor = Color.FromName ("#ffffff").WithAlpha (0); // TODO
			TabBarHoverActiveTextColor = TabBarActiveTextColor; // TODO
			TabBarInactiveTextColor = Color.FromName ("#888888");
			TabBarHoverInactiveTextColor = Color.FromName ("#000000"); // TODO
			TabBarInnerBorderColor = Color.FromName ("#ffffff").WithAlpha (.5); // TODO
			TabBarInactiveGradientStartColor = Color.FromName ("#f4f4f4"); // TODO
			TabBarInactiveGradientEndColor = Color.FromName ("#cecece"); // TODO

			// Breadcrumb
			// TODO: Will be new with roslyn-dark-skin-tabs

			BreadcrumbGradientStartColor = Color.FromName ("#ffffff"); // TODO
			BreadcrumbBackgroundColor = Shift (BreadcrumbGradientStartColor, .95); // TODO
			BreadcrumbGradientEndColor = Shift (BreadcrumbGradientStartColor, 0.9); // TODO
			BreadcrumbBorderColor = Shift (BreadcrumbBackgroundColor, 0.6); // TODO
			BreadcrumbInnerBorderColor = BaseBackgroundColor.WithAlpha (0.1d); // TODO
			BreadcrumbTextColor = Shift (BaseForegroundColor, 0.8); // TODO
			BreadcrumbButtonBorderColor = Shift (BaseBackgroundColor, 0.8); // TODO
			BreadcrumbButtonFillColor = BaseBackgroundColor.WithAlpha (0.1d); // TODO
			BreadcrumbBottomBorderColor = Shift (BreadcrumbBackgroundColor, 0.7d); // TODO

			// Document Subview Tabs
			// TODO: Will be new with roslyn-dark-skin-tabs

			SubTabBarBackgroundGradientTopColor = Color.FromName ("#ffffff"); // TODO
			SubTabBarBackgroundGradientStartColor = Color.FromName ("#f1f1f1"); // TODO
			SubTabBarBackgroundGradientEndColor = SubTabBarBackgroundGradientStartColor; // TODO
			SubTabBarTextColor = BaseForegroundColor; // TODO
			SubTabBarActiveGradientTopColor = Color.FromName ("#ffffff").WithAlpha (.05); // TODO
			SubTabBarActiveGradientStartColor = Color.FromName ("#5c5d5e"); // TODO
			SubTabBarActiveGradientEndColor = Color.FromName ("#86888a"); // TODO
			SubTabBarActiveTextColor = Color.FromName ("#ffffff"); // TODO
			SubTabBarHoverGradientStartColor = Color.FromName ("#5c5d5e").WithAlpha (.2); // TODO
			SubTabBarHoverGradientEndColor = Color.FromName ("#868889").WithAlpha (.2); // TODO
			SubTabBarSeparatorColor = Color.FromName ("#ababab"); // TODO

			// Dock pads
			// FIXME: Seems to be unused, kill

			// DockTabBarGradientTop = Color.FromName ("#ff0000"); // f8f8f8 // UNUSED
			// DockTabBarGradientStart = Color.FromName ("#00ff00"); // f2f2f2 // UNUSED
			// DockTabBarGradientEnd = Color.FromName ("#0000ff"); // e6e6e6 // UNUSED
			// DockTabBarShadowGradientStart = Color.FromName ("#00ffff"); // 9a9a9a // UNUSED
			// DockTabBarShadowGradientEnd = Color.FromName ("#ffff00"); // .WithAlpha (0); // UNUSED
			// WidgetBorderColor = Color.FromName ("#ff0000"); // 8c8c8c // UNUSED (used for custom drawn `SearchEntry` but it isn’t used anymore, so its deprecated)

			PadBackground = Color.FromName ("#fafafa");
			InactivePadBackground = Color.FromName ("#e8e8e8");
			PadLabelColor = Color.FromName ("#ff0000"); // 57656b // TODO: Doesn't work currently
			InactivePadLabelColor = Color.FromName ("#979797");
			DockFrameBackground = Color.FromName ("#bababa");
			DockSeparatorColor = DockFrameBackground;
			DockBarBackground = Color.FromName ("#dddddd");
			DockBarPrelightColor = Color.FromName ("#eeeeee");
			BrowserPadBackground = Color.FromName ("#f0f1f3");
			InactiveBrowserPadBackground = Color.FromName ("#f0f0f0");
			PropertyPadDividerColor = Color.FromName ("#efefef");

			// Status area (GTK)

			StatusBarBorderColor = Color.FromName ("#919191"); // TODO
			StatusBarFill1Color = Color.FromName ("#f5fafc"); // TODO
			StatusBarFill2Color = Color.FromName ("#e9f1f3"); // TODO
			StatusBarFill3Color = Color.FromName ("#d8e7ea"); // TODO
			StatusBarFill4Color = Color.FromName ("#d1e3e7"); // TODO
			StatusBarErrorColor = Color.FromName ("#FF6363"); // TODO
			StatusBarInnerColor = Color.FromName ("#000000").WithAlpha (.08); // TODO
			StatusBarShadowColor1 = Color.FromName ("#000000").WithAlpha (.06); // TODO
			StatusBarShadowColor2 = Color.FromName ("#000000").WithAlpha (.02); // TODO
			StatusBarTextColor = BaseForegroundColor; // TODO
			StatusBarProgressBackgroundColor = Color.FromName ("#000000").WithAlpha (.1); // TODO
			StatusBarProgressOutlineColor = Color.FromName ("#000000").WithAlpha (.1); // TODO

			// Toolbar

			ToolbarBottomBorderColor = Color.FromName ("#afafaf");

			// Popover Window

			PopoverWindow.DefaultBackgroundColor = Color.FromName ("#f2f2f2");
			PopoverWindow.DefaultTextColor = Color.FromName ("#888888");
			PopoverWindow.ShadowColor = Color.FromName ("#000000").WithAlpha (.05);

			PopoverWindow.ParamaterWindows.GradientStartColor = Color.FromName ("#fffee6");
			PopoverWindow.ParamaterWindows.GradientEndColor = Color.FromName ("#fffcd1");

			// Code Completion

			CodeCompletion.BackgroundColor = Color.FromName ("#eef1f2");
			CodeCompletion.TextColor = Color.FromName ("#646566");
			CodeCompletion.HighlightColor = Color.FromName ("#ba3373");
			CodeCompletion.SelectionBackgroundInactiveColor = Color.FromName ("#bbbbbb");
			CodeCompletion.SelectionHighlightColor = CodeCompletion.HighlightColor;

			// New Project Dialog

			NewProjectDialog.BannerBackgroundColor = Color.FromName ("#77828c");
			NewProjectDialog.BannerLineColor = Color.FromName ("#707a83");
			NewProjectDialog.BannerForegroundColor = Color.FromName ("#ffffff");
			NewProjectDialog.TemplateListBackgroundColor = Color.FromName ("#f9f9fa");
			NewProjectDialog.TemplateSectionSeparatorColor = Color.FromName ("#e2e2e2");
			NewProjectDialog.TemplateLanguageButtonBackground = Color.FromName ("#ffffff");
			NewProjectDialog.ProjectConfigurationSeparatorColor = Color.FromName ("#d2d5d9");
		}

		internal static void LoadDarkStyle ()
		{
			BaseIconColor = Color.FromName ("#575757");
			ThinSplitterColor = Color.FromName ("#ff0000"); // 353535
			SeparatorColor = Color.FromName ("#00ff00"); // f2f2f4
			PrimaryBackgroundColor = Color.FromName ("#0000ff"); // 444444
			SecondaryBackgroundDarkerColor = Color.FromName ("#ffff00"); // 555555
			SecondaryBackgroundLighterColor = Color.FromName ("#00ffff"); // 666666
			DimTextColor = Color.FromName ("#aaaaaa");

			// these colors need to match colors from status icons
			StatusInformationBackgroundColor = Color.FromName ("#87b6f0");
			StatusInformationTextColor = Color.FromName ("#ffffff");
			StatusWarningBackgroundColor = Color.FromName ("#f1c40f");
			StatusWarningTextColor = Color.FromName ("#ffffff");
			StatusErrorBackgroundColor = Color.FromName ("#f56d4f");
			StatusErrorTextColor = Color.FromName ("#ffffff");

			PadBackground = Color.FromName ("#5a5a5a");
			InactivePadBackground = ReduceLight (PadBackground, 0.9);
			PadLabelColor = Color.FromName ("#ff0000"); // 57656b // TODO: Doesn't work currently
			InactivePadLabelColor = Color.FromName ("#979797");
			DockFrameBackground = Color.FromName ("#222222");
			DockSeparatorColor = Color.FromName ("#222222");
			DockBarBackground = Color.FromName ("#4a4a4a");
			DockBarPrelightColor = Color.FromName ("#5a5a5a");
			BrowserPadBackground = Color.FromName ("#484b55");
			InactiveBrowserPadBackground = Color.FromName ("#141414");
			PropertyPadDividerColor = PadBackground;

			// Toolbar

			ToolbarBottomBorderColor = Color.FromName ("#00ff00"); // ffffff.WithAlpha (.2);

			// Popover window

			PopoverWindow.DefaultBackgroundColor = Color.FromName ("#5A5A5A");
			PopoverWindow.DefaultTextColor = Color.FromName ("#ffffff");
			PopoverWindow.ShadowColor = Color.FromName ("#000000").WithAlpha (0); // transparent since dark skin doesn't need shadows

			PopoverWindow.ParamaterWindows.GradientStartColor = Color.FromName ("#fffee6");
			PopoverWindow.ParamaterWindows.GradientEndColor = Color.FromName ("#fffcd1");

			// Code Completion

			CodeCompletion.BackgroundColor = Color.FromName ("#5b6365");
			CodeCompletion.TextColor = Color.FromName ("#c3c5c6");
			CodeCompletion.HighlightColor = Color.FromName ("#f9d33c");

			CodeCompletion.SelectionBackgroundInactiveColor = Color.FromName ("#bbbbbb");
			CodeCompletion.SelectionHighlightColor = CodeCompletion.HighlightColor;

			// New Project Dialog

			NewProjectDialog.BannerBackgroundColor = Color.FromName ("#77828c");
			NewProjectDialog.BannerLineColor = Color.FromName ("#707a83");
			NewProjectDialog.BannerForegroundColor = Color.FromName ("#ffffff");
			NewProjectDialog.TemplateListBackgroundColor = Color.FromName ("#5a5a5a");
			NewProjectDialog.TemplateSectionSeparatorColor = ThinSplitterColor;
			NewProjectDialog.TemplateLanguageButtonBackground = Color.FromName ("#616161");
			NewProjectDialog.ProjectConfigurationSeparatorColor = ThinSplitterColor;
		}
	}
}

