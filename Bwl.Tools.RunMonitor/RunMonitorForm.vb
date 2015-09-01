Imports System.IO
Imports Bwl.Framework

Public Class RunMonitorForm
    Protected _app As New AppBase(True)
    Protected _tasks As New List(Of Task)
    Protected _showMinimized As New BooleanSetting(_app.RootStorage, "MinimizeAtStart", False)
    Protected _delayStart As New IntegerSetting(_app.RootStorage, "DelayAppsStart", 10)
    Protected _killExplorer As New BooleanSetting(_app.RootStorage, "KillExplorer", False)
    Protected _selectedTask As Task
    Protected _oldstate As New List(Of Boolean())
    Protected _netWatcher As New NetWatcher(_app.RootStorage.CreateChildStorage("NetWatcher"), _app.RootLogger.CreateChildLogger("NetWatcher"))
    Protected _memWatcher As New MemWatcher(_app.RootStorage.CreateChildStorage("MemWatcher"), _app.RootLogger.CreateChildLogger("MemWatcher"))


    Public Delegate Function SecurityCheckDelegate() As Boolean

    Protected _securityCheckDelegate As SecurityCheckDelegate = Function() True

    Public Sub New()
        InitializeComponent()
        If _showMinimized.Value Then
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub

    Private Sub RunMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _showMinimized.Value Then
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
        End If
        If _delayStart.Value > 0 Then
            Dim timer As New System.Timers.Timer
            timer.Interval = _delayStart.Value * 1000
            timer.AutoReset = False
            AddHandler timer.Elapsed, AddressOf Init
            timer.Start()
        Else
            Init()
        End If
    End Sub

    Public Event InitRunMonitor(sender As RunMonitorForm)
    Public Event TaskError(sender As RunMonitorForm, task As Task)

    Private Sub Init()
        If _killExplorer.Value Then
            ExplorerKiller.KillExplorer(_app.RootLogger)
        End If

        If Me.InvokeRequired Then
            Me.Invoke(Sub() Init())
        Else
            _app.RootLogger.ConnectWriter(LogWriterList1)
            Dim files = IO.Directory.GetFiles(_app.DataFolder, "*.txt")
            For Each file In files
                Dim app = AppInfo.CreateFrom(file)
                Dim exist As Boolean = False
                For Each task In _tasks
                    If task.AppInfo.Path = app.Path Then
                        exist = True
                        Exit For
                    End If
                Next
                If app IsNot Nothing AndAlso exist = False Then
                    Try
                        Dim task = New Task(app, _app.RootLogger.CreateChildLogger(app.ID))
                        _tasks.Add(task)

                    Catch ex As Exception
                        _app.RootLogger.AddError("Не удалось создать задачу " + app.ID + ":" + ex.Message)
                    End Try
                End If
            Next
            If _tasks.Count > 0 Then
                Threading.Thread.Sleep(1000)
                _selectedTask = _tasks(0)
                ButtonText(_tasks(0))
            End If
            timerHider.Start()
            RaiseEvent InitRunMonitor(Me)
        End If
    End Sub

    Private Sub ButtonText(task As Task)
        _btnStartStopTask.Text = If(task.TaskRunning, "Остановить задачу", "Запустить задачу")
        _btnStartStopPocess.Text = If(task.ProcessRunning, "Остановить процесс", "Запустить процесс")
    End Sub

    Private Sub ShowTasks()
        Dim result As Boolean = False
        If _oldstate IsNot Nothing Then
            If _oldstate.Count = _tasks.Count AndAlso _tasks.Count > 0 Then
                For i = 0 To _oldstate.Count - 1
                    If _oldstate(i)(0) <> _tasks(i).TaskRunning Or _oldstate(i)(1) <> _tasks(i).ProcessRunning Then
                        result = True
                        Exit For
                    End If
                Next
            Else : result = True
            End If
        End If

        If result = True Then
            _oldstate.Clear()
            _dgvTasks.Rows.Clear()
            For i = 0 To _tasks.Count - 1

                Dim task_i = i
                AddHandler _tasks(i).TaskError, Sub()
                                                    RaiseEvent TaskError(Me, _tasks(task_i))
                                                End Sub

                _dgvTasks.Rows.Add()
                _dgvTasks.Item(0, i).Value = _tasks(i).AppInfo.ID

                If _selectedTask IsNot Nothing AndAlso _selectedTask.AppInfo.ID = _tasks(i).AppInfo.ID Then
                    _dgvTasks.Rows(i).Selected = True
                    ButtonText(_tasks(i))
                End If
                _dgvTasks.Rows(i).Cells(1).Value = _tasks(i).TaskRunning
                _dgvTasks.Rows(i).Cells(2).Value = _tasks(i).ProcessRunning
                Dim olditem() As Boolean = New Boolean() {_tasks(i).TaskRunning, _tasks(i).ProcessRunning}
                _oldstate.Add(olditem)
            Next
            If _tasks.Count = 0 Then
                _selectedTask = Nothing
                _btnStartStopTask.Text = "Запустить задачу"
                _btnStartStopPocess.Text = "Запустить процесс"
            End If

        End If
    End Sub


    Private Sub ВыходToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem.Click
        If _securityCheckDelegate.Invoke Then
            Application.Exit()
        End If
    End Sub

    Private Sub НастройкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НастройкиToolStripMenuItem.Click
        If _securityCheckDelegate.Invoke Then
            _app.RootStorage.ShowSettingsForm()
            _app.RootStorage.SaveSettings()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As EventArgs) Handles trayicon.MouseDoubleClick, trayicon.Click
        Me.Visible = Not Me.Visible
        Me.WindowState = FormWindowState.Normal
        Activate()
    End Sub

    Private Sub timerHider_Tick(sender As Object, e As EventArgs) Handles timerHider.Tick
        If _showMinimized.Value Then Me.Visible = False
        timerHider.Stop()
    End Sub

    Private Sub RunMonitor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub _btnEdit_Click(sender As Object, e As EventArgs) Handles _btnEdit.Click
        If _securityCheckDelegate.Invoke Then

            If _selectedTask IsNot Nothing Then
            If _selectedTask.AppInfo.Path IsNot Nothing Then
                Using taskDialog = New TaskDialog(_selectedTask.AppInfo)
                    If taskDialog.ShowDialog() = DialogResult.OK Then
                        If taskDialog.AppName = Nothing Then
                            MessageBox.Show("Не указано название задачи", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf File.Exists(taskDialog.Path) Then
                            Dim oldPath = IO.Path.Combine(_app.DataFolder, _selectedTask.AppInfo.ID + ".txt")
                            If File.Exists(oldPath) Then
                                File.Delete(oldPath)
                            End If

                            _selectedTask.AppInfo.Args = taskDialog.Args
                            _selectedTask.AppInfo.ID = taskDialog.AppName
                            _selectedTask.AppInfo.Path = taskDialog.Path
                            Dim newPath = IO.Path.Combine(_app.DataFolder, _selectedTask.AppInfo.ID + ".txt")

                            _selectedTask.AppInfo.SaveTo(newPath)
                            _oldstate.Clear()
                        Else
                            MessageBox.Show("Файл не существует", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End Using
            End If
        Else : MessageBox.Show("Не выбран элемент из списка программ", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
TODO:   End If

    End Sub

    Private Sub _btnAdd_Click(sender As Object, e As EventArgs) Handles _btnAdd.Click
        If _securityCheckDelegate.Invoke Then

            Using taskDialog = New TaskDialog(Nothing)
                taskDialog.Text = "Добавление новой задачи"
                If taskDialog.ShowDialog() = DialogResult.OK Then
                    If taskDialog.AppName = Nothing Then
                        MessageBox.Show("Не указано название задачи", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf File.Exists(taskDialog.Path) Then
                        Dim app = New AppInfo()
                        app.ID = taskDialog.AppName
                        app.Path = taskDialog.Path
                        app.Args = taskDialog.Args
                        app.LoadedFrom = _app.DataFolder + "\" + app.ID + ".txt"
                        app.SaveTo(app.LoadedFrom)

                        Dim task = New Task(app, _app.RootLogger.CreateChildLogger(app.ID))
                        _tasks.Add(task)

                        If _tasks.Count = 1 Then _selectedTask = _tasks(0)
                        _btnStartStopTask.Text = "Остановить задачу"
                        _btnStartStopPocess.Text = "Остановить процесс"
                    Else
                        MessageBox.Show("Файл не существует", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End Using
        End If

    End Sub

    Private Sub _btnDelete_Click(sender As Object, e As EventArgs) Handles _btnDelete.Click
        If _securityCheckDelegate.Invoke Then

            Dim files = IO.Directory.GetFiles(_app.DataFolder, "*.txt")
            For Each file In files
                If _selectedTask IsNot Nothing Then
                    If MessageBox.Show(String.Format("Вы уверены, что хотите удалить задачу {0}?", _selectedTask.AppInfo.ID), "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        _selectedTask.StopTask()
                        Dim path = _selectedTask.AppInfo.LoadedFrom
                        If System.IO.File.Exists(path) Then
                            System.IO.File.Delete(path)
                            _tasks.Remove(_tasks.Find(Function(x) x.AppInfo.ID = _selectedTask.AppInfo.ID))
                            If _tasks.Count > 0 Then _selectedTask = _tasks(0)
                            Exit For
                        End If
                    End If
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub timerUpdateList_Tick(sender As Object, e As EventArgs) Handles timerUpdateList.Tick
        ShowTasks()
    End Sub


    Private Sub _dgvTasks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles _dgvTasks.CellClick
        If e.RowIndex <= _tasks.Count - 1 AndAlso e.RowIndex >= 0 Then
            _selectedTask = _tasks.Find(Function(x) x.AppInfo.ID = _dgvTasks.Item(0, e.RowIndex).Value.ToString)
            If _selectedTask IsNot Nothing Then
                ButtonText(_selectedTask)
            End If
        End If
    End Sub

    Private Sub _btnStartStopTask_Click(sender As Object, e As EventArgs) Handles _btnStartStopTask.Click
        If _securityCheckDelegate.Invoke Then
            If _selectedTask IsNot Nothing Then
                If _selectedTask.TaskRunning Then
                    _selectedTask.StopTask()
                Else
                    _selectedTask.StartTask()
                End If
            End If
        End If
    End Sub

    Private Sub _btnStartStopPocess_Click(sender As Object, e As EventArgs) Handles _btnStartStopPocess.Click
        If _securityCheckDelegate.Invoke Then
            If _selectedTask IsNot Nothing Then
                _selectedTask.ProcessRunning = Not _selectedTask.ProcessRunning
            End If
        End If
    End Sub
End Class
