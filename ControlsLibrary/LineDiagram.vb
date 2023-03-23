Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.VisualBasic.Devices


<Serializable>
    Public Class LineDiagram
        Inherits PlotBase

        Public Sub New()
            MyBase.New

            'Setting up audiogram properties
            PlotAreaRelativeMarginLeft = 0.1
            PlotAreaRelativeMarginRight = 0.1
            PlotAreaRelativeMarginTop = 0.1
            PlotAreaRelativeMarginBottom = 0.1

            XlimMin = 0
        XlimMax = 300
        'XlimMax = 8000

        YlimMin = 0
            YlimMax = 1

            PlotAreaBorderColor = Color.DarkGray
            PlotAreaBorder = True
            'GridLineColor = Color.Gray

            'XaxisGridLinePositions = New List(Of Single) From {125, 250, 500, 1000, 2000, 4000, 8000}
            'XaxisDashedGridLinePositions = New List(Of Single) From {750, 1500, 3000, 6000}
            'XaxisDrawTop = False
            'XaxisDrawBottom = True
            'XaxisTickPositions = New List(Of Single)
            'XaxisTickHeight = 2
            'XaxisTextPositions = New List(Of Single) From {125, 250, 500, 1000, 2000, 4000, 8000}
            'XaxisTextValues = {"125", "250", "500", "1k", "2k", "4k", "8k"}
            'XaxisTextSize = 8
            'XAxisTextBrush = Brushes.Black

            'YaxisGridLinePositions = New List(Of Single) From {0, 10, 20, 30, 40, 50, 60, 70}
            'YaxisDashedGridLinePositions = New List(Of Single) From {-5, 5, 15, 25, 35, 45, 55, 65}
            'YaxisDrawLeft = True
            'YaxisDrawRight = False
            'YaxisTickPositions = New List(Of Single)
            'YaxisTickWidth = 2
            'YaxisTextPositions = New List(Of Single) From {-10, 0, 10, 20, 30, 40, 50, 60, 80, 90, 100}
            'YaxisTextValues = {"-10", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"}
            'YaxisTextSize = 8
            'YaxisTextBrush = Brushes.Black

        End Sub



#Region "Diagram editing"

    Public Sub DrawLineAndPointData(ByVal XValues As Single(), ByVal YValues As Single())

        If XValues.Length = 0 Then Exit Sub

        If Not XValues.Length = YValues.Length Then Exit Sub

        XlimMax = Math.Max(1, XValues.Max)

        'Clearing any previously stored data
        PointSeries.Clear()
        Lines.Clear()

        'Draw points
        'PointSeries.Add(New PointSerie With {.Color = Color.Black, .PointSize = 2, .XValues = XValues, .YValues = YValues})

        'Adding LinesSeries
        Lines.Add(New Line With {.Color = Color.Red, .LineWidth = 1, .XValues = XValues, .YValues = YValues})

        'Updates the layout
        Invalidate()
        Update()

    End Sub

    Public Sub ClearLines()

        'Clearing any previously stored data
        PointSeries.Clear()
        Lines.Clear()

        'Updates the layout
        Invalidate()
        Update()

    End Sub



#End Region


End Class

