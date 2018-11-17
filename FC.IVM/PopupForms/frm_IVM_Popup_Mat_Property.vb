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
    ''' <summary>ฟอร์มสำหรับเลือก คุณสมบัติของการ Unload วัตถุดิบ</summary>
    Public Class frm_IVM_Popup_Mat_Properties
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

        ''' <exclude />
        Dim dt As New DataTable()

        ''' <exclude />
        Public Sub New()
            Try
                InitializeComponent()
                'dt = func_IVM_Get_Material_Property()
                Dim colCategory As New GridColumn
                Dim colValue As New GridColumn
                Dim colCategory1 As New GridColumn
                Dim colValue1 As New GridColumn
                Dim colCategory2 As New GridColumn
                Dim colValue2 As New GridColumn

                GridLookUpMatProperties.Properties.DataSource = GetDataMatProperties()
                GridLookUpMatProperties.EditValue = tmpMatProperties

                colCategory.FieldName = "Category"
                colCategory.Caption = "กลุ่ม"
                colCategory.VisibleIndex = 0
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1

                colCategory1.FieldName = "Category"
                colCategory1.Caption = "กลุ่ม"
                colCategory1.VisibleIndex = 0
                colValue1.FieldName = "Value"
                colValue1.Caption = "รายละเอียด"
                colValue1.VisibleIndex = 1

                colCategory2.FieldName = "Category"
                colCategory2.Caption = "กลุ่ม"
                colCategory2.VisibleIndex = 0
                colValue2.FieldName = "Value"
                colValue2.Caption = "รายละเอียด"
                colValue2.VisibleIndex = 1

                Dim gViewMat As GridView = GridLookUpMatPropertiesView
                gViewMat.Columns.Clear()
                gViewMat.Columns.Add(colValue)

                With GridLookUpMatProperties.Properties
                    .DisplayMember = "Value"
                    .ValueMember = "Value"
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With

                GridLookUpTransferPoint.Properties.DataSource = GetDataTransferPointProperties()
                GridLookUpTransferPoint.EditValue = tmpTransferPointProperties
                Dim gViewTransferPoint As GridView = GridLookupTransferPointView
                gViewTransferPoint.Columns.Clear()
                gViewTransferPoint.Columns.Add(colValue1)
                With GridLookUpTransferPoint.Properties
                    .DisplayMember = "Value"
                    .ValueMember = "Value"
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With

                GridLookUpBalingSeal.Properties.DataSource = GetDataBalingSealProperties()
                GridLookUpBalingSeal.EditValue = tmpBalingSealProperties
                Dim gViewBalingSeal As GridView = GridLookupBalingSealView
                gViewBalingSeal.Columns.Clear()
                gViewBalingSeal.Columns.Add(colValue2)
                ' Hide the group panel
                gViewBalingSeal.OptionsView.ShowGroupPanel = False

                With GridLookUpBalingSeal.Properties
                    .DisplayMember = "Value"
                    .ValueMember = "Value"
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With

                initLookupContractor()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [New]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub
        ''' <summary>Get material properties data from database fill into datatable.</summary>
        ''' <returns>Datatable of material properties</returns>
        Function GetDataMatProperties() As DataTable
            Dim dtMatProperties As New DataTable
            Try
                dtMatProperties = func_IVM_Get_Material_Properties()
                Dim foundRow As DataRow() = Nothing
                foundRow = dtMatProperties.Select("Category <> 'Material'")
                If foundRow.Count > 0 Then
                    For Each row As DataRow In foundRow
                        row.Delete()
                    Next
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetDataMatProperties]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return dtMatProperties
        End Function
        ''' <summary>Get transferpoint properties data from database fill into datatable.</summary>
        ''' <returns>Datatable of transferpoint properties</returns>
        Function GetDataTransferPointProperties() As DataTable
            Dim dtTransferProperties As New DataTable
            Try
                dtTransferProperties = func_IVM_Get_Material_Properties()
                Dim foundRow As DataRow() = Nothing
                foundRow = dtTransferProperties.Select("Category <> 'TransferPoint'")
                If foundRow.Count > 0 Then
                    For Each row As DataRow In foundRow
                        row.Delete()
                    Next
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetDataTransferPointProperties]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return dtTransferProperties
        End Function
        ''' <summary>Get balingseal data from database fill into datatable.</summary>
        ''' <returns>Datatable of balingseal properties</returns>
        Function GetDataBalingSealProperties() As DataTable
            Dim dtBalingSealProperties As New DataTable
            Try
                dtBalingSealProperties = func_IVM_Get_Material_Properties()
                Dim foundRow As DataRow() = Nothing
                foundRow = dtBalingSealProperties.Select("Category <> 'BalingSeal'")
                If foundRow.Count > 0 Then
                    For Each row As DataRow In foundRow
                        row.Delete()
                    Next
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GetDataBalingSealProperties]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
            Return dtBalingSealProperties
        End Function
        ''' <exclude />
        Private Sub initLookupContractor()
            btnOK.Enabled = False
            GridLookUpContractor.Properties.NullText = "(เลือก ข้อมูล)"
            GridLookUpContractor.Properties.DataSource = func_View_IVM_Contractor_1954()
            GridLookUpContractor.Properties.ValueMember = "Name"
            GridLookUpContractor.Properties.View.ViewCaption = "ชื่อผู้รับเหมา"
            GridLookUpContractor.Properties.DisplayMember = GridLookUpContractor.Properties.ValueMember
            GridLookUpContractor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            GridLookUpContractor.Properties.PopupFormWidth = 350
            GridLookUpContractor.EditValue = tmpContractorProperties
        End Sub

        ''' <summary>ปุ่มสำหรับเลือก คุณสมบัติในการ Unload</summary>
        Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            Try
                If Not GridLookUpContractor.EditValue Is Nothing Then
                    tmpContractorProperties = GridLookUpContractor.EditValue.ToString()
                End If

                If Not GridLookUpMatProperties.EditValue Is Nothing Then
                    tmpMatProperties = GridLookUpMatProperties.EditValue.ToString()
                End If

                If Not GridLookUpBalingSeal.EditValue Is Nothing Then
                    tmpBalingSealProperties = GridLookUpBalingSeal.EditValue.ToString()
                End If

                If Not GridLookUpTransferPoint.EditValue Is Nothing Then
                    tmpTransferPointProperties = GridLookUpTransferPoint.EditValue.ToString()
                End If

                Me.Close()

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [btnOK_Click]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub GridLookUpMatProperties_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpMatProperties.EditValueChanged
            Try
                If (tmpMatProperties <> GridLookUpMatProperties.EditValue.ToString) Then
                    tmpMatProperties = GridLookUpMatProperties.EditValue.ToString
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpMatProperties_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub GridLookUpBalingSeal_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpBalingSeal.EditValueChanged
            Try
                If (tmpBalingSealProperties <> GridLookUpBalingSeal.EditValue.ToString) Then
                    tmpBalingSealProperties = GridLookUpBalingSeal.EditValue.ToString
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpBalingSeal_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub GridLookUpTransferPoint_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpTransferPoint.EditValueChanged
            Try
                If (tmpTransferPointProperties <> GridLookUpTransferPoint.EditValue.ToString) Then
                    tmpTransferPointProperties = GridLookUpTransferPoint.EditValue.ToString
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpTransferPoint_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub GridLookUpContractor_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpContractor.EditValueChanged
            Try
                If (tmpContractorProperties <> GridLookUpContractor.EditValue.ToString) Then
                    tmpContractorProperties = GridLookUpContractor.EditValue.ToString
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpContractor_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub check_input_data()
            Try
                If (CType(GridLookUpMatProperties.EditValue, String) <> "" _
                    And CType(GridLookUpTransferPoint.EditValue, String) <> "" _
                    And CType(GridLookUpBalingSeal.EditValue, String) <> "" _
                    And CType(GridLookUpContractor.EditValue, String) <> "") Then

                    btnOK.Enabled = True
                Else
                    btnOK.Enabled = False
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [check_input_data]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        'Private Sub frm_IVM_Popup_Mat_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        '    AddHandler Me.FormClosed, AddressOf ClearTempData
        'End Sub
        'Private Sub ClearTempData(sender As Object, e As FormClosedEventArgs)
        '    Try
        '        tmpContractorProperties = ""
        '        tmpTransferPointProperties = ""
        '        tmpBalingSealProperties = ""
        '        tmpMatProperties = ""
        '    Catch ex As Exception
        '        Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
        '        Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [ClearTempData]")
        '        Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
        '    End Try
        'End Sub
    End Class
End Namespace
