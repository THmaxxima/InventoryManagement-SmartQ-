Imports FC.MainWinApp
Imports FC.SharedWinForm.Classes
Imports FC.M.BLL_Util
Imports DevExpress.XtraEditors
Imports FC.SharedWinFormBus.Classes
Imports FC.M.PSL_Win.Classes_Helper
Imports FC.MReportBus.Classes


Namespace Forms
    Public Class frmTest
        Implements IChildOfMainForm

        Public Event OnPassData As IChildOfMainForm.OnPassDataEventHandler Implements IChildOfMainForm.OnPassData

        Public Sub OnBeforeFormLoad(param As Object) Implements IChildOfMainForm.OnBeforeFormLoad

        End Sub

        Public Sub OnClickClear(Optional ByRef showMessageType As EnumMainFormShowMessageType = FC.MainWinApp.EnumMainFormShowMessageType.None, Optional ByRef customMessage As String = "", Optional ByRef customCaption As String = "") Implements IChildOfMainForm.OnClickClear

        End Sub

        Public Sub OnClickCustomButton(customButtonName As String, Optional ByRef showMessageType As EnumMainFormShowMessageType = FC.MainWinApp.EnumMainFormShowMessageType.None, Optional ByRef customMessage As String = "", Optional ByRef customCaption As String = "") Implements IChildOfMainForm.OnClickCustomButton

        End Sub

        Public Sub OnClickReload(Optional ByRef showMessageType As EnumMainFormShowMessageType = FC.MainWinApp.EnumMainFormShowMessageType.None, Optional ByRef customMessage As String = "", Optional ByRef customCaption As String = "") Implements IChildOfMainForm.OnClickReload
            'Try
            '    Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.InfoMesage, FC.MainSQL.Modules.SQL.GenScriptTable("127.0.0.1", "sa", "4Ward", "PIS", "MT_Material"))
            '    Infolog.ShowMessage(FC.M.PSL_Win.MessageType.InfoMesage)
            'Catch ex As Exception
            '    Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            'End Try



            'Try
            '    Dim dicOfOutput As Dictionary(Of Integer, DataTable) = Nothing
            '    Dim objIncompletedInput As IncompletedInput = Nothing
            '    GenericTableInterface.ImportFromExcelFile(1000000001, "D:\PIS_PlanTemplate_5805W4R0.xlsx", "Sheet1", dicOfOutput, objIncompletedInput, False, True, True, True, True, True)
            '    Dim x As Integer = dicOfOutput.Count
            '    'Dim p As String = FC.MainApp.Modules.ModMainApp.GetTempResourcePath()
            '    'Dim i As Integer = 0
            'Catch ex As Exception
            '    FC.M.PSL_Win.Classes_Helper.Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            'End Try
            'FC.MainWinApp.Manager.Modules.ModManager.ChangeDefaultConnectionTo(2) ' 2 is DataSourceID
            'FC.MainWinApp.Manager.Modules.ModManager.RevertToDefaultConnection()
            'Dim name As String = CStr(DataSource.Find(1)("Name"))


            'Try
            '    Dim fileName As String = "D:\Temp\CRTC_W02.xlsx"
            '    Dim sheetName As String = "NP"
            '    Dim dicOfOutput As Dictionary(Of Integer, DataTable) = Nothing
            '    Dim objIncompletedInput As IncompletedInput = Nothing
            '    GenericTableInterface.ImportFromExcelFile(1000000001, fileName, sheetName, dicOfOutput, objIncompletedInput, False, True, False, True, True, True)
            '    Dim x As Integer = 0
            '    ''Dim SelectedDate As String = "2015-07-20 00:00:00"
            '    ''Dim MachineId As Integer = 34
            '    ''Dim lstOfReportCriteria As New List(Of ReportCriteria)()
            '    ' ''Dim dicOfSortNoWithChartFile As New Dictionary(Of Integer, String)()

            '    ''lstOfReportCriteria.Add(ReportCriteria.Create("@SelectedDate", String.Empty, FC.SharedWinFormBus.Enums.EnumConditionLinker.And _
            '    ''                                                 , FC.SharedWinFormBus.Enums.EnumConditionOperator.Equal _
            '    ''                                                 , String.Empty, False, SelectedDate, String.Empty))
            '    ''lstOfReportCriteria.Add(ReportCriteria.Create("@SelectedMachine", String.Empty, FC.SharedWinFormBus.Enums.EnumConditionLinker.And _
            '    ''                                                 , FC.SharedWinFormBus.Enums.EnumConditionOperator.Equal _
            '    ''                                                 , String.Empty, False, CStr(MachineId), String.Empty))
            '    ''lstOfReportCriteria.Add(ReportCriteria.Create("@KPITypeList", String.Empty, FC.SharedWinFormBus.Enums.EnumConditionLinker.And _
            '    ''                                                 , FC.SharedWinFormBus.Enums.EnumConditionOperator.Equal _
            '    ''                                                 , String.Empty, False, "GrossMassYield,NetMassYield,Reject,Lock", String.Empty))


            '    ''Dim das As DataSet = FC.MReportWin.Modules.ModShare.GetReportData("Web-KPI-Chart", FC.MainSQL.Modules.ModMainSQL.SqlConStr, Nothing, lstOfReportCriteria, Nothing, String.Empty, String.Empty, Nothing)
            '    ''Dim tableCount As Integer = das.Tables.Count
            'Catch ex As Exception
            '    Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            'End Try

            Dim datPreview As New DataTable()
            datPreview.Columns.Add("Code")
            datPreview.Columns.Add("Qty", GetType(Integer))
            datPreview.Rows.Add("001", 1000)
            datPreview.Rows.Add("002", 2320)
            datPreview.Rows.Add("003", 2300)
            Dim datasources(0) As Object
            datasources(0) = datPreview
            Dim frm As New FC.MReportWin.Forms.frmRawData()
            frm.OnBeforeFormLoad(New Object() {0, "Preview Data", datasources, String.Empty})
            frm.ShowDialog()
        End Sub

        Public Sub OnClickSave(Optional ByRef showMessageType As EnumMainFormShowMessageType = FC.MainWinApp.EnumMainFormShowMessageType.None, Optional ByRef customMessage As String = "", Optional ByRef customCaption As String = "") Implements IChildOfMainForm.OnClickSave
            'FC.MainApp.Modules.ModMainApp.Log.Log4N("TestLog").DebugFormat("Test ")
            'FC.MainApp.Modules.ModMainApp.Log.Log4N("TestLog").Debug(String.Format("Test {0}", 1))
            'Dim obj As FC.SharedWinFormBus.Classes.GenericTableBase = FC.MainApp.Modules.ModMainApp.Spring.GetObject(Of FC.SharedWinFormBus.Classes.GenericTableBase)("MaterialTypeRepository")

            'Dim str As String = String.Format("Test {0}", 1)

            'Dim data As New DataTable()
            'FC.MainSQL.Modules.ModMainSQL.SQL.FillDataTable(data, "select * from Table1")
            'Dim sharedCon As Sqlconnection = FC.MainSQL.Modules.ModMainSQL.SQL.SharedCon.Open()
            'FC.MainSQL.Modules.ModMainSQL.SQL.FillDataTable(data, "select * from Table1", sharedCon)
            'Using con As Sqlconnection = FC.MainSQL.Modules.ModMainSQL.SQL.CreateConnection()
            '    con.Open()
            '    Dim cmd As SqlCommand = con.CreateCommand()
            '    cmd.E.......

            'End Using
            'FC.MainSQL.Modules.ModMainSQL.GetNextId("Table1", con, )
            'FC.MainSQL.Modules.ModMainSQL.SQL.SharedCon.Close()
            '' Category | Level | Message
            '' TestLog   |  | Test 1


            Try
                Throw New Exception("Test Error")
            Catch ex As Exception
                'แบบที่ 1
                WinUtil.ShowErrorBox(Util.GetErrorsToLines(ex), "Example Caption")

                'แบบที่ 2
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
                'Or 
                Infolog.ShowExMessageWithTopic(ex, FC.M.PSL_Win.MessageType.ErrorMessage, "Example Caption")

                'แบบที่ 3
                WinDevHelper.ShowAlertWindow(Me, FC.M.PSL_Win.MessageType.ErrorMessage, "Example Caption", Util.GetErrorsToLines(ex))
            End Try



        End Sub

        Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Dim sessionCompany As New AutoRelatedCombo(cmbCompany, FC.SharedWinFormBus.Enums.EnumComboMasterType.Spring, 1000000000)
            'Dim sessionPlant As New AutoRelatedCombo(cmbPlant, FC.SharedWinFormBus.Enums.EnumComboMasterType.Spring, 4)
            'Dim sessionMachine As New AutoRelatedCombo(cmbMachine, FC.SharedWinFormBus.Enums.EnumComboMasterType.Spring, 11)

            'sessionCompany.AddChildRelatedCombo(sessionPlant, "CompanyID", "ID")

            'sessionPlant.AddChildRelatedCombo(sessionMachine, "PlantID", "ID")

            ' New Binding (DT_Test_LV1)
            ' Add Child Binding (DT_Test_LV2) -> ChildFields,ParentFields  , createConstaint
            ' Add Child Binding (DT_Test_LV3) -> ChildFields,ParentFields  , createConstaint
            'Dim H As New BindingData(1)
            'Dim D1 As New BindingData(2)
            'Dim D2 As New BindingData(3)
            'D1.LinkParent(H, "ParentID", "ID")
            'D2.LinkParent(H, "ParentID", "ID")

            'Dim myBinding As New BindingData()
            'myBinding.AddTable(1)
            'myBinding.AddTable(2, 1)


            'Dim datH As New DataTable()
            'Dim datLV1 As New DataTable()
            'Dim datLV2 As New DataTable()

            'Dim das As New DataSet()
            'das.Tables.Add(datH)
            'das.Tables.Add(datLV1)
            'das.Tables.Add(datLV2)
            'das.Relations.Add("H1_LV1", datH.Columns("ID"), datLV1.Columns("ParentID"), False)
            'das.Relations.Add("H1_LV2", datH.Columns("ID"), datLV2.Columns("ParentID"), False)

            'Dim bindingH As New BindingSource()
            'bindingH.DataSource = das
            'bindingH.DataMember = datH.TableName

            'Dim bindingLV1 As New BindingSource()
            'bindingLV1.DataSource = bindingH
            'bindingLV1.DataMember = das.Relations(0).RelationName

            'Dim bindingLV2 As New BindingSource()
            'bindingLV2.DataSource = bindingLV1
            'bindingLV2.DataMember = das.Relations(1).RelationName

            'Dim frm As New XtraForm1()
            'For Each ctrl As Control In frm.LayoutControl1.Controls
            '    If ctrl.Name = String.Empty Then
            '        Continue For
            '    End If
            '    If TypeOf ctrl Is BaseEdit _
            '        OrElse TypeOf ctrl Is DevExpress.XtraTreeList.TreeList _
            '        OrElse TypeOf ctrl Is DevExpress.XtraGrid.GridControl _
            '        OrElse TypeOf ctrl Is DevExpress.XtraPivotGrid.PivotGridControl _
            '        OrElse TypeOf ctrl Is DevExpress.XtraVerticalGrid.PropertyGridControl _
            '        OrElse TypeOf ctrl Is DevExpress.XtraVerticalGrid.VGridControl _
            '        OrElse TypeOf ctrl Is DevExpress.XtraEditors.DataNavigator Then
            '        '_datControl.Rows.Add(ctrl.Name, ctrl.GetType().ToString())
            '        Console.WriteLine("{0} {1}", ctrl.Name, ctrl.GetType().ToString())
            '    End If
            'Next
            Try
                'Dim displayMember As String = String.Empty
                'Dim valueMember As String = String.Empty
                'Dim showColumnName As String = String.Empty
                'Dim dat As DataTable = Nothing
                'Dim comboNames() As String = {"EquipmentSerial -> ID"}
                'For Each comboName As String In comboNames
                '    dat = ComboBoxRepository.GetDataTableOfMaster(comboName, displayMember, valueMember, showColumnName)
                'Next

                'Dim strSql As String = String.Empty
                'Dim comboBoxIds() As Integer = {1030000007}
                'For Each comboBoxId As Integer In comboBoxIds
                '    strSql = ComboBoxRepository.GenStrSql(comboBoxId, True)
                'Next

            Catch ex As Exception
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
            End Try


        End Sub

        Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
            Dim rowStore As DataRow = StoreProcedure.Find(1)
            Dim datParam As DataTable = StoreProcedure.FindParams(1)
            Dim dicOfParamOutput As Dictionary(Of String, Object) = Nothing
            For Each row As DataRow In datParam.Select("ParamName = '@Date'")
                row("ExactValue") = "2017-01-01"
            Next

            Dim dasOutput As DataSet = StoreProcedure.ExecuteByNameConfigured(CStr(rowStore("StoreName")), datParam, dicOfParamOutput, "")

        End Sub
    End Class
End Namespace
