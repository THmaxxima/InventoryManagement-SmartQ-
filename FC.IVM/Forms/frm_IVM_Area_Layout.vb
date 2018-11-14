Imports System.Windows.Forms
Imports FC.IVM.Bus.Modules
Imports FC.MainApp.Modules
Imports FC.MainWinApp
Imports FC.M.BLL_Util
Imports DevExpress.XtraMap
Imports DevExpress.XtraBars
Imports System.Drawing
Imports System.Threading
Imports FC.MainSQL.Modules
Imports FC.SharedWinFormBus.Classes
Imports FC.M.PSL_Win.Classes_Helper

Namespace Forms
    ''' <summary>
    '''   <para style="PADDING-BOTTOM: 0px; TEXT-ALIGN: left; PADDING-TOP: 0px; PADDING-LEFT: 0px; MARGIN: 0px 0px 0px 40px; LINE-HEIGHT: 1.15; PADDING-RIGHT: 0px; TEXT-INDENT: 0pt">
    '''     <u>
    '''       <strong>หน้าจอหลัก Main Layout (frm_IVM_Area_Layout)</strong>
    '''     </u>
    '''   </para>
    '''   <blockquote style="MARGIN-RIGHT: 0px" dir="ltr">
    '''     <para>
    '''       <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">เป็นชื่อที่ใช้เรียกแทน frm_IVM_Main_Layout</span>
    '''     </para>
    '''   </blockquote>
    '''   <para style="FONT-SIZE: 14px; FONT-FAMILY: &quot;Segoe UI&quot;, &quot;Helvetica Neue&quot;, Helvetica, Arial, Verdana; WHITE-SPACE: normal; WORD-SPACING: 0px; TEXT-TRANSFORM: none; FONT-WEIGHT: 400; COLOR: rgb(34,34,34); PADDING-BOTTOM: 0px; FONT-STYLE: normal; TEXT-ALIGN: left; PADDING-TOP: 0px; PADDING-LEFT: 0px; ORPHANS: 2; WIDOWS: 2; MARGIN: 0px 0px 12px; LETTER-SPACING: normal; LINE-HEIGHT: 1.15; PADDING-RIGHT: 0px; TEXT-INDENT: 0px; font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial" align="left">
    '''     <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; FONT-WEIGHT: bold; COLOR: rgb(0,0,0)">ฟังก์ชั่นหลักของฟอร์ม frm_IVM_Main_Layout</span>
    '''   </para>
    '''   <para style="FONT-SIZE: 14px; FONT-FAMILY: &quot;Segoe UI&quot;, &quot;Helvetica Neue&quot;, Helvetica, Arial, Verdana; WHITE-SPACE: normal; WORD-SPACING: 0px; TEXT-TRANSFORM: none; FONT-WEIGHT: 400; COLOR: rgb(34,34,34); PADDING-BOTTOM: 0px; FONT-STYLE: normal; TEXT-ALIGN: left; PADDING-TOP: 0px; PADDING-LEFT: 0px; ORPHANS: 2; WIDOWS: 2; MARGIN: 0px 0px 12px; LETTER-SPACING: normal; LINE-HEIGHT: 1.15; PADDING-RIGHT: 0px; TEXT-INDENT: 0px; font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial" align="left"></para>
    '''   <para dir="ltr" align="left">
    '''     <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">1. Unload material : เป็นกระบวนการ
    ''' การรับวัตถุติบเข้าสู่ พื้นที่จัดเก็บในแต่ละลาน แบ่งออกเป็น 2 ประเภท ได้แก่</span>
    '''   </para>
    '''   <blockquote style="MARGIN-RIGHT: 0px" dir="ltr">
    '''     <blockquote style="MARGIN-RIGHT: 0px" dir="ltr">
    '''       <para align="left">
    '''         <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">1.1 Unload domistic</span>
    '''       </para>
    '''       <para align="left">
    '''         <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">
    '''           <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">1.2 Unload import</span>
    '''         </span>
    '''       </para>
    '''     </blockquote>
    '''   </blockquote>
    '''   <para align="left">
    '''     <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">2. ย้าย วัตถุดิบ : เป็นกระบวนการ ย้ายวัตถุดิบ
    ''' จากพื้นที่จัดเก็บต้นทาง (Souce area) ไปยังอีกพื้นที่จัดเก็บปลายทาง (Distination) ภายในลานเดียวกัน.</span>
    '''   </para>
    '''   <para align="left">
    '''     <span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">3. ตัดจ่าย วัตถุดิบ : เป็นกระบวนการ
    ''' นำวัตถุดิบจากพื้นที่จัด<span style="FONT-SIZE: 16px; FONT-FAMILY: &quot;Segoe UI&quot;; COLOR: rgb(0,0,0); FONT-STYLE: normal">เก็บไดๆ ในพื้นที่จัดเก็บ
    ''' ไปเข้าสู่กระบวนการการผลิต (WP).</span></span>
    '''   </para>
    '''   <para align="left"></para>
    ''' </summary>
    Public Class frm_IVM_Area_Layout
        Implements IChildOfMainForm

        ''' <exclude />
        Public Event OnPassData As IChildOfMainForm.OnPassDataEventHandler Implements IChildOfMainForm.OnPassData

        ''' <summary>ฟังก์ชั่น ของ X10 ฟังก์ชั่นนี้จะถูกเรียกใช้ก่อน Form load</summary>
        ''' <param name="param">ID ของแต่ละลาน กำหนดโดย X10 (Manu management)</param>
        Public Sub OnBeforeFormLoad(param As Object) Implements IChildOfMainForm.OnBeforeFormLoad

            If Not IsNothing(param) AndAlso TypeOf param Is List(Of PropertyWithValue) Then
                'If Not IsNothing(param) Then
                Dim lstOfPropertyWithValue As List(Of PropertyWithValue) = CType(param, List(Of PropertyWithValue))
                For Each item As PropertyWithValue In lstOfPropertyWithValue
                    If item.Name = "Area_ID" Then
                        idOfField = CInt(item.Value)
                    End If
                Next
            End If
        End Sub

        ''' <exclude />
        ''' <excludetoc />
        Public Sub OnClickClear(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickClear

        End Sub

        ''' <exclude />
        ''' <excludetoc />
        Public Sub OnClickCustomButton(customButtonName As String, ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickCustomButton

        End Sub

        ''' <exclude />
        ''' <excludetoc />
        Public Sub OnClickReload(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickReload

        End Sub

        ''' <exclude />
        ''' <excludetoc />
        Public Sub OnClickSave(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickSave
            Dim strValue As String = ModMainApp.GetResourceMessage("Test")
            Dim deployConfigValue As String = ModMainApp.DeployConfigDll.GetConfig("IVM", "App_Data_Mode")
            Dim appConfigValue As String = ModMainApp.AppConfigDll.GetConfig("System", "Program_Name")
            Dim userConfigValue As String = ModMainApp.MainAppConfigFile.ReadAppSetting("IVMConfig1")
        End Sub


        ''' <exclude />
        Dim idOfField As Integer = 0 ' อาจย้ายไปประกาศตัวแปรระดับ form เพื่อเอาไปใช้ที่ส่วนอื่นๆใน form

        ''' <summary>Mapitem ListSourceDataAdapter</summary>
        ''' <exclude />
        Dim adapter As ListSourceDataAdapter
        ''' <exclude />
        Dim itemsLayer As New VectorItemsLayer()
        ''' <exclude />
        Dim itemsLayerStatus As MapItemStorage
        ''' <exclude />
        Dim allItemsRec As New List(Of MapItem)()

        ''' <exclude />
        Dim timer As New Windows.Forms.Timer()

        ''' <exclude />
        Dim Pcapacity As Double = 0
        ''' <exclude />
        Dim AID As Integer
        ''' <exclude />
        Dim AName As String = ""
        ''' <exclude />
        Dim zoom_lavel As Double = 0
        ''' <exclude />
        Dim iIndex_detail As String = ""
        ''' <exclude />
        Dim iIname As String = ""
        ''' <exclude />
        Dim icount1 As Integer = 0
        ''' <exclude />
        Dim tmpid As String = ""
        ''' <exclude />
        Dim tmp_area_name As String = ""
        ''' <exclude />
        Dim tmp_mat_name As String = ""
        ''' <exclude />
        Dim tmp_balance As String = ""
        ''' <exclude />
        Dim tmp_weight As String = ""
        ''' <exclude />
        Dim tmp_capacity As String = ""
        ''' <exclude />
        Dim tmp_capacity_status As String = ""
        ''' <exclude />
        Dim tmp_last_transac As String = ""
        ''' <exclude />
        Dim tmp_inuse As String = ""
        ''' <exclude />
        Dim tmp_sumcapacity As String = ""
        ''' <exclude />
        Dim tmp_transac_status As String = ""
        ''' <exclude />
        Dim tmp_inuse_status As String = ""
        ''' <exclude />
        Dim str As String = ""
        ''' <exclude />
        Dim currentLocation As GeoPoint
        ''' <exclude />
        Dim iRowArea As Integer
        ''' <exclude />
        Dim iAreaCount As Integer = 0

        ''' <exclude />
        Dim DT_Area_Info As New DataTable

        ''' <exclude />
        Private Const MatTypeID As Integer = 1
        ''' <exclude />
        Private Const UnloadLocalTypeID As Integer = 1
        ''' <exclude />
        Private Const UnloadImportTypeID As Integer = 2
        ''' <exclude />
        Dim FUPD As Boolean = False
        ''' <exclude />
        Dim iChk As Boolean = False
        ''' <exclude />
        Dim AreaWeight As Double = 0
        ''' <exclude />
        Dim PAreaWeight As Double = 0
        ''' <exclude />
        Dim currentTime As DateTime

        ''' <exclude />
        ''' <excludetoc />
        Public Sub New()
            'setParam(rootAreaID)
            InitializeComponent()
            UID = FC.MainApp.Modules.ModMainApp.UserId
            fontsCollection_Renamed = CreateFonts()
            '+++++++++++++++++++++++++++++++++++++++
            currentTime = Get_Curr_DB_Time()
            '+++++++++++++++++++++++++++++++++++++++
            'ExportToShp("D:\Projects\1454-Inventory_Manage_V1.0.0\shp.shp")
        End Sub

        ''' <summary>
        '''   <para>Create a map control and add it to the form.</para>
        '''   <para>Generate a data storage for the layer.</para>
        '''   <para>Create a colorizer for the layer.</para>
        '''   <para>Create a legend forthe layer.</para>
        ''' </summary>
        Private Sub InitializeMap()
            Try
                ' Create a map control and add it to the form.
                mapControl1.SetMapItemFactory(New MapItemFactory())
                adapter = CType(Layer.Data, ListSourceDataAdapter)
                ' Generate a data storage for the layer.
                Dim itemType As New MapItemType
                itemType = MapItemType.Rectangle
                adapter.DefaultMapItemType = itemType
                adapter.Mappings.Latitude = "Lat"
                adapter.Mappings.Longitude = "Lon"

                '+++++++++++++++ Check select root area ++++++++++++++++
                Select Case idOfField
                    Case 1
                        adapter.DataSource = CreateAreaItem.Instance
                    Case 2
                        adapter.DataSource = CreateAreaItem.Instance_Area2
                    Case 3
                        adapter.DataSource = CreateAreaItem.Instance_Area3
                    Case Else
                        adapter.DataSource = CreateAreaItem.Instance
                End Select
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++
                ' Create a colorizer for the layer.
                itemsLayer.Colorizer = CreateColorizer()
                ' Create a legend for the layer.
                mapControl1.Legends.Add(CreateLegend(itemsLayer, idOfField))

                AddHandler mapControl1.MapItemClick, AddressOf MapItemClick
                AddHandler timer.Tick, AddressOf OnTimedEvent
                timer.Interval = 20000

            Catch ex As Exception
                'MsgBox("InitializeMap := " & ex.Message)
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [InitializeMap]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>Return mapcontrol.</summary>
        Public ReadOnly Property MapControl() As MapControl
            Get
                Return mapControl1
            End Get
        End Property
        ''' <summary>Return object VectorItemsLayer in mapcontrol layer 0.</summary>
        Public ReadOnly Property Layer() As VectorItemsLayer
            Get
                ' Create a layer to display vector data.
                itemsLayer.SelectedItems.Clear()
                itemsLayer = CType(mapControl1.Layers(0), VectorItemsLayer)
                Return itemsLayer
            End Get
        End Property
        '++++++++++++++++++++++++++ItemStorage++++++++++++++++++++++++++++++++
        ''' <summary>Return object VectorItemsLayer in mapcontrol layer 1.</summary>
        Private ReadOnly Property VectorLayer() As VectorItemsLayer
            Get
                Return CType(mapControl1.Layers(1), VectorItemsLayer)
            End Get
        End Property
        ''' <summary>Return object ItemStorage in mapcontrol layer 1.</summary>
        Private ReadOnly Property ItemStorage() As MapItemStorage
            Get
                Return CType(VectorLayer.Data, MapItemStorage)
            End Get
        End Property
        ''' <summary>คิวรี่ข้อมูลของพื้นที่กองเก็บของแต่ละลานมากำหนดค่า ให้กับ Mapitem.</summary>
        ''' <remarks>
        '''   <para>-Area ID</para>
        '''   <para>-Area Name</para>
        '''   <para>-Full status</para>
        '''   <para>-Capacity</para>
        '''   <para></para>
        ''' </remarks>
        Public Sub UpdateItemMap()
            Dim AreaName As String = ""
            Dim iCount As Integer
            Dim capacity As Double = 0
            Dim isFull As Boolean = False
            Dim AID As Integer
            Dim AName As String = ""
            Try
                iRowArea = DT_Area_Info.Rows.Count
                For Each item As MapItem In (CType(Layer.Data, IMapDataAdapter)).Items
                    AreaName = DataHelper.DBNullOrNothingTo(Of String)(item.Attributes("Label").Value, "")
                    'iRowArea = DT_Area_Info.Rows.Count
                    If (iRowArea > 0) Then
                        For iCount = 0 To iRowArea - 1
                            AName = DataHelper.DBNullOrNothingTo(Of String)(DT_Area_Info.Rows(iCount).Item("GroupName"), "")
                            If (AreaName = AName) Then
                                AID = DataHelper.DBNullOrNothingTo(Of Integer)(DT_Area_Info.Rows(iCount).Item("GroupID"), 0)
                                capacity = DataHelper.DBNullOrNothingTo(Of Double)(DT_Area_Info.Rows(iCount).Item("StorageCapacity"), 0)
                                If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(iCount).Item("InUseCapacity").ToString)) Then
                                    Pcapacity = CType(DT_Area_Info.Rows(iCount).Item("InUseCapacity"), Double)
                                End If

                                If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(iCount).Item("IsFull").ToString)) Then
                                    isFull = CType(DT_Area_Info.Rows(iCount).Item("IsFull"), Boolean)
                                Else
                                    isFull = False
                                End If
                                'isFull
                                Dim RemoveAtt_ID = (item.Attributes("ID"))
                                Dim RemoveAtt_Capacity = (item.Attributes("Capacity"))
                                Dim RemoveAtt_Font = (item.Attributes("Font"))
                                item.Attributes.Remove(CType(RemoveAtt_Capacity, MapItemAttribute))
                                item.Attributes.Remove(CType(RemoveAtt_Font, MapItemAttribute))

                                item.Attributes.Add(New MapItemAttribute() With {.Name = "ID", .Value = AID, .Type = GetType(Integer)})
                                item.Attributes.Add(New MapItemAttribute() With {.Name = CapacityST, .Value = Pcapacity, .Type = GetType(Double)})
                                item.Attributes.Add(New MapItemAttribute() With {.Name = "Font", .Value = FontsCollection("mainArea"), .Type = GetType(Object)})

                                Dim color1 As Color = CType(item.Attributes("ColorProp").Value, Color)
                                Dim color2 As Color = Color.FromArgb(255 - color1.R, 255 - color1.G, 255 - color1.B)
                                Dim power As Double = 0
                                Dim linearizedPower As Integer

                                linearizedPower = CInt(calRangeColor(Pcapacity))
                                If (isFull) Then
                                    item.Fill = colors_ST(linearizedPower)
                                    item.HighlightedFill = Color.LightGray
                                Else
                                    If (CType(item.Attributes("Label").Value, String) = "Other") Then
                                        item.Fill = Color.Gray
                                    Else
                                        item.Fill = colors_ST(linearizedPower)
                                    End If
                                End If

                                Layer.ItemStyle.Font = FontsCollection("mainArea")
                                Layer.ItemStyle.TextColor = Color.Blue

                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [UpdateItemMap]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>คิวรี่ข้อมูลของพื้นที่กองเก็บของแต่ละลานมากำหนดค่า ให้กับ Mapitem vectorItemsLayer และกำหนดสีของเส้นกรอบเป็นสีเทา สำหรับ Area ที่ Isfull = True.</summary>
        Private Sub mapControl1_DrawMapItem(ByVal sender As Object, ByVal e As DrawMapItemEventArgs) Handles mapControl1.DrawMapItem
            Dim AreaName As String = ""
            Dim iCount As Integer
            Dim capacity As Double = 0
            Dim isFull As Boolean = False

            Try
                If (FUPD = True) Then
                    Return
                End If

                Dim layername As String = ""
                layername = CType(e.Item.Layer.Name, String)
                If (CType(e.Item.Layer.Name, String) = "vectorItemsLayer") Then

                    AreaName = DataHelper.DBNullOrNothingTo(Of String)(e.Item.Attributes("Label").Value, "")

                    iRowArea = DT_Area_Info.Rows.Count
                    If (iRowArea > 0) Then
                        For iCount = 0 To iRowArea - 1
                            Dim AID As Integer
                            Dim AName As String = ""
                            AName = DT_Area_Info.Rows(iCount).Item("GroupName").ToString

                            If (AreaName = "Other") Then
                                e.Item.Fill = Color.Gray
                            End If

                            If (AreaName = AName) Then
                                AID = DataHelper.DBNullOrNothingTo(Of Integer)(DT_Area_Info.Rows(iCount).Item("GroupID"), 0)
                                capacity = DataHelper.DBNullOrNothingTo(Of Double)(DT_Area_Info.Rows(iCount).Item("StorageCapacity"), 0)

                                If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(iCount).Item("InUseCapacity").ToString)) Then
                                    Pcapacity = CType(DT_Area_Info.Rows(iCount).Item("InUseCapacity"), Double)
                                End If

                                If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(iCount).Item("IsFull").ToString)) Then
                                    isFull = CType(DT_Area_Info.Rows(iCount).Item("IsFull"), Boolean)
                                Else
                                    isFull = False
                                End If
                                'isFull
                                Dim RemoveAtt_ID = (e.Item.Attributes("ID"))
                                Dim RemoveAtt_Capacity = (e.Item.Attributes("Capacity"))
                                Dim RemoveAtt_Font = (e.Item.Attributes("Font"))

                                e.Item.Attributes.Remove(CType(RemoveAtt_Capacity, MapItemAttribute))
                                e.Item.Attributes.Remove(CType(RemoveAtt_Font, MapItemAttribute))

                                e.Item.Attributes.Add(New MapItemAttribute() With {.Name = "ID", .Value = AID, .Type = GetType(Integer)})
                                e.Item.Attributes.Add(New MapItemAttribute() With {.Name = CapacityST, .Value = Pcapacity, .Type = GetType(Double)})
                                e.Item.Attributes.Add(New MapItemAttribute() With {.Name = "Font", .Value = FontsCollection("mainArea"), .Type = GetType(Object)})

                                Dim color1 As Color = CType(e.Item.Attributes("ColorProp").Value, Color)
                                Dim color2 As Color = Color.FromArgb(255 - color1.R, 255 - color1.G, 255 - color1.B)
                                Dim power As Double = 0
                                Dim linearizedPower As Integer

                                linearizedPower = CInt(calRangeColor(Pcapacity))

                                If (isFull) Then
                                    e.Item.Fill = colors_ST(linearizedPower)
                                    e.Item.HighlightedFill = Color.LightGray
                                    e.Stroke = If(e.IsHighlighted, Color.LightGray, Color.LightGray)
                                    e.StrokeWidth = If(e.IsHighlighted, 10, 10)
                                Else
                                    e.Item.Fill = colors_ST(linearizedPower)
                                    e.Stroke = If(e.IsHighlighted, color1, color2)
                                    e.StrokeWidth = If(e.IsHighlighted, 5, 2)
                                End If

                                e.Layer.ItemStyle.Font = FontsCollection("mainArea")
                                e.Layer.ItemStyle.TextColor = Color.Blue

                            End If
                        Next iCount

                    End If
                End If

                FUPD = True

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [MapControl1_DrawMapItem]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            Finally
                FUPD = True
            End Try
        End Sub
        ''' <summary>เช็ค Zoom level ของแต่ละลาน ในหน้า Main layout เพื่อกำหนด Action operation</summary>
        ''' <remarks>
        '''   <para>Zoom level 1 = Unload, ตัดจ่าย, ย้าย วัตถุดิบ</para>
        '''   <para>Zoom level 2 = แสดงเมนูสำหรับ Set disable/enable area</para>
        '''   <para>Zoom level 3 = เรียก Popup form สำหรับแสดงรายละเอียดของวัตถุดิบที่จัดเก็บไว้ใน พื้นที่ทั้งหมด</para>
        '''   <para></para>
        ''' </remarks>
        Private Sub mapControl1_ViewPortChange(ByVal sender As Object, ByVal e As ViewportChangedEventArgs) Handles vectorItemsLayer.ViewportChanged
            Dim id As String = String.Empty
            Dim label As String = String.Empty
            Dim name As String = String.Empty
            Dim full As Boolean = False
            Dim iChk_Count As Integer = 0
            Dim ObjWidth As Double = 0
            Dim ObjHeight As Double = 0

            Try
                zoom_lavel = getZoomLavel(mapControl1)
                'Me.Text = "Zoom := " & zoom_lavel.ToString
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Select Case idOfField
                    Case 1
                        If (zoom_lavel >= 7) Then
                            '+++++++++++ Show Icon material data on zoom Lv.8++++++++++++
                            If (zoom_lavel >= 7) Then
                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                        item.Visible = True
                                    End If
                                Next
                            ElseIf (zoom_lavel < 7) Then
                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                        item.Visible = False
                                    End If
                                Next
                            End If

                        ElseIf (zoom_lavel >= 6.46 And zoom_lavel < 7.5) Then

                            If (iChk = True) Then
                                '++++++++++++++++++++++++++++++++ Before return visible icon info ++++++++++++++++
                                If (zoom_lavel >= 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = True
                                        End If
                                    Next
                                ElseIf (zoom_lavel < 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                Return
                            End If

                            If (allItemsRec.Count > 0) Then
                                GetSummaryAreaData()
                                For Each Vitem As MapItem In allItemsRec
                                    If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                        iIndex_detail = CType(Vitem.Attributes("ID").Value, String)

                                        For icount1 = 0 To iAreaCount - 1

                                            tmpid = DT_Area_Info.Rows(icount1).Item("GroupID").ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            tmp_area_name = DT_Area_Info.Rows(icount1).Item("GroupName").ToString

                                            If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(icount1).Item("InUseCapacity").ToString)) Then
                                                Dim WC As Double = CDbl(DT_Area_Info.Rows(icount1).Item("InUseCapacity"))
                                                tmp_sumcapacity = WC.ToString("0.00")
                                            Else
                                                tmp_sumcapacity = "0.00"
                                            End If
                                            '++++++++++++++ Prepare storage information ++++++++++++
                                            str = prePareInfo(tmp_area_name, CDbl(tmp_sumcapacity)).ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            If (iIndex_detail = tmpid) Then
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                                Vitem.Attributes("Label").Value = str
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            End If
                                        Next icount1

                                    End If
                                Next

                                If (zoom_lavel >= 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = True
                                        End If
                                    Next
                                ElseIf (zoom_lavel < 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                iChk = True
                            End If
                        ElseIf (zoom_lavel >= 4.5 And zoom_lavel <= 6.5) Then
                            If (allItemsRec.Count > 0) Then
                                For Each Vitem As MapItem In allItemsRec
                                    If Vitem.GetType() Is GetType(MapRectangle) Then

                                        If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("ID").Value.ToString)) Then
                                                id = CType(Vitem.Attributes("ID").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Label").Value.ToString)) Then
                                                label = CType(Vitem.Attributes("Label").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Name").Value.ToString)) Then
                                                name = CType(Vitem.Attributes("Name").Value, String)
                                            End If

                                            If (zoom_lavel < 6.5) Then
                                                If (Vitem.Attributes("Label").Value.ToString <> Vitem.Attributes("Name").Value.ToString) Then
                                                    Vitem.Attributes("Label").Value = name
                                                End If
                                            End If

                                        End If
                                    End If
                                Next

                                If (zoom_lavel >= 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = True
                                        End If
                                    Next
                                ElseIf (zoom_lavel < 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                iChk = False
                            End If
                        Else
                            If (VectorLayer().Data IsNot Nothing) Then
                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                        item.Visible = False
                                    End If
                                Next

                            End If
                            iChk = False
                            Return
                        End If

                    Case 2
                        If (zoom_lavel >= 7) Then
                            '+++++++++++ Show Icon material data on zoom Lv.8++++++++++++
                            For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                    item.Visible = True
                                End If
                            Next

                        ElseIf (zoom_lavel >= 6.1 And zoom_lavel < 7.5) Then

                            If (iChk = True) Then
                                '++++++++++++++++++++++++++++++++ Before return visible icon info ++++++++++++++++
                                If (zoom_lavel >= 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = True
                                        End If
                                    Next
                                ElseIf (zoom_lavel < 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                Return
                            End If

                            If (allItemsRec.Count > 0) Then
                                GetSummaryAreaData()
                                For Each Vitem As MapItem In allItemsRec
                                    'If Vitem.GetType() Is GetType(MapRectangle) Then
                                    If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                        iIndex_detail = CType(Vitem.Attributes("ID").Value, String)

                                        For icount1 = 0 To iAreaCount - 1

                                            tmpid = DT_Area_Info.Rows(icount1).Item("GroupID").ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            tmp_area_name = DT_Area_Info.Rows(icount1).Item("GroupName").ToString

                                            If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(icount1).Item("InUseCapacity").ToString)) Then
                                                Dim WC As Double = CDbl(DT_Area_Info.Rows(icount1).Item("InUseCapacity"))
                                                tmp_sumcapacity = WC.ToString("0.00")
                                            Else
                                                tmp_sumcapacity = "0.00"
                                            End If
                                            '++++++++++++++ Prepare storage information ++++++++++++
                                            str = prePareInfo(tmp_area_name, CDbl(tmp_sumcapacity)).ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            If (iIndex_detail = tmpid) Then
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                                Vitem.Attributes("Label").Value = str
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            End If
                                        Next icount1

                                    End If
                                Next
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next

                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                                iChk = True
                            End If
                        ElseIf (zoom_lavel >= 4.5 And zoom_lavel < 6.1) Then
                            If (allItemsRec.Count > 0) Then
                                For Each Vitem As MapItem In allItemsRec
                                    If Vitem.GetType() Is GetType(MapRectangle) Then

                                        If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("ID").Value.ToString)) Then
                                                id = CType(Vitem.Attributes("ID").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Label").Value.ToString)) Then
                                                label = CType(Vitem.Attributes("Label").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Name").Value.ToString)) Then
                                                name = CType(Vitem.Attributes("Name").Value, String)
                                            End If
                                            If (Vitem.Attributes("Label").Value.ToString <> Vitem.Attributes("Name").Value.ToString) Then
                                                Vitem.Attributes("Label").Value = name
                                            End If

                                        End If
                                    End If
                                Next
                                '+++++++++++++++++++++++++++++++ Hide icon material info ++++++++++++++++++++++

                                If (zoom_lavel <= 6.46) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                                iChk = False
                            End If
                        Else
                            If (VectorLayer().Data IsNot Nothing) Then
                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                        item.Visible = False
                                    End If
                                Next

                            End If
                            iChk = False
                            Return
                        End If

                    Case 3
                        If (zoom_lavel >= 7) Then
                            '+++++++++++ Show Icon material data on zoom Lv.8++++++++++++
                            For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                    item.Visible = True
                                End If
                            Next

                        ElseIf (zoom_lavel >= 6.1 And zoom_lavel < 7.5) Then

                            If (iChk = True) Then
                                '++++++++++++++++++++++++++++++++ Before return visible icon info ++++++++++++++++
                                If (zoom_lavel >= 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = True
                                        End If
                                    Next
                                ElseIf (zoom_lavel < 7) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                Return
                            End If

                            If (allItemsRec.Count > 0) Then
                                GetSummaryAreaData()
                                For Each Vitem As MapItem In allItemsRec
                                    If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                        iIndex_detail = CType(Vitem.Attributes("ID").Value, String)

                                        For icount1 = 0 To iAreaCount - 1

                                            tmpid = DT_Area_Info.Rows(icount1).Item("GroupID").ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            tmp_area_name = DT_Area_Info.Rows(icount1).Item("GroupName").ToString

                                            If Not (String.IsNullOrEmpty(DT_Area_Info.Rows(icount1).Item("InUseCapacity").ToString)) Then
                                                Dim WC As Double = CDbl(DT_Area_Info.Rows(icount1).Item("InUseCapacity"))
                                                tmp_sumcapacity = WC.ToString("0.00")
                                            Else
                                                tmp_sumcapacity = "0.00"
                                            End If
                                            '++++++++++++++ Prepare storage information ++++++++++++
                                            str = prePareInfo(tmp_area_name, CDbl(tmp_sumcapacity)).ToString
                                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            If (iIndex_detail = tmpid) Then
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                                Vitem.Attributes("Label").Value = str
                                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            End If
                                        Next icount1

                                    End If
                                Next

                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then

                                        item.Visible = False
                                    End If
                                Next

                                iChk = True
                            End If
                        ElseIf (zoom_lavel >= 4.5 And zoom_lavel < 6.1) Then
                            If (allItemsRec.Count > 0) Then
                                For Each Vitem As MapItem In allItemsRec
                                    If Vitem.GetType() Is GetType(MapRectangle) Then

                                        If (Vitem.Layer.Name = "vectorItemsLayer") Then

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("ID").Value.ToString)) Then
                                                id = CType(Vitem.Attributes("ID").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Label").Value.ToString)) Then
                                                label = CType(Vitem.Attributes("Label").Value, String)
                                            End If

                                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Name").Value.ToString)) Then
                                                name = CType(Vitem.Attributes("Name").Value, String)
                                            End If
                                            If (Vitem.Attributes("Label").Value.ToString <> Vitem.Attributes("Name").Value.ToString) Then
                                                Vitem.Attributes("Label").Value = name
                                            End If

                                        End If
                                    End If
                                Next
                                '+++++++++++++++++++++++++++++++ Hide icon material info ++++++++++++++++++++++
                                If (zoom_lavel <= 6.45) Then
                                    For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                        If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                            item.Visible = False
                                        End If
                                    Next
                                End If
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                iChk = False
                            End If
                        Else
                            If (VectorLayer().Data IsNot Nothing) Then
                                For Each item As MapItem In (CType(VectorLayer().Data, IMapDataAdapter)).Items
                                    If (item.Attributes("Name").Value.ToString = "MatInfoIcon") Then
                                        item.Visible = False
                                    End If
                                Next
                            End If
                            iChk = False
                            Return
                        End If

                End Select

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [mapControl1_ViewPortChange]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>Open from for management material when zoom level = 1</summary>
        Private Sub MapItemClick(ByVal sender As Object, ByVal e As MapItemClickEventArgs)
            Dim ItemMap As New MapRectangle
            Dim res As Integer = 0
            Dim StorageID As Integer = 0
            Try
                'CreateMenu(e)
                'Do nothing when zoom for display detail data(disable click event)
                Select Case idOfField
                    Case 1
                        If (zoom_lavel >= 6.46 And zoom_lavel < 7) Then
                            Return
                        End If
                    Case 2
                        If (zoom_lavel >= 6.1 And zoom_lavel < 7) Then
                            Return
                        End If
                    Case 3
                        If (zoom_lavel >= 6.1 And zoom_lavel < 7) Then
                            Return
                        End If
                End Select
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                ItemMap = TryCast(e.Item, MapRectangle)
                If (e.Item.Layer.Name = "vectorItemsLayer") Then
                    'ItemMap = TryCast(e.Item, MapRectangle)
                    AID = CInt(ItemMap.Attributes("ID").Value)
                    AName = (TryCast(ItemMap.Attributes("Label").Value, String))
                ElseIf (e.Item.Layer.Name = "VectorItemsLayerCustom") Then
                    AID = CInt(e.Item.Attributes("ID").Value)
                    AName = CType(e.Item.Attributes("Name").Value, String)
                End If

                If (AID = 1 And AName = "Unload") Then
                    Dim frmPopupSubArea As New PopupForms.frm_IVM_Popup_Material_Manage
                    frmPopupSubArea._PUserID = CType(UID, String)
                    '++++++++++++++Use for debug only+++++++++++
                    frmPopupSubArea._MaterialSourceID = UnloadLocalTypeID
                    frmPopupSubArea._materialTypeId = MatTypeID
                    frmPopupSubArea._fieldID = idOfField.ToString
                    frmPopupSubArea._GroupID = AID
                    frmPopupSubArea._siteName = AName
                    frmPopupSubArea._ToDo = AName

                    AddHandler frmPopupSubArea.FormClosed, AddressOf frmPopupSubArea_FormClosed

                    frmPopupSubArea.ShowDialog()
                ElseIf (AID = 2 And AName = "UnloadImport") Then
                    Dim frmPopupSubArea As New PopupForms.frm_IVM_Popup_Material_Manage
                    frmPopupSubArea._PUserID = CType(UID, String)
                    '++++++++++++++Use for debug only+++++++++++
                    frmPopupSubArea._MaterialSourceID = UnloadLocalTypeID
                    frmPopupSubArea._materialTypeId = MatTypeID
                    frmPopupSubArea._fieldID = idOfField.ToString
                    frmPopupSubArea._GroupID = AID
                    frmPopupSubArea._siteName = AName
                    frmPopupSubArea._ToDo = AName

                    AddHandler frmPopupSubArea.FormClosed, AddressOf frmPopupSubArea_FormClosed

                    frmPopupSubArea.ShowDialog()
                ElseIf (AID >= 0 And AName = "MatInfoIcon") Then
                    Dim DT_StorageInfo As New DataTable
                    DT_StorageInfo = func_IVM_Get_ChildStorageData(AID, idOfField)
                    Dim Popup_MatInfo = New PopupForms.frm_IVM_Popup_Material_Info
                    Popup_MatInfo.setParam(AID, DT_StorageInfo)
                    Popup_MatInfo.ShowDialog()
                    Return
                ElseIf (AID >= 0 And (AName <> "Unload" Or AName <> "UnloadImport")) Then
                    'If (zoom_lavel >= 6.46) Then Return
                    Dim frmPopupSubArea As New PopupForms.frm_IVM_Popup_Material_Manage
                    frmPopupSubArea._PUserID = CType(UID, String)
                    frmPopupSubArea._fieldID = idOfField.ToString
                    frmPopupSubArea._GroupID = AID
                    frmPopupSubArea._siteName = AName
                    frmPopupSubArea._ToDo = "MoveInv"
                    AddHandler frmPopupSubArea.FormClosed, AddressOf frmPopupSubArea_FormClosed
                    frmPopupSubArea.ShowDialog()
                Else
                    Return
                End If
            Catch ex As Exception
                'MsgBox("MapItemClick := Unknow Area ID # " & ex.Message)
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [MapItemClick]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>เมื่อปิด Popup form จะทำการคิวรี่ข้อมูลจากฐานข้อมูลมาเก็บไว้ใน Datatable เพื่ออัพเดทการแสดงผล Mapitem ให้เป็นปัจจุบัน.</summary>
        Private Sub frmPopupSubArea_FormClosed(sender As Object, e As FormClosedEventArgs)
            GetDataFromDB()
        End Sub
        ''' <summary>ทำหน้าที่จัดการ การแสดงผลหน้าจอ พื้นที่กองเก็บของแต่ละลานให้พอดีกับหน้าจอ และเก็บค่า Object mapitem ทั้งหมดไว้เพื่อใช้งานในโปรเซสอื่นๆ</summary>
        Public Sub ZoomToFitAllVectorLayers()
            ' TODO: how to determine if all layers are loaded?
            allItemsRec = New List(Of MapItem)()
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            Dim allItems = New List(Of MapItem)()
            For Each layer As VectorItemsLayer In mapControl1.Layers.OfType(Of VectorItemsLayer)() ' go thru all visible layers containing vector items
                If (layer.Visible) Then
                    allItems.AddRange(layer.Data.Items) ' add them to list
                    allItemsRec.AddRange(layer.Data.Items) ' add them to list
                End If
            Next layer
            mapControl1.ZoomToFit(allItems) ' zoom to those items
        End Sub
        ''' <summary>สร้างเมนูสำหรับกำหนดให้ พื้นที่กองเก็บ มีสถานะ ว่าง หรือเต็ม</summary>
        ''' <param name="e">Object area mapitem</param>
        ''' <remarks>จะแสดงเมนู ตอนคลิ๊ก Area ใน Zoom level 2</remarks>
        Private Sub CreateMenu(ByVal e As DevExpress.XtraMap.MapItemClickEventArgs)
            Dim menu As PopupMenu = New PopupMenu()
            ' Bind the menu to a bar manager.
            menu.Manager = BarManager1
            ' Add two items that belong to the bar manager.
            menu.ItemLinks.Add(BarManager1.Items("PopMenuStatus"))
            'menu.ItemLinks.Add(BarManager1.Items("test1"))
            menu.Name = e.Item.Attributes("ID").Value.ToString
            Select Case idOfField
                Case 1
                    If (zoom_lavel >= 6.46 And zoom_lavel < 7) Then
                        menu.ShowPopup(mapControl1.PointToScreen(e.MouseArgs.Location))
                    Else
                        Return
                    End If
                Case 2
                    If (zoom_lavel >= 6.1 And zoom_lavel < 7) Then
                        menu.ShowPopup(mapControl1.PointToScreen(e.MouseArgs.Location))
                    Else
                        Return
                    End If
                Case 3
                    If (zoom_lavel >= 6.1 And zoom_lavel < 7) Then
                        menu.ShowPopup(mapControl1.PointToScreen(e.MouseArgs.Location))
                    Else
                        Return
                    End If
            End Select
        End Sub
        ''' <summary>เรียกใช้ฟังก์ชั่นในการสร้าง เมนู กรณีที่ Object ID &lt;&gt; 1 and 2</summary>
        Private Sub mapControl1_MapItemClick(sender As Object, e As MapItemClickEventArgs) Handles mapControl1.MapItemClick
            tmpMapItemForStatus = (CType(e.Item, MapItem))
            tmpIDForStatus = CInt(e.Item.Attributes("ID").Value)
            If (tmpIDForStatus = 1 Or tmpIDForStatus = 2) Then
                Return
            Else
                CreateMenu(e)
            End If
        End Sub
        ''' <summary>
        '''   <para>Prepare mapcontrol with default config values.</para>
        '''   <para>Draw icon for show material information that store in each area.</para>
        '''   <para></para>
        ''' </summary>
        ''' <param name="sender">Object area mapitem.</param>
        Private Sub VectorItemsLayer_DataLoaded(ByVal sender As Object, ByVal e As DataLoadedEventArgs) Handles vectorItemsLayer.DataLoaded
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ResetMinMaxZoomLevel(mapControl1)
            mapControl1.ZoomToFitLayerItems()
            ZoomToFitAllVectorLayers()
            SetMinMaxZoomLevel(mapControl1)
            '+++++++++++++++++++++++++++ Update mapitem attribute ++++++++++++++++++
            UpdateItemMap()
            '++++++++++++++++++ Draw material info icon zoom lavel7 ++++++++++++++++
            For Each VitemInfo As MapItem In allItemsRec
                If (VitemInfo.GetType() Is GetType(MapRectangle)) Then
                    Dim RecItem As New MapRectangle
                    RecItem = CType(VitemInfo, MapRectangle)
                    Dim px As Double = 0
                    Dim py As Double = 0
                    Dim point As GeoPoint
                    Dim hideItemID As String = String.Empty

                    If Not (String.IsNullOrEmpty(VitemInfo.Attributes("ID").Value.ToString)) Then
                        hideItemID = RecItem.Attributes("ID").Value.ToString
                    End If

                    px = CDbl(RecItem.Location.GetX())
                    py = CDbl(RecItem.Location.GetY())
                    px = (px + 1.4)
                    py = (py - 0.5)
                    point = New GeoPoint(py, px)
                    ItemStorage.Items.Add(CreateLayerStatus(point, "MatInfoIcon", "Information_Icon.png", CInt(hideItemID)))
                    HideLayerItems(CType(VectorItemsLayerCustom, VectorItemsLayer), hideItemID, "MatInfoIcon")
                End If
            Next
        End Sub
        ''' <summary>เรียกใช้งาน ตอนคลิกไอคอน Default zoom ในหน้า Main layout</summary>
        Private Sub mapControl1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mapControl1.MouseUp
            If e.Button <> MouseButtons.Left Then
                Return
            End If
            Dim hitInfo As MapHitInfo = MapControl.CalcHitInfo(e.Location)
            Dim clickedItem As MapOverlayItemBase = GetClickedOverlayItem(hitInfo)
            If clickedItem Is Nothing Then
                Return
            End If
            If clickedItem Is backImage_Renamed Then
                DefaultZoomLevel()
                GetDataFromDB()
                mapControl1.Refresh()
            ElseIf clickedItem Is closeImage_Renamed Then
                Me.Close()
            ElseIf clickedItem Is rollbackImage_Renamed Then
                Dim frmPopupRollback As New Forms.frm_IVM_Rollback
                frmPopupRollback._fieldID = idOfField.ToString
                frmPopupRollback.ShowDialog()
            End If

        End Sub
        ''' <summary>เป็นฟังก์ชั่น สำหรับ จัดการแสดงผล พื้นที่ต่างๆ ของแต่ละ ลาน ให้พอดีกับหน้าจอ.</summary>
        ''' <remarks>เรียกใช้ตอน คลิกที่เมนู ลาน แต่ละลาน</remarks>
        Public Sub DefaultZoomLevel()
            Try
                Dim id As String = ""
                Dim Label As String = ""
                ZoomToFitAllVectorLayers()

                For Each Vitem As MapItem In allItemsRec
                    If Vitem.GetType() Is GetType(MapRectangle) Then
                        If (Vitem.Layer.Name = "vectorItemsLayer") Then
                            If Not (String.IsNullOrEmpty(Vitem.Attributes("ID").Value.ToString)) Then
                                id = CType(Vitem.Attributes("ID").Value, String)
                            End If

                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Label").Value.ToString)) Then
                                Label = CType(Vitem.Attributes("Label").Value, String)
                            End If

                            If Not (String.IsNullOrEmpty(Vitem.Attributes("Name").Value.ToString)) Then
                                Name = CType(Vitem.Attributes("Name").Value, String)
                            End If
                            If (Vitem.Attributes("Label").Value.ToString <> Vitem.Attributes("Name").Value.ToString) Then
                                Vitem.Attributes("Label").Value = Name
                            End If

                        End If
                    End If
                Next
            Catch ex As Exception

            End Try
        End Sub
        ''' <exclude />
        ''' <excludetoc />
        Public Shared Function GetClickedOverlayItem(ByVal hitInfo As MapHitInfo) As MapOverlayItemBase
            If hitInfo.InUIElement Then
                Dim overlayHitInfo As MapOverlayHitInfo = TryCast(hitInfo.UiHitInfo, MapOverlayHitInfo)
                If overlayHitInfo IsNot Nothing Then
                    Return overlayHitInfo.OverlayItem
                End If
            End If
            Return Nothing
        End Function
        ''' <summary>เป็นฟังก์ชั่น สำหรับ Reset ของการซูม mapcontrol.</summary>
        Private Sub ResetMinMaxZoomLevel(ByVal mapControl1 As MapControl)
            mapControl1.MinZoomLevel = 4.5
            'mapControl1.MaxZoomLevel = 11
            mapControl1.MaxZoomLevel = 11.5
        End Sub
        ''' <summary>เป็นฟังก์ชั่น สำหรับ Set default zoom mapcontrol.</summary>
        Private Sub SetMinMaxZoomLevel(ByVal mapControl1 As MapControl)
            mapControl1.MinZoomLevel = mapControl1.ZoomLevel
            mapControl1.MaxZoomLevel = mapControl1.ZoomLevel + 3
        End Sub
        ''' <summary>Return Object Storagemapitem in mapcontrol.</summary>
        ''' <param name="location">Item position</param>
        ''' <param name="name">Object name</param>
        ''' <param name="path">Image path</param>
        ''' <param name="index">Image index</param>
        ''' <returns>DevExpress.XtraMap.MapItem</returns>
        Private Function CreateStorageInformation(ByVal location As GeoPoint, ByVal name As String, ByVal path As String, ByVal index As Integer) As MapItem
            Dim StorageCustomItem As New MapCustomElement() With {.Location = location, .Text = name, .ImageIndex = 0, .TextAlignment = TextAlignment.TopCenter, .Font = FontsCollection("city")}
            StorageCustomItem.Attributes.Add(New MapItemAttribute() With {.Name = "path", .Value = path, .Type = GetType(String)})
            Return StorageCustomItem
        End Function
        ''' <summary>
        '''   <para>ฟังก์ชั่น สำหรับ Query ข้อมูลจากฐานข้อมูล มาแสดงผล ในกรณี ที่ข้อมูลมีการอัพเดทข้อมูล จากโปรเซสต่างๆ.</para>
        '''   <para>-Update area data.</para>
        '''   <para>-Update field total weight.</para>
        ''' </summary>
        Public Sub GetDataFromDB()
            Try
                DT_Area_Info = func_IVM_Get_Area_Info(idOfField)
                iAreaCount = DT_Area_Info.Rows.Count
                '+++++++++++ Redraw mapitem +++++++
                UpdateItemMap()
                AreaWeight = GetSumAreaWeight(idOfField)
                PAreaWeight = GetPAreaWeight(idOfField)
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetDataFromDB]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>คิวรี่ข้อมูลของลานจากฐานข้อมูลมาเก็บไว้ใน Datatable</summary>
        Private Sub GetSummaryAreaData()
            Try
                DT_Area_Info = func_IVM_Get_Area_Info(idOfField)
                iAreaCount = DT_Area_Info.Rows.Count
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetSummaryAreaData]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>เริ่มต้นการทำงาน คิวรี่ข้อมูลต่างๆ จากฐานข้อมูล มาเก็บไว้ใน Datatable เพื่อใช้ในโปรเซส อื่นๆ ต่อไป.</summary>
        Private Sub frm_IVM_Area_Layout_Load(sender As Object, e As EventArgs) Handles Me.Load
            '+++++++++++++++++++++++++++++++++++++
            InitializeMap()
            Me.FormBorderStyle = FormBorderStyle.None
            Me.Size = SystemInformation.PrimaryMonitorSize
            GetDataFromDB()
            '+++++++++++++++++++++++++++++++++++++
            Dim customElement = New MapCustomElement()
            '+++++++++++++++++++++++Unload Local+++++++++++++++++++
            customElement = createUnloadLocalItem(customElement, idOfField)
            ItemStorage.Items.Add(customElement)
            '+++++++++++++++++++++++Unload Import+++++++++++++++++++
            Dim customElement1 = New MapCustomElement()
            customElement1 = createUnloadImportItem(customElement1, idOfField)
            ItemStorage.Items.Add(customElement1)
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            CreateOverlay(mapControl1, lastTime, idOfField)
        End Sub
        ''' <summary>ฟังก์ชั่น สำหรับ กำหนดให้พื้นที่จัดเก็บไดๆ มีสถานะ "เต็ม" เพื่อไม่ให้สามารถ ย้ายวัตถุดิบ หรือ Unload  เข้าในพื้นที่ได้</summary>
        ''' <param name="sender">Mapitem area object that need to set disable</param>
        ''' <remarks>จะแสดงเมนู ตอนคลิก Area ใน Zoom level 2</remarks>
        Public Sub SetFullStatus(sender As Object, e As ItemClickEventArgs) Handles BarButtonItemFull.ItemClick
            Dim Res As Integer = 0
            Res = func_IVM_UpdateStorageFullFlag(tmpIDForStatus, 1, UID)
            UpdateFullIcon(tmpMapItemForStatus, True)
            '++++++++++++++++++++++ For debug only ++++++++++++++++++++++
            GetDataFromDB()
            mapControl1.Refresh()
        End Sub
        ''' <summary>ฟังก์ชั่น สำหรับ กำหนดให้พื้นที่จัดเก็บไดๆ มีสถานะ "ว่าง" เพื่อให้สามารถ ย้ายวัตถุดิบ หรือ Unload เข้าในพื้นที่ได้</summary>
        ''' <param name="sender">Mapitem area object that need to set disable</param>
        ''' <remarks>จะแสดงเมนู ตอนคลิก Area ใน Zoom level 2</remarks>
        Public Sub SetStatusUnFull(sender As Object, e As ItemClickEventArgs) Handles BarButtonItemUnFull.ItemClick
            Dim Res As Integer = 0
            Res = func_IVM_UpdateStorageFullFlag(tmpIDForStatus, 0, UID)
            UpdateFullIcon(tmpMapItemForStatus, False)
            GetDataFromDB()
            mapControl1.Refresh()
        End Sub
        ''' <exclude />
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            Update()
        End Sub
        ''' <summary>เช็คอัพเดทข้อมูลจากฐานข้อมูล ทุกๆ 20 วินาที ถ้าข้อมูลมีการเปลี่ยนแปลง จะทำการคิวรี่ข้อมูลแล้วทำการแสดงผลในระบบต่อไป.</summary>
        Public Overloads Sub Update()
            Try
                Dim TargetTime As DateTime
                Dim DT_GetLastUpdateTime As New DataTable

                '+++++++++++++++++ Get update datetime in database +++++++++++++++++
                TargetTime = func_IVM_Get_LasUpdateData()

                If TargetTime > lastTime Then
                    'DT_Area_Info = func_IVM_Get_Area_Info(idOfField)
                    GetDataFromDB()
                    mapControl1.Refresh()
                    lastTime = TargetTime
                    Dim interval As TimeSpan = currentTime.Subtract(lastTime)
                    Me.Text = lastTime.ToString

                    SetInfoMessage(AreaWeight.ToString("0,##0") & " (ตัน)" & vbCrLf & PAreaWeight.ToString("##0.00") & " (%)" _
                    & vbCrLf & vbCrLf & "Last synchronize data" & vbCrLf & lastTime.ToString)

                End If
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [Update]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Protected Overrides Sub OnVisibleChanged(ByVal e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Me.Visible Then
                Me.Start()
            Else
                Me.Stop()
            End If
        End Sub
        ''' <exclude />
        ''' <excludetoc />
        Public Sub Start()
            lastTime = DateTime.Now
            timer.Start()
        End Sub
        ''' <exclude />
        ''' <excludetoc />
        Public Sub [Stop]()
            timer.Stop()
        End Sub
        ''' <exclude />
        Private Sub OnDispose(ByVal disposing As Boolean)
            If disposing AndAlso (Not IsDisposed) Then
                Me.Stop()
                If timer IsNot Nothing Then
                    timer.Dispose()
                End If
                If Me.adapter IsNot Nothing Then
                    Me.adapter.Dispose()
                    Me.adapter = Nothing
                    Me.allItemsRec.Clear()
                    Me.DT_Area_Info = Nothing
                End If
            End If
        End Sub

        ''' <summary>เมื่อปิดฟอร์ม จะทำการเคลียร์ค่าตัวแปรต่างๆในระบบ</summary>
        Private Sub frm_IVM_Area_Layout_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
            tmpMatProperties = ""
            tmpBalingSealProperties = ""
            tmpTransferPointProperties = ""
            tmpContractorProperties = ""
        End Sub

    End Class
End Namespace
