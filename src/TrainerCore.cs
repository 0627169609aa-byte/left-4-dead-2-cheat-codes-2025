```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessMemory
{
    private const int PROCESS_VM_READ = 0x0010;
    private const int PROCESS_VM_WRITE = 0x0020;
    private const int PROCESS_VM_OPERATION = 0x0008;

    private static IntPtr processHandle;

    public static bool AttachToProcess(string processName)
    {
        Process process = Process.GetProcessesByName(processName).FirstOrDefault();
        if (process == null)
            return false;

        processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
        return processHandle != IntPtr.Zero;
    }

    public static void CloseProcess()
    {
        if (processHandle != IntPtr.Zero)
        {
            CloseHandle(processHandle);
            processHandle = IntPtr.Zero;
        }
    }

    public static int ReadInt(IntPtr address)
    {
        int value = 0;
        ReadProcessMemory(processHandle, address, BitConverter.GetBytes(value), sizeof(int), IntPtr.Zero);
        return value;
    }

    public static void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(processHandle, address, BitConverter.GetBytes(value), sizeof(int), IntPtr.Zero);
    }

    public static float ReadFloat(IntPtr address)
    {
        float value = 0;
        ReadProcessMemory(processHandle, address, BitConverter.GetBytes(value), sizeof(float), IntPtr.Zero);
        return value;
    }

    public static void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(processHandle, address, BitConverter.GetBytes(value), sizeof(float), IntPtr.Zero);
    }

    public static bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle(IntPtr hObject);
}

public static class TrainerCore
{
    public const string ProcessName = "left4dead2";
    public static readonly IntPtr PlayerHealthAddress = new IntPtr(0x00ABCDEF); // Example address
    public static readonly IntPtr PlayerAmmoAddress = new