﻿Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

<Serializable>
Public Class RatingPanel
    Inherits TableLayoutPanel

    Public CurrentRatingStimulus As VideoTrial

    Public Event ResponseGiven()
    Public Event ResponseRemoved()

    Public Sub AddQuestions(ByRef CurrentRatingStimulus As VideoTrial)

        Me.CurrentRatingStimulus = CurrentRatingStimulus

        Dim RandomColorSource = New Random(1)

        Me.Visible = False
        Me.Padding = New Padding(10)

        Me.RowStyles.Clear()
        Dim NewItemRatingPanel As New RatingQuestionPanel(2, RandomColorSource)
        AddHandler NewItemRatingPanel.ResponseGiven, AddressOf ResponseGivenHandler
        AddHandler NewItemRatingPanel.ResponseRemoved, AddressOf ResponseRemovedHandler
        Me.Controls.Add(NewItemRatingPanel)
        NewItemRatingPanel.AddQuestion(CurrentRatingStimulus.Question)

        RatingItems_TableLayoutPanel_Resize(Nothing, Nothing)
        Me.Visible = True

    End Sub

    Private Sub RatingItems_TableLayoutPanel_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Visible = False
        For Each Control In Me.Controls
            Control.ResizeNow
        Next
        Me.Visible = True
    End Sub

    Public Sub ResponseGivenHandler()

        'Checks if all items have responses
        If CurrentRatingStimulus.IsRated = True Then
            RaiseEvent ResponseGiven()
        End If

    End Sub

    Public Sub ResponseRemovedHandler()

        'Raises the ResponseRemoved event so that the main form can inactivate next/previous buttons
        RaiseEvent ResponseRemoved()

    End Sub


End Class

<Serializable>
Public Class ScoringQuestion
    Public Question As String
    Public CategoricalResponseAlternatives As List(Of String)
    Public ScaleValues As List(Of Double)
    Public CategoricalResponse As String = ""
    Public ScaleResponse As Double? = Nothing
    Public TextResponse As String = ""

    Public Enum QuestionTypes
        Categorical
        ContinousScale
        IntegerScale
        Text
    End Enum

    Public Property QuestionType As QuestionTypes

End Class

<Serializable>
Public Class RatingQuestionPanel
    Inherits FlowLayoutPanel

    Public WithEvents ItemResponseInterface As IRatingResponse

    Private FontIncrease As Single
    Public Property SkipBackcolorRandomization As Boolean = False

    Private QuestionTextBox As Label

    Public Event ResponseGiven()
    Public Event ResponseRemoved()

    Public Sub New()
        Me.New(2, New Random)
    End Sub

    Public Sub New(ByVal FontIncrease As Single, ByVal RandomColorSource As Random)
        Me.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Me.FontIncrease = FontIncrease
        Me.AutoSize = False

        'Setting a random background color on the control
        If SkipBackcolorRandomization = False Then
            Me.BackColor = Drawing.Color.FromArgb(20, CSng(RandomColorSource.Next(10, 255)), CSng(RandomColorSource.Next(10, 255)), CSng(RandomColorSource.Next(10, 255)))
        End If

    End Sub

    Public Sub AddQuestion(ByRef Question As ScoringQuestion)

        'Adding question text box
        QuestionTextBox = New Label
        QuestionTextBox.Text = Question.Question
        QuestionTextBox.TextAlign = ContentAlignment.MiddleLeft
        QuestionTextBox.Font = New Font(QuestionTextBox.Font.FontFamily, QuestionTextBox.Font.Size + FontIncrease)
        QuestionTextBox.Width = Parent.ClientRectangle.Width
        QuestionTextBox.AutoSize = True
        QuestionTextBox.Padding = New Padding(5)
        QuestionTextBox.BackColor = Color.Transparent

        Me.Controls.Add(QuestionTextBox)

        Select Case Question.QuestionType
            Case ScoringQuestion.QuestionTypes.Categorical
                ItemResponseInterface = New RatingCategoriesPanel
            Case ScoringQuestion.QuestionTypes.ContinousScale, ScoringQuestion.QuestionTypes.IntegerScale
                ItemResponseInterface = New RatingScalePanel
            Case ScoringQuestion.QuestionTypes.Text
                ItemResponseInterface = New RatingTextPanel
        End Select

        Me.Controls.Add(ItemResponseInterface)
        ItemResponseInterface.AddRatingQuestion(Question)

    End Sub

    Public Sub ResizeNow()

        If QuestionTextBox Is Nothing Then Exit Sub
        If ItemResponseInterface Is Nothing Then Exit Sub

        Me.Width = Parent.ClientRectangle.Width
        QuestionTextBox.Width = Parent.ClientRectangle.Width
        ItemResponseInterface.ResizeNow()
        Me.Height = QuestionTextBox.Height + QuestionTextBox.Padding.Vertical + ItemResponseInterface.GetHeight

    End Sub

    Public Sub ResponseGivenHandler() Handles ItemResponseInterface.ResponseGiven
        RaiseEvent ResponseGiven()
    End Sub

    Public Sub ResponseRemovedHandler() Handles ItemResponseInterface.ResponseRemoved
        RaiseEvent ResponseRemoved()
    End Sub

