Imports System.IO
Imports System.Net
Imports Bwl.Framework

Public Class NetWatcher
    Private _logger As Logger
    Private _netAddress As StringSetting
    Private _enabled As BooleanSetting
    Private _timeout As IntegerSetting
    Private _period As IntegerSetting
    Private _command As StringSetting
    Private _lastRequest As DateTime = Now

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _logger = logger
        _netAddress = storage.CreateStringSetting("NetAddress", "http://ya.ru/")
        _enabled = storage.CreateBooleanSetting("Enabled", False)
        _timeout = storage.CreateIntegerSetting("TimeoutMin", 20)
        _period = storage.CreateIntegerSetting("PeriodSec", 30)
        _command = storage.CreateStringSetting("Command", "shutdown -r -t 15")
        Dim process As New Threading.Thread(AddressOf NetWathcherThread)
        process.IsBackground = True
        process.Start()

    End Sub


    Private Sub NetWathcherThread()
        Do
            If _enabled.Value = True Then
                Try
                    Dim result = HttpGet(_netAddress.Value)
                    If result.Length > 32 Then
                        _lastRequest = Now
                    Else
                        _logger.AddWarning(result)
                    End If
                Catch ex As Exception
                    _logger.AddWarning(ex.Message)
                End Try

                Try
                    If (Now - _lastRequest).TotalMinutes > _timeout.Value Then
                        _logger.AddError("Нет сети дольше заданного времени, выполнение команды " + _command.Value)
                        Shell(_command.Value)
                    End If
                Catch ex As Exception
                    _logger.AddWarning(ex.Message)
                End Try
            End If
            Threading.Thread.Sleep(_period.Value * 1000)
        Loop
    End Sub


    Shared Function HttpGet(ByVal url As String) As String
        Dim req = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "GET"
        Dim response As String = New StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd()
        Return response
    End Function
End Class
