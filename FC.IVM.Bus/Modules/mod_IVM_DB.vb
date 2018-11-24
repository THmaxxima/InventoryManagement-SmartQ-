Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports FC.M.BLL_Util
Imports FC.MainSQL.Modules
Imports FC.MainApp.Modules
Imports FC.M.PSL_Win.Classes_Helper

Namespace Modules
    ''' <summary>Module for management database</summary>
    ''' <remarks>ฟังก์ชั่นสำหรับ บริหารจัดการข้อมูล ในฐานข้อมูล</remarks>
    Public Module mod_IVM_DB
        ''' <exclude />
        Public DXSchedulerDataset As DataSet
        ''' <exclude />
        Public AppointmentDataAdapter As SqlDataAdapter
        ''' <exclude />
        Public ResourceDataAdapter As SqlDataAdapter
        ''' <exclude />
        Public DXSchedulerConn As SqlConnection
        ''' <exclude />
        Public IDReader As IDataReader
        ''' <exclude />
        Public AreaReaderDataAdapter As IDbDataAdapter

        'Private Const FieldID As Integer = 1

        ''' <summary>ฟังก์ชั่นแสดงข้อมูลของ Group area ในแต่ละลาน</summary>
        ''' <param name="FieldID">รหัสลาน</param>
        ''' <returns>Datatable : Group area detail data</returns>
        Public Function func_IVM_Get_Area_Info(ByVal FieldID As Integer) As DataSet
            Dim DS_Area As New DataSet
            Try

                'Dim Dt_Area As New DataTable
                SQL.FillDataSet(DS_Area, String.Format("proc_IVM_PrimaryStorage_1664 {0}", DataHelper.ToSqlValue(FieldID)))
                'SQL.FillDataTable(Dt_Area, String.Format("proc_IVM_PrimaryStorage_1664 {0}", DataHelper.ToSqlValue(FieldID)))

            Catch ex As Exception
                Dim parentId As Integer = Infolog.AddMessage(0, FC.M.PSL_Win.MessageType.ErrorMessage, "FC.IVM.Bus")
                Infolog.AddMessage(parentId, FC.M.PSL_Win.MessageType.ErrorMessage, "File := mod_IVM_DB Function := func_IVM_Get_Area_Info")
                Infolog.ShowExMessage(ex, FC.M.PSL_Win.MessageType.ErrorMessage)

                ModMainApp.Log.Log4N("func_IVM_Get_Area_Info [Catch]").DebugFormat("Err := {0} ", ex.Message)
                'Throw New Exception("Error : func_IVM_Get_Area_Info ", ex)
            End Try
            Return DS_Area
        End Function
        ''' <summary>Function use to get WP. detail data</summary>
        ''' <param name="FieldID">รหัสลาน</param>
        ''' <returns>Datatable : WP. Information</returns>
        Public Function func_IVM_Get_WP_Info(ByVal FieldID As Integer) As DataTable
            Try
                Dim Dt_WP As New DataTable
                SQL.FillDataTable(Dt_WP, String.Format("SELECT * FROM View_IVM_WastePlantList_2148 WHERE ParentStorageID = {0}", DataHelper.ToSqlValue(FieldID)))
                Return Dt_WP
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_WP_Info ", ex)
            End Try

        End Function
        ''' <summary>ฟังก์ชั่นแสดงข้อมูลของ Sub area ในแต่ละลาน</summary>
        ''' <param name="GroupID">รหัสพื้นที่ย่อย</param>
        ''' <param name="FieldID">รหัสลาน</param>
        ''' <returns>Datatable : Sub area detail data</returns>
        Public Function func_IVM_Get_ChildStorageData(ByVal GroupID As Integer, ByVal FieldID As Integer) As DataTable
            Try
                Dim Dt_Area As New DataTable
                ModMainSQL.SQL.FillDataTable(Dt_Area, String.Format("proc_IVM_ChildStorageData_1726 {0},{1}", DataHelper.ToSqlValue(GroupID), DataHelper.ToSqlValue(FieldID)))
                Return Dt_Area
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_ChildStorageData ", ex)
            End Try
        End Function

        ''' <summary>ฟังก์ชั่น สำหรับแสดงข้อมูลรายละเอียดตั๋วชั่ง</summary>
        ''' <param name="MatTypeID">ประเภทของวัตถุดิบ</param>
        ''' <param name="UserID">รหัสผู้ใช้งาน</param>
        ''' <remarks>ประเภทของวัตถุดิบ : 1 ในประเทศ 2 นำเข้า</remarks>
        Public Function func_IVM_Get_WeightTicketData(ByVal MatTypeID As Integer, ByVal UserID As String) As DataTable
            Try
                Dim Dt_Area As New DataTable
                ModMainSQL.SQL.FillDataTable(Dt_Area, String.Format("proc_IVM_GetUnloadTruck_1862 {0},{1}",
                DataHelper.ToSqlValue(MatTypeID), DataHelper.ToSqlValue(UserID)))

                Return Dt_Area
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_WeightTicketData ", ex)
            End Try
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับแสดงข้อมูล ชื่อผู้รับเหมา</summary>
        ''' <returns>Datatable : Contractor name</returns>
        Public Function func_View_IVM_Contractor_1954() As DataTable
            Try
                Dim Dt_Contractor As New DataTable
                ModMainSQL.SQL.FillDataTable(Dt_Contractor, "SELECT * FROM View_IVM_Contractor_1954 ")
                Return Dt_Contractor
            Catch ex As Exception
                Throw New Exception("Error : func_View_IVM_Contractor_1954 ", ex)
            End Try
        End Function
        ''' <summary>ฟังก์ชั่นสำหรับแสดงชื่อของวัตถุดิบ</summary>
        ''' <returns>Datatable : Material ID, Name</returns>
        Public Function func_IVM_Getmaterial() As DataTable
            Try
                Dim Dt_Material As New DataTable
                ModMainSQL.SQL.FillDataTable(Dt_Material, "SELECT ID,Name FROM MT_Material ")
                Return Dt_Material
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Getmaterial ", ex)
            End Try
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับการย้าย/ตัดจ่าย วัตถุดิบ</summary>
        ''' <param name="SourceTypeID">พื้นที่ต้นทาง</param>
        ''' <param name="DestTypeID">พื้นที่ปลายทาง</param>
        ''' <param name="MaterialID">รหัสวัตถุดิบ</param>
        ''' <param name="Amount">จำนวน</param>
        ''' <param name="UserID">รหัสผู้ใช้งาน</param>
        Public Function func_IVM_MoveStockTemp(ByVal SourceTypeID As Integer, ByVal DestTypeID As Integer,
                                          ByVal MaterialID As Integer, ByVal Amount As Double,
                                          ByVal UserID As Integer) As String
            Dim Res As String = ""
            Try
                Res = ModMainSQL.SQL.ExecuteNonQuery(String.Format("proc_IVM_MoveStockTemp_1847 {0},{1},{2},{3},{4}",
            DataHelper.ToSqlValue(SourceTypeID), DataHelper.ToSqlValue(DestTypeID), DataHelper.ToSqlValue(MaterialID),
                                DataHelper.ToSqlValue(Amount), DataHelper.ToSqlValue(UserID))).ToString

            Catch ex As Exception
                Throw New Exception("Error : func_IVM_MoveStockTemp ", ex)
            End Try

            Return Res
        End Function
        'proc_IVM_UnloadTruckToTemp_1859
        ''' <summary>ฟังก์ชั่น สำหรับบันทึกข้อมูลการ Unload วัตถุดิบ</summary>
        ''' <param name="Ticket">ตั๋วชั่ง</param>
        ''' <param name="Amount">จำนวน</param>
        ''' <param name="MatID">รหัสวัตถุดิบ</param>
        ''' <param name="ContractName">ชื่อผู้รับเหมา</param>
        ''' <param name="UserID">รหัสผู้ใช้งาน</param>
        ''' <param name="viewUnloadData">รายละเอียดวัตถุดิบ</param>
        ''' <param name="MatCategory">กลุ่มวัตถุดิบ</param>
        ''' <param name="BalingSeal">Balingseal status</param>
        ''' <param name="TransferPoint">จุดขนส่ง</param>
        Public Function func_IVM_UnloadTruckToTemp_1859(ByVal Ticket As String, ByVal Amount As Decimal,
                ByVal MatID As Integer, ByVal ContractName As String, ByVal UserID As Integer,
                ByVal viewUnloadData As GridView, ByVal MatCategory As String, ByVal BalingSeal As String, ByVal TransferPoint As String) As String

            Dim Res As String = ""
            'Declare @Mtabletype contactType 
            'Dim tblMainTicket As Table = TblTicket
            'Dim dt As New DataTable
            Dim TblTicket As DataTable
            Dim TblUnloadFromTruck As DataTable
            Try
                '+++++++++++++++ create table +++++++++++++++
                Dim TblTicketRow, TblUnloadRow As DataRow
                'create a table named tmptbl
                TblTicket = New DataTable("tmpTblTicket")
                TblUnloadFromTruck = New DataTable("tmpTblUnloadFromTruck")
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                Dim dataColTicket As DataColumn = New DataColumn("Ticket")
                dataColTicket.DataType = System.Type.GetType("System.String")
                TblTicket.Columns.Add(dataColTicket)

                Dim dataColMatID As DataColumn = New DataColumn("MaterialID")
                dataColMatID.DataType = System.Type.GetType("System.Int32")
                TblTicket.Columns.Add(dataColMatID)

                Dim dataColAmount As DataColumn = New DataColumn("BaleAmount")
                dataColAmount.DataType = GetType(Decimal)
                TblTicket.Columns.Add(dataColAmount)

                Dim dataColContractorName As DataColumn = New DataColumn("ContractorName")
                dataColContractorName.DataType = System.Type.GetType("System.String")
                TblTicket.Columns.Add(dataColContractorName)

                Dim dataMaterialCategory As DataColumn = New DataColumn("MaterialCategory")
                dataMaterialCategory.DataType = System.Type.GetType("System.String")
                TblTicket.Columns.Add(dataMaterialCategory)

                Dim dataBalingSealValidity As DataColumn = New DataColumn("BalingSealValidity")
                dataBalingSealValidity.DataType = System.Type.GetType("System.String")
                TblTicket.Columns.Add(dataBalingSealValidity)

                Dim dataTransferPoint As DataColumn = New DataColumn("TransferPoint")
                dataTransferPoint.DataType = System.Type.GetType("System.String")
                TblTicket.Columns.Add(dataTransferPoint)

                'declaring a new row
                TblTicketRow = TblTicket.NewRow()
                'filling the row with values. Item property is used to set the field value.
                'filling the row with values. adding a contactID
                TblTicketRow.Item("Ticket") = Ticket
                TblTicketRow.Item("MaterialID") = MatID
                TblTicketRow.Item("BaleAmount") = Amount
                TblTicketRow.Item("ContractorName") = ContractName
                TblTicketRow.Item("MaterialCategory") = MatCategory
                TblTicketRow.Item("BalingSealValidity") = BalingSeal
                TblTicketRow.Item("TransferPoint") = TransferPoint
                TblTicket.Rows.Add(TblTicketRow)
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                '[DestinationStorageID]
                Dim dataColDest As DataColumn = New DataColumn("DestinationStorageID")
                dataColDest.DataType = System.Type.GetType("System.Int32")
                TblUnloadFromTruck.Columns.Add(dataColDest)

                'Amount
                Dim dataColMoveAmount As DataColumn = New DataColumn("Amount")
                dataColMoveAmount.DataType = GetType(Decimal)
                TblUnloadFromTruck.Columns.Add(dataColMoveAmount)

                'PackageName
                Dim dataColPackageName As DataColumn = New DataColumn("PackageName")
                dataColPackageName.DataType = System.Type.GetType("System.String")
                TblUnloadFromTruck.Columns.Add(dataColPackageName)

                For iGridRow As Integer = 0 To viewUnloadData.RowCount - 1
                    TblUnloadRow = TblUnloadFromTruck.NewRow()
                    TblUnloadRow.Item("DestinationStorageID") = CType(viewUnloadData.GetRowCellValue(iGridRow, "GroupID"), Integer)
                    TblUnloadRow.Item("Amount") = CType(viewUnloadData.GetRowCellValue(iGridRow, "Quantity"), Decimal)
                    TblUnloadRow.Item("PackageName") = CType(viewUnloadData.GetRowCellValue(iGridRow, "PackageSize"), String)
                    TblUnloadFromTruck.Rows.Add(TblUnloadRow)
                Next
                '+++++++++++++++++++++++++++++++++++++ Insert to userdefind table +++++++++++++++++++++++++++++++
                Using cnn As SqlConnection = ModMainSQL.SQL.CreateConnection
                    'Dim Dt_Area As New DataTable
                    Try
                        cnn.Open()
                        Dim cmd As SqlCommand = cnn.CreateCommand
                        With cmd
                            .CommandText = "proc_IVM_insertTblTicket"
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("@TicketTruck", TblTicket))
                            .Parameters(0).SqlDbType = SqlDbType.Structured
                            .Parameters.Add(New SqlParameter("@UnloadFromTruckData", TblUnloadFromTruck))
                            .Parameters(1).SqlDbType = SqlDbType.Structured
                        End With

                        cmd.ExecuteNonQuery()
                        '++++++++++++++++++++++++++++++++ Insert to tabal +++++++++++++++++++++++++++++++++++++++++

                        With cmd
                            .Parameters.Clear()
                            .CommandText = "proc_IVM_UnloadTruckToTemp_1859"
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("@TicketTruck", TblTicket))
                            .Parameters(0).SqlDbType = SqlDbType.Structured
                            .Parameters.Add(New SqlParameter("@DestStorage", TblUnloadFromTruck))
                            .Parameters(1).SqlDbType = SqlDbType.Structured
                            .Parameters.AddWithValue("@UserID", UserID)
                        End With
                        Res = cmd.ExecuteNonQuery().ToString
                        'cnn.Close()
                    Catch ex As Exception
                        Throw New Exception("Error : proc_IVM_UnloadTruckToTemp_1859 ", ex)
                    End Try
                End Using
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Catch ex As SqlException
                Throw New Exception("Error : func_IVM_UnloadTruckToTemp_1859 ", ex)
            End Try

            Return Res
        End Function
        ''' <summary>Function use to set disable/enable area</summary>
        ''' <param name="StorageID">รหัสพื้นที่</param>
        ''' <param name="Status">สถานะ</param>
        ''' <param name="UserID">รหัสผู้ใช้งาน</param>
        ''' <remarks>Status : 1 Disable ,2 Enable</remarks>
        Public Function func_IVM_UpdateStorageFullFlag(ByVal StorageID As Integer, ByVal Status As Integer,
                                         ByVal UserID As Integer) As Integer
            Dim Res As Integer
            Try
                Res = ModMainSQL.SQL.ExecuteNonQuery(String.Format("proc_IVM_UpdateStorageFullFlag_1776 {0},{1},{2}",
            DataHelper.ToSqlValue(StorageID), DataHelper.ToSqlValue(Status), DataHelper.ToSqlValue(UserID)))

            Catch ex As Exception
                Throw New Exception("Error : func_IVM_UpdateStorageFullFlag ", ex)
            End Try

            Return Res
        End Function

        ''' <summary>ฟังก์ชั่น สำหรับแสดง วัน/เวลา ที่มีการอัพเดทข้อมูลล่าสุดในฐานข้อมูล</summary>
        ''' <returns>Datetime : Last update time</returns>
        Public Function func_IVM_Get_LasUpdateData() As DateTime
            'Dim Dt_Area As New DataTable
            Dim dateParam As DateTime

            Dim sql As String = "SELECT TOP(1) * FROM view_IVM_StorageLastUpdate_1928 "
            '++++++++++++++++ For debug only +++++++++++++++++++++++++++
            'Dim sql As String = "SELECT TOP(1) * FROM view_IVM_StorageLastUpdate_1928 WHERE CONVERT(Varchar, LatestUpdateDate, 120) < Convert(Varchar, '" & dateParam & "', 120) "
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Try
                Dim Dt_Last_UPD As New DataTable
                ModMainSQL.SQL.FillDataTable(Dt_Last_UPD, sql)
                dateParam = CDate(Dt_Last_UPD.Rows(0).Item("LatestUpdateDate"))

                'If (dateParam > lastUpdateTime) Then
                '    Return dateParam
                'End If
                Return dateParam
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_LasUpdateData ", ex)
            End Try
        End Function
        ''' <summary>Function use to get material properties</summary>
        ''' <returns>Datatable : Material properties</returns>
        Public Function func_IVM_Get_Material_Properties() As DataTable

            Try
                Dim Dt_Property As New DataTable
                Dim sql As String = "SELECT  * FROM View_IVM_TruckProperties_2456 "

                ModMainSQL.SQL.FillDataTable(Dt_Property, sql)
                Return Dt_Property
            Catch ex As Exception
                Throw New Exception("Error : View_IVM_TruckProperties_2456 ", ex)
            End Try

        End Function
        ''' <summary>Function use to get tentative transaction data</summary>
        ''' <returns>Datatable : Tentative data</returns>
        Public Function func_IVM_Get_Tentative_Data() As DataSet
            Try
                Dim Ds_Tentative_Data As New DataSet
                ModMainSQL.SQL.FillDataSet(Ds_Tentative_Data, String.Format("proc_IVM_TentativeTransaction_2398 {0}", DataHelper.ToSqlValue(UserId)))
                Return Ds_Tentative_Data
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_Tentative_Data ", ex)
            End Try
        End Function
        ''' <summary>ฟังก์ชั่นแสดงชื่อพื้นที่กองเก็บ ใน Gridcontrol </summary>
        ''' <returns>Datatable : Area name</returns>
        Public Function func_IVM_Get_LookUp_Area_Data() As DataTable
            Try
                Dim Dt_Area_Data As New DataTable

                'ไม่ต้องเอา ไอดีลานมาเป็นเงื่อนไขในการคิวรี่ จะทำให้ไม่สามารถแสดงชื่อกองเก็บได้ กรณีเป็นการย้ายข้ามลาน
                ModMainSQL.SQL.FillDataTable(Dt_Area_Data, String.Format("select ID, Name from MT_StorageArea"))

                'ModMainSQL.SQL.FillDataTable(Dt_Area_Data, String.Format(
                '    "select ID, Name from MT_StorageArea where ParentStorageID in ({0},{1}) OR ParentStorageID IS NULL", DataHelper.ToSqlValue(ParentStorageID), DataHelper.ToSqlValue(0)))

                Return Dt_Area_Data
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Get_LookUp_Area_Data ", ex)
            End Try
        End Function

        '''' <summary>ฟังก์ชั่นแสดงชื่อพื้นที่กองเก็บ</summary>
        '''' <param name="GroupID">รหัสของพื้นที่กองเก็บในแต่ละลาน</param>
        '''' <returns>Datatable : Area name</returns>
        'Public Function func_IVM_Get_Area_Data_By_GroupID(ByVal GroupID As Integer) As DataTable
        '    Try
        '        Dim Dt_Area_Data As New DataTable
        '        ModMainSQL.SQL.FillDataTable(Dt_Area_Data, String.Format("select ID, Name from MT_StorageArea where GroupID = {0} OR GroupID IS NULL", DataHelper.ToSqlValue(GroupID)))
        '        Return Dt_Area_Data
        '    Catch ex As Exception
        '        Throw New Exception("Error : func_IVM_Get_Area_Data_By_GroupID ", ex)
        '    End Try
        'End Function

        ''' <summary>Function use to rollback tentative transaction data</summary>
        ''' <param name="Ref_ID">Data ID</param>
        ''' <param name="Transac_Type">Transaction type</param>
        ''' <param name="UserID">User ID</param>
        ''' <remarks>Transaction type : 1 Unload ,2 Movement</remarks>
        Public Function func_IVM_Rollback_Tentative_Data(ByVal Ref_ID As Integer, ByVal Transac_Type As Integer, ByVal UserID As Integer) As Integer
            Try
                'proc_IVM_RollBack_1894
                Dim Res As Integer
                Res = ModMainSQL.SQL.ExecuteNonQuery(String.Format("proc_IVM_RollBack_1894 {0},{1},{2}",
            DataHelper.ToSqlValue(Ref_ID), DataHelper.ToSqlValue(Transac_Type), DataHelper.ToSqlValue(UserID)))
                Return Res
            Catch ex As Exception
                Throw New Exception("Error : func_IVM_Rollback_Tentative_Data ", ex)
            End Try
        End Function
        ''' <summary>ฟังก์ชั่น สำหรับ แสดง วัน/เวลา ของเครื่อง Server</summary>
        Public ReadOnly Property Get_Curr_DB_Time() As DateTime
            Get
                Return ModMainSQL.SQL.GetCurrentDateTime()
            End Get
        End Property

        ''' <exclude />
        Public Function func_IVM_Get_Site_Name() As DataTable

            Try
                Dim Dt_Property As New DataTable
                Dim sql As String = "SELECT  * FROM View_IVM_TruckProperties_2456 "

                ModMainSQL.SQL.FillDataTable(Dt_Property, sql)
                Return Dt_Property
            Catch ex As Exception
                Throw New Exception("Error : View_IVM_TruckProperties_2456 ", ex)
            End Try

        End Function
    End Module

End Namespace