End Class

Public Interface IRatingResponse
    Sub AddRatingQuestion(ByRef Question As ScoringQuestion)
    Sub ResizeNow()
    Function GetHeight()
    Event ResponseGiven()
    Event ResponseRemoved()
End Interface

Public Class RatingCategoriesPanel
    Inherits TableLayoutPanel
    Implements IRatingResponse

    Private FontIncrease As Single
    Private Question As ScoringQuestion

    Public Sub New()
        Me.New(2)
    End Sub

    Public Sub New(ByVal FontIncrease As Single)
        Me.FontIncrease = FontIncrease
        Me.BackColor = Color.Transparent
    End Sub

    Public Event ResponseGiven() Implements IRatingResponse.ResponseGiven

    'This class cannot remove responses and does not need to raise this event
    Public Event ResponseRemoved() Implements IRatingResponse.ResponseRemoved

    Public Sub AddRatingQuestion(ByRef Question As ScoringQuestion) Implements IRatingResponse.AddRatingQuestion

        Me.Question = Question

        Me.RowCount = 2
        Me.ColumnCount = Question.CategoricalResponseAlternatives.Count

        'Me.Dock = DockStyle.Top
        Me.GrowStyle = TableLayoutPanelGrowStyle.FixedSize

        'Adding reponse radiobuttons
        For n = 0 To Question.CategoricalResponseAlternatives.Count - 1
            Dim NewResponseAlternativeButton As New RadioButtonWithResponseText
            NewResponseAlternativeButton.CheckAlign = ContentAlignment.MiddleCenter
            NewResponseAlternativeButton.Dock = DockStyle.Fill
            NewResponseAlternativeButton.BackColor = Color.Transparent

            NewResponseAlternativeButton.ResponseText = Question.CategoricalResponseAlternatives(n)

            'Checks if already answeres
            If Question.CategoricalResponse <> "" Then If Question.CategoricalResponse = Question.CategoricalResponseAlternatives(n) Then NewResponseAlternativeButton.Checked = True

            AddHandler NewResponseAlternativeButton.Click, AddressOf HandleResponse

            Me.Controls.Add(NewResponseAlternativeButton, n, 0)
        Next

        For n = 0 To Question.CategoricalResponseAlternatives.Count - 1
            Dim NewResponseAlternativeLabel As New Label
            NewResponseAlternativeLabel.Text = Question.CategoricalResponseAlternatives(n)
            NewResponseAlternativeLabel.Font = New Font(NewResponseAlternativeLabel.Font.FontFamily, NewResponseAlternativeLabel.Font.Size + FontIncrease)
            NewResponseAlternativeLabel.TextAlign = ContentAlignment.MiddleCenter
            NewResponseAlternativeLabel.AutoSize = False
            NewResponseAlternativeLabel.Dock = DockStyle.Fill
            NewResponseAlternativeLabel.BackColor = Color.Transparent
            Me.Controls.Add(NewResponseAlternativeLabel, n, 1)
        Next

    End Sub

    Public Sub ResizeNow() Implements IRatingResponse.ResizeNow

        If Me.Question Is Nothing Then Exit Sub

        Me.Width = Parent.ClientRectangle.Width - Parent.Padding.Horizontal
        Me.Height = 50

        RowStyles.Clear()
        ColumnStyles.Clear()
        RowStyles.Add(New RowStyle(SizeType.Absolute, 20))
        RowStyles.Add(New RowStyle(SizeType.Percent, 100))
        For Each ResponseAlternative In Question.CategoricalResponseAlternatives
            ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / Question.CategoricalResponseAlternatives.Count))
        Next

    End Sub

    Public Function GetHeight() Implements IRatingResponse.GetHeight
        Return Me.Height + Me.Padding.Vertical
    End Function

    Public Sub HandleResponse(sender As Object, e As EventArgs)

        'Stores the response
        Question.CategoricalResponse = sender.ResponseText

        RaiseEvent ResponseGiven()

    End Sub

End Class

