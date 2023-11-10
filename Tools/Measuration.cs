using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace StandardTest.Tools;

public static class Measuration
{
    /// <summary>
    /// 250B接口地址
    /// </summary>
    private const string DllUrl = "C:\\Program Files (x86)\\Saunders & Associates\\250B\\W250BOLE.dll";

    /// <summary>
    /// 正则规则
    /// </summary>
    private static readonly string[] Regular = { "=", "(", "),", ")" };

    /// <summary>
    /// 250BPort    
    /// </summary>
    private const int Port = 1;

    /// <summary>
    /// 启动服务
    /// </summary>
    /// <param name="path">QCC文件路径</param>
    /// <returns>是否成功</returns>
    public static bool StartService(string? path)
    {
        try
        {
            var result = ConnectToServer();
            if (result != 0) return false;
            if (path == null) return false;
            var fileOpen = FileOpenB(path);
            return fileOpen == 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 停止服务
    /// </summary>
    /// <returns>是否成功</returns>
    public static bool StopService()
    {
        try
        {
            var result = DisconnectFromServer();
            return result == 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 重启服务
    /// </summary>
    /// <param name="path">QCC文件路径</param>
    /// <returns>是否成功</returns>
    public static bool RestartService(string? path)
    {
        try
        {
            var result = StopService();
            Thread.Sleep(100);
            return result && StartService(path);
        }
        catch
        {
            return false;
        }
    }

    // 250B公共接口

    // 启动250B服务器
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int ConnectToServer();

    // 停止250B服务器
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int DisconnectFromServer();

    // 端口切换
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int SelectPort(int nIsPortA, int nPortIndex);

    // 单次测试
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int Measure();

    // 单次测试-指定250B
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int Measure(int nMeasure250B1, int nMeasure250B2, int nMeasure250B3, int nMeasure250B4,
        ref int pn250BsTriggered);

    // 校准短路界面
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int CalibrateShortB(int nIsPortA, int nPortIndex, string bstrFixture, ref int pnSuccess);

    // 校准负载界面
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int CalibrateLoadB(int nIsPortA, int nPortIndex, string bstrFixture, ref int pnSuccess);

    // 校准开路界面 
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int CalibrateOpenB(int nIsPortA, int nPortIndex, string bstrFixture, ref int pnSuccess);

    // 保存校准
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int SaveCalibration(int nIsPortA, int nPortIndex, ref int pnSuccess);

    // 取消校准
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int CancelCalibration(int nIsPortA, int nPortIndex, ref int pnSuccess);

    // 清除测量
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int ClearAllMeasurements();

    // 验证校准
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int VerifyCalibrationB(int nPromptForLoadResistor, ref string pbstrResult);

    // 测量并返回数据
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int MeasureAndGetResultsB(int nMeasure250B1AndGetResults,
        [MarshalAs(UnmanagedType.BStr)] ref string pbstr250B1Results);

    // 测量并返回数据-指定250B
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int MeasureAndGetResultsB(int nMeasure250B1AndGetResults,
        [MarshalAs(UnmanagedType.BStr)] ref string pbstr250B1Results, int nMeasure250B2AndGetResults,
        [MarshalAs(UnmanagedType.BStr)] ref string pbstr250B2Results, int nMeasure250B3AndGetResults,
        [MarshalAs(UnmanagedType.BStr)] ref string pbstr250B3Results, int nMeasure250B4AndGetResults,
        [MarshalAs(UnmanagedType.BStr)] ref string pbstr250B4Results);

    // 打开或改变QCC文件
    [DllImport(DllUrl, CharSet = CharSet.Unicode)]
    private static extern int FileOpenB(string? bstrQccFilePath);
}