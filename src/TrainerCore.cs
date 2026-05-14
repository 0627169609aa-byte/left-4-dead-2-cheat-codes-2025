```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process process;
    private IntPtr processHandle;

    public bool AttachToProcess(string processName)
    {
        process = Process.GetProcessesByName(processName)[0];
        if (process == null) return false;

        processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
        return processHandle != IntPtr.Zero;
    }

    public void Detach()
    {
        if (processHandle != IntPtr.Zero)
        {
            CloseHandle(processHandle);
            processHandle = IntPtr.Zero;
        }
    }

    public float ReadFloat(IntPtr address)
    {
        float value;
        ReadProcessMemory(processHandle, address, out value, Marshal.SizeOf(typeof(float)), out _);
        return value;
    }

    public void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(processHandle, address, ref value, Marshal.SizeOf(typeof(float)), out _);
    }

    public int ReadInt(IntPtr address)
    {
        int value;
        ReadProcessMemory(processHandle, address, out value, Marshal.SizeOf(typeof(int)), out _);
        return value;
    }

    public void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(processHandle, address, ref value, Marshal.SizeOf(typeof(int)), out _);
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
    
    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out float lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
    
    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
    
    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref float lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);
    
    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);
    
    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        QueryInformation = 0x0400