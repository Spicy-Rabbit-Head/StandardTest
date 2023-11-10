using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using StandardTest.Data;
using StandardTest.Model;
using StandardTest.Tools;

namespace StandardTest.ViewModel;

public class MainViewModel : ObservableObject
{
    /// <summary>
    /// 配置工具
    /// </summary>
    private readonly ConfigUtis configUtis = new();

    /// <summary>
    /// 连接属性
    /// </summary>
    private ConnectionProperties connectionProperties = new();

    /// <summary>
    /// 连接属性属性
    /// </summary>
    public ConnectionProperties ConnectionProperties
    {
        get => connectionProperties;
        set => SetField(ref connectionProperties, value);
    }

    /// <summary>
    /// 构造
    /// </summary>
    public MainViewModel()
    {
        ChangeQccFileCommand = new ObservableCommand<string>(ChangeQccFile);
        InitializingServices();
    }

    /// <summary>
    /// 初始化服务
    /// </summary>
    private void InitializingServices()
    {
        var baseConfig = configUtis.GetValueBySection("Base");
        if (baseConfig == null)
        {
            var initializedOrNot = configUtis.InitializeConfigurationFile();
            baseConfig = configUtis.GetValueBySection("Base");
        }

        ConnectionProperties.IpText = baseConfig?["IP"];
        ConnectionProperties.PortText = baseConfig?["Port"];
        ConnectionProperties.QccPath = baseConfig?["QccPath"];

        var specificationListStr = configUtis.GetValueBySection("SpecificationList");
        if (specificationListStr == null)
        {
            ConnectionProperties.SpecificationList = new List<Specification>();
        }

        var specificationList = (from v in specificationListStr
            let strings = v.Value.Split(':')
            select new Specification()
                { Name = v.KeyName, Start = ushort.Parse(strings[0]), End = ushort.Parse(strings[1]) }).ToList();

        ConnectionProperties.SpecificationList = specificationList;
    }

    /// <summary>
    /// 关闭服务
    /// </summary>
    public void CloseService()
    {
    }

    public ObservableCommand<string> ChangeQccFileCommand { get; set; }

    /// <summary>
    /// 保存QCC文件路径
    /// </summary>
    /// <param name="qccPath">QCC文件路径</param>
    private void ChangeQccFile(string qccPath)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "QCC文件|*.qcc",
            Multiselect = false
        };

        var result = openFileDialog.ShowDialog();
        if (result != true) return;
        ConnectionProperties.QccPath = openFileDialog.FileName;
        configUtis.SetValueBySectionAndKey("Base", "QccPath", openFileDialog.FileName);

        var childThread = new Thread(() =>
        {
            if (Measuration.RestartService(openFileDialog.FileName)) return;
            MessageBox.Show("启动服务失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        });

        childThread.Start();
    }
}