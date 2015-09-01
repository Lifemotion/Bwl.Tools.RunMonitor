Imports System.Threading
Imports System.IO
Imports Bwl.Framework

Public Class Task
    Private _appInfo As AppInfo
    Private _process As Process
    Private _logger As Logger
    Private _monitor As Thread
    Private _lastRunTime As DateTime
    Private ReadOnly _lastActions As New List(Of KeyValuePair(Of String, String))
    Private _stopped As Boolean = False

    Public Event TaskError()

    Public ReadOnly Property LastActions As KeyValuePair(Of String, String)()
        Get
            SyncLock (_lastActions)
                Return _lastActions.ToArray
            End SyncLock
        End Get
    End Property

    Public Sub New(appInfo As AppInfo, logger As Logger)
        _appInfo = appInfo
        _logger = logger
        StartTask()
        _logger.AddMessage(appInfo.ID + " - создана")
        AddInfo(appInfo.ID + " - создана")
    End Sub
    Public Sub StartTask()
        _stopped = False
        _monitor = New Threading.Thread(AddressOf MonitorThread)
        _monitor.Priority = Threading.ThreadPriority.BelowNormal
        _monitor.IsBackground = True
        _monitor.Start()
        AddInfo("StartTask")
    End Sub
    Public ReadOnly Property AppInfo As AppInfo
        Get
            Return _appInfo
        End Get
    End Property
    Public ReadOnly Property Running As Boolean
        Get
            Return IsRunning()
        End Get
    End Property

    Public ReadOnly Property TaskRunning As Boolean
        Get
            Return _monitor.IsAlive()
        End Get
    End Property

    Public Property ProcessRunning As Boolean
        Get
            Return _process IsNot Nothing
        End Get
        Set(value As Boolean)

            If value <> (_process IsNot Nothing) Then
                If value = True Then
                    FindOrRun()
                Else
                    Try
                        _process.Kill()
                        _process = Nothing
                    Catch ex As Exception
                        _process = Nothing
                    End Try
                End If
            End If
        End Set
    End Property

    Private Sub AddInfo(info As String)
        Try
            SyncLock (_lastActions)
                Dim dtNow = DateTime.Now
                Dim oldInfo = _lastActions.Where(Function(pair) (Convert.ToDateTime(pair.Key) - dtNow).TotalHours > 1)
                If oldInfo IsNot Nothing AndAlso oldInfo.Any Then
                    For Each oi In oldInfo.ToArray
                        _lastActions.Remove(oi)
                    Next
                End If
                _lastActions.Add(New KeyValuePair(Of String, String)(dtNow.ToString, info))
            End SyncLock
        Catch ex As Exception
            _logger.AddError("Task.AddInfo " + ex.ToString)
        End Try
    End Sub

    Public Sub StopTask()
        _stopped = True
        _process = Nothing
        _logger.AddMessage(AppInfo.ID + " - остановлена")
        AddInfo(AppInfo.ID + " - остановлена")
    End Sub

    Private Function GoodHelperFile() As Boolean
        Dim res = False
        Dim fp = Path.GetDirectoryName(_appInfo.Path) + "\\..\\monitor.txt"
        If File.Exists(fp) Then
            Dim str = File.ReadAllText(fp)
            Dim ts = Convert.ToDateTime(str)
            If (DateTime.Now - ts).TotalSeconds < 3 Then
                res = True
            End If
        End If
        Return res
    End Function

    Private Function IsRunning() As Boolean
        Try
            If _process Is Nothing Then
                _logger.AddMessage(AppInfo.ID + " - не определен")
                AddInfo(AppInfo.ID + " - не определен")
                Return False
            End If

            If (DateTime.Now - _lastRunTime).TotalSeconds > 60 Then
                If _process Is Nothing OrElse _process.Responding = False OrElse _process.Handle = IntPtr.Zero OrElse _process.HasExited Then
                    If Not GoodHelperFile() Then
                        _logger.AddMessage(AppInfo.ID + " - не отвечает")
                        AddInfo(AppInfo.ID + " - не отвечает")
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub FindOrRun()
        _process = Nothing
        If File.Exists(_appInfo.Path) Then
            Dim procName = System.IO.Path.GetFileNameWithoutExtension(_appInfo.Path)
            Dim processes = System.Diagnostics.Process.GetProcessesByName(procName)
            If processes IsNot Nothing AndAlso processes.Any Then
                For Each pr In processes
                    If pr.MainModule.FileName.ToLower = _appInfo.Path.ToLower Then
                        _process = pr
                        Exit For
                    End If
                Next
            End If
            If _process Is Nothing Then
                _lastRunTime = DateTime.Now
                _process = New Process
                _process.StartInfo.FileName = _appInfo.Path
                _process.StartInfo.Arguments = _appInfo.Args
                _process.StartInfo.WorkingDirectory = _appInfo.Basedir
                _process.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                _process.Start()
            End If
        End If
    End Sub

    Private Sub MonitorThread()
        Do
            If _stopped Then
                Exit Do
            End If
            If Not IsRunning() Then
                If _process Is Nothing Then
                    _logger.AddMessage(_appInfo.ID + " - запускается")
                    AddInfo(_appInfo.ID + " - запускается")
                Else
                    RaiseEvent TaskError()
                    _logger.AddWarning(_appInfo.ID + " - перезапускается, пауза")
                    AddInfo(_appInfo.ID + " - перезапускается, пауза")
                    Threading.Thread.Sleep(3000)
                End If
                Try
                    Try
                        _logger.AddMessage(_appInfo.ID + " - закрывается")
                        AddInfo(_appInfo.ID + " - закрывается")
                        If _process IsNot Nothing Then
                            _process.Kill()
                        End If
                    Catch ex As Exception
                    End Try
                    Threading.Thread.Sleep(500)
                    _logger.AddMessage(_appInfo.ID + " - запускается")
                    AddInfo(_appInfo.ID + " - запускается")
                    FindOrRun()
                    _logger.AddInformation(_appInfo.ID + " - запуск успешен")
                    AddInfo(_appInfo.ID + " - запуск успешен")
                Catch ex As Exception
                    RaiseEvent TaskError()
                    _logger.AddError(_appInfo.ID + " - не удалось запустить задачу: " + ex.Message)
                    AddInfo(_appInfo.ID + " - не удалось запустить задачу: " + ex.Message)
                End Try
            End If
            Threading.Thread.Sleep(1000)
        Loop
    End Sub
End Class
