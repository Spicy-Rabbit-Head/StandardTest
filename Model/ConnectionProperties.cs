using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using StandardTest.Data;
using StandardTest.Tools;

namespace StandardTest.Model;

/// <summary>
/// 主页模型
/// </summary>
public class ConnectionProperties : ObservableObject
{
    // QCC 文件截取正则
    private readonly Regex regex = new(@"([^\\]+)(?=[\\.]qcc$)");

    /// <summary>
    /// IP地址
    /// </summary>
    private string? ipText;

    /// <summary>
    /// IP地址属性
    /// </summary>
    public string? IpText
    {
        get => ipText;
        set => SetField(ref ipText, value);
    }

    /// <summary>
    /// 端口号
    /// </summary>
    private string? portText;

    /// <summary>
    /// 端口号属性
    /// </summary>
    public string? PortText
    {
        get => portText;
        set => SetField(ref portText, value);
    }

    /// <summary>
    /// 当前选择的 QCC 文件
    /// </summary>
    private string? currentQccFile = string.Empty;

    /// <summary>
    /// 当前选择的 QCC 文件属性
    /// </summary>
    public string? CurrentQccFile
    {
        get => currentQccFile;
        set => SetField(ref currentQccFile, value);
    }

    /// <summary>
    /// QCC地址
    /// </summary>
    private string? qccPath;

    /// <summary>
    /// QCC地址属性
    /// </summary>
    public string? QccPath
    {
        get => qccPath;
        set
        {
            if (value != null)
            {
                SetField(ref qccPath, value);
                CurrentQccFile = regex.Match(value).Value;
            }
            else
            {
                SetField(ref qccPath, "未选择");
                CurrentQccFile = "未选择";
            }
        }
    }

    /// <summary>
    /// 规则名称列表
    /// </summary>
    private List<string>? specificationNameList;

    /// <summary>
    /// 规则名称列表属性
    /// </summary>
    public List<string>? SpecificationNameList
    {
        get => specificationNameList;
        set => SetField(ref specificationNameList, value);
    }

    /// <summary>
    /// 规格列表
    /// </summary>
    private List<Specification>? specificationList;

    /// <summary>
    /// 规格列表属性
    /// </summary>
    public List<Specification>? SpecificationList
    {
        get => specificationList;
        set
        {
            SetField(ref specificationList, value);
            if (value != null)
            {
                var list = value.Select(v => v.Name)!.ToList<string>();
                SpecificationNameList = list;
            }
            else
            {
                SpecificationNameList = new List<string>();
            }
        }
    }
}