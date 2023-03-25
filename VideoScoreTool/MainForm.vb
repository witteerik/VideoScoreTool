Imports System.Globalization
Imports ControlsLibrary
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.CV.UI

Public Class MainForm

    Private CurrentDataFilePath As String = ""
    Private CurrentExportFilePath As String = ""
    Private DataFileColumns As List(Of String)
    Private RawDataFileInputRows As List(Of String)
    Private CurrentExperimentVideoFilePath As String = ""
    Private CurrentVideoTrialSet As VideoTrialSet

    Private CorrectVideosFolder As String = ""

    Private WithEvents CurrentExperimentVideo As VideoCapture = Nothing
    Private CurrentExperimentVideoFrameRate As Double
    Private CurrentExperimentVideoFrameInterval As Integer = 1
    Private CurrentExperimentVideoLength As Integer
    Private CurrentExperimentVideoStartFrame As Integer = -1
    Private CurrentExperimentVideoEndFrame As Integer = -1
    Private WithEvents ExperimentVideoFrameTimer As New Windows.Forms.Timer

    Private ExpVideoSyncTime As Double

    Private WithEvents CurrentCorrectVideo As VideoCapture = Nothing
    Private CurrentCorrectVideoFrameRate As Double
    Private CurrentCorrectVideoFrameInterval As Integer = 1
    Private CurrentCorrectVideoLength As Integer
    Private CurrentCorrectVideoStartFrame As Integer = -1
    Private CurrentCorrectVideoEndFrame As Integer = -1
    Private WithEvents CorrectVideoFrameTimer As New Windows.Forms.Timer

    Private FrameIntervalFactor As Double = 0.9

    Private WithEvents ScoringPanel As New ControlsLibrary.ScoringQuestionPanel
    Private SyncTime_TextBox As ControlsLibrary.DoubleParsingTextBox

    Private SwappingTrials As Boolean = False

    Private Enum NextViewSenderTypes
        Timer
        TrackBar
        TrialLauncher
    End Enum


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
        ScoringPanel = New ControlsLibrary.ScoringQuestionPanel
        ScoringPanel.Dock = DockStyle.Fill
        ScoringPanelHolder_Panel.Controls.Add(ScoringPanel)

        SyncTime_TextBox = New DoubleParsingTextBox
        SyncTime_TextBox.Dock = DockStyle.Fill
        SyncTimeInputBoxHolder_Panel.Controls.Add(SyncTime_TextBox)

        For Value As Double = 15.0R To -15.0R Step -1.0R
            AdjustStart_ComboBox.Items.Add(Value)
            AdjustEnd_ComboBox.Items.Add(Value)
        Next
        AdjustStart_ComboBox.SelectedIndex = 15
        AdjustEnd_ComboBox.SelectedIndex = 15


    End Sub

    Private Sub SelectDataFile_Button_Click(sender As Object, e As EventArgs) Handles SelectDataFile_Button.Click

        Dim DataFileDialog As New OpenFileDialog
        DataFileDialog.Title = "Select a tab delimited (.csv) experiment file"
        DataFileDialog.Filter = "CSV file (.csv)|*.csv"
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
        DataFileDialog.UseDescriptionForTitle = True

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

        If SyncTime_TextBox.Value Is Nothing Then
            MsgBox("You must supply a sync time! This is the duration in seconds into the experiment video film until the syncronization was done (for example a filmed click on a syncronization button in the testing software used). ", MsgBoxStyle.Exclamation, "Add additional info")
            Exit Sub
        End If

        'Storing the sync time
        ExpVideoSyncTime = SyncTime_TextBox.Value

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

            Dim ShouldBeScored As Boolean = True
            If CorrectVideo = "" Then ShouldBeScored = False
            If TrialVideoStartTime = -1 Then ShouldBeScored = False
            If TrialVideoEndTime = -1 Then ShouldBeScored = False

            If CorrectVideo.Trim <> "" Then
                'Trimming off everything but the file name, and adds the user supplied CorrectVideosFolder
                CorrectVideo = IO.Path.Join(CorrectVideosFolder, IO.Path.GetFileName(CorrectVideo))
            End If

            CurrentVideoTrialSet.TrialList.Add(New VideoTrial(ScoringType_ComboBox.SelectedItem, ShouldBeScored, DataRow, CorrectVideo, TrialVideoStartTime, TrialVideoEndTime, i))

        Next

        'Files loaded ok

        'Disabling the Settings_GroupBox
        Settings_GroupBox.Enabled = False

        'Setting trial order (the default id to first randomize trials, and then set them in aphabetic order, which will then be randomized within trials with the same stimuli)
        CurrentVideoTrialSet.SetTrialOrder(ControlsLibrary.VideoTrialSet.Trialorders.Random)
        CurrentVideoTrialSet.SetTrialOrder(ControlsLibrary.VideoTrialSet.Trialorders.Alphabetic)

        'Adding trials to be scored into the Trials_ListBox
        Trials_ListBox.Items.Clear()
        For i = 0 To CurrentVideoTrialSet.TrialList.Count - 1
            If CurrentVideoTrialSet.TrialList(i).ShouldBeScored = True Then
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



    'Video loading

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
            CurrentExperimentVideoFrameInterval = GetFrameInterval(CurrentExperimentVideoFrameRate)

            ExperimentVideoFrameTimer.Interval = Math.Max(1, Math.Round(FrameIntervalFactor * 1000 / (CurrentExperimentVideoFrameRate / CurrentExperimentVideoFrameInterval)))


        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

        Return True

    End Function

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
            CurrentCorrectVideoFrameInterval = GetFrameInterval(CurrentCorrectVideoFrameRate)

            CorrectVideoFrameTimer.Interval = Math.Max(1, Math.Round(FrameIntervalFactor * 1000 / (CurrentCorrectVideoFrameRate / CurrentCorrectVideoFrameInterval)))

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Error")
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

    ' Scoring and trial swapping 

    'Shifting randomization order
    Private Sub TrialOrder_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TrialOrder_ComboBox.SelectedIndexChanged

        'UpdatingListBoxItems = True

        If CurrentVideoTrialSet Is Nothing Then Exit Sub

        Trials_ListBox.Items.Clear()

        CurrentVideoTrialSet.SetTrialOrder(TrialOrder_ComboBox.SelectedItem)

        'Adding trials to be scored into the Trials_ListBox
        For i = 0 To CurrentVideoTrialSet.TrialList.Count - 1
            If CurrentVideoTrialSet.TrialList(i).ShouldBeScored = True Then
                Trials_ListBox.Items.Add(CurrentVideoTrialSet.TrialList(i))
            End If
        Next

        'UpdatingListBoxItems = False

        If Trials_ListBox.Items.Count > 0 Then Trials_ListBox.SelectedIndex = 0

    End Sub

    Private Sub AdjustTime_ComboBoxs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AdjustStart_ComboBox.SelectedIndexChanged, AdjustEnd_ComboBox.SelectedIndexChanged

        'Reloads the trial by selecting the already selected trial in the Trials_ListBox
        Dim CurrentIndex = Trials_ListBox.SelectedIndex
        If Trials_ListBox.Items.Count > 0 Then
            Trials_ListBox.SelectedIndex = -1
            Trials_ListBox.SelectedIndex = CurrentIndex
        End If

    End Sub


    Dim UpdatingListBoxItems As Boolean = False

    Private Sub Trials_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Trials_ListBox.SelectedIndexChanged

        If Trials_ListBox.SelectedIndex = -1 Then Exit Sub

        If UpdatingListBoxItems = True Then Exit Sub

        'gets the selected item
        Dim NewTrial = TryCast(Trials_ListBox.SelectedItem, ControlsLibrary.VideoTrial)

        UpdatingListBoxItems = True

        'Updating items (to get the correct ToString() representation) (See https://stackoverflow.com/questions/33175381/how-we-can-refresh-items-text-in-listbox-without-reinserting-it for this smart idea!
        For i = 0 To Trials_ListBox.Items.Count - 1
            Trials_ListBox.Items(i) = Trials_ListBox.Items(i)
        Next
        UpdatingListBoxItems = False

        'Showing the new item
        If NewTrial IsNot Nothing Then
            ShowNewTrial(NewTrial)
        End If

    End Sub


    Private Sub ShowNewTrial(ByVal Trial As VideoTrial)

        'Stopping timers and resetting play buttons
        CorrectVideoFrameTimer.Stop()
        ExperimentVideoFrameTimer.Stop()
        CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play
        ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play

        SwappingTrials = True

        'Clearing the images
        CorrectVideo_ImageBox.Image = Nothing
        ExperimentVideo_ImageBox.Image = Nothing
        CorrectVideo_ImageBox.Update()
        ExperimentVideo_ImageBox.Update()

        'Loading correct video
        If LoadCorrectVideo(Trial.CorrectVideoPath) = False Then
            MsgBox("Unable to load trial video: " & Trial.CorrectVideoPath, MsgBoxStyle.Exclamation, "Error!")
            Exit Sub
        End If

        CurrentCorrectVideoStartFrame = 0
        CurrentCorrectVideoEndFrame = Math.Max(0, CurrentCorrectVideoLength - 1)

        CorrectVideo_TrackBar.Minimum = 0
        CorrectVideo_TrackBar.Maximum = CurrentCorrectVideoEndFrame

        CurrentExperimentVideoStartFrame = Math.Min(CurrentExperimentVideoLength - 1, Math.Max(0, Math.Floor((Trial.TrialVideoStartTime + ExpVideoSyncTime + AdjustStart_ComboBox.SelectedItem) * CurrentExperimentVideoFrameRate)))
        CurrentExperimentVideoEndFrame = Math.Min(CurrentExperimentVideoLength - 1, Math.Max(0, Math.Ceiling((Trial.TrialVideoEndTime + ExpVideoSyncTime + AdjustEnd_ComboBox.SelectedItem) * CurrentExperimentVideoFrameRate)))

        'Warns if time is outside of video length
        If CurrentExperimentVideoLength < Math.Floor(Trial.TrialVideoStartTime * CurrentExperimentVideoFrameRate) Then
            MsgBox("The specified start time is beyond the duration of the experiment video. No trial video will be shown!", MsgBoxStyle.Information, "Missing trial video")
        End If

        ExperimentVideo_TrackBar.Minimum = 0
        ExperimentVideo_TrackBar.Maximum = Math.Max(0, CurrentExperimentVideoEndFrame - CurrentExperimentVideoStartFrame)

        'Adding question
        ScoringPanel.Controls.Clear()
        ScoringPanel.AddQuestion(Trial.Question)
        ScoringPanel.ResizeNow()

        SwappingTrials = False

        Try
            CurrentCorrectVideo.Set(CapProp.PosFrames, 0)
            CurrentExperimentVideo.Set(CapProp.PosFrames, CurrentExperimentVideoStartFrame)
        Catch ex As Exception
            'ignoring any error here
        End Try

        CorrectVideo_TrackBar.Value = 0
        ExperimentVideo_TrackBar.Value = 0

        ViewNextCorrectVideoFrame(NextViewSenderTypes.TrialLauncher)
        ViewNextExperimentVideoFrame(NextViewSenderTypes.TrialLauncher)

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


    Private Sub Next_Button_Click(sender As Object, e As EventArgs) Handles Next_Button.Click

        If SwappingTrials = True Then Exit Sub

        If Trials_ListBox.SelectedIndex = Trials_ListBox.Items.Count - 1 Then
            MsgBox("You allready display the last trial!", MsgBoxStyle.Information, "Video Score Tool")
            Exit Sub
        Else
            Trials_ListBox.SelectedIndex += 1
        End If

    End Sub

    Private Sub Previous_Button_Click(sender As Object, e As EventArgs) Handles Previous_Button.Click

        If SwappingTrials = True Then Exit Sub

        If Trials_ListBox.SelectedIndex = 0 Then
            MsgBox("You allready display the first trial!", MsgBoxStyle.Information, "Video Score Tool")
            Exit Sub
        Else
            Trials_ListBox.SelectedIndex -= 1
        End If

    End Sub

    Private Sub FirstUnscored_Button_Click(sender As Object, e As EventArgs) Handles FirstUnscored_Button.Click

        If SwappingTrials = True Then Exit Sub

        'Selecting the trial in the Trials_ListBox
        For i = 0 To Trials_ListBox.Items.Count - 1
            Dim CastItem = TryCast(Trials_ListBox.Items(i), ControlsLibrary.VideoTrial)
            If CastItem IsNot Nothing Then
                If CastItem.ShouldBeScored = True Then
                    If CastItem.IsScored = False Then
                        Trials_ListBox.SelectedIndex = i
                        Exit Sub
                    End If
                End If
            End If
        Next

        MsgBox("No more unscored trials to show", MsgBoxStyle.Information, "Video Score Tool")

    End Sub



    'Video viewing and playback

    Private Sub CorrectVideo_TrackBar_ValueChanged(sender As Object, e As EventArgs) Handles CorrectVideo_TrackBar.ValueChanged

        If SwappingTrials = True Then Exit Sub
        If CurrentCorrectVideo Is Nothing Then Exit Sub
        If CorrectVideoFrameTimer.Enabled = True Then Exit Sub

        If CurrentCorrectVideo IsNot Nothing Then

            'Getting the index of the frame to display
            If CorrectVideo_TrackBar.Value < CurrentCorrectVideo.Get(CapProp.FrameCount) Then
                CurrentCorrectVideo.Set(CapProp.PosFrames, CorrectVideo_TrackBar.Value)
            Else
                Exit Sub
            End If

            ViewNextCorrectVideoFrame(NextViewSenderTypes.TrackBar)

        End If

    End Sub

    Private Sub ViewNextCorrectVideoFrame(ByVal SenderType As NextViewSenderTypes)

        If SwappingTrials = True Then Exit Sub

        Dim CurrentPosition = CurrentCorrectVideo.Get(CapProp.PosFrames)
        If SenderType = NextViewSenderTypes.Timer Then
            If CurrentPosition Mod CurrentCorrectVideoFrameInterval <> 0 Then
                CurrentCorrectVideo.Set(CapProp.PosFrames, CurrentPosition + (CurrentCorrectVideoFrameInterval - 1))
            End If
            CurrentPosition = CurrentCorrectVideo.Get(CapProp.PosFrames)
        End If

        If CurrentPosition < CurrentCorrectVideo.Get(CapProp.FrameCount) - 1 Then

            Dim CurrentImage As Image(Of Bgr, Byte)

            If CorrectVideo_ImageBox.Image Is Nothing Then
                CurrentImage = New Image(Of Bgr, Byte)(CurrentCorrectVideo.Width, CurrentCorrectVideo.Height)
            Else
                CurrentImage = CorrectVideo_ImageBox.Image
            End If

            CurrentCorrectVideo.Read(CurrentImage)
            CorrectVideo_ImageBox.Image = CurrentImage
            CorrectVideo_ImageBox.Update()

            CorrectVideo_TrackBar.Value = CurrentPosition

        Else

            CorrectVideo_ImageBox.Image = Nothing
            CorrectVideo_ImageBox.Update()

        End If

    End Sub

    Private Sub ExperimentVideo_TrackBar_ValueChanged(sender As Object, e As EventArgs) Handles ExperimentVideo_TrackBar.ValueChanged

        If SwappingTrials = True Then Exit Sub
        If CurrentExperimentVideo Is Nothing Then Exit Sub
        If ExperimentVideoFrameTimer.Enabled = True Then Exit Sub

        If CurrentExperimentVideo IsNot Nothing Then

            Dim VideoPositionToSet = ExperimentVideo_TrackBar.Value + CurrentExperimentVideoStartFrame

            'Getting the index of the frame to display
            If VideoPositionToSet < CurrentExperimentVideo.Get(CapProp.FrameCount) Then
                CurrentExperimentVideo.Set(CapProp.PosFrames, VideoPositionToSet)
            Else
                Exit Sub
            End If

            ViewNextExperimentVideoFrame(NextViewSenderTypes.TrackBar)

        End If

    End Sub


    Private Sub ViewNextExperimentVideoFrame(ByVal SenderType As NextViewSenderTypes)

        If SwappingTrials = True Then Exit Sub
        Dim CurrentVideoPosition = CurrentExperimentVideo.Get(CapProp.PosFrames)
        If SenderType = NextViewSenderTypes.Timer Then
            If CurrentVideoPosition Mod CurrentExperimentVideoFrameInterval <> 0 Then
                CurrentExperimentVideo.Set(CapProp.PosFrames, CurrentVideoPosition + (CurrentExperimentVideoFrameInterval - 1))
            End If
            CurrentVideoPosition = CurrentExperimentVideo.Get(CapProp.PosFrames)
        End If

        If CurrentVideoPosition < CurrentExperimentVideo.Get(CapProp.FrameCount) - 1 Then

            Dim CurrentImage As Image(Of Bgr, Byte)

            If ExperimentVideo_ImageBox.Image Is Nothing Then
                CurrentImage = New Image(Of Bgr, Byte)(CurrentExperimentVideo.Width, CurrentExperimentVideo.Height)
            Else
                CurrentImage = ExperimentVideo_ImageBox.Image
            End If

            CurrentExperimentVideo.Read(CurrentImage)
            ExperimentVideo_ImageBox.Image = CurrentImage
            ExperimentVideo_ImageBox.Update()

            Dim TrackBarPositionToSet = CurrentVideoPosition - CurrentExperimentVideoStartFrame

            ExperimentVideo_TrackBar.Value = Math.Min(Math.Max(0, TrackBarPositionToSet), ExperimentVideo_TrackBar.Maximum)

        Else
            ExperimentVideo_ImageBox.Image = Nothing
            ExperimentVideo_ImageBox.Update()
        End If
    End Sub

    Private Sub CorrectVideo_PlayButton_Click(sender As Object, e As EventArgs) Handles CorrectVideo_PlayButton.Click

        If SwappingTrials = True Then Exit Sub

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

        If SwappingTrials = True Then Exit Sub

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

        If SwappingTrials = True Then Exit Sub

        If CurrentCorrectVideo.Get(CapProp.PosFrames) >= CurrentCorrectVideoEndFrame Then
            'Stopping timer
            CorrectVideoFrameTimer.Stop()
            CorrectVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play

            'Starting experiment video if its CheckBox is ticked
            If ExperimentVideoAutoplay_CheckBox.Checked = True Then
                PlayExperimentVideo()
            End If

        Else

            ViewNextCorrectVideoFrame(NextViewSenderTypes.Timer)

            'Showing next frame
            'CorrectVideo_TrackBar.Value += 1
        End If

    End Sub


    Private Sub ExperimentVideoFrameTimer_Tick() Handles ExperimentVideoFrameTimer.Tick

        If SwappingTrials = True Then Exit Sub

        If CurrentExperimentVideo.Get(CapProp.PosFrames) >= CurrentExperimentVideoEndFrame Then
            'Stopping timer
            ExperimentVideoFrameTimer.Stop()
            ExperimentVideo_PlayButton.ViewMode = PlayButton.ViewModes.Play
        Else

            ViewNextExperimentVideoFrame(NextViewSenderTypes.Timer)

            'Showing next frame
            'ExperimentVideo_TrackBar.Value += 1
        End If

    End Sub

    ' Saving
    Private Sub Save_Button_Click(sender As Object, e As EventArgs) Handles Save_Button.Click

        If CurrentExportFilePath = "" Then
            CurrentVideoTrialSet.SaveAs(CurrentDataFilePath, DataFileColumns, CurrentExportFilePath)
        Else
            CurrentVideoTrialSet.SaveResults(CurrentExportFilePath, DataFileColumns)
        End If

    End Sub

    Private Sub SaveAs_Button_Click(sender As Object, e As EventArgs) Handles SaveAs_Button.Click

        CurrentVideoTrialSet.SaveAs(CurrentDataFilePath, DataFileColumns, CurrentExportFilePath)

    End Sub

    Private Sub NewDataFile_Button_Click(sender As Object, e As EventArgs) Handles NewDataFile_Button.Click

        Dim Result = MsgBox("Make sure you have saved all data before continuing! Do you want to continue?", MsgBoxStyle.YesNo, "All non-saved data will be lost!")
        If Result = MsgBoxResult.Yes Then

            'Resetting things

            CorrectVideoFrameTimer.Stop()
            ExperimentVideoFrameTimer.Stop()

            WorkFlow_TableLayoutPanel.Enabled = False
            CurrentDataFilePath = ""
            CurrentExportFilePath = ""
            DataFileColumns = New List(Of String)
            RawDataFileInputRows = New List(Of String)
            CurrentExperimentVideoFilePath = ""

            DataFile_TextBox.Text = ""
            CorrectVideosFolder_TextBox.Text = ""
            ExperimentVideoFile_TextBox.Text = ""

            CorrectVideoColumn_ComboBox.SelectedItem = Nothing
            TrialStartColumn_ComboBox.SelectedItem = Nothing
            TrialEndColumnComboBox.SelectedItem = Nothing

            CorrectVideoColumn_ComboBox.Items.Clear()
            TrialStartColumn_ComboBox.Items.Clear()
            TrialEndColumnComboBox.Items.Clear()

            CorrectVideo_ImageBox.Image = Nothing
            ExperimentVideo_ImageBox.Image = Nothing
            CorrectVideo_ImageBox.Update()
            ExperimentVideo_ImageBox.Update()

            Trials_ListBox.Items.Clear()
            ScoringPanel.Controls.Clear()

            CurrentExperimentVideo = Nothing
            CurrentCorrectVideo = Nothing

            CorrectVideo_TrackBar.Maximum = 10
            ExperimentVideo_TrackBar.Maximum = 10

            CorrectVideo_TrackBar.Value = 0
            ExperimentVideo_TrackBar.Value = 0

            CurrentVideoTrialSet = Nothing
            Settings_GroupBox.Enabled = True

        End If

    End Sub

    'Resizing

    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        If ScoringPanel IsNot Nothing Then
            ScoringPanel.ResizeNow()
        End If

    End Sub

    'Info

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub


End Class