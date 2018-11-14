Imports FC.M.BLL_Util
Imports FC.M.PSL_Win.Classes_Helper
Imports FC.MainSQL.Modules
Imports FC.MainWinApp.Manager.Modules
Imports FC.MainApp.Modules
Imports FC.ThirdPartyHelper
Imports FC.SharedWinFormBus.Classes
Imports System.IO

Namespace Modules
    Module ModProgram

        Private _dicOfKeyAndValue As Dictionary(Of String, String) = Nothing

        Sub Main(ByVal args() As String)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            DevExpress.UserSkins.BonusSkins.Register()
            DevExpress.Skins.SkinManager.EnableFormSkins()

            Dim applicationName As String = String.Empty
            Dim appConfigDllFileName As String = String.Empty
            Dim deployConfigDllFileName As String = String.Empty
            Dim log4netFileName As String = String.Empty
            Dim springFileName As String = String.Empty
            Dim listOfDllFileName As String = String.Empty
            Dim serverIp As String = Util.GetServerIpFromClickOnceURL(_dicOfKeyAndValue)
            For Each kv As KeyValuePair(Of String, String) In _dicOfKeyAndValue
                If kv.Key.ToLower() = "app" Then
                    applicationName = kv.Value
                End If
            Next

            If serverIp <> String.Empty Then

                appConfigDllFileName = String.Format("http://{0}/FC/{1}/Config/AppConfig.dll", serverIp, applicationName)
                log4netFileName = String.Format("http://{0}/FC/{1}/Config/Log4Net.xml", serverIp, applicationName)
                springFileName = String.Format("http://{0}/FC/{1}/Config/Spring.xml", serverIp, applicationName)
                listOfDllFileName = String.Format("http://{0}/FC/{1}/Bin/", serverIp, applicationName)
                Try
                    Dim log4netDllFileName As String = String.Format("http://{0}/FC/{1}/Bin/log4net.dll", serverIp, applicationName)
                    Dim log4netDllAppFile As String = Util.FileCombine(Util.GetAppPath(), "log4net.dll")
                    If Not File.Exists(log4netDllAppFile) Then
                        Util.DownloadFileTo(log4netDllFileName, log4netDllAppFile)
                    End If
                Catch ex As Exception
                    WinUtil.ShowErrorBox(Util.GetErrorsToLines(ex))
                End Try
                If applicationName.ToUpper() = "MREPORT" Then
                    deployConfigDllFileName = String.Format("http://{0}/FC/Common/MProductShareFiles/Config/DeployConfig.dll", serverIp)
                    If _dicOfKeyAndValue.Count > 1 Then
                        If Not CheckAuthorizeForRunApp() Then
                            WinUtil.ShowWarningBox("You are not authorized to run application.")
                            End
                        End If
                        Try
                            FC.MainWinApp.Manager.Modules.ModManager.RunX10(applicationName, applicationName, serverIp, appConfigDllFileName, deployConfigDllFileName _
                                                                               , log4netFileName, springFileName, listOfDllFileName, False, Nothing, AddressOf SetUser, args)
                        Catch ex As Exception
                            MessageBox.Show(Util.GetErrorsToLines(ex))
                        End Try

                        End
                    End If
                Else
                    deployConfigDllFileName = String.Format("http://{0}/FC/{1}/Config/DeployConfig.dll", serverIp, applicationName)
                End If
 
            Else
                serverIp = "127.0.0.1" '"172.26.64.64" '
                applicationName = "IVM" '  "M2" '    "M2Web" '   "MQueue" ' "MReport" '  "PIS" ' "XDPackage" ' "Test"  '   "CostVariance" '  Change program name here
                deployConfigDllFileName = String.Format("C:\Forward Consulting\{0}\Config\DeployConfig.dll", applicationName)
                appConfigDllFileName = String.Format("C:\Forward Consulting\{0}\Config\AppConfig.dll", applicationName)
                log4netFileName = String.Format("C:\Forward Consulting\{0}\Config\Log4Net.xml", applicationName)
                springFileName = String.Format("C:\Forward Consulting\{0}\Config\Spring.xml", applicationName)
                listOfDllFileName = String.Format("C:\Forward Consulting\{0}\Bin", applicationName)
            End If

            Try
                FC.MainWinApp.Manager.Modules.ModManager.RunX10(applicationName, applicationName, serverIp, appConfigDllFileName, deployConfigDllFileName _
                                                            , log4netFileName, springFileName, listOfDllFileName, True, Nothing, Nothing, args)

            Catch ex As Exception
                MessageBox.Show(Util.GetErrorsToLines(ex))
            End Try

            End
        End Sub

        Private Function CheckAuthorizeForRunApp() As Boolean
            Dim lstOfProcess As New List(Of String)
            For Each p As Process In Process.GetProcesses
                lstOfProcess.Add(p.ProcessName)
            Next

            For Each processName As String In lstOfProcess
                Dim pName As String = processName.ToUpper()
                If {"MPLAN", "MSCALE", "MQUEUE", "MPLAN.VSHOST", "MSCALE.VSHOST", "MQUEUE.VSHOST"}.Contains(pName) Then
                    Return True
                End If
            Next

            Return False
        End Function

        Private Sub SetUser()
            Dim databaseIp As String = String.Empty
            Dim loginName As String = String.Empty

            For Each kv As KeyValuePair(Of String, String) In _dicOfKeyAndValue
                Select Case kv.Key
                    Case "dbip"
                        databaseIp = kv.Value
                    Case "ln"
                        loginName = kv.Value
                End Select
            Next

            If loginName.Trim() = String.Empty OrElse databaseIp.Trim() = String.Empty Then
                WinUtil.ShowErrorBox("Invalid argument supplied.")
                End
            End If

            If IsNothing(ModMainSQL.SQL) Then
                Dim databaseName As String = ModMainApp.DeployConfigDll.GetConfig("MReport", "SQL_Database_Name")
                ModManager.InitDefaultDatabase(databaseName, False)
            End If

            Dim rowUser As DataRow = User.FindByLoginName(loginName)
            If IsNothing(rowUser) Then
                WinUtil.ShowWarningBox("Not found login in system.")
                End
            End If
            If Not X10Role.CheckHaveOneOrMoreAssignRole(CInt(rowUser("ID"))) Then
                WinUtil.ShowWarningBox(String.Format("Not assign role to user {0} in system.", rowUser("LoginName")))
                End
            End If
            ModMainApp.SetUser(CInt(rowUser("ID")), DataHelper.DBNullOrNothingTo(Of String)(rowUser("FirstName"), String.Empty) _
                               , DataHelper.DBNullOrNothingTo(Of String)(rowUser("LastName"), String.Empty) _
                               , DataHelper.DBNullOrNothingTo(Of String)(rowUser("Email"), String.Empty))
            Log4NetHelper.SetPropertyLog4Net("userid", ModMainApp.UserId)
            Log4NetHelper.SetAppenderPropertyLog4Net("SmtpAppender", "From", New Object() {ModMainApp.ApplicationName + "<" + ModMainApp.DeployConfigDll.GetConfig("Common", "SMTP_User_Name") + ">"})
            Log4NetHelper.SetAppenderPropertyLog4Net("SmtpAppender", "Subject" _
                                                     , New Object() {String.Format("[Version={0}, ServerDB={1}, Sender={2}]", ModMainApp.ApplicationVersion, ModMainSQL.ServerNameOrIPAddress, Util.GetLocalIPAddress())})
        End Sub

    End Module
End Namespace

