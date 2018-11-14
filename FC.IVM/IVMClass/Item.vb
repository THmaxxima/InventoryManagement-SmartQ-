Namespace IVMClass
    ''' <summary>คลาสสำหรับ Binding data souce custom popup form</summary>
    Public Class Item

        Private _dataType As Integer
        Private _text As String
        Private _check As Boolean
        Private _iD As Integer
        Private _name As String
        Private _name2 As String
        Private _packageSize As String
        Private _isEdit As Boolean

        ''' <summary>Database field name</summary>
        Public Property ColumnName As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        Public Property ColumnName2 As String
            Get
                Return _name2
            End Get
            Set(ByVal Value As String)
                _name2 = Value
            End Set
        End Property

        ''' <summary>Material package size</summary>
        Public Property PackageSize As String
            Get
                Return _packageSize
            End Get
            Set(ByVal Value As String)
                _packageSize = Value
            End Set
        End Property

        Public Property DataType As Integer
            Get
                Return _dataType
            End Get
            Set(ByVal Value As Integer)
                _dataType = Value
            End Set
        End Property

        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal Value As Integer)
                _iD = Value
            End Set
        End Property

        Public Property AutoNumber As Boolean
            Get
                Return _check
            End Get
            Set(ByVal Value As Boolean)
                _check = Value
            End Set
        End Property

        Public Property Memo As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property

        Public Property isEdit As Boolean
            Get
                Return _isEdit
            End Get
            Set(ByVal Value As Boolean)
                _isEdit = Value
            End Set
        End Property

    End Class

End Namespace
