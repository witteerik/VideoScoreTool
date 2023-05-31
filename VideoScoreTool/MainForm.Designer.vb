<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.WorkFlow_TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.NewDataFile_Button = New System.Windows.Forms.Button()
        Me.Save_Button = New System.Windows.Forms.Button()
        Me.SaveAs_Button = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Video1_TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.CorrectVideo_ImageBox = New Emgu.CV.UI.ImageBox()
        Me.CorrectVideo_PlayButton = New ControlsLibrary.PlayButton()
        Me.CorrectVideo_TrackBar = New System.Windows.Forms.TrackBar()
        Me.CorrectVideoAutoplay_CheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Trials_ListBox = New System.Windows.Forms.ListBox()
        Me.TrialOrder_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.AdjustEnd_ComboBox = New System.Windows.Forms.ComboBox()
        Me.ExperimentVideo_ImageBox = New Emgu.CV.UI.ImageBox()
        Me.ExperimentVideo_PlayButton = New ControlsLibrary.PlayButton()
        Me.ExperimentVideo_TrackBar = New System.Windows.Forms.TrackBar()
        Me.ExperimentVideoAutoplay_CheckBox = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.AdjustStart_ComboBox = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Previous_Button = New System.Windows.Forms.Button()
        Me.FirstUnscored_Button = New System.Windows.Forms.Button()
        Me.Next_Button = New System.Windows.Forms.Button()
        Me.ScoringPanelHolder_Panel = New System.Windows.Forms.Panel()
        Me.Settings_GroupBox = New System.Windows.Forms.GroupBox()
        Me.SyncTimeInputBoxHolder_Panel = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SelectCorrectVideosFolder_Button = New System.Windows.Forms.Button()
        Me.CorrectVideosFolder_TextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SelectExperimentVideoFile_Button = New System.Windows.Forms.Button()
        Me.ExperimentVideoFile_TextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LockSettings_Button = New System.Windows.Forms.Button()
        Me.ScoringType_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TrialEndColumnComboBox = New System.Windows.Forms.ComboBox()
        Me.TrialStartColumn_ComboBox = New System.Windows.Forms.ComboBox()
        Me.CorrectVideoColumn_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SelectDataFile_Button = New System.Windows.Forms.Button()
        Me.DataFile_TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkFlow_TableLayoutPanel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Video1_TableLayoutPanel.SuspendLayout()
        CType(Me.CorrectVideo_ImageBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CorrectVideo_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.ExperimentVideo_ImageBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ExperimentVideo_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Settings_GroupBox.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'WorkFlow_TableLayoutPanel
        '
        Me.WorkFlow_TableLayoutPanel.ColumnCount = 4
        Me.WorkFlow_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.WorkFlow_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.WorkFlow_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.WorkFlow_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 255.0!))
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.GroupBox2, 0, 0)
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.GroupBox5, 1, 0)
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.GroupBox4, 3, 0)
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.GroupBox3, 2, 0)
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.WorkFlow_TableLayoutPanel.Controls.Add(Me.ScoringPanelHolder_Panel, 2, 1)
        Me.WorkFlow_TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkFlow_TableLayoutPanel.Enabled = False
        Me.WorkFlow_TableLayoutPanel.Location = New System.Drawing.Point(3, 143)
        Me.WorkFlow_TableLayoutPanel.Name = "WorkFlow_TableLayoutPanel"
        Me.WorkFlow_TableLayoutPanel.RowCount = 2
        Me.WorkFlow_TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.WorkFlow_TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.WorkFlow_TableLayoutPanel.Size = New System.Drawing.Size(1188, 432)
        Me.WorkFlow_TableLayoutPanel.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.NewDataFile_Button)
        Me.GroupBox2.Controls.Add(Me.Save_Button)
        Me.GroupBox2.Controls.Add(Me.SaveAs_Button)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.WorkFlow_TableLayoutPanel.SetRowSpan(Me.GroupBox2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(114, 426)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Functions"
        '
        'NewDataFile_Button
        '
        Me.NewDataFile_Button.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewDataFile_Button.Location = New System.Drawing.Point(6, 80)
        Me.NewDataFile_Button.Name = "NewDataFile_Button"
        Me.NewDataFile_Button.Size = New System.Drawing.Size(102, 23)
        Me.NewDataFile_Button.TabIndex = 2
        Me.NewDataFile_Button.Text = "New data file"
        Me.NewDataFile_Button.UseVisualStyleBackColor = True
        '
        'Save_Button
        '
        Me.Save_Button.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save_Button.Location = New System.Drawing.Point(6, 51)
        Me.Save_Button.Name = "Save_Button"
        Me.Save_Button.Size = New System.Drawing.Size(102, 23)
        Me.Save_Button.TabIndex = 1
        Me.Save_Button.Text = "&Save"
        Me.Save_Button.UseVisualStyleBackColor = True
        '
        'SaveAs_Button
        '
        Me.SaveAs_Button.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveAs_Button.Location = New System.Drawing.Point(6, 22)
        Me.SaveAs_Button.Name = "SaveAs_Button"
        Me.SaveAs_Button.Size = New System.Drawing.Size(102, 23)
        Me.SaveAs_Button.TabIndex = 0
        Me.SaveAs_Button.Text = "Save &as..."
        Me.SaveAs_Button.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Video1_TableLayoutPanel)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(123, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(400, 320)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Correct video"
        '
        'Video1_TableLayoutPanel
        '
        Me.Video1_TableLayoutPanel.ColumnCount = 2
        Me.Video1_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.11765!))
        Me.Video1_TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.88235!))
        Me.Video1_TableLayoutPanel.Controls.Add(Me.CorrectVideo_ImageBox, 0, 0)
        Me.Video1_TableLayoutPanel.Controls.Add(Me.CorrectVideo_PlayButton, 1, 2)
        Me.Video1_TableLayoutPanel.Controls.Add(Me.CorrectVideo_TrackBar, 0, 1)
        Me.Video1_TableLayoutPanel.Controls.Add(Me.CorrectVideoAutoplay_CheckBox, 0, 2)
        Me.Video1_TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Video1_TableLayoutPanel.Location = New System.Drawing.Point(3, 19)
        Me.Video1_TableLayoutPanel.Name = "Video1_TableLayoutPanel"
        Me.Video1_TableLayoutPanel.RowCount = 3
        Me.Video1_TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Video1_TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.Video1_TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.Video1_TableLayoutPanel.Size = New System.Drawing.Size(394, 298)
        Me.Video1_TableLayoutPanel.TabIndex = 19
        '
        'CorrectVideo_ImageBox
        '
        Me.CorrectVideo_ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Video1_TableLayoutPanel.SetColumnSpan(Me.CorrectVideo_ImageBox, 2)
        Me.CorrectVideo_ImageBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CorrectVideo_ImageBox.Location = New System.Drawing.Point(3, 3)
        Me.CorrectVideo_ImageBox.Name = "CorrectVideo_ImageBox"
        Me.CorrectVideo_ImageBox.Size = New System.Drawing.Size(388, 222)
        Me.CorrectVideo_ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.CorrectVideo_ImageBox.TabIndex = 2
        Me.CorrectVideo_ImageBox.TabStop = False
        '
        'CorrectVideo_PlayButton
        '
        Me.CorrectVideo_PlayButton.Enabled = False
        Me.CorrectVideo_PlayButton.Location = New System.Drawing.Point(90, 266)
        Me.CorrectVideo_PlayButton.Name = "CorrectVideo_PlayButton"
        Me.CorrectVideo_PlayButton.Size = New System.Drawing.Size(44, 28)
        Me.CorrectVideo_PlayButton.TabIndex = 1
        Me.CorrectVideo_PlayButton.UseVisualStyleBackColor = True
        Me.CorrectVideo_PlayButton.ViewMode = ControlsLibrary.PlayButton.ViewModes.Play
        '
        'CorrectVideo_TrackBar
        '
        Me.Video1_TableLayoutPanel.SetColumnSpan(Me.CorrectVideo_TrackBar, 2)
        Me.CorrectVideo_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CorrectVideo_TrackBar.Location = New System.Drawing.Point(3, 231)
        Me.CorrectVideo_TrackBar.Name = "CorrectVideo_TrackBar"
        Me.CorrectVideo_TrackBar.Size = New System.Drawing.Size(388, 29)
        Me.CorrectVideo_TrackBar.TabIndex = 4
        '
        'CorrectVideoAutoplay_CheckBox
        '
        Me.CorrectVideoAutoplay_CheckBox.AutoSize = True
        Me.CorrectVideoAutoplay_CheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CorrectVideoAutoplay_CheckBox.Location = New System.Drawing.Point(10, 266)
        Me.CorrectVideoAutoplay_CheckBox.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CorrectVideoAutoplay_CheckBox.Name = "CorrectVideoAutoplay_CheckBox"
        Me.CorrectVideoAutoplay_CheckBox.Size = New System.Drawing.Size(74, 29)
        Me.CorrectVideoAutoplay_CheckBox.TabIndex = 18
        Me.CorrectVideoAutoplay_CheckBox.Text = "Autoplay"
        Me.CorrectVideoAutoplay_CheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(935, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.WorkFlow_TableLayoutPanel.SetRowSpan(Me.GroupBox4, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(250, 426)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Trials"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Trials_ListBox, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TrialOrder_ComboBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(244, 404)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Trials_ListBox
        '
        Me.Trials_ListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Trials_ListBox.FormattingEnabled = True
        Me.Trials_ListBox.ItemHeight = 15
        Me.Trials_ListBox.Location = New System.Drawing.Point(3, 46)
        Me.Trials_ListBox.Name = "Trials_ListBox"
        Me.Trials_ListBox.Size = New System.Drawing.Size(238, 355)
        Me.Trials_ListBox.TabIndex = 0
        '
        'TrialOrder_ComboBox
        '
        Me.TrialOrder_ComboBox.FormattingEnabled = True
        Me.TrialOrder_ComboBox.Location = New System.Drawing.Point(3, 18)
        Me.TrialOrder_ComboBox.Name = "TrialOrder_ComboBox"
        Me.TrialOrder_ComboBox.Size = New System.Drawing.Size(238, 23)
        Me.TrialOrder_ComboBox.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(182, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Trial order"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(529, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(400, 320)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Trial video"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 6
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.AdjustEnd_ComboBox, 5, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ExperimentVideo_ImageBox, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ExperimentVideo_PlayButton, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ExperimentVideo_TrackBar, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ExperimentVideoAutoplay_CheckBox, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label11, 4, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.AdjustStart_ComboBox, 3, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(394, 298)
        Me.TableLayoutPanel3.TabIndex = 20
        '
        'AdjustEnd_ComboBox
        '
        Me.AdjustEnd_ComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdjustEnd_ComboBox.FormattingEnabled = True
        Me.AdjustEnd_ComboBox.Location = New System.Drawing.Point(347, 266)
        Me.AdjustEnd_ComboBox.Name = "AdjustEnd_ComboBox"
        Me.AdjustEnd_ComboBox.Size = New System.Drawing.Size(44, 23)
        Me.AdjustEnd_ComboBox.TabIndex = 22
        '
        'ExperimentVideo_ImageBox
        '
        Me.ExperimentVideo_ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TableLayoutPanel3.SetColumnSpan(Me.ExperimentVideo_ImageBox, 6)
        Me.ExperimentVideo_ImageBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExperimentVideo_ImageBox.Location = New System.Drawing.Point(3, 3)
        Me.ExperimentVideo_ImageBox.Name = "ExperimentVideo_ImageBox"
        Me.ExperimentVideo_ImageBox.Size = New System.Drawing.Size(388, 222)
        Me.ExperimentVideo_ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ExperimentVideo_ImageBox.TabIndex = 2
        Me.ExperimentVideo_ImageBox.TabStop = False
        '
        'ExperimentVideo_PlayButton
        '
        Me.ExperimentVideo_PlayButton.Enabled = False
        Me.ExperimentVideo_PlayButton.Location = New System.Drawing.Point(91, 266)
        Me.ExperimentVideo_PlayButton.Name = "ExperimentVideo_PlayButton"
        Me.ExperimentVideo_PlayButton.Size = New System.Drawing.Size(44, 28)
        Me.ExperimentVideo_PlayButton.TabIndex = 1
        Me.ExperimentVideo_PlayButton.UseVisualStyleBackColor = True
        Me.ExperimentVideo_PlayButton.ViewMode = ControlsLibrary.PlayButton.ViewModes.Play
        '
        'ExperimentVideo_TrackBar
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.ExperimentVideo_TrackBar, 6)
        Me.ExperimentVideo_TrackBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExperimentVideo_TrackBar.Location = New System.Drawing.Point(3, 231)
        Me.ExperimentVideo_TrackBar.Name = "ExperimentVideo_TrackBar"
        Me.ExperimentVideo_TrackBar.Size = New System.Drawing.Size(388, 29)
        Me.ExperimentVideo_TrackBar.TabIndex = 4
        '
        'ExperimentVideoAutoplay_CheckBox
        '
        Me.ExperimentVideoAutoplay_CheckBox.AutoSize = True
        Me.ExperimentVideoAutoplay_CheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExperimentVideoAutoplay_CheckBox.Location = New System.Drawing.Point(10, 266)
        Me.ExperimentVideoAutoplay_CheckBox.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.ExperimentVideoAutoplay_CheckBox.Name = "ExperimentVideoAutoplay_CheckBox"
        Me.ExperimentVideoAutoplay_CheckBox.Size = New System.Drawing.Size(75, 29)
        Me.ExperimentVideoAutoplay_CheckBox.TabIndex = 18
        Me.ExperimentVideoAutoplay_CheckBox.Text = "Autoplay"
        Me.ExperimentVideoAutoplay_CheckBox.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Location = New System.Drawing.Point(141, 263)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 35)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Adjust start"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(269, 263)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 35)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Adjust end"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AdjustStart_ComboBox
        '
        Me.AdjustStart_ComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdjustStart_ComboBox.FormattingEnabled = True
        Me.AdjustStart_ComboBox.Location = New System.Drawing.Point(219, 266)
        Me.AdjustStart_ComboBox.Name = "AdjustStart_ComboBox"
        Me.AdjustStart_ComboBox.Size = New System.Drawing.Size(44, 23)
        Me.AdjustStart_ComboBox.TabIndex = 21
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.Previous_Button, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.FirstUnscored_Button, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Next_Button, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(123, 329)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(400, 100)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'Previous_Button
        '
        Me.Previous_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Previous_Button.Location = New System.Drawing.Point(136, 3)
        Me.Previous_Button.Name = "Previous_Button"
        Me.Previous_Button.Size = New System.Drawing.Size(127, 94)
        Me.Previous_Button.TabIndex = 8
        Me.Previous_Button.Text = "&Previous trial"
        Me.Previous_Button.UseVisualStyleBackColor = True
        '
        'FirstUnscored_Button
        '
        Me.FirstUnscored_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FirstUnscored_Button.Location = New System.Drawing.Point(269, 3)
        Me.FirstUnscored_Button.Name = "FirstUnscored_Button"
        Me.FirstUnscored_Button.Size = New System.Drawing.Size(128, 94)
        Me.FirstUnscored_Button.TabIndex = 10
        Me.FirstUnscored_Button.Text = "First &unscored trial"
        Me.FirstUnscored_Button.UseVisualStyleBackColor = True
        '
        'Next_Button
        '
        Me.Next_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Next_Button.Location = New System.Drawing.Point(3, 3)
        Me.Next_Button.Name = "Next_Button"
        Me.Next_Button.Size = New System.Drawing.Size(127, 94)
        Me.Next_Button.TabIndex = 9
        Me.Next_Button.Text = "&Next trial"
        Me.Next_Button.UseVisualStyleBackColor = True
        '
        'ScoringPanelHolder_Panel
        '
        Me.ScoringPanelHolder_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScoringPanelHolder_Panel.Location = New System.Drawing.Point(526, 326)
        Me.ScoringPanelHolder_Panel.Margin = New System.Windows.Forms.Padding(0)
        Me.ScoringPanelHolder_Panel.Name = "ScoringPanelHolder_Panel"
        Me.ScoringPanelHolder_Panel.Padding = New System.Windows.Forms.Padding(3)
        Me.ScoringPanelHolder_Panel.Size = New System.Drawing.Size(406, 106)
        Me.ScoringPanelHolder_Panel.TabIndex = 12
        '
        'Settings_GroupBox
        '
        Me.Settings_GroupBox.Controls.Add(Me.SyncTimeInputBoxHolder_Panel)
        Me.Settings_GroupBox.Controls.Add(Me.Label9)
        Me.Settings_GroupBox.Controls.Add(Me.SelectCorrectVideosFolder_Button)
        Me.Settings_GroupBox.Controls.Add(Me.CorrectVideosFolder_TextBox)
        Me.Settings_GroupBox.Controls.Add(Me.Label8)
        Me.Settings_GroupBox.Controls.Add(Me.SelectExperimentVideoFile_Button)
        Me.Settings_GroupBox.Controls.Add(Me.ExperimentVideoFile_TextBox)
        Me.Settings_GroupBox.Controls.Add(Me.Label7)
        Me.Settings_GroupBox.Controls.Add(Me.LockSettings_Button)
        Me.Settings_GroupBox.Controls.Add(Me.ScoringType_ComboBox)
        Me.Settings_GroupBox.Controls.Add(Me.Label6)
        Me.Settings_GroupBox.Controls.Add(Me.TrialEndColumnComboBox)
        Me.Settings_GroupBox.Controls.Add(Me.TrialStartColumn_ComboBox)
        Me.Settings_GroupBox.Controls.Add(Me.CorrectVideoColumn_ComboBox)
        Me.Settings_GroupBox.Controls.Add(Me.Label4)
        Me.Settings_GroupBox.Controls.Add(Me.Label3)
        Me.Settings_GroupBox.Controls.Add(Me.Label2)
        Me.Settings_GroupBox.Controls.Add(Me.SelectDataFile_Button)
        Me.Settings_GroupBox.Controls.Add(Me.DataFile_TextBox)
        Me.Settings_GroupBox.Controls.Add(Me.Label1)
        Me.Settings_GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Settings_GroupBox.Location = New System.Drawing.Point(3, 3)
        Me.Settings_GroupBox.Name = "Settings_GroupBox"
        Me.Settings_GroupBox.Size = New System.Drawing.Size(1188, 134)
        Me.Settings_GroupBox.TabIndex = 0
        Me.Settings_GroupBox.TabStop = False
        Me.Settings_GroupBox.Text = "Settings"
        '
        'SyncTimeInputBoxHolder_Panel
        '
        Me.SyncTimeInputBoxHolder_Panel.Location = New System.Drawing.Point(663, 103)
        Me.SyncTimeInputBoxHolder_Panel.Margin = New System.Windows.Forms.Padding(0)
        Me.SyncTimeInputBoxHolder_Panel.Name = "SyncTimeInputBoxHolder_Panel"
        Me.SyncTimeInputBoxHolder_Panel.Size = New System.Drawing.Size(224, 23)
        Me.SyncTimeInputBoxHolder_Panel.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(444, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(216, 15)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Sync time (s, in experiment video time):"
        '
        'SelectCorrectVideosFolder_Button
        '
        Me.SelectCorrectVideosFolder_Button.Location = New System.Drawing.Point(812, 16)
        Me.SelectCorrectVideosFolder_Button.Name = "SelectCorrectVideosFolder_Button"
        Me.SelectCorrectVideosFolder_Button.Size = New System.Drawing.Size(75, 23)
        Me.SelectCorrectVideosFolder_Button.TabIndex = 19
        Me.SelectCorrectVideosFolder_Button.Text = "Select"
        Me.SelectCorrectVideosFolder_Button.UseVisualStyleBackColor = True
        '
        'CorrectVideosFolder_TextBox
        '
        Me.CorrectVideosFolder_TextBox.Location = New System.Drawing.Point(570, 16)
        Me.CorrectVideosFolder_TextBox.Name = "CorrectVideosFolder_TextBox"
        Me.CorrectVideosFolder_TextBox.ReadOnly = True
        Me.CorrectVideosFolder_TextBox.Size = New System.Drawing.Size(236, 23)
        Me.CorrectVideosFolder_TextBox.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(444, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 15)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Correct videos folder:"
        '
        'SelectExperimentVideoFile_Button
        '
        Me.SelectExperimentVideoFile_Button.Location = New System.Drawing.Point(812, 45)
        Me.SelectExperimentVideoFile_Button.Name = "SelectExperimentVideoFile_Button"
        Me.SelectExperimentVideoFile_Button.Size = New System.Drawing.Size(75, 23)
        Me.SelectExperimentVideoFile_Button.TabIndex = 16
        Me.SelectExperimentVideoFile_Button.Text = "Select"
        Me.SelectExperimentVideoFile_Button.UseVisualStyleBackColor = True
        '
        'ExperimentVideoFile_TextBox
        '
        Me.ExperimentVideoFile_TextBox.Location = New System.Drawing.Point(570, 45)
        Me.ExperimentVideoFile_TextBox.Name = "ExperimentVideoFile_TextBox"
        Me.ExperimentVideoFile_TextBox.ReadOnly = True
        Me.ExperimentVideoFile_TextBox.Size = New System.Drawing.Size(236, 23)
        Me.ExperimentVideoFile_TextBox.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(444, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 15)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Experiment video file:"
        '
        'LockSettings_Button
        '
        Me.LockSettings_Button.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LockSettings_Button.Location = New System.Drawing.Point(893, 16)
        Me.LockSettings_Button.Name = "LockSettings_Button"
        Me.LockSettings_Button.Size = New System.Drawing.Size(286, 110)
        Me.LockSettings_Button.TabIndex = 13
        Me.LockSettings_Button.Text = "Lock settings and start scoring"
        Me.LockSettings_Button.UseVisualStyleBackColor = True
        '
        'ScoringType_ComboBox
        '
        Me.ScoringType_ComboBox.FormattingEnabled = True
        Me.ScoringType_ComboBox.Location = New System.Drawing.Point(570, 74)
        Me.ScoringType_ComboBox.Name = "ScoringType_ComboBox"
        Me.ScoringType_ComboBox.Size = New System.Drawing.Size(317, 23)
        Me.ScoringType_ComboBox.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(444, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Scoring type:"
        '
        'TrialEndColumnComboBox
        '
        Me.TrialEndColumnComboBox.FormattingEnabled = True
        Me.TrialEndColumnComboBox.Location = New System.Drawing.Point(163, 103)
        Me.TrialEndColumnComboBox.Name = "TrialEndColumnComboBox"
        Me.TrialEndColumnComboBox.Size = New System.Drawing.Size(252, 23)
        Me.TrialEndColumnComboBox.TabIndex = 8
        '
        'TrialStartColumn_ComboBox
        '
        Me.TrialStartColumn_ComboBox.FormattingEnabled = True
        Me.TrialStartColumn_ComboBox.Location = New System.Drawing.Point(163, 74)
        Me.TrialStartColumn_ComboBox.Name = "TrialStartColumn_ComboBox"
        Me.TrialStartColumn_ComboBox.Size = New System.Drawing.Size(252, 23)
        Me.TrialStartColumn_ComboBox.TabIndex = 7
        '
        'CorrectVideoColumn_ComboBox
        '
        Me.CorrectVideoColumn_ComboBox.FormattingEnabled = True
        Me.CorrectVideoColumn_ComboBox.Location = New System.Drawing.Point(163, 45)
        Me.CorrectVideoColumn_ComboBox.Name = "CorrectVideoColumn_ComboBox"
        Me.CorrectVideoColumn_ComboBox.Size = New System.Drawing.Size(252, 23)
        Me.CorrectVideoColumn_ComboBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Trial end time (s) column:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Trial start time (s) column:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Correct videos column:"
        '
        'SelectDataFile_Button
        '
        Me.SelectDataFile_Button.Location = New System.Drawing.Point(340, 16)
        Me.SelectDataFile_Button.Name = "SelectDataFile_Button"
        Me.SelectDataFile_Button.Size = New System.Drawing.Size(75, 23)
        Me.SelectDataFile_Button.TabIndex = 2
        Me.SelectDataFile_Button.Text = "Select"
        Me.SelectDataFile_Button.UseVisualStyleBackColor = True
        '
        'DataFile_TextBox
        '
        Me.DataFile_TextBox.Location = New System.Drawing.Point(98, 16)
        Me.DataFile_TextBox.Name = "DataFile_TextBox"
        Me.DataFile_TextBox.ReadOnly = True
        Me.DataFile_TextBox.Size = New System.Drawing.Size(236, 23)
        Me.DataFile_TextBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Data file (.csv):"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Settings_GroupBox, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.WorkFlow_TableLayoutPanel, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1194, 578)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(1194, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 602)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "Video Score Tool"
        Me.WorkFlow_TableLayoutPanel.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.Video1_TableLayoutPanel.ResumeLayout(False)
        Me.Video1_TableLayoutPanel.PerformLayout()
        CType(Me.CorrectVideo_ImageBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CorrectVideo_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.ExperimentVideo_ImageBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ExperimentVideo_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Settings_GroupBox.ResumeLayout(False)
        Me.Settings_GroupBox.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents WorkFlow_TableLayoutPanel As TableLayoutPanel
    Friend WithEvents Settings_GroupBox As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Previous_Button As Button
    Friend WithEvents Next_Button As Button
    Friend WithEvents FirstUnscored_Button As Button
    Friend WithEvents Video1_TableLayoutPanel As TableLayoutPanel
    Friend WithEvents CorrectVideo_ImageBox As Emgu.CV.UI.ImageBox
    Friend WithEvents CorrectVideo_PlayButton As ControlsLibrary.PlayButton
    Friend WithEvents CorrectVideo_TrackBar As TrackBar
    Friend WithEvents CorrectVideoAutoplay_CheckBox As CheckBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents ExperimentVideo_ImageBox As Emgu.CV.UI.ImageBox
    Friend WithEvents ExperimentVideo_PlayButton As ControlsLibrary.PlayButton
    Friend WithEvents ExperimentVideo_TrackBar As TrackBar
    Friend WithEvents ExperimentVideoAutoplay_CheckBox As CheckBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents SelectDataFile_Button As Button
    Friend WithEvents DataFile_TextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TrialEndColumnComboBox As ComboBox
    Friend WithEvents TrialStartColumn_ComboBox As ComboBox
    Friend WithEvents CorrectVideoColumn_ComboBox As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ScoringType_ComboBox As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TrialOrder_ComboBox As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents LockSettings_Button As Button
    Friend WithEvents SaveAs_Button As Button
    Friend WithEvents Save_Button As Button
    Friend WithEvents NewDataFile_Button As Button
    Friend WithEvents Trials_ListBox As ListBox
    Friend WithEvents SelectExperimentVideoFile_Button As Button
    Friend WithEvents ExperimentVideoFile_TextBox As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents SelectCorrectVideosFolder_Button As Button
    Friend WithEvents CorrectVideosFolder_TextBox As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents ScoringPanelHolder_Panel As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label9 As Label
    Friend WithEvents SyncTimeInputBoxHolder_Panel As Panel
    Friend WithEvents AdjustEnd_ComboBox As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents AdjustStart_ComboBox As ComboBox
End Class
