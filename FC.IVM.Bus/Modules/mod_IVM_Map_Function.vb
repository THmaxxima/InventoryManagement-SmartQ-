Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraBars
Imports DevExpress.XtraMap
Imports FC.M.BLL_Util
Imports FC.M.PSL_Win.Classes_Helper
Imports FC.MainApp.Modules

Namespace Modules
    ''' <summary>Module for management object mapcontrol</summary>
    Public Module mod_IVM_Map_Function
        'Public Const icoFilePath As String = "C:/Forward Consulting/IVM/Bin/Data/Icons/"
        'Public Const imageFilePath As String = "C:/Forward Consulting/IVM/Bin//Data/Images/"
        ''' <exclude />
        Public tmpIDForStatus As Integer = 0
        ''' <exclude />
        Public tmpMapItemForStatus As MapItem
        ''' <exclude />
        Public Const CapacityST As String = "Capacity"
        ''' <exclude />
        Public colors_ST() As Color = {Color.DarkRed, Color.Red, Color.GreenYellow, Color.LimeGreen, Color.Green}

        ''' <exclude />
        Public overlay_Renamed As MapOverlay
        ''' <exclude />
        Public backImage_Renamed As MapOverlayImageItem
        ''' <exclude />
        Public closeImage_Renamed As MapOverlayImageItem
        ''' <exclude />
        Public rollbackImage_Renamed As MapOverlayImageItem
        ''' <exclude />
        Public mapcontrolName_Renamed As MapOverlayTextItem
        ''' <exclude />
        Public mapcontrolRollback_Form As MapOverlayTextItem
        ''' <exclude />
        Public mapcontrolClose_Form As MapOverlayTextItem
        ''' <exclude />
        Public overlayMessage As MapOverlayTextItem
        ''' <exclude />
        Public imageOverlay As MapOverlay
        ''' <exclude />
        Public infoOverlay As MapOverlay
        ''' <exclude />
        Public imageItem As MapOverlayImageItem
        ''' <exclude />
        Public itemValue As Double()
        ''' <exclude />
        Public itemsNames As Dictionary(Of String, String)
        ''' <exclude />
        Public itemsInfo As Dictionary(Of String, MapRectangle)

        ''' <summary>Function use to hide map layer item</summary>
        Public Sub HideLayerItems(ByVal layer As VectorItemsLayer, ByVal ItemID As String, ByVal ItemName As String)
            Try
                For Each item As MapItem In (CType(layer.Data, IMapDataAdapter)).Items

                    If (item.Attributes("ID").Value.ToString = ItemID And item.Attributes("Name").Value.ToString = ItemName) Then
                        item.Visible = False
                        Return
                    End If
                Next
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := HideLayerItems")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to hide map layer item")

                ModMainApp.Log.Log4N("HideLayerItems [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

        End Sub
        Public Function CreateLayerStatus(ByVal location As GeoPoint, ByVal name As String, ByVal iconPath As String, ByVal index As Integer) As MapItem
            Dim IconST As New MapCustomElement() With {.Location = location}
            Try
                Dim imageIco As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("Information_Icon"), Bitmap)
                IconST.Image = New Bitmap(imageIco, New Size(50, 50))
                IconST.Attributes.Add(New MapItemAttribute() With {.Name = "ID", .Value = index, .Type = GetType(String)})
                IconST.Attributes.Add(New MapItemAttribute() With {.Name = "Label", .Value = name, .Type = GetType(String)})
                IconST.Attributes.Add(New MapItemAttribute() With {.Name = "Name", .Value = name, .Type = GetType(String)})
                IconST.BackgroundDrawingMode = ElementState.None

                IconST.Visible = False
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := CreateLayerStatus")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create map layer status")

                ModMainApp.Log.Log4N("CreateLayerStatus [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

            Return IconST
        End Function
        ''' <exclude />
        Public Sub UpdateFullIcon(ByVal ItmeMapRec As MapItem, ByVal fullSt As Boolean)

            Try
                Dim RecItem As New MapRectangle()
                RecItem = CType(ItmeMapRec, MapRectangle)
                Dim color1 As Color = CType(RecItem.Attributes("ColorProp").Value, Color)
                Dim color2 As Color = Color.FromArgb(255 - color1.R, 255 - color1.G, 255 - color1.B)
                Dim linearizedPower As Integer
                Dim Pcapacity As Integer = CType(ItmeMapRec.Attributes("Capacity").Value, Integer)
                linearizedPower = CInt(calRangeColor(Pcapacity))
                If (fullSt) Then
                    RecItem.Fill = colors_ST(linearizedPower)
                    RecItem.HighlightedFill = Color.LightGray
                    RecItem.HighlightedStroke = Color.LightGray
                    RecItem.Stroke = Color.LightGray
                    RecItem.HighlightedStrokeWidth = 10
                    RecItem.StrokeWidth = 10
                Else
                    If (CType(RecItem.Attributes("Label").Value, String) = "Other") Then
                        RecItem.Fill = Color.Gray
                        'RecItem.Stroke = color2
                        RecItem.HighlightedFill = colors_ST(linearizedPower)
                        RecItem.HighlightedStroke = color1
                        RecItem.HighlightedStrokeWidth = 5
                        RecItem.StrokeWidth = 2
                    Else
                        RecItem.Fill = colors_ST(linearizedPower)
                        'RecItem.Stroke = color2
                        RecItem.HighlightedFill = colors_ST(linearizedPower)
                        RecItem.HighlightedStroke = color1
                        RecItem.HighlightedStrokeWidth = 5
                        RecItem.StrokeWidth = 2
                    End If

                End If
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := UpdateFullIcon")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create full icon")

                ModMainApp.Log.Log4N("UpdateFullIcon [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

        End Sub
        Public Function CreateColorizer() As MapColorizer
            Dim colorizer As New ChoroplethColorizer()
            Try
                colorizer.RangeStops.AddRange(New List(Of Double) From {0, 25, 50, 75, 100})
                colorizer.ColorItems.AddRange(New List(Of ColorizerColorItem) From {
                New ColorizerColorItem(Color.DarkRed),
                New ColorizerColorItem(Color.Red),
                New ColorizerColorItem(Color.GreenYellow),
                New ColorizerColorItem(Color.LimeGreen),
                New ColorizerColorItem(Color.Green)
            })
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := CreateColorizer")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create map colorizer")

                ModMainApp.Log.Log4N("CreateColorizer [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

            Return colorizer
        End Function

        ''' <summary>Function use to create map legend</summary>
        Public Function CreateLegend(ByVal layer As MapItemsLayerBase, ByVal idOfField As Integer) As MapLegendBase
            Dim legend As New ColorScaleLegend()
            Try

                legend.HeaderStyle.Font = FontsCollection("mainArea")
                legend.Header = "W / C Ranges ( % ลาน : " & idOfField & " )"
                legend.Layer = layer
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := CreateLegend")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create map legend")

                ModMainApp.Log.Log4N("CreateLegend [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

            Return legend
        End Function

        ''' <summary>Function use to create icon unload local</summary>
        Public Function createUnloadLocalItem(ByVal UnloadLocal As MapCustomElement, ByVal AreaID As Integer) As MapCustomElement
            Try
                Dim CustomItemElement As New MapCustomElement()
                CustomItemElement = UnloadLocal
                With CustomItemElement
                    '.Location = New GeoPoint(8, -29) Y,X
                    If (AreaID = 1) Then
                        .Location = New GeoPoint(25, -22)
                    ElseIf (AreaID = 2) Then
                        .Location = New GeoPoint(25, -18)
                    ElseIf (AreaID = 3) Then
                        .Location = New GeoPoint(25, -20)
                    End If
                    .Text = "Unload"
                    .Font = FontsCollection("mainArea")
                End With

                Dim image As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("logistic_icon"), Bitmap)
                UnloadLocal.Image = New Bitmap(image, New Size(50, 50))
                UnloadLocal.Attributes.Add(New MapItemAttribute() With {.Name = "ID", .Value = 1, .Type = GetType(Integer)})
                UnloadLocal.Attributes.Add(New MapItemAttribute() With {.Name = "Name", .Value = "Unload", .Type = GetType(String)})
                UnloadLocal.Attributes.Add(New MapItemAttribute() With {.Name = "Label", .Value = "Unload", .Type = GetType(String)})
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := createUnloadLocalItem")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create icon unload local")

                ModMainApp.Log.Log4N("createUnloadLocalItem [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

            Return UnloadLocal
        End Function
        ''' <summary>Function use to create icon unload import</summary>
        Public Function createUnloadImportItem(ByVal UnloadImport As MapCustomElement, ByVal AreaID As Integer) As MapCustomElement
            Try
                Dim CustomItemElement As New MapCustomElement()
                CustomItemElement = UnloadImport
                With CustomItemElement
                    '.Location = New GeoPoint(8, 5)
                    '.Location = New GeoPoint(25, 0)
                    If (AreaID = 1) Then
                        .Location = New GeoPoint(25, 5)
                    ElseIf (AreaID = 2) Then
                        .Location = New GeoPoint(25, 5)
                    ElseIf (AreaID = 3) Then
                        .Location = New GeoPoint(25, 5)
                    End If
                    .Location = New GeoPoint(25, 5) 'Y,X
                    .Text = "Unload Import"
                    .Font = FontsCollection("mainArea")
                End With

                Dim image As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("import_icon"), Bitmap)
                UnloadImport.Image = New Bitmap(image, New Size(50, 50))
                UnloadImport.Attributes.Add(New MapItemAttribute() With {.Name = "ID", .Value = 2, .Type = GetType(Integer)})
                UnloadImport.Attributes.Add(New MapItemAttribute() With {.Name = "Name", .Value = "UnloadImport", .Type = GetType(String)})
                UnloadImport.Attributes.Add(New MapItemAttribute() With {.Name = "Label", .Value = "UnloadImport", .Type = GetType(String)})
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := createUnloadImportItem")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create icon unload import")

                ModMainApp.Log.Log4N("createUnloadImportItem [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try

            Return UnloadImport
        End Function

        ''' <summary>Function use to get mapcontrol zoom level</summary>
        Public Function getZoomLavel(ByVal ObjectmapControl As MapControl) As Double
            Dim zoom_lavel As Double = 0
            zoom_lavel = CDbl(ObjectmapControl.ZoomLevel)
            Return zoom_lavel
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับแสดงสี ของสถานะพื้นที่กองเก็บ</summary>
        ''' <param name="capacity">Capacity of area</param>
        ''' <remarks>0 : 0-24<see cref=" 25-49">, 1 </see>, 2 : 50-74<see cref=" 75-99">, 3 </see>, 4 : &gt;=100%</remarks>
        ''' <returns>Integer</returns>
        Public Function calRangeColor(ByVal capacity As Double) As Integer
            'Math.Round((32.635), 2, MidPointRounding.AwayFromZero)
            Dim linearizedPower As Integer
            Dim truncatedResult As Double = 0
            If (capacity >= 0 And capacity < 25) Then
                linearizedPower = 0
            ElseIf (capacity >= 25 And capacity < 50) Then
                linearizedPower = 1
            ElseIf (capacity >= 50 And capacity < 75) Then
                linearizedPower = 2
            ElseIf (capacity >= 75 And capacity < 100) Then
                linearizedPower = 3
            ElseIf (capacity >= 100) Then
                linearizedPower = 4
            Else
                linearizedPower = 0
            End If
            Return linearizedPower
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับแสดงข้อมูล น้ำหนักวัตถุดิบรวมของแต่ละลาน</summary>
        Public Function GetSumAreaWeight(ByVal FieldID As Integer) As Double
            Dim result As Double = 0
            Try
                Dim DS_GetSumAreaWeight As DataSet = mod_IVM_DB.func_IVM_Get_Area_Info(FieldID)
                Dim DT_Area_Info As DataTable = DS_GetSumAreaWeight.Tables(1)
                result = DataHelper.DBNullOrNothingTo(Of Double)(DT_Area_Info.Rows(0).Item("TotalWeight"), 0)
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := func_IVM_Get_Area_Info")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "ฟังก์ชั่น สำหรับแสดงข้อมูล น้ำหนักวัตถุดิบรวมของแต่ละลาน")

                ModMainApp.Log.Log4N("GetSumAreaWeight [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try
            Return result
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับแสดงข้อมูล % น้ำหนักรวม ของแต่ละลาน</summary>
        Public Function GetPAreaWeight(ByVal FieldID As Integer) As Double
            Dim result As Double = 0
            Try
                Dim DS_GetSumPAreaWeight As DataSet = mod_IVM_DB.func_IVM_Get_Area_Info(FieldID)
                Dim DT_Area_Info As DataTable = DS_GetSumPAreaWeight.Tables(1)
                result = DataHelper.DBNullOrNothingTo(Of Double)(DT_Area_Info.Rows(0).Item("InUseCapcity"), 0)
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := GetPAreaWeight")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "ฟังก์ชั่น สำหรับแสดงข้อมูล % น้ำหนักรวม ของแต่ละลาน")

                ModMainApp.Log.Log4N("GetPAreaWeight [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try
            Return result
        End Function
        ''' <summary>Function use to create map overlay</summary>
        Public Sub CreateOverlay(ByVal map As MapControl, ByVal DTUpdate As DateTime, ByVal FieldID As Integer)
            'backImage_Renamed = New MapOverlayImageItem() With {.Padding = New Padding(5), .ImageUri = New Uri("D:\Projects\1454-Inventory_Manage_V1.0.0\InventoryMovement\bin\Debug\Data\Images\import_icon.png")}
            'mapcontrolName_Renamed = New MapOverlayTextItem() With {.Padding = New Padding(15)}
            'mapcontrolName_Renamed.TextStyle.Font = FontsCollection("desc")
            'overlay_Renamed = New MapOverlay() With {.Alignment = ContentAlignment.TopLeft, .Margin = New Padding(10, 10, 0, 0)}
            'overlay_Renamed.BackgroundStyle.Fill = Color.Transparent
            'overlay_Renamed.Items.AddRange(New MapOverlayItemBase() {backImage_Renamed, mapcontrolName_Renamed})
            Try
                Dim tmptimeUPD As DateTime
                tmptimeUPD = CDate(DTUpdate)
                Dim AreaWeight As Double = GetSumAreaWeight(FieldID)
                Dim PAreaWeight As Double = GetPAreaWeight(FieldID)

                Dim overlayWithText As MapOverlay = New MapOverlay With
                    {.Alignment = ContentAlignment.TopRight, .JoiningOrientation = Orientation.Vertical,
                    .Margin = New Padding(10, 10, 0, 0), .Padding = New Padding(7)}
                Dim image As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("Synchronize_Icon"), Bitmap)
                backImage_Renamed = New MapOverlayImageItem() With {.Padding = New Padding(5), .Image = image}
                'overlayWithText.BackgroundStyle.Fill = Color.LightSkyBlue D:\Projects\IVM\FC.IVM.Bus\bin\Debug\Data\Icons
                'mapcontrolName_Renamed = New MapOverlayTextItem() With {.Text = "Last synchronize data" & vbCrLf & tmptimeUPD & vbCrLf &
                '                   " Total weight : " & AreaWeight.ToString("0,##0"), .TextAlignment = ContentAlignment.TopCenter}

                mapcontrolName_Renamed = New MapOverlayTextItem() With {.Text = AreaWeight.ToString("0,##0") &
                    " (ตัน)" & vbCrLf & PAreaWeight.ToString("##0.00") & " (%)" & vbCrLf & vbCrLf &
                    "Last synchronize data" & vbCrLf & tmptimeUPD, .TextAlignment = ContentAlignment.TopCenter}
                mapcontrolName_Renamed.TextStyle.Font = FontsCollection("mainArea")

                overlayWithText.Items.AddRange(New MapOverlayItemBase() {backImage_Renamed, mapcontrolName_Renamed})
                map.Overlays.Add(overlayWithText)

                '+++++++++++++++++++++++++++++++++++++++Close page icon+++++++++++++++++++++++++++++++++++++++++++++
                Dim overlayWithTextClose As MapOverlay = New MapOverlay With
                    {.Alignment = ContentAlignment.BottomRight, .JoiningOrientation = Orientation.Horizontal,
                    .Margin = New Padding(0, 0, 20, -55), .Padding = New Padding(1)}
                Dim image_Close As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("CloseForm"), Bitmap)
                closeImage_Renamed = New MapOverlayImageItem() With {.Padding = New Padding(5), .Image = image_Close}
                mapcontrolClose_Form = New MapOverlayTextItem() With {.Text = "Close", .TextAlignment = ContentAlignment.MiddleLeft, .Alignment = ContentAlignment.MiddleRight}
                mapcontrolClose_Form.TextStyle.Font = FontsCollection("mainArea")

                overlayWithTextClose.Items.AddRange(New MapOverlayItemBase() {closeImage_Renamed})
                map.Overlays.Add(overlayWithTextClose)

                '+++++++++++++++++++++++++++++++++++++++Rollback icon+++++++++++++++++++++++++++++++++++++++++++++
                Dim overlayWithTextRollback As MapOverlay = New MapOverlay With
                    {.Alignment = ContentAlignment.BottomRight, .JoiningOrientation = Orientation.Horizontal,
                    .Margin = New Padding(0, 0, 20, -55), .Padding = New Padding(1)}
                Dim image_Rollback As Bitmap = CType(ModMainApp.GetResourceObject(Of Image)("RollbackForm"), Bitmap)
                rollbackImage_Renamed = New MapOverlayImageItem() With {.Padding = New Padding(5), .Image = image_Rollback}
                mapcontrolRollback_Form = New MapOverlayTextItem() With {.Text = "Rollback", .TextAlignment = ContentAlignment.MiddleLeft, .Alignment = ContentAlignment.MiddleRight}
                mapcontrolRollback_Form.TextStyle.Font = FontsCollection("mainArea")

                overlayWithTextRollback.Items.AddRange(New MapOverlayItemBase() {rollbackImage_Renamed})
                map.Overlays.Add(overlayWithTextRollback)
            Catch ex As Exception
                Infolog.ClearMessage()
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := CreateOverlay")
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create map overlay")

                ModMainApp.Log.Log4N("CreateOverlay [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
            End Try
        End Sub
        ''' <exclude />
        Public Sub SetInfoMessage(ByVal text As String)
            'DataHelper.DBNullOrNothingTo(Of String)(text, "")
            mapcontrolName_Renamed.Text = DataHelper.DBNullOrNothingTo(Of String)(text, String.Empty)
        End Sub
        ''' <summary>Function use to show area inuse capacity</summary>
        Public Function prePareInfo(ByVal Name As String, ByVal Inusecapacity As Double) As String
            Dim StringItmeInfo As New StringBuilder
            StringItmeInfo.Append(Name & Environment.NewLine & Environment.NewLine).AppendLine()
            StringItmeInfo.AppendFormat("(%) W/C    {0:0.00}", Inusecapacity).AppendLine()
            Return StringItmeInfo.ToString
        End Function


        'Public Function genStorageMatDetailData(DT_MatData As DataTable) As String
        '    Dim res As String = String.Empty
        '    Dim storagename As String = String.Empty
        '    Dim matname As String = String.Empty
        '    Dim matbalance As Double = 0
        '    Dim matweight As Double = 0
        '    Try
        '        For i As Integer = 0 To DT_MatData.Rows.Count - 1
        '            storagename = CType(DT_MatData.Rows(i).Item("StorageName"), String)
        '            matname = CType(DT_MatData.Rows(i).Item("MaterialName"), String)
        '            matbalance = CType(DT_MatData.Rows(i).Item("Quantity"), Double)
        '            matweight = CType(DT_MatData.Rows(i).Item("WeightADT"), Double)

        '            res &= prePareMatInfo(storagename, matname, matbalance, matweight)
        '        Next
        '    Catch ex As Exception
        '        Infolog.ClearMessage()
        '        Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus Error")
        '        Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "File name := mod_IVM_Map_Function")
        '        Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.InfoMesage, "Function name := genStorageMatDetailData")
        '        Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Function use to create map overlay")

        '        ModMainApp.Log.Log4N("genStorageMatDetailData [Catch]").DebugFormat("Err detail := {0} ", ex.Message)
        '    End Try

        '    Return res
        'End Function

        'Public Function prePareMatInfo(ByVal StorageName As String, ByVal MatName As String,
        '                    ByVal Balance As Double, ByVal Weight As Double) As String

        '    Dim StringMatInfo As New StringBuilder
        '    StringMatInfo.Append("วัตถุดิบ    {MatName}    ")
        '    StringMatInfo.Replace("{MatName}", MatName)
        '    StringMatInfo.AppendFormat("จำนวน    {0:0}    ", Balance)
        '    StringMatInfo.AppendFormat("น้ำหนัก    {0:0.000}    ", Weight).AppendLine()

        '    Return StringMatInfo.ToString
        'End Function

        ''' <exclude />
        Public ReadOnly Property Overlay() As MapOverlay
            Get
                Return overlay_Renamed
            End Get
        End Property
        ''' <exclude />
        Public ReadOnly Property BackImage() As MapOverlayImageItem
            Get
                Return backImage_Renamed
            End Get
        End Property
        ''' <exclude />
        Public ReadOnly Property MapName() As MapOverlayTextItem
            Get
                Return mapcontrolName_Renamed
            End Get
        End Property
        ''' <exclude />
        Public Function GetOverlays() As MapOverlay()
            Return New MapOverlay() {overlay_Renamed}
        End Function

    End Module

End Namespace
