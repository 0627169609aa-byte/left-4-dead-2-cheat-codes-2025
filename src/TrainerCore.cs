```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private const string GameProcessName = "left4dead2";
    private const int HealthAddress = 0x00A9F7B0; // Example address for player health
    private const int AmmoAddress = 0x00A9F7C0; // Example address for player ammo

    private Process gameProcess;
    private IntPtr processHandle;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint size, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint size, out IntPtr lpNumberOfBytesWritten);

    private const uint PROCESS_VM_READ = 0x0010;
    private const uint PROCESS_VM_WRITE = 0x0020;
    private const uint PROCESS_QUERY_INFORMATION = 0x0400;

    public bool AttachToProcess()
    {
        gameProcess = Process.GetProcessesByName(GameProcessName)[0];
        if (gameProcess == null) return false;

        processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_QUERY_INFORMATION, false, gameProcess.Id);
        return processHandle != IntPtr.Zero;
    }

    public bool IsGameRunning()
    {
        return Process.GetProcessesByName(GameProcessName).Length > 0;
    }

    public float ReadFloat(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(int address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, (uint)buffer.Length, out _);
    }

    public int ReadInt(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(int address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, (uint)buffer.Length, out _);
    }

    public void SetPlayerHealth(float health)
    {
        WriteFloat(HealthAddress, health);
    }

    public