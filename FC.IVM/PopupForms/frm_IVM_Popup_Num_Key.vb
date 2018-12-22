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
Imports DevExpress.XtraLayout

Namespace PopupForms
    ''' <summary>Windows form สำหรับอำนวยความสะดวกในการป้อนตัวเลข</summary>
    ''' <remarks>
    '''   <para>ใช้งานในโปรเซส Unload วัตถุดิบ</para>
    ''' </remarks>
    Public Class frm_IVM_Popup_Num_Key
        'Inherits DevExpress.XtraLayout.Customization.UserCustomizationForm
        Inherits System.Windows.Forms.Form
        Implements IChildOfMainForm
        Public Event OnPassData As IChildOfMainForm.OnPassDataEventHandler Implements IChildOfMainForm.OnPassData
        Public Sub OnBeforeFormLoad(param As Object) Implements IChildOfMainForm.OnBeforeFormLoad
        End Sub
        Public Sub OnClickClear(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickClear
        End Sub
        Public Sub OnClickCustomButton(customButtonName As String, ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickCustomButton
        End Sub
        Public Sub OnClickReload(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickReload
        End Sub
        Public Sub OnClickSave(ByRef Optional showMessageType As EnumMainFormShowMessageType = EnumMainFormShowMessageType.None, ByRef Optional customMessage As String = "", ByRef Optional customCaption As String = "") Implements IChildOfMainForm.OnClickSave
        End Sub
        Public MatQTY As Decimal = 0

        Sub New()
            InitializeComponent()
            'Me.FormBorderStyle = FormBorderStyle.None
        End Sub

        Private Sub frm_IVM_Popup_Num_Key_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                txtQTY.EditValue = MatQTY
                txtQTY.Focus()
                txtQTY.ReadOnly = True
            Catch ex As Exception

            End Try
        End Sub

        Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
            Try
                If (CType(txtQTY.EditValue, Decimal) <= 0) Then
                    WinUtil.ShowWarningBox("จำนวนวัตถุดิบที่แก้ไขต้องมีค่า มากกว่า (0)", "กรุณาตรวจสอบข้อมูล")
                    txtQTY.EditValue = MatQTY
                    isEdit = False
                Else
                    tmpUnloadQuantity = CType(txtQTY.EditValue, Decimal)
                    isEdit = False
                    Me.Close()
                End If

            Catch ex As Exception

            End Try
        End Sub
        Private Sub press_num(sender As Object, e As EventArgs) Handles btn9.Click, btn8.Click, btn7.Click, btn6.Click, btn5.Click, btn4.Click, btn3.Click, btn2.Click, btn1.Click, btn0.Click, btnClear.Click, btnP.Click
            Try
                Dim btn As New DevExpress.XtraEditors.SimpleButton
                btn = CType(sender, DevExpress.XtraEditors.SimpleButton)

                If (txtQTY.Text = "0") Then
                    txtQTY.Text = String.Empty
                End If
                If (btn.Text = ".") Then
                    If (txtQTY.Text.Contains(".") = False) Then
                        txtQTY.Text &= btn.Text
                    End If
                ElseIf (btn.Text = "C") Then
                    txtQTY.Text = CDbl(0).ToString
                ElseIf (isEdit = True) Then
                    txtQTY.Text &= btn.Text
                    isEdit = True
                Else
                    txtQTY.Text = btn.Text
                    isEdit = True
                End If
                btnUpdate.Focus()
            Catch ex As Exception

            Finally

            End Try
        End Sub
        Private Sub frm_IVM_Popup_Num_Key_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
            e.Cancel = False
            tmpUnloadQuantity = CDec(txtQTY.Text)
            isEdit = False
        End Sub
        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub

    End Class
End Namespace
