Imports System.Globalization
Imports ControlsLibrary
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.CV.UI

Public Class MainForm

    Private CurrentDataFilePath As String = ""
    Private DataFileColumns As List(Of String)
    Private RawDataFileInputRows As List(Of String)
    Private CurrentExperimentVideoFilePath As String = ""
    Private CurrentVideoTrialSet As VideoTrialSet

    Private CorrectVideosFolder As String = ""

    Private WithEvents CurrentExperimentVideo As VideoCapture = Nothing
    Private CurrentExperimentVideoFrameRate As Double
    Private CurrentExperimentVideoFrameInterval As Integer = 1
    Private CurrentExperimentVideoLength As Integer
    Private CurrentExperimentVideoHeight As Integer
    Private CurrentExperimentVideoWidth As Integer
    Private CurrentExperimentVideoStartFrame As Integer = -1
    Private CurrentExperimentVideoEndFrame As Integer = -1
    Private WithEvents ExperimentVideoFrameTimer As New Windows.Forms.Timer

    Private WithEvents CurrentCorrectVideo As VideoCapture = Nothing
    Private CurrentCorrectVideoFrameRate As Double
    Private CurrentCorrectVideoFrameInterval As Integer = 1
    Private CurrentCorrectVideoLength As Integer
    Private CurrentCorrectVideoHeight As Integer
    Private CurrentCorrectVideoWidth As Integer
    Private CurrentCorrectVideoStartFrame As Integer = -1
    Private CurrentCorrectVideoEndFrame As Integer = -1
    Private WithEvents CorrectVideoFrameTimer As New Windows.Forms.Timer

    Private PlaySpeed As Double = 1

    Private WithEvents ScoringPanel As New ControlsLibrary.RatingPanel


    'Settings

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TrialOrder_ComboBox.Items.Add(ControlsLibrary.VideoTrialSet.Trialorders.Alphabetic)
        TrialOrder_ComboBox.Items.Add(ControlsLibrary.VideoTrialSet.Trialorders.Random)
        TrialOrder_ComboBox.Items.Add(ControlsLibrary.VideoTrialSet.Trialorders.Chronological)
        TrialOrder_ComboBox.Items.Add(ControlsLibrary.VideoTrialSet.Trialorders.Input)
        TrialOrder_ComboBox.SelectedIndex = 0

        ScoringType_ComboBox.Items.Add(ControlsLibrary.VideoTrial.ScoringTypes.Binary)
        ScoringType_ComboBox.Items.Add(ControlsLibrary.VideoTrial.ScoringTypes.Ordinal_1_to_5)
        ScoringType_ComboBox.Items.Add(ControlsLibrary.VideoTrial.ScoringTypes.Ordinal_1_to_7)
        ScoringType_ComboBox.Items.Add(ControlsLibrary.VideoTrial.ScoringTypes.Scale_0_to_10)
        ScoringType_ComboBox.Items.Add(ControlsLibrary.VideoTrial.ScoringTypes.Text)
        ScoringType_ComboBox.SelectedIndex = 0

        'Creating and adding the scoring panel
        ScoringPanel = New ControlsLibrary.RatingPanel
        ScoringPanel.Dock = DockStyle.Fill
        ScoringPanelHolder_Panel.Controls.Add(ScoringPanel)

    End Sub

    Private Sub SelectDataFile_Button_Click(sender As Object, e As EventArgs) Handles SelectDataFile_Button.Click

        Dim DataFileDialog As New OpenFileDialog
        DataFileDialog.Title = "Select a tab delimited (.csv) experiment file"
        DataFileDialog.CheckPathExists = True

        Dim DialogResult = DataFileDialog.ShowDialog
        If DialogResult = DialogResult.OK Then
            CurrentDataFilePath = DataFileDialog.FileName
        Else
            Exit Sub
        End If

        'Try to parse the file
        Try
            Dim FileContent = IO.File.ReadAllLines(CurrentDataFilePath)

            If FileContent.Length = 1 Then
                MsgBox("There must be at least two rows in the input file. The first column should contain variable names in tab-delimited columns and the remaining rows should contain trial data with one row for each trial.")
                Exit Sub
            End If

            If FileContent(0).Trim = "" Then
                MsgBox("The first row in the data file must contain variable names in tab-delimited columns!")
                Exit Sub
            Else

                Dim Columns = FileContent(0).Split(vbTab)
                DataFileColumns = New List(Of String)
                For i = 0 To Columns.Length - 1
                    DataFileColumns.Add(Columns(i).Trim)
                Next
            End If

            RawDataFileInputRows = New List(Of String)
            For i = 1 To FileContent.Length - 1
                RawDataFileInputRows.Add(FileContent(i))
            Next

            'Adding column values into the ComboBoxes
            CorrectVideoColumn_ComboBox.Items.Clear()
            TrialStartColumn_ComboBox.Items.Clear()
            TrialEndColumnComboBox.Items.Clear()
            For Each Column In DataFileColumns
                CorrectVideoColumn_ComboBox.Items.Add(Column.Trim)
                TrialStartColumn_ComboBox.Items.Add(Column.Trim)
                TrialEndColumnComboBox.Items.Add(Column.Trim)
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
            DataFile_TextBox.Text = ""
            Exit Sub
        End Try

        DataFile_TextBox.Text = CurrentDataFilePath

    End Sub

    Private Sub SelectCorrectVideosFolder_Button_Click(sender As Object, e As EventArgs) Handles SelectCorrectVideosFolder_Button.Click


        Dim DataFileDialog As New FolderBrowserDialog
        DataFileDialog.Description = "Select the folder containing the correct videos and press OK"

        Dim DialogResult = DataFileDialog.ShowDialog
        If DialogResult = DialogResult.OK Then
            CorrectVideosFolder = DataFileDialog.SelectedPath
        Else
            Exit Sub
        End If

        CorrectVideosFolder_TextBox.Text = CorrectVideosFolder

    End Sub


    Private Sub SelectExperimentVideoFile_Button_Click(sender As Object, e As EventArgs) Handles SelectExperimentVideoFile_Button.Click

        Dim DataFileDialog As New OpenFileDialog
        DataFileDialog.Title = "Select the video file recorded at the experiment"
        DataFileDialog.CheckPathExists = True

        Dim DialogResult = DataFileDialog.ShowDialog
        If DialogResult = DialogResult.OK Then
            CurrentExperimentVideoFilePath = DataFileDialog.FileName
        Else
            Exit Sub
        End If

        If LoadExperimentVideo(CurrentExperimentVideoFilePath) = False Then
            MsgBox("Unable to load the file " & CurrentExperimentVideoFilePath)
            Exit Sub
        End If

        ExperimentVideoFile_TextBox.Text = CurrentExperimentVideoFilePath

    End Sub

    Private Function LoadExperimentVideo(ByVal InputFile As String) As Boolean

        If System.IO.File.Exists(InputFile) = False Then
            MsgBox("The file " & InputFile & " could not be found!", MsgBoxStyle.Exclamation, "File not found!")
            Return False
        End If

        Try

            'Loading the new video
            CurrentExperimentVideo = New Emgu.CV.VideoCapture(InputFile, VideoCapture.API.Ffmpeg)

            CurrentExperimentVideoFrameRate = CurrentExperimentVideo.Get(CapProp.Fps)
            CurrentExperimentVideoLength = CurrentExperimentVideo.Get(CapProp.FrameCount)
            CurrentExperimentVideoWidth = CurrentExperimentVideo.Get(CapProp.FrameWidth)
            CurrentExperimentVideoHeight = CurrentExperimentVideo.Get(CapProp.FrameHeight)

            CurrentExperimentVideoFrameInterval = GetFrameInterval(CurrentExperimentVideoFrameRate)

            ExperimentVideoFrameTimer.Interval = Math.Max(1, Math.Round(PlaySpeed * 1000 / (CurrentExperimentVideoFrameRate / CurrentExperimentVideoFrameInterval)))


        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

        Return True

    End Function

    Public Function GetFrameInterval(ByVal FrameRate As Double) As Integer

        If FrameRate > 80 Then
            Return 4
        ElseIf FrameRate > 60 Then
            Return 3
        ElseIf FrameRate > 40 Then
            Return 2
        Else
            Return 1
        End If

    End Function

    Private Sub LockSettings_Button_Click(sender As Object, e As EventArgs) Handles LockSettings_Button.Click

        'Checking stuff
        If CorrectVideoColumn_ComboBox.SelectedItem Is Nothing Then
            MsgBox("You must select the column that holds the correct video paths", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If
        If TrialStartColumn_ComboBox.SelectedItem Is Nothing Then
            MsgBox("You must select the column that holds the trial start times (in the experiment video)", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If
        If TrialEndColumnComboBox.SelectedItem Is Nothing Then
            MsgBox("You must select the column that holds the trial end times (in the experiment video)", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If
        If CorrectVideosFolder = "" Then
            MsgBox("You must indicate the folder in which the correct videos are stored", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If
        If CurrentExperimentVideo Is Nothing Then
            MsgBox("No experiment video, containing the filmed trials, has been loaded", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If

        'Attemtping to create trials
        CurrentVideoTrialSet = New VideoTrialSet

        For i = 0 To RawDataFileInputRows.Count - 1

            Dim DataRow = RawDataFileInputRows(i)

            Dim Columns = DataRow.Split(vbTab)
            Dim CorrectVideo As String = Columns(CorrectVideoColumn_ComboBox.SelectedIndex)

            Dim TrialVideoStartTime As Double = -1
            Dim ParsedValue1 As Double
            If Double.TryParse(Columns(TrialStartColumn_ComboBox.SelectedIndex).Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, ParsedValue1) = True Then TrialVideoStartTime = ParsedValue1

            Dim TrialVideoEndTime As Double = -1
            Dim ParsedValue2 As Double
            If Double.TryParse(Columns(TrialEndColumnComboBox.SelectedIndex).Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, ParsedValue2) = True Then TrialVideoEndTime = ParsedValue2

            Dim ShouldBeRated As Boolean = True
            If CorrectVideo = "" Then ShouldBeRated = False
            If TrialVideoStartTime = -1 Then ShouldBeRated = False
            If TrialVideoEndTime = -1 Then ShouldBeRated = False

            If CorrectVideo.Trim <> "" Then
                'Trimming off everything but the file name, and adds the user supplied CorrectVideosFolder
                CorrectVideo = IO.Path.Join(CorrectVideosFolder, IO.Path.GetFileName(CorrectVideo))
            End If

            CurrentVideoTrialSet.TrialList.Add(New VideoTrial(ScoringType_ComboBox.SelectedItem, ShouldBeRated, DataRow, CorrectVideo, TrialVideoStartTime, TrialVideoEndTime, i))

        Next

        'Files loaded ok

        'Disabling the Settings_GroupBox
        Settings_GroupBox.Enabled = False

        'Setting trial order
        CurrentVideoTrialSet.SetTrialOrder(TrialOrder_ComboBox.SelectedItem)

        'Adding trials to be scored into the Trials_ListBox
        Trials_ListBox.Items.Clear()
        For i = 0 To CurrentVideoTrialSet.TrialList.Count - 1
            If CurrentVideoTrialSet.TrialList(i).ShouldBeRated = True Then
                Trials_ListBox.Items.Add(CurrentVideoTrialSet.TrialList(i))
            End If
        Next

        'Enabling the WorkFlow_TableLayoutPanel
        WorkFlow_TableLayoutPanel.Enabled = True

        'Shows the first trial
        If Trials_ListBox.Items.Count = 0 Then
            MsgBox("No trials to show!", MsgBoxStyle.Information, "Video Score Tool")
            Exit Sub
        Else
            Trials_ListBox.SelectedIndex = 0
        End If

    End Sub

    ' Scoring

    Private Sub ShowNewTrial(ByVal Trial As VideoTrial)

        'Loading correct video
        If LoadCorrectVideo(Trial.CorrectVideoPath) = False Then
            MsgBox("Unable to load trial video: " & Trial.CorrectVideoPath, MsgBoxStyle.Exclamation, "Error!")
            Exit Sub
        End If

        CurrentCorrectVideoStartFrame = 0
        CurrentCorrectVideoEndFrame = Math.Max(0, CurrentCorrectVideoLength - 1)

        CorrectVideo_TrackBar.Minimum = 0
        CorrectVideo_TrackBar.Maximum = CurrentCorrectVideoEndFrame

        CurrentExperimentVideoStartFrame = Math.Min(CurrentExperimentVideoLength - 1, Math.Max(0, Math.Floor(Trial.TrialVideoStartTime * CurrentExperimentVideoFrameRate)))
        CurrentExperimentVideoEndFrame = Math.Min(CurrentExperimentVideoLength - 1, Math.Max(0, Math.Ceiling(Trial.TrialVideoEndTime * CurrentExperimentVideoFrameRate)))

        'Warns if time is outside of video length
        If CurrentExperimentVideoLength < Math.Floor(Trial.TrialVideoStartTime * CurrentExperimentVideoFrameRate) Then
            MsgBox("The specified start time is beyond the duration of the experiment video. No trial video will be shown!", MsgBoxStyle.Information, "Missing trial video")
        End If

        ExperimentVideo_TrackBar.Minimum = 0
        ExperimentVideo_TrackBar.Maximum = Math.Max(0, CurrentExperimentVideoEndFrame - CurrentExperimentVideoStartFrame)

        'Adding question
        ScoringPanel.Controls.Clear()
        ScoringPanel.AddQuestion(Trial)

        CorrectVideo_PlayButton.Enabled = True
        ExperimentVideo_PlayButton.Enabled = True

        If CorrectVideoAutoplay_CheckBox.Checked = True Then
            PlayCorrectVideo()
        Else
            If ExperimentVideoAutoplay_CheckBox.Checked = True Then
                PlayExperimentVideo()
            End If
        End If

    End Sub

    Private Function LoadCorrectVideo(ByVal InputFile As String) As Boolean

        If System.IO.File.Exists(InputFile) = False Then
            MsgBox("The file " & InputFile & " could not be found!", MsgBoxStyle.Exclamation, "File not found!")
            Return False
        End If

        Try

            'Loading the new video
            CurrentCorrectVideo = New Emgu.CV.VideoCapture(InputFile, VideoCapture.API.Ffmpeg)

            CurrentCorrectVideoFrameRate = CurrentCorrectVideo.Get(CapProp.Fps)
            CurrentCorrectVideoLength = CurrentCorrectVideo.Get(CapProp.FrameCount)
            CurrentCorrectVideoWidth = CurrentCorrectVideo.Get(CapProp.FrameWidth)
            CurrentCorrectVideoHeight = CurrentCorrectVideo.Get(CapProp.FrameHeight)

            CurrentCorrectVideoFrameInterval = GetFrameInterval(CurrentCorrectVideoFrameRate)

            CorrectVideoFrameTimer.Interval = Math.Max(1, Math.Round(PlaySpeed * 1000 / (CurrentCorrectVideoFrameRate / CurrentCorrectVideoFrameInterval)))

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Error")
            Return False
        End Try

        Return True

    End Function


    'Play
    Private Sub CorrectVideo_TrackBar_ValueChanged(sender As Object, e As EventArgs) Handles CorrectVideo_TrackBar.ValueChanged

        If CurrentCorrectVideo IsNot Nothing Then

            'Getting the index of the frame to display
            If CorrectVideo_TrackBar.Value < CurrentCorrectVideo.Get(CapProp.FrameCount) Then
                CurrentCorrectVideo.Set(CapProp.PosFrames, CorrectVideo_TrackBar.Value)
            Else
                Exit Sub
            End If

            ViewNextCorrectVideoFrame()

        End If

    End Sub

    Private Sub ViewNextCorrectVideoFrame()

        Dim CurrentPosition = CurrentCorrectVideo.Get(CapProp.PosFrames)
        If CurrentPosition Mod CurrentCorrectVideoFrameInterval <> 0 Then
            CurrentCorrectVideo.Set(CapProp.PosFrames, CurrentPosition + (CurrentCorrectVideoFrameInterval - 1))
        End If
        CurrentPosition = CurrentCorrectVideo.Get(CapProp.PosFrames)

        If CurrentPosition < CurrentCorrectVideo.Get(CapProp.FrameCount) Then

            Dim CurrentImage As Image(Of Bgr, Byte)

            If CorrectVideo_ImageBox.Image Is Nothing Then
                CurrentImage = New Image(Of Bgr, Byte)(CurrentCorrectVideo.Width, CurrentCorrectVideo.Height)
            Else
                CurrentImage = CorrectVideo_ImageBox.Image
            End If

            CurrentCorrectVideo.Read(CurrentImage)
            CorrectVideo_ImageBox.Image = CurrentImage
            CorrectVideo_ImageBox.Update()

        End If

    End Sub

    Private Sub ExperimentVideo_TrackBar_ValueChanged(sender As Object, e As EventArgs) Handles ExperimentVideo_TrackBar.ValueChanged

        If CurrentExperimentVideo IsNot Nothing Then

            'Getting the index of the frame to display
            If ExperimentVideo_TrackBar.Value < CurrentExperimentVideo.Get(CapProp.FrameCount) Then
                CurrentExperimentVideo.Set(CapProp.PosFrames, ExperimentVideo_TrackBar.Value)
            Else
                Exit Sub
            End If

            ViewNextExperimentVideoFrame()

        End If

    End Sub

    Private Sub ViewNextExperimentVideoFrame()

        Dim CurrentPosition = CurrentExperimentVideo.Get(CapProp.PosFrames)
        If CurrentPosition Mod CurrentExperimentVideoFrameInterval <> 0 Then
            CurrentExperimentVideo.Set(CapProp.PosFrames, CurrentPosition + (CurrentExperimentVideoFrameInterval - 1))
        End If
        CurrentPosition = CurrentExperimentVideo.Get(CapProp.PosFrames)

        If CurrentPosition < CurrentExperimentVideo.Get(CapProp.FrameCount) Then

            Dim CurrentImage As Image(Of Bgr, Byte)

            If ExperimentVideo_ImageBox.Image Is Nothing Then
                CurrentImage = New Image(Of Bgr, Byte)(CurrentExperimentVideo.Width, CurrentExperimentVideo.Height)
            Else
                CurrentImage = ExperimentVideo_ImageBox.Image
            End If

            CurrentExperimentVideo.Read(CurrentImage)
            ExperimentVideo_ImageBox.Image = CurrentImage
            ExperimentVideo_ImageBox.Update()

        End If
    End Sub

    Private Sub Next_Button_Click(sender As Object, e As EventArgs) Handles Next_Button.Click

        If Trials_ListBox.SelectedIndex = Trials_ListBox.Items.Count - 1 Then
            MsgBox("You allready display the last trial!", MsgBoxStyle.Information, "Video Score Tool")
            Exit Sub
        Else
            Trials_ListBox.SelectedIndex += 1
        End If

    End Sub

    Private Sub Previous_Button_Click(sender As Object, e As EventArgs) Handles Previous_Button.Click

        If Trials_ListBox.SelectedIndex = 0 Then
            MsgBox("You allready display the first trial!", MsgBoxStyle.Information, "Video Score Tool")
            Exit Sub
        Else
            Trials_ListBox.SelectedIndex -= 1
        End If

    End Sub

    Private Sub FirstUnscored_Button_Click(sender As Object, e As EventArgs) Handles FirstUnscored_Button.Click

        'Selecting the trial in the Trials_ListBox
        For i = 0 To Trials_ListBox.Items.Count - 1
            Dim CastItem = TryCast(Trials_ListBox.Items(i), ControlsLibrary.VideoTrial)
            If CastItem IsNot Nothing Then
                If CastItem.ShouldBeRated = True Then
                    If CastItem.IsRated = False Then
                        Trials_ListBox.SelectedIndex = i
                        Exit Sub
                    End If
                End If
            End If
        Next

        MsgBox("No more unscored trials to show", MsgBoxStyle.Information, "Video Score Tool")

    End Sub

    Private Sub CorrectVideo_PlayButton_Click(sender As Object, e As EventArgs) Handles CorrectVideo_PlayButton.Click
        If CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play Then
            PlayCorrectVideo()
        Else
            CorrectVideoFrameTimer.Stop()
            CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play
        End If
    End Sub

    Private Sub PlayCorrectVideo()
        CorrectVideoFrameTimer.Stop()

        CorrectVideo_ImageBox.Image = Nothing
        CurrentCorrectVideoStartFrame = 0
        CurrentCorrectVideoEndFrame = Math.Max(0, CurrentCorrectVideoLength - 1)
        CurrentCorrectVideo.Set(CapProp.PosFrames, CurrentCorrectVideoStartFrame)
        CorrectVideoFrameTimer.Start()

        CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Stop

    End Sub

    Private Sub ExperimentVideo_PlayButton_Click(sender As Object, e As EventArgs) Handles ExperimentVideo_PlayButton.Click
        ExperimentVideoFrameTimer.Stop()
        If ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play Then
            PlayExperimentVideo()
        Else
            ExperimentVideoFrameTimer.Stop()
            ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play
        End If
    End Sub

    Private Sub PlayExperimentVideo()
        ExperimentVideoFrameTimer.Stop()

        ExperimentVideo_ImageBox.Image = Nothing
        CurrentExperimentVideo.Set(CapProp.PosFrames, CurrentExperimentVideoStartFrame)
        ExperimentVideoFrameTimer.Start()

        ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Stop

    End Sub

    Private Sub CorrectVideoFrameTimer_Tick() Handles CorrectVideoFrameTimer.Tick

        If CurrentCorrectVideo.Get(CapProp.PosFrames) >= CurrentCorrectVideoEndFrame Then
            'Stopping timer
            CorrectVideoFrameTimer.Stop()
            CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play

            'Starting expeiment video if its CheckBox is ticked
            If ExperimentVideoAutoplay_CheckBox.Checked = True Then
                PlayExperimentVideo()
            End If

        Else

            ViewNextCorrectVideoFrame()

            'Showing next frame
            'CorrectVideo_TrackBar.Value += 1
        End If

    End Sub


    Private Sub ExperimentVideoFrameTimer_Tick() Handles ExperimentVideoFrameTimer.Tick

        If CurrentExperimentVideo.Get(CapProp.PosFrames) >= CurrentExperimentVideoEndFrame Then
            'Stopping timer
            ExperimentVideoFrameTimer.Stop()
            ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play
        Else

            ViewNextExperimentVideoFrame()

            'Showing next frame
            'ExperimentVideo_TrackBar.Value += 1
        End If

    End Sub

    Private Sub Trials_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Trials_ListBox.SelectedIndexChanged

        Dim NewTrial = TryCast(Trials_ListBox.SelectedItem, ControlsLibrary.VideoTrial)
        If NewTrial IsNot Nothing Then
            ShowNewTrial(NewTrial)
        End If

    End Sub
End Class