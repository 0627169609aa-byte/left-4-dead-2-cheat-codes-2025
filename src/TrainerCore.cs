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
        if (process == null)
            return false;

        processHandle = OpenProcess(ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite, false, process.Id);
        return processHandle != IntPtr.Zero;
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(IntPtr address)
    {
        float value;
        ReadProcessMemory(processHandle, address, out value, sizeof(float), out _);
        return value;
    }

    public void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(processHandle, address, ref value, sizeof(float), out _);
    }

    public int ReadInt(IntPtr address)
    {
        int value;
        ReadProcessMemory(processHandle, address, out value, sizeof(int), out _);
        return value;
    }

    public void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(processHandle, address, ref value, sizeof(int), out _);
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

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        VirtualMemoryRead = 0x0010,
        VirtualMemoryWrite = 0x0020,
        // Add more access flags as needed
    }
}

// Static addresses for Left 4 Dead 2 specific values
public static class TrainerCore
{
    public static readonly IntPtr PlayerHealthAddress = (IntPtr)0x12345678; // Replace with actual address
    public static readonly IntPtr Ammo