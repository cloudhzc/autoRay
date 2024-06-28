using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Windows;
using System.ComponentModel;
using Microsoft.UI;
using System.Xml.Linq;
using WinUIEx;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace autoRay
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        bool v2rayNRunning = false;

        public MainWindow()
        {
            this.InitializeComponent();
            WindowManager.Get(this).IsMinimizable = false;
            WindowManager.Get(this).IsMaximizable = false;
            WindowManager.Get(this).IsResizable = false;
            WindowManager.Get(this).IsAlwaysOnTop = true;
            AppWindow.Resize(new SizeInt32(495, 325));
            this.ExtendsContentIntoTitleBar = true;  // enable custom titlebar
            this.SetTitleBar(AppTitleBar);      // set user ui element as titlebar
            IsV2rayNRunning();
        }

        void action_Click(object sender, RoutedEventArgs e)
        {
            if (v2rayNRunning == true)
            {
                // Create a process object targeting "taskkill"
                Process process = new Process();
                process.StartInfo.FileName = "taskkill";

                // Add arguments to forcefully terminate v2rayN.exe
                process.StartInfo.Arguments = "/f /im v2rayN.exe";
                process.Start();
                process.WaitForExit();
                Environment.Exit(0);
            }
            else if (v2rayNRunning == false)
            {
                // Define the process start information
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"D:\v2rayN\v2rayN.exe"; // Use verbatim string for paths with special characters

                // Start the process
                Process.Start(startInfo);
                Environment.Exit(0);
            }
        }

        public void IsV2rayNRunning()
        {
            // Get all running processes
            var processes = Process.GetProcesses();

            // Check each process for the name "v2rayN.exe"
            foreach (var process in processes)
            {
                if (process.ProcessName.ToLower().Contains("v2ray"))
                {
                    statusDot.Fill = new SolidColorBrush(Colors.Green);
                    statusText.Text = " v2rayN is running.";
                    action.Content = "Stop";
                    v2rayNRunning = true;
                    //Debug.WriteLine("v2rayN is running.");
                    return;
                }
            }

            // No matching process found
            statusDot.Fill = new SolidColorBrush(Colors.Red);
            statusText.Text = " v2rayN isn't running.";
            action.Content = "Run";
            v2rayNRunning = false;
            //Debug.WriteLine("v2rayN isn't running.");
            return;
        }

        

    }
}