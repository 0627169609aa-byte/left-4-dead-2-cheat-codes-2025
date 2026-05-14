```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private const string GameProcessName = "left4dead2";
    
    // Example static addresses
    private static readonly IntPtr PlayerHealthAddress = (IntPtr)0x01234567; // Replace with actual address
    private static readonly IntPtr PlayerAmmoAddress = (IntPtr)0x01234568; // Replace with actual address
    
    private Process gameProcess;
    
    public bool AttachToProcess()
    {
        gameProcess = Process.GetProcessesByName(GameProcessName)[0];
        return gameProcess != null;
    }

    public bool IsGameRunning()
    {
        return gameProcess != null && !gameProcess.HasExited;
    }

    public float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
    }

    public int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(gameProcess.Handle, address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
    
    public float GetPlayerHealth()
    {
        return ReadFloat(PlayerHealthAddress);
    }

    public void SetPlayerHealth(float health)
    {
        WriteFloat(PlayerHealthAddress, health);
    }

    public int GetPlayerAmmo()
    {
        return ReadInt(PlayerAmmoAddress);
    }

    public void SetPlayerAmmo(int ammo)
    {
        WriteInt(PlayerAmmoAddress, ammo);
    }

    public void Cleanup()
    {
        if (gameProcess != null && !gameProcess.HasExited)
            gameProcess.Close();
    }
}
```