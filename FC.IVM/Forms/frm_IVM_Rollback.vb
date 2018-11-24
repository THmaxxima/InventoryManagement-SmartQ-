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
        Dim tmpTapAction As Integer = 2

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

                If (_fieldID <> "") Then

                    idOfField = CInt(_fieldID)

                    btnClose.Visible = True

                    Select Case CInt(_fieldID)
                        Case 1
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('2','3')") 'only site 1
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('2','3')") 'only site 1
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    row.Delete()
                                Next
                            End If
                            GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                            initLookupDestinationArea()
                        Case 2
                            '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','3')") 'only site 2
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','3')") 'only site 2
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    row.Delete()
                                Next
                            End If
                            GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                            initLookupDestinationArea()
                        Case 3
                            '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                            GetTentativeTransacData()
                            Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                            Dim Dr As DataRow = Nothing
                            foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','2')") 'only site 3
                            If foundRowUnload.Count > 0 Then
                                For Each row As DataRow In foundRowUnload
                                    row.Delete()
                                Next
                            End If
                            GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac
                            '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                            Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                            Dim Dr_Move As DataRow = Nothing
                            foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','2')") 'only site 3
                            If foundRowMovement.Count > 0 Then
                                For Each row As DataRow In foundRowMovement
                                    row.Delete()
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
        Private Sub cboSite_EditValueChanged(sender As Object, e As EventArgs) Handles cboSite.EditValueChanged
            Try

                Select Case CInt(cboSite.EditValue)
                    Case 1
                        GetTentativeTransacData()
                        Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                        Dim Dr As DataRow = Nothing
                        foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('2','3')") 'only site 1
                        If foundRowUnload.Count > 0 Then
                            For Each row As DataRow In foundRowUnload
                                row.Delete()
                            Next
                        End If
                        GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                        '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                        Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                        Dim Dr_Move As DataRow = Nothing
                        foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('2','3')") 'only site 1
                        If foundRowMovement.Count > 0 Then
                            For Each row As DataRow In foundRowMovement
                                row.Delete()
                            Next
                        End If
                        GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                        initLookupDestinationArea()
                    Case 2
                        '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                        GetTentativeTransacData()
                        Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                        Dim Dr As DataRow = Nothing
                        foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','3')") 'only site 2
                        If foundRowUnload.Count > 0 Then
                            For Each row As DataRow In foundRowUnload
                                row.Delete()
                            Next
                        End If
                        GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac

                        '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                        Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                        Dim Dr_Move As DataRow = Nothing
                        foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','3')") 'only site 2
                        If foundRowMovement.Count > 0 Then
                            For Each row As DataRow In foundRowMovement
                                row.Delete()
                            Next
                        End If
                        GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data

                        initLookupDestinationArea()
                    Case 3
                        '+++++++++++++++++++++++++++++++ Unload Tentative +++++++++++++++++++++++++++++++
                        GetTentativeTransacData()
                        Dt_tmpUnloadTentativeTransac = DT_Tentative_Unload_Data
                        Dim Dr As DataRow = Nothing
                        foundRowUnload = Dt_tmpUnloadTentativeTransac.Select("ParentStorageID in ('1','2')") 'only site 3
                        If foundRowUnload.Count > 0 Then
                            For Each row As DataRow In foundRowUnload
                                row.Delete()
                            Next
                        End If
                        GridControl_Unload_Tentative.DataSource = Dt_tmpUnloadTentativeTransac
                        '+++++++++++++++++++++++++++++++ Movement Tentative +++++++++++++++++++++++++++++++
                        Dt_tmpMovementTentativeTransac = DT_Tentative_Movement_Data
                        Dim Dr_Move As DataRow = Nothing
                        foundRowMovement = Dt_tmpMovementTentativeTransac.Select("ParentStorageID in ('1','2')") 'only site 3
                        If foundRowMovement.Count > 0 Then
                            For Each row As DataRow In foundRowMovement
                                row.Delete()
                            Next
                        End If
                        GridControl_Movement_Tentative.DataSource = DT_Tentative_Movement_Data
                        initLookupDestinationArea()
                End Select
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
                If (tmpTapAction = 1) Then
                    Dim selectedRowHandles As Int32() = GridView_Unload_Tentative.GetSelectedRows()
                    Dim I As Integer
                    For I = 0 To selectedRowHandles.Length - 1
                        Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        If (selectedRowHandle >= 0) Then
                            Rows_Value.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("ID").ToString)
                            Rows_Value_ID.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("Ticket").ToString)

                            Rows_Value_Desc_GroupID.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("DestinationGroupID").ToString)
                            Dest_StorageID.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("DestinationStorageID").ToString)
                            MatID.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("MaterialID").ToString)
                            Bale_Rollback.Add(GridView_Unload_Tentative.GetDataRow(selectedRowHandle).Item("Amount").ToString)
                        End If
                    Next

                    If (WinUtil.ShowQuestionBox("ต้องการลบข้อมูลที่เลือก ใช่หรือไม่?", "ลบข้อมูล") = DialogResult.Yes) Then
                        For icount = 0 To Rows_Value.Count - 1
                            '+++++++++++++++ Check Bale amount before rollback +++++++++++++++++++++
                            DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(Rows_Value_Desc_GroupID(icount)), idOfField)
                            foundRow = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(icount).ToString & "' and MaterialID='" & MatID(icount).ToString & "' and Quantity >='" & Bale_Rollback(icount).ToString & "'")

                            'Source_Rollback_Data_ID = CType(Rows_Value_Desc_GroupID(icount), Integer)
                            'DT_Source_Rollback_Data_Name = func_IVM_Get_Area_Data_By_GroupID(Source_Rollback_Data_ID)
                            'Storage_Name = DT_Source_Rollback_Data_Name.Select("ID = '" & Dest_StorageID(icount).ToString & "'")
                            'SN = Storage_Name(0).Item("Name").ToString

                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            If foundRow.Count > 0 Then
                                porc_Res = func_IVM_Rollback_Tentative_Data(CInt(Rows_Value(icount)), tmpTapAction, UserId)

                                GridView_Unload_Tentative.BeginSort()
                                Try
                                    GridView_Unload_Tentative.DeleteSelectedRows()
                                Finally
                                    GridView_Unload_Tentative.EndSort()
                                End Try

                                GridView_Unload_Tentative.ClearSelection()
                                GridView_Unload_Tentative.RefreshData()
                                '+++++++++++++++ Log ++++++++++++++++
                                ModMainApp.Log.Log4N("Rollback tentative data").DebugFormat("User ID := {0} ReferenceID := {1} RollBackTypeID := {2} Area := {3} Date := {4}", UserId.ToString, Rows_Value(icount).ToString & Storage_Name(0).Item("Name").ToString & "[" & Rows_Value_ID(icount).ToString & "]", "Unload", idOfField, DateTime.Now)
                                '++++++++++++++++++++++++++++++++++++
                            Else
                                WinUtil.ShowWarningBox("ไม่สามารถ Rollback data ได้ เนื่องจาก ไม่มีวัตถุดิบในพื้นที่ หรือมีไม่เพียงพอ", "จำนวนวัตถุดิบในพื้นที่ ไม่เพียงพอ")
                            End If
                        Next
                    End If
                ElseIf (tmpTapAction = 2) Then
                    Dim selectedRowHandles As Int32() = GridView_Movement_Tentative.GetSelectedRows()
                    Dim I As Integer
                    For I = 0 To selectedRowHandles.Length - 1
                        Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        If (selectedRowHandle >= 0) Then
                            Rows_Value.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("ID").ToString)
                            Rows_Value_ID.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("SourceStorageID").ToString)

                            '999999 W.P.
                            'Rows_Value_Desc_GroupID.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("ParentStorageID").ToString)
                            Rows_Value_Desc_GroupID.Add(DataHelper.DBNullOrNothingTo(Of String)(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("DestinationGroupID"), "999999").ToString)

                            Dest_StorageID.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("DestinationStorageID").ToString)
                            MatID.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("MaterialID").ToString)
                            Bale_Rollback.Add(GridView_Movement_Tentative.GetDataRow(selectedRowHandle).Item("Amount").ToString)
                        End If
                    Next

                    If (WinUtil.ShowQuestionBox("ต้องการลบข้อมูลที่เลือก ใช่หรือไม่?", "ลบข้อมูล") = DialogResult.Yes) Then
                        For icount = 0 To Rows_Value.Count - 1
                            '+++++++++++++++ Check Bale amount before rollback +++++++++++++++++++++
                            DT_Check_Mat_Bale = func_IVM_Get_ChildStorageData(CInt(Rows_Value_Desc_GroupID(icount)), idOfField)
                            foundRow = DT_Check_Mat_Bale.Select("StorageID = '" & Dest_StorageID(icount).ToString & "' and MaterialID='" & MatID(icount).ToString & "' and Quantity >='" & Bale_Rollback(icount).ToString & "'")

                            'Source_Rollback_Data_ID = CType(Rows_Value_Desc_GroupID(icount), Integer)
                            'DT_Source_Rollback_Data_Name = func_IVM_Get_Area_Data_By_GroupID(Source_Rollback_Data_ID)
                            'Storage_Name = DT_Source_Rollback_Data_Name.Select("ID = '" & Dest_StorageID(icount).ToString & "'")
                            'SN = Storage_Name(0).Item("Name").ToString

                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            If foundRow.Count > 0 Then
                                porc_Res = func_IVM_Rollback_Tentative_Data(CInt(Rows_Value(icount)), tmpTapAction, UserId)
                                GridView_Movement_Tentative.BeginSort()
                                Try
                                    GridView_Movement_Tentative.DeleteSelectedRows()
                                Finally
                                    GridView_Movement_Tentative.EndSort()
                                End Try
                                GridView_Movement_Tentative.ClearSelection()
                                GridView_Movement_Tentative.RefreshData()
                                '+++++++++++++++ Log ++++++++++++++++
                                ModMainApp.Log.Log4N("Rollback tentative data").DebugFormat("User ID := {0} ReferenceID := {1} RollBackTypeID := {2} Area := {3} Date := {4}", UserId.ToString, Rows_Value(icount).ToString & "[" & Rows_Value_ID(icount).ToString & "]", "Movement", idOfField, DateTime.Now)
                                '++++++++++++++++++++++++++++++++++++
                            Else
                                WinUtil.ShowWarningBox("ไม่สามารถ Rollback data ได้ เนื่องจาก ไม่มีวัตถุดิบในพื้นที่ หรือมีไม่เพียงพอ", "จำนวนวัตถุดิบในพื้นที่ ไม่เพียงพอ")
                            End If
                        Next
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
        Private Sub LayoutControlGroupUnload_Shown(sender As Object, e As EventArgs) 
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


    End Class
End Namespace
