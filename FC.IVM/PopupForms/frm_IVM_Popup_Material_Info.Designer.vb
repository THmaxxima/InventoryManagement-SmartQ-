Namespace PopupForms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frm_IVM_Popup_Material_Info
        Inherits DevExpress.XtraEditors.XtraForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_IVM_Popup_Material_Info))
            Me.GridMatInfo = New DevExpress.XtraGrid.GridControl()
            Me.GridViewMatInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
            Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
            Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
            Me.lblAreaInfo = New DevExpress.XtraLayout.SimpleLabelItem()
            Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LookUpEditGroupArea = New DevExpress.XtraEditors.LookUpEdit()
            CType(Me.GridMatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridViewMatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.LayoutControl1.SuspendLayout()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblAreaInfo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LookUpEditGroupArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GridMatInfo
            '
            Me.GridMatInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.GridMatInfo.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
            Me.GridMatInfo.EmbeddedNavigator.Buttons.Edit.Visible = False
            Me.GridMatInfo.Location = New System.Drawing.Point(19, 65)
            Me.GridMatInfo.MainView = Me.GridViewMatInfo
            Me.GridMatInfo.Name = "GridMatInfo"
            Me.GridMatInfo.Size = New System.Drawing.Size(829, 288)
            Me.GridMatInfo.TabIndex = 0
            Me.GridMatInfo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewMatInfo})
            '
            'GridViewMatInfo
            '
            Me.GridViewMatInfo.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.ActiveBorder
            Me.GridViewMatInfo.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.ActiveBorder
            Me.GridViewMatInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Black
            Me.GridViewMatInfo.Appearance.FocusedRow.Options.UseBackColor = True
            Me.GridViewMatInfo.Appearance.FocusedRow.Options.UseBorderColor = True
            Me.GridViewMatInfo.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GridViewMatInfo.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
            Me.GridViewMatInfo.Appearance.FooterPanel.Options.UseFont = True
            Me.GridViewMatInfo.Appearance.FooterPanel.Options.UseForeColor = True
            Me.GridViewMatInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GridViewMatInfo.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
            Me.GridViewMatInfo.Appearance.HeaderPanel.Options.UseFont = True
            Me.GridViewMatInfo.Appearance.HeaderPanel.Options.UseForeColor = True
            Me.GridViewMatInfo.Appearance.HeaderPanel.Options.UseTextOptions = True
            Me.GridViewMatInfo.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me.GridViewMatInfo.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GridViewMatInfo.Appearance.Row.ForeColor = System.Drawing.Color.Black
            Me.GridViewMatInfo.Appearance.Row.Options.UseFont = True
            Me.GridViewMatInfo.Appearance.Row.Options.UseForeColor = True
            Me.GridViewMatInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn5, Me.GridColumn3, Me.GridColumn4})
            Me.GridViewMatInfo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
            Me.GridViewMatInfo.GridControl = Me.GridMatInfo
            Me.GridViewMatInfo.Name = "GridViewMatInfo"
            Me.GridViewMatInfo.OptionsBehavior.ReadOnly = True
            Me.GridViewMatInfo.OptionsCustomization.AllowFilter = False
            Me.GridViewMatInfo.OptionsCustomization.AllowQuickHideColumns = False
            Me.GridViewMatInfo.OptionsCustomization.AllowSort = False
            Me.GridViewMatInfo.OptionsDetail.ShowDetailTabs = False
            Me.GridViewMatInfo.OptionsFind.AllowFindPanel = False
            Me.GridViewMatInfo.OptionsFind.ShowClearButton = False
            Me.GridViewMatInfo.OptionsFind.ShowFindButton = False
            Me.GridViewMatInfo.OptionsLayout.Columns.AddNewColumns = False
            Me.GridViewMatInfo.OptionsSelection.EnableAppearanceFocusedCell = False
            Me.GridViewMatInfo.OptionsSelection.EnableAppearanceFocusedRow = False
            Me.GridViewMatInfo.OptionsSelection.UseIndicatorForSelection = False
            Me.GridViewMatInfo.OptionsView.RowAutoHeight = True
            Me.GridViewMatInfo.OptionsView.ShowDetailButtons = False
            Me.GridViewMatInfo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
            Me.GridViewMatInfo.OptionsView.ShowFooter = True
            Me.GridViewMatInfo.OptionsView.ShowGroupExpandCollapseButtons = False
            Me.GridViewMatInfo.OptionsView.ShowGroupPanel = False
            Me.GridViewMatInfo.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
            Me.GridViewMatInfo.OptionsView.ShowIndicator = False
            Me.GridViewMatInfo.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.[False]
            Me.GridViewMatInfo.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
            '
            'GridColumn1
            '
            Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
            Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            Me.GridColumn1.Caption = "พื้นที่"
            Me.GridColumn1.FieldName = "StorageName"
            Me.GridColumn1.Name = "GridColumn1"
            Me.GridColumn1.OptionsColumn.AllowEdit = False
            Me.GridColumn1.OptionsColumn.AllowFocus = False
            Me.GridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
            Me.GridColumn1.OptionsColumn.AllowIncrementalSearch = False
            Me.GridColumn1.OptionsColumn.ReadOnly = True
            Me.GridColumn1.OptionsColumn.ShowInExpressionEditor = False
            Me.GridColumn1.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "StorageName", "รวม")})
            Me.GridColumn1.Visible = True
            Me.GridColumn1.VisibleIndex = 0
            Me.GridColumn1.Width = 166
            '
            'GridColumn2
            '
            Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
            Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            Me.GridColumn2.Caption = "วัตถุดิบ"
            Me.GridColumn2.FieldName = "MaterialName"
            Me.GridColumn2.Name = "GridColumn2"
            Me.GridColumn2.OptionsColumn.ReadOnly = True
            Me.GridColumn2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaterialName", "{0} รายการ")})
            Me.GridColumn2.Visible = True
            Me.GridColumn2.VisibleIndex = 1
            Me.GridColumn2.Width = 266
            '
            'GridColumn5
            '
            Me.GridColumn5.Caption = "อายุ"
            Me.GridColumn5.FieldName = "AgeDay"
            Me.GridColumn5.Name = "GridColumn5"
            Me.GridColumn5.OptionsColumn.ReadOnly = True
            Me.GridColumn5.Visible = True
            Me.GridColumn5.VisibleIndex = 2
            Me.GridColumn5.Width = 94
            '
            'GridColumn3
            '
            Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
            Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Me.GridColumn3.Caption = "น้ำหนัก"
            Me.GridColumn3.DisplayFormat.FormatString = "0.000"
            Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridColumn3.FieldName = "WeightADT"
            Me.GridColumn3.Name = "GridColumn3"
            Me.GridColumn3.OptionsColumn.ReadOnly = True
            Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WeightADT", "{0:0.000}")})
            Me.GridColumn3.Visible = True
            Me.GridColumn3.VisibleIndex = 3
            Me.GridColumn3.Width = 147
            '
            'GridColumn4
            '
            Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
            Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Me.GridColumn4.Caption = "จำนวน"
            Me.GridColumn4.DisplayFormat.FormatString = "{0:n1}"
            Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridColumn4.FieldName = "Quantity"
            Me.GridColumn4.GroupFormat.FormatString = "{0:n1}"
            Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.GridColumn4.Name = "GridColumn4"
            Me.GridColumn4.OptionsColumn.ReadOnly = True
            Me.GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n1}")})
            Me.GridColumn4.Visible = True
            Me.GridColumn4.VisibleIndex = 4
            Me.GridColumn4.Width = 156
            '
            'btnClose
            '
            Me.btnClose.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnClose.Appearance.Options.UseFont = True
            Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
            Me.btnClose.Location = New System.Drawing.Point(738, 19)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(110, 40)
            Me.btnClose.StyleController = Me.LayoutControl1
            Me.btnClose.TabIndex = 36
            Me.btnClose.Text = "Close"
            '
            'LayoutControl1
            '
            Me.LayoutControl1.Controls.Add(Me.btnClose)
            Me.LayoutControl1.Controls.Add(Me.GridMatInfo)
            Me.LayoutControl1.Controls.Add(Me.LookUpEditGroupArea)
            Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControl1.Name = "LayoutControl1"
            Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1173, 110, 375, 525)
            Me.LayoutControl1.Root = Me.LayoutControlGroup1
            Me.LayoutControl1.Size = New System.Drawing.Size(867, 372)
            Me.LayoutControl1.TabIndex = 37
            Me.LayoutControl1.Text = "LayoutControl1"
            '
            'LayoutControlGroup1
            '
            Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
            Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.lblAreaInfo, Me.LayoutControlItem4})
            Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlGroup1.Name = "Root"
            Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
            Me.LayoutControlGroup1.Size = New System.Drawing.Size(867, 372)
            Me.LayoutControlGroup1.TextVisible = False
            '
            'LayoutControlItem1
            '
            Me.LayoutControlItem1.Control = Me.GridMatInfo
            Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 46)
            Me.LayoutControlItem1.Name = "LayoutControlItem1"
            Me.LayoutControlItem1.Size = New System.Drawing.Size(835, 294)
            Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem1.TextVisible = False
            '
            'LayoutControlItem2
            '
            Me.LayoutControlItem2.Control = Me.btnClose
            Me.LayoutControlItem2.Location = New System.Drawing.Point(719, 0)
            Me.LayoutControlItem2.Name = "LayoutControlItem2"
            Me.LayoutControlItem2.Size = New System.Drawing.Size(116, 46)
            Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem2.TextVisible = False
            '
            'EmptySpaceItem1
            '
            Me.EmptySpaceItem1.AllowHotTrack = False
            Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
            Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
            Me.EmptySpaceItem1.Size = New System.Drawing.Size(56, 46)
            Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
            '
            'EmptySpaceItem2
            '
            Me.EmptySpaceItem2.AllowHotTrack = False
            Me.EmptySpaceItem2.Location = New System.Drawing.Point(664, 0)
            Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
            Me.EmptySpaceItem2.Size = New System.Drawing.Size(55, 46)
            Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
            '
            'lblAreaInfo
            '
            Me.lblAreaInfo.AllowHotTrack = False
            Me.lblAreaInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAreaInfo.AppearanceItemCaption.Options.UseFont = True
            Me.lblAreaInfo.AppearanceItemCaption.Options.UseTextOptions = True
            Me.lblAreaInfo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me.lblAreaInfo.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
            Me.lblAreaInfo.Location = New System.Drawing.Point(56, 0)
            Me.lblAreaInfo.Name = "lblAreaInfo"
            Me.lblAreaInfo.Size = New System.Drawing.Size(306, 46)
            Me.lblAreaInfo.Text = "Info"
            Me.lblAreaInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
            Me.lblAreaInfo.TextSize = New System.Drawing.Size(60, 34)
            '
            'LayoutControlItem4
            '
            Me.LayoutControlItem4.Control = Me.LookUpEditGroupArea
            Me.LayoutControlItem4.Location = New System.Drawing.Point(362, 0)
            Me.LayoutControlItem4.MinSize = New System.Drawing.Size(50, 25)
            Me.LayoutControlItem4.Name = "LayoutControlItem4"
            Me.LayoutControlItem4.Size = New System.Drawing.Size(302, 46)
            Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem4.TextVisible = False
            '
            'LookUpEditGroupArea
            '
            Me.LookUpEditGroupArea.Location = New System.Drawing.Point(381, 19)
            Me.LookUpEditGroupArea.Name = "LookUpEditGroupArea"
            Me.LookUpEditGroupArea.Properties.AllowFocused = False
            Me.LookUpEditGroupArea.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LookUpEditGroupArea.Properties.Appearance.Options.UseFont = True
            Me.LookUpEditGroupArea.Properties.Appearance.Options.UseTextOptions = True
            Me.LookUpEditGroupArea.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me.LookUpEditGroupArea.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            Me.LookUpEditGroupArea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.LookUpEditGroupArea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            Me.LookUpEditGroupArea.Properties.NullText = ""
            Me.LookUpEditGroupArea.Properties.ReadOnly = True
            Me.LookUpEditGroupArea.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
            Me.LookUpEditGroupArea.Size = New System.Drawing.Size(296, 38)
            Me.LookUpEditGroupArea.StyleController = Me.LayoutControl1
            Me.LookUpEditGroupArea.TabIndex = 38
            '
            'frm_IVM_Popup_Material_Info
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(867, 372)
            Me.ControlBox = False
            Me.Controls.Add(Me.LayoutControl1)
            Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frm_IVM_Popup_Material_Info"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            CType(Me.GridMatInfo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridViewMatInfo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.LayoutControl1.ResumeLayout(False)
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblAreaInfo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LookUpEditGroupArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
        Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
        Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
        Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
        Private WithEvents GridMatInfo As DevExpress.XtraGrid.GridControl
        Private WithEvents GridViewMatInfo As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
        Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents lblAreaInfo As DevExpress.XtraLayout.SimpleLabelItem
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LookUpEditGroupArea As DevExpress.XtraEditors.LookUpEdit
    End Class

End Namespace
