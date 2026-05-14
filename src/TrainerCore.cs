```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process process;
    private IntPtr processHandle;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    private static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesRead);
    
    [DllImport("kernel32.dll")]
    private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesWritten);

    private const uint PROCESS_VM_READ = 0x0010;
    private const uint PROCESS_VM_WRITE = 0x0020;
    private const uint PROCESS_VM_OPERATION = 0x0008;

    public bool AttachToProcess(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
            return false;

        process = processes[0];
        processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
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

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
    }

    public int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
    }
}

// Sample static addresses (replace with actual addresses for Left 4 Dead 2)
public static class Addresses
{
    public static readonly IntPtr