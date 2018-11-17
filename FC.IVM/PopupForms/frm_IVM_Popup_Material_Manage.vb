Imports FC.IVM.Bus.Modules
Imports FC.MainApp.Modules
Imports FC.MainWinApp
Imports FC.M.BLL_Util
Imports DevExpress.XtraMap
Imports DevExpress.XtraBars
Imports System.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Layout.Events
Imports System.ComponentModel
Imports FC.M.PSL_Win.Classes_Helper
Imports DevExpress.XtraGrid.Controls
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid
Imports DevExpress.XtraLayout


Namespace PopupForms
    ''' <summary>
    '''   <para>หน้าจอ สำหรับการจัดการข้อมูลวัตถุดิบของแต่ละลาน</para>
    '''   <para>-Unload</para>
    '''   <para>-ย้าย</para>
    '''   <para>-ตัดจ่าย</para>
    ''' </summary>
    Public Class frm_IVM_Popup_Material_Manage
        Implements IChildOfMainForm
        ''' <exclude />
        Public Event OnPassData As IChildOfMainForm.OnPassDataEventHandler Implements IChildOfMainForm.OnPassData
        ''' <exclude />
        Public Sub OnBeforeFormLoad(param As Object) Implements IChildOfMainForm.OnBeforeFormLoad

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
        End Sub

        Public _ToDo As String = ""
        Public _PUserID As String = ""
        Public _fieldID As String = ""
        Public _siteName As String = ""
        Public _materialTypeId As Integer
        Public _MaterialSourceID As Integer
        Public _GroupID As Integer

        Dim ds As New DataSet()
        Dim DT As New DataTable()
        Dim DTWP As New DataTable()
        Dim DS_Area_Data As New DataSet
        Dim DT_Area_Data As DataTable
        Dim list As New List(Of IVMClass.Item)
        Dim sum As Double = 0
        Dim _isEdit As Boolean = False
        Public dt_Destination As New DataTable
        Private weight As Double = 0
        Private WeightPUnit As Double = 0

        Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
        Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
        Private osk As String = "C:\Windows\System32\osk.exe"

        ''' <exclude />
        Public Sub New()
            InitializeComponent()
            SetGridFont(GridControlSouceArea, New Font("Tahoma", 16, FontStyle.Bold))
            SetGridFont(gridControlDestGroupArea, New Font("Tahoma", 16, FontStyle.Bold))
            SetGridFont(GridControlDestSubArea, New Font("Tahoma", 16, FontStyle.Bold))
            SetGridFont(GridControlWP, New Font("Tahoma", 16, FontStyle.Bold))
            CheckShowAll.Visible = False
            lblStroageID.Text = String.Empty
            lblStroageName.Text = String.Empty
            lblMatID.Text = String.Empty
            lblMatName.Text = String.Empty
            lblWeight.Text = String.Empty
            txtquantity.EditValue = 0
            lblNetAmount.Text = "0"
        End Sub
        ''' <exclude />
        Sub SetGridFont(view As GridControl, font As Font)
            Dim ap As AppearanceObject
            For Each ap In LayoutViewSouceArea.Appearance
                ap.Font = font
            Next
        End Sub
        ''' <exclude />
        Private Sub Prepare_Grid_Movement_Souce()
            Try
                dt_Destination.Columns.Clear()
                dt_Destination.Columns.Add("GroupID", GetType(Integer))
                dt_Destination.Columns.Add("GroupName", GetType(String))
                dt_Destination.Columns.Add("MaterialID", GetType(Integer))
                dt_Destination.Columns.Add("MaterialName", GetType(String))
                dt_Destination.Columns.Add("Quantity", GetType(Decimal))
                dt_Destination.Columns.Add("Action", GetType(String))
                dt_Destination.Columns.Add("PackageSize", GetType(String))
                GridControlMoveMentDetail.DataSource = dt_Destination
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [Prepare_Grid_Movement_Souce]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_ShowingPopupEditForm(ByVal sender As Object, ByVal e As ShowingPopupEditFormEventArgs)
            AddHandler e.EditForm.FormClosing, AddressOf EditForm_FormClosing
        End Sub
        ''' <exclude />
        Private Sub EditForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
            Try
                Dim Row_Index As Integer = GridViewMoveMentDetail.GetFocusedDataSourceRowIndex
                '++++++++++++++++++ If new row set value to last new row +++++++++++++++
                If (Row_Index < 0) Then Row_Index = GridViewMoveMentDetail.RowCount - 1
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim PackageCell As GridColumn = GridViewMoveMentDetail.Columns(7)
                GridViewMoveMentDetail.SetRowCellValue(Row_Index, PackageCell, tmpUnloadPackSize)
                '+++++++++++++++++++++++++++ Set variable for use on custom edit form +++++++++++++++++
                isEdit = False
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [EditForm_FormClosing]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Sub InitData()
            Try
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                GridControlWP.DataSource = GetWPInfo(CInt(_fieldID))
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                LayoutViewSouceArea.BeginUpdate()

                If (_siteName = "UnloadImport" Or _siteName = "Unload") Then

                    GridControlWeightTicket.DataSource = GetSubAreaINVDataSet()
                    GridControlWeightTicket.Visible = True
                    GridControlSouceArea.Visible = False

                    LayoutViewWeightTicket.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    LayoutViewWeightTicket.Columns(7).DisplayFormat.FormatString = "{0:n1}"
                    LayoutViewWeightTicket.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    LayoutViewWeightTicket.Columns(8).DisplayFormat.FormatString = "{0:n3}"

                    '++++++++++ กรณี Unload จะไม่สามารถเลือก W.P. ได้ ++++++++++
                    txtquantity.ReadOnly = False
                    txtquantity.Enabled = True
                Else
                    GridControlSouceArea.DataSource = GetSubAreaINVDataSet()

                    GridControlWeightTicket.Visible = False
                    GridControlSouceArea.Visible = True

                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    Me.CheckShowAll.Visible = False
                    Me.btnProperties.Enabled = False
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    LayoutViewSouceArea.BeginUpdate()
                    Me.LayoutViewSouceArea.CardHorzInterval = 2 'ระยะห่างระหว่าง การ์ด แนวนอน
                    Me.LayoutViewSouceArea.CardMinSize = New System.Drawing.Size(134, 106)
                    Me.LayoutViewSouceArea.CardVertInterval = 1 'ระยะห่างระหว่าง การ์ด แนวตั้ง
                    LayoutViewSouceArea.EndUpdate()

                    layoutViewDestGroupArea.BeginUpdate()
                    Me.layoutViewDestGroupArea.CardHorzInterval = 2
                    Me.layoutViewDestGroupArea.CardVertInterval = 1
                    layoutViewDestGroupArea.EndUpdate()

                    LayoutViewDestSubArea.BeginUpdate()
                    Me.LayoutViewDestSubArea.CardHorzInterval = 2
                    Me.LayoutViewDestSubArea.CardVertInterval = 1
                    LayoutViewDestSubArea.EndUpdate()

                    lblWeightTicket.Visible = False
                    txtquantity.ReadOnly = True
                    txtquantity.Enabled = False

                End If

                LayoutViewSouceArea.EndUpdate()
                LayoutViewSouceArea.RefreshData()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [InitData]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub LayoutViewSouceArea_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewSouceArea.CardClick
            Dim rowind As String
            Dim quantity As Decimal
            Try
                rowind = CType(LayoutViewSouceArea.FocusedRowHandle, String)

                Dim areaid As String
                    Dim areaname As String
                    Dim matid As String
                    Dim matname As String
                    Dim weight As Double
                    rowind = CType(LayoutViewSouceArea.FocusedRowHandle, String)
                    areaid = CType(LayoutViewSouceArea.GetFocusedRowCellValue("StorageID"), String)
                    areaname = CType(LayoutViewSouceArea.GetFocusedRowCellValue("StorageName"), String)
                    matid = CType(LayoutViewSouceArea.GetFocusedRowCellValue("MaterialID"), String)
                    matname = CType(LayoutViewSouceArea.GetFocusedRowCellValue("MaterialName"), String)
                    quantity = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewSouceArea.GetFocusedRowCellValue("Quantity"), 0)
                    weight = DataHelper.DBNullOrNothingTo(Of Double)(LayoutViewSouceArea.GetFocusedRowCellValue("WeightADT"), 0)
                    'DataHelper.DBNullOrNothingTo(Of Double)(LayoutViewSouceArea.GetFocusedRowCellValue("MillWeight"), 0)
                    '+++++ If not unload must to clear value ++++++
                    lblTicketID.Text = String.Empty

                    '++++++++++++++++++++++++++++++++++++++++++++++
                    lblStroageID.Text = areaid
                    lblStroageName.Text = areaname
                    lblWeight.Text = weight.ToString("0.000")
                    txtquantity.EditValue = quantity.ToString("0.0")
                    lblNetAmount.Text = quantity.ToString
                    '+++++++ Cal weight per unit +++++
                    WeightPUnit = weight / quantity
                    '+++++++++++++++++++ Binding Search lookupedit Mat +++++++++++++++++++++++++++
                    BindingSearchMaterial()
                    '+++++++++++++ Lock object ++++++++++++++++++++
                    LockObject()

                '++++++++++++++++++++ Clear ข้อมูล grid ก่อนการเลือกพื้นที่ใหม่ ++++++++++++++++++
                If (GridViewMoveMentDetail.RowCount > 0) Then
                    Dim iCountRows As Integer
                    For iCountRows = 0 To GridViewMoveMentDetail.RowCount - 1
                        GridViewMoveMentDetail.DeleteSelectedRows()
                    Next iCountRows
                End If
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Me.TabPane1.SelectNextPage()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [LayoutViewSouceArea_CardClick]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub BindingSearchMaterial()
            Try
                Dim searchlookupmat As New SearchLookUpEdit
                searchlookupmat = SearchMaterial
                Dim matid As String
                Dim matname As String
                Dim LayoutViewRename As New LayoutView

                '+++++++++++++++++++ Binding Search lookupedit Mat +++++++++++++++++++++++++++
                If (_siteName = "UnloadImport" Or _siteName = "Unload") Then
                    LayoutViewRename = LayoutViewWeightTicket
                Else
                    LayoutViewRename = LayoutViewSouceArea
                End If
                searchlookupmat.EditValue = LayoutViewRename.GetFocusedRowCellValue("MaterialID")
                searchlookupmat.SelectedText = LayoutViewRename.GetFocusedRowCellValue("MaterialName").ToString
                matid = CType(searchlookupmat.EditValue, String)
                matname = searchlookupmat.SelectedText
                lblMatID.Text = matid
                lblMatName.Text = matname
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [BindingSearchMaterial]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
                ModMainApp.Log.Log4N("BindingSearchMaterial [Catch]").DebugFormat("Err := {0} ", ex.Message)
            End Try
        End Sub
        Function GetGroupAreaInfo(ByVal FieldID As Integer) As DataTable
            Try
                'SetWaitDialogCaption(My.Resources.LoadingTables)
                ModMainApp.Log.Log4N("GetGroupAreaInfo [Before]").DebugFormat("Call := proc_IVM_PrimaryStorage_1664 {0}",
                  String.Format("Proc param := {0}", DataHelper.ToSqlValue(FieldID)))

                '******************************************************
                DT = func_IVM_Get_Area_Info(FieldID)
                '******************************************************

                ModMainApp.Log.Log4N("GetGroupAreaInfo [Return]").DebugFormat("1664 Return data table rows count := {0} ",
                  DT.Rows.Count.ToString)
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetGroupAreaInfo]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)

                ModMainApp.Log.Log4N("GetGroupAreaInfo [Catch]").DebugFormat("Err := {0} ", ex.Message)
            End Try
            Return DT
        End Function

        Function GetWPInfo(ByVal FieldID As Integer) As DataTable
            Try
                'SetWaitDialogCaption(My.Resources.LoadingTables)
                ModMainApp.Log.Log4N("GetGroupAreaInfo [Before]").DebugFormat("Call := proc_IVM_PrimaryStorage_1664 {0}",
                  String.Format("Proc param := {0}", DataHelper.ToSqlValue(FieldID)))

                '******************************************************
                DTWP = func_IVM_Get_WP_Info(FieldID)
                '******************************************************

                ModMainApp.Log.Log4N("GetWPInfo [Return]").DebugFormat("1664 Return data table rows count := {0} ",
                  DTWP.Rows.Count.ToString)
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetWPInfo]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)

                'ModMainApp.Log.Log4N("GetWPInfo [Catch]").DebugFormat("Err := {0} ", ex.MGetTarketSubAreaDataSetessage)
            End Try
            Return DTWP
        End Function
        Function GetSubAreaINVDataSet() As DataTable
            'Dim tmpDT_Area_Data As DataTable
            Dim foundRow As DataRow()

            Try
                If (_GroupID > 0 And (_siteName = "Unload")) Then
                    '   ModMainApp.Log.Log4N("GetSubAreaINVDataSet [Before]").DebugFormat("Call := proc_IVM_GetUnloadTruck_1862 {0}",
                    'String.Format("Proc param := {0},{1}", DataHelper.ToSqlValue(_MaterialSourceID), DataHelper.ToSqlValue(_PUserID)))
                    DT_Area_Data = New DataTable
                    DT_Area_Data = func_IVM_Get_WeightTicketData(_materialTypeId, _PUserID)

                    If (CheckShowAll.Checked = True) Then
                        'Dim tmpConditionString As String = "MaterialSource=\"Import\""
                        foundRow = DT_Area_Data.Select("MaterialSource='Import'")
                    Else
                        'Dim tmpConditionString As String = "MaterialSource = """Import""" And UnloadStation Not Like """%ลาน1%"
                        'foundRow = DT_Area_Data.Select("MaterialSource='Import' AND UnloadStation Not Like '%ลาน1%'")
                        foundRow = DT_Area_Data.Select("MaterialSource = 'Import'")
                    End If

                    If foundRow.Count > 0 Then
                        'For Each row As DataRow In foundRow
                        '    row.Delete()
                        'Next
                        For iRow As Integer = 0 To foundRow.Count - 1
                            DT_Area_Data.Rows.Remove(foundRow(iRow))
                            DT_Area_Data.AcceptChanges()
                        Next

                    End If
                    '   ModMainApp.Log.Log4N("GetSubAreaINVDataSet [Return]").DebugFormat("1862 Return data table rows count := {0} ",
                    'DT_Area_Data.Rows.Count.ToString)
                ElseIf (_GroupID > 0 And (_siteName = "UnloadImport")) Then
                    DT_Area_Data = New DataTable
                    DT_Area_Data = func_IVM_Get_WeightTicketData(_materialTypeId, _PUserID)

                    If (CheckShowAll.Checked = True) Then
                        foundRow = DT_Area_Data.Select("MaterialSource = 'Local'")
                    Else
                        'foundRow = DT_Area_Data.Select("MaterialSource = 'Local' And UnloadStation NOT LIKE '%ลาน1%'")
                        foundRow = DT_Area_Data.Select("MaterialSource = 'Local'")
                    End If

                    If foundRow.Count > 0 Then
                        'For Each row As DataRow In foundRow
                        '    row.Delete()
                        'Next
                        For iRow As Integer = 0 To foundRow.Count - 1
                            DT_Area_Data.Rows.Remove(foundRow(iRow))
                            DT_Area_Data.AcceptChanges()
                        Next

                    End If

                Else
                    '   ModMainApp.Log.Log4N("GetSubAreaINVDataSet [Before]").DebugFormat("Call := proc_IVM_ChildStorageData_1726 {0}",
                    'String.Format("Proc param := {0},{1}", DataHelper.ToSqlValue(_GroupID)), DataHelper.ToSqlValue(_fieldID))
                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    'Dim Amount As Double = DataHelper.DBNullOrNothingTo(Of Double)(ViewData.GetRowCellValue(icountGridRow, "Quantity"), 0)
                    DT_Area_Data = New DataTable
                    DT_Area_Data = func_IVM_Get_ChildStorageData(_GroupID, CInt(_fieldID))
                    foundRow = DT_Area_Data.Select("MaterialID IS NULL")
                    If foundRow.Count > 0 Then
                        'For Each row As DataRow In foundRow
                        '    row.Delete()
                        'Next
                        For iRow As Integer = 0 To foundRow.Count - 1
                            DT_Area_Data.Rows.Remove(foundRow(iRow))
                            DT_Area_Data.AcceptChanges()
                        Next

                    End If
                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    '   ModMainApp.Log.Log4N("GetSubAreaINVDataSet [Return]").DebugFormat("1726 Return data table rows count := {0} ",
                    'DT_Area_Data.Rows.Count.ToString)
                End If

                Return DT_Area_Data

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetSubAreaINVDataSet]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Function
        ''' <exclude />
        Private Sub frm_IVM_Popup_Material_Manage_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                Me.FormBorderStyle = FormBorderStyle.None
                WinDevHelper.ShowWaitForm(Me)
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Prepare_Grid_Movement_Souce()
                'GridControlSouceArea.DataSource = GetSubAreaINVDataSet()
                InitData()
                gridControlDestGroupArea.DataSource = GetGroupAreaInfo(CInt(_fieldID))
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                AddHandler GridViewMoveMentDetail.ShowingPopupEditForm, AddressOf GridViewMoveMentDetail_ShowingPopupEditForm
                GridViewMoveMentDetail.OptionsBehavior.EditingMode = GridEditingMode.EditForm
                initLookupMaterial()

                Dim find As FindControl = TryCast(LayoutViewSouceArea.GridControl.Controls.Find("FindControl", True)(0), FindControl)
                find.FindEdit.Focus()

                find.FocusFindEdit()

                If (_ToDo = "Unload" Or _ToDo = "UnloadImport") Then
                    UnLockObject()
                    TabNP_WP.PageVisible = False
                    lblWeightTicket.Visible = True
                Else
                    LockObject()
                    TabNP_WP.PageVisible = True
                    lblWeightTicket.Visible = False
                End If

                LayoutViewSouceArea.Focus()

                Me.Text = "Storage area information := " & _siteName & " [ " & _fieldID & " ] [ " & _ToDo & " ]"
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [frm_IVM_Popup_Material_Manage_Load]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            Finally
                WinDevHelper.CloseWaitForm()
            End Try
        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_ValidateRow(ByVal sender As Object,
                ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) _
                Handles GridViewMoveMentDetail.ValidateRow
            Try
                Dim View As GridView = CType(sender, GridView)
                Dim Quantity As GridColumn = View.Columns("Quantity")
                Dim matBalance As Decimal = CType(lblNetAmount.Text, Decimal)
                Dim moveQuantity As Decimal = CType(View.GetRowCellValue(e.RowHandle, Quantity), Decimal)
                Dim sum_QTY As Decimal = 0
                '++++++++++++++++++++++++++++++++++++++++++
                For i As Integer = 0 To GridViewMoveMentDetail.DataRowCount - 1
                    sum_QTY += CType(GridViewMoveMentDetail.GetRowCellValue(i, "Quantity"), Decimal)
                Next i
                If (GridViewMoveMentDetail.IsNewItemRow(GridViewMoveMentDetail.FocusedRowHandle) = True) Then
                    sum_QTY += moveQuantity
                End If
                '++++++++++++++++++++++++++++++++++++++++++
                'Validity criterion
                If (((sum_QTY) > matBalance) Or (moveQuantity < 0)) Then
                    e.Valid = False
                    'Set errors with specific descriptions for the columns
                    View.SetColumnError(Quantity, "จำนวนวัตถุดิบไม่เพียงพอ!")
                Else
                    isEdit = True
                    txtquantity.EditValue = (matBalance - sum_QTY).ToString("##0.0")
                    lblWeight.Text = (CType(txtquantity.EditValue, Decimal) * WeightPUnit).ToString("##0.000")
                End If
                '+++++++++++++++++++++++++++++++++++++++++++++++++++
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridViewMoveMentDetail_ValidateRow]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_InvalidRowException(ByVal sender As Object,
            ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) _
            Handles GridViewMoveMentDetail.InvalidRowException
            WinUtil.ShowWarningBox("จำนวนวัตถุดิบไม่เพียงพอ!", "กรุณาตรวจสอบจำนวนวัตถุดิบที่มี")
            e.ExceptionMode = ExceptionMode.NoAction
        End Sub
        Private Sub GetData()
            Try
                Dim iIndex As Integer = 0
                Dim iCountGridRow As Integer = 0
                iCountGridRow = GridViewMoveMentDetail.RowCount
                iIndex += 1
                '+++++++++++++++Prepare destination inventory grid+++++++++++++++++++++
                GridViewMoveMentDetail.AddNewRow()
                If (TabPane1.SelectedPageIndex = 3) Then
                    GridViewMoveMentDetail.SetFocusedRowCellValue("Quantity", CDec(txtquantity.EditValue))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupID", LayoutViewWP.GetFocusedRowCellValue("StorageID"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupName", LayoutViewWP.GetFocusedRowCellValue("StorageName"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialID", "0")
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialName", String.Empty)
                    GridViewMoveMentDetail.SetFocusedRowCellValue("PackageSize", "A")
                Else
                    GridViewMoveMentDetail.SetFocusedRowCellValue("Quantity", CDec(txtquantity.EditValue))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupID", LayoutViewDestSubArea.GetFocusedRowCellValue("StorageID"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupName", LayoutViewDestSubArea.GetFocusedRowCellValue("StorageName"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialID", LayoutViewDestSubArea.GetFocusedRowCellValue("MaterialID"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialName", LayoutViewDestSubArea.GetFocusedRowCellValue("MaterialName"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("PackageSize", "A")
                End If

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetData]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Function GetDataTable(Optional ByVal count As Integer = 2) As DataTable
            Dim dataTable = New DataTable()
            dataTable.Columns.Add("PackageName", GetType(String))
            dataTable.Columns.Add("PackageFullName", GetType(String))
            dataTable.Rows.Add("A", "Package Size A")
            dataTable.Rows.Add("B", "Package Size B")
            dataTable.Rows.Add("C", "Package Size C")
            dataTable.Rows.Add("D", "Package Size D")
            dataTable.Rows.Add("E", "Package Size E")
            dataTable.Rows.Add("F", "Package Size F")
            Return dataTable
        End Function
        Private Sub initLookupMaterial()
            AddHandler SearchMaterial.EditValueChanged, AddressOf SearchMaterial_EditValueChanged
            Dim DT_GetMaterial As DataTable = func_IVM_Getmaterial()
            SearchMaterial.Properties.NullText = "(เลือก ข้อมูล)"
            SearchMaterial.Properties.DataSource = DT_GetMaterial
            SearchMaterial.Properties.ValueMember = "ID"
            SearchMaterial.Properties.DisplayMember = "Name"
            SearchMaterial.Properties.DisplayMember = SearchMaterial.Properties.DisplayMember
            SearchMaterial.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            SearchMaterial.Properties.ShowClearButton = False

        End Sub

        ''' <exclude />
        Private Sub SearchMaterial_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            'Display lookup editor's current value.
            Dim lookupEditor As LookUpEditBase = TryCast(SearchMaterial, LookUpEditBase)
            If lookupEditor Is Nothing Then
                Return
            End If
            Dim label As LabelControl = labelDictionary(lookupEditor)

            If lookupEditor.EditValue Is Nothing Then
                label.Text = "แก้ไขวัตถุดิบ"
            Else
                label.Text = lookupEditor.EditValue.ToString()
                lblMatName.Text = lookupEditor.Text.ToString()
            End If
        End Sub
        Private labelDictionaryCore As Dictionary(Of LookUpEditBase, LabelControl)
        Private ReadOnly Property labelDictionary() As Dictionary(Of LookUpEditBase, LabelControl)
            Get
                If labelDictionaryCore Is Nothing Then
                    labelDictionaryCore = New Dictionary(Of LookUpEditBase, LabelControl)()
                    labelDictionaryCore.Add(SearchMaterial, lblMatID)
                End If
                Return labelDictionaryCore
            End Get
        End Property
        Private Sub layoutViewDestGroupArea_CardClick(sender As Object, e As CardClickEventArgs) Handles layoutViewDestGroupArea.CardClick
            Try
                Dim View As LayoutView = TryCast(sender, LayoutView)
                Dim isFull As String = CType(View.GetFocusedRowCellValue("IsFull"), String)
                If (CType(isFull, Boolean) = True) Then
                    WinUtil.ShowInfoBox("ไม่สามารถนำเข้าวัตถุดิบได้ เนื่องจากพื้นที่เต็ม! ", "พื้นที่เต็ม")
                    Return
                Else
                    Dim rowind As String
                    Dim areaid As String
                    Dim areaname As String
                    rowind = CType(layoutViewDestGroupArea.FocusedRowHandle, String)
                    areaid = CType(layoutViewDestGroupArea.GetFocusedRowCellValue("GroupID"), String)
                    areaname = CType(layoutViewDestGroupArea.GetFocusedRowCellValue("GroupName"), String)
                    GetTarketSubAreaDataSet(areaid, CInt(_fieldID))
                    Me.TabPane1.SelectNextPage()
                    Me.LayoutViewDestSubArea.Focus()
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [layoutViewDestGroupArea_CardClick]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Function GetTarketSubAreaDataSet(ByVal GroupID As String, ByVal FieldID As Integer) As DataSet
            Dim DS_Tarket_Area_Data As New DataSet
            Dim DT_Tarket_Area_Data As New DataTable
            Try
                ModMainApp.Log.Log4N("GetTarketSubAreaDataSet [Before]").DebugFormat("Call := proc_IVM_ChildStorageData_1726 {0}",
                 String.Format("Proc param := {0}", DataHelper.ToSqlValue(FieldID)))

                DT_Tarket_Area_Data = func_IVM_Get_ChildStorageData(CInt(GroupID), FieldID)
                DT_Tarket_Area_Data.TableName = "TarketSubAreaData"
                DS_Tarket_Area_Data.Tables.Add(DT_Tarket_Area_Data)
                DS_Tarket_Area_Data.DataSetName = "TarketSubAreaData"
                GridControlDestSubArea.DataSource = DS_Tarket_Area_Data.Tables("TarketSubAreaData")

                ModMainApp.Log.Log4N("GetTarketSubAreaDataSet [Return]").DebugFormat("1726 Return data table rows count := {0} ",
                                DT_Tarket_Area_Data.Rows.Count.ToString)
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetTarketSubAreaDataSet]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return DS_Tarket_Area_Data
        End Function
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_EditFormPrepared(sender As Object, e As EditFormPreparedEventArgs) Handles GridViewMoveMentDetail.EditFormPrepared
            Try
                TryCast(e.Panel.Parent, Form).StartPosition = FormStartPosition.CenterScreen

                isEdit = False
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridViewMoveMentDetail_EditFormPrepared]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <exclude />
        Private Sub RemoveRow(sender As Object, e As ButtonPressedEventArgs) Handles DeleteCustomRow.ButtonClick
            Dim View As GridView = TryCast(GridViewMoveMentDetail, GridView)
            Dim storageID As String = ""
            If CType(e.Button.Tag, String) = "Remove" Then
                View.DeleteSelectedRows()
            Else
                Return
            End If
        End Sub
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Dim ViewData As GridView = TryCast(GridViewMoveMentDetail, GridView)
            Dim res As String = ""
            Dim icountGridRow As Integer = 0
            Try
                If (ViewData.RowCount > 0) Then
                    Dim summaryValue As Decimal = CDec(GridViewMoveMentDetail.Columns(4).SummaryItem.SummaryValue)

                    If (((_ToDo = "Unload" Or _ToDo = "UnloadImport") And CDec(txtquantity.EditValue) = 0)) Then
                        If (lblTruckProperties.Text = "") Then
                            WinUtil.ShowWarningBox("กรุณาระบุ ตรวจสอบ Properties ในการตรวรับ ก่อนการบันทึกข้อมูล", "ผลการตรวจสอบ")
                        Else
                            If (tmpContractorProperties = "" Or tmpMatProperties = "" Or tmpBalingSealProperties = "" Or
                            tmpTransferPointProperties = "") Then
                                WinUtil.ShowWarningBox("กรุณาระบุ Properties ในการตรวรับ ให้ครบถ้วน ก่อนการบันทึกข้อมูล", "ผลการตรวจสอบ")
                            Else
                                '+++++++++++++++++++++++++Call proc to save data +++++++++++++++++++++++++
                                res = CType(func_IVM_UnloadTruckToTemp_1859(lblTicketID.Text,
                             summaryValue, CInt(lblMatID.Text), tmpContractorProperties, CInt(_PUserID), ViewData,
                             tmpMatProperties, tmpBalingSealProperties, tmpTransferPointProperties), String)
                                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                                If (res = "-1") Then
                                    '+++++++++++++++++++++++++++++++++
                                    btnSave.Enabled = False
                                    Me.TabPane1.Pages(1).Select()
                                    '+++++++++++++++++++++++++++++++++
                                    WinUtil.ShowInfoBox("บันทึกข้อมูล " & _ToDo &
                            " [" & lblTicketID.Text & " ] เรียบร้อย", "ผลการทำงาน" & _ToDo)

                                End If
                            End If
                        End If
                    ElseIf (_ToDo = "MoveInv") Then

                        For icountGridRow = 0 To ViewData.RowCount - 1
                            Dim SouceAreaID As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(ViewData.GetRowCellValue(icountGridRow, "GroupID"), 0)
                            Dim MatID As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(lblMatID.Text, 0) ' CType(lblMatID.Text, Integer)
                            Dim Amount As Double = DataHelper.DBNullOrNothingTo(Of Double)(ViewData.GetRowCellValue(icountGridRow, "Quantity"), 0) 'CType(ViewData.GetRowCellValue(icountGridRow, "Quantity"), Double)
                            res = func_IVM_MoveStockTemp(CInt(lblStroageID.Text), SouceAreaID, MatID, Amount,
                                                             CType(_PUserID, Integer)).ToString
                        Next

                        If (res = "-1") Then
                            '+++++++++++++++++++++++++++++++++
                            btnSave.Enabled = False
                            Me.TabPane1.Pages(1).Select()
                            '+++++++++++++++++++++++++++++++++

                            WinUtil.ShowInfoBox("บันทึกข้อมูลการย้ายวัตถุดิบ เรียบร้อย", "ผลการทำงาน" & _ToDo)
                        End If

                    Else
                        WinUtil.ShowInfoBox("จำนวนวัตถุดิบยังคงเหลืออยู่ หากต้องการบันทึกข้อมูล" & vbCrLf & " กรุณาแก้ไขจำนวนวัตถุดิบให้ถูกต้องก่อน ทำรายการ",
                                            "กรุณาตรวจสอบข้อมูล" & _ToDo)
                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [btnSave_Click]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_ShowingEditor(sender As Object, e As CancelEventArgs) Handles GridViewMoveMentDetail.ShowingEditor
            Dim gv As GridView = CType(sender, GridView)
            Dim Quantity As GridColumn = gv.Columns("Quantity")
            Dim FRow As Integer = gv.FocusedRowHandle
            Try
                If gv.FocusedColumn.FieldName = "colCustomAction" Then
                    e.Cancel = True
                    'Dim moveQuantity As Integer = CType(gv.GetRowCellValue(FRow, "Quantity"), Integer)
                    Dim moveQuantity As Decimal = CType(gv.GetRowCellValue(FRow, "Quantity"), Decimal)
                    gv.DeleteSelectedRows()
                    txtquantity.EditValue = (CDec(txtquantity.EditValue) + moveQuantity)
                    lblWeight.Text = (CType(txtquantity.EditValue, Decimal) * WeightPUnit).ToString("##0.000")
                    isEdit = False
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridViewMoveMentDetail_ShowingEditor]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub UnLockObject()
            Dim lblLocat As Point
            Dim SearchMaterialLocat As Point
            GridViewMoveMentDetail.Columns("PackageSize").Visible = True
            lblLocat = New Point(6, 63)
            SearchMaterialLocat = New Point(6, 63)
            '+++++++++++++ Lock object ++++++++++++++++++++
            SearchMaterial.ReadOnly = False
            txtquantity.ReadOnly = False
            lblMatNameInfo.Location = lblLocat
            SearchMaterial.Location = SearchMaterialLocat
            CheckShowAll.Visible = True
            btnProperties.Enabled = True
            lblTruckProperties.Visible = True
            lblWeightTicket.Visible = True
            '++++++++++++++++++++++++++++++++++++++++++++++
        End Sub
        Private Sub LockObject()
            Dim lblLocat As Point
            Dim SearchMaterialLocat As Point

            GridViewMoveMentDetail.Columns("PackageSize").Visible = False
            lblLocat = New Point(120, 64)
            SearchMaterialLocat = New Point(115, 63)
            '+++++++++++++ Lock object ++++++++++++++++++++
            SearchMaterial.ReadOnly = True
            lblContractorInfo.Visible = False
            lblMatNameInfo.Location = lblLocat
            txtquantity.ReadOnly = True
            SearchMaterial.Location = SearchMaterialLocat
            CheckShowAll.Visible = False
            btnProperties.Enabled = False
            lblTruckProperties.Visible = False
            lblWeightTicket.Visible = False
            '++++++++++++++++++++++++++++++++++++++++++++++
        End Sub
        Private Sub LayoutViewDestSubArea_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewDestSubArea.CardClick
            Try
                Dim View As LayoutView = TryCast(sender, LayoutView)
                '++++++++++++++++++ Prepare data for popup customform +++++++++++++++++
                list.Clear()
                Dim strings(0) As String
                strings(0) = "Inventory amount"
                Dim item As IVMClass.Item = New IVMClass.Item()
                item.AutoNumber = False
                item.ColumnName = "Quantity"
                item.ColumnName2 = CType(txtquantity.EditValue, Decimal).ToString("##0.0")
                item.PackageSize = "PackageSize"
                item.DataType = 1
                item.ID = 0
                item.Memo = _siteName
                item.isEdit = isEdit
                list.Add(item)
                '++++++++++++++++++ Register custom edit form +++++++++++++++++++++++++
                GridViewMoveMentDetail.OptionsEditForm.CustomEditFormLayout = New frm_IVM_Custom_Form_Num_Key(list, strings,
                                                    GridViewMoveMentDetail.Appearance.Row.Font, GetDataTable())
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim storageID As String = View.GetFocusedRowCellValue("StorageID").ToString
                If (storageID <> lblStroageID.Text) Then
                    GetData()
                    SearchMaterial.Focus()
                    GridViewMoveMentDetail.ShowEditForm()
                Else
                    WinUtil.ShowWarningBox("ไม่สามารถย้ายวัตถุดิบเข้าสู่พื้นที่เดียวกันได้", "กรุณาตรวจสอบข้อมูล พีื้นที่")
                    Return
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [LayoutViewDestSubArea_CardClick]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_RowCountChanged(sender As Object, e As EventArgs) Handles GridViewMoveMentDetail.RowCountChanged
            Try
                If (GridViewMoveMentDetail.RowCount > 0) Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridViewMoveMentDetail_RowCountChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub CheckShowAll_EditValueChanged(sender As Object, e As EventArgs) Handles CheckShowAll.EditValueChanged
            Try
                'InitData()
                'GridControlSouceArea.DataSource = GetSubAreaINVDataSet()
                'LayoutViewDestSubArea.RefreshData()
                GridControlWeightTicket.DataSource = GetSubAreaINVDataSet()
                LayoutViewWeightTicket.RefreshData()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [CheckShowAll_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub LayoutViewWP_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewWP.CardClick
            Try
                Dim View As LayoutView = TryCast(sender, LayoutView)
                '++++++++++++++++++ Prepare data for popup customform +++++++++++++++++
                list.Clear()
                Dim strings(0) As String
                strings(0) = "Inventory amount"
                Dim item As IVMClass.Item = New IVMClass.Item()
                item.AutoNumber = False
                item.ColumnName = "Quantity"
                item.ColumnName2 = CType(txtquantity.EditValue, Decimal).ToString()
                item.PackageSize = "PackageSize"
                item.DataType = 1
                item.ID = 0
                item.Memo = _siteName
                item.isEdit = isEdit
                list.Add(item)
                '++++++++++++++++++ Register custom edit form +++++++++++++++++++++++++
                GridViewMoveMentDetail.OptionsEditForm.CustomEditFormLayout = New frm_IVM_Custom_Form_Num_Key(list, strings,
                                                    GridViewMoveMentDetail.Appearance.Row.Font, GetDataTable())
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim storageID As String = View.GetFocusedRowCellValue("StorageID").ToString
                If (storageID <> lblStroageID.Text) Then
                    GetData()
                    SearchMaterial.Focus()
                    GridViewMoveMentDetail.ShowEditForm()
                Else
                    WinUtil.ShowWarningBox("ไม่สามารถย้ายวัตถุดิบเข้าสู่พื้นที่เดียวกันได้", "กรุณาตรวจสอบข้อมูล พีื้นที่")
                    Return
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [LayoutViewWP_CardClick]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub txtquantity_Click(sender As Object, e As EventArgs) Handles txtquantity.Click
            Try
                'LayoutControl1.ShowCustomizationForm()
                If (((_ToDo = "Unload" Or _ToDo = "UnloadImport") And CDec(txtquantity.EditValue) > 0)) Then
                    Dim frmPopupNumkey As New frm_IVM_Popup_Num_Key
                    frmPopupNumkey.MatQTY = CDec(txtquantity.EditValue)
                    frmPopupNumkey.Show()
                    AddHandler frmPopupNumkey.FormClosed, AddressOf frmPopupNumKey_FormClosed
                End If

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
            End Try
        End Sub
        ''' <exclude />
        Private Sub frmPopupNumKey_FormClosed(sender As Object, e As FormClosedEventArgs)
            Try
                txtquantity.EditValue = tmpUnloadQuantity.ToString("##0.0")
                lblNetAmount.Text = tmpUnloadQuantity.ToString("##0.0")
                layoutViewDestGroupArea.Focus()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
            End Try
        End Sub
        Private Sub txtquantity_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
            Try
                Dim QTY As Decimal = CDec(CType(sender, DevExpress.XtraEditors.TextEdit).EditValue)

                If (QTY < 0) Then
                    e.Cancel = True
                    isEdit = False
                End If
            Catch ex As Exception
                MsgBox("txtquantity_Validating : " & ex.Message)
            End Try
        End Sub
        Private Sub txtquantity_InvalidValue(sender As Object, e As InvalidValueExceptionEventArgs)
            Try
                WinUtil.ShowWarningBox("จำนวนวัตถุดิบที่แก้ไข ต้องมีจำนวน มากกว่า 0 !", "ผิดพลาด")
                e.ExceptionMode = ExceptionMode.NoAction
            Catch ex As Exception

            End Try
        End Sub

        ''' <exclude />
        Public Sub onScreenKeyboard()
            Dim old As Long
            If Environment.Is64BitOperatingSystem Then
                If Wow64DisableWow64FsRedirection(old) Then
                    Process.Start(osk)
                    Wow64EnableWow64FsRedirection(old)
                Else
                    Process.Start(osk)
                    Wow64EnableWow64FsRedirection(old)
                End If
            Else
                Process.Start(osk)
            End If
        End Sub
        ''' <exclude />
        Private Sub GridControlSouceArea_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles GridControlSouceArea.ProcessGridKey
            If e.KeyCode = Keys.K Then
                onScreenKeyboard()
            End If
        End Sub

        ''' <exclude />
        Private Sub GridControlWP_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles GridControlWP.ProcessGridKey
            If e.KeyCode = Keys.K Then
                onScreenKeyboard()
            End If
        End Sub

        ''' <exclude />
        Private Sub gridControlDestGroupArea_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles gridControlDestGroupArea.ProcessGridKey
            If e.KeyCode = Keys.K Then
                onScreenKeyboard()
            End If
        End Sub

        ''' <exclude />
        Private Sub GridControlDestSubArea_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles GridControlDestSubArea.ProcessGridKey
            If e.KeyCode = Keys.K Then
                onScreenKeyboard()
            End If
        End Sub
        ''' <exclude />
        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        ''' <exclude />
        Private Sub frm_IVM_Popup_Mat_Properties_FormClosed(sender As Object, e As FormClosedEventArgs)
            Try
                lblTruckProperties.Text = "" &
                    "ซีลโรงอัด : " & tmpBalingSealProperties & vbCrLf &
                    "จุดขนถ่าย : " & tmpTransferPointProperties & vbCrLf &
                    "ชนิดวัตถุดิบ : " & tmpMatProperties & vbCrLf &
                    "ผู้รับเหมา : " & tmpContractorProperties
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
            End Try
        End Sub

        Private Sub btnProperties_Click(sender As Object, e As EventArgs) Handles btnProperties.Click
            Try
                Dim frmMatProperties As New PopupForms.frm_IVM_Popup_Mat_Properties
                AddHandler frmMatProperties.FormClosed, AddressOf frm_IVM_Popup_Mat_Properties_FormClosed
                frmMatProperties.ShowDialog()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [btnProperties_Click]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub LayoutViewWeightTicket_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewWeightTicket.CardClick
            Dim rowind As String
            Dim quantity As Decimal
            Try
                rowind = CType(LayoutViewWeightTicket.FocusedRowHandle, String)

                Dim Ticket As String
                Dim ReceiveDate As DateTime
                Dim PlateLicense As String
                Dim SupplierName As String

                Ticket = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("Ticket"), String)
                PlateLicense = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("PlateLicense"), String)
                ReceiveDate = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("ReceiveDate"), DateTime)
                SupplierName = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("SupplierName"), String)
                '+++++++++++++++++++ Binding Search lookupedit Mat +++++++++++++++++++++++++++
                BindingSearchMaterial()
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                quantity = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("Quantity"), Decimal)
                'weight = CType(LayoutViewWeightTicket.GetFocusedRowCellValue("MillWeight"), Double)
                weight = DataHelper.DBNullOrNothingTo(Of Double)(LayoutViewWeightTicket.GetFocusedRowCellValue("MillWeight"), 0)
                lblWeightTicket.Text = Ticket
                '++++++++++ Use for debug only +++++++++++++
                'Must to get ID from database.
                lblStroageID.Text = _MaterialSourceID.ToString
                '+++++++++++++++++++++++++++++++++++++++++++
                lblTicketID.Text = Ticket
                lblStroageName.Text = PlateLicense
                lblWeight.Text = weight.ToString("0.000")
                txtquantity.EditValue = quantity
                lblNetAmount.Text = quantity.ToString
                '+++++++ Cal weight per unit +++++
                WeightPUnit = weight / quantity
                '+++++++++++++ Lock object ++++++++++++++++++++
                UnLockObject()
                '++++++++++++++++++++++++++++++++++++++++++++++

                '++++++++++++++++++++ Clear ข้อมูล grid ก่อนการเลือกพื้นที่ใหม่ ++++++++++++++++++
                If (GridViewMoveMentDetail.RowCount > 0) Then
                    Dim iCountRows As Integer
                    For iCountRows = 0 To GridViewMoveMentDetail.RowCount - 1
                        GridViewMoveMentDetail.DeleteSelectedRows()
                    Next iCountRows
                End If
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Me.TabPane1.SelectNextPage()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [LayoutViewWeightTicket_CardClick]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

    End Class
End Namespace
