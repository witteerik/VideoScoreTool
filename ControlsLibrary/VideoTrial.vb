Public Class VideoTrialSet
    Public Property TrialList As New List(Of VideoTrial)
    Public Property CurrentItemIndex As Integer = 0

    Public Enum Trialorders
        Random
        Chronological
        Alphabetic
        Input
    End Enum

    Public Sub SetTrialOrder(ByVal TrialOrder As Trialorders)

        'Sorting the trial list
        Dim MySortedList As New List(Of VideoTrial)
        Dim SortQuery As IOrderedEnumerable(Of VideoTrial) = Nothing

        Select Case TrialOrder
            Case Trialorders.Alphabetic
                SortQuery = TrialList.OrderBy(Function(x) x.CorrectVideoPath)
            Case Trialorders.Chronological
                SortQuery = TrialList.OrderBy(Function(x) x.TrialVideoStartTime)
            Case Trialorders.Random
                Dim rnd As New Random
                SortQuery = TrialList.OrderBy(Function(x) x.GetRandomNumber(rnd))
            Case Trialorders.Input
                SortQuery = TrialList.OrderBy(Function(x) x.RawDataLine)
            Case Else
                SortQuery = TrialList.OrderBy(Function(x) x.RawDataLine)
        End Select

        'Adding in sorted order
        For Each CurrentList In SortQuery
            MySortedList.Add(CurrentList)
        Next
        TrialList = MySortedList

    End Sub


    Public Function GetNextNonCompleteStimulus() As VideoTrial

        Dim SelectedStimulus As VideoTrial = Nothing

        For n = 0 To TrialList.Count - 1
            If TrialList(n).ShouldBeRated = True Then
                If TrialList(n).IsRated = False Then
                    CurrentItemIndex = n
                    SelectedStimulus = TrialList(n)
                    Exit For
                End If
            End If
        Next

        Return SelectedStimulus

    End Function

    Public Function GetPreviousTrial() As VideoTrial

        Dim SelectedStimulus As VideoTrial = Nothing

        If CurrentItemIndex - 1 < 0 Then
            Return SelectedStimulus
        End If

        For n = CurrentItemIndex - 1 To 0 Step -1
            If TrialList(n).ShouldBeRated = True Then
                If TrialList(n).IsRated = False Then
                    CurrentItemIndex = n
                    SelectedStimulus = TrialList(n)
                    Exit For
                End If
            End If
        Next

        Return SelectedStimulus

    End Function

    Public Function GetNextTrial() As VideoTrial

        Dim SelectedStimulus As VideoTrial = Nothing

        If CurrentItemIndex + 1 > TrialList.Count - 1 Then
            Return SelectedStimulus
        End If

        For n = CurrentItemIndex + 1 To TrialList.Count - 1
            If TrialList(n).ShouldBeRated = True Then
                If TrialList(n).IsRated = False Then
                    CurrentItemIndex = n
                    SelectedStimulus = TrialList(n)
                    Exit For
                End If
            End If
        Next

        Return SelectedStimulus

    End Function

    'Public Sub SaveResults(ByVal ExportFileName As String, ByVal ParticipantID As String, ByVal ParticipantNumber As Integer)

    '    If StimulusList.Count > 0 Then

    '        Dim ExportList As New List(Of String)

    '        Dim IncludeHeadings As Boolean = True
    '        For n = 0 To StimulusList.Count - 1
    '            ExportList.Add(StimulusList(n).ToString(IncludeHeadings, ParticipantID, ParticipantNumber))
    '            IncludeHeadings = False
    '        Next

    '        'Exporting data
    '        Utils.SendInfoToLog(String.Join(vbCrLf, ExportList), IO.Path.GetFileName(ExportFileName), IO.Path.GetDirectoryName(ExportFileName), True, True)

    '        'MsgBox(Utils.GetGuiString(Utils.VrtGuiStringKeys.SavedToFile) & " " & ExportFileName & vbCrLf & vbCrLf & Utils.GetGuiString(Utils.VrtGuiStringKeys.CloseApp2), MsgBoxStyle.Information, My.Application.Info.Title)

    '    End If

    'End Sub

End Class


