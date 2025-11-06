using cYo.Common.Win32;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Forms.Theme.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Dialogs
{
    public partial class PreferencesDialog
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PreferencesDialog));
            ListViewGroup listViewGroup1 = new ListViewGroup("Installed", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("To be removed (requires restart)", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("To be installed (requires restart)", HorizontalAlignment.Left);
            btOK = new Button();
            btCancel = new Button();
            imageList = new ImageList(components);
            btApply = new Button();
            toolTip = new ToolTip(components);
            txtCaptionFormat = new TextBox();
            pageBehavior = new Panel();
            pageReader = new Panel();
            groupHardwareAcceleration = new CollapsibleGroupBox();
            chkEnableHardwareFiltering = new CheckBox();
            chkEnableSoftwareFiltering = new CheckBox();
            chkEnableHardware = new CheckBox();
            chkEnableDisplayChangeAnimation = new CheckBox();
            grpMouse = new CollapsibleGroupBox();
            chkSmoothAutoScrolling = new CheckBox();
            lblFast = new Label();
            lblMouseWheel = new Label();
            chkEnableInertialMouseScrolling = new CheckBox();
            lblSlow = new Label();
            tbMouseWheel = new TrackBarLite();
            groupOverlays = new CollapsibleGroupBox();
            panelReaderOverlays = new Panel();
            labelVisiblePartOverlay = new Label();
            labelNavigationOverlay = new Label();
            labelStatusOverlay = new Label();
            labelPageOverlay = new Label();
            cbNavigationOverlayPosition = new ComboBox();
            labelNavigationOverlayPosition = new Label();
            chkShowPageNames = new CheckBox();
            tbOverlayScaling = new TrackBarLite();
            chkShowCurrentPageOverlay = new CheckBox();
            chkShowStatusOverlay = new CheckBox();
            chkShowVisiblePartOverlay = new CheckBox();
            chkShowNavigationOverlay = new CheckBox();
            labelOverlaySize = new Label();
            grpKeyboard = new CollapsibleGroupBox();
            btExportKeyboard = new Button();
            btImportKeyboard = new SplitButton();
            cmKeyboardLayout = new ContextMenuStrip(components);
            miDefaultKeyboardLayout = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            keyboardShortcutEditor = new KeyboardShortcutEditor();
            grpDisplay = new CollapsibleGroupBox();
            tbGamma = new TrackBarLite();
            labelGamma = new Label();
            chkAnamorphicScaling = new CheckBox();
            chkHighQualityDisplay = new CheckBox();
            labelSharpening = new Label();
            tbSharpening = new TrackBarLite();
            btResetColor = new Button();
            chkAutoContrast = new CheckBox();
            labelSaturation = new Label();
            tbSaturation = new TrackBarLite();
            labelBrightness = new Label();
            tbBrightness = new TrackBarLite();
            tbContrast = new TrackBarLite();
            labelContrast = new Label();
            pageAdvanced = new Panel();
            grpWirelessSetup = new CollapsibleGroupBox();
            btTestWifi = new Button();
            lblWifiStatus = new Label();
            lblWifiAddresses = new Label();
            txWifiAddresses = new TextBox();
            grpIntegration = new CollapsibleGroupBox();
            btAssociateExtensions = new Button();
            labelCheckedFormats = new Label();
            chkOverwriteAssociations = new CheckBox();
            lbFormats = new CheckedListBox();
            groupMessagesAndSocial = new CollapsibleGroupBox();
            btResetMessages = new Button();
            labelReshowHidden = new Label();
            groupMemory = new CollapsibleGroupBox();
            grpMaximumMemoryUsage = new GroupBox();
            lblMaximumMemoryUsageValue = new Label();
            tbMaximumMemoryUsage = new TrackBarLite();
            lblMaximumMemoryUsage = new Label();
            grpMemoryCache = new GroupBox();
            lblPageMemCacheUsage = new Label();
            labelMemThumbSize = new Label();
            lblThumbMemCacheUsage = new Label();
            numMemPageCount = new NumericUpDown();
            labelMemPageCount = new Label();
            chkMemPageOptimized = new CheckBox();
            chkMemThumbOptimized = new CheckBox();
            numMemThumbSize = new NumericUpDown();
            grpDiskCache = new GroupBox();
            chkEnableInternetCache = new CheckBox();
            lblInternetCacheUsage = new Label();
            btClearPageCache = new Button();
            numPageCacheSize = new NumericUpDown();
            numInternetCacheSize = new NumericUpDown();
            btClearThumbnailCache = new Button();
            btClearInternetCache = new Button();
            chkEnablePageCache = new CheckBox();
            lblPageCacheUsage = new Label();
            numThumbnailCacheSize = new NumericUpDown();
            chkEnableThumbnailCache = new CheckBox();
            lblThumbCacheUsage = new Label();
            grpDatabaseBackup = new CollapsibleGroupBox();
            btRestoreDatabase = new Button();
            btBackupDatabase = new Button();
            groupOtherComics = new CollapsibleGroupBox();
            chkUpdateComicFiles = new CheckBox();
            labelExcludeCover = new Label();
            chkAutoUpdateComicFiles = new CheckBox();
            txCoverFilter = new TextBox();
            grpLanguages = new CollapsibleGroupBox();
            btTranslate = new Button();
            labelLanguage = new Label();
            lbLanguages = new ListBox();
            pageLibrary = new Panel();
            grpVirtualTags = new CollapsibleGroupBox();
            btnVTagsHelp = new Button();
            grpVtagConfig = new GroupBox();
            lblCaptionFormat = new Label();
            btInsertValue = new Button();
            lblVirtualTagDescription = new Label();
            lblVirtualTagName = new Label();
            txtVirtualTagDescription = new TextBox();
            txtVirtualTagName = new TextBox();
            chkVirtualTagEnable = new CheckBox();
            lblCaptionText = new Label();
            lblCaptionSuffix = new Label();
            rtfVirtualTagCaption = new RichTextBox();
            lblFieldConfig = new Label();
            txtCaptionPrefix = new TextBox();
            lblCaptionPrefix = new Label();
            btnCaptionInsert = new Button();
            txtCaptionSuffix = new TextBox();
            lblVirtualTags = new Label();
            cbVirtualTags = new ComboBox();
            grpServerSettings = new CollapsibleGroupBox();
            txPrivateListingPassword = new PasswordTextBox();
            labelPrivateListPassword = new Label();
            labelPublicServerAddress = new Label();
            txPublicServerAddress = new TextBox();
            grpSharing = new CollapsibleGroupBox();
            chkAutoConnectShares = new CheckBox();
            btRemoveShare = new Button();
            btAddShare = new Button();
            tabShares = new TabControl();
            chkLookForShared = new CheckBox();
            groupLibraryDisplay = new CollapsibleGroupBox();
            chkLibraryGaugesTotal = new CheckBox();
            chkLibraryGaugesUnread = new CheckBox();
            chkLibraryGaugesNumeric = new CheckBox();
            chkLibraryGaugesNew = new CheckBox();
            chkLibraryGauges = new CheckBox();
            grpScanning = new CollapsibleGroupBox();
            chkDontAddRemovedFiles = new CheckBox();
            chkAutoRemoveMissing = new CheckBox();
            lblScan = new Label();
            btScan = new Button();
            groupComicFolders = new CollapsibleGroupBox();
            btOpenFolder = new Button();
            btChangeFolder = new Button();
            lbPaths = new CheckedListBoxEx();
            labelWatchedFolders = new Label();
            btRemoveFolder = new Button();
            btAddFolder = new Button();
            memCacheUpate = new Timer(components);
            pageScripts = new Panel();
            grpScriptSettings = new CollapsibleGroupBox();
            btAddLibraryFolder = new Button();
            chkDisableScripting = new CheckBox();
            labelScriptPaths = new Label();
            txLibraries = new TextBox();
            grpScripts = new CollapsibleGroupBox();
            chkHideSampleScripts = new CheckBox();
            btConfigScript = new Button();
            lvScripts = new ListView();
            chScriptName = new ColumnHeader();
            chScriptPackage = new ColumnHeader();
            grpPackages = new CollapsibleGroupBox();
            btRemovePackage = new Button();
            btInstallPackage = new Button();
            lvPackages = new ListView();
            chPackageName = new ColumnHeader();
            chPackageAuthor = new ColumnHeader();
            chPackageDescription = new ColumnHeader();
            packageImageList = new ImageList(components);
            tabReader = new CheckBox();
            tabLibraries = new CheckBox();
            tabBehavior = new CheckBox();
            tabScripts = new CheckBox();
            tabAdvanced = new CheckBox();
            pageReader.SuspendLayout();
            groupHardwareAcceleration.SuspendLayout();
            grpMouse.SuspendLayout();
            groupOverlays.SuspendLayout();
            panelReaderOverlays.SuspendLayout();
            grpKeyboard.SuspendLayout();
            cmKeyboardLayout.SuspendLayout();
            grpDisplay.SuspendLayout();
            pageAdvanced.SuspendLayout();
            grpWirelessSetup.SuspendLayout();
            grpIntegration.SuspendLayout();
            groupMessagesAndSocial.SuspendLayout();
            groupMemory.SuspendLayout();
            grpMaximumMemoryUsage.SuspendLayout();
            grpMemoryCache.SuspendLayout();
            ((ISupportInitialize)numMemPageCount).BeginInit();
            ((ISupportInitialize)numMemThumbSize).BeginInit();
            grpDiskCache.SuspendLayout();
            ((ISupportInitialize)numPageCacheSize).BeginInit();
            ((ISupportInitialize)numInternetCacheSize).BeginInit();
            ((ISupportInitialize)numThumbnailCacheSize).BeginInit();
            grpDatabaseBackup.SuspendLayout();
            groupOtherComics.SuspendLayout();
            grpLanguages.SuspendLayout();
            pageLibrary.SuspendLayout();
            grpVirtualTags.SuspendLayout();
            grpVtagConfig.SuspendLayout();
            grpServerSettings.SuspendLayout();
            grpSharing.SuspendLayout();
            groupLibraryDisplay.SuspendLayout();
            grpScanning.SuspendLayout();
            groupComicFolders.SuspendLayout();
            pageScripts.SuspendLayout();
            grpScriptSettings.SuspendLayout();
            grpScripts.SuspendLayout();
            grpPackages.SuspendLayout();
            SuspendLayout();
            // 
            // btOK
            // 
            btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btOK.DialogResult = DialogResult.OK;
            btOK.FlatStyle = FlatStyle.System;
            btOK.Location = new System.Drawing.Point(351, 422);
            btOK.Name = "btOK";
            btOK.Size = new System.Drawing.Size(80, 24);
            btOK.TabIndex = 1;
            btOK.Text = "&OK";
            // 
            // btCancel
            // 
            btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.FlatStyle = FlatStyle.System;
            btCancel.Location = new System.Drawing.Point(437, 422);
            btCancel.Name = "btCancel";
            btCancel.Size = new System.Drawing.Size(80, 24);
            btCancel.TabIndex = 2;
            btCancel.Text = "&Cancel";
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new System.Drawing.Size(16, 16);
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btApply
            // 
            btApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btApply.FlatStyle = FlatStyle.System;
            btApply.Location = new System.Drawing.Point(523, 422);
            btApply.Name = "btApply";
            btApply.Size = new System.Drawing.Size(80, 24);
            btApply.TabIndex = 3;
            btApply.Text = "&Apply";
            btApply.Click += btApply_Click;
            // 
            // txtCaptionFormat
            // 
            txtCaptionFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCaptionFormat.BorderStyle = BorderStyle.FixedSingle;
            txtCaptionFormat.Location = new System.Drawing.Point(269, 167);
            txtCaptionFormat.Name = "txtCaptionFormat";
            txtCaptionFormat.Size = new System.Drawing.Size(50, 23);
            txtCaptionFormat.TabIndex = 27;
            toolTip.SetToolTip(txtCaptionFormat, resources.GetString("txtCaptionFormat.ToolTip"));
            txtCaptionFormat.WordWrap = false;
            // 
            // pageBehavior
            // 
            pageBehavior.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pageBehavior.AutoScroll = true;
            pageBehavior.BorderStyle = BorderStyle.FixedSingle;
            pageBehavior.Location = new System.Drawing.Point(84, 8);
            pageBehavior.Name = "pageBehavior";
            pageBehavior.Size = new System.Drawing.Size(517, 408);
            pageBehavior.TabIndex = 6;
            // 
            // pageReader
            // 
            pageReader.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pageReader.AutoScroll = true;
            pageReader.BorderStyle = BorderStyle.FixedSingle;
            pageReader.Controls.Add(groupHardwareAcceleration);
            pageReader.Controls.Add(grpMouse);
            pageReader.Controls.Add(groupOverlays);
            pageReader.Controls.Add(grpKeyboard);
            pageReader.Controls.Add(grpDisplay);
            pageReader.Location = new System.Drawing.Point(84, 8);
            pageReader.Name = "pageReader";
            pageReader.Size = new System.Drawing.Size(517, 408);
            pageReader.TabIndex = 8;
            // 
            // groupHardwareAcceleration
            // 
            groupHardwareAcceleration.Controls.Add(chkEnableHardwareFiltering);
            groupHardwareAcceleration.Controls.Add(chkEnableSoftwareFiltering);
            groupHardwareAcceleration.Controls.Add(chkEnableHardware);
            groupHardwareAcceleration.Controls.Add(chkEnableDisplayChangeAnimation);
            groupHardwareAcceleration.Dock = DockStyle.Top;
            groupHardwareAcceleration.Location = new System.Drawing.Point(0, 1180);
            groupHardwareAcceleration.Name = "groupHardwareAcceleration";
            groupHardwareAcceleration.Size = new System.Drawing.Size(498, 137);
            groupHardwareAcceleration.TabIndex = 3;
            groupHardwareAcceleration.Text = "Hardware Acceleration";
            // 
            // chkEnableHardwareFiltering
            // 
            chkEnableHardwareFiltering.AutoSize = true;
            chkEnableHardwareFiltering.Location = new System.Drawing.Point(33, 70);
            chkEnableHardwareFiltering.Name = "chkEnableHardwareFiltering";
            chkEnableHardwareFiltering.Size = new System.Drawing.Size(149, 19);
            chkEnableHardwareFiltering.TabIndex = 1;
            chkEnableHardwareFiltering.Text = "Enable Hardware Filters";
            chkEnableHardwareFiltering.UseVisualStyleBackColor = true;
            // 
            // chkEnableSoftwareFiltering
            // 
            chkEnableSoftwareFiltering.AutoSize = true;
            chkEnableSoftwareFiltering.Location = new System.Drawing.Point(33, 88);
            chkEnableSoftwareFiltering.Name = "chkEnableSoftwareFiltering";
            chkEnableSoftwareFiltering.Size = new System.Drawing.Size(144, 19);
            chkEnableSoftwareFiltering.TabIndex = 2;
            chkEnableSoftwareFiltering.Text = "Enable Software Filters";
            chkEnableSoftwareFiltering.UseVisualStyleBackColor = true;
            // 
            // chkEnableHardware
            // 
            chkEnableHardware.AutoSize = true;
            chkEnableHardware.Location = new System.Drawing.Point(12, 38);
            chkEnableHardware.Name = "chkEnableHardware";
            chkEnableHardware.Size = new System.Drawing.Size(184, 19);
            chkEnableHardware.TabIndex = 0;
            chkEnableHardware.Text = "Enable Hardware Acceleration";
            chkEnableHardware.UseVisualStyleBackColor = true;
            // 
            // chkEnableDisplayChangeAnimation
            // 
            chkEnableDisplayChangeAnimation.AutoSize = true;
            chkEnableDisplayChangeAnimation.Location = new System.Drawing.Point(33, 108);
            chkEnableDisplayChangeAnimation.Name = "chkEnableDisplayChangeAnimation";
            chkEnableDisplayChangeAnimation.Size = new System.Drawing.Size(251, 19);
            chkEnableDisplayChangeAnimation.TabIndex = 3;
            chkEnableDisplayChangeAnimation.Text = "Enable Animation of Page Display changes";
            chkEnableDisplayChangeAnimation.UseVisualStyleBackColor = true;
            // 
            // grpMouse
            // 
            grpMouse.Controls.Add(chkSmoothAutoScrolling);
            grpMouse.Controls.Add(lblFast);
            grpMouse.Controls.Add(lblMouseWheel);
            grpMouse.Controls.Add(chkEnableInertialMouseScrolling);
            grpMouse.Controls.Add(lblSlow);
            grpMouse.Controls.Add(tbMouseWheel);
            grpMouse.Dock = DockStyle.Top;
            grpMouse.Location = new System.Drawing.Point(0, 1046);
            grpMouse.Name = "grpMouse";
            grpMouse.Size = new System.Drawing.Size(498, 134);
            grpMouse.TabIndex = 5;
            grpMouse.Text = "Mouse & Scrolling";
            // 
            // chkSmoothAutoScrolling
            // 
            chkSmoothAutoScrolling.AutoSize = true;
            chkSmoothAutoScrolling.Location = new System.Drawing.Point(9, 39);
            chkSmoothAutoScrolling.Name = "chkSmoothAutoScrolling";
            chkSmoothAutoScrolling.Size = new System.Drawing.Size(146, 19);
            chkSmoothAutoScrolling.TabIndex = 0;
            chkSmoothAutoScrolling.Text = "Smooth Auto Scrolling";
            chkSmoothAutoScrolling.UseVisualStyleBackColor = true;
            // 
            // lblFast
            // 
            lblFast.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFast.Location = new System.Drawing.Point(426, 96);
            lblFast.Name = "lblFast";
            lblFast.Size = new System.Drawing.Size(56, 19);
            lblFast.TabIndex = 4;
            lblFast.Text = "fast";
            // 
            // lblMouseWheel
            // 
            lblMouseWheel.AutoSize = true;
            lblMouseWheel.Location = new System.Drawing.Point(9, 97);
            lblMouseWheel.Name = "lblMouseWheel";
            lblMouseWheel.Size = new System.Drawing.Size(130, 15);
            lblMouseWheel.TabIndex = 0;
            lblMouseWheel.Text = "Mouse Wheel scrolling:";
            // 
            // chkEnableInertialMouseScrolling
            // 
            chkEnableInertialMouseScrolling.AutoSize = true;
            chkEnableInertialMouseScrolling.Location = new System.Drawing.Point(9, 62);
            chkEnableInertialMouseScrolling.Name = "chkEnableInertialMouseScrolling";
            chkEnableInertialMouseScrolling.Size = new System.Drawing.Size(187, 19);
            chkEnableInertialMouseScrolling.TabIndex = 1;
            chkEnableInertialMouseScrolling.Text = "Enable Inertial Mouse scrolling";
            chkEnableInertialMouseScrolling.UseVisualStyleBackColor = true;
            // 
            // lblSlow
            // 
            lblSlow.Location = new System.Drawing.Point(186, 97);
            lblSlow.Name = "lblSlow";
            lblSlow.Size = new System.Drawing.Size(55, 19);
            lblSlow.TabIndex = 2;
            lblSlow.Text = "slow";
            lblSlow.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbMouseWheel
            // 
            tbMouseWheel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbMouseWheel.Location = new System.Drawing.Point(247, 97);
            tbMouseWheel.Maximum = 50;
            tbMouseWheel.Minimum = 5;
            tbMouseWheel.Name = "tbMouseWheel";
            tbMouseWheel.Size = new System.Drawing.Size(173, 16);
            tbMouseWheel.TabIndex = 3;
            tbMouseWheel.ThumbSize = new System.Drawing.Size(8, 16);
            tbMouseWheel.TickStyle = TickStyle.BottomRight;
            tbMouseWheel.Value = 5;
            // 
            // groupOverlays
            // 
            groupOverlays.Controls.Add(panelReaderOverlays);
            groupOverlays.Controls.Add(cbNavigationOverlayPosition);
            groupOverlays.Controls.Add(labelNavigationOverlayPosition);
            groupOverlays.Controls.Add(chkShowPageNames);
            groupOverlays.Controls.Add(tbOverlayScaling);
            groupOverlays.Controls.Add(chkShowCurrentPageOverlay);
            groupOverlays.Controls.Add(chkShowStatusOverlay);
            groupOverlays.Controls.Add(chkShowVisiblePartOverlay);
            groupOverlays.Controls.Add(chkShowNavigationOverlay);
            groupOverlays.Controls.Add(labelOverlaySize);
            groupOverlays.Dock = DockStyle.Top;
            groupOverlays.Location = new System.Drawing.Point(0, 690);
            groupOverlays.Name = "groupOverlays";
            groupOverlays.Size = new System.Drawing.Size(498, 356);
            groupOverlays.TabIndex = 2;
            groupOverlays.Text = "Overlays";
            // 
            // panelReaderOverlays
            // 
            panelReaderOverlays.Anchor = AnchorStyles.None;
            panelReaderOverlays.BackColor = System.Drawing.Color.WhiteSmoke;
            panelReaderOverlays.BorderStyle = BorderStyle.FixedSingle;
            panelReaderOverlays.Controls.Add(labelVisiblePartOverlay);
            panelReaderOverlays.Controls.Add(labelNavigationOverlay);
            panelReaderOverlays.Controls.Add(labelStatusOverlay);
            panelReaderOverlays.Controls.Add(labelPageOverlay);
            panelReaderOverlays.Location = new System.Drawing.Point(118, 39);
            panelReaderOverlays.Name = "panelReaderOverlays";
            panelReaderOverlays.Size = new System.Drawing.Size(258, 134);
            panelReaderOverlays.TabIndex = 8;
            // 
            // labelVisiblePartOverlay
            // 
            labelVisiblePartOverlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelVisiblePartOverlay.BackColor = System.Drawing.Color.Gainsboro;
            labelVisiblePartOverlay.BorderStyle = BorderStyle.FixedSingle;
            labelVisiblePartOverlay.FlatStyle = FlatStyle.Popup;
            labelVisiblePartOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelVisiblePartOverlay.Location = new System.Drawing.Point(204, 75);
            labelVisiblePartOverlay.Name = "labelVisiblePartOverlay";
            labelVisiblePartOverlay.Size = new System.Drawing.Size(49, 51);
            labelVisiblePartOverlay.TabIndex = 3;
            labelVisiblePartOverlay.Text = "Visible Part";
            labelVisiblePartOverlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelVisiblePartOverlay.UseMnemonic = false;
            // 
            // labelNavigationOverlay
            // 
            labelNavigationOverlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelNavigationOverlay.BackColor = System.Drawing.Color.Gainsboro;
            labelNavigationOverlay.BorderStyle = BorderStyle.FixedSingle;
            labelNavigationOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelNavigationOverlay.Location = new System.Drawing.Point(55, 100);
            labelNavigationOverlay.Name = "labelNavigationOverlay";
            labelNavigationOverlay.Size = new System.Drawing.Size(143, 26);
            labelNavigationOverlay.TabIndex = 2;
            labelNavigationOverlay.Text = "Navigation";
            labelNavigationOverlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelNavigationOverlay.UseMnemonic = false;
            // 
            // labelStatusOverlay
            // 
            labelStatusOverlay.BackColor = System.Drawing.Color.Gainsboro;
            labelStatusOverlay.BorderStyle = BorderStyle.FixedSingle;
            labelStatusOverlay.FlatStyle = FlatStyle.Popup;
            labelStatusOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelStatusOverlay.Location = new System.Drawing.Point(60, 49);
            labelStatusOverlay.Name = "labelStatusOverlay";
            labelStatusOverlay.Size = new System.Drawing.Size(134, 26);
            labelStatusOverlay.TabIndex = 1;
            labelStatusOverlay.Text = "Messages and Status";
            labelStatusOverlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelStatusOverlay.UseMnemonic = false;
            // 
            // labelPageOverlay
            // 
            labelPageOverlay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelPageOverlay.BackColor = System.Drawing.Color.Gainsboro;
            labelPageOverlay.BorderStyle = BorderStyle.FixedSingle;
            labelPageOverlay.FlatStyle = FlatStyle.Popup;
            labelPageOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelPageOverlay.Location = new System.Drawing.Point(204, 3);
            labelPageOverlay.Name = "labelPageOverlay";
            labelPageOverlay.Size = new System.Drawing.Size(49, 36);
            labelPageOverlay.TabIndex = 0;
            labelPageOverlay.Text = "Page";
            labelPageOverlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelPageOverlay.UseMnemonic = false;
            // 
            // cbNavigationOverlayPosition
            // 
            cbNavigationOverlayPosition.Anchor = AnchorStyles.None;
            cbNavigationOverlayPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNavigationOverlayPosition.FormattingEnabled = true;
            cbNavigationOverlayPosition.Items.AddRange(new object[] { "at Bottom", "on Top" });
            cbNavigationOverlayPosition.Location = new System.Drawing.Point(84, 313);
            cbNavigationOverlayPosition.Name = "cbNavigationOverlayPosition";
            cbNavigationOverlayPosition.Size = new System.Drawing.Size(121, 23);
            cbNavigationOverlayPosition.TabIndex = 6;
            // 
            // labelNavigationOverlayPosition
            // 
            labelNavigationOverlayPosition.Anchor = AnchorStyles.None;
            labelNavigationOverlayPosition.AutoSize = true;
            labelNavigationOverlayPosition.Location = new System.Drawing.Point(18, 316);
            labelNavigationOverlayPosition.Name = "labelNavigationOverlayPosition";
            labelNavigationOverlayPosition.Size = new System.Drawing.Size(68, 15);
            labelNavigationOverlayPosition.TabIndex = 5;
            labelNavigationOverlayPosition.Text = "Navigation:";
            // 
            // chkShowPageNames
            // 
            chkShowPageNames.Anchor = AnchorStyles.None;
            chkShowPageNames.AutoSize = true;
            chkShowPageNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            chkShowPageNames.Location = new System.Drawing.Point(283, 193);
            chkShowPageNames.Name = "chkShowPageNames";
            chkShowPageNames.Size = new System.Drawing.Size(181, 17);
            chkShowPageNames.TabIndex = 4;
            chkShowPageNames.Text = "Current Page also displays Name";
            chkShowPageNames.UseVisualStyleBackColor = true;
            // 
            // tbOverlayScaling
            // 
            tbOverlayScaling.Anchor = AnchorStyles.None;
            tbOverlayScaling.Location = new System.Drawing.Point(288, 316);
            tbOverlayScaling.Maximum = 150;
            tbOverlayScaling.Minimum = 40;
            tbOverlayScaling.Name = "tbOverlayScaling";
            tbOverlayScaling.Size = new System.Drawing.Size(184, 16);
            tbOverlayScaling.TabIndex = 8;
            tbOverlayScaling.ThumbSize = new System.Drawing.Size(8, 16);
            tbOverlayScaling.TickStyle = TickStyle.BottomRight;
            tbOverlayScaling.Value = 50;
            tbOverlayScaling.ValueChanged += tbOverlayScalingChanged;
            // 
            // chkShowCurrentPageOverlay
            // 
            chkShowCurrentPageOverlay.Anchor = AnchorStyles.None;
            chkShowCurrentPageOverlay.AutoSize = true;
            chkShowCurrentPageOverlay.Location = new System.Drawing.Point(58, 193);
            chkShowCurrentPageOverlay.Name = "chkShowCurrentPageOverlay";
            chkShowCurrentPageOverlay.Size = new System.Drawing.Size(125, 19);
            chkShowCurrentPageOverlay.TabIndex = 0;
            chkShowCurrentPageOverlay.Text = "Show current Page";
            chkShowCurrentPageOverlay.UseVisualStyleBackColor = true;
            // 
            // chkShowStatusOverlay
            // 
            chkShowStatusOverlay.Anchor = AnchorStyles.None;
            chkShowStatusOverlay.AutoSize = true;
            chkShowStatusOverlay.Location = new System.Drawing.Point(58, 217);
            chkShowStatusOverlay.Name = "chkShowStatusOverlay";
            chkShowStatusOverlay.Size = new System.Drawing.Size(167, 19);
            chkShowStatusOverlay.TabIndex = 1;
            chkShowStatusOverlay.Text = "Show Messages and Status";
            chkShowStatusOverlay.UseVisualStyleBackColor = true;
            // 
            // chkShowVisiblePartOverlay
            // 
            chkShowVisiblePartOverlay.Anchor = AnchorStyles.None;
            chkShowVisiblePartOverlay.AutoSize = true;
            chkShowVisiblePartOverlay.Location = new System.Drawing.Point(58, 241);
            chkShowVisiblePartOverlay.Name = "chkShowVisiblePartOverlay";
            chkShowVisiblePartOverlay.Size = new System.Drawing.Size(144, 19);
            chkShowVisiblePartOverlay.TabIndex = 2;
            chkShowVisiblePartOverlay.Text = "Show visible Page Part";
            chkShowVisiblePartOverlay.UseVisualStyleBackColor = true;
            // 
            // chkShowNavigationOverlay
            // 
            chkShowNavigationOverlay.Anchor = AnchorStyles.None;
            chkShowNavigationOverlay.AutoSize = true;
            chkShowNavigationOverlay.Location = new System.Drawing.Point(58, 264);
            chkShowNavigationOverlay.Name = "chkShowNavigationOverlay";
            chkShowNavigationOverlay.Size = new System.Drawing.Size(191, 19);
            chkShowNavigationOverlay.TabIndex = 3;
            chkShowNavigationOverlay.Text = "Show Navigation automatically";
            chkShowNavigationOverlay.UseVisualStyleBackColor = true;
            // 
            // labelOverlaySize
            // 
            labelOverlaySize.Anchor = AnchorStyles.None;
            labelOverlaySize.AutoSize = true;
            labelOverlaySize.Location = new System.Drawing.Point(244, 316);
            labelOverlaySize.Name = "labelOverlaySize";
            labelOverlaySize.Size = new System.Drawing.Size(41, 15);
            labelOverlaySize.TabIndex = 7;
            labelOverlaySize.Text = "Sizing:";
            // 
            // grpKeyboard
            // 
            grpKeyboard.Controls.Add(btExportKeyboard);
            grpKeyboard.Controls.Add(btImportKeyboard);
            grpKeyboard.Controls.Add(keyboardShortcutEditor);
            grpKeyboard.Dock = DockStyle.Top;
            grpKeyboard.Location = new System.Drawing.Point(0, 300);
            grpKeyboard.Name = "grpKeyboard";
            grpKeyboard.Size = new System.Drawing.Size(498, 390);
            grpKeyboard.TabIndex = 4;
            grpKeyboard.Text = "Keyboard";
            // 
            // btExportKeyboard
            // 
            btExportKeyboard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btExportKeyboard.Location = new System.Drawing.Point(274, 357);
            btExportKeyboard.Name = "btExportKeyboard";
            btExportKeyboard.Size = new System.Drawing.Size(102, 23);
            btExportKeyboard.TabIndex = 1;
            btExportKeyboard.Text = "Export...";
            btExportKeyboard.UseVisualStyleBackColor = true;
            btExportKeyboard.Click += btExportKeyboard_Click;
            // 
            // btImportKeyboard
            // 
            btImportKeyboard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btImportKeyboard.ContextMenuStrip = cmKeyboardLayout;
            btImportKeyboard.Location = new System.Drawing.Point(382, 357);
            btImportKeyboard.Name = "btImportKeyboard";
            btImportKeyboard.Size = new System.Drawing.Size(102, 23);
            btImportKeyboard.TabIndex = 2;
            btImportKeyboard.Text = "Import...";
            btImportKeyboard.UseVisualStyleBackColor = true;
            btImportKeyboard.ShowContextMenu += btImportKeyboard_ShowContextMenu;
            btImportKeyboard.Click += btLoadKeyboard_Click;
            // 
            // cmKeyboardLayout
            // 
            cmKeyboardLayout.Items.AddRange(new ToolStripItem[] { miDefaultKeyboardLayout, toolStripMenuItem1 });
            cmKeyboardLayout.Name = "cmKeyboardLayout";
            cmKeyboardLayout.Size = new System.Drawing.Size(113, 32);
            // 
            // miDefaultKeyboardLayout
            // 
            miDefaultKeyboardLayout.Name = "miDefaultKeyboardLayout";
            miDefaultKeyboardLayout.Size = new System.Drawing.Size(112, 22);
            miDefaultKeyboardLayout.Text = "&Default";
            miDefaultKeyboardLayout.Click += miDefaultKeyboardLayout_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(109, 6);
            // 
            // keyboardShortcutEditor
            // 
            keyboardShortcutEditor.AllowDrop = true;
            keyboardShortcutEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            keyboardShortcutEditor.Location = new System.Drawing.Point(12, 37);
            keyboardShortcutEditor.Name = "keyboardShortcutEditor";
            keyboardShortcutEditor.Shortcuts = null;
            keyboardShortcutEditor.Size = new System.Drawing.Size(472, 314);
            keyboardShortcutEditor.TabIndex = 0;
            keyboardShortcutEditor.DragDrop += keyboardShortcutEditor_DragDrop;
            keyboardShortcutEditor.DragOver += keyboardShortcutEditor_DragOver;
            // 
            // grpDisplay
            // 
            grpDisplay.Controls.Add(tbGamma);
            grpDisplay.Controls.Add(labelGamma);
            grpDisplay.Controls.Add(chkAnamorphicScaling);
            grpDisplay.Controls.Add(chkHighQualityDisplay);
            grpDisplay.Controls.Add(labelSharpening);
            grpDisplay.Controls.Add(tbSharpening);
            grpDisplay.Controls.Add(btResetColor);
            grpDisplay.Controls.Add(chkAutoContrast);
            grpDisplay.Controls.Add(labelSaturation);
            grpDisplay.Controls.Add(tbSaturation);
            grpDisplay.Controls.Add(labelBrightness);
            grpDisplay.Controls.Add(tbBrightness);
            grpDisplay.Controls.Add(tbContrast);
            grpDisplay.Controls.Add(labelContrast);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Location = new System.Drawing.Point(0, 0);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new System.Drawing.Size(498, 300);
            grpDisplay.TabIndex = 1;
            grpDisplay.Text = "Display";
            // 
            // tbGamma
            // 
            tbGamma.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbGamma.Location = new System.Drawing.Point(150, 193);
            tbGamma.Minimum = -100;
            tbGamma.Name = "tbGamma";
            tbGamma.Size = new System.Drawing.Size(332, 16);
            tbGamma.TabIndex = 12;
            tbGamma.Text = "tbSaturation";
            tbGamma.ThumbSize = new System.Drawing.Size(8, 16);
            tbGamma.TickStyle = TickStyle.BottomRight;
            tbGamma.ValueChanged += tbColorAdjustmentChanged;
            tbGamma.DoubleClick += tbGamma_DoubleClick;
            // 
            // labelGamma
            // 
            labelGamma.Location = new System.Drawing.Point(14, 193);
            labelGamma.Name = "labelGamma";
            labelGamma.Size = new System.Drawing.Size(133, 13);
            labelGamma.TabIndex = 11;
            labelGamma.Text = "Gamma Adjustment:";
            labelGamma.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkAnamorphicScaling
            // 
            chkAnamorphicScaling.AutoSize = true;
            chkAnamorphicScaling.Location = new System.Drawing.Point(12, 60);
            chkAnamorphicScaling.Name = "chkAnamorphicScaling";
            chkAnamorphicScaling.Size = new System.Drawing.Size(133, 19);
            chkAnamorphicScaling.TabIndex = 0;
            chkAnamorphicScaling.Text = "&Anamorphic Scaling";
            chkAnamorphicScaling.UseVisualStyleBackColor = true;
            // 
            // chkHighQualityDisplay
            // 
            chkHighQualityDisplay.AutoSize = true;
            chkHighQualityDisplay.Location = new System.Drawing.Point(12, 37);
            chkHighQualityDisplay.Name = "chkHighQualityDisplay";
            chkHighQualityDisplay.Size = new System.Drawing.Size(93, 19);
            chkHighQualityDisplay.TabIndex = 0;
            chkHighQualityDisplay.Text = "&High Quality";
            chkHighQualityDisplay.UseVisualStyleBackColor = true;
            // 
            // labelSharpening
            // 
            labelSharpening.Location = new System.Drawing.Point(17, 225);
            labelSharpening.Name = "labelSharpening";
            labelSharpening.Size = new System.Drawing.Size(132, 13);
            labelSharpening.TabIndex = 8;
            labelSharpening.Text = "Sharpening:";
            labelSharpening.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSharpening
            // 
            tbSharpening.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSharpening.LargeChange = 1;
            tbSharpening.Location = new System.Drawing.Point(149, 225);
            tbSharpening.Maximum = 3;
            tbSharpening.Name = "tbSharpening";
            tbSharpening.Size = new System.Drawing.Size(333, 18);
            tbSharpening.TabIndex = 9;
            tbSharpening.Text = "tbSaturation";
            tbSharpening.ThumbSize = new System.Drawing.Size(8, 16);
            tbSharpening.TickFrequency = 1;
            tbSharpening.TickStyle = TickStyle.BottomRight;
            tbSharpening.DoubleClick += tbSharpening_DoubleClick;
            // 
            // btResetColor
            // 
            btResetColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btResetColor.Location = new System.Drawing.Point(394, 265);
            btResetColor.Name = "btResetColor";
            btResetColor.Size = new System.Drawing.Size(91, 23);
            btResetColor.TabIndex = 10;
            btResetColor.Text = "&Reset";
            btResetColor.UseVisualStyleBackColor = true;
            btResetColor.Click += btReset_Click;
            // 
            // chkAutoContrast
            // 
            chkAutoContrast.AutoSize = true;
            chkAutoContrast.Location = new System.Drawing.Point(12, 95);
            chkAutoContrast.Name = "chkAutoContrast";
            chkAutoContrast.Size = new System.Drawing.Size(206, 19);
            chkAutoContrast.TabIndex = 1;
            chkAutoContrast.Text = "Automatic &Contrast Enhancement";
            chkAutoContrast.UseVisualStyleBackColor = true;
            // 
            // labelSaturation
            // 
            labelSaturation.Location = new System.Drawing.Point(11, 122);
            labelSaturation.Name = "labelSaturation";
            labelSaturation.Size = new System.Drawing.Size(136, 13);
            labelSaturation.TabIndex = 2;
            labelSaturation.Text = "Saturation Adjustment:";
            labelSaturation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSaturation
            // 
            tbSaturation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSaturation.Location = new System.Drawing.Point(148, 122);
            tbSaturation.Minimum = -100;
            tbSaturation.Name = "tbSaturation";
            tbSaturation.Size = new System.Drawing.Size(334, 16);
            tbSaturation.TabIndex = 3;
            tbSaturation.ThumbSize = new System.Drawing.Size(8, 16);
            tbSaturation.TickStyle = TickStyle.BottomRight;
            tbSaturation.ValueChanged += tbColorAdjustmentChanged;
            tbSaturation.DoubleClick += tbSaturation_DoubleClick;
            // 
            // labelBrightness
            // 
            labelBrightness.Location = new System.Drawing.Point(14, 144);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new System.Drawing.Size(133, 13);
            labelBrightness.TabIndex = 4;
            labelBrightness.Text = "Brightness Adjustment:";
            labelBrightness.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbBrightness
            // 
            tbBrightness.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbBrightness.Location = new System.Drawing.Point(148, 144);
            tbBrightness.Minimum = -100;
            tbBrightness.Name = "tbBrightness";
            tbBrightness.Size = new System.Drawing.Size(334, 16);
            tbBrightness.TabIndex = 5;
            tbBrightness.Text = "tbBrightness";
            tbBrightness.ThumbSize = new System.Drawing.Size(8, 16);
            tbBrightness.TickStyle = TickStyle.BottomRight;
            tbBrightness.ValueChanged += tbColorAdjustmentChanged;
            tbBrightness.DoubleClick += tbBrightness_DoubleClick;
            // 
            // tbContrast
            // 
            tbContrast.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbContrast.Location = new System.Drawing.Point(148, 168);
            tbContrast.Minimum = -100;
            tbContrast.Name = "tbContrast";
            tbContrast.Size = new System.Drawing.Size(334, 16);
            tbContrast.TabIndex = 7;
            tbContrast.Text = "tbSaturation";
            tbContrast.ThumbSize = new System.Drawing.Size(8, 16);
            tbContrast.TickStyle = TickStyle.BottomRight;
            tbContrast.ValueChanged += tbColorAdjustmentChanged;
            tbContrast.DoubleClick += tbContrast_DoubleClick;
            // 
            // labelContrast
            // 
            labelContrast.Location = new System.Drawing.Point(14, 168);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new System.Drawing.Size(133, 13);
            labelContrast.TabIndex = 6;
            labelContrast.Text = "Contrast Adjustment:";
            labelContrast.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pageAdvanced
            // 
            pageAdvanced.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pageAdvanced.AutoScroll = true;
            pageAdvanced.BorderStyle = BorderStyle.FixedSingle;
            pageAdvanced.Controls.Add(grpWirelessSetup);
            pageAdvanced.Controls.Add(grpIntegration);
            pageAdvanced.Controls.Add(groupMessagesAndSocial);
            pageAdvanced.Controls.Add(groupMemory);
            pageAdvanced.Controls.Add(grpDatabaseBackup);
            pageAdvanced.Controls.Add(groupOtherComics);
            pageAdvanced.Controls.Add(grpLanguages);
            pageAdvanced.Location = new System.Drawing.Point(84, 8);
            pageAdvanced.Name = "pageAdvanced";
            pageAdvanced.Size = new System.Drawing.Size(517, 408);
            pageAdvanced.TabIndex = 9;
            // 
            // grpWirelessSetup
            // 
            grpWirelessSetup.Controls.Add(btTestWifi);
            grpWirelessSetup.Controls.Add(lblWifiStatus);
            grpWirelessSetup.Controls.Add(lblWifiAddresses);
            grpWirelessSetup.Controls.Add(txWifiAddresses);
            grpWirelessSetup.Dock = DockStyle.Top;
            grpWirelessSetup.Location = new System.Drawing.Point(0, 1411);
            grpWirelessSetup.Name = "grpWirelessSetup";
            grpWirelessSetup.Size = new System.Drawing.Size(498, 136);
            grpWirelessSetup.TabIndex = 8;
            grpWirelessSetup.Text = "Wireless Setup";
            // 
            // btTestWifi
            // 
            btTestWifi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btTestWifi.Location = new System.Drawing.Point(382, 63);
            btTestWifi.Name = "btTestWifi";
            btTestWifi.Size = new System.Drawing.Size(104, 23);
            btTestWifi.TabIndex = 3;
            btTestWifi.Text = "Test";
            btTestWifi.UseVisualStyleBackColor = true;
            btTestWifi.Click += btTestWifi_Click;
            // 
            // lblWifiStatus
            // 
            lblWifiStatus.Location = new System.Drawing.Point(6, 93);
            lblWifiStatus.Name = "lblWifiStatus";
            lblWifiStatus.Size = new System.Drawing.Size(370, 21);
            lblWifiStatus.TabIndex = 2;
            lblWifiStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWifiAddresses
            // 
            lblWifiAddresses.AutoSize = true;
            lblWifiAddresses.Location = new System.Drawing.Point(4, 41);
            lblWifiAddresses.Name = "lblWifiAddresses";
            lblWifiAddresses.Size = new System.Drawing.Size(541, 15);
            lblWifiAddresses.TabIndex = 1;
            lblWifiAddresses.Text = "Semicolon separated list of IP addresses for Wireless Devices which where not detected automatically:";
            // 
            // txWifiAddresses
            // 
            txWifiAddresses.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txWifiAddresses.Location = new System.Drawing.Point(6, 65);
            txWifiAddresses.Name = "txWifiAddresses";
            txWifiAddresses.Size = new System.Drawing.Size(370, 23);
            txWifiAddresses.TabIndex = 0;
            // 
            // grpIntegration
            // 
            grpIntegration.Controls.Add(btAssociateExtensions);
            grpIntegration.Controls.Add(labelCheckedFormats);
            grpIntegration.Controls.Add(chkOverwriteAssociations);
            grpIntegration.Controls.Add(lbFormats);
            grpIntegration.Dock = DockStyle.Top;
            grpIntegration.Location = new System.Drawing.Point(0, 1071);
            grpIntegration.Name = "grpIntegration";
            grpIntegration.Size = new System.Drawing.Size(498, 340);
            grpIntegration.TabIndex = 0;
            grpIntegration.Text = "Explorer Integration";
            // 
            // btAssociateExtensions
            // 
            btAssociateExtensions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAssociateExtensions.Location = new System.Drawing.Point(382, 57);
            btAssociateExtensions.Name = "btAssociateExtensions";
            btAssociateExtensions.Size = new System.Drawing.Size(104, 23);
            btAssociateExtensions.TabIndex = 4;
            btAssociateExtensions.Text = "Change...";
            btAssociateExtensions.UseVisualStyleBackColor = true;
            btAssociateExtensions.Click += btAssociateExtensions_Click;
            // 
            // labelCheckedFormats
            // 
            labelCheckedFormats.AutoSize = true;
            labelCheckedFormats.Location = new System.Drawing.Point(3, 35);
            labelCheckedFormats.Name = "labelCheckedFormats";
            labelCheckedFormats.Size = new System.Drawing.Size(281, 15);
            labelCheckedFormats.TabIndex = 0;
            labelCheckedFormats.Text = "Checked formats will be associated with ComicRack";
            // 
            // chkOverwriteAssociations
            // 
            chkOverwriteAssociations.AutoSize = true;
            chkOverwriteAssociations.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            chkOverwriteAssociations.Location = new System.Drawing.Point(6, 307);
            chkOverwriteAssociations.Name = "chkOverwriteAssociations";
            chkOverwriteAssociations.Size = new System.Drawing.Size(321, 19);
            chkOverwriteAssociations.TabIndex = 2;
            chkOverwriteAssociations.Text = "Overwrite existing associations instead of 'Open With ...'";
            chkOverwriteAssociations.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            chkOverwriteAssociations.UseVisualStyleBackColor = true;
            // 
            // lbFormats
            // 
            lbFormats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbFormats.CheckOnClick = true;
            lbFormats.FormattingEnabled = true;
            lbFormats.Location = new System.Drawing.Point(6, 57);
            lbFormats.Name = "lbFormats";
            lbFormats.Size = new System.Drawing.Size(371, 238);
            lbFormats.TabIndex = 1;
            // 
            // groupMessagesAndSocial
            // 
            groupMessagesAndSocial.Controls.Add(btResetMessages);
            groupMessagesAndSocial.Controls.Add(labelReshowHidden);
            groupMessagesAndSocial.Dock = DockStyle.Top;
            groupMessagesAndSocial.Location = new System.Drawing.Point(0, 996);
            groupMessagesAndSocial.Name = "groupMessagesAndSocial";
            groupMessagesAndSocial.Size = new System.Drawing.Size(498, 75);
            groupMessagesAndSocial.TabIndex = 6;
            groupMessagesAndSocial.Text = "Messages and Social";
            // 
            // btResetMessages
            // 
            btResetMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btResetMessages.Location = new System.Drawing.Point(382, 41);
            btResetMessages.Name = "btResetMessages";
            btResetMessages.Size = new System.Drawing.Size(104, 23);
            btResetMessages.TabIndex = 1;
            btResetMessages.Text = "Reset";
            btResetMessages.UseVisualStyleBackColor = true;
            btResetMessages.Click += btResetMessages_Click;
            // 
            // labelReshowHidden
            // 
            labelReshowHidden.Location = new System.Drawing.Point(6, 46);
            labelReshowHidden.Name = "labelReshowHidden";
            labelReshowHidden.Size = new System.Drawing.Size(370, 17);
            labelReshowHidden.TabIndex = 0;
            labelReshowHidden.Text = "To reshow hidden messages press";
            // 
            // groupMemory
            // 
            groupMemory.Controls.Add(grpMaximumMemoryUsage);
            groupMemory.Controls.Add(grpMemoryCache);
            groupMemory.Controls.Add(grpDiskCache);
            groupMemory.Dock = DockStyle.Top;
            groupMemory.Location = new System.Drawing.Point(0, 641);
            groupMemory.Name = "groupMemory";
            groupMemory.Size = new System.Drawing.Size(498, 355);
            groupMemory.TabIndex = 1;
            groupMemory.Text = "Caches & Memory Usage";
            // 
            // grpMaximumMemoryUsage
            // 
            grpMaximumMemoryUsage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpMaximumMemoryUsage.Controls.Add(lblMaximumMemoryUsageValue);
            grpMaximumMemoryUsage.Controls.Add(tbMaximumMemoryUsage);
            grpMaximumMemoryUsage.Controls.Add(lblMaximumMemoryUsage);
            grpMaximumMemoryUsage.Location = new System.Drawing.Point(7, 255);
            grpMaximumMemoryUsage.Name = "grpMaximumMemoryUsage";
            grpMaximumMemoryUsage.Size = new System.Drawing.Size(476, 86);
            grpMaximumMemoryUsage.TabIndex = 14;
            grpMaximumMemoryUsage.TabStop = false;
            grpMaximumMemoryUsage.Text = "Maximum Memory Usage";
            // 
            // lblMaximumMemoryUsageValue
            // 
            lblMaximumMemoryUsageValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMaximumMemoryUsageValue.AutoSize = true;
            lblMaximumMemoryUsageValue.Location = new System.Drawing.Point(397, 31);
            lblMaximumMemoryUsageValue.Name = "lblMaximumMemoryUsageValue";
            lblMaximumMemoryUsageValue.Size = new System.Drawing.Size(67, 15);
            lblMaximumMemoryUsageValue.TabIndex = 2;
            lblMaximumMemoryUsageValue.Text = "Slider Value";
            // 
            // tbMaximumMemoryUsage
            // 
            tbMaximumMemoryUsage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbMaximumMemoryUsage.LargeChange = 4;
            tbMaximumMemoryUsage.Location = new System.Drawing.Point(7, 24);
            tbMaximumMemoryUsage.Maximum = 64;
            tbMaximumMemoryUsage.Name = "tbMaximumMemoryUsage";
            tbMaximumMemoryUsage.Size = new System.Drawing.Size(379, 29);
            tbMaximumMemoryUsage.TabIndex = 1;
            tbMaximumMemoryUsage.ThumbSize = new System.Drawing.Size(10, 20);
            tbMaximumMemoryUsage.TickFrequency = 8;
            tbMaximumMemoryUsage.TickStyle = TickStyle.BottomRight;
            tbMaximumMemoryUsage.TickThickness = 2;
            tbMaximumMemoryUsage.ValueChanged += tbSystemMemory_ValueChanged;
            // 
            // lblMaximumMemoryUsage
            // 
            lblMaximumMemoryUsage.Dock = DockStyle.Bottom;
            lblMaximumMemoryUsage.Location = new System.Drawing.Point(3, 58);
            lblMaximumMemoryUsage.Name = "lblMaximumMemoryUsage";
            lblMaximumMemoryUsage.Size = new System.Drawing.Size(470, 25);
            lblMaximumMemoryUsage.TabIndex = 0;
            lblMaximumMemoryUsage.Text = "Limiting the memory can adversely affect the performance.";
            lblMaximumMemoryUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpMemoryCache
            // 
            grpMemoryCache.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpMemoryCache.Controls.Add(lblPageMemCacheUsage);
            grpMemoryCache.Controls.Add(labelMemThumbSize);
            grpMemoryCache.Controls.Add(lblThumbMemCacheUsage);
            grpMemoryCache.Controls.Add(numMemPageCount);
            grpMemoryCache.Controls.Add(labelMemPageCount);
            grpMemoryCache.Controls.Add(chkMemPageOptimized);
            grpMemoryCache.Controls.Add(chkMemThumbOptimized);
            grpMemoryCache.Controls.Add(numMemThumbSize);
            grpMemoryCache.Location = new System.Drawing.Point(6, 162);
            grpMemoryCache.Name = "grpMemoryCache";
            grpMemoryCache.Size = new System.Drawing.Size(476, 85);
            grpMemoryCache.TabIndex = 13;
            grpMemoryCache.TabStop = false;
            grpMemoryCache.Text = "Memory Cache";
            // 
            // lblPageMemCacheUsage
            // 
            lblPageMemCacheUsage.Anchor = AnchorStyles.None;
            lblPageMemCacheUsage.AutoSize = true;
            lblPageMemCacheUsage.Location = new System.Drawing.Point(299, 52);
            lblPageMemCacheUsage.Name = "lblPageMemCacheUsage";
            lblPageMemCacheUsage.Size = new System.Drawing.Size(134, 15);
            lblPageMemCacheUsage.TabIndex = 8;
            lblPageMemCacheUsage.Text = "usage Page Mem Cache";
            // 
            // labelMemThumbSize
            // 
            labelMemThumbSize.Anchor = AnchorStyles.None;
            labelMemThumbSize.AutoSize = true;
            labelMemThumbSize.Location = new System.Drawing.Point(19, 29);
            labelMemThumbSize.Name = "labelMemThumbSize";
            labelMemThumbSize.Size = new System.Drawing.Size(98, 15);
            labelMemThumbSize.TabIndex = 0;
            labelMemThumbSize.Text = "Thumbnails [MB]";
            // 
            // lblThumbMemCacheUsage
            // 
            lblThumbMemCacheUsage.Anchor = AnchorStyles.None;
            lblThumbMemCacheUsage.AutoSize = true;
            lblThumbMemCacheUsage.Location = new System.Drawing.Point(299, 26);
            lblThumbMemCacheUsage.Name = "lblThumbMemCacheUsage";
            lblThumbMemCacheUsage.Size = new System.Drawing.Size(146, 15);
            lblThumbMemCacheUsage.TabIndex = 7;
            lblThumbMemCacheUsage.Text = "usage Thumb Mem Cache";
            // 
            // numMemPageCount
            // 
            numMemPageCount.Anchor = AnchorStyles.None;
            numMemPageCount.Location = new System.Drawing.Point(145, 51);
            numMemPageCount.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            numMemPageCount.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numMemPageCount.Name = "numMemPageCount";
            numMemPageCount.Size = new System.Drawing.Size(67, 23);
            numMemPageCount.TabIndex = 4;
            numMemPageCount.TextAlign = HorizontalAlignment.Right;
            numMemPageCount.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // labelMemPageCount
            // 
            labelMemPageCount.Anchor = AnchorStyles.None;
            labelMemPageCount.AutoSize = true;
            labelMemPageCount.Location = new System.Drawing.Point(19, 52);
            labelMemPageCount.Name = "labelMemPageCount";
            labelMemPageCount.Size = new System.Drawing.Size(80, 15);
            labelMemPageCount.TabIndex = 3;
            labelMemPageCount.Text = "Pages [count]";
            // 
            // chkMemPageOptimized
            // 
            chkMemPageOptimized.Anchor = AnchorStyles.None;
            chkMemPageOptimized.AutoSize = true;
            chkMemPageOptimized.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            chkMemPageOptimized.Location = new System.Drawing.Point(218, 51);
            chkMemPageOptimized.Name = "chkMemPageOptimized";
            chkMemPageOptimized.Size = new System.Drawing.Size(79, 19);
            chkMemPageOptimized.TabIndex = 5;
            chkMemPageOptimized.Text = "optimized";
            chkMemPageOptimized.UseVisualStyleBackColor = true;
            // 
            // chkMemThumbOptimized
            // 
            chkMemThumbOptimized.Anchor = AnchorStyles.None;
            chkMemThumbOptimized.AutoSize = true;
            chkMemThumbOptimized.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            chkMemThumbOptimized.Location = new System.Drawing.Point(218, 25);
            chkMemThumbOptimized.Name = "chkMemThumbOptimized";
            chkMemThumbOptimized.Size = new System.Drawing.Size(79, 19);
            chkMemThumbOptimized.TabIndex = 2;
            chkMemThumbOptimized.Text = "optimized";
            chkMemThumbOptimized.UseVisualStyleBackColor = true;
            // 
            // numMemThumbSize
            // 
            numMemThumbSize.Anchor = AnchorStyles.None;
            numMemThumbSize.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numMemThumbSize.Location = new System.Drawing.Point(145, 24);
            numMemThumbSize.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numMemThumbSize.Name = "numMemThumbSize";
            numMemThumbSize.Size = new System.Drawing.Size(67, 23);
            numMemThumbSize.TabIndex = 1;
            numMemThumbSize.TextAlign = HorizontalAlignment.Right;
            numMemThumbSize.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // grpDiskCache
            // 
            grpDiskCache.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDiskCache.Controls.Add(chkEnableInternetCache);
            grpDiskCache.Controls.Add(lblInternetCacheUsage);
            grpDiskCache.Controls.Add(btClearPageCache);
            grpDiskCache.Controls.Add(numPageCacheSize);
            grpDiskCache.Controls.Add(numInternetCacheSize);
            grpDiskCache.Controls.Add(btClearThumbnailCache);
            grpDiskCache.Controls.Add(btClearInternetCache);
            grpDiskCache.Controls.Add(chkEnablePageCache);
            grpDiskCache.Controls.Add(lblPageCacheUsage);
            grpDiskCache.Controls.Add(numThumbnailCacheSize);
            grpDiskCache.Controls.Add(chkEnableThumbnailCache);
            grpDiskCache.Controls.Add(lblThumbCacheUsage);
            grpDiskCache.Location = new System.Drawing.Point(6, 35);
            grpDiskCache.Name = "grpDiskCache";
            grpDiskCache.Size = new System.Drawing.Size(476, 120);
            grpDiskCache.TabIndex = 12;
            grpDiskCache.TabStop = false;
            grpDiskCache.Text = "Disk Cache";
            // 
            // chkEnableInternetCache
            // 
            chkEnableInternetCache.Anchor = AnchorStyles.None;
            chkEnableInternetCache.AutoSize = true;
            chkEnableInternetCache.Location = new System.Drawing.Point(22, 31);
            chkEnableInternetCache.Name = "chkEnableInternetCache";
            chkEnableInternetCache.Size = new System.Drawing.Size(96, 19);
            chkEnableInternetCache.TabIndex = 0;
            chkEnableInternetCache.Text = "Internet [MB]";
            chkEnableInternetCache.UseVisualStyleBackColor = true;
            // 
            // lblInternetCacheUsage
            // 
            lblInternetCacheUsage.Anchor = AnchorStyles.None;
            lblInternetCacheUsage.AutoSize = true;
            lblInternetCacheUsage.Location = new System.Drawing.Point(298, 31);
            lblInternetCacheUsage.Name = "lblInternetCacheUsage";
            lblInternetCacheUsage.Size = new System.Drawing.Size(118, 15);
            lblInternetCacheUsage.TabIndex = 3;
            lblInternetCacheUsage.Text = "usage Internet Cache";
            // 
            // btClearPageCache
            // 
            btClearPageCache.Anchor = AnchorStyles.None;
            btClearPageCache.Location = new System.Drawing.Point(218, 80);
            btClearPageCache.Name = "btClearPageCache";
            btClearPageCache.Size = new System.Drawing.Size(74, 21);
            btClearPageCache.TabIndex = 10;
            btClearPageCache.Text = "Clear";
            btClearPageCache.UseVisualStyleBackColor = true;
            btClearPageCache.Click += btClearPageCache_Click;
            // 
            // numPageCacheSize
            // 
            numPageCacheSize.Anchor = AnchorStyles.None;
            numPageCacheSize.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numPageCacheSize.Location = new System.Drawing.Point(145, 82);
            numPageCacheSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPageCacheSize.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numPageCacheSize.Name = "numPageCacheSize";
            numPageCacheSize.Size = new System.Drawing.Size(67, 23);
            numPageCacheSize.TabIndex = 9;
            numPageCacheSize.TextAlign = HorizontalAlignment.Right;
            numPageCacheSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numInternetCacheSize
            // 
            numInternetCacheSize.Anchor = AnchorStyles.None;
            numInternetCacheSize.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numInternetCacheSize.Location = new System.Drawing.Point(145, 29);
            numInternetCacheSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numInternetCacheSize.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numInternetCacheSize.Name = "numInternetCacheSize";
            numInternetCacheSize.Size = new System.Drawing.Size(67, 23);
            numInternetCacheSize.TabIndex = 1;
            numInternetCacheSize.TextAlign = HorizontalAlignment.Right;
            numInternetCacheSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // btClearThumbnailCache
            // 
            btClearThumbnailCache.Anchor = AnchorStyles.None;
            btClearThumbnailCache.Location = new System.Drawing.Point(218, 54);
            btClearThumbnailCache.Name = "btClearThumbnailCache";
            btClearThumbnailCache.Size = new System.Drawing.Size(74, 21);
            btClearThumbnailCache.TabIndex = 6;
            btClearThumbnailCache.Text = "Clear";
            btClearThumbnailCache.UseVisualStyleBackColor = true;
            btClearThumbnailCache.Click += btClearThumbnailCache_Click;
            // 
            // btClearInternetCache
            // 
            btClearInternetCache.Anchor = AnchorStyles.None;
            btClearInternetCache.Location = new System.Drawing.Point(218, 27);
            btClearInternetCache.Name = "btClearInternetCache";
            btClearInternetCache.Size = new System.Drawing.Size(74, 21);
            btClearInternetCache.TabIndex = 2;
            btClearInternetCache.Text = "Clear";
            btClearInternetCache.UseVisualStyleBackColor = true;
            btClearInternetCache.Click += btClearInternetCache_Click;
            // 
            // chkEnablePageCache
            // 
            chkEnablePageCache.Anchor = AnchorStyles.None;
            chkEnablePageCache.AutoSize = true;
            chkEnablePageCache.Location = new System.Drawing.Point(22, 84);
            chkEnablePageCache.Name = "chkEnablePageCache";
            chkEnablePageCache.Size = new System.Drawing.Size(86, 19);
            chkEnablePageCache.TabIndex = 8;
            chkEnablePageCache.Text = "&Pages [MB]";
            chkEnablePageCache.UseVisualStyleBackColor = true;
            // 
            // lblPageCacheUsage
            // 
            lblPageCacheUsage.Anchor = AnchorStyles.None;
            lblPageCacheUsage.AutoSize = true;
            lblPageCacheUsage.Location = new System.Drawing.Point(298, 86);
            lblPageCacheUsage.Name = "lblPageCacheUsage";
            lblPageCacheUsage.Size = new System.Drawing.Size(103, 15);
            lblPageCacheUsage.TabIndex = 11;
            lblPageCacheUsage.Text = "usage Page Cache";
            // 
            // numThumbnailCacheSize
            // 
            numThumbnailCacheSize.Anchor = AnchorStyles.None;
            numThumbnailCacheSize.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numThumbnailCacheSize.Location = new System.Drawing.Point(145, 55);
            numThumbnailCacheSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numThumbnailCacheSize.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numThumbnailCacheSize.Name = "numThumbnailCacheSize";
            numThumbnailCacheSize.Size = new System.Drawing.Size(67, 23);
            numThumbnailCacheSize.TabIndex = 5;
            numThumbnailCacheSize.TextAlign = HorizontalAlignment.Right;
            numThumbnailCacheSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // chkEnableThumbnailCache
            // 
            chkEnableThumbnailCache.Anchor = AnchorStyles.None;
            chkEnableThumbnailCache.AutoSize = true;
            chkEnableThumbnailCache.Location = new System.Drawing.Point(22, 57);
            chkEnableThumbnailCache.Name = "chkEnableThumbnailCache";
            chkEnableThumbnailCache.Size = new System.Drawing.Size(117, 19);
            chkEnableThumbnailCache.TabIndex = 4;
            chkEnableThumbnailCache.Text = "&Thumbnails [MB]";
            chkEnableThumbnailCache.UseVisualStyleBackColor = true;
            // 
            // lblThumbCacheUsage
            // 
            lblThumbCacheUsage.Anchor = AnchorStyles.None;
            lblThumbCacheUsage.AutoSize = true;
            lblThumbCacheUsage.Location = new System.Drawing.Point(298, 58);
            lblThumbCacheUsage.Name = "lblThumbCacheUsage";
            lblThumbCacheUsage.Size = new System.Drawing.Size(115, 15);
            lblThumbCacheUsage.TabIndex = 7;
            lblThumbCacheUsage.Text = "usage Thumb Cache";
            // 
            // grpDatabaseBackup
            // 
            grpDatabaseBackup.Controls.Add(btRestoreDatabase);
            grpDatabaseBackup.Controls.Add(btBackupDatabase);
            grpDatabaseBackup.Dock = DockStyle.Top;
            grpDatabaseBackup.Location = new System.Drawing.Point(0, 548);
            grpDatabaseBackup.Name = "grpDatabaseBackup";
            grpDatabaseBackup.Size = new System.Drawing.Size(498, 93);
            grpDatabaseBackup.TabIndex = 4;
            grpDatabaseBackup.Text = "Database Backup";
            // 
            // btRestoreDatabase
            // 
            btRestoreDatabase.Anchor = AnchorStyles.None;
            btRestoreDatabase.Location = new System.Drawing.Point(259, 41);
            btRestoreDatabase.Name = "btRestoreDatabase";
            btRestoreDatabase.Size = new System.Drawing.Size(227, 23);
            btRestoreDatabase.TabIndex = 1;
            btRestoreDatabase.Text = "Restore Database...";
            btRestoreDatabase.UseVisualStyleBackColor = true;
            btRestoreDatabase.Click += btRestoreDatabase_Click;
            // 
            // btBackupDatabase
            // 
            btBackupDatabase.Anchor = AnchorStyles.None;
            btBackupDatabase.Location = new System.Drawing.Point(9, 41);
            btBackupDatabase.Name = "btBackupDatabase";
            btBackupDatabase.Size = new System.Drawing.Size(247, 23);
            btBackupDatabase.TabIndex = 0;
            btBackupDatabase.Text = "Backup Database...";
            btBackupDatabase.UseVisualStyleBackColor = true;
            btBackupDatabase.Click += btBackupDatabase_Click;
            // 
            // groupOtherComics
            // 
            groupOtherComics.Controls.Add(chkUpdateComicFiles);
            groupOtherComics.Controls.Add(labelExcludeCover);
            groupOtherComics.Controls.Add(chkAutoUpdateComicFiles);
            groupOtherComics.Controls.Add(txCoverFilter);
            groupOtherComics.Dock = DockStyle.Top;
            groupOtherComics.Location = new System.Drawing.Point(0, 372);
            groupOtherComics.Name = "groupOtherComics";
            groupOtherComics.Size = new System.Drawing.Size(498, 176);
            groupOtherComics.TabIndex = 5;
            groupOtherComics.Text = "Books";
            // 
            // chkUpdateComicFiles
            // 
            chkUpdateComicFiles.AutoSize = true;
            chkUpdateComicFiles.Location = new System.Drawing.Point(9, 42);
            chkUpdateComicFiles.Name = "chkUpdateComicFiles";
            chkUpdateComicFiles.Size = new System.Drawing.Size(212, 19);
            chkUpdateComicFiles.TabIndex = 0;
            chkUpdateComicFiles.Text = "Allow writing of Book info into files";
            chkUpdateComicFiles.UseVisualStyleBackColor = true;
            // 
            // labelExcludeCover
            // 
            labelExcludeCover.AutoSize = true;
            labelExcludeCover.Location = new System.Drawing.Point(6, 93);
            labelExcludeCover.Name = "labelExcludeCover";
            labelExcludeCover.Size = new System.Drawing.Size(425, 15);
            labelExcludeCover.TabIndex = 2;
            labelExcludeCover.Text = "Semicolon separated list of image names never to be used as cover thumbnails:";
            // 
            // chkAutoUpdateComicFiles
            // 
            chkAutoUpdateComicFiles.AutoSize = true;
            chkAutoUpdateComicFiles.Location = new System.Drawing.Point(9, 65);
            chkAutoUpdateComicFiles.Name = "chkAutoUpdateComicFiles";
            chkAutoUpdateComicFiles.Size = new System.Drawing.Size(218, 19);
            chkAutoUpdateComicFiles.TabIndex = 1;
            chkAutoUpdateComicFiles.Text = "Book files are updated automatically";
            chkAutoUpdateComicFiles.UseVisualStyleBackColor = true;
            // 
            // txCoverFilter
            // 
            txCoverFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txCoverFilter.Location = new System.Drawing.Point(9, 112);
            txCoverFilter.Multiline = true;
            txCoverFilter.Name = "txCoverFilter";
            txCoverFilter.Size = new System.Drawing.Size(482, 54);
            txCoverFilter.TabIndex = 3;
            // 
            // grpLanguages
            // 
            grpLanguages.Controls.Add(btTranslate);
            grpLanguages.Controls.Add(labelLanguage);
            grpLanguages.Controls.Add(lbLanguages);
            grpLanguages.Dock = DockStyle.Top;
            grpLanguages.Location = new System.Drawing.Point(0, 0);
            grpLanguages.Name = "grpLanguages";
            grpLanguages.Size = new System.Drawing.Size(498, 372);
            grpLanguages.TabIndex = 7;
            grpLanguages.Text = "Languages";
            // 
            // btTranslate
            // 
            btTranslate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btTranslate.Location = new System.Drawing.Point(207, 339);
            btTranslate.Name = "btTranslate";
            btTranslate.Size = new System.Drawing.Size(284, 23);
            btTranslate.TabIndex = 12;
            btTranslate.Text = "Help localizing ComicRack...";
            btTranslate.UseVisualStyleBackColor = true;
            btTranslate.Click += btTranslate_Click;
            // 
            // labelLanguage
            // 
            labelLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelLanguage.Location = new System.Drawing.Point(6, 33);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new System.Drawing.Size(485, 35);
            labelLanguage.TabIndex = 11;
            labelLanguage.Text = "Select the User Interface language for ComicRack (ComicRack must be restarted for any change to take effect):";
            // 
            // lbLanguages
            // 
            lbLanguages.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbLanguages.DrawMode = DrawMode.OwnerDrawFixed;
            lbLanguages.FormattingEnabled = true;
            lbLanguages.ItemHeight = 15;
            lbLanguages.Location = new System.Drawing.Point(6, 75);
            lbLanguages.Name = "lbLanguages";
            lbLanguages.Size = new System.Drawing.Size(485, 259);
            lbLanguages.TabIndex = 0;
            lbLanguages.DrawItem += lbLanguages_DrawItem;
            // 
            // pageLibrary
            // 
            pageLibrary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pageLibrary.AutoScroll = true;
            pageLibrary.BorderStyle = BorderStyle.FixedSingle;
            pageLibrary.Controls.Add(grpVirtualTags);
            pageLibrary.Controls.Add(grpServerSettings);
            pageLibrary.Controls.Add(grpSharing);
            pageLibrary.Controls.Add(groupLibraryDisplay);
            pageLibrary.Controls.Add(grpScanning);
            pageLibrary.Controls.Add(groupComicFolders);
            pageLibrary.Location = new System.Drawing.Point(84, 8);
            pageLibrary.Name = "pageLibrary";
            pageLibrary.Size = new System.Drawing.Size(517, 408);
            pageLibrary.TabIndex = 10;
            // 
            // grpVirtualTags
            // 
            grpVirtualTags.Controls.Add(btnVTagsHelp);
            grpVirtualTags.Controls.Add(grpVtagConfig);
            grpVirtualTags.Controls.Add(lblVirtualTags);
            grpVirtualTags.Controls.Add(cbVirtualTags);
            grpVirtualTags.Dock = DockStyle.Top;
            grpVirtualTags.Location = new System.Drawing.Point(0, 1058);
            grpVirtualTags.Name = "grpVirtualTags";
            grpVirtualTags.Size = new System.Drawing.Size(498, 279);
            grpVirtualTags.TabIndex = 0;
            grpVirtualTags.Text = "Virtual Tags";
            // 
            // btnVTagsHelp
            // 
            btnVTagsHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVTagsHelp.FlatAppearance.BorderSize = 0;
            btnVTagsHelp.FlatStyle = FlatStyle.Flat;
            btnVTagsHelp.ForeColor = System.Drawing.Color.Transparent;
            btnVTagsHelp.Image = Properties.Resources.Help;
            btnVTagsHelp.Location = new System.Drawing.Point(466, 33);
            btnVTagsHelp.Name = "btnVTagsHelp";
            btnVTagsHelp.Size = new System.Drawing.Size(24, 24);
            btnVTagsHelp.TabIndex = 6;
            btnVTagsHelp.UseVisualStyleBackColor = true;
            btnVTagsHelp.Click += btnVTagsHelp_Click;
            // 
            // grpVtagConfig
            // 
            grpVtagConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpVtagConfig.Controls.Add(lblCaptionFormat);
            grpVtagConfig.Controls.Add(txtCaptionFormat);
            grpVtagConfig.Controls.Add(btInsertValue);
            grpVtagConfig.Controls.Add(lblVirtualTagDescription);
            grpVtagConfig.Controls.Add(lblVirtualTagName);
            grpVtagConfig.Controls.Add(txtVirtualTagDescription);
            grpVtagConfig.Controls.Add(txtVirtualTagName);
            grpVtagConfig.Controls.Add(chkVirtualTagEnable);
            grpVtagConfig.Controls.Add(lblCaptionText);
            grpVtagConfig.Controls.Add(lblCaptionSuffix);
            grpVtagConfig.Controls.Add(rtfVirtualTagCaption);
            grpVtagConfig.Controls.Add(lblFieldConfig);
            grpVtagConfig.Controls.Add(txtCaptionPrefix);
            grpVtagConfig.Controls.Add(lblCaptionPrefix);
            grpVtagConfig.Controls.Add(btnCaptionInsert);
            grpVtagConfig.Controls.Add(txtCaptionSuffix);
            grpVtagConfig.Location = new System.Drawing.Point(14, 63);
            grpVtagConfig.Name = "grpVtagConfig";
            grpVtagConfig.Size = new System.Drawing.Size(472, 199);
            grpVtagConfig.TabIndex = 5;
            grpVtagConfig.TabStop = false;
            grpVtagConfig.Text = "Config";
            // 
            // lblCaptionFormat
            // 
            lblCaptionFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCaptionFormat.AutoSize = true;
            lblCaptionFormat.Location = new System.Drawing.Point(275, 151);
            lblCaptionFormat.Name = "lblCaptionFormat";
            lblCaptionFormat.Size = new System.Drawing.Size(45, 15);
            lblCaptionFormat.TabIndex = 28;
            lblCaptionFormat.Text = "Format";
            lblCaptionFormat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btInsertValue
            // 
            btInsertValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btInsertValue.Image = Properties.Resources.SmallArrowDown;
            btInsertValue.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            btInsertValue.Location = new System.Drawing.Point(96, 164);
            btInsertValue.Name = "btInsertValue";
            btInsertValue.Size = new System.Drawing.Size(167, 23);
            btInsertValue.TabIndex = 26;
            btInsertValue.Text = "Choose Value";
            btInsertValue.UseVisualStyleBackColor = true;
            // 
            // lblVirtualTagDescription
            // 
            lblVirtualTagDescription.AutoSize = true;
            lblVirtualTagDescription.Location = new System.Drawing.Point(72, 49);
            lblVirtualTagDescription.Name = "lblVirtualTagDescription";
            lblVirtualTagDescription.Size = new System.Drawing.Size(76, 15);
            lblVirtualTagDescription.TabIndex = 25;
            lblVirtualTagDescription.Text = "Description : ";
            // 
            // lblVirtualTagName
            // 
            lblVirtualTagName.AutoSize = true;
            lblVirtualTagName.Location = new System.Drawing.Point(72, 23);
            lblVirtualTagName.Name = "lblVirtualTagName";
            lblVirtualTagName.Size = new System.Drawing.Size(48, 15);
            lblVirtualTagName.TabIndex = 24;
            lblVirtualTagName.Text = "Name : ";
            // 
            // txtVirtualTagDescription
            // 
            txtVirtualTagDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVirtualTagDescription.Location = new System.Drawing.Point(147, 46);
            txtVirtualTagDescription.Name = "txtVirtualTagDescription";
            txtVirtualTagDescription.Size = new System.Drawing.Size(308, 23);
            txtVirtualTagDescription.TabIndex = 2;
            txtVirtualTagDescription.Validated += VirtualTag_Validated;
            // 
            // txtVirtualTagName
            // 
            txtVirtualTagName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVirtualTagName.Location = new System.Drawing.Point(122, 20);
            txtVirtualTagName.Name = "txtVirtualTagName";
            txtVirtualTagName.Size = new System.Drawing.Size(333, 23);
            txtVirtualTagName.TabIndex = 1;
            txtVirtualTagName.Validated += UpdateVirtualTagsComboBox;
            // 
            // chkVirtualTagEnable
            // 
            chkVirtualTagEnable.AutoSize = true;
            chkVirtualTagEnable.Location = new System.Drawing.Point(10, 34);
            chkVirtualTagEnable.Name = "chkVirtualTagEnable";
            chkVirtualTagEnable.Size = new System.Drawing.Size(61, 19);
            chkVirtualTagEnable.TabIndex = 3;
            chkVirtualTagEnable.Text = "Enable";
            chkVirtualTagEnable.UseVisualStyleBackColor = true;
            chkVirtualTagEnable.Validated += UpdateVirtualTagsComboBox;
            // 
            // lblCaptionText
            // 
            lblCaptionText.AutoSize = true;
            lblCaptionText.Location = new System.Drawing.Point(7, 82);
            lblCaptionText.Name = "lblCaptionText";
            lblCaptionText.Size = new System.Drawing.Size(58, 15);
            lblCaptionText.TabIndex = 3;
            lblCaptionText.Text = "Caption : ";
            // 
            // lblCaptionSuffix
            // 
            lblCaptionSuffix.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCaptionSuffix.AutoSize = true;
            lblCaptionSuffix.Location = new System.Drawing.Point(349, 151);
            lblCaptionSuffix.Name = "lblCaptionSuffix";
            lblCaptionSuffix.Size = new System.Drawing.Size(37, 15);
            lblCaptionSuffix.TabIndex = 20;
            lblCaptionSuffix.Text = "Suffix";
            // 
            // rtfVirtualTagCaption
            // 
            rtfVirtualTagCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rtfVirtualTagCaption.BorderStyle = BorderStyle.FixedSingle;
            rtfVirtualTagCaption.DetectUrls = false;
            rtfVirtualTagCaption.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            rtfVirtualTagCaption.Location = new System.Drawing.Point(64, 79);
            rtfVirtualTagCaption.Name = "rtfVirtualTagCaption";
            rtfVirtualTagCaption.ScrollBars = RichTextBoxScrollBars.None;
            rtfVirtualTagCaption.Size = new System.Drawing.Size(391, 62);
            rtfVirtualTagCaption.TabIndex = 4;
            rtfVirtualTagCaption.Text = "";
            rtfVirtualTagCaption.Validated += VirtualTag_Validated;
            // 
            // lblFieldConfig
            // 
            lblFieldConfig.Anchor = AnchorStyles.None;
            lblFieldConfig.AutoSize = true;
            lblFieldConfig.Location = new System.Drawing.Point(165, 151);
            lblFieldConfig.Name = "lblFieldConfig";
            lblFieldConfig.Size = new System.Drawing.Size(32, 15);
            lblFieldConfig.TabIndex = 19;
            lblFieldConfig.Text = "Field";
            lblFieldConfig.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCaptionPrefix
            // 
            txtCaptionPrefix.BorderStyle = BorderStyle.FixedSingle;
            txtCaptionPrefix.Location = new System.Drawing.Point(10, 167);
            txtCaptionPrefix.Name = "txtCaptionPrefix";
            txtCaptionPrefix.Size = new System.Drawing.Size(80, 23);
            txtCaptionPrefix.TabIndex = 5;
            // 
            // lblCaptionPrefix
            // 
            lblCaptionPrefix.AutoSize = true;
            lblCaptionPrefix.Location = new System.Drawing.Point(34, 151);
            lblCaptionPrefix.Name = "lblCaptionPrefix";
            lblCaptionPrefix.Size = new System.Drawing.Size(37, 15);
            lblCaptionPrefix.TabIndex = 18;
            lblCaptionPrefix.Text = "Prefix";
            // 
            // btnCaptionInsert
            // 
            btnCaptionInsert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCaptionInsert.Location = new System.Drawing.Point(410, 164);
            btnCaptionInsert.Name = "btnCaptionInsert";
            btnCaptionInsert.Size = new System.Drawing.Size(51, 23);
            btnCaptionInsert.TabIndex = 8;
            btnCaptionInsert.Text = "Insert";
            btnCaptionInsert.UseVisualStyleBackColor = true;
            btnCaptionInsert.Click += btnCaptionInsert_Click;
            // 
            // txtCaptionSuffix
            // 
            txtCaptionSuffix.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCaptionSuffix.BorderStyle = BorderStyle.FixedSingle;
            txtCaptionSuffix.Location = new System.Drawing.Point(325, 167);
            txtCaptionSuffix.Name = "txtCaptionSuffix";
            txtCaptionSuffix.Size = new System.Drawing.Size(80, 23);
            txtCaptionSuffix.TabIndex = 7;
            // 
            // lblVirtualTags
            // 
            lblVirtualTags.AutoSize = true;
            lblVirtualTags.Location = new System.Drawing.Point(10, 39);
            lblVirtualTags.Name = "lblVirtualTags";
            lblVirtualTags.Size = new System.Drawing.Size(44, 15);
            lblVirtualTags.TabIndex = 1;
            lblVirtualTags.Text = "Tag # : ";
            // 
            // cbVirtualTags
            // 
            cbVirtualTags.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbVirtualTags.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVirtualTags.FormattingEnabled = true;
            cbVirtualTags.Location = new System.Drawing.Point(61, 36);
            cbVirtualTags.Name = "cbVirtualTags";
            cbVirtualTags.Size = new System.Drawing.Size(400, 23);
            cbVirtualTags.TabIndex = 0;
            cbVirtualTags.SelectedIndexChanged += cbVirtualTags_SelectedIndexChanged;
            // 
            // grpServerSettings
            // 
            grpServerSettings.Controls.Add(txPrivateListingPassword);
            grpServerSettings.Controls.Add(labelPrivateListPassword);
            grpServerSettings.Controls.Add(labelPublicServerAddress);
            grpServerSettings.Controls.Add(txPublicServerAddress);
            grpServerSettings.Dock = DockStyle.Top;
            grpServerSettings.Location = new System.Drawing.Point(0, 910);
            grpServerSettings.Name = "grpServerSettings";
            grpServerSettings.Size = new System.Drawing.Size(498, 148);
            grpServerSettings.TabIndex = 3;
            grpServerSettings.Text = "Server Settings";
            // 
            // txPrivateListingPassword
            // 
            txPrivateListingPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txPrivateListingPassword.Location = new System.Drawing.Point(12, 114);
            txPrivateListingPassword.Name = "txPrivateListingPassword";
            txPrivateListingPassword.Password = null;
            txPrivateListingPassword.Size = new System.Drawing.Size(379, 23);
            txPrivateListingPassword.TabIndex = 3;
            txPrivateListingPassword.UseSystemPasswordChar = true;
            // 
            // labelPrivateListPassword
            // 
            labelPrivateListPassword.AutoSize = true;
            labelPrivateListPassword.Location = new System.Drawing.Point(13, 96);
            labelPrivateListPassword.Name = "labelPrivateListPassword";
            labelPrivateListPassword.Size = new System.Drawing.Size(341, 15);
            labelPrivateListPassword.TabIndex = 2;
            labelPrivateListPassword.Text = "Password used to protect your private Internet Share list entries:";
            // 
            // labelPublicServerAddress
            // 
            labelPublicServerAddress.AutoSize = true;
            labelPublicServerAddress.Location = new System.Drawing.Point(14, 41);
            labelPublicServerAddress.Name = "labelPublicServerAddress";
            labelPublicServerAddress.Size = new System.Drawing.Size(408, 15);
            labelPublicServerAddress.TabIndex = 0;
            labelPublicServerAddress.Text = "External IP address of your server if ComicRack should not guess it correctly:";
            // 
            // txPublicServerAddress
            // 
            txPublicServerAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txPublicServerAddress.Location = new System.Drawing.Point(12, 60);
            txPublicServerAddress.Name = "txPublicServerAddress";
            txPublicServerAddress.Size = new System.Drawing.Size(379, 23);
            txPublicServerAddress.TabIndex = 1;
            // 
            // grpSharing
            // 
            grpSharing.Controls.Add(chkAutoConnectShares);
            grpSharing.Controls.Add(btRemoveShare);
            grpSharing.Controls.Add(btAddShare);
            grpSharing.Controls.Add(tabShares);
            grpSharing.Controls.Add(chkLookForShared);
            grpSharing.Dock = DockStyle.Top;
            grpSharing.Location = new System.Drawing.Point(0, 509);
            grpSharing.Name = "grpSharing";
            grpSharing.Size = new System.Drawing.Size(498, 401);
            grpSharing.TabIndex = 1;
            grpSharing.Text = "Sharing";
            // 
            // chkAutoConnectShares
            // 
            chkAutoConnectShares.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkAutoConnectShares.AutoSize = true;
            chkAutoConnectShares.Location = new System.Drawing.Point(245, 36);
            chkAutoConnectShares.Name = "chkAutoConnectShares";
            chkAutoConnectShares.Size = new System.Drawing.Size(146, 19);
            chkAutoConnectShares.TabIndex = 1;
            chkAutoConnectShares.Text = "Connect automatically";
            chkAutoConnectShares.UseVisualStyleBackColor = true;
            // 
            // btRemoveShare
            // 
            btRemoveShare.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRemoveShare.Location = new System.Drawing.Point(398, 89);
            btRemoveShare.Name = "btRemoveShare";
            btRemoveShare.Size = new System.Drawing.Size(92, 23);
            btRemoveShare.TabIndex = 4;
            btRemoveShare.Text = "Remove";
            btRemoveShare.UseVisualStyleBackColor = true;
            btRemoveShare.Click += btRmoveShare_Click;
            // 
            // btAddShare
            // 
            btAddShare.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAddShare.Location = new System.Drawing.Point(398, 61);
            btAddShare.Name = "btAddShare";
            btAddShare.Size = new System.Drawing.Size(92, 23);
            btAddShare.TabIndex = 3;
            btAddShare.Text = "Add Share";
            btAddShare.UseVisualStyleBackColor = true;
            btAddShare.Click += btAddShare_Click;
            // 
            // tabShares
            // 
            tabShares.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabShares.Location = new System.Drawing.Point(12, 59);
            tabShares.Name = "tabShares";
            tabShares.SelectedIndex = 0;
            tabShares.Size = new System.Drawing.Size(381, 336);
            tabShares.TabIndex = 2;
            // 
            // chkLookForShared
            // 
            chkLookForShared.AutoSize = true;
            chkLookForShared.Location = new System.Drawing.Point(12, 36);
            chkLookForShared.Name = "chkLookForShared";
            chkLookForShared.Size = new System.Drawing.Size(165, 19);
            chkLookForShared.TabIndex = 0;
            chkLookForShared.Text = "Look for local Book Shares";
            chkLookForShared.UseVisualStyleBackColor = true;
            // 
            // groupLibraryDisplay
            // 
            groupLibraryDisplay.Controls.Add(chkLibraryGaugesTotal);
            groupLibraryDisplay.Controls.Add(chkLibraryGaugesUnread);
            groupLibraryDisplay.Controls.Add(chkLibraryGaugesNumeric);
            groupLibraryDisplay.Controls.Add(chkLibraryGaugesNew);
            groupLibraryDisplay.Controls.Add(chkLibraryGauges);
            groupLibraryDisplay.Dock = DockStyle.Top;
            groupLibraryDisplay.Location = new System.Drawing.Point(0, 339);
            groupLibraryDisplay.Name = "groupLibraryDisplay";
            groupLibraryDisplay.Size = new System.Drawing.Size(498, 170);
            groupLibraryDisplay.TabIndex = 4;
            groupLibraryDisplay.Text = "Display";
            // 
            // chkLibraryGaugesTotal
            // 
            chkLibraryGaugesTotal.AutoSize = true;
            chkLibraryGaugesTotal.Location = new System.Drawing.Point(33, 111);
            chkLibraryGaugesTotal.Name = "chkLibraryGaugesTotal";
            chkLibraryGaugesTotal.Size = new System.Drawing.Size(120, 19);
            chkLibraryGaugesTotal.TabIndex = 1;
            chkLibraryGaugesTotal.Text = "For Total of Books";
            chkLibraryGaugesTotal.UseVisualStyleBackColor = true;
            // 
            // chkLibraryGaugesUnread
            // 
            chkLibraryGaugesUnread.AutoSize = true;
            chkLibraryGaugesUnread.Location = new System.Drawing.Point(33, 92);
            chkLibraryGaugesUnread.Name = "chkLibraryGaugesUnread";
            chkLibraryGaugesUnread.Size = new System.Drawing.Size(119, 19);
            chkLibraryGaugesUnread.TabIndex = 1;
            chkLibraryGaugesUnread.Text = "For Unread Books";
            chkLibraryGaugesUnread.UseVisualStyleBackColor = true;
            // 
            // chkLibraryGaugesNumeric
            // 
            chkLibraryGaugesNumeric.AutoSize = true;
            chkLibraryGaugesNumeric.Location = new System.Drawing.Point(33, 131);
            chkLibraryGaugesNumeric.Name = "chkLibraryGaugesNumeric";
            chkLibraryGaugesNumeric.Size = new System.Drawing.Size(225, 19);
            chkLibraryGaugesNumeric.TabIndex = 1;
            chkLibraryGaugesNumeric.Text = "Also show numbers and not only bars";
            chkLibraryGaugesNumeric.UseVisualStyleBackColor = true;
            // 
            // chkLibraryGaugesNew
            // 
            chkLibraryGaugesNew.AutoSize = true;
            chkLibraryGaugesNew.Location = new System.Drawing.Point(33, 72);
            chkLibraryGaugesNew.Name = "chkLibraryGaugesNew";
            chkLibraryGaugesNew.Size = new System.Drawing.Size(105, 19);
            chkLibraryGaugesNew.TabIndex = 1;
            chkLibraryGaugesNew.Text = "For New Books";
            chkLibraryGaugesNew.UseVisualStyleBackColor = true;
            // 
            // chkLibraryGauges
            // 
            chkLibraryGauges.AutoSize = true;
            chkLibraryGauges.Location = new System.Drawing.Point(12, 42);
            chkLibraryGauges.Name = "chkLibraryGauges";
            chkLibraryGauges.Size = new System.Drawing.Size(136, 19);
            chkLibraryGauges.TabIndex = 0;
            chkLibraryGauges.Text = "Enable Live Counters";
            chkLibraryGauges.UseVisualStyleBackColor = true;
            chkLibraryGauges.CheckedChanged += chkLibraryGauges_CheckedChanged;
            // 
            // grpScanning
            // 
            grpScanning.Controls.Add(chkDontAddRemovedFiles);
            grpScanning.Controls.Add(chkAutoRemoveMissing);
            grpScanning.Controls.Add(lblScan);
            grpScanning.Controls.Add(btScan);
            grpScanning.Dock = DockStyle.Top;
            grpScanning.Location = new System.Drawing.Point(0, 203);
            grpScanning.Name = "grpScanning";
            grpScanning.Size = new System.Drawing.Size(498, 136);
            grpScanning.TabIndex = 0;
            grpScanning.Text = "Scanning";
            // 
            // chkDontAddRemovedFiles
            // 
            chkDontAddRemovedFiles.AutoSize = true;
            chkDontAddRemovedFiles.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            chkDontAddRemovedFiles.Location = new System.Drawing.Point(12, 58);
            chkDontAddRemovedFiles.Name = "chkDontAddRemovedFiles";
            chkDontAddRemovedFiles.Size = new System.Drawing.Size(365, 19);
            chkDontAddRemovedFiles.TabIndex = 1;
            chkDontAddRemovedFiles.Text = "Files manually removed from the Library will not be added again";
            chkDontAddRemovedFiles.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            chkDontAddRemovedFiles.UseVisualStyleBackColor = true;
            // 
            // chkAutoRemoveMissing
            // 
            chkAutoRemoveMissing.AutoSize = true;
            chkAutoRemoveMissing.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            chkAutoRemoveMissing.Location = new System.Drawing.Point(12, 35);
            chkAutoRemoveMissing.Name = "chkAutoRemoveMissing";
            chkAutoRemoveMissing.Size = new System.Drawing.Size(345, 19);
            chkAutoRemoveMissing.TabIndex = 0;
            chkAutoRemoveMissing.Text = "Automatically remove missing files from Library during Scan";
            chkAutoRemoveMissing.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            chkAutoRemoveMissing.UseVisualStyleBackColor = true;
            // 
            // lblScan
            // 
            lblScan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblScan.Location = new System.Drawing.Point(9, 83);
            lblScan.Name = "lblScan";
            lblScan.Size = new System.Drawing.Size(480, 43);
            lblScan.TabIndex = 8;
            lblScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btScan
            // 
            btScan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btScan.Location = new System.Drawing.Point(403, 33);
            btScan.Name = "btScan";
            btScan.Size = new System.Drawing.Size(88, 23);
            btScan.TabIndex = 2;
            btScan.Text = "Scan";
            btScan.UseVisualStyleBackColor = true;
            btScan.Click += btScan_Click;
            // 
            // groupComicFolders
            // 
            groupComicFolders.Controls.Add(btOpenFolder);
            groupComicFolders.Controls.Add(btChangeFolder);
            groupComicFolders.Controls.Add(lbPaths);
            groupComicFolders.Controls.Add(labelWatchedFolders);
            groupComicFolders.Controls.Add(btRemoveFolder);
            groupComicFolders.Controls.Add(btAddFolder);
            groupComicFolders.Dock = DockStyle.Top;
            groupComicFolders.Location = new System.Drawing.Point(0, 0);
            groupComicFolders.Name = "groupComicFolders";
            groupComicFolders.Size = new System.Drawing.Size(498, 203);
            groupComicFolders.TabIndex = 0;
            groupComicFolders.Text = "Book Folders";
            // 
            // btOpenFolder
            // 
            btOpenFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btOpenFolder.Location = new System.Drawing.Point(400, 134);
            btOpenFolder.Name = "btOpenFolder";
            btOpenFolder.Size = new System.Drawing.Size(89, 23);
            btOpenFolder.TabIndex = 4;
            btOpenFolder.Text = "Open";
            btOpenFolder.UseVisualStyleBackColor = true;
            btOpenFolder.Click += btOpenFolder_Click;
            // 
            // btChangeFolder
            // 
            btChangeFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btChangeFolder.Location = new System.Drawing.Point(400, 66);
            btChangeFolder.Name = "btChangeFolder";
            btChangeFolder.Size = new System.Drawing.Size(89, 23);
            btChangeFolder.TabIndex = 2;
            btChangeFolder.Text = "&Change...";
            btChangeFolder.UseVisualStyleBackColor = true;
            btChangeFolder.Click += btChangeFolder_Click;
            // 
            // lbPaths
            // 
            lbPaths.AllowDrop = true;
            lbPaths.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbPaths.FormattingEnabled = true;
            lbPaths.IntegralHeight = false;
            lbPaths.Location = new System.Drawing.Point(12, 37);
            lbPaths.Name = "lbPaths";
            lbPaths.Size = new System.Drawing.Size(377, 120);
            lbPaths.TabIndex = 0;
            lbPaths.DrawItemText += lbPaths_DrawItemText;
            lbPaths.DrawItem += lbPaths_DrawItem;
            lbPaths.SelectedIndexChanged += lbPaths_SelectedIndexChanged;
            lbPaths.DragDrop += lbPaths_DragDrop;
            lbPaths.DragOver += lbPaths_DragOver;
            // 
            // labelWatchedFolders
            // 
            labelWatchedFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelWatchedFolders.Location = new System.Drawing.Point(9, 163);
            labelWatchedFolders.Name = "labelWatchedFolders";
            labelWatchedFolders.Size = new System.Drawing.Size(480, 26);
            labelWatchedFolders.TabIndex = 0;
            labelWatchedFolders.Text = "Checked folders will be watched for changes (rename, move) while the program is running.";
            labelWatchedFolders.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btRemoveFolder
            // 
            btRemoveFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRemoveFolder.Location = new System.Drawing.Point(400, 95);
            btRemoveFolder.Name = "btRemoveFolder";
            btRemoveFolder.Size = new System.Drawing.Size(89, 23);
            btRemoveFolder.TabIndex = 3;
            btRemoveFolder.Text = "&Remove";
            btRemoveFolder.UseVisualStyleBackColor = true;
            btRemoveFolder.Click += btRemoveFolder_Click;
            // 
            // btAddFolder
            // 
            btAddFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAddFolder.Location = new System.Drawing.Point(400, 37);
            btAddFolder.Name = "btAddFolder";
            btAddFolder.Size = new System.Drawing.Size(89, 23);
            btAddFolder.TabIndex = 1;
            btAddFolder.Text = "&Add...";
            btAddFolder.UseVisualStyleBackColor = true;
            btAddFolder.Click += btAddFolder_Click;
            // 
            // memCacheUpate
            // 
            memCacheUpate.Enabled = true;
            memCacheUpate.Interval = 1000;
            memCacheUpate.Tick += memCacheUpate_Tick;
            // 
            // pageScripts
            // 
            pageScripts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pageScripts.AutoScroll = true;
            pageScripts.BorderStyle = BorderStyle.FixedSingle;
            pageScripts.Controls.Add(grpScriptSettings);
            pageScripts.Controls.Add(grpScripts);
            pageScripts.Controls.Add(grpPackages);
            pageScripts.Location = new System.Drawing.Point(84, 8);
            pageScripts.Name = "pageScripts";
            pageScripts.Size = new System.Drawing.Size(517, 408);
            pageScripts.TabIndex = 11;
            // 
            // grpScriptSettings
            // 
            grpScriptSettings.Controls.Add(btAddLibraryFolder);
            grpScriptSettings.Controls.Add(chkDisableScripting);
            grpScriptSettings.Controls.Add(labelScriptPaths);
            grpScriptSettings.Controls.Add(txLibraries);
            grpScriptSettings.Dock = DockStyle.Top;
            grpScriptSettings.Location = new System.Drawing.Point(0, 752);
            grpScriptSettings.Name = "grpScriptSettings";
            grpScriptSettings.Size = new System.Drawing.Size(498, 192);
            grpScriptSettings.TabIndex = 5;
            grpScriptSettings.Text = "Script Settings";
            // 
            // btAddLibraryFolder
            // 
            btAddLibraryFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAddLibraryFolder.Location = new System.Drawing.Point(369, 163);
            btAddLibraryFolder.Name = "btAddLibraryFolder";
            btAddLibraryFolder.Size = new System.Drawing.Size(121, 23);
            btAddLibraryFolder.TabIndex = 3;
            btAddLibraryFolder.Text = "Add Folder...";
            btAddLibraryFolder.UseVisualStyleBackColor = true;
            btAddLibraryFolder.Click += btAddLibraryFolder_Click;
            // 
            // chkDisableScripting
            // 
            chkDisableScripting.AutoSize = true;
            chkDisableScripting.Location = new System.Drawing.Point(9, 39);
            chkDisableScripting.Name = "chkDisableScripting";
            chkDisableScripting.Size = new System.Drawing.Size(117, 19);
            chkDisableScripting.TabIndex = 0;
            chkDisableScripting.Text = "Disable all Scripts";
            chkDisableScripting.UseVisualStyleBackColor = true;
            // 
            // labelScriptPaths
            // 
            labelScriptPaths.Location = new System.Drawing.Point(6, 60);
            labelScriptPaths.Name = "labelScriptPaths";
            labelScriptPaths.Size = new System.Drawing.Size(478, 29);
            labelScriptPaths.TabIndex = 1;
            labelScriptPaths.Text = "Semicolon separated list of library paths for scripts (e.g. python libraries):";
            labelScriptPaths.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txLibraries
            // 
            txLibraries.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txLibraries.Location = new System.Drawing.Point(7, 92);
            txLibraries.Multiline = true;
            txLibraries.Name = "txLibraries";
            txLibraries.Size = new System.Drawing.Size(482, 63);
            txLibraries.TabIndex = 2;
            // 
            // grpScripts
            // 
            grpScripts.Controls.Add(chkHideSampleScripts);
            grpScripts.Controls.Add(btConfigScript);
            grpScripts.Controls.Add(lvScripts);
            grpScripts.Dock = DockStyle.Top;
            grpScripts.Location = new System.Drawing.Point(0, 378);
            grpScripts.Name = "grpScripts";
            grpScripts.Size = new System.Drawing.Size(498, 374);
            grpScripts.TabIndex = 4;
            grpScripts.Text = "Available Scripts";
            // 
            // chkHideSampleScripts
            // 
            chkHideSampleScripts.AutoSize = true;
            chkHideSampleScripts.Location = new System.Drawing.Point(9, 345);
            chkHideSampleScripts.Name = "chkHideSampleScripts";
            chkHideSampleScripts.Size = new System.Drawing.Size(130, 19);
            chkHideSampleScripts.TabIndex = 8;
            chkHideSampleScripts.Text = "Hide sample Scripts";
            chkHideSampleScripts.UseVisualStyleBackColor = true;
            chkHideSampleScripts.CheckedChanged += chkHideSampleScripts_CheckedChanged;
            // 
            // btConfigScript
            // 
            btConfigScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btConfigScript.Enabled = false;
            btConfigScript.Location = new System.Drawing.Point(398, 339);
            btConfigScript.Name = "btConfigScript";
            btConfigScript.Size = new System.Drawing.Size(87, 23);
            btConfigScript.TabIndex = 7;
            btConfigScript.Text = "Configure...";
            btConfigScript.UseVisualStyleBackColor = true;
            btConfigScript.Click += btConfigScript_Click;
            // 
            // lvScripts
            // 
            lvScripts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvScripts.CheckBoxes = true;
            lvScripts.Columns.AddRange(new ColumnHeader[] { chScriptName, chScriptPackage });
            lvScripts.FullRowSelect = true;
            lvScripts.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvScripts.Location = new System.Drawing.Point(9, 42);
            lvScripts.MultiSelect = false;
            lvScripts.Name = "lvScripts";
            lvScripts.ShowItemToolTips = true;
            lvScripts.Size = new System.Drawing.Size(476, 291);
            lvScripts.SmallImageList = imageList;
            lvScripts.Sorting = SortOrder.Ascending;
            lvScripts.TabIndex = 6;
            lvScripts.UseCompatibleStateImageBehavior = false;
            lvScripts.View = View.Details;
            lvScripts.ItemChecked += lvScripts_ItemChecked;
            lvScripts.SelectedIndexChanged += lvScripts_SelectedIndexChanged;
            // 
            // chScriptName
            // 
            chScriptName.Text = "Name";
            chScriptName.Width = 250;
            // 
            // chScriptPackage
            // 
            chScriptPackage.Text = "Package";
            chScriptPackage.Width = 190;
            // 
            // grpPackages
            // 
            grpPackages.Controls.Add(btRemovePackage);
            grpPackages.Controls.Add(btInstallPackage);
            grpPackages.Controls.Add(lvPackages);
            grpPackages.Dock = DockStyle.Top;
            grpPackages.Location = new System.Drawing.Point(0, 0);
            grpPackages.Name = "grpPackages";
            grpPackages.Size = new System.Drawing.Size(498, 378);
            grpPackages.TabIndex = 13;
            grpPackages.Text = "Script Packages";
            // 
            // btRemovePackage
            // 
            btRemovePackage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRemovePackage.Location = new System.Drawing.Point(398, 344);
            btRemovePackage.Name = "btRemovePackage";
            btRemovePackage.Size = new System.Drawing.Size(86, 23);
            btRemovePackage.TabIndex = 2;
            btRemovePackage.Text = "Remove";
            btRemovePackage.UseVisualStyleBackColor = true;
            btRemovePackage.Click += btRemovePackage_Click;
            // 
            // btInstallPackage
            // 
            btInstallPackage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btInstallPackage.Location = new System.Drawing.Point(306, 344);
            btInstallPackage.Name = "btInstallPackage";
            btInstallPackage.Size = new System.Drawing.Size(86, 23);
            btInstallPackage.TabIndex = 1;
            btInstallPackage.Text = "Install...";
            btInstallPackage.UseVisualStyleBackColor = true;
            btInstallPackage.Click += btInstallPackage_Click;
            // 
            // lvPackages
            // 
            lvPackages.AllowDrop = true;
            lvPackages.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvPackages.Columns.AddRange(new ColumnHeader[] { chPackageName, chPackageAuthor, chPackageDescription });
            listViewGroup1.Header = "Installed";
            listViewGroup1.Name = "packageGroupInstalled";
            listViewGroup2.Header = "To be removed (requires restart)";
            listViewGroup2.Name = "packageGroupRemove";
            listViewGroup3.Header = "To be installed (requires restart)";
            listViewGroup3.Name = "packageGroupInstall";
            lvPackages.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2, listViewGroup3 });
            lvPackages.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvPackages.LargeImageList = packageImageList;
            lvPackages.Location = new System.Drawing.Point(16, 37);
            lvPackages.Name = "lvPackages";
            lvPackages.ShowItemToolTips = true;
            lvPackages.Size = new System.Drawing.Size(468, 301);
            lvPackages.SmallImageList = packageImageList;
            lvPackages.Sorting = SortOrder.Ascending;
            lvPackages.TabIndex = 0;
            lvPackages.UseCompatibleStateImageBehavior = false;
            lvPackages.View = View.Details;
            lvPackages.DragDrop += lvPackages_DragDrop;
            lvPackages.DragOver += lvPackages_DragOver;
            lvPackages.DoubleClick += lvPackages_DoubleClick;
            // 
            // chPackageName
            // 
            chPackageName.Text = "Package";
            chPackageName.Width = 130;
            // 
            // chPackageAuthor
            // 
            chPackageAuthor.Text = "Author";
            chPackageAuthor.Width = 89;
            // 
            // chPackageDescription
            // 
            chPackageDescription.Text = "Description";
            chPackageDescription.Width = 217;
            // 
            // packageImageList
            // 
            packageImageList.ColorDepth = ColorDepth.Depth32Bit;
            packageImageList.ImageSize = new System.Drawing.Size(32, 32);
            packageImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabReader
            // 
            tabReader.Appearance = Appearance.Button;
            tabReader.AutoEllipsis = true;
            tabReader.Image = Properties.Resources.ReaderPref;
            tabReader.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            tabReader.Location = new System.Drawing.Point(3, 7);
            tabReader.Name = "tabReader";
            tabReader.Size = new System.Drawing.Size(75, 56);
            tabReader.TabIndex = 13;
            tabReader.Text = "Reader";
            tabReader.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            tabReader.UseVisualStyleBackColor = true;
            tabReader.CheckedChanged += chkAdvanced_CheckedChanged;
            // 
            // tabLibraries
            // 
            tabLibraries.Appearance = Appearance.Button;
            tabLibraries.AutoEllipsis = true;
            tabLibraries.Image = Properties.Resources.LibraryPref;
            tabLibraries.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            tabLibraries.Location = new System.Drawing.Point(3, 66);
            tabLibraries.Name = "tabLibraries";
            tabLibraries.Size = new System.Drawing.Size(75, 56);
            tabLibraries.TabIndex = 14;
            tabLibraries.Text = "Libraries";
            tabLibraries.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            tabLibraries.UseVisualStyleBackColor = true;
            tabLibraries.CheckedChanged += chkAdvanced_CheckedChanged;
            // 
            // tabBehavior
            // 
            tabBehavior.Appearance = Appearance.Button;
            tabBehavior.AutoEllipsis = true;
            tabBehavior.Image = Properties.Resources.BehaviorPref;
            tabBehavior.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            tabBehavior.Location = new System.Drawing.Point(3, 126);
            tabBehavior.Name = "tabBehavior";
            tabBehavior.Size = new System.Drawing.Size(75, 56);
            tabBehavior.TabIndex = 15;
            tabBehavior.Text = "Behavior";
            tabBehavior.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            tabBehavior.UseVisualStyleBackColor = true;
            tabBehavior.CheckedChanged += chkAdvanced_CheckedChanged;
            // 
            // tabScripts
            // 
            tabScripts.Appearance = Appearance.Button;
            tabScripts.AutoEllipsis = true;
            tabScripts.Image = Properties.Resources.ScriptingPref;
            tabScripts.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            tabScripts.Location = new System.Drawing.Point(3, 187);
            tabScripts.Name = "tabScripts";
            tabScripts.Size = new System.Drawing.Size(75, 56);
            tabScripts.TabIndex = 16;
            tabScripts.Text = "Scripts";
            tabScripts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            tabScripts.UseVisualStyleBackColor = true;
            tabScripts.CheckedChanged += chkAdvanced_CheckedChanged;
            // 
            // tabAdvanced
            // 
            tabAdvanced.Appearance = Appearance.Button;
            tabAdvanced.AutoEllipsis = true;
            tabAdvanced.Image = Properties.Resources.AdvancedPref;
            tabAdvanced.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            tabAdvanced.Location = new System.Drawing.Point(3, 248);
            tabAdvanced.Name = "tabAdvanced";
            tabAdvanced.Size = new System.Drawing.Size(75, 56);
            tabAdvanced.TabIndex = 17;
            tabAdvanced.Text = "Advanced";
            tabAdvanced.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            tabAdvanced.UseVisualStyleBackColor = true;
            tabAdvanced.CheckedChanged += chkAdvanced_CheckedChanged;
            // 
            // PreferencesDialog
            // 
            AcceptButton = btOK;
            CancelButton = btCancel;
            ClientSize = new System.Drawing.Size(610, 453);
            Controls.Add(pageAdvanced);
            Controls.Add(tabAdvanced);
            Controls.Add(tabScripts);
            Controls.Add(tabBehavior);
            Controls.Add(tabLibraries);
            Controls.Add(tabReader);
            Controls.Add(pageReader);
            Controls.Add(pageLibrary);
            Controls.Add(pageScripts);
            Controls.Add(pageBehavior);
            Controls.Add(btApply);
            Controls.Add(btCancel);
            Controls.Add(btOK);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(626, 492);
            Name = "PreferencesDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Preferences";
            pageReader.ResumeLayout(false);
            groupHardwareAcceleration.ResumeLayout(false);
            groupHardwareAcceleration.PerformLayout();
            grpMouse.ResumeLayout(false);
            grpMouse.PerformLayout();
            groupOverlays.ResumeLayout(false);
            groupOverlays.PerformLayout();
            panelReaderOverlays.ResumeLayout(false);
            grpKeyboard.ResumeLayout(false);
            cmKeyboardLayout.ResumeLayout(false);
            grpDisplay.ResumeLayout(false);
            grpDisplay.PerformLayout();
            pageAdvanced.ResumeLayout(false);
            grpWirelessSetup.ResumeLayout(false);
            grpWirelessSetup.PerformLayout();
            grpIntegration.ResumeLayout(false);
            grpIntegration.PerformLayout();
            groupMessagesAndSocial.ResumeLayout(false);
            groupMemory.ResumeLayout(false);
            grpMaximumMemoryUsage.ResumeLayout(false);
            grpMaximumMemoryUsage.PerformLayout();
            grpMemoryCache.ResumeLayout(false);
            grpMemoryCache.PerformLayout();
            ((ISupportInitialize)numMemPageCount).EndInit();
            ((ISupportInitialize)numMemThumbSize).EndInit();
            grpDiskCache.ResumeLayout(false);
            grpDiskCache.PerformLayout();
            ((ISupportInitialize)numPageCacheSize).EndInit();
            ((ISupportInitialize)numInternetCacheSize).EndInit();
            ((ISupportInitialize)numThumbnailCacheSize).EndInit();
            grpDatabaseBackup.ResumeLayout(false);
            groupOtherComics.ResumeLayout(false);
            groupOtherComics.PerformLayout();
            grpLanguages.ResumeLayout(false);
            pageLibrary.ResumeLayout(false);
            grpVirtualTags.ResumeLayout(false);
            grpVirtualTags.PerformLayout();
            grpVtagConfig.ResumeLayout(false);
            grpVtagConfig.PerformLayout();
            grpServerSettings.ResumeLayout(false);
            grpServerSettings.PerformLayout();
            grpSharing.ResumeLayout(false);
            grpSharing.PerformLayout();
            groupLibraryDisplay.ResumeLayout(false);
            groupLibraryDisplay.PerformLayout();
            grpScanning.ResumeLayout(false);
            grpScanning.PerformLayout();
            groupComicFolders.ResumeLayout(false);
            pageScripts.ResumeLayout(false);
            grpScriptSettings.ResumeLayout(false);
            grpScriptSettings.PerformLayout();
            grpScripts.ResumeLayout(false);
            grpScripts.PerformLayout();
            grpPackages.ResumeLayout(false);
            ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				activeTab = tabButtons.FindIndex((CheckBox c) => c.Checked);
				Program.Scanner.ScanNotify -= DatabaseScanNotify;
				IdleProcess.Idle -= ApplicationIdle;
				Program.InternetCache.SizeChanged -= UpdateDiskCacheStatus;
				Program.ImagePool.Pages.DiskCache.SizeChanged -= UpdateDiskCacheStatus;
				Program.ImagePool.Thumbs.DiskCache.SizeChanged -= UpdateDiskCacheStatus;
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private CheckBox chkHighQualityDisplay;
		private CheckBox chkEnableThumbnailCache;
		private NumericUpDown numThumbnailCacheSize;
		private Button btClearThumbnailCache;
		private Button btRemoveFolder;
		private Button btAddFolder;
		private CheckedListBoxEx lbPaths;
		private Button btOK;
		private Button btCancel;
		private Label lblScan;
		private Button btScan;
		private CheckBox chkAutoRemoveMissing;
		private Button btResetMessages;
		private Label labelReshowHidden;
		private Label labelWatchedFolders;
		private CheckedListBox lbFormats;
		private Label labelCheckedFormats;
		private CheckBox chkOverwriteAssociations;
		private CheckBox chkEnablePageCache;
		private Button btClearPageCache;
		private NumericUpDown numPageCacheSize;
		private CheckBox chkLookForShared;
		private Button btChangeFolder;
		private CheckBox chkAutoUpdateComicFiles;
		private TrackBarLite tbContrast;
		private TrackBarLite tbBrightness;
		private TrackBarLite tbSaturation;
		private Label labelContrast;
		private Label labelBrightness;
		private Label labelSaturation;
		private Button btApply;
		private CheckBox chkAutoContrast;
		private TextBox txCoverFilter;
		private Label labelExcludeCover;
		private ImageList imageList;
		private Button btResetColor;
		private TrackBarLite tbOverlayScaling;
		private Label labelOverlaySize;
		private ToolTip toolTip;
		private ListBox lbLanguages;
		private Label labelLanguage;
		private KeyboardShortcutEditor keyboardShortcutEditor;
		private Label labelMemThumbSize;
		private NumericUpDown numMemPageCount;
		private Label labelMemPageCount;
		private NumericUpDown numMemThumbSize;
		private CheckBox chkMemPageOptimized;
		private CheckBox chkMemThumbOptimized;
		private Button btBackupDatabase;
		private Button btRestoreDatabase;
		private Button btTranslate;
		private CheckBox chkEnableHardware;
		private CheckBox chkShowStatusOverlay;
		private CheckBox chkShowNavigationOverlay;
		private CheckBox chkShowVisiblePartOverlay;
		private CheckBox chkShowCurrentPageOverlay;
		private CheckBox chkEnableDisplayChangeAnimation;
		private CheckBox chkEnableInertialMouseScrolling;
		private Panel pageBehavior;
		private Label lblMouseWheel;
		private Label lblFast;
		private Label lblSlow;
		private TrackBarLite tbMouseWheel;
		private Label lblPageCacheUsage;
		private Label lblThumbCacheUsage;
		private CheckBox chkUpdateComicFiles;
		private CheckBox chkAnamorphicScaling;
		private Panel pageReader;
		private CollapsibleGroupBox groupOverlays;
		private CollapsibleGroupBox grpDisplay;
		private CollapsibleGroupBox grpMouse;
		private CollapsibleGroupBox grpKeyboard;
		private CollapsibleGroupBox groupHardwareAcceleration;
		private Panel pageAdvanced;
		private CollapsibleGroupBox grpLanguages;
		private CollapsibleGroupBox groupMessagesAndSocial;
		private CollapsibleGroupBox groupOtherComics;
		private CollapsibleGroupBox grpDatabaseBackup;
		private CollapsibleGroupBox groupMemory;
		private CollapsibleGroupBox grpIntegration;
		private Panel pageLibrary;
		private CollapsibleGroupBox grpSharing;
		private CollapsibleGroupBox groupComicFolders;
		private CollapsibleGroupBox grpScanning;
		private CheckBox chkEnableSoftwareFiltering;
		private Label lblPageMemCacheUsage;
		private Label lblThumbMemCacheUsage;
		private Timer memCacheUpate;
		private CheckBox chkShowPageNames;
		private CheckBox chkEnableHardwareFiltering;
		private Panel pageScripts;
		private CollapsibleGroupBox grpScripts;
		private ListView lvScripts;
		private ColumnHeader chScriptName;
		private ColumnHeader chScriptPackage;
		private CheckBox chkDisableScripting;
		private CollapsibleGroupBox grpScriptSettings;
		private Button btAddLibraryFolder;
		private Label labelScriptPaths;
		private TextBox txLibraries;
		private CollapsibleGroupBox grpPackages;
		private Button btRemovePackage;
		private Button btInstallPackage;
		private ListView lvPackages;
		private ImageList packageImageList;
		private ColumnHeader chPackageName;
		private ColumnHeader chPackageAuthor;
		private ColumnHeader chPackageDescription;
		private Button btAssociateExtensions;
		private Label lblInternetCacheUsage;
		private CheckBox chkEnableInternetCache;
		private NumericUpDown numInternetCacheSize;
		private Button btClearInternetCache;
		private CollapsibleGroupBox grpServerSettings;
		private TextBox txPublicServerAddress;
		private Label labelPublicServerAddress;
		private TabControl tabShares;
		private Button btRemoveShare;
		private Button btAddShare;
		private PasswordTextBox txPrivateListingPassword;
		private Label labelPrivateListPassword;
		private Label labelSharpening;
		private TrackBarLite tbSharpening;
		private CheckBox chkDontAddRemovedFiles;
		private Button btConfigScript;
		private Button btOpenFolder;
		private CheckBox chkAutoConnectShares;
		private Button btExportKeyboard;
		private SplitButton btImportKeyboard;
		private ContextMenuStrip cmKeyboardLayout;
		private ToolStripMenuItem miDefaultKeyboardLayout;
		private ToolStripSeparator toolStripMenuItem1;
		private ComboBox cbNavigationOverlayPosition;
		private Label labelNavigationOverlayPosition;
		private Panel panelReaderOverlays;
		private Label labelVisiblePartOverlay;
		private Label labelNavigationOverlay;
		private Label labelStatusOverlay;
		private Label labelPageOverlay;
		private CheckBox chkHideSampleScripts;
		private CheckBox chkSmoothAutoScrolling;
		private TrackBarLite tbGamma;
		private Label labelGamma;
		private GroupBox grpDiskCache;
		private GroupBox grpMaximumMemoryUsage;
		private Label lblMaximumMemoryUsageValue;
		private TrackBarLite tbMaximumMemoryUsage;
		private Label lblMaximumMemoryUsage;
		private GroupBox grpMemoryCache;
		private CollapsibleGroupBox groupLibraryDisplay;
		private CheckBox chkLibraryGaugesTotal;
		private CheckBox chkLibraryGaugesUnread;
		private CheckBox chkLibraryGaugesNumeric;
		private CheckBox chkLibraryGaugesNew;
		private CheckBox chkLibraryGauges;
		private CheckBox tabReader;
		private CheckBox tabLibraries;
		private CheckBox tabBehavior;
		private CheckBox tabScripts;
		private CheckBox tabAdvanced;
		private CollapsibleGroupBox grpWirelessSetup;
		private Label lblWifiStatus;
		private Label lblWifiAddresses;
		private TextBox txWifiAddresses;
		private Button btTestWifi;
		private static int activeTab = -1;
		private readonly List<CheckBox> tabButtons = new List<CheckBox>();
        private CollapsibleGroupBox grpVirtualTags;
        private Label lblVirtualTags;
        private ComboBox cbVirtualTags;
        private GroupBox grpVtagConfig;
        private Label lblCaptionText;
        private Label lblCaptionSuffix;
        private RichTextBox rtfVirtualTagCaption;
        private Label lblFieldConfig;
        private TextBox txtCaptionPrefix;
        private Label lblCaptionPrefix;
        private Button btnCaptionInsert;
        private TextBox txtCaptionSuffix;
        private CheckBox chkVirtualTagEnable;
        private Label lblVirtualTagDescription;
        private Label lblVirtualTagName;
        private TextBox txtVirtualTagDescription;
        private TextBox txtVirtualTagName;
        private Button btInsertValue;
        private TextBox txtCaptionFormat;
        private Label lblCaptionFormat;
		private Button btnVTagsHelp;
	}
}
