Imports FC.IVM.Bus.Modules
Imports FC.MainApp.Modules
Imports FC.MainWinApp

Namespace Forms
    Public Class frm_IVM_Test
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

        Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
            MemoEdit1.Text = String.Format("UserID:{0}, UserName:{1}, Email:{2}" _
                                           , FC.MainApp.Modules.ModMainApp.UserId _
                                           , FC.MainApp.Modules.ModMainApp.UserName _
                                           , FC.MainApp.Modules.ModMainApp.UserEmail)


        End Sub

        Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
            ModMainApp.Log.Log4N("MyCategory").DebugFormat("Test debug at {0}", DateTime.Now)
            ModMainApp.Log.Log4N("MyCategory").InfoFormat("Test info at {0}", DateTime.Now)
            ModMainApp.Log.Log4N("MyCategory").WarnFormat("Test warn at {0}", DateTime.Now)
            ModMainApp.Log.Log4N("MyCategory").ErrorFormat("Test error at {0}", DateTime.Now)
            ModMainApp.Log.Log4N("MyCategory").FatalFormat("Test fatal at {0}", DateTime.Now)
        End Sub

        Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
            mod_IVM_center.TestInsert()

        End Sub
    End Class
End Namespace
