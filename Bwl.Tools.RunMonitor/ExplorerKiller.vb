Imports System.Runtime.InteropServices
Imports Bwl.Framework

Public Class ExplorerKiller

    Public Shared Sub KillExplorer(logger As Logger)
        Try
            Threading.Thread.Sleep(20000)
            Dim processes = Process.GetProcessesByName("explorer")
            If processes IsNot Nothing AndAlso processes.Any Then
                Dim hProcess = OpenProcess(ProcessAccessFlags.Terminate, False, processes(0).Id)
                TerminateProcess(hProcess, 1)
            End If
        Catch ex As Exception
            logger.AddError("ExplorerKiller.KillExplorer " + ex.ToString)
        End Try
    End Sub

    Enum ProcessAccessFlags As UInteger
        All = &H1F0FFF
        Terminate = &H1
        CreateThread = &H2
        VMOperation = &H8
        VMRead = &H10
        VMWrite = &H20
        DupHandle = &H40
        SetInformation = &H200
        QueryInformation = &H400
        Synchronize = &H100000
    End Enum

    <DllImport("kernel32.dll")>
    Private Shared Function OpenProcess(ByVal dwDesiredAccess As ProcessAccessFlags, <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function TerminateProcess(ByVal hProcess As IntPtr, ByVal uExitCode As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
End Class
