Imports FC.IVM.Bus.Modules
Imports FC.MainApp.Modules
Imports FC.MainWinApp
Imports FC.M.BLL_Util
Imports FC.M.PSL_Win.Classes_Helper
Imports System.Windows.Documents
Imports System.Windows.Forms

Namespace PopupForms
    ''' <summary>หน้าจอสำหรับแสดงรายละเอียดของวัตถุดิบ ที่จัดเก็บไว้ในพื้นที่</summary>
    ''' <remarks>จะเรียกใช้งานได้ ในหน้าจอ Mainlayout zoom level 3</remarks>
    Public Class frm_IVM_Popup_Material_Info
        Implements IChildOfMainForm

        ''' <exclude />
        Private _GroupID As Integer
        ''' <exclude />
        Private _DT_MatInfo As DataTable
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
        Public Sub setParam(ByVal GroupID As Integer, ByVal DT_MatInfo As DataTable)
            Try
                _GroupID = GroupID
                _DT_MatInfo = DT_MatInfo
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name)
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [setParam]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub frm_IVM_Popup_Material_Info_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                Me.FormBorderStyle = FormBorderStyle.None
                Dim foundRow As DataRow() = Nothing

                If (_DT_MatInfo.Rows.Count > 0) Then
                    foundRow = _DT_MatInfo.Select("MaterialID IS NULL")
                    If foundRow.Count > 0 Then
                        For Each row As DataRow In foundRow
                            row.Delete()
                        Next
                    End If
                    GridMatInfo.DataSource = _DT_MatInfo
                Else
                    Dim GroupName As String = "ไม่พบข้อมูลวัตถุดิบ!"
                    Me.Text &= " [" & GroupName & " #" & _GroupID & "]"
                End If
            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, frm_Name & Me.Name.ToString & "]")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "Fnc := [frm_IVM_Popup_Material_Info_Load]")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub
    End Class
End Namespace
