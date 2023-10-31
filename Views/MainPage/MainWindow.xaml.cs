using System;
using System.ComponentModel;
using System.Windows;

namespace StandardTest.Views.MainPage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Console.WriteLine("关闭窗口");
            base.OnClosing(e);
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            Console.WriteLine("关闭程序");
            Application.Current.Shutdown();
        }
    }
}