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
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors

Namespace Forms
    ''' <summary>Windows form use to rollback tentative data.</summary>
    ''' <remarks>หน้าจอสำหรับแก้ไขข้อมูล การ Unload, ย้าย, ตัดจ่าย วัตถุดิบ</remarks>
    Public Class frm_IVM_Rollback
        Implements IChildOfMainForm

        ''' <exclude />
        Public Event OnPassData As IChildOfMainForm.OnPassDataEventHandler Implements IChildOfMainForm.OnPassData
        ''' <exclude />
        Dim userAccessSite As Integer = 0
        'Public _rootAreaID As Integer
        ''' <exclude />
        Dim idOfField As Integer = 0 ' อาจย้ายไปประกาศตัวแปรระดับ form เพื่อเอาไปใช้ที่ส่วนอื่นๆใน form
        ''' <exclude />
        Dim tmpTapAction As Integer = 1 ' Tab rollback unload

        ''' <exclude />
        Dim DS_Tentative_Unload_Data As New DataSet
        ''' <exclude />
        Dim DT_Tentative_Unload_Data As New DataTable
        ''' <exclude />
        Dim DT_Tentative_Movement_Data As New DataTable
        ''' <exclude />
        Public _fieldID As String = ""
        Dim Dt_tmpUnloadTentativeTransac As New DataTable
        Dim Dt_tmpMovementTentativeTransac As New DataTable
        Dim foundRowUnload As DataRow() = Nothing
        Dim foundRowMovement As DataRow() = Nothing
        ''' <exclude />
        Public Sub OnBeforeFormLoad(param As Object) Implements IChildOfMainForm.OnBeforeFormLoad

            If Not IsNothing(param) AndAlso TypeOf param Is List(Of PropertyWithValue) Then
                Dim lstOfPropertyWithValue As List(Of PropertyWithValue) = CType(param, List(Of PropertyWithValue))
                For Each item As PropertyWithValue In lstOfPropertyWithValue
                    If item.Name = "Area_ID" Then
                        idOfField = CInt(item.Value)
                    End If
                Next
            End If
        End Sub

        ''' <exclude />
        Public Sub OnClickClear(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickClear

        End Sub

        ''' <exclude />
        Public Sub OnClickCustomButton(customButtonName As String, ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickCustomButton

        End Sub

        ''' <exclude />
        Public Sub OnClickReload(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickReload

        End Sub

        ''' <exclude />
        Public Sub OnClickSave(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickSave
            'Dim strValue As String = ModMainApp.GetResourceMessage("Test")
            'Dim deployConfigValue As String = ModMainApp.DeployConfigDll.GetConfig("IVM", "App_Data_Mode")
            'Dim appConfigValue As String = ModMainApp.AppConfigDll.GetConfig("System", "Program_Name")
            'Dim userConfigValue As String = ModMainApp.MainAppConfigFile.ReadAppSetting("IVMConfig1")
        End Sub

        ''' <exclude />
        Dim isLockRow As Boolean = False

        Private Sub frm_IVM_Rollback_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                Me.FormBorderStyle = FormBorderStyle.None

                Dim display_member As String = ""
                Dim value_member As String = ""
                Dim show_column_names As String = ""
                btnClose.Visible = False
                '+++++++++++++++++++++++++ X10 Combo Repository With role +++++++++++++++++++++ IVM Access Site -> ID
                Dim dt_Access_Site As DataTable = ComboBoxRepository.GetDataTableOfMasterCheckedRole("IVM Site Data -> ID", display_member, value_member, show_column_names)
                WinDevHelper.InitGridLookUpEditTemplateProperties(cboSite, True)
                WinDevHelper.InitDataSourceGridLookUpEdit(cboSite, dt_Access_Site, display_member, value_member, show_column_names.Split(","c))

                'AddHandler GridView_Unload_Tentative.RowStyle, AddressOf GridView_Unload_Tentative_RowStyle

                Dim foreColor As Color = Color.Brown
                Dim backColor As Color = Color.LightGreen

                If (_fieldID <> "") Then

                    idOfField = CInt(_fieldID)

                    btnClose.Visible = True

                    Select Case CInt(_fieldID)
                        Case 1
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('2','3')  or ParentStorageID IS NULL") 'only site 1
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('2','3','0')  or ParentStorageID IS NULL") 'only site 1
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    'ถ้า parentID =0 ให้เช็คต่อว่า DestinationGroupID เป็นพื้นที่ของลานหรือไม่?
                                    'call [func_IVM_Get_ChildStorageData] โดยส่ง ID ลานปัจจุบัน เป็นพารามิเตอร์
                                    'ใช้ DestinationGroupID เป็นพารามิเตอร์ในการค้นหา ใน DT 
                                    'ถ้าไม่เจอข้อมูล แสดงว่าเป็น จาก Share ไปพื้นที่ลานอื่น ลบ row ออกไม่ต้องแสดงผล
                                    Dim parent_st_id As Integer = CType(row.Item("ParentStorageID"), Integer)
                                    If (parent_st_id > 0) Then
                                        'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                        row.Delete()
                                    Else
                                        Dim dest_st_group_id As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(row.Item("DestinationGroupID"), 999999)
                                        Dim DT_Temp_Check_Area As DataTable = GetGroupAreaInfo(CInt(_fieldID))
                                        Dim foundRowCheckArea As DataRow() = DT_Temp_Check_Area.Select("GroupID =" & dest_st_group_id)

                                        If (foundRowCheckArea.Length <= 0) Then
                                            'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                            row.Delete()
                                        End If
                                    End If
                                Next
                            End If
                            GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                            initLookupDestinationArea()
                        Case 2
                            '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','3') or ParentStorageID IS NULL") 'only site 2
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','3','0')  or ParentStorageID IS NULL") 'only site 2
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    'ถ้า parentID =0 ให้เช็คต่อว่า DestinationGroupID เป็นพื้นที่ของลานหรือไม่?
                                    'call [func_IVM_Get_ChildStorageData] โดยส่ง ID ลานปัจจุบัน เป็นพารามิเตอร์
                                    'ใช้ DestinationGroupID เป็นพารามิเตอร์ในการค้นหา ใน DT 
                                    'ถ้าไม่เจอข้อมูล แสดงว่าเป็น จาก Share ไปพื้นที่ลานอื่น ลบ row ออกไม่ต้องแสดงผล
                                    Dim parent_st_id As Integer = CType(row.Item("ParentStorageID"), Integer)
                                    If (parent_st_id > 0) Then
                                        'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                        row.Delete()
                                    Else
                                        Dim dest_st_group_id As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(row.Item("DestinationGroupID"), 999999)
                                        Dim DT_Temp_Check_Area As DataTable = GetGroupAreaInfo(CInt(_fieldID))
                                        Dim foundRowCheckArea As DataRow() = DT_Temp_Check_Area.Select("GroupID =" & dest_st_group_id)

                                        If (foundRowCheckArea.Length <= 0) Then
                                            'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                            row.Delete()
                                        End If
                                    End If
                                Next
                            End If
                            GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                            initLookupDestinationArea()
                        Case 3
                            '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','2')  or ParentStorageID IS NULL") 'only site 3
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac
                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','2','0')  or ParentStorageID IS NULL") 'only site 3
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    'ถ้า parentID =0 ให้เช็คต่อว่า DestinationGroupID เป็นพื้นที่ของลานหรือไม่?
                                    'call [func_IVM_Get_ChildStorageData] โดยส่ง ID ลานปัจจุบัน เป็นพารามิเตอร์
                                    'ใช้ DestinationGroupID เป็นพารามิเตอร์ในการค้นหา ใน DT 
                                    'ถ้าไม่เจอข้อมูล แสดงว่าเป็น จาก Share ไปพื้นที่ลานอื่น ลบ row ออกไม่ต้องแสดงผล
                                    Dim parent_st_id As Integer = CType(row.Item("ParentStorageID"), Integer)
                                    If (parent_st_id > 0) Then
                                        'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                        row.Delete()
                                    Else
                                        Dim dest_st_group_id As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(row.Item("DestinationGroupID"), 999999)
                                        Dim DT_Temp_Check_Area As DataTable = GetGroupAreaInfo(CInt(_fieldID))
                                        Dim foundRowCheckArea As DataRow() = DT_Temp_Check_Area.Select("GroupID =" & dest_st_group_id)

                                        If (foundRowCheckArea.Length <= 0) Then
                                            'ถ้าเป็นพื้นที่ของลานอื่นให้ลบออก ไม่แสดงข้อมูล
                                            row.Delete()
                                        End If
                                    End If

                                Next
                            End If
                            GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data
                            initLookupDestinationArea()
                    End Select

                    cboSite.EditValue = CInt(_fieldID)
                    cboSite.ReadOnly = True

                End If

                LayoutControlGroupUnload.Selected = True
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [frm_IVM_Rollback_Load]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>Prepare material data for fill into lookup editor</summary>
        Private Sub initLookupDestinationArea()
            Try
                'ไม่ต้องเอา ไอดีลาน มาเป็นเงื่อนไขในการคิวรี่ข้อมูล เพราะจะทำให้ไม่สามารถแสดงชื่อพื้นที่กองเก็บ กรณีมีการทำรายการข้ามลาน
                RepositoryDestinationStorage.NullValuePrompt = "พื้นที่ปลายทาง"

                RepositoryDestinationStorage.DataSource = func_IVM_Get_LookUp_Area_Data()
                RepositoryDestinationStorage.ValueMember = "ID"
                RepositoryDestinationStorage.DisplayMember = "Name"

                RepositoryItemLookUpEditSouce.NullValuePrompt = "พื้นที่ต้นทาง"

                RepositoryItemLookUpEditSouce.DataSource = func_IVM_Get_LookUp_Area_Data()
                RepositoryItemLookUpEditSouce.ValueMember = "ID"
                RepositoryItemLookUpEditSouce.DisplayMember = "Name"

                RepositoryItemLookUpEditMaterial.DataSource = func_IVM_Getmaterial()
                RepositoryItemLookUpEditMaterial.ValueMember = "ID"
                RepositoryItemLookUpEditMaterial.DisplayMember = "Name"

                'RepositoryItemLookUpEditMaterialUnload
                RepositoryItemLookUpEditMaterialUnload.DataSource = func_IVM_Getmaterial()
                RepositoryItemLookUpEditMaterialUnload.ValueMember = "ID"
                RepositoryItemLookUpEditMaterialUnload.DisplayMember = "Name"
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [initLookupDestinationArea]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>Get tentative data from database.</summary>
        ''' <remarks>Fill into dataset and datatable</remarks>
        Public Sub GetTentativeTransacData()
            Try
                '++++++++++++++++++++ Prepare tentative dataset +++++++++++++++++
                DS_Tentative_Unload_Data = func_IVM_Get_Tentative_Data()
                '++++++++++++++++++++ Prepare tentative datatable +++++++++++++++++
                DT_Tentative_Unload_Data = DS_Tentative_Unload_Data.Tables(0)
                DT_Tentative_Movement_Data = DS_Tentative_Unload_Data.Tables(1)
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetTentativeTransacData]")
                Infolog.ShowExMessage(ex, M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub btnRollbackData_Click(sender As Object, e As EventArgs) Handles btnRollbackData.Click
            Dim Rows_Value As New ArrayList()
            Dim Rows_Value_ID As New ArrayList()
            Dim Rows_Value_Desc_GroupID As New ArrayList()
            Dim Dest_StorageID As New ArrayList()
            Dim MatID As New ArrayList()
            Dim Bale_Rollback As New ArrayList()
            Dim porc_Res As Integer = 0
            Dim DT_Check_Mat_Bale As New DataTable
            Dim foundRow As DataRow() = Nothing
            Dim DT_Source_Rollback_Data_Name As New DataTable
            Dim Storage_Name As DataRow() = Nothing
            Dim SN As String = ""
            Dim Source_Rollback_Data_ID As Integer = 0
            Try
                ' Add the selected rows to the list.
                If (tmpTapAction = 1) Then '***Unload material
                    Dim selectedRowHandles As Int32() = DataHelper.DBNullOrNothingTo(Of Int32())(GridView_Unload_Tentative.GetSelectedRows(), 0)
                    Dim I As Integer
                    For I = 0 To selectedRowHandles.Length - 1
                        Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        If (selectedRowHandle >= 0) Then
                            Rows_Value.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("ID").ToString)
                        End If
                    Next

                    If (WinUtil.ShowQuestionBox("ต้องการ Rollback ข้อมูลที่เลือก ใช่หรือไม่?", "ลบข้อมูล") = DialogResult.Yes) Then
                        For icount = 0 To Rows_Value.Count - 1
                            porc_Res = func_IVM_Rollback_Tentative_Data(CInt(Rows_Value(icount)), tmpTapAction, UserId)
                            GridView_Unload_Tentative.BeginSort()
                            Try
                                GridView_Unload_Tentative.DeleteSelectedRows()
                            Finally
                                GridView_Unload_Tentative.EndSort()
                            End Try
                            'GridView_Unload_Tentative.ClearSelection()
                            GridView_Unload_Tentative.RefreshData()
                        Next
                        WinDevHelper.ShowAlertWindowSaveCompleted(Me, "เสร็จสมบูรณ์", "Rollback Successfully.")
                    End If
                ElseIf (tmpTapAction = 2) Then '***Movement material
                    Dim selectedRowHandles As Int32() = DataHelper.DBNullOrNothingTo(Of Int32())(GridView_Movement_Tentative.GetSelectedRows(), 0)
                    Dim I As Integer
                    For I = 0 To selectedRowHandles.Length - 1
                        Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        If (selectedRowHandle >= 0) Then
                            Rows_Value.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("ID").ToString)
                        End If
                    Next

                    If (WinUtil.ShowQuestionBox("ต้องการ Rollback ข้อมูลที่เลือก ใช่หรือไม่?", "ลบข้อมูล") = DialogResult.Yes) Then
                        For icount = 0 To Rows_Value.Count - 1
                            porc_Res = func_IVM_Rollback_Tentative_Data(CInt(Rows_Value(icount)), tmpTapAction, UserId)
                            GridView_Movement_Tentative.BeginSort()
                            Try
                                GridView_Movement_Tentative.DeleteSelectedRows()
                            Finally
                                GridView_Movement_Tentative.EndSort()
                            End Try
                            GridView_Movement_Tentative.RefreshData()
                            '+++++++++++++++ Log ++++++++++++++++
                            'ModMainApp.Log.Log4N("Rollback tentative data").DebugFormat("User ID := {0} ReferenceID := {1} RollBackTypeID := {2} Area := {3} Date := {4}", UserId.ToString, Rows_Value(icount).ToString & "[" & Rows_Value_ID(icount).ToString & "]", "Movement", idOfField, DateTime.Now)
                            '++++++++++++++++++++++++++++++++++++
                        Next
                        WinDevHelper.ShowAlertWindowSaveCompleted(Me, "เสร็จสมบูรณ์", "Rollback Successfully.")
                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [btnRollbackData_Click]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
                ModMainApp.Log.Log4N("btnRollbackData_Click [Catch]").DebugFormat("Err := {0} ", ex.Message)
            End Try
        End Sub

        ''' <exclude />
        Private Sub LayoutControlGroupUnload_Shown(sender As Object, e As EventArgs) Handles LayoutControlGroupUnload.Shown
            tmpTapAction = 1
        End Sub

        ''' <exclude />
        Private Sub LayoutControlGroupMovement_Shown(sender As Object, e As EventArgs) Handles LayoutControlGroupMovement.Shown
            tmpTapAction = 2
        End Sub

        ''' <exclude />
        Private Sub DeleteSelectedRows(ByVal View As GridView)
            Dim Row As DataRow
            Dim Rows() As DataRow
            Dim I As Integer
            Dim selectedRowHandles As Int32() = View.GetSelectedRows()

            ReDim Rows(selectedRowHandles.Count - 1)
            For I = 0 To View.SelectedRowsCount - 1
                Rows(I) = View.GetDataRow(View.GetSelectedRows(I))
            Next
            View.BeginSort()
            Try
                For Each Row In Rows
                    Row.Delete()
                Next
            Finally
                View.EndSort()
            End Try
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        Private Sub GridView_Unload_Tentative_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GridView_Unload_Tentative.SelectionChanged
            Try
                Dim Rows_Value As New ArrayList()
                Dim Rows_Value_ID As New ArrayList()
                Dim Parent_ID As New ArrayList()
                Dim Rows_Value_Desc_GroupID As New ArrayList()
                Dim Dest_StorageID As New ArrayList()
                Dim MatID As New ArrayList()
                Dim Bale_Rollback As New ArrayList()
                Dim foundRow As DataRow() = Nothing
                Dim foundQTY As DataRow()
                Dim foundAllTicket As DataRow()
                Dim DT_Check_Mat_Bale As New DataTable

                If (GridView_Unload_Tentative.IsRowSelected(e.ControllerRow)) Then
                    Dim getFocusRow As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(GridView_Unload_Tentative.GetRowHandle(GridView_Unload_Tentative.GetFocusedDataSourceRowIndex), -1)
                    Rows_Value.Add(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("ID").ToString)
                    Rows_Value_ID.Add(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("Ticket").ToString)
                    Dim tmpParent_ID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("ParentStorageID"), "-1")
                    'NULL = 999999 Other
                    Dim tmpDes_Group_ID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("DestinationGroupID"), "999999")
                    Rows_Value_Desc_GroupID.Add(tmpDes_Group_ID)
                    Dest_StorageID.Add(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("DestinationStorageID").ToString)
                    MatID.Add(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("MaterialID").ToString)
                    Bale_Rollback.Add(DataHelper.DBNullOrNothingTo(Of Decimal)(GridView_Unload_Tentative.GetDataRow(getFocusRow).Item("Amount"), 0))

                    'If (tmpDes_Group_ID = "999999") Then
                    '    DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(999999, idOfField)
                    'Else
                    '    DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(tmpDes_Group_ID), idOfField)
                    'End If

                    DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(tmpDes_Group_ID), idOfField)

                    foundRow = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(0).ToString & "' and MaterialID='" & MatID(0).ToString & "' and Quantity >=" & CDec(Bale_Rollback(0)))

                    ModMainApp.Log.Log4N("Check bel rollback").DebugFormat("StorageID := {0},MaterialID := {1}, Quantity := {2}, found.lenght := {3}",
                  Dest_StorageID(0).ToString, MatID(0).ToString, CDec(Bale_Rollback(0)).ToString, foundRow.Length.ToString)

                    If (foundRow.Count <= 0) Then
                        foundQTY = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(0).ToString & "' and MaterialID='" & MatID(0).ToString & "' and Quantity < " & CDec(Bale_Rollback(0)))
                        Dim DestAreaName As String = Dest_StorageID(0).ToString
                        Dim gv As GridView = GridView_Unload_Tentative
                        Dim ColDesArea As GridColumn = gv.Columns("DestinationStorageID")
                        Dim QTY As GridColumn = gv.Columns("Amount")
                        Dim colTicket As GridColumn = gv.Columns("Ticket")
                        Dim DesQTY As String = "0.0"

                        If (foundQTY.Count > 0) Then
                            DesQTY = DataHelper.DBNullOrNothingTo(Of String)(foundQTY(0).ItemArray(6), "0.0")

                            ModMainApp.Log.Log4N("Check bel rollback").DebugFormat("StorageID := {0},MaterialID := {1}, DesQTY := {2}, found.lenght := {3}",
                  Dest_StorageID(0).ToString, MatID(0).ToString, CDec(DesQTY).ToString, foundQTY.Length.ToString)

                        End If
                        Dim CurrArea As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(ColDesArea), "-")
                        Dim CurrQTY As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(QTY), "0.0")

                        Dim iri As Integer
                        Dim List_Dest_Area As New ArrayList()
                        Dim List_Mat_Id As New ArrayList()
                        For iri = 0 To GridView_Unload_Tentative.RowCount - 1
                            Dim isCheck As Boolean = CType(GridView_Unload_Tentative.GetRowCellValue(iri, "DX$CheckboxSelectorColumn"), Boolean)
                            If (isCheck) Then
                                Dim gridFormatRule As New GridFormatRule()
                                Dim formatConditionRuleValue As New FormatConditionRuleValue()
                                gridFormatRule.Column = colTicket
                                gridFormatRule.ApplyToRow = True
                                formatConditionRuleValue.PredefinedName = "Red Fill, Red Text"

                                Dim tmpTicket As String = GridView_Unload_Tentative.GetRowCellValue(iri, "Ticket").ToString
                                If (tmpTicket = Rows_Value_ID(0).ToString) Then
                                    GridView_Unload_Tentative.UnselectRow(iri)
                                    formatConditionRuleValue.Value1 = Rows_Value_ID(0).ToString
                                    formatConditionRuleValue.Condition = FormatCondition.Equal
                                    gridFormatRule.Rule = formatConditionRuleValue
                                    GridView_Unload_Tentative.FormatRules.Add(gridFormatRule)
                                End If
                            End If
                        Next

                        Dim tmpMessage As String = "ตั๋วชั่ง [" & Rows_Value_ID(0).ToString & "] วัตถุดิบในพื้นที่ [" & CurrArea & "] := " & DesQTY & "(" & CurrQTY & ") ไม่เพียงพอในการ Rollback"
                        WinUtil.ShowWarningBox(tmpMessage, "พื้นที่ [" & CurrArea & "] จำนวนวัตถุดิบ [" & CurrQTY & "]")

                    Else 'เช็คว่ามีการโหลด วัตถุดิบตั๋วชั่งเดียวกันไว้มากกว่า 1 พื้นหที่หรือไม่
                        Dim countAllGridRow As Integer = GridView_Unload_Tentative.RowCount
                        For iCount As Integer = 0 To countAllGridRow - 1

                            Dim TicketID As String = GridView_Unload_Tentative.GetDataRow(iCount).Item("Ticket").ToString
                            Dim StorageID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Unload_Tentative.GetDataRow(iCount).Item("DestinationStorageID"), "0")
                            Dim MaterialID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Unload_Tentative.GetDataRow(iCount).Item("MaterialID"), "0")
                            Dim Quantity As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(GridView_Unload_Tentative.GetDataRow(iCount).Item("Amount"), 0)
                            Dim GroupStorageID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Unload_Tentative.GetDataRow(iCount).Item("DestinationGroupID"), "999999")

                            DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(GroupStorageID), idOfField)

                            If (TicketID = Rows_Value_ID(0).ToString) Then
                                foundAllTicket = DT_Check_Mat_Bale.Select("StorageID = '" & StorageID & "' and MaterialID='" & MaterialID & "' and Quantity >=" & Quantity)
                                If (foundAllTicket.Count <= 0) Then
                                    foundQTY = DT_Check_Mat_Bale.Select("StorageID = '" & StorageID & "' and MaterialID='" & MaterialID & "' and Quantity < " & Quantity)
                                    Dim gv As GridView = CType(GridView_Unload_Tentative, GridView)
                                    Dim ColDesArea As GridColumn = gv.Columns("DestinationStorageID")
                                    Dim QTY As GridColumn = gv.Columns("Amount")
                                    Dim colTicket As GridColumn = gv.Columns("Ticket")
                                    Dim CurrArea As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(ColDesArea), "-")
                                    Dim CurrQTY As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(QTY), "0.0")
                                    Dim DesName As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetRowCellDisplayText(iCount, "DestinationStorageID"), "-1")
                                    Dim DesRealAmount As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetRowCellDisplayText(iCount, "Amount"), "0.0")
                                    Dim DesQTY As String = "0.0"
                                    If (foundQTY.Count > 0) Then DesQTY = DataHelper.DBNullOrNothingTo(Of String)(foundQTY(0).ItemArray(6), "0.0")

                                    Dim gridFormatRule As New GridFormatRule()
                                    Dim formatConditionRuleValue As New FormatConditionRuleValue()
                                    gridFormatRule.Column = colTicket
                                    gridFormatRule.ApplyToRow = True
                                    formatConditionRuleValue.PredefinedName = "Red Fill, Red Text"
                                    formatConditionRuleValue.Value1 = Rows_Value_ID(0).ToString
                                    formatConditionRuleValue.Condition = FormatCondition.Equal
                                    gridFormatRule.Rule = formatConditionRuleValue
                                    GridView_Unload_Tentative.FormatRules.Add(gridFormatRule)

                                    Dim isCheck As Boolean = CType(GridView_Unload_Tentative.GetRowCellValue(iCount, "DX$CheckboxSelectorColumn"), Boolean)
                                    GridView_Unload_Tentative.UnselectRow(getFocusRow)
                                    Dim tmpMessage As String = "ตั๋วชั่ง [" & Rows_Value_ID(0).ToString & "] วัตถุดิบในพื้นที่ [" & DesName & "] := " & DesQTY & "(" & DesRealAmount & ") ไม่เพียงพอในการ Rollback"
                                    WinUtil.ShowWarningBox(tmpMessage, "พื้นที่ [" & CurrArea & "] จำนวนวัตถุดิบ [" & CurrQTY & "]")
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridView_Unload_Tentative_SelectionChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
                ModMainApp.Log.Log4N("GridView_Unload_Tentative_SelectionChanged [Catch]").DebugFormat("Err := {0} ", ex.Message)
            End Try
        End Sub

        Private Sub GridView_Movement_Tentative_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GridView_Movement_Tentative.SelectionChanged
            Try
                Dim Rows_Value As New ArrayList()
                Dim Parent_ID As New ArrayList()
                Dim Rows_Value_Desc_GroupID As New ArrayList()
                Dim Dest_StorageID As New ArrayList()
                Dim MatID As New ArrayList()
                Dim Bale_Rollback As New ArrayList()
                Dim foundRow As DataRow() = Nothing
                Dim foundQTY As DataRow() = Nothing
                Dim DT_Check_Mat_Bale As New DataTable

                If (GridView_Movement_Tentative.IsRowSelected(e.ControllerRow)) Then
                    Dim gv As GridView = CType(GridView_Movement_Tentative, GridView)
                    Dim colID As GridColumn = gv.Columns("ID")

                    Dim getFocusRow As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(GridView_Movement_Tentative.GetRowHandle(GridView_Movement_Tentative.GetFocusedDataSourceRowIndex), -1)
                    Rows_Value.Add(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("ID").ToString)
                    Dim tmpParent_ID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("ParentStorageID"), "-1")
                    'NULL = 999999 Other
                    Dim tmpDes_Group_ID As String = DataHelper.DBNullOrNothingTo(Of String)(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("DestinationGroupID"), "999999")
                    Rows_Value_Desc_GroupID.Add(tmpDes_Group_ID)
                    Dest_StorageID.Add(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("DestinationStorageID").ToString)
                    MatID.Add(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("MaterialID").ToString)
                    Bale_Rollback.Add(GridView_Movement_Tentative.GetDataRow(getFocusRow).Item("Amount").ToString)

                    If (tmpDes_Group_ID = "999999") Then
                        DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(999999, idOfField)
                    Else
                        DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(tmpDes_Group_ID), idOfField)
                    End If
                    foundRow = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(0).ToString & "' and MaterialID='" & MatID(0).ToString & "' and Quantity >=" & CDec(Bale_Rollback(0)))

                    If (foundRow.Count <= 0) Then
                        foundQTY = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(0).ToString & "' and MaterialID='" & MatID(0).ToString & "' and Quantity < " & CDec(Bale_Rollback(0)))
                        Dim DesQTY As String = "0.0"
                        If (foundQTY.Count > 0) Then DesQTY = DataHelper.DBNullOrNothingTo(Of String)(foundQTY(0).ItemArray(6), "0.0")
                        Dim ColDesArea As GridColumn = gv.Columns("DestinationStorageID")
                        Dim QTY As GridColumn = gv.Columns("Amount")
                        Dim CurrArea As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(ColDesArea), "-")
                        Dim CurrQTY As String = DataHelper.DBNullOrNothingTo(Of String)(gv.GetFocusedRowCellDisplayText(QTY), "0.0")
                        Dim DestAreaName As String = Dest_StorageID(0).ToString

                        Dim gridFormatRule As New GridFormatRule()
                        Dim formatConditionRuleValue As New FormatConditionRuleValue()
                        gridFormatRule.Column = colID
                        gridFormatRule.ApplyToRow = True
                        formatConditionRuleValue.PredefinedName = "Red Fill, Red Text"
                        formatConditionRuleValue.Value1 = Rows_Value(0).ToString
                        formatConditionRuleValue.Condition = FormatCondition.Equal
                        gridFormatRule.Rule = formatConditionRuleValue
                        GridView_Movement_Tentative.FormatRules.Add(gridFormatRule)

                        GridView_Movement_Tentative.UnselectRow(getFocusRow)

                        WinUtil.ShowWarningBox("พื้นที่  [" & CurrArea & "] มีวัตถุดิบในพื้นที่ := " & DesQTY & "(" & CurrQTY & ") ไม่เพียงพอในการ Rollback", "จำนวนวัตถุดิบในพื้นที่ ไม่เพียงพอ")

                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridView_Movement_Tentative_SelectionChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
                ModMainApp.Log.Log4N("GridView_Movement_Tentative_SelectionChanged [Catch]").DebugFormat("Err := {0} ", ex.Message)
            End Try
        End Sub

    End Class
End Namespace
