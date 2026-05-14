```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class TrainerCore
{
    private const string GameProcessName = "left4dead2";
    private static Process gameProcess;

    // Example memory addresses (these need to be updated for actual use)
    private const int HealthAddress = 0x01234567; // Replace with correct address
    private const int AmmoAddress = 0x01234568;   // Replace with correct address
    private const int ScoreAddress = 0x01234569;  // Replace with correct address

    public static bool AttachToProcess()
    {
        try
        {
            gameProcess = Process.GetProcessesByName(GameProcessName)[0];
            return gameProcess != null;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsGameRunning()
    {
        return gameProcess != null && !gameProcess.HasExited;
    }

    public static float ReadFloat(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteFloat(int address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, (IntPtr)address, buffer, buffer.Length, out _);
    }

    public static int ReadInt(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static void WriteInt(int address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, (IntPtr)address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);
}
```

This code provides a basic structure for a cheat trainer for a game like Left 4 Dead 2. The memory addresses are placeholders and need to be replaced with the actual addresses from the game. Ensure you understand the legal and ethical implications of using cheat trainers.