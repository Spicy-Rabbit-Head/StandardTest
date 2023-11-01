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

        /// <summary>
        /// 主窗口关闭时询问是否退出
        /// </summary>
        /// <param name="e">取消事件参数</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Console.WriteLine("关闭窗口");
            var result = MessageBox.Show("确定是退出吗？", "询问", MessageBoxButton.YesNo, MessageBoxImage.Question);
            e.Cancel = result != MessageBoxResult.Yes;
        }

        /// <summary>
        /// 主窗口关闭后关闭程序
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Console.WriteLine("关闭程序");
            Application.Current.Shutdown();
        }
    }
}