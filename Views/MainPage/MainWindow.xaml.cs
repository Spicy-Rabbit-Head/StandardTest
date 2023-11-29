using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using ModbusTcp;
using StandardTest.Tools;
using StandardTest.ViewModel;

namespace StandardTest.Views.MainPage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 主视图模型
        /// </summary>
        private readonly MainViewModel mainViewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        /// <summary>
        /// 主窗口关闭时询问是否退出
        /// </summary>
        /// <param name="e">取消事件参数</param>
        protected override void OnClosing(CancelEventArgs e)
        {
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
            Measuration.StopService();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 主窗口加载完成后
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            var childThread = new Thread(() =>
            {
                if (Measuration.StartService(mainViewModel.ConnectionProperties.QccPath)) return;
                MessageBox.Show("启动服务失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            });

            childThread.Start();
        }
    }
}