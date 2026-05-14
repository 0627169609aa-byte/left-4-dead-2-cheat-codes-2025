```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private IntPtr processHandle;
    private Process process;
    
    public bool AttachToProcess(string processName)
    {
        process = Process.GetProcessesByName(processName)[0];
        if (process == null) return false;

        processHandle = OpenProcess(ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite, false, process.Id);
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
        float value = 0;
        ReadProcessMemory(processHandle, address, BitConverter.GetBytes(value), 4, out _);
        return value;
    }

    public void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(processHandle, address, BitConverter.GetBytes(value), 4, out _);
    }

    public int ReadInt(IntPtr address)
    {
        int value = 0;
        ReadProcessMemory(processHandle, address, BitConverter.GetBytes(value), 4, out _);
        return value;
    }

    public void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(processHandle, address, BitConverter.GetBytes(value), 4, out _);
    }

    public static bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        VirtualMemoryRead = 0x0010,
        VirtualMemoryWrite = 0x0020,
        All = 0x001F0FFF
    }
}

// Addresses for Left 4 Dead 2 specific values
public static class TrainerCore
{
    public static readonly IntPtr PlayerHealthAddress = (IntPtr)0x12345678; // Example address
    public static readonly IntPtr AmmoCountAddress = (IntPtr)0x87654321;  // Example address