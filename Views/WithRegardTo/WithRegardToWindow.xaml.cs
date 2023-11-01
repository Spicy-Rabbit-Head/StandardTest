using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace StandardTest.Views.WithRegardTo;

public partial class WithRegardToWindow : Window
{
    public WithRegardToWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 跳转到GitHub
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">请求导航事件参数</param>
    private void JumpToGitHub(object sender, RequestNavigateEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }
    }
}