Imports System.Data.SqlClient
Imports FC.M.BLL_Util
Imports FC.MainSQL.Modules

Namespace Modules
    Public Module mod_IVM_center
        Public Sub TestInsert()

            Dim strSql As String = String.Format("insert into tbl_Test(Name) values({0})", DataHelper.ToSqlValue(DateTime.Now))
            ModMainSQL.SQL.ExecuteNonQuery(strSql)

            strSql = "select NULL"
            'Dim countRecoud As Integer = CInt(ModMainSQL.SQL.ExecuteScalar(strSql))
            Dim countRecoud As Integer = DataHelper.DBNullOrNothingTo(Of Integer)(ModMainSQL.SQL.ExecuteScalar(strSql), 0)

            Dim currentDateTime As DateTime = ModMainSQL.SQL.GetCurrentDateTime()

            strSql = "select * from MT_Material"
            Dim dat As New DataTable()
            ModMainSQL.SQL.FillDataTable(dat, strSql)

            strSql = "select * from MT_Material;select * from MT_Supplier"
            Dim das As New DataSet()
            ModMainSQL.SQL.FillDataSet(das, strSql)

            Dim tb1 As DataTable = das.Tables(0) ' das.Tables("Table")
            Dim tb2 As DataTable = das.Tables(1) ' das.Tables("Table1")


        End Sub
    End Module
End Namespace

