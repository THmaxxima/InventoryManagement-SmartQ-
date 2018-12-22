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
        Public Sub New()
            Try
                InitializeComponent()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [New]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        ''' <exclude />
        Private Sub initLookupContractor()
            Dim colValue As GridColumn
            GridLookUpContractor.Properties.DataSource = func_View_IVM_Contractor_1954()
            With GridLookUpContractor.Properties
                .DisplayMember = "Name"
                .ValueMember = .DisplayMember
                .View.BestFitColumns()
                .PopupFormWidth = 350
            End With
            GridLookupContractorView.Columns.Clear()
            colValue = New GridColumn
            colValue.FieldName = "Name"
            colValue.Caption = "รายละเอียด"
            colValue.VisibleIndex = 1
            GridLookupContractorView.Columns.Add(colValue)
            'GridLookUpContractor.Properties.DisplayMember = GridLookUpContractor.Properties.ValueMember
            GridLookUpContractor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
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

                If Not GridLookUpEditTruckCondition.EditValue Is Nothing Then
                    tmpTruckConditionProperties = GridLookUpEditTruckCondition.EditValue.ToString()
                End If

                If Not GridLookUpEditWedge.EditValue Is Nothing Then
                    tmpWedgeProperties = GridLookUpEditWedge.EditValue.ToString()
                End If

                tmpIsClose = False
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
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpMatProperties.EditValue, "")
                If (tmpMatProperties <> gridSelectValue) Then
                    tmpMatProperties = gridSelectValue
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
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpBalingSeal.EditValue, "")
                If (tmpBalingSealProperties <> gridSelectValue) Then
                    tmpBalingSealProperties = gridSelectValue
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
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpTransferPoint.EditValue, "")
                If (tmpTransferPointProperties <> gridSelectValue) Then
                    tmpTransferPointProperties = gridSelectValue
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
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpContractor.EditValue, "")
                If (tmpContractorProperties <> gridSelectValue) Then
                    tmpContractorProperties = gridSelectValue
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
                If (DataHelper.DBNullOrNothingTo(Of String)(GridLookUpMatProperties.EditValue, "") <> "" _
                    And DataHelper.DBNullOrNothingTo(Of String)(GridLookUpTransferPoint.EditValue, "") <> "" _
                    And DataHelper.DBNullOrNothingTo(Of String)(GridLookUpBalingSeal.EditValue, "") <> "" _
                    And DataHelper.DBNullOrNothingTo(Of String)(GridLookUpEditTruckCondition.EditValue, "") <> "" _
                    And DataHelper.DBNullOrNothingTo(Of String)(GridLookUpEditWedge.EditValue, "") <> "" _
                    And DataHelper.DBNullOrNothingTo(Of String)(GridLookUpContractor.EditValue, "") <> "") Then

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
            tmpIsClose = True
            Me.Close()
        End Sub

        Private Sub GridLookUpEditTruckCondition_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpEditTruckCondition.EditValueChanged
            Try
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpEditTruckCondition.EditValue, "")
                If (tmpTruckConditionProperties <> gridSelectValue) Then
                    tmpTruckConditionProperties = gridSelectValue
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpBalingSeal_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub GridLookUpEditWedge_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpEditWedge.EditValueChanged
            Try
                Dim gridSelectValue As String = DataHelper.DBNullOrNothingTo(Of String)(GridLookUpEditWedge.EditValue, "")
                If (tmpWedgeProperties <> gridSelectValue) Then
                    tmpWedgeProperties = gridSelectValue
                End If
                check_input_data()
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [GridLookUpBalingSeal_EditValueChanged]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub


        Private Sub frm_IVM_Popup_Mat_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try

                Me.FormBorderStyle = FormBorderStyle.None
                Dim colValue As GridColumn
                GridLookUpMatProperties.Properties.DataSource = func_IVM_Get_Material_Properties("Material")
                GridLookUpMatProperties.EditValue = tmpMatProperties
                With GridLookUpMatProperties.Properties
                    .DisplayMember = "Value"
                    .ValueMember = .DisplayMember
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With
                Dim gViewMat As GridView = GridLookUpMatPropertiesView
                gViewMat.Columns.Clear()

                colValue = New GridColumn
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1
                gViewMat.Columns.Add(colValue)

                GridLookUpTransferPoint.Properties.DataSource = func_IVM_Get_Material_Properties("TransferPoint")
                GridLookUpTransferPoint.EditValue = tmpTransferPointProperties
                With GridLookUpTransferPoint.Properties
                    .DisplayMember = "Value"
                    .ValueMember = .DisplayMember
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With
                Dim gViewTransferPoint As GridView = GridLookupTransferPointView
                gViewTransferPoint.Columns.Clear()
                colValue = New GridColumn
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1
                gViewTransferPoint.Columns.Add(colValue)

                GridLookUpBalingSeal.Properties.DataSource = func_IVM_Get_Material_Properties("BalingSeal")
                GridLookUpBalingSeal.EditValue = tmpBalingSealProperties
                With GridLookUpBalingSeal.Properties
                    .DisplayMember = "Value"
                    .ValueMember = .DisplayMember
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With
                Dim gViewBalingSeal As GridView = GridLookupBalingSealView
                gViewBalingSeal.Columns.Clear()
                colValue = New GridColumn
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1
                gViewBalingSeal.Columns.Add(colValue)
                '--------------------------------------------------------
                GridLookUpEditTruckCondition.Properties.DataSource = func_IVM_Get_Material_Properties("TruckCondition")
                GridLookUpEditTruckCondition.EditValue = tmpTruckConditionProperties
                With GridLookUpEditTruckCondition.Properties
                    .DisplayMember = "Value"
                    .ValueMember = .DisplayMember
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With
                Dim gViewTruckCondition As GridView = GridViewTruckCondition
                gViewTruckCondition.Columns.Clear()
                colValue = New GridColumn
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1
                gViewTruckCondition.Columns.Add(colValue)

                GridLookUpEditWedge.Properties.DataSource = func_IVM_Get_Material_Properties("Wedge")
                GridLookUpEditWedge.EditValue = tmpWedgeProperties
                With GridLookUpEditWedge.Properties
                    .DisplayMember = "Value"
                    .ValueMember = .DisplayMember
                    .View.BestFitColumns()
                    .PopupFormWidth = 350
                End With
                Dim gViewWedge As GridView = GridViewWedge
                gViewWedge.Columns.Clear()
                colValue = New GridColumn
                colValue.FieldName = "Value"
                colValue.Caption = "รายละเอียด"
                colValue.VisibleIndex = 1
                gViewWedge.Columns.Add(colValue)

                '********************************************
                initLookupContractor()
                '********************************************
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [frm_IVM_Popup_Mat_Properties_Load]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try

        End Sub

    End Class
End Namespace
