Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Drawing
Namespace Forms
    ''' <summary>Class default area mapitem property.</summary>
    Public Class LayoutAreaItem
        Public privateLabel As String
        ''' <summary>Area label use to display on main layout</summary>
        Public Property Label() As String
            Get
                Return privateLabel
            End Get
            Set(ByVal value As String)
                privateLabel = value
            End Set
        End Property
        Private privateLat As Double
        ''' <summary>Position X</summary>
        Public Property Lat() As Double
            Get
                Return privateLat
            End Get
            Set(ByVal value As Double)
                privateLat = value
            End Set
        End Property
        Private privateLon As Double
        ''' <summary>Position Y</summary>
        Public Property Lon() As Double
            Get
                Return privateLon
            End Get
            Set(ByVal value As Double)
                privateLon = value
            End Set
        End Property
        Private privateTag As Color
        ''' <summary>Default area color</summary>
        Public Property Tag() As Color
            Get
                Return privateTag
            End Get
            Set(ByVal value As Color)
                privateTag = value
            End Set
        End Property
        Private privatecapacity As Double
        ''' <summary>Capacity for each area</summary>
        Public Property Capacity() As Double
            Get
                Return privatecapacity
            End Get
            Set(ByVal value As Double)
                privatecapacity = value
            End Set
        End Property
        Private privateid As Integer
        ''' <summary>Area ID</summary>
        Public Property ID() As Integer
            Get
                Return privateid
            End Get
            Set(ByVal value As Integer)
                privateid = value
            End Set
        End Property
        Private privateName As String
        ''' <summary>Area name</summary>
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateFont As Font
        ''' <summary>Default font use to display area information</summary>
        Public Property Font() As Font
            Get
                Return privateFont
            End Get
            Set(ByVal value As Font)
                privateFont = value
            End Set
        End Property
    End Class
    ''' <summary>Class default for draw object area mapitem.</summary>
    Public Class CreateAreaItem
        Inherits List(Of LayoutAreaItem)
        Private Shared ReadOnly instance_Renamed As CreateAreaItem
        Private Shared ReadOnly instance_Area_Item2 As CreateAreaItem
        Private Shared ReadOnly instance_Area_Item3 As CreateAreaItem
        Private Shared ReadOnly custom_icon As CreateAreaItem

        Shared Sub New()
            instance_Renamed = New CreateAreaItem()
            instance_Area_Item2 = New CreateAreaItem()
            instance_Area_Item3 = New CreateAreaItem()
            '+++++++++++++++++++++++++++++++++++++++++++++++

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -13, .Label = "Share", .Tag = Color.Blue, .Capacity = 0, .Name = "Share"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -6, .Label = "Other", .Tag = Color.Blue, .Capacity = 0, .Name = "Other"})
            '+++++++++++++++++++++++++++++++++++++++++++++++ Share and Other area +++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -28, .Label = "C23", .Tag = Color.Blue, .Capacity = 0, .Name = "C23"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -25, .Label = "C24", .Tag = Color.Blue, .Capacity = 0, .Name = "C24"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -22, .Label = "C25", .Tag = Color.Blue, .Capacity = 0, .Name = "C25"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -19, .Label = "C26", .Tag = Color.Blue, .Capacity = 0, .Name = "C26"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -16, .Label = "C27", .Tag = Color.Blue, .Capacity = 0, .Name = "C27"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -13, .Label = "C28", .Tag = Color.Blue, .Capacity = 0, .Name = "C28"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -10, .Label = "C29", .Tag = Color.Blue, .Capacity = 0, .Name = "C29"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -6, .Label = "C30", .Tag = Color.Blue, .Capacity = 0, .Name = "C30"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -3, .Label = "C31", .Tag = Color.Blue, .Capacity = 0, .Name = "C31"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 0, .Label = "C32", .Tag = Color.Blue, .Capacity = 0, .Name = "C32"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 3, .Label = "D8", .Tag = Color.Blue, .Capacity = 0, .Name = "D8"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 6, .Label = "D9", .Tag = Color.Blue, .Capacity = 0, .Name = "D9"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 9, .Label = "D10", .Tag = Color.Blue, .Capacity = 0, .Name = "D10"})
            '++++++++++++++++++++++++++++++++++++++++++C,D 1 row++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -28, .Label = "C12", .Tag = Color.Blue, .Capacity = 0, .Name = "C12"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -25, .Label = "C13", .Tag = Color.Blue, .Capacity = 0, .Name = "C13"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -22, .Label = "C14", .Tag = Color.Blue, .Capacity = 0, .Name = "C14"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -19, .Label = "C15", .Tag = Color.Blue, .Capacity = 0, .Name = "C15"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -16, .Label = "C16", .Tag = Color.Blue, .Capacity = 0, .Name = "C16"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -13, .Label = "C17", .Tag = Color.Blue, .Capacity = 0, .Name = "C17"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -10, .Label = "C18", .Tag = Color.Blue, .Capacity = 0, .Name = "C18"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -7, .Label = "C19", .Tag = Color.Blue, .Capacity = 0, .Name = "C19"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -4, .Label = "C20", .Tag = Color.Blue, .Capacity = 0, .Name = "C20"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -1, .Label = "C21", .Tag = Color.Blue, .Capacity = 0, .Name = "C21"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 2, .Label = "C22", .Tag = Color.Blue, .Capacity = 0, .Name = "C22"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 5, .Label = "D5", .Tag = Color.Blue, .Capacity = 0, .Name = "D5"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 8, .Label = "D6", .Tag = Color.Blue, .Capacity = 0, .Name = "D6"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 11, .Label = "D7", .Tag = Color.Blue, .Capacity = 0, .Name = "D7"})
            '++++++++++++++++++++++++++++++++++++++++++C,D 2 row++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -28, .Label = "C1", .Tag = Color.Blue, .Capacity = 0, .Name = "C1"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -25, .Label = "C2", .Tag = Color.Blue, .Capacity = 0, .Name = "C2"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -22, .Label = "C3", .Tag = Color.Blue, .Capacity = 0, .Name = "C3"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -19, .Label = "C4", .Tag = Color.Blue, .Capacity = 0, .Name = "C4"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -16, .Label = "C5", .Tag = Color.Blue, .Capacity = 0, .Name = "C5"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -13, .Label = "C6", .Tag = Color.Blue, .Capacity = 0, .Name = "C6"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -10, .Label = "C7", .Tag = Color.Blue, .Capacity = 0, .Name = "C7"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -4, .Label = "C8", .Tag = Color.Blue, .Capacity = 0, .Name = "C8"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -1, .Label = "C9", .Tag = Color.Blue, .Capacity = 0, .Name = "C9"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 2, .Label = "C10", .Tag = Color.Blue, .Capacity = 0, .Name = "C10"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 5, .Label = "C11", .Tag = Color.Blue, .Capacity = 0, .Name = "C11"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 8, .Label = "D1", .Tag = Color.Blue, .Capacity = 0, .Name = "D1"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 11, .Label = "D2", .Tag = Color.Blue, .Capacity = 0, .Name = "D2"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 14, .Label = "D3", .Tag = Color.Blue, .Capacity = 0, .Name = "D3"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 17, .Label = "D4", .Tag = Color.Blue, .Capacity = 0, .Name = "D4"})
            '++++++++++++++++++++++++++++++++++++++++++C,D 3 row++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -16, .Label = "A39", .Tag = Color.Blue, .Capacity = 0, .Name = "A39"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -13, .Label = "A38", .Tag = Color.Blue, .Capacity = 0, .Name = "A38"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -10, .Label = "A37", .Tag = Color.Blue, .Capacity = 0, .Name = "A37"})
            '++++++++++++++++++++++++++++++++++++++++++C,D 3 row++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -40, .Label = "A36", .Tag = Color.Blue, .Capacity = 0, .Name = "A36"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -37, .Label = "A35", .Tag = Color.Blue, .Capacity = 0, .Name = "A35"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -34, .Label = "A34", .Tag = Color.Blue, .Capacity = 0, .Name = "A34"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -31, .Label = "A33", .Tag = Color.Blue, .Capacity = 0, .Name = "A33"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -28, .Label = "A32", .Tag = Color.Blue, .Capacity = 0, .Name = "A32"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -25, .Label = "A31", .Tag = Color.Blue, .Capacity = 0, .Name = "A31"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -21, .Label = "A30", .Tag = Color.Blue, .Capacity = 0, .Name = "A30"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -18, .Label = "A29", .Tag = Color.Blue, .Capacity = 0, .Name = "A29"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -15, .Label = "A28", .Tag = Color.Blue, .Capacity = 0, .Name = "A28"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -12, .Label = "A27", .Tag = Color.Blue, .Capacity = 0, .Name = "A27"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -9, .Label = "A26", .Tag = Color.Blue, .Capacity = 0, .Name = "A26"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -5, .Label = "A25", .Tag = Color.Blue, .Capacity = 0, .Name = "A25"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = -2, .Label = "A24", .Tag = Color.Blue, .Capacity = 0, .Name = "A24"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = 1, .Label = "A23", .Tag = Color.Blue, .Capacity = 0, .Name = "A23"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = 4, .Label = "A22", .Tag = Color.Blue, .Capacity = 0, .Name = "A22"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = 7, .Label = "A21", .Tag = Color.Blue, .Capacity = 0, .Name = "A21"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = 10, .Label = "A20", .Tag = Color.Blue, .Capacity = 0, .Name = "A20"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5.5, .Lon = 13, .Label = "A19", .Tag = Color.Blue, .Capacity = 0, .Name = "A19"})
            '++++++++++++++++++++++++++++++++++++++++++C,D 3 row++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -40, .Label = "A18", .Tag = Color.Blue, .Capacity = 0, .Name = "A18"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -37, .Label = "A17", .Tag = Color.Blue, .Capacity = 0, .Name = "A17"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -34, .Label = "A16", .Tag = Color.Blue, .Capacity = 0, .Name = "A16"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -31, .Label = "A15", .Tag = Color.Blue, .Capacity = 0, .Name = "A15"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -28, .Label = "A14", .Tag = Color.Blue, .Capacity = 0, .Name = "A14"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -25, .Label = "A13", .Tag = Color.Blue, .Capacity = 0, .Name = "A13"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -21, .Label = "A12", .Tag = Color.Blue, .Capacity = 0, .Name = "A12"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -18, .Label = "A11", .Tag = Color.Blue, .Capacity = 0, .Name = "A11"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -15, .Label = "A10", .Tag = Color.Blue, .Capacity = 0, .Name = "A10"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -12, .Label = "A9", .Tag = Color.Blue, .Capacity = 0, .Name = "A9"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -9, .Label = "A8", .Tag = Color.Blue, .Capacity = 0, .Name = "A8"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -5, .Label = "A7", .Tag = Color.Blue, .Capacity = 0, .Name = "A7"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = -2, .Label = "A6", .Tag = Color.Blue, .Capacity = 0, .Name = "A6"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = 1, .Label = "A5", .Tag = Color.Blue, .Capacity = 0, .Name = "A5"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = 4, .Label = "A4", .Tag = Color.Blue, .Capacity = 0, .Name = "A4"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = 7, .Label = "A3", .Tag = Color.Blue, .Capacity = 0, .Name = "A3"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = 10, .Label = "A2", .Tag = Color.Blue, .Capacity = 0, .Name = "A2"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 0.5, .Lon = 13, .Label = "A1", .Tag = Color.Blue, .Capacity = 0, .Name = "A1"})
            '++++++++++++++++++++++++++++++++++++++++++B++++++++++++++++++++++++++++++++++++++++++++++
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 22, .Label = "B10", .Tag = Color.Blue, .Capacity = 0, .Name = "B10"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 22, .Label = "B11", .Tag = Color.Blue, .Capacity = 0, .Name = "B11"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 25, .Label = "B1", .Tag = Color.Blue, .Capacity = 0, .Name = "B1"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 28, .Label = "B2", .Tag = Color.Blue, .Capacity = 0, .Name = "B2"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 31, .Label = "B3", .Tag = Color.Blue, .Capacity = 0, .Name = "B3"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 34, .Label = "B4", .Tag = Color.Blue, .Capacity = 0, .Name = "B4"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 37, .Label = "B5", .Tag = Color.Blue, .Capacity = 0, .Name = "B5"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 25, .Label = "B6", .Tag = Color.Blue, .Capacity = 0, .Name = "B6"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 28, .Label = "B7", .Tag = Color.Blue, .Capacity = 0, .Name = "B7"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 31, .Label = "B8", .Tag = Color.Blue, .Capacity = 0, .Name = "B8"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 34, .Label = "B9", .Tag = Color.Blue, .Capacity = 0, .Name = "B9"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 25, .Label = "B12", .Tag = Color.Blue, .Capacity = 0, .Name = "B12"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 28, .Label = "B13", .Tag = Color.Blue, .Capacity = 0, .Name = "B13"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 31, .Label = "B14", .Tag = Color.Blue, .Capacity = 0, .Name = "B14"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 34, .Label = "B15", .Tag = Color.Blue, .Capacity = 0, .Name = "B15"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 37, .Label = "B16", .Tag = Color.Blue, .Capacity = 0, .Name = "B16"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 25, .Label = "B17", .Tag = Color.Blue, .Capacity = 0, .Name = "B17"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 28, .Label = "B18", .Tag = Color.Blue, .Capacity = 0, .Name = "B18"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 31, .Label = "B19", .Tag = Color.Blue, .Capacity = 0, .Name = "B19"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 34, .Label = "B20", .Tag = Color.Blue, .Capacity = 0, .Name = "B20"})

            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = 25, .Label = "B21", .Tag = Color.Blue, .Capacity = 0, .Name = "B21"})
            instance_Renamed.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = 28, .Label = "B22", .Tag = Color.Blue, .Capacity = 0, .Name = "B22"})

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++ End ลาน1 ++++++++++++++++++++++++++++++++++++++++++++

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++ ลาน2 ++++++++++++++++++++++++++++++++++++++++++++
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -10, .Label = "Share", .Tag = Color.Blue, .Capacity = 0, .Name = "Share"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -3, .Label = "Other", .Tag = Color.Blue, .Capacity = 0, .Name = "Other"})
            '+++++++++++++++++++++++++++++++++++++++++++++++ Share and Other area +++++++++++++++++++++++++++++
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -16, .Label = "R11", .Tag = Color.Blue, .Capacity = 0, .Name = "R11"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -13, .Label = "R10", .Tag = Color.Blue, .Capacity = 0, .Name = "R10"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -10, .Label = "R9", .Tag = Color.Blue, .Capacity = 0, .Name = "R9"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -16, .Label = "R8", .Tag = Color.Blue, .Capacity = 0, .Name = "R8"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -13, .Label = "R7", .Tag = Color.Blue, .Capacity = 0, .Name = "R7"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -10, .Label = "R6", .Tag = Color.Blue, .Capacity = 0, .Name = "R6"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -7, .Label = "R5", .Tag = Color.Blue, .Capacity = 0, .Name = "R5"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -16, .Label = "R4", .Tag = Color.Blue, .Capacity = 0, .Name = "R4"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -13, .Label = "R3", .Tag = Color.Blue, .Capacity = 0, .Name = "R3"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -10, .Label = "R2", .Tag = Color.Blue, .Capacity = 0, .Name = "R2"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -7, .Label = "R1", .Tag = Color.Blue, .Capacity = 0, .Name = "R1"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -28, .Label = "R12", .Tag = Color.Blue, .Capacity = 0, .Name = "R12"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -25, .Label = "R13", .Tag = Color.Blue, .Capacity = 0, .Name = "R13"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -22, .Label = "R14", .Tag = Color.Blue, .Capacity = 0, .Name = "R14"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -19, .Label = "R15", .Tag = Color.Blue, .Capacity = 0, .Name = "R15"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -16, .Label = "R16", .Tag = Color.Blue, .Capacity = 0, .Name = "R16"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -13, .Label = "R17", .Tag = Color.Blue, .Capacity = 0, .Name = "R17"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -10, .Label = "R18", .Tag = Color.Blue, .Capacity = 0, .Name = "R18"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -7, .Label = "R19", .Tag = Color.Blue, .Capacity = 0, .Name = "R19"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -4, .Label = "R20", .Tag = Color.Blue, .Capacity = 0, .Name = "R20"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -3, .Label = "QC1", .Tag = Color.Blue, .Capacity = 0, .Name = "QC1"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -3, .Label = "QC2", .Tag = Color.Blue, .Capacity = 0, .Name = "QC2"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -3, .Label = "QC3", .Tag = Color.Blue, .Capacity = 0, .Name = "QC3"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 0, .Label = "QC4", .Tag = Color.Blue, .Capacity = 0, .Name = "QC4"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 0, .Label = "QC5", .Tag = Color.Blue, .Capacity = 0, .Name = "QC5"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 3, .Label = "QC6", .Tag = Color.Blue, .Capacity = 0, .Name = "QC6"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 3, .Label = "QC7", .Tag = Color.Blue, .Capacity = 0, .Name = "QC7"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -28, .Label = "G1", .Tag = Color.Blue, .Capacity = 0, .Name = "G1"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -25, .Label = "G2", .Tag = Color.Blue, .Capacity = 0, .Name = "G2"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -22, .Label = "G3", .Tag = Color.Blue, .Capacity = 0, .Name = "G3"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -19, .Label = "G4", .Tag = Color.Blue, .Capacity = 0, .Name = "G4"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -16, .Label = "G5", .Tag = Color.Blue, .Capacity = 0, .Name = "G5"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -13, .Label = "G6", .Tag = Color.Blue, .Capacity = 0, .Name = "G6"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -10, .Label = "G7", .Tag = Color.Blue, .Capacity = 0, .Name = "G7"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -7, .Label = "G8", .Tag = Color.Blue, .Capacity = 0, .Name = "G8"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -4, .Label = "G9", .Tag = Color.Blue, .Capacity = 0, .Name = "G9"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = -1, .Label = "G10", .Tag = Color.Blue, .Capacity = 0, .Name = "G10"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 2, .Label = "G11", .Tag = Color.Blue, .Capacity = 0, .Name = "G11"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -28, .Label = "G19", .Tag = Color.Blue, .Capacity = 0, .Name = "G19"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -25, .Label = "G18", .Tag = Color.Blue, .Capacity = 0, .Name = "G18"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -22, .Label = "G17", .Tag = Color.Blue, .Capacity = 0, .Name = "G17"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -19, .Label = "G16", .Tag = Color.Blue, .Capacity = 0, .Name = "G16"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -16, .Label = "G15", .Tag = Color.Blue, .Capacity = 0, .Name = "G15"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -13, .Label = "G14", .Tag = Color.Blue, .Capacity = 0, .Name = "G14"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -10, .Label = "G13", .Tag = Color.Blue, .Capacity = 0, .Name = "G13"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = -7, .Label = "G12", .Tag = Color.Blue, .Capacity = 0, .Name = "G12"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 8, .Label = "E1", .Tag = Color.Blue, .Capacity = 0, .Name = "E1"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 11, .Label = "E2", .Tag = Color.Blue, .Capacity = 0, .Name = "E2"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 14, .Label = "E3", .Tag = Color.Blue, .Capacity = 0, .Name = "E3"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 17, .Label = "E4", .Tag = Color.Blue, .Capacity = 0, .Name = "E4"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 20, .Label = "E5", .Tag = Color.Blue, .Capacity = 0, .Name = "E5"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 23, .Label = "E6", .Tag = Color.Blue, .Capacity = 0, .Name = "E6"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 26, .Label = "E7", .Tag = Color.Blue, .Capacity = 0, .Name = "E7"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 29, .Label = "E8", .Tag = Color.Blue, .Capacity = 0, .Name = "E8"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 32, .Label = "E9", .Tag = Color.Blue, .Capacity = 0, .Name = "E9"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 8, .Label = "E10", .Tag = Color.Blue, .Capacity = 0, .Name = "E10"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 11, .Label = "E11", .Tag = Color.Blue, .Capacity = 0, .Name = "E11"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 14, .Label = "E12", .Tag = Color.Blue, .Capacity = 0, .Name = "E12"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 17, .Label = "E13", .Tag = Color.Blue, .Capacity = 0, .Name = "E13"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 20, .Label = "E14", .Tag = Color.Blue, .Capacity = 0, .Name = "E14"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 23, .Label = "E15", .Tag = Color.Blue, .Capacity = 0, .Name = "E15"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 26, .Label = "E16", .Tag = Color.Blue, .Capacity = 0, .Name = "E16"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 29, .Label = "E17", .Tag = Color.Blue, .Capacity = 0, .Name = "E17"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 32, .Label = "E18", .Tag = Color.Blue, .Capacity = 0, .Name = "E18"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 8, .Label = "E19", .Tag = Color.Blue, .Capacity = 0, .Name = "E19"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 11, .Label = "E20", .Tag = Color.Blue, .Capacity = 0, .Name = "E20"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 14, .Label = "E21", .Tag = Color.Blue, .Capacity = 0, .Name = "E21"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 17, .Label = "E22", .Tag = Color.Blue, .Capacity = 0, .Name = "E22"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 20, .Label = "E23", .Tag = Color.Blue, .Capacity = 0, .Name = "E23"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 23, .Label = "E24", .Tag = Color.Blue, .Capacity = 0, .Name = "E24"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 26, .Label = "E25", .Tag = Color.Blue, .Capacity = 0, .Name = "E25"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 29, .Label = "E26", .Tag = Color.Blue, .Capacity = 0, .Name = "E26"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 8, .Label = "F1", .Tag = Color.Blue, .Capacity = 0, .Name = "F1"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 11, .Label = "F2", .Tag = Color.Blue, .Capacity = 0, .Name = "F2"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 14, .Label = "F3", .Tag = Color.Blue, .Capacity = 0, .Name = "F3"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 17, .Label = "F4", .Tag = Color.Blue, .Capacity = 0, .Name = "F4"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 20, .Label = "F5", .Tag = Color.Blue, .Capacity = 0, .Name = "F5"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 23, .Label = "F6", .Tag = Color.Blue, .Capacity = 0, .Name = "F6"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 26, .Label = "F7", .Tag = Color.Blue, .Capacity = 0, .Name = "F7"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 5, .Lon = 29, .Label = "F8", .Tag = Color.Blue, .Capacity = 0, .Name = "F8"})

            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 8, .Label = "F9", .Tag = Color.Blue, .Capacity = 0, .Name = "F9"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 11, .Label = "F10", .Tag = Color.Blue, .Capacity = 0, .Name = "F10"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 14, .Label = "F11", .Tag = Color.Blue, .Capacity = 0, .Name = "F11"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 17, .Label = "F12", .Tag = Color.Blue, .Capacity = 0, .Name = "F12"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 20, .Label = "F13", .Tag = Color.Blue, .Capacity = 0, .Name = "F13"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 23, .Label = "F14", .Tag = Color.Blue, .Capacity = 0, .Name = "F14"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 26, .Label = "F15", .Tag = Color.Blue, .Capacity = 0, .Name = "F15"})
            instance_Area_Item2.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 1, .Lon = 29, .Label = "F16", .Tag = Color.Blue, .Capacity = 0, .Name = "F16"})

            '++++++++++++++++++++++++++++++++++++++++++End ลาน2+++++++++++++++++++++++++++++++++++++++++++++++++++++

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++ ลาน3 ++++++++++++++++++++++++++++++++++++++++++++
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -13, .Label = "Share", .Tag = Color.Blue, .Capacity = 0, .Name = "Share"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 26, .Lon = -6, .Label = "Other", .Tag = Color.Blue, .Capacity = 0, .Name = "Other"})
            '+++++++++++++++++++++++++++++++++++++++++++++++ Share and Other area +++++++++++++++++++++++++++++

            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -28, .Label = "C23", .Tag = Color.Blue, .Capacity = 0, .Name = "C23"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -25, .Label = "C24", .Tag = Color.Blue, .Capacity = 0, .Name = "C24"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -22, .Label = "C25", .Tag = Color.Blue, .Capacity = 0, .Name = "C25"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -19, .Label = "C26", .Tag = Color.Blue, .Capacity = 0, .Name = "C26"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -16, .Label = "C27", .Tag = Color.Blue, .Capacity = 0, .Name = "C27"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -16, .Label = "M16", .Tag = Color.Blue, .Capacity = 0, .Name = "M16"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -13, .Label = "M10", .Tag = Color.Blue, .Capacity = 0, .Name = "M10"})

            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -6, .Label = "C30", .Tag = Color.Blue, .Capacity = 0, .Name = "C30"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -3, .Label = "C31", .Tag = Color.Blue, .Capacity = 0, .Name = "C31"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 0, .Label = "C32", .Tag = Color.Blue, .Capacity = 0, .Name = "C32"})

            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 3, .Label = "D8", .Tag = Color.Blue, .Capacity = 0, .Name = "D13"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 6, .Label = "D9", .Tag = Color.Blue, .Capacity = 0, .Name = "D14"})
            'instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 9, .Label = "D10", .Tag = Color.Blue, .Capacity = 0, .Name = "D15"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -10, .Label = "M15", .Tag = Color.Blue, .Capacity = 0, .Name = "M15"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -7, .Label = "M9", .Tag = Color.Blue, .Capacity = 0, .Name = "M9"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -16, .Label = "M14", .Tag = Color.Blue, .Capacity = 0, .Name = "M14"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -13, .Label = "M8", .Tag = Color.Blue, .Capacity = 0, .Name = "M8"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -10, .Label = "M4", .Tag = Color.Blue, .Capacity = 0, .Name = "M4"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -16, .Label = "M13", .Tag = Color.Blue, .Capacity = 0, .Name = "M13"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -13, .Label = "M7", .Tag = Color.Blue, .Capacity = 0, .Name = "M7"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -10, .Label = "M3", .Tag = Color.Blue, .Capacity = 0, .Name = "M3"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -16, .Label = "M12", .Tag = Color.Blue, .Capacity = 0, .Name = "M12"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -13, .Label = "M6", .Tag = Color.Blue, .Capacity = 0, .Name = "M6"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -10, .Label = "M2", .Tag = Color.Blue, .Capacity = 0, .Name = "M2"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = -19, .Label = "M11", .Tag = Color.Blue, .Capacity = 0, .Name = "M11"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = -16, .Label = "M5", .Tag = Color.Blue, .Capacity = 0, .Name = "M5"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = -13, .Label = "M1", .Tag = Color.Blue, .Capacity = 0, .Name = "M1"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = -10, .Label = "T4", .Tag = Color.Blue, .Capacity = 0, .Name = "T4"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -28, .Label = "U8", .Tag = Color.Blue, .Capacity = 0, .Name = "U8"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -25, .Label = "U7", .Tag = Color.Blue, .Capacity = 0, .Name = "U7"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -22, .Label = "U6", .Tag = Color.Blue, .Capacity = 0, .Name = "U6"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -19, .Label = "U5", .Tag = Color.Blue, .Capacity = 0, .Name = "U5"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -16, .Label = "U4", .Tag = Color.Blue, .Capacity = 0, .Name = "U4"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -13, .Label = "U3", .Tag = Color.Blue, .Capacity = 0, .Name = "U3"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -10, .Label = "U2", .Tag = Color.Blue, .Capacity = 0, .Name = "U2"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -7, .Label = "U1", .Tag = Color.Blue, .Capacity = 0, .Name = "U1"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -28, .Label = "U16", .Tag = Color.Blue, .Capacity = 0, .Name = "U16"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -25, .Label = "U15", .Tag = Color.Blue, .Capacity = 0, .Name = "U15"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -22, .Label = "U14", .Tag = Color.Blue, .Capacity = 0, .Name = "U14"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -19, .Label = "U13", .Tag = Color.Blue, .Capacity = 0, .Name = "U13"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -16, .Label = "U12", .Tag = Color.Blue, .Capacity = 0, .Name = "U12"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -13, .Label = "U11", .Tag = Color.Blue, .Capacity = 0, .Name = "U11"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -10, .Label = "U10", .Tag = Color.Blue, .Capacity = 0, .Name = "U10"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -7, .Label = "U9", .Tag = Color.Blue, .Capacity = 0, .Name = "U9"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -6, .Lon = -28, .Label = "I5", .Tag = Color.Blue, .Capacity = 0, .Name = "I5"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -6, .Lon = -25, .Label = "I4", .Tag = Color.Blue, .Capacity = 0, .Name = "I4"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -6, .Lon = -22, .Label = "I3", .Tag = Color.Blue, .Capacity = 0, .Name = "I3"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -6, .Lon = -19, .Label = "I2", .Tag = Color.Blue, .Capacity = 0, .Name = "I2"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -6, .Lon = -16, .Label = "I1", .Tag = Color.Blue, .Capacity = 0, .Name = "I1"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = -1, .Label = "J8", .Tag = Color.Blue, .Capacity = 0, .Name = "J8"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 2, .Label = "T3", .Tag = Color.Blue, .Capacity = 0, .Name = "T3"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 22, .Lon = 5, .Label = "T5", .Tag = Color.Blue, .Capacity = 0, .Name = "T5"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = -1, .Label = "J7", .Tag = Color.Blue, .Capacity = 0, .Name = "J7"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 2, .Label = "T2", .Tag = Color.Blue, .Capacity = 0, .Name = "T2"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 18, .Lon = 5, .Label = "T6", .Tag = Color.Blue, .Capacity = 0, .Name = "T6"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = -1, .Label = "J5", .Tag = Color.Blue, .Capacity = 0, .Name = "J5"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 14, .Lon = 2, .Label = "T1", .Tag = Color.Blue, .Capacity = 0, .Name = "T1"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = -1, .Label = "J4", .Tag = Color.Blue, .Capacity = 0, .Name = "J4"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 10, .Lon = 6, .Label = "N10", .Tag = Color.Blue, .Capacity = 0, .Name = "N10"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = -1, .Label = "J3", .Tag = Color.Blue, .Capacity = 0, .Name = "J3"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 6, .Lon = 6, .Label = "N9", .Tag = Color.Blue, .Capacity = 0, .Name = "N9"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = -1, .Label = "J2", .Tag = Color.Blue, .Capacity = 0, .Name = "J2"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 2, .Label = "J1", .Tag = Color.Blue, .Capacity = 0, .Name = "J1"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 5, .Label = "N8", .Tag = Color.Blue, .Capacity = 0, .Name = "N8"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 8, .Label = "N7", .Tag = Color.Blue, .Capacity = 0, .Name = "N7"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 11, .Label = "N6", .Tag = Color.Blue, .Capacity = 0, .Name = "N6"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = 2, .Lon = 14, .Label = "N5", .Tag = Color.Blue, .Capacity = 0, .Name = "N5"})

            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = -1, .Label = "N4", .Tag = Color.Blue, .Capacity = 0, .Name = "N4"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = 3, .Label = "N3", .Tag = Color.Blue, .Capacity = 0, .Name = "N3"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = 7, .Label = "N2", .Tag = Color.Blue, .Capacity = 0, .Name = "N2"})
            instance_Area_Item3.Add(New LayoutAreaItem() With {.ID = 0, .Lat = -2, .Lon = 10, .Label = "N1", .Tag = Color.Blue, .Capacity = 0, .Name = "N1"})
            '++++++++++++++++++++++++++++++++++++++++++End ลาน3++++++++++++++++++++++++++++++++++++++++++++++

        End Sub

        ''' <summary>Return object mapitem arear 1</summary>
        Public Shared ReadOnly Property Instance() As CreateAreaItem
            Get
                Return instance_Renamed
            End Get
        End Property
        ''' <summary>Return object mapitem arear 2</summary>
        Public Shared ReadOnly Property Instance_Area2() As CreateAreaItem
            Get
                Return instance_Area_Item2
            End Get
        End Property
        ''' <summary>Return object mapitem arear 3</summary>
        Public Shared ReadOnly Property Instance_Area3() As CreateAreaItem
            Get
                Return instance_Area_Item3
            End Get
        End Property
    End Class

End Namespace

