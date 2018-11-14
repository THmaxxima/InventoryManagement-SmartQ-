Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.XtraEditors
Imports DevExpress.Utils

Namespace DevExpress.XtraMap.Demos

	Partial Public Class MapElements
		Inherits MapTutorialControl
		Private Shared imageLocker_Renamed As Object = New Object()

		Private overlayManager_Renamed As MapElementsOverlayManager
		Private dataGenerator As FlightMapDataGenerator

		Private ReadOnly Property OverlayManager() As MapElementsOverlayManager
			Get
				If overlayManager_Renamed Is Nothing Then
					overlayManager_Renamed = New MapElementsOverlayManager()
				End If
				Return overlayManager_Renamed
			End Get
		End Property
		Public Overrides ReadOnly Property MapControl() As MapControl
			Get
				Return mapControl1
			End Get
		End Property
		Protected Overrides ReadOnly Property Overlays() As MapOverlay()
			Get
				Return OverlayManager.GetOverlays()
			End Get
		End Property

		Public Shared ReadOnly Property ImageLocker() As Object
			Get
				Return imageLocker_Renamed
			End Get
		End Property

		Public Sub New()
			InitializeComponent()
			mapControl1.SetMapItemFactory(New FlightMapFactory())

			dataGenerator = New FlightMapDataGenerator(OverlayManager)
			AddHandler dataGenerator.DataChanged, AddressOf OnDataChanged

			PlanesDataAdapter.DataSource = dataGenerator.Planes
			PathsDataAdapter.DataSource = dataGenerator.AirPaths
		End Sub
		Private Sub MapElements_DataLoaded(ByVal sender As Object, ByVal e As DataLoadedEventArgs) Handles PathsLayer.DataLoaded
			DemoUtils.SetBingMapDataProviderKey(BingMapDataProvider)
			dataGenerator.SelectedPlane = dataGenerator.Planes(0)
			OnActivePlaneChanged()
		End Sub
		Private Sub OnDataChanged(ByVal sender As Object, ByVal e As EventArgs)
			For Each info As PlaneInfo In dataGenerator.Planes
				If (Not info.IsLanded) Then
					Dim item As MapCustomElement = TryCast(PlanesLayer.GetMapItemBySourceObject(info), MapCustomElement)
					If item IsNot Nothing Then
						item.Location = New GeoPoint(info.Latitude, info.Longitude)
					End If
				End If
			Next info
		End Sub
		Private Sub OnActivePlaneChanged()
			HideLayerItems(PathsLayer)
			Dim airPath As List(Of InfoBase) = dataGenerator.FindAirPath(dataGenerator.SelectedPlane)
			For Each airPathElement As InfoBase In airPath
				PathsLayer.GetMapItemBySourceObject(airPathElement).Visible = True
			Next airPathElement
		End Sub
		Private Sub HideLayerItems(ByVal layer As VectorItemsLayer)
			For Each item As MapItem In (CType(layer.Data, IMapDataAdapter)).Items
				item.Visible = False
			Next item
		End Sub

		Private Sub mapControl1_DrawMapItem(ByVal sender As Object, ByVal e As DrawMapItemEventArgs) Handles mapControl1.DrawMapItem
			Dim args As DrawMapPointerEventArgs = TryCast(e, DrawMapPointerEventArgs)
			If args IsNot Nothing Then
				args.DisposeImage = True
				Dim item As MapItem = e.Item
				Dim info As InfoBase = TryCast((CType(e.Item.Layer, VectorItemsLayer)).GetItemSourceObject(item), InfoBase)
				If info IsNot Nothing Then
					SyncLock ImageLocker
						args.Image = If(info.Icon IsNot Nothing, CType(info.Icon.Clone(), Image), Nothing)
					End SyncLock
				End If
			End If
		End Sub
		Private Sub mapControl1_SelectionChanged(ByVal sender As Object, ByVal e As MapSelectionChangedEventArgs) Handles mapControl1.SelectionChanged
			dataGenerator.SelectedPlane = If(e.Selection.Count > 0, CType(e.Selection(0), PlaneInfo), Nothing)
			OnActivePlaneChanged()
		End Sub
		Private Sub mapControl1_SelectionChanging(ByVal sender As Object, ByVal e As MapSelectionChangingEventArgs) Handles mapControl1.SelectionChanging
			Dim plainInfo As PlaneInfo = If(e.Selection.Count > 0, TryCast(e.Selection(0), PlaneInfo), Nothing)
			e.Cancel = plainInfo Is Nothing
		End Sub
		Protected Overrides Sub OnVisibleChanged(ByVal e As EventArgs)
			MyBase.OnVisibleChanged(e)
			If Me.Visible Then
				dataGenerator.Start()
			Else
				dataGenerator.Stop()
			End If
		End Sub

		Private Sub OnDispose(ByVal disposing As Boolean)
			If Me.dataGenerator IsNot Nothing Then
				Me.dataGenerator.Dispose()
				Me.dataGenerator = Nothing
			End If
			If Me.overlayManager_Renamed IsNot Nothing Then
				Me.overlayManager_Renamed.Dispose()
				Me.overlayManager_Renamed = Nothing
			End If
		End Sub
	End Class

	Public MustInherit Class InfoBase
		Protected MustOverride ReadOnly Property Type() As MapItemType
		Public ReadOnly Property ItemType() As Integer
			Get
				Return CInt(Fix(Type))
			End Get
		End Property
		Public Overridable ReadOnly Property Icon() As Image
			Get
				Return Nothing
			End Get
		End Property

		Public Property Latitude() As Double
		Public Property Longitude() As Double
	End Class

	Public Class AirportInfo
		Inherits InfoBase
		Private Shared icon_Renamed As New Bitmap(DemoUtils.GetRelativePath("\Images\Airport.png"))

		Protected Overrides ReadOnly Property Type() As MapItemType
			Get
				Return MapItemType.Custom
			End Get
		End Property
		Public Overrides ReadOnly Property Icon() As Image
			Get
				Return icon_Renamed
			End Get
		End Property

		Public Sub New(ByVal location As GeoPoint)
			Me.Latitude = location.Latitude
			Me.Longitude = location.Longitude
		End Sub
	End Class

	Public Class PlaneTrajectory
		Inherits InfoBase
		Private Class TrajectoryPart
			Private ReadOnly startPointField As GeoPoint
			Private ReadOnly endPointField As GeoPoint
			Private ReadOnly flightTimeField As Double
			Private ReadOnly courseField As Double

			Public ReadOnly Property StartPoint() As GeoPoint
				Get
					Return startPointField
				End Get
			End Property
			Public ReadOnly Property EndPoint() As GeoPoint
				Get
					Return endPointField
				End Get
			End Property
			Public ReadOnly Property FlightTime() As Double
				Get
					Return flightTimeField
				End Get
			End Property
			Public ReadOnly Property Course() As Double
				Get
					Return courseField
				End Get
			End Property

			Public Sub New(ByVal projection As ProjectionBase, ByVal startPoint As GeoPoint, ByVal endPoint As GeoPoint, ByVal speedInKmH As Double)
				Me.startPointField = startPoint
				Me.endPointField = endPoint
				Dim sizeInKm As MapSize = projection.GeoToKilometersSize(startPoint, New MapSize(Math.Abs(startPoint.Longitude - endPoint.Longitude), Math.Abs(startPoint.Latitude - endPoint.Latitude)))
				Dim partlength As Double = Math.Sqrt(sizeInKm.Width * sizeInKm.Width + sizeInKm.Height * sizeInKm.Height)
				flightTimeField = partlength / speedInKmH
				courseField = Math.Atan2((endPoint.Longitude - startPoint.Longitude), (endPoint.Latitude - startPoint.Latitude)) * 180 / Math.PI
			End Sub
			Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double) As GeoPoint
				If currentFlightTime > FlightTime Then
					Return endPointField
				End If
				Dim ratio As Double = currentFlightTime / FlightTime
				Return New GeoPoint(startPointField.Latitude + ratio * (endPointField.Latitude - startPointField.Latitude), startPointField.Longitude + ratio * (endPointField.Longitude - startPointField.Longitude))
			End Function
		End Class

		Private ReadOnly projection As New SphericalMercatorProjection()
		Private ReadOnly trajectory As New List(Of TrajectoryPart)()
		Private ReadOnly startPoint_Renamed As AirportInfo
		Private ReadOnly endPoint_Renamed As AirportInfo
		Private ReadOnly speedInKmH As Double

		Protected Overrides ReadOnly Property Type() As MapItemType
			Get
				Return MapItemType.Polyline
			End Get
		End Property
		Public ReadOnly Property StartPoint() As AirportInfo
			Get
				Return startPoint_Renamed
			End Get
		End Property
		Public ReadOnly Property EndPoint() As AirportInfo
			Get
				Return endPoint_Renamed
			End Get
		End Property
		Public ReadOnly Property FlightTime() As Double
			Get
				Dim result As Double = 0.0
				For Each part As TrajectoryPart In trajectory
					result += part.FlightTime
				Next part
				Return result
			End Get
		End Property

		Public Sub New(ByVal points As List(Of CoordPoint), ByVal speedInKmH As Double)
			Me.speedInKmH = speedInKmH
			UpdateTrajectory(points)
			startPoint_Renamed = New AirportInfo(If((trajectory.Count > 0), trajectory(0).StartPoint, New GeoPoint(0, 0)))
			endPoint_Renamed = New AirportInfo(If((trajectory.Count > 0), trajectory(trajectory.Count - 1).EndPoint, New GeoPoint(0, 0)))
		End Sub
		Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double) As GeoPoint
			Dim time As Double = 0.0
			For i As Integer = 0 To trajectory.Count - 2
				If trajectory(i).FlightTime > currentFlightTime - time Then
					Return trajectory(i).GetPointByCurrentFlightTime(currentFlightTime - time)
				End If
				time += trajectory(i).FlightTime
			Next i
			Return trajectory(trajectory.Count - 1).GetPointByCurrentFlightTime(currentFlightTime - time)
		End Function
		Public Function GetAirPath() As CoordPointCollection
			Dim result As New CoordPointCollection()
			For Each trajectoryPart As TrajectoryPart In trajectory
				result.Add(trajectoryPart.StartPoint)
			Next trajectoryPart
			If trajectory.Count > 0 Then
				result.Add(trajectory(trajectory.Count - 1).EndPoint)
			End If
			Return result
		End Function
		Public Function GetCourseByCurrentFlightTime(ByVal currentFlightTime As Double) As Double
			Dim time As Double = 0.0
			For i As Integer = 0 To trajectory.Count - 2
				If trajectory(i).FlightTime > currentFlightTime - time Then
					Return trajectory(i).Course
				End If
				time += trajectory(i).FlightTime
			Next i
			Return trajectory(trajectory.Count - 1).Course
		End Function
		Public Sub UpdateTrajectory(ByVal points As List(Of CoordPoint))
			trajectory.Clear()
			For i As Integer = 0 To points.Count - 2
				trajectory.Add(New TrajectoryPart(projection, CType(points(i), GeoPoint), CType(points(i + 1), GeoPoint), speedInKmH))
			Next i
		End Sub
	End Class

	Public Class PlaneInfo
		Inherits InfoBase
		Private Shared Function ConvertPlaneNameToFilePath(ByVal PlaneName As String) As String
			Dim result As String = PlaneName.Replace(" ", "")
			result = "\Images\Planes\" & result.Replace("-", "") & ".png"
			Return DemoUtils.GetRelativePath(result)
		End Function

		Private isLandedField As Boolean = False
		Private currentFlightTime_Renamed As Double
		Private course_Renamed As Double
		Private icon_Renamed As Image
		Private ReadOnly sourceIcon As Image
		Private ReadOnly planeIDField As String
		Private ReadOnly nameField As String
		Private ReadOnly endPointNameField As String
		Private ReadOnly startPointNameField As String
		Private ReadOnly speedInKmHField As Double
		Private ReadOnly flightAltitudeField As Double
		Private ReadOnly imageField As Image
		Private ReadOnly trajectoryField As PlaneTrajectory

		Private Property Course() As Double
			Get
				Return course_Renamed
			End Get
			Set(ByVal value As Double)
				If course_Renamed = value Then
					Return
				End If
				course_Renamed = value
				UpdateIcon()
			End Set
		End Property
		Protected Overrides ReadOnly Property Type() As MapItemType
			Get
				Return MapItemType.Custom
			End Get
		End Property
		Public Property CurrentFlightTime() As Double
			Get
				Return currentFlightTime_Renamed
			End Get
			Set(ByVal value As Double)
				If currentFlightTime_Renamed = value Then
					Return
				End If
				currentFlightTime_Renamed = value
				UpdatePosition(currentFlightTime_Renamed)
			End Set
		End Property
		Public ReadOnly Property PlaneID() As String
			Get
				Return planeIDField
			End Get
		End Property
		Public ReadOnly Property Name() As String
			Get
				Return nameField
			End Get
		End Property
		Public ReadOnly Property EndPointName() As String
			Get
				Return endPointNameField
			End Get
		End Property
		Public ReadOnly Property StartPointName() As String
			Get
				Return startPointNameField
			End Get
		End Property
		Public ReadOnly Property SpeedKmH() As Double
			Get
				Return If(isLandedField, 0.0, speedInKmHField)
			End Get
		End Property
		Public ReadOnly Property FlightAltitude() As Double
			Get
				Return If(isLandedField, 0.0, flightAltitudeField)
			End Get
		End Property
		Public ReadOnly Property Image() As Image
			Get
				Return imageField
			End Get
		End Property
		Public ReadOnly Property IsLanded() As Boolean
			Get
				Return isLandedField
			End Get
		End Property
		Public ReadOnly Property TotalFlightTime() As Double
			Get
				Return trajectoryField.FlightTime
			End Get
		End Property
		Public Overrides ReadOnly Property Icon() As Image
			Get
				Return icon_Renamed
			End Get
		End Property
		Public ReadOnly Property Trajectory() As PlaneTrajectory
			Get
				Return trajectoryField
			End Get
		End Property

		Public Sub New(ByVal name As String, ByVal id As String, ByVal endPointName As String, ByVal startPointName As String, ByVal speedInKmH As Double, ByVal flightAltitude As Double, ByVal points As List(Of CoordPoint), ByVal sourceIcon As Image)
			Me.nameField = name
			Me.planeIDField = id
			Me.sourceIcon = sourceIcon
			Me.endPointNameField = endPointName
			Me.startPointNameField = startPointName
			Me.speedInKmHField = speedInKmH
			Me.flightAltitudeField = flightAltitude
			Me.imageField = New Bitmap(ConvertPlaneNameToFilePath(name))
			trajectoryField = New PlaneTrajectory(points, speedInKmH)
			UpdatePosition(CurrentFlightTime)
		End Sub
		Private Sub UpdatePosition(ByVal flightTime As Double)
			isLandedField = flightTime >= trajectoryField.FlightTime
			Dim point As GeoPoint = trajectoryField.GetPointByCurrentFlightTime(flightTime)
			Latitude = point.Latitude
			Longitude = point.Longitude
			Course = trajectoryField.GetCourseByCurrentFlightTime(flightTime)
		End Sub
		Private Sub UpdateIcon()
			SyncLock MapElements.ImageLocker
				If icon_Renamed IsNot Nothing Then
					icon_Renamed.Dispose()
					icon_Renamed = Nothing
				End If
				icon_Renamed = DemoUtils.GetRotatedImage(sourceIcon, Course)
			End SyncLock
		End Sub
	End Class

	Public Class FlightMapDataGenerator
		Implements IDisposable
		Private Const mSecPerHour As Double = 3600000
		Private Const SpeedScale As Double = 100.0

		Private selectedPlane_Renamed As PlaneInfo
		Private lastTime As DateTime
		Private isDisposed As Boolean
		Private ReadOnly planes_Renamed As New List(Of PlaneInfo)()
		Private ReadOnly airPaths_Renamed As New List(Of InfoBase)()
		Private ReadOnly timer As New Timer()
		Private ReadOnly sourcePlaneIcon As Image
		Private ReadOnly visualizer As PlaneInfoVisualizer

		Public ReadOnly Property Planes() As List(Of PlaneInfo)
			Get
				Return planes_Renamed
			End Get
		End Property
		Public ReadOnly Property AirPaths() As List(Of InfoBase)
			Get
				Return airPaths_Renamed
			End Get
		End Property
		Public Property SelectedPlane() As PlaneInfo
			Get
				Return selectedPlane_Renamed
			End Get
			Set(ByVal value As PlaneInfo)
				If selectedPlane_Renamed Is value Then
					Return
				End If
				selectedPlane_Renamed = value
				UpdatePlaneInfo(selectedPlane_Renamed)
			End Set
		End Property

		Public Sub New(ByVal overlayManager As MapElementsOverlayManager)
			visualizer = New PlaneInfoVisualizer(overlayManager)
			sourcePlaneIcon = New Bitmap(DemoUtils.GetRelativePath("Images\Plane.png"))
			LoadFromXML()
			AddHandler timer.Tick, AddressOf OnTimedEvent
			timer.Interval = 2000
		End Sub
		Protected Overrides Sub Finalize()
			Dispose(False)
		End Sub

		Public Event DataChanged As EventHandler

		Private Sub RaiseDataChanged()
			RaiseEvent DataChanged(Me, EventArgs.Empty)
		End Sub
		Private Sub LoadFromXML()
			Dim document As XDocument = DemoUtils.LoadXml("FlightMap.xml")
			If document IsNot Nothing Then
				For Each element As XElement In document.Element("Planes").Elements()
					Dim points As New List(Of CoordPoint)()
					For Each infoElement As XElement In element.Element("Path").Elements()
						Dim geoPoint As New GeoPoint(Convert.ToDouble(infoElement.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(infoElement.Element("Longitude").Value, CultureInfo.InvariantCulture))
						points.Add(geoPoint)
					Next infoElement
					Dim info As New PlaneInfo(element.Element("PlaneName").Value, element.Element("PlaneID").Value, element.Element("EndPointName").Value, element.Element("StartPointName").Value, Convert.ToInt32(element.Element("Speed").Value), Convert.ToInt32(element.Element("Altitude").Value), points, sourcePlaneIcon)
					info.CurrentFlightTime = Convert.ToDouble(element.Element("CurrentFlightTime").Value, CultureInfo.InvariantCulture)
					planes_Renamed.Add(info)
					airPaths_Renamed.Add(info.Trajectory)
					airPaths_Renamed.Add(info.Trajectory.StartPoint)
					airPaths_Renamed.Add(info.Trajectory.EndPoint)
				Next element
			End If
		End Sub
		Private Sub UpdatePlaneInfo(ByVal planeInfo As PlaneInfo)
			visualizer.SelectedPlane = planeInfo
		End Sub
		Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
			Dim currentTime As DateTime = DateTime.Now
			Dim interval As TimeSpan = currentTime.Subtract(lastTime)
			For Each info As PlaneInfo In planes_Renamed
				If (Not info.IsLanded) Then
					info.CurrentFlightTime += SpeedScale * interval.TotalMilliseconds / mSecPerHour
				End If
			Next info
			lastTime = currentTime
			visualizer.Update()
			RaiseDataChanged()
		End Sub
		Private Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not isDisposed) Then
				isDisposed = True
				Me.Stop()
				If timer IsNot Nothing Then
					timer.Dispose()
				End If
				If sourcePlaneIcon IsNot Nothing Then
					sourcePlaneIcon.Dispose()
				End If
			End If
		End Sub
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub
		Public Sub Start()
			lastTime = DateTime.Now
			timer.Start()
		End Sub
		Public Sub [Stop]()
			timer.Stop()
		End Sub
		Public Function FindAirPath(ByVal plane As PlaneInfo) As List(Of InfoBase)
			Return airPaths_Renamed.FindAll(Function(airPath) plane.Trajectory Is airPath OrElse plane.Trajectory.StartPoint Is airPath OrElse plane.Trajectory.EndPoint Is airPath)
		End Function
	End Class

	Public Class PlaneInfoVisualizer
		Private overlayManager As MapElementsOverlayManager
		Private selectedPlane_Renamed As PlaneInfo


		Public Property SelectedPlane() As PlaneInfo
			Get
				Return selectedPlane_Renamed
			End Get
			Set(ByVal value As PlaneInfo)
				If selectedPlane_Renamed Is value Then
					Return
				End If
				selectedPlane_Renamed = value
				Update()
			End Set
		End Property

		Public Sub New(ByVal overlayManager As MapElementsOverlayManager)
			Me.overlayManager = overlayManager
		End Sub

		Public Sub Update()
			If selectedPlane_Renamed Is Nothing Then
				overlayManager.SetOverlaysVisibility(False)
				Return
			End If
			overlayManager.SetOverlaysVisibility(True)
			overlayManager.SetTextToItemByKey("name", selectedPlane_Renamed.Name)
			overlayManager.SetTextToItemByKey("id", selectedPlane_Renamed.PlaneID)
			overlayManager.SetTextToItemByKey("from", selectedPlane_Renamed.StartPointName)
			overlayManager.SetTextToItemByKey("to", selectedPlane_Renamed.EndPointName)
			overlayManager.SetTextToItemByKey("current_time", New TimeSpan(0, 0, CInt(Fix(Math.Ceiling(selectedPlane_Renamed.CurrentFlightTime * 3600)))).ToString())
			overlayManager.SetTextToItemByKey("flight_time", New TimeSpan(0, 0, CInt(Fix(Math.Ceiling(selectedPlane_Renamed.TotalFlightTime * 3600)))).ToString())
			overlayManager.SetTextToItemByKey("speed", selectedPlane_Renamed.SpeedKmH.ToString())
			overlayManager.SetTextToItemByKey("altitude", selectedPlane_Renamed.FlightAltitude.ToString())
			overlayManager.SetImage(selectedPlane_Renamed.Image)
		End Sub
	End Class

	Public Class FlightMapFactory
		Inherits DefaultMapItemFactory
		Protected Overrides Sub InitializeItem(ByVal item As MapItem, ByVal obj As Object)
			MyBase.InitializeItem(item, obj)

			Dim polyLine As MapPolyline = TryCast(item, MapPolyline)
			Dim trajectory As PlaneTrajectory = TryCast(obj, PlaneTrajectory)
			If polyLine IsNot Nothing AndAlso trajectory IsNot Nothing Then
				polyLine.IsGeodesic = True
				polyLine.Points = trajectory.GetAirPath()
				polyLine.Fill = Color.Empty
				polyLine.Stroke = Color.FromArgb(127, 255, 0, 199)
				polyLine.StrokeWidth = 4
				trajectory.UpdateTrajectory(polyLine.ActualPoints.ToList())
			End If
			Dim customElement As MapCustomElement = TryCast(item, MapCustomElement)
			If customElement IsNot Nothing Then
				customElement.UseAnimation = False
				customElement.BackgroundDrawingMode = ElementState.None
			End If
		End Sub
	End Class
End Namespace
