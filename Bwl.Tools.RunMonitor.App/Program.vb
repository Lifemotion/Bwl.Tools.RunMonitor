Imports System.Threading
Imports System.Windows.Forms

Public Module Program
    Private _isProcess As Boolean = False

    Public Sub Main(ByVal args As String())
        Application.EnableVisualStyles()
        AddHandler Application.ThreadException, AddressOf Application_ThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException

        RunProcess()
    End Sub

    Private Sub RunProcess()
        _isProcess = True
        Application.Run(New RunMonitorForm)
    End Sub

    Private Sub CurrentDomain_UnhandledException(sender As Object, arg As UnhandledExceptionEventArgs)
        Try
            HandleUnhandledException(CType(arg.ExceptionObject, Exception))
            System.Diagnostics.Process.GetCurrentProcess().Kill()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Application_ThreadException(sender As Object, arg As ThreadExceptionEventArgs)
        Try
            HandleUnhandledException(arg.Exception)
            System.Diagnostics.Process.GetCurrentProcess().Kill()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub HandleUnhandledException(exc As Exception)
        Try
            If (_isProcess) Then
                MessageBox.Show("RunMonitor - Ошибка выполнения: " + exc.Message)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Module