Public Class RatingScalePanel
    Inherits PictureBox
    Implements IRatingResponse

    Private FontIncrease As Single

    Private Min As Double
    Private Max As Double
    Private MidcentreStringFormat As New StringFormat
    Private ResponsePen = New Pen(Brushes.DarkGreen, 2)

    Private Question As ScoringQuestion

    Public Event ResponseGiven() Implements IRatingResponse.ResponseGiven

    'This class cannot remove responses and does not need to raise this event
    Public Event ResponseRemoved() Implements IRatingResponse.ResponseRemoved

    Public Sub New()
        Me.New(2)
    End Sub

    Public Sub New(ByVal FontIncrease As Single)
        Me.FontIncrease = FontIncrease
        Me.BackColor = Color.Transparent
        Me.Font = New Font(Me.Font.FontFamily, Me.Font.Size + FontIncrease)
        Me.MidcentreStringFormat.Alignment = StringAlignment.Center
        Me.MidcentreStringFormat.LineAlignment = StringAlignment.Center
    End Sub

    Public Sub AddRatingQuestion(ByRef Question As ScoringQuestion) Implements IRatingResponse.AddRatingQuestion

        Me.Question = Question

        Me.Min = Me.Question.ScaleValues.Min
        Me.Max = Me.Question.ScaleValues.Max

    End Sub

    Public Sub ResizeNow() Implements IRatingResponse.ResizeNow

        If Me.Question Is Nothing Then Exit Sub

        Me.Width = Parent.ClientRectangle.Width - Parent.Padding.Horizontal
        Me.Height = 70

    End Sub

    Public Function GetHeight() Implements IRatingResponse.GetHeight
        Return Me.Height + Me.Padding.Vertical
    End Function

    Private Sub DrawScale(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint

        If Question Is Nothing Then Exit Sub

        Dim CurrentScaleData = GetCurrentScaleData()

        e.Graphics.DrawLine(Pens.Black, CurrentScaleData.LeftEndPoint, CurrentScaleData.RightEndPoint)
        e.Graphics.DrawLine(Pens.Black, CurrentScaleData.LeftEndPoint.X, CurrentScaleData.LeftEndPoint.Y, CurrentScaleData.LeftEndPoint.X + CurrentScaleData.ArrowHeight, CurrentScaleData.LeftEndPoint.Y - CurrentScaleData.ArrowHeight)
        e.Graphics.DrawLine(Pens.Black, CurrentScaleData.LeftEndPoint.X, CurrentScaleData.LeftEndPoint.Y, CurrentScaleData.LeftEndPoint.X + CurrentScaleData.ArrowHeight, CurrentScaleData.LeftEndPoint.Y + CurrentScaleData.ArrowHeight)
        e.Graphics.DrawLine(Pens.Black, CurrentScaleData.RightEndPoint.X, CurrentScaleData.RightEndPoint.Y, CurrentScaleData.RightEndPoint.X - CurrentScaleData.ArrowHeight, CurrentScaleData.RightEndPoint.Y - CurrentScaleData.ArrowHeight)
        e.Graphics.DrawLine(Pens.Black, CurrentScaleData.RightEndPoint.X, CurrentScaleData.RightEndPoint.Y, CurrentScaleData.RightEndPoint.X - CurrentScaleData.ArrowHeight, CurrentScaleData.RightEndPoint.Y + CurrentScaleData.ArrowHeight)

        For Each ScaledPoint In CurrentScaleData.ScaledPoints
            e.Graphics.DrawString(ScaledPoint.Item1.ToString, Me.Font, Brushes.Black, ScaledPoint.Item2, MidcentreStringFormat)
        Next

        'Draws response if available
        If CurrentScaleData.ResponsePoint.IsEmpty = False Then
            e.Graphics.DrawLine(ResponsePen, CurrentScaleData.ResponsePoint.X - CurrentScaleData.CrossHeight, CurrentScaleData.ResponsePoint.Y + CurrentScaleData.CrossHeight,
                                CurrentScaleData.ResponsePoint.X + CurrentScaleData.CrossHeight, CurrentScaleData.ResponsePoint.Y - CurrentScaleData.CrossHeight)
            e.Graphics.DrawLine(ResponsePen, CurrentScaleData.ResponsePoint.X - CurrentScaleData.CrossHeight, CurrentScaleData.ResponsePoint.Y - CurrentScaleData.CrossHeight,
                                CurrentScaleData.ResponsePoint.X + CurrentScaleData.CrossHeight, CurrentScaleData.ResponsePoint.Y + CurrentScaleData.CrossHeight)
        End If

    End Sub

    Public Class ScaleData
        Public LeftEndPoint As Point
        Public RightEndPoint As Point
        Public LineHeight As Integer
        Public ArrowHeight As Integer
        Public CrossHeight As Integer
        Public CurrentGuiScale As Double
        Public ScaledPoints As New List(Of Tuple(Of Integer, Point))
        Public ResponsePoint As Point = Point.Empty
    End Class

    Public Function GetCurrentScaleData() As ScaleData

        Dim NewScaleData As New ScaleData

        Dim Rect = Me.ClientRectangle
        Rect.Inflate(-Rect.Width / 10, 0)
        Dim LeftEnd As Integer = Rect.X
        Dim RightEnd As Integer = Rect.X + Rect.Width
        Dim ScaleWidth As Integer = RightEnd - LeftEnd
        NewScaleData.LineHeight = Rect.Height / 4
        NewScaleData.ArrowHeight = Rect.Height / 8
        NewScaleData.CrossHeight = Me.ClientRectangle.Height / 5
        Dim TextHeight As Integer = 2.8 * Rect.Height / 4
        NewScaleData.CurrentGuiScale = ScaleWidth / (Me.Max - Me.Min)
        NewScaleData.LeftEndPoint = New Point(LeftEnd, NewScaleData.LineHeight)
        NewScaleData.RightEndPoint = New Point(RightEnd, NewScaleData.LineHeight)

        For Each ScaleValue In Me.Question.ScaleValues
            Dim ScalevalueCentrePoint As New Point(LeftEnd + NewScaleData.CurrentGuiScale * (ScaleValue - Me.Min), TextHeight)
            NewScaleData.ScaledPoints.Add(New Tuple(Of Integer, Point)(ScaleValue, ScalevalueCentrePoint))
        Next

        If Question.ScaleResponse.HasValue Then
            NewScaleData.ResponsePoint = New Point(LeftEnd + NewScaleData.CurrentGuiScale * (Question.ScaleResponse - Me.Min), NewScaleData.LineHeight)
        End If

        Return NewScaleData

    End Function

    Private Sub Scale_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick

        Select Case e.Button
            Case MouseButtons.Left

                Dim CurrentScaleData = GetCurrentScaleData()
                Dim TransformedToResponseScale = (e.X - CurrentScaleData.LeftEndPoint.X) / CurrentScaleData.CurrentGuiScale + Me.Min

                If Question.QuestionType = ScoringQuestion.QuestionTypes.IntegerScale Then
                    'Rounding to the closest integer value
                    TransformedToResponseScale = Math.Round(TransformedToResponseScale)
                End If

                TransformedToResponseScale = Math.Min(TransformedToResponseScale, Me.Max)
                TransformedToResponseScale = Math.Max(TransformedToResponseScale, Me.Min)

                Question.ScaleResponse = TransformedToResponseScale

                Me.Invalidate()
                RaiseEvent ResponseGiven()

        End Select
    End Sub

End Class

Public Class RadioButtonWithResponseText
    Inherits RadioButton
    Public Property ResponseText As String

End Class


Public Class RatingTextPanel
    Inherits Panel
    Implements IRatingResponse

    Private FontIncrease As Single
    Private Question As ScoringQuestion

    Public Sub New()
        Me.New(2)
    End Sub

    Public Sub New(ByVal FontIncrease As Single)
        Me.FontIncrease = FontIncrease
        Me.BackColor = Color.Transparent
    End Sub

    Public Event ResponseGiven() Implements IRatingResponse.ResponseGiven

    Public Event ResponseRemoved() Implements IRatingResponse.ResponseRemoved

    Public Sub AddRatingQuestion(ByRef Question As ScoringQuestion) Implements IRatingResponse.AddRatingQuestion

        Me.Question = Question

        'Adding a text box where the response should be entered by the user
        Dim NewResponseTextBox As New TextBox
        NewResponseTextBox.Dock = DockStyle.Fill
        NewResponseTextBox.Multiline = True
        NewResponseTextBox.Font = New Font(NewResponseTextBox.Font.FontFamily, NewResponseTextBox.Font.Size + FontIncrease)
        NewResponseTextBox.ScrollBars = ScrollBars.Vertical

        'Adds repsonses already given
        If Question.TextResponse <> "" Then
            NewResponseTextBox.Text = Question.TextResponse
        End If

        AddHandler NewResponseTextBox.TextChanged, AddressOf HandleResponse
        Me.Controls.Add(NewResponseTextBox)

    End Sub

    Public Sub ResizeNow() Implements IRatingResponse.ResizeNow

        If Me.Question Is Nothing Then Exit Sub

        Me.Width = Parent.ClientRectangle.Width - Parent.Padding.Horizontal
        Me.Height = 70

    End Sub

    Public Function GetHeight() Implements IRatingResponse.GetHeight
        Return Me.Height + Me.Padding.Vertical
    End Function

    Public Sub HandleResponse(sender As Object, e As EventArgs)

        'Stores the response
        Question.TextResponse = sender.Text

        If Question.TextResponse <> "" Then
            RaiseEvent ResponseGiven()
        Else
            RaiseEvent ResponseRemoved()
        End If

    End Sub

End Class