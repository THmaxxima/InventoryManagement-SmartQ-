Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraMap

Namespace Forms
    ''' <summary>คลาส สำหรับ สร้าง Default object MapRectangle</summary>
    Public Class MapItemFactory
        Inherits DefaultMapItemFactory

        ''' <param name="item">Mapitem</param>
        Protected Overrides Sub InitializeItem(ByVal item As MapItem, ByVal obj As Object)
            MyBase.InitializeItem(item, obj)
            Dim rect As MapRectangle = TryCast(item, MapRectangle)
            If rect IsNot Nothing Then
                rect.Width = 309
                rect.Height = 350

            End If
        End Sub
    End Class
End Namespace
