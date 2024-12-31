using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class ProcessInfo
{
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    public static int GetPolProcessInfo()
    {
        Process[] processes = Process.GetProcessesByName("pol");

        if (processes.Length == 0)
        {
            return -1;
        }

        Console.WriteLine("Select a character from the list below:");
        for (int i = 0; i < processes.Length; i++)
        {
            Process process = processes[i];
            IntPtr mainWindowHandle = process.MainWindowHandle;

            if (mainWindowHandle != IntPtr.Zero && IsWindowVisible(mainWindowHandle))
            {
                StringBuilder windowText = new StringBuilder(256);
                GetWindowText(mainWindowHandle, windowText, windowText.Capacity);
                Console.WriteLine($"{i + 1}. {windowText}");
            }
            else
            {
                Console.WriteLine($"{i + 1}. [No Window Title], PID: {process.Id}");
            }
        }

        Console.Write("Enter the number of the process you want to select: ");
        if (int.TryParse(Console.ReadLine(), out int selection) &&
            selection > 0 && selection <= processes.Length)
        {
            return processes[selection - 1].Id; // Return the selected PID as an int
        }

        Console.WriteLine("Invalid selection.");
        return -1; // Return -1 if no valid process was selected
    }
}