Public Class VideoTrial

    Public ReadOnly CorrectVideoPath As String = ""

    Public ReadOnly TrialVideoStartTime As Double

    Public ReadOnly TrialVideoEndTime As Double

    Public ReadOnly RawData As String = ""

    Public ReadOnly ShouldBeRated As Boolean

    Public ReadOnly RawDataLine As Integer

    Public Question As ScoringQuestion

    Public Enum ScoringTypes
        Binary
        Ordinal_1_to_5
        Ordinal_1_to_7
        Scale_0_to_10
        Text
    End Enum

    Public Sub New(ByVal ScoringType As ScoringTypes, ByVal ShouldBeRated As Boolean, ByVal RawData As String, ByVal CorrectVideoPath As String,
                   ByVal TrialVideoStartTime As Double, ByVal TrialVideoEndTime As Double, ByVal RawDataLine As Integer)

        Me.ShouldBeRated = ShouldBeRated
        Me.RawData = RawData
        Me.CorrectVideoPath = CorrectVideoPath
        Me.TrialVideoStartTime = TrialVideoStartTime
        Me.TrialVideoEndTime = TrialVideoEndTime
        Me.RawDataLine = RawDataLine

        'Creating the question
        Question = New ScoringQuestion
        Question.Question = ""

        Select Case ScoringType
            Case ScoringTypes.Binary
                Question.QuestionType = ScoringQuestion.QuestionTypes.Categorical
                Question.CategoricalResponseAlternatives = New List(Of String) From {"Correct", "Incorrect"}

            Case ScoringTypes.Text
                Question.Question = "Type a written comment or and hyphen to skip."
                Question.QuestionType = ScoringQuestion.QuestionTypes.Text

            Case ScoringTypes.Ordinal_1_to_5
                Question.QuestionType = ScoringQuestion.QuestionTypes.IntegerScale
                Question.ScaleValues = New List(Of Double) From {1, 2, 3, 4, 5}

            Case ScoringTypes.Ordinal_1_to_7
                Question.QuestionType = ScoringQuestion.QuestionTypes.IntegerScale
                Question.ScaleValues = New List(Of Double) From {1, 2, 3, 4, 5, 6, 7}

            Case ScoringTypes.Scale_0_to_10
                Question.QuestionType = ScoringQuestion.QuestionTypes.ContinousScale
                Question.ScaleValues = New List(Of Double) From {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}

        End Select

    End Sub

    Public Function IsRated() As Boolean

        Select Case Question.QuestionType
            Case ScoringQuestion.QuestionTypes.Categorical
                If Question.CategoricalResponse = "" Then
                    Return False
                End If

            Case ScoringQuestion.QuestionTypes.ContinousScale, ScoringQuestion.QuestionTypes.IntegerScale
                If Question.ScaleResponse.HasValue = False Then
                    Return False
                End If

            Case ScoringQuestion.QuestionTypes.Text
                If Question.TextResponse.Trim = "" Then
                    Return False
                End If

            Case Else
                Throw New NotImplementedException("Unknown QuestionType (" & Question.QuestionType & ") supplied  for question: " & Question.Question)
        End Select

        Return True

    End Function


    Public Function GetResponse() As String

        If Question.QuestionType = ScoringQuestion.QuestionTypes.Categorical Then
            If Question.CategoricalResponse <> "" Then
                Return Question.CategoricalResponse
            Else
                Return ""
            End If
        ElseIf Question.QuestionType = ScoringQuestion.QuestionTypes.Text Then
            If Question.TextResponse.Trim <> "" Then
                Return Question.TextResponse.Replace(vbCrLf, "; ")
            Else
                Return ""
            End If
        Else
            If Question.ScaleResponse.HasValue = True Then
                Return Question.ScaleResponse
            Else
                Return ""
            End If
        End If

    End Function

    Public Function GetRandomNumber(ByRef rnd As Random) As Double
        Return rnd.NextDouble()
    End Function

    Public Shadows Function ToString() As String

        Dim OutputList As New List(Of String)

        OutputList.Add(RawDataLine)
        OutputList.Add(IO.Path.GetFileNameWithoutExtension(CorrectVideoPath))
        OutputList.Add(TrialVideoStartTime)

        If IsRated() = True Then
            OutputList.Add(GetResponse)
        End If

        Return String.Join(" ", OutputList)

    End Function

End Class

