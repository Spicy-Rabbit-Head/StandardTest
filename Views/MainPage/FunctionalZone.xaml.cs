using System;
using System.Windows;
using System.Windows.Controls;

namespace StandardTest.Views.MainPage;

public partial class FunctionalZone : UserControl
{
    public FunctionalZone()
    {
        InitializeComponent();
    }


    /// <summary>
    /// 运行触发事件
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">事件</param>
    private void Run(object sender, RoutedEventArgs e)
    {
        RunButton.IsEnabled = false;
        PauseButton.IsEnabled = true;
        ResumeButton.IsEnabled = false;
        StopButton.IsEnabled = true;
        OriginButton.IsEnabled = false;
    }

    /// <summary>
    /// 暂停触发事件
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">事件</param>
    private void Pause(object sender, RoutedEventArgs e)
    {
        PauseButton.IsEnabled = false;
        ResumeButton.IsEnabled = true;
    }

    /// <summary>
    /// 继续触发事件
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">事件</param>
    private void Resume(object sender, RoutedEventArgs e)
    {
        PauseButton.IsEnabled = true;
        ResumeButton.IsEnabled = false;
    }

    /// <summary>
    /// 停止触发事件
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">事件</param>
    private void Stop(object sender, RoutedEventArgs e)
    {
        RunButton.IsEnabled = true;
        PauseButton.IsEnabled = false;
        ResumeButton.IsEnabled = false;
        StopButton.IsEnabled = false;
        OriginButton.IsEnabled = true;
    }
}