```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessMemory
{
    private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

    private static Process gameProcess;

    public static void AttachToProcess(string processName)
    {
        var processes = Process.GetProcessesByName(processName);

        if (processes.Length > 0)
        {
            gameProcess = processes[0];
        }
        else
        {
            throw new Exception($"Process '{processName}' not found.");
        }
    }

    public static bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public static float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
    }

    public static int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
    }
}

public static class TrainerCore
{
    private static readonly IntPtr HealthAddress = new IntPtr(0x008C3A24); // Example address for health
    private static readonly IntPtr AmmoAddress = new IntPtr(0x008C3A28); // Example address for ammo

    public static void SetHealth(float value)
    {
        if (ProcessMemory.IsGameRunning("left4dead2"))
        {
            ProcessMemory.WriteFloat(HealthAddress, value);
        }
    }

    public static void SetAmmo(int value)
    {
        if (ProcessMemory.IsGameRunning("left4dead2"))