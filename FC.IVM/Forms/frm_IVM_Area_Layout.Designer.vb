Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frm_IVM_Area_Layout
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
            Me.components = New System.ComponentModel.Container()
            Dim MapItemAttributeMapping13 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim MapItemAttributeMapping14 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim MapItemAttributeMapping15 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim MapItemAttributeMapping16 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim MapItemAttributeMapping17 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim MapItemAttributeMapping18 As DevExpress.XtraMap.MapItemAttributeMapping = New DevExpress.XtraMap.MapItemAttributeMapping()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_IVM_Area_Layout))
            Me.mapControl1 = New DevExpress.XtraMap.MapControl()
            Me.vectorItemsLayer = New DevExpress.XtraMap.VectorItemsLayer()
            Me.ListSourceDataAdapter1 = New DevExpress.XtraMap.ListSourceDataAdapter()
            Me.VectorItemsLayerCustom = New DevExpress.XtraMap.VectorItemsLayer()
            Me.MapItemStorage1 = New DevExpress.XtraMap.MapItemStorage()
            Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
            Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
            Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
            Me.PopMenuStatus = New DevExpress.XtraBars.BarSubItem()
            Me.BarButtonItemFull = New DevExpress.XtraBars.BarButtonItem()
            Me.BarButtonItemUnFull = New DevExpress.XtraBars.BarButtonItem()
            CType(Me.mapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'mapControl1
            '
            Me.mapControl1.CenterPoint = New DevExpress.XtraMap.GeoPoint(9.0R, -12.0R)
            Me.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.mapControl1.EnableAnimation = False
            Me.mapControl1.Layers.Add(Me.vectorItemsLayer)
            Me.mapControl1.Layers.Add(Me.VectorItemsLayerCustom)
            Me.mapControl1.Location = New System.Drawing.Point(0, 0)
            Me.mapControl1.LookAndFeel.SkinName = "VS2010"
            Me.mapControl1.LookAndFeel.UseDefaultLookAndFeel = False
            Me.mapControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
            Me.mapControl1.MaxZoomLevel = 10.0R
            Me.mapControl1.MinZoomLevel = 4.0R
            Me.mapControl1.Name = "mapControl1"
            Me.mapControl1.NavigationPanelOptions.ShowCoordinates = False
            Me.mapControl1.NavigationPanelOptions.ShowKilometersScale = False
            Me.mapControl1.NavigationPanelOptions.ShowMilesScale = False
            Me.mapControl1.SelectionMode = DevExpress.XtraMap.ElementSelectionMode.[Single]
            Me.mapControl1.ShowToolTips = False
            Me.mapControl1.Size = New System.Drawing.Size(1203, 663)
            Me.mapControl1.TabIndex = 0
            Me.mapControl1.ZoomLevel = 4.5R
            '
            'vectorItemsLayer
            '
            Me.vectorItemsLayer.Data = Me.ListSourceDataAdapter1
            Me.vectorItemsLayer.Name = "vectorItemsLayer"
            Me.vectorItemsLayer.ShapeTitlesPattern = "{Label}"
            '
            'ListSourceDataAdapter1
            '
            MapItemAttributeMapping13.Member = "Tag"
            MapItemAttributeMapping13.Name = "ColorProp"
            MapItemAttributeMapping14.Member = "Label"
            MapItemAttributeMapping14.Name = "Label"
            MapItemAttributeMapping14.ValueType = DevExpress.XtraMap.FieldValueType.[String]
            MapItemAttributeMapping15.Member = "ID"
            MapItemAttributeMapping15.Name = "ID"
            MapItemAttributeMapping16.Member = "Capacity"
            MapItemAttributeMapping16.Name = "Capacity"
            MapItemAttributeMapping17.Member = "Name"
            MapItemAttributeMapping17.Name = "Name"
            MapItemAttributeMapping18.Member = "Font"
            MapItemAttributeMapping18.Name = "Font"
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping13)
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping14)
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping15)
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping16)
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping17)
            Me.ListSourceDataAdapter1.AttributeMappings.Add(MapItemAttributeMapping18)
            Me.ListSourceDataAdapter1.DefaultMapItemType = DevExpress.XtraMap.MapItemType.Rectangle
            Me.ListSourceDataAdapter1.Mappings.Latitude = "Lat"
            Me.ListSourceDataAdapter1.Mappings.Longitude = "Lon"
            Me.ListSourceDataAdapter1.Mappings.Text = "Label"
            '
            'VectorItemsLayerCustom
            '
            Me.VectorItemsLayerCustom.Data = Me.MapItemStorage1
            Me.VectorItemsLayerCustom.Name = "VectorItemsLayerCustom"
            '
            'BarManager1
            '
            Me.BarManager1.DockControls.Add(Me.barDockControlTop)
            Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
            Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
            Me.BarManager1.DockControls.Add(Me.barDockControlRight)
            Me.BarManager1.Form = Me
            Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PopMenuStatus, Me.BarButtonItemFull, Me.BarButtonItemUnFull})
            Me.BarManager1.MaxItemId = 3
            '
            'barDockControlTop
            '
            Me.barDockControlTop.CausesValidation = False
            Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
            Me.barDockControlTop.Size = New System.Drawing.Size(1203, 0)
            '
            'barDockControlBottom
            '
            Me.barDockControlBottom.CausesValidation = False
            Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.barDockControlBottom.Location = New System.Drawing.Point(0, 663)
            Me.barDockControlBottom.Size = New System.Drawing.Size(1203, 0)
            '
            'barDockControlLeft
            '
            Me.barDockControlLeft.CausesValidation = False
            Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
            Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
            Me.barDockControlLeft.Size = New System.Drawing.Size(0, 663)
            '
            'barDockControlRight
            '
            Me.barDockControlRight.CausesValidation = False
            Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
            Me.barDockControlRight.Location = New System.Drawing.Point(1203, 0)
            Me.barDockControlRight.Size = New System.Drawing.Size(0, 663)
            '
            'PopMenuStatus
            '
            Me.PopMenuStatus.Caption = "จัดการสถานะพื้นที่เก็บวัตถุดิบ"
            Me.PopMenuStatus.Glyph = CType(resources.GetObject("PopMenuStatus.Glyph"), System.Drawing.Image)
            Me.PopMenuStatus.Id = 0
            Me.PopMenuStatus.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItemFull), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItemUnFull)})
            Me.PopMenuStatus.Name = "PopMenuStatus"
            Me.PopMenuStatus.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
            '
            'BarButtonItemFull
            '
            Me.BarButtonItemFull.Caption = "พื้นที่เต็ม"
            Me.BarButtonItemFull.Glyph = CType(resources.GetObject("BarButtonItemFull.Glyph"), System.Drawing.Image)
            Me.BarButtonItemFull.Id = 1
            Me.BarButtonItemFull.Name = "BarButtonItemFull"
            '
            'BarButtonItemUnFull
            '
            Me.BarButtonItemUnFull.Caption = "พื้นที่ว่าง"
            Me.BarButtonItemUnFull.Glyph = CType(resources.GetObject("BarButtonItemUnFull.Glyph"), System.Drawing.Image)
            Me.BarButtonItemUnFull.Id = 2
            Me.BarButtonItemUnFull.Name = "BarButtonItemUnFull"
            '
            'frm_IVM_Area_Layout
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1203, 663)
            Me.Controls.Add(Me.mapControl1)
            Me.Controls.Add(Me.barDockControlLeft)
            Me.Controls.Add(Me.barDockControlRight)
            Me.Controls.Add(Me.barDockControlBottom)
            Me.Controls.Add(Me.barDockControlTop)
            Me.Name = "frm_IVM_Area_Layout"
            Me.Text = "frm_IVM_Area_Layout"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            CType(Me.mapControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents mapControl1 As DevExpress.XtraMap.MapControl
        Private WithEvents vectorItemsLayer As DevExpress.XtraMap.VectorItemsLayer
        Friend WithEvents ListSourceDataAdapter1 As DevExpress.XtraMap.ListSourceDataAdapter
        Private WithEvents VectorItemsLayerCustom As DevExpress.XtraMap.VectorItemsLayer
        Friend WithEvents MapItemStorage1 As DevExpress.XtraMap.MapItemStorage
        Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
        Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
        Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
        Friend WithEvents BarButtonItemFull As DevExpress.XtraBars.BarButtonItem
        Friend WithEvents BarButtonItemUnFull As DevExpress.XtraBars.BarButtonItem
        Private WithEvents PopMenuStatus As DevExpress.XtraBars.BarSubItem
        'Friend WithEvents PopMenuStatus As DevExpress.XtraBars.BarButtonItem
        'Friend WithEvents BarButtonItemFull As DevExpress.XtraBars.BarSubItem
        'Friend WithEvents BarButtonItemUnFull As DevExpress.XtraBars.BarSubItem
        'Friend WithEvents barSubItem1 As DevExpress.XtraBars.BarSubItem
        'Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
        'Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
        'Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
        'Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
        'Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
        'Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
        'Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    End Class

End Namespace
