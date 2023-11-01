using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace StandardTest.Views.MainPage;

/// <summary>
/// 运行参数 View
/// </summary>
public partial class RunningParameter : UserControl
{
    // 当前选择的 QCC 文件
    private string currentPartNumber = string.Empty;

    // QCC 文件截取正则
    private readonly Regex regex = new(@"([^\\]+)(?=[\\.]qcc$)");

    public RunningParameter()
    {
        InitializeComponent();
        if (currentPartNumber == string.Empty)
        {
            NumberShow.Text = "当前未选择QCC文件";
        }
        else
        {
            NumberShow.Text = currentPartNumber;
        }
        SpecificationSelection.ItemsSource = new List<string> { "1", "2", "3", "4", "5" };
    }

    /// <summary>
    /// 切换 QCC
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">事件</param>
    private void ChangeQcc(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "QCC文件|*.qcc",
            Multiselect = false
        };

        var result = openFileDialog.ShowDialog();
        if (result != true) return;
        var fileName = openFileDialog.FileName;
        currentPartNumber = fileName;
        var value = regex.Match(fileName).Value;
        NumberShow.Text = value;
    }
}