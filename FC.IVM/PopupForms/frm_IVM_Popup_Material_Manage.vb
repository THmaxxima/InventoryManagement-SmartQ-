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
        Public _IsClose As Boolean = False
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
        Private WeightPUnit As Decimal = 0
        Dim IsSave As Boolean = False
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
            lblMatID.Text = String.Empty
            lblMatName.Text = String.Empty
            txtquantity.EditValue = 0
            txtquantity.Enabled = False
            lblNetAmount.Text = "0.0"
            lblCaptionPLW.Visible = False
            lblPLWeight.Visible = False
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
                If (_siteName = "UnloadImport" Or _siteName = "Unload") Then

                    GridControlWeightTicket.DataSource = GetSubAreaINVDataSet()
                    GridControlWeightTicket.Visible = True
                    GridControlSouceArea.Visible = False

                    LayoutViewWeightTicket.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    LayoutViewWeightTicket.Columns(7).DisplayFormat.FormatString = "{0:n1}"
                    LayoutViewWeightTicket.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    LayoutViewWeightTicket.Columns(8).DisplayFormat.FormatString = "{0:n3}"

                    If (_siteName = "Unload") Then
                        lblCaptionPLW.Visible = True
                        lblPLWeight.Visible = True
                        LayoutViewWeightTicket.Columns(12).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        LayoutViewWeightTicket.Columns(12).DisplayFormat.FormatString = "{0:n3}"
                    Else
                        lblCaptionPLW.Visible = False
                        lblPLWeight.Visible = False
                    End If
                Else
                    GridControlSouceArea.DataSource = GetSubAreaINVDataSet()
                    GridControlWeightTicket.Visible = False
                    GridControlSouceArea.Visible = True
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    lblStroageName.Text = "พื้นที่ต้นทาง"
                    btnProperties.Enabled = False
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    lblCaptionPLW.Visible = True
                    lblPLWeight.Visible = True

                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [InitData]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub LayoutViewSouceArea_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewSouceArea.CardClick

            Try
                Dim rowind As String
                Dim quantity As Decimal
                Dim areaid As String
                Dim matid As String
                Dim matname As String
                Dim areaname As String
                Dim weight As Decimal

                rowind = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewSouceArea.FocusedRowHandle, 0)
                areaid = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewSouceArea.GetFocusedRowCellValue("StorageID"), 0)
                areaname = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewSouceArea.GetFocusedRowCellValue("StorageName"), 0)
                matid = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewSouceArea.GetFocusedRowCellValue("MaterialID"), 0)
                matname = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewSouceArea.GetFocusedRowCellValue("MaterialName"), 0)
                quantity = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewSouceArea.GetFocusedRowCellValue("Quantity"), 0)
                weight = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewSouceArea.GetFocusedRowCellValue("WeightADT"), 0)
                'DataHelper.DBNullOrNothingTo(Of Double)(LayoutViewSouceArea.GetFocusedRowCellValue("MillWeight"), 0)
                '+++++ If not unload must to clear value ++++++
                lblTicketID.Text = String.Empty
                '++++++++++++++++++++++++++++++++++++++++++++++
                lblStroageID.Text = DataHelper.DBNullOrNothingTo(Of String)(areaid, "-1")
                lblStroageName.Text = DataHelper.DBNullOrNothingTo(Of String)(areaname, "-")
                lblWeight.Text = DataHelper.DBNullOrNothingTo(Of String)(weight, "0.000")
                txtquantity.EditValue = DataHelper.DBNullOrNothingTo(Of Decimal)(quantity, 0)
                lblNetAmount.Text = DataHelper.DBNullOrNothingTo(Of String)(quantity, "0")
                '+++++++ Cal weight per unit +++++
                WeightPUnit = DataHelper.DBNullOrNothingTo(Of Decimal)((weight / quantity), 0)
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
                searchlookupmat.EditValue = DataHelper.DBNullOrNothingTo(Of Integer)(LayoutViewRename.GetFocusedRowCellValue("MaterialID"), 0)
                searchlookupmat.SelectedText = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewRename.GetFocusedRowCellValue("MaterialName"), "")
                matid = DataHelper.DBNullOrNothingTo(Of String)(searchlookupmat.EditValue, "0")
                matname = DataHelper.DBNullOrNothingTo(Of String)(searchlookupmat.SelectedText, "-")
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

        Function GetWPInfo(ByVal FieldID As Integer) As DataTable
            Try
                '******************************************************
                DTWP = func_IVM_Get_WP_Info(FieldID)
                '******************************************************
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetWPInfo]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return DTWP
        End Function
        Function GetSubAreaINVDataSet() As DataTable
            Dim Res_DT As New DataTable()
            Try
                Dim DataView As New DataView()
                Dim SiteNo As String = "ลาน" & _fieldID

                If (_GroupID > 0 And (_siteName = "Unload")) Then
                    DT_Area_Data = New DataTable()
                    DT_Area_Data = func_IVM_Get_WeightTicketData(DataHelper.DBNullOrNothingTo(Of Integer)(1, 0), _PUserID) 'Fix Material Type=1 in proc
                    DataView = DT_Area_Data.DefaultView()

                    If (CheckShowAll.Checked = True) Then
                        DataView.RowFilter = ("MaterialSource LIKE '%Local%'")
                        If (DataView.Count > 0) Then
                            Res_DT = DataView.ToTable
                            Res_DT.AcceptChanges()
                        End If
                    Else

                        DataView.RowFilter = "MaterialSource LIKE '%Local%' and UnloadStation LIKE '%" & SiteNo & "%'"
                        If (DataView.Count > 0) Then
                            Res_DT = DataView.ToTable
                            Res_DT.AcceptChanges()
                        End If
                    End If

                ElseIf (_GroupID > 0 And (_siteName = "UnloadImport")) Then
                    DT_Area_Data = New DataTable()
                    DT_Area_Data = func_IVM_Get_WeightTicketData(DataHelper.DBNullOrNothingTo(Of Integer)(1, 0), _PUserID) 'Fix Material Type=1 in proc
                    DataView = DT_Area_Data.DefaultView()
                    If (CheckShowAll.Checked = True) Then
                        DataView.RowFilter = ("MaterialSource LIKE '%Import%'")
                        If (DataView.Count > 0) Then
                            Res_DT = DataView.ToTable
                            Res_DT.AcceptChanges()
                        End If
                    Else
                        DataView.RowFilter = "MaterialSource LIKE '%Import%' and UnloadStation LIKE '%" & SiteNo & "%'"
                        If (DataView.Count > 0) Then
                            Res_DT = DataView.ToTable
                            Res_DT.AcceptChanges()
                        End If
                    End If

                Else
                    DT_Area_Data = New DataTable()
                    DT_Area_Data = func_IVM_Get_ChildStorageData(_GroupID, CInt(_fieldID))
                    DataView = DT_Area_Data.DefaultView()
                    DataView.RowFilter = ("MaterialID IS NOT NULL")
                    If (DataView.Count > 0) Then
                        Res_DT = DataView.ToTable
                        Res_DT.AcceptChanges()
                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetSubAreaINVDataSet]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return Res_DT
        End Function
        ''' <exclude />
        Private Sub frm_IVM_Popup_Material_Manage_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                Me.FormBorderStyle = FormBorderStyle.None
                WinDevHelper.ShowWaitForm(Me)
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Prepare_Grid_Movement_Souce()
                InitData()
                gridControlDestGroupArea.DataSource = GetGroupAreaInfo(CInt(_fieldID))
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                AddHandler GridViewMoveMentDetail.ShowingPopupEditForm, AddressOf GridViewMoveMentDetail_ShowingPopupEditForm
                GridViewMoveMentDetail.OptionsBehavior.EditingMode = GridEditingMode.EditForm

                AddHandler GridViewMoveMentDetail.EditFormShowing, AddressOf OnEditFormShowing

                initLookupMaterial()
                Dim find As FindControl = TryCast(LayoutViewSouceArea.GridControl.Controls.Find("FindControl", True)(0), FindControl)
                find.FindEdit.Focus()
                find.FocusFindEdit()

                If (_ToDo = "Unload" Or _ToDo = "UnloadImport") Then
                    'UnLockObject()
                    TabNP_WP.PageVisible = False
                    lblWeightTicket.Visible = True
                    If (_ToDo = "Unload") Then
                        lblCaptionPLW.Visible = True
                        lblPLWeight.Visible = True
                    Else
                        lblCaptionPLW.Visible = False
                        lblPLWeight.Visible = False
                    End If
                    LayoutViewWeightTicket.Focus()
                    SearchMaterial.ReadOnly = True
                    CheckShowAll.Visible = True
                    CheckShowAll.Enabled = True
                Else
                    LockObject()
                    TabNP_WP.PageVisible = True
                    lblWeightTicket.Visible = False
                    lblCaptionPLW.Visible = False
                    lblPLWeight.Visible = False
                    LayoutViewSouceArea.Focus()
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [frm_IVM_Popup_Material_Manage_Load]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            Finally
                WinDevHelper.CloseWaitForm()
            End Try
        End Sub
        Private Sub OnEditFormShowing(sender As Object, e As EditFormShowingEventArgs)
            Try
                Dim view As GridView = TryCast(sender, GridView)
                If view Is Nothing Then
                    Return
                End If
                If (IsSave = True) Then e.Allow = False
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [OnEditFormShowing]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try

        End Sub
        ''' <exclude />
        Private Sub GridViewMoveMentDetail_ValidateRow(ByVal sender As Object,
                ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) _
                Handles GridViewMoveMentDetail.ValidateRow
            Try
                Dim View As GridView = CType(sender, GridView)
                Dim Quantity As GridColumn = DataHelper.DBNullOrNothingTo(Of GridColumn)(View.Columns("Quantity"), 0)
                Dim matBalance As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(lblNetAmount.Text, 0)
                Dim moveQuantity As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(View.GetRowCellValue(e.RowHandle, Quantity), 0)
                Dim sum_QTY As Decimal = 0
                '++++++++++++++++++++++++++++++++++++++++++
                For i As Integer = 0 To GridViewMoveMentDetail.DataRowCount - 1
                    sum_QTY += DataHelper.DBNullOrNothingTo(Of Decimal)(GridViewMoveMentDetail.GetRowCellValue(i, "Quantity"), 0)
                Next i
                If (GridViewMoveMentDetail.IsNewItemRow(GridViewMoveMentDetail.FocusedRowHandle) = True) Then
                    sum_QTY += DataHelper.DBNullOrNothingTo(Of Decimal)(moveQuantity, 0)
                End If
                '++++++++++++++++++++++++++++++++++++++++++
                'Validity criterion
                If (sum_QTY > matBalance) Then
                    e.Valid = False
                    'Set errors with specific descriptions for the columns
                    View.SetColumnError(Quantity, "จำนวนวัตถุดิบไม่เพียงพอ!")
                    isEdit = False
                ElseIf (moveQuantity <= 0 And sum_QTY >= 0) Then
                    e.Valid = False
                    View.SetColumnError(Quantity, "จำนวนวัตถุดิบต้องมากกว่า 0!")
                    isEdit = False
                Else
                    isEdit = True
                    txtquantity.EditValue = DataHelper.DBNullOrNothingTo(Of Decimal)((matBalance - sum_QTY), 0)
                    lblWeight.Text = DataHelper.DBNullOrNothingTo(Of Decimal)(CDec(txtquantity.EditValue) * WeightPUnit, 0).ToString("##0.000")
                    'If (DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0) <= 0) Then
                    '    txtquantity.Enabled = False
                    'End If
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
            Dim View As GridView = CType(sender, GridView)
            Dim Quantity As GridColumn = DataHelper.DBNullOrNothingTo(Of GridColumn)(View.Columns("Quantity"), 0)
            Dim GridQTYMessage As String = CType(View.GetColumnError(Quantity), String)
            WinUtil.ShowWarningBox("ระบุจำนวนวัตถุดิบผิดพลาด := " & GridQTYMessage, "กรุณาตรวจสอบข้อมูล")
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
                    GridViewMoveMentDetail.SetFocusedRowCellValue("Quantity", DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupID", DataHelper.DBNullOrNothingTo(Of Integer)(LayoutViewWP.GetFocusedRowCellValue("StorageID"), 0))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupName", DataHelper.DBNullOrNothingTo(Of String)(LayoutViewWP.GetFocusedRowCellValue("StorageName"), "-"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialID", "0")
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialName", String.Empty)
                    GridViewMoveMentDetail.SetFocusedRowCellValue("PackageSize", "A")
                Else
                    GridViewMoveMentDetail.SetFocusedRowCellValue("Quantity", DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupID", DataHelper.DBNullOrNothingTo(Of Integer)(LayoutViewDestSubArea.GetFocusedRowCellValue("StorageID"), 0))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("GroupName", DataHelper.DBNullOrNothingTo(Of String)(LayoutViewDestSubArea.GetFocusedRowCellValue("StorageName"), "-"))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialID", DataHelper.DBNullOrNothingTo(Of Integer)(LayoutViewDestSubArea.GetFocusedRowCellValue("MaterialID"), 0))
                    GridViewMoveMentDetail.SetFocusedRowCellValue("MaterialName", DataHelper.DBNullOrNothingTo(Of String)(LayoutViewDestSubArea.GetFocusedRowCellValue("MaterialName"), "-"))
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
            dataTable.Rows.Add("A", "Size A")
            dataTable.Rows.Add("B", "Size B")
            dataTable.Rows.Add("C", "Size C")
            dataTable.Rows.Add("D", "Size D")
            dataTable.Rows.Add("E", "Size E")
            dataTable.Rows.Add("F", "Size F")
            Return dataTable
        End Function
        Private Sub initLookupMaterial()
            AddHandler SearchMaterial.EditValueChanged, AddressOf SearchMaterial_EditValueChanged
            Dim DT_GetMaterial As DataTable = func_IVM_Getmaterial()
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
                label.Text = DataHelper.DBNullOrNothingTo(Of String)(lookupEditor.EditValue, "-")
                lblMatName.Text = DataHelper.DBNullOrNothingTo(Of String)(lookupEditor.Text, "-")
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
                    WinUtil.ShowWarningBox("ไม่สามารถนำเข้าวัตถุดิบได้ เนื่องจากพื้นที่เต็ม!", "พื้นที่กองเก็บเต็ม")
                    Return
                Else
                    Dim rowind As String
                    Dim areaid As String
                    Dim areaname As String
                    rowind = DataHelper.DBNullOrNothingTo(Of String)(layoutViewDestGroupArea.FocusedRowHandle, "0")
                    areaid = DataHelper.DBNullOrNothingTo(Of String)(layoutViewDestGroupArea.GetFocusedRowCellValue("GroupID"), "0")
                    areaname = DataHelper.DBNullOrNothingTo(Of String)(layoutViewDestGroupArea.GetFocusedRowCellValue("GroupName"), "0")
                    GetTarketSubAreaDataSet(areaid, DataHelper.DBNullOrNothingTo(Of Integer)(_fieldID, -1))
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
                DT_Tarket_Area_Data = func_IVM_Get_ChildStorageData(CInt(GroupID), FieldID)
                DT_Tarket_Area_Data.TableName = "TarketSubAreaData"
                DS_Tarket_Area_Data.Tables.Add(DT_Tarket_Area_Data)
                DS_Tarket_Area_Data.DataSetName = "TarketSubAreaData"
                GridControlDestSubArea.DataSource = DS_Tarket_Area_Data.Tables("TarketSubAreaData")
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
            If (IsSave = True) Then
                Return
            End If
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
                    Dim summaryValue As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(GridViewMoveMentDetail.Columns(4).SummaryItem.SummaryValue, 0)
                    If (((_ToDo = "Unload" Or _ToDo = "UnloadImport") And CDec(txtquantity.EditValue) = 0)) Then
                        If (lblTruckProperties.Text = "") Then
                            WinUtil.ShowWarningBox("กรุณาระบุ ตรวจสอบ Properties ในการตรวรับ ก่อนการบันทึกข้อมูล", "ผลการตรวจสอบ")
                        Else
                            If (tmpContractorProperties = "" Or tmpMatProperties = "" Or tmpBalingSealProperties = "" Or
                            tmpTransferPointProperties = "") Then
                                WinUtil.ShowWarningBox("กรุณาระบุ Properties ในการตรวรับ ให้ครบถ้วน ก่อนการบันทึกข้อมูล", "ผลการตรวจสอบ")
                            Else
                                '+++++++++++++++++++++++++Call proc to save data +++++++++++++++++++++++++
                                res = CType(func_IVM_UnloadTruckToTemp_1859(DataHelper.DBNullOrNothingTo(Of String)(lblTicketID.Text, "-"),
                                DataHelper.DBNullOrNothingTo(Of Decimal)(summaryValue, 0),
                                DataHelper.DBNullOrNothingTo(Of Integer)(lblMatID.Text, -1),
                                DataHelper.DBNullOrNothingTo(Of String)(tmpContractorProperties, "-"),
                                DataHelper.DBNullOrNothingTo(Of Integer)(_PUserID, 0),
                                ViewData,
                                DataHelper.DBNullOrNothingTo(Of String)(tmpMatProperties, "-"),
                                DataHelper.DBNullOrNothingTo(Of String)(tmpBalingSealProperties, "-"),
                                DataHelper.DBNullOrNothingTo(Of String)(tmpTransferPointProperties, "-"),
                                DataHelper.DBNullOrNothingTo(Of String)(tmpWedgeProperties, "-"),
                                DataHelper.DBNullOrNothingTo(Of String)(tmpTruckConditionProperties, "-")), String)
                                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                If (res = "-1") Then
                                    '+++++++++++++++++++++++++++++++++
                                    btnSave.Enabled = False
                                    Me.TabPane1.Pages(1).Select()
                                    '+++++++++++++++++++++++++++++++++
                                    WinDevHelper.ShowAlertWindowSaveCompleted(Me, "เสร็จสมบูรณ์", "Saved Successfully.")
                                    btnProperties.Enabled = False
                                    btnSave.Enabled = False
                                    SearchMaterial.ReadOnly = True
                                    IsSave = True

                                    'Dim edit As New RepositoryItemButtonEdit
                                    'edit = CType(EditRow, RepositoryItemButtonEdit)


                                End If
                            End If
                        End If
                    ElseIf (_ToDo = "MoveInv") Then
                        For icountGridRow = 0 To ViewData.RowCount - 1
                            Dim SouceAreaID As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(ViewData.GetRowCellValue(icountGridRow, "GroupID"), 0)
                            Dim MatID As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(lblMatID.Text, 0) ' CType(lblMatID.Text, Integer)
                            Dim Amount As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(ViewData.GetRowCellValue(icountGridRow, "Quantity"), 0) 'CType(ViewData.GetRowCellValue(icountGridRow, "Quantity"), Double)
                            res = func_IVM_MoveStockTemp(DataHelper.DBNullOrNothingTo(Of Integer)(lblStroageID.Text, 0), SouceAreaID, MatID, Amount,
                                  DataHelper.DBNullOrNothingTo(Of Integer)(_PUserID, 0)).ToString
                        Next

                        If (res = "-1") Then
                            '+++++++++++++++++++++++++++++++++
                            btnSave.Enabled = False
                            Me.TabPane1.Pages(1).Select()
                            '+++++++++++++++++++++++++++++++++
                            WinDevHelper.ShowAlertWindowSaveCompleted(Me, "เสร็จสมบูรณ์", "Saved Successfully.")
                            btnProperties.Enabled = False
                            btnSave.Enabled = False
                            IsSave = True
                        End If
                    Else
                        WinUtil.ShowInfoBox("จำนวนวัตถุดิบยังคงเหลืออยู่ หากต้องการบันทึกข้อมูล" & vbCrLf & " กรุณาแก้ไขจำนวนวัตถุดิบให้ถูกต้องก่อน ทำรายการ",
                                            "กรุณาตรวจสอบข้อมูล" & _ToDo)
                        btnProperties.Enabled = True
                        btnSave.Enabled = True
                        IsSave = False
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
            'Dim DelRow As GridColumn = gv.Columns("GridColumn1")
            Dim FRow As Integer = gv.FocusedRowHandle
            Try

                'Dim CellValue As String = gv.GetFocusedRowCellValue(DelRow).ToString()
                'If CellValue = "GridColumn1" Then e.Cancel = True

                If (IsSave = True) Then
                    Return
                End If
                If gv.FocusedColumn.FieldName = "colCustomAction" Then
                    e.Cancel = True
                    Dim moveQuantity As Decimal = DataHelper.DBNullOrNothingTo(Of Decimal)(gv.GetRowCellValue(FRow, "Quantity"), 0)
                    gv.DeleteSelectedRows()
                    txtquantity.EditValue = DataHelper.DBNullOrNothingTo(Of Decimal)(CDec(txtquantity.EditValue) + moveQuantity, 0)
                    lblWeight.Text = DataHelper.DBNullOrNothingTo(Of Decimal)(CDec(txtquantity.EditValue) * WeightPUnit, 0).ToString("##0.000")
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
            lblMatNameInfo.Location = lblLocat
            SearchMaterial.Location = SearchMaterialLocat
            CheckShowAll.Visible = True
            lblTruckProperties.Visible = True
            btnProperties.Enabled = True
            btnSave.Enabled = True
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
            SearchMaterial.Location = SearchMaterialLocat
            CheckShowAll.Visible = False
            CheckShowAll.Enabled = False
            lblTruckProperties.Visible = False
            btnProperties.Enabled = False
            btnSave.Enabled = False
            '++++++++++++++++++++++++++++++++++++++++++++++
        End Sub
        Private Sub LayoutViewDestSubArea_CardClick(sender As Object, e As CardClickEventArgs) Handles LayoutViewDestSubArea.CardClick
            Try
                If (CDec(txtquantity.EditValue) <= 0) Then Return
                If (IsSave = True) Then Return

                Dim View As LayoutView = TryCast(sender, LayoutView)
                '++++++++++++++++++ Prepare data for popup customform +++++++++++++++++
                list.Clear()
                Dim strings(0) As String
                strings(0) = "Inventory amount"
                Dim item As IVMClass.Item = New IVMClass.Item()
                item.AutoNumber = False
                item.ColumnName = "Quantity"
                item.ColumnName2 = DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0).ToString
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
                Dim storageID As String = DataHelper.DBNullOrNothingTo(Of String)(View.GetFocusedRowCellValue("StorageID"), "0")
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
                    txtquantity.Enabled = False
                    SearchMaterial.ReadOnly = True
                Else
                    btnSave.Enabled = False
                    If (tmpUnloadQuantity > 0 And (_ToDo = "Unload" Or _ToDo = "UnloadImport")) Then
                        txtquantity.Enabled = True
                        SearchMaterial.ReadOnly = False
                    End If
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridViewMoveMentDetail_RowCountChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        Private Sub CheckShowAll_EditValueChanged(sender As Object, e As EventArgs) Handles CheckShowAll.EditValueChanged
            Try
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
                item.ColumnName2 = DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0).ToString
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
                Dim storageID As String = DataHelper.DBNullOrNothingTo(Of String)(View.GetFocusedRowCellValue("StorageID"), "0")
                If (Not (DataHelper.DBNullOrNothingTo(Of Integer)(storageID, 0)) = DataHelper.DBNullOrNothingTo(Of Integer)(lblStroageID.Text, -1)) Then
                    GetData()
                    SearchMaterial.Focus()
                    GridViewMoveMentDetail.ShowEditForm()
                Else
                    WinDevHelper.ShowAlertWindowSaveCompleted(Me, "กรุณาตรวจสอบข้อมูล พีื้นที่", "ไม่สามารถย้ายวัตถุดิบเข้าสู่พื้นที่เดียวกันได้")
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
                    frmPopupNumkey.MatQTY = DataHelper.DBNullOrNothingTo(Of Decimal)(txtquantity.EditValue, 0)
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
                txtquantity.EditValue = DataHelper.DBNullOrNothingTo(Of Decimal)(tmpUnloadQuantity, 0)
                lblNetAmount.Text = DataHelper.DBNullOrNothingTo(Of String)(tmpUnloadQuantity, "0")
                'lblWeight.Text = (CType(txtquantity.EditValue, Decimal) * WeightPUnit).ToString("##0.000")
                lblWeight.Text = DataHelper.DBNullOrNothingTo(Of Decimal)(CDec(txtquantity.EditValue) * WeightPUnit, 0).ToString("##0.000")
                layoutViewDestGroupArea.Focus()
                gridControlDestGroupArea.Focus()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
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
                If (tmpIsClose = True) Then
                    Return
                Else
                    tmpIsClose = False

                    If (tmpBalingSealProperties = "") Then
                        Return
                    End If
                    If (tmpMatProperties = "") Then
                        Return
                    End If
                    If (tmpTransferPointProperties = "") Then
                        Return
                    End If
                    If (tmpContractorProperties = "") Then
                        Return
                    End If
                    If (tmpTruckConditionProperties = "") Then
                        Return
                    End If
                    If (tmpWedgeProperties = "") Then
                        Return
                    End If
                    lblTruckProperties.Text = "" &
                        "ซีลโรงอัด : " & tmpBalingSealProperties & vbCrLf & "จุดขนถ่าย : " & tmpTransferPointProperties & vbCrLf &
                        "ชนิดวัตถุดิบ : " & tmpMatProperties & vbCrLf & "ผู้รับเหมา : " & tmpContractorProperties & vbCrLf &
                        "สภาพรถ : " & tmpTruckConditionProperties & vbCrLf & "การใช้ลิ่ม : " & tmpWedgeProperties
                End If

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
            Dim quantity As Decimal = 0
            Dim PLWeight As Double = 0
            Dim Ticket As String
            Dim ReceiveDate As DateTime
            Dim PlateLicense As String
            Dim SupplierName As String

            Try
                rowind = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewWeightTicket.FocusedRowHandle, "0")
                Ticket = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewWeightTicket.GetFocusedRowCellValue("Ticket"), "-")
                PlateLicense = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewWeightTicket.GetFocusedRowCellValue("PlateLicense"), "-")
                ReceiveDate = DataHelper.DBNullOrNothingTo(Of DateTime)(LayoutViewWeightTicket.GetFocusedRowCellValue("ReceiveDate"), "000-00-00 00:00:00")
                SupplierName = DataHelper.DBNullOrNothingTo(Of String)(LayoutViewWeightTicket.GetFocusedRowCellValue("SupplierName"), "-")
                '+++++++++++++++++++ Binding Search lookupedit Mat +++++++++++++++++++++++++++
                BindingSearchMaterial()
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                quantity = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewWeightTicket.GetFocusedRowCellValue("Quantity"), 0)
                weight = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewWeightTicket.GetFocusedRowCellValue("MillWeight"), 0)
                lblWeightTicket.Text = Ticket
                tmpBeginQTY = quantity
                '++++++++++ Use for debug only +++++++++++++
                lblStroageID.Text = _MaterialSourceID.ToString
                '+++++++++++++++++++++++++++++++++++++++++++
                lblTicketID.Text = DataHelper.DBNullOrNothingTo(Of String)(Ticket, "-")
                lblStroageName.Text = DataHelper.DBNullOrNothingTo(Of String)(PlateLicense, "-")
                lblWeight.Text = DataHelper.DBNullOrNothingTo(Of Decimal)(weight, 0).ToString("##0.000")
                lblPLWeight.Text = DataHelper.DBNullOrNothingTo(Of Decimal)(LayoutViewWeightTicket.GetFocusedRowCellValue("PLWeight"), 0).ToString("##0.000")
                txtquantity.EditValue = DataHelper.DBNullOrNothingTo(Of Decimal)(quantity, 0)
                lblNetAmount.Text = DataHelper.DBNullOrNothingTo(Of String)(quantity, "0")
                '+++++++ Cal weight per unit +++++
                WeightPUnit = DataHelper.DBNullOrNothingTo(Of Decimal)((weight / quantity), 0)
                '+++++++++++++ Lock object ++++++++++++++++++++
                UnLockObject()
                txtquantity.Enabled = True
                btnSave.Enabled = False
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

        Private Sub txtquantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtquantity.KeyPress
            txtquantity.ReadOnly = True
        End Sub

        Private Sub txtquantity_MouseUp(sender As Object, e As MouseEventArgs) Handles txtquantity.MouseUp
            lblNetAmount.Text = DataHelper.DBNullOrNothingTo(Of String)(tmpBeginQTY, "0")
        End Sub

        Private Sub frm_IVM_Popup_Material_Manage_Closed(sender As Object, e As EventArgs) Handles Me.Closed
            IsSave = False
        End Sub

    End Class
End Namespace
