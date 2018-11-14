Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmTest
        Inherits DevExpress.XtraEditors.XtraForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
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
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
            Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
            Dim TimeRuler3 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTest))
            Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            Me.SchedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
            Me.SchedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage()
            Me.DataNavigator1 = New DevExpress.XtraEditors.DataNavigator()
            Me.grdItem = New DevExpress.XtraGrid.GridControl()
            Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.dte = New DevExpress.XtraEditors.DateEdit()
            Me.cmbMachine = New DevExpress.XtraEditors.GridLookUpEdit()
            Me.GridLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.cmbPlant = New DevExpress.XtraEditors.GridLookUpEdit()
            Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.cmbCompany = New DevExpress.XtraEditors.GridLookUpEdit()
            Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.txtRemark = New DevExpress.XtraEditors.MemoEdit()
            Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
            Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.LayoutControl1.SuspendLayout()
            CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.cmbMachine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.cmbPlant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.cmbCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'LayoutControl1
            '
            Me.LayoutControl1.Controls.Add(Me.SimpleButton1)
            Me.LayoutControl1.Controls.Add(Me.SchedulerControl1)
            Me.LayoutControl1.Controls.Add(Me.DataNavigator1)
            Me.LayoutControl1.Controls.Add(Me.grdItem)
            Me.LayoutControl1.Controls.Add(Me.dte)
            Me.LayoutControl1.Controls.Add(Me.cmbMachine)
            Me.LayoutControl1.Controls.Add(Me.cmbPlant)
            Me.LayoutControl1.Controls.Add(Me.cmbCompany)
            Me.LayoutControl1.Controls.Add(Me.txtRemark)
            resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
            Me.LayoutControl1.Name = "LayoutControl1"
            Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(438, 192, 250, 350)
            Me.LayoutControl1.Root = Me.LayoutControlGroup1
            '
            'SchedulerControl1
            '
            resources.ApplyResources(Me.SchedulerControl1, "SchedulerControl1")
            Me.SchedulerControl1.Name = "SchedulerControl1"
            Me.SchedulerControl1.Start = New Date(2015, 7, 22, 0, 0, 0, 0)
            Me.SchedulerControl1.Storage = Me.SchedulerStorage1
            Me.SchedulerControl1.Views.DayView.TimeRulers.Add(TimeRuler1)
            Me.SchedulerControl1.Views.FullWeekView.Enabled = True
            Me.SchedulerControl1.Views.FullWeekView.TimeRulers.Add(TimeRuler2)
            Me.SchedulerControl1.Views.WeekView.Enabled = False
            Me.SchedulerControl1.Views.WorkWeekView.TimeRulers.Add(TimeRuler3)
            '
            'DataNavigator1
            '
            resources.ApplyResources(Me.DataNavigator1, "DataNavigator1")
            Me.DataNavigator1.Name = "DataNavigator1"
            Me.DataNavigator1.StyleController = Me.LayoutControl1
            '
            'grdItem
            '
            Me.grdItem.EmbeddedNavigator.Margin = CType(resources.GetObject("grdItem.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
            resources.ApplyResources(Me.grdItem, "grdItem")
            Me.grdItem.MainView = Me.GridView1
            Me.grdItem.Name = "grdItem"
            Me.grdItem.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
            '
            'GridView1
            '
            Me.GridView1.GridControl = Me.grdItem
            Me.GridView1.Name = "GridView1"
            '
            'dte
            '
            resources.ApplyResources(Me.dte, "dte")
            Me.dte.Name = "dte"
            Me.dte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dte.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.dte.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dte.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.dte.StyleController = Me.LayoutControl1
            '
            'cmbMachine
            '
            resources.ApplyResources(Me.cmbMachine, "cmbMachine")
            Me.cmbMachine.Name = "cmbMachine"
            Me.cmbMachine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbMachine.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.cmbMachine.Properties.View = Me.GridLookUpEdit3View
            Me.cmbMachine.StyleController = Me.LayoutControl1
            '
            'GridLookUpEdit3View
            '
            Me.GridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            Me.GridLookUpEdit3View.Name = "GridLookUpEdit3View"
            Me.GridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
            Me.GridLookUpEdit3View.OptionsView.ShowGroupPanel = False
            '
            'cmbPlant
            '
            resources.ApplyResources(Me.cmbPlant, "cmbPlant")
            Me.cmbPlant.Name = "cmbPlant"
            Me.cmbPlant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbPlant.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.cmbPlant.Properties.View = Me.GridLookUpEdit2View
            Me.cmbPlant.StyleController = Me.LayoutControl1
            '
            'GridLookUpEdit2View
            '
            Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
            Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
            Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
            '
            'cmbCompany
            '
            resources.ApplyResources(Me.cmbCompany, "cmbCompany")
            Me.cmbCompany.Name = "cmbCompany"
            Me.cmbCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbCompany.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.cmbCompany.Properties.View = Me.GridLookUpEdit1View
            Me.cmbCompany.StyleController = Me.LayoutControl1
            '
            'GridLookUpEdit1View
            '
            Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
            Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
            Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
            '
            'txtRemark
            '
            resources.ApplyResources(Me.txtRemark, "txtRemark")
            Me.txtRemark.Name = "txtRemark"
            Me.txtRemark.StyleController = Me.LayoutControl1
            '
            'LayoutControlGroup1
            '
            resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
            Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
            Me.LayoutControlGroup1.GroupBordersVisible = False
            Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem1, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9})
            Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlGroup1.Name = "Root"
            Me.LayoutControlGroup1.Size = New System.Drawing.Size(842, 571)
            Me.LayoutControlGroup1.TextVisible = False
            '
            'LayoutControlItem2
            '
            Me.LayoutControlItem2.Control = Me.cmbCompany
            resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
            Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlItem2.Name = "LayoutControlItem2"
            Me.LayoutControlItem2.Size = New System.Drawing.Size(411, 24)
            Me.LayoutControlItem2.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem4
            '
            Me.LayoutControlItem4.Control = Me.cmbMachine
            resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
            Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 24)
            Me.LayoutControlItem4.Name = "LayoutControlItem4"
            Me.LayoutControlItem4.Size = New System.Drawing.Size(411, 24)
            Me.LayoutControlItem4.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem3
            '
            Me.LayoutControlItem3.Control = Me.cmbPlant
            resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
            Me.LayoutControlItem3.Location = New System.Drawing.Point(411, 0)
            Me.LayoutControlItem3.Name = "LayoutControlItem3"
            Me.LayoutControlItem3.Size = New System.Drawing.Size(411, 24)
            Me.LayoutControlItem3.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem5
            '
            Me.LayoutControlItem5.Control = Me.txtRemark
            resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
            Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
            Me.LayoutControlItem5.MinSize = New System.Drawing.Size(79, 22)
            Me.LayoutControlItem5.Name = "LayoutControlItem5"
            Me.LayoutControlItem5.Size = New System.Drawing.Size(822, 78)
            Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            Me.LayoutControlItem5.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem6
            '
            Me.LayoutControlItem6.Control = Me.dte
            resources.ApplyResources(Me.LayoutControlItem6, "LayoutControlItem6")
            Me.LayoutControlItem6.Location = New System.Drawing.Point(411, 24)
            Me.LayoutControlItem6.Name = "LayoutControlItem6"
            Me.LayoutControlItem6.Size = New System.Drawing.Size(411, 24)
            Me.LayoutControlItem6.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem1
            '
            Me.LayoutControlItem1.Control = Me.grdItem
            resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
            Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 175)
            Me.LayoutControlItem1.Name = "LayoutControlItem1"
            Me.LayoutControlItem1.Size = New System.Drawing.Size(822, 187)
            Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
            Me.LayoutControlItem1.TextSize = New System.Drawing.Size(96, 13)
            '
            'LayoutControlItem7
            '
            Me.LayoutControlItem7.Control = Me.DataNavigator1
            resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
            Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 126)
            Me.LayoutControlItem7.Name = "LayoutControlItem7"
            Me.LayoutControlItem7.Size = New System.Drawing.Size(822, 23)
            Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem7.TextVisible = False
            '
            'LayoutControlItem8
            '
            Me.LayoutControlItem8.Control = Me.SchedulerControl1
            Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 362)
            Me.LayoutControlItem8.Name = "LayoutControlItem8"
            Me.LayoutControlItem8.Size = New System.Drawing.Size(822, 189)
            Me.LayoutControlItem8.TextSize = New System.Drawing.Size(96, 13)
            '
            'SimpleButton1
            '
            resources.ApplyResources(Me.SimpleButton1, "SimpleButton1")
            Me.SimpleButton1.Name = "SimpleButton1"
            Me.SimpleButton1.StyleController = Me.LayoutControl1
            '
            'LayoutControlItem9
            '
            Me.LayoutControlItem9.Control = Me.SimpleButton1
            Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 149)
            Me.LayoutControlItem9.Name = "LayoutControlItem9"
            Me.LayoutControlItem9.Size = New System.Drawing.Size(822, 26)
            Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem9.TextVisible = False
            '
            'frmTest
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.LayoutControl1)
            Me.Name = "frmTest"
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.LayoutControl1.ResumeLayout(False)
            CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.cmbMachine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.cmbPlant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.cmbCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents cmbMachine As DevExpress.XtraEditors.GridLookUpEdit
        Friend WithEvents GridLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents cmbPlant As DevExpress.XtraEditors.GridLookUpEdit
        Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents cmbCompany As DevExpress.XtraEditors.GridLookUpEdit
        Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents dte As DevExpress.XtraEditors.DateEdit
        Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents DataNavigator1 As DevExpress.XtraEditors.DataNavigator
        Friend WithEvents grdItem As DevExpress.XtraGrid.GridControl
        Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents txtRemark As DevExpress.XtraEditors.MemoEdit
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SchedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
        Friend WithEvents SchedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
        Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    End Class
End Namespace

