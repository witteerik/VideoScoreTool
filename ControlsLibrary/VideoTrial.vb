Public Class VideoTrialSet
    Public Property TrialList As New List(Of VideoTrial)

    Public Enum Trialorders
        Random
        Chronological
        Alphabetic
        Input
    End Enum

    Private Function GetSortedTrialListCopy(ByVal TrialOrder As Trialorders) As List(Of VideoTrial)

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

        Return MySortedList

    End Function

    Public Sub SetTrialOrder(ByVal TrialOrder As Trialorders)

        TrialList = GetSortedTrialListCopy(TrialOrder)

    End Sub


    Public Function GetSuggestedExportFilename(ByVal OriginalDataFilepath As String) As String

        Dim ExportFileName As String = IO.Path.Combine(IO.Path.GetDirectoryName(OriginalDataFilepath), IO.Path.GetFileNameWithoutExtension(OriginalDataFilepath) & "_Scored" & ".csv")

        Return ExportFileName

    End Function

    Public Sub SaveAs(ByVal OriginalDataFilepath As String, ByVal VariableList As List(Of String), Optional ByRef ExportFile As String = "")

        Try

            If ExportFile = "" Then
                ExportFile = GetSuggestedExportFilename(OriginalDataFilepath)
            End If

            Dim FileDialog As New SaveFileDialog
            FileDialog.FileName = ExportFile
            FileDialog.Filter = "CSV file (.csv)|*.csv"
            FileDialog.OverwritePrompt = True

            Dim Result = FileDialog.ShowDialog
            If Result = DialogResult.OK Then

                ExportFile = FileDialog.FileName

                SaveResults(ExportFile, VariableList)

            Else
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("The following error occurred during save: " & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation, "Error saving")
        End Try

    End Sub

    Public Sub SaveResults(ByVal ExportFile As String, ByVal VariableList As List(Of String))

        Try


            'Trimming off file extension
            ExportFile = IO.Path.Combine(IO.Path.GetDirectoryName(ExportFile), IO.Path.GetFileNameWithoutExtension(ExportFile))

            Dim ExportList As New List(Of String)

            'Exporting headings
            Dim HeadingList As New List(Of String)
            For Each Heading In VariableList
                HeadingList.Add(Heading)
            Next
            HeadingList.Add("VideoScore")
            ExportList.Add(String.Join(vbTab, HeadingList))

            Dim SortedTrialListCopy = GetSortedTrialListCopy(Trialorders.Input)

            If SortedTrialListCopy.Count > 0 Then

                For n = 0 To SortedTrialListCopy.Count - 1

                    If SortedTrialListCopy(n).IsScored = True Then
                        ExportList.Add(SortedTrialListCopy(n).RawData & vbTab & SortedTrialListCopy(n).GetScore)
                    Else
                        ExportList.Add(SortedTrialListCopy(n).RawData & vbTab & "")
                    End If

                Next

            End If

            'Exporting data
            Utils.SendInfoToLog(String.Join(vbCrLf, ExportList), IO.Path.GetFileName(ExportFile), IO.Path.GetDirectoryName(ExportFile), True, True, True, ".csv")

            MsgBox("Your data was saved to the file " & ExportFile & ".csv", MsgBoxStyle.Information, "Data saved")


        Catch ex As Exception
            MsgBox("The following error occurred during save: " & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation, "Error saving")
        End Try


    End Sub

End Class


Public Class VideoTrial

    Public ReadOnly CorrectVideoPath As String = ""

    Public ReadOnly TrialVideoStartTime As Double

    Public ReadOnly TrialVideoEndTime As Double

    Public ReadOnly RawData As String = ""

    Public ReadOnly ShouldBeScored As Boolean

    Public ReadOnly RawDataLine As Integer

    Public Question As ScoringQuestion

    Public Enum ScoringTypes
        Binary
        Ordinal_1_to_5
        Ordinal_1_to_7
        Scale_0_to_10
        Text
    End Enum

    Public Sub New(ByVal ScoringType As ScoringTypes, ByVal ShouldBeScored As Boolean, ByVal RawData As String, ByVal CorrectVideoPath As String,
                   ByVal TrialVideoStartTime As Double, ByVal TrialVideoEndTime As Double, ByVal RawDataLine As Integer)

        Me.ShouldBeScored = ShouldBeScored
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

    Public Function IsScored() As Boolean

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


    Public Function GetScore() As String

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

    Public Overrides Function ToString() As String

        Dim OutputList As New List(Of String)

        OutputList.Add(RawDataLine - 1)
        OutputList.Add(IO.Path.GetFileName(CorrectVideoPath))
        OutputList.Add(Math.Round(TrialVideoStartTime) & " s")

        If IsScored() = True Then
            OutputList.Add(GetScore)
        End If

        Return String.Join(", ", OutputList)

    End Function

End Class

