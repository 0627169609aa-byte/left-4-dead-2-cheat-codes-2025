```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    const int PROCESS_ALL_ACCESS = 0x1F0FFF;

    private Process process;

    public bool AttachToProcess(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
            return false;

        process = processes[0];
        return true;
    }

    public bool IsGameRunning()
    {
        return process != null && !process.HasExited;
    }

    public float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
    }

    public int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll")]
    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    // Left 4 Dead 2 static addresses (replace with real addresses)
    public static readonly IntPtr PlayerHealthAddress = (IntPtr)0x01234567;
    public static readonly IntPtr PlayerAmmoAddress = (IntPtr)0x01234568;

    // Additional static addresses can be defined here
}

public class TrainerCore
{
    private ProcessMemory processMemory = new ProcessMemory();

    public void Initialize()
    {
        if (!processMemory.AttachToProcess("left4dead2"))
        {
            Console.WriteLine("Game not running.");
            return;
        }

        Console.WriteLine("Trainer initialized.");
    }

    public void SetPlayerHealth(float health)
    {
        if (processMemory.IsGameRunning())
            processMemory.WriteFloat(ProcessMemory.PlayerHealthAddress, health);
    }

    public void SetPlayerAmmo(int ammo)
    {
        if (processMemory.IsGameRunning())
            processMemory.WriteInt(ProcessMemory.PlayerAmmoAddress, ammo);
    }
}
```