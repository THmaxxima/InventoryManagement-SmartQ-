Imports DevExpress.XtraGrid
Imports FC.M.BLL_Util
Imports FC.M.PSL_Win.Classes_Helper
Imports DevExpress.XtraGrid.Views.Grid
Imports FC.SharedWinFormBus.Enums
Imports DevExpress.XtraGrid.Columns

Public Class X10ConditionBuilder

    Private _das As DataSet = Nothing
    Private _datGroup As DataTable = Nothing
    Private _datCondition As DataTable = Nothing
    Private WithEvents _grid As GridControl = Nothing
    Private WithEvents _grdVwGroup As GridView = Nothing
    Private WithEvents _grdVwCondition As GridView = Nothing
    Public Sub New()

    End Sub

    Public Sub SetParam(ByVal grd As GridControl)
        Try
            _grid = grd

            InitGrid()
            InitSchemaDataTables()

            Dim bindingGroup As New BindingSource()
            bindingGroup.DataSource = _das
            bindingGroup.DataMember = _datGroup.TableName

            Dim bindingCondition As New BindingSource()
            bindingCondition.DataSource = bindingGroup
            bindingCondition.DataMember = _das.Relations(0).RelationName

            _grid.DataSource = bindingGroup
            _grid.LevelTree.Nodes.Add(_das.Relations(0).RelationName, _grdVwCondition)

        Catch ex As Exception
            Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)
        End Try

    End Sub
    Private Sub InitGrid()
        Dim colGrpLinker As New GridColumn()
        colGrpLinker.Caption = "Linker"
        colGrpLinker.FieldName = "LinkerID"
        colGrpLinker.Name = "GridColumn_GrpLinker"
        colGrpLinker.Visible = True
        colGrpLinker.VisibleIndex = 0
        colGrpLinker.Width = 69

        Dim colGrpName As New GridColumn()
        colGrpName.Caption = "Group"
        colGrpName.FieldName = "Name"
        colGrpName.Name = "GridColumn_GrpName"
        colGrpName.Visible = True
        colGrpName.VisibleIndex = 1
        colGrpName.Width = 135

        _grdVwGroup = New GridView()
        _grdVwGroup.Name = "grdVwGroup"
        _grdVwGroup.Columns.AddRange(New GridColumn() {colGrpLinker, colGrpName})

        Dim colLinker As New GridColumn()
        colLinker.Caption = "Linker"
        colLinker.FieldName = "LinkerID"
        colLinker.Name = "GridColumn_Linker"
        colLinker.Visible = True
        colLinker.VisibleIndex = 1
        colLinker.Width = 57

        Dim colFieldCaption As New GridColumn()
        colFieldCaption.Caption = "Field"
        colFieldCaption.FieldName = "FieldCaption"
        colFieldCaption.Name = "GridColumn_FieldCaption"
        colFieldCaption.Width = 178

        Dim colOperator As New GridColumn()
        colOperator.AppearanceCell.Options.UseTextOptions = True
        colOperator.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        colOperator.Caption = "Operator"
        colOperator.FieldName = "OperatorID"
        colOperator.Name = "GridColumn_Operator"
        colOperator.Visible = True
        colOperator.VisibleIndex = 2
        colOperator.Width = 93

        Dim colIsCompareField As New GridColumn()
        colIsCompareField.Caption = "Compare Field"
        colIsCompareField.FieldName = "IsCompareField"
        colIsCompareField.Name = "GridColumn_IsCompareField"
        colIsCompareField.Visible = True
        colIsCompareField.VisibleIndex = 3
        colIsCompareField.Width = 80

        _grdVwCondition = New GridView()
        _grdVwCondition.Name = "grdVwCondition"

    End Sub
    Private Sub InitSchemaDataTables()
        _datGroup = New DataTable("Group")
        _datGroup.Columns.Add("ID", GetType(Integer))
        _datGroup.Columns.Add("Name", GetType(String))
        _datGroup.Columns.Add("LinkerID", GetType(Integer))
        Dim colId As DataColumn = _datGroup.Columns("ID")
        colId.AutoIncrement = True
        colId.AutoIncrementSeed = -1
        colId.AutoIncrementStep = -1

        _datCondition = New DataTable("Condition")
        _datCondition.Columns.Add("ID", GetType(Integer))
        _datCondition.Columns.Add("GroupID", GetType(Integer))
        _datCondition.Columns.Add("FieldName", GetType(String))
        _datCondition.Columns.Add("FieldCaption", GetType(String))
        _datCondition.Columns.Add("LinkerID", GetType(Integer))
        _datCondition.Columns.Add("OperatorID", GetType(Integer))
        _datCondition.Columns.Add("IsCompareField", GetType(Boolean))
        _datCondition.Columns.Add("DatePeriodFunctionID", GetType(Integer))
        _datCondition.Columns.Add("Value1", GetType(String))
        _datCondition.Columns.Add("Value2", GetType(String))
        _datCondition.Columns.Add("ExactValue1", GetType(String))
        _datCondition.Columns.Add("ExactValue2", GetType(String))

        _das = New DataSet()
        _das.Tables.Add(_datGroup)
        _das.Tables.Add(_datCondition)
        _das.Relations.Add("Group_Condition", _datGroup.Columns("ID"), _datCondition.Columns.Add("GroupID"))
    End Sub

    Private Function GetNewGroupName() As String
        If IsNothing(_datGroup) Then
            Return Nothing
        End If

        Dim rowsFound() As DataRow = _datGroup.Select("", "Name desc")
        If rowsFound.Length = 0 Then
            Return "GROUP #1"
        End If
        Dim maxGroupName As String = DataHelper.DBNullOrNothingTo(Of String)(rowsFound(0)("Name"), String.Empty)
        Return String.Format("GROUP #{0}", CInt(maxGroupName.Replace("GROUP #", String.Empty)) + 1)
    End Function

    Private Sub _grdVwGroup_InitNewRow(sender As Object, e As InitNewRowEventArgs) Handles _grdVwGroup.InitNewRow
        Dim grdVw As GridView = CType(sender, GridView)
        grdVw.SetRowCellValue(e.RowHandle, "LinkerID", CInt(EnumConditionLinker.And))
        grdVw.SetRowCellValue(e.RowHandle, "Name", GetNewGroupName())
    End Sub
End Class
