Imports System.Drawing
Imports DevExpress.Utils

Namespace Modules
    ''' <summary>Utility function</summary>
    Public Module mod_IVM_Utility
        Public tmpUnloadPackSize As String = String.Empty
        Public tmpUnloadQuantity As Decimal = 0
        Public isEdit As Boolean = False
        Public fontsCollection_Renamed As Dictionary(Of String, Font)
        Public lastTime As DateTime
        Public UID As Integer
        Public Const frm_Name As String = "Form := ["

        Public tmpMatProperties As String = ""
        Public tmpBalingSealProperties As String = ""
        Public tmpTransferPointProperties As String = ""
        Public tmpContractorProperties As String = ""
        Public tmpTruckConditionProperties As String = ""
        Public tmpWedgeProperties As String = ""
        Public tmpIsClose As Boolean = False
        Public tmpBeginQTY As Decimal = 0

        Public ReadOnly Property FontsCollection() As Dictionary(Of String, Font)
            Get
                Return fontsCollection_Renamed
            End Get
        End Property
        ''' <summary>Function use to set fonts</summary>
        Public Function CreateFonts() As Dictionary(Of String, Font)
            'New Font("Angsana New", 14, FontStyle.Bold)
            '
            Dim collection As New Dictionary(Of String, Font)()
            collection.Add("mainArea", New Font(AppearanceObject.DefaultFont.FontFamily, 11, FontStyle.Bold))
            collection.Add("areaInfo", New Font("Angsana New", 20, FontStyle.Bold))
            'collection.Add("areaInfo", New Font(AppearanceObject.DefaultFont.FontFamily, 12, FontStyle.Bold))
            collection.Add("desc", New Font(AppearanceObject.DefaultFont.FontFamily, 8, FontStyle.Regular))
            Return collection
        End Function

    End Module

End Namespace
