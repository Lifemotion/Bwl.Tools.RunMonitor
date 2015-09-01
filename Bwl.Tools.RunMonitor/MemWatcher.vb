Imports System.IO
Imports System.Net
Imports Bwl.Framework

Public Class MemWatcher
    Private _logger As Logger
    Private _netAddress As StringSetting
    Private _enabled As BooleanSetting
    Private _timeout As IntegerSetting
    Private _period As IntegerSetting
    Private _minFreeMem As IntegerSetting
    Private _command As StringSetting
    Private _lastRequest As DateTime = Now

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _logger = logger
        _enabled = storage.CreateBooleanSetting("Enabled", False)
        _period = storage.CreateIntegerSetting("PeriodSec", 60)
        _minFreeMem = storage.CreateIntegerSetting("MinFreeMem", 200, "MinFreeMem", "MB")
        _command = storage.CreateStringSetting("Command", "shutdown -r -t 5")
        Dim process As New Threading.Thread(AddressOf MemWathcherThread)
        process.IsBackground = True
        process.Start()
    End Sub

    Private Sub MemWathcherThread()
        Do
            If _enabled.Value = True Then
                Dim mem = 0UL
                Try
                    mem = Hardware.GetFreeMemoryInfo
                Catch ex As Exception
                    _logger.AddWarning("MemWatcher.MemWathcherThread " + ex.Message)
                End Try
                _logger.AddInformation("FreeMem " + mem.ToString + " MB.")
                Try
                    If mem < _minFreeMem.Value Then
                        _logger.AddError("Слишком маленький объем свободной ОЗУ, выполнение команды " + _command.Value)
                        Shell(_command.Value)
                    End If
                Catch ex As Exception
                    _logger.AddWarning(ex.Message)
                End Try
            End If
            Threading.Thread.Sleep(_period.Value * 1000)
        Loop
    End Sub
End Class
