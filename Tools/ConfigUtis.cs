using System;
using IniParser;
using IniParser.Model;

namespace StandardTest.Tools;

public class ConfigUtis
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    private static readonly string ConfigPath = "config.ini";

    /// <summary>
    /// 配置文件数据
    /// </summary>
    private IniData? data;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ConfigUtis()
    {
        MakeItOperational();
    }

    /// <summary>
    /// 初始配置文件
    /// </summary>
    public bool InitializeConfigurationFile()
    {
        try
        {
            var parser = new FileIniDataParser();
            data = new IniData();
            data.Sections.AddSection("Base");
            data["Base"].AddKey("IP", "150.110.60.6");
            data["Base"].AddKey("Port", "502");
            data["SpecificationList"].AddKey("9x24", "0:14");
            parser.WriteFile(ConfigPath, data);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 读取配置文件
    /// </summary>
    private bool ReadConfigurationFile()
    {
        try
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile(ConfigPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 使其可操作
    /// </summary>
    /// <param name="found">是否创建</param>
    private void MakeItOperational(bool found = false)
    {
        if (data != null) return;
        if (ReadConfigurationFile()) return;
        if (found)
        {
            data ??= new IniData();
        }
    }


    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfigurationFile()
    {
        var parser = new FileIniDataParser();
        parser.WriteFile(ConfigPath, data);
    }

    /// <summary>
    /// 创建节点
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <returns>是否成功</returns>
    public bool CreateSection(string sectionName)
    {
        try
        {
            if (data != null && data.Sections.ContainsSection(sectionName))
            {
                return true;
            }

            data?.Sections.AddSection(sectionName);
            SaveConfigurationFile();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 删除节点
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <returns>是否成功</returns>
    public bool DeleteSection(string sectionName)
    {
        try
        {
            if (data != null && !data.Sections.ContainsSection(sectionName))
            {
                return true;
            }

            data?.Sections.RemoveSection(sectionName);
            SaveConfigurationFile();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 按节点获取值
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <returns>节点值</returns>
    public KeyDataCollection? GetValueBySection(string sectionName)
    {
        MakeItOperational();
        return data?[sectionName];
    }

    /// <summary>
    /// 按节点写入值
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <param name="value">键名称</param>
    /// <returns>是否成功</returns>
    public bool SetValueBySection(string sectionName, KeyData value)
    {
        try
        {
            data?[sectionName].SetKeyData(value);
            SaveConfigurationFile();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 按节点和键删除值
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <param name="keyName">键名称</param>
    /// <returns>是否成功</returns>
    public bool DeleteValueBySectionAndKey(string sectionName, string keyName)
    {
        try
        {
            data?[sectionName].RemoveKey(keyName);
            SaveConfigurationFile();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 按节点和键获取值
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <param name="keyName">键名称</param>
    /// <returns>键值</returns>
    public string? GetValueBySectionAndKey(string sectionName, string keyName)
    {
        return data?[sectionName][keyName];
    }

    /// <summary>
    /// 按节点和键写入值
    /// </summary>
    /// <param name="sectionName">节点名称</param>
    /// <param name="keyName">键名称</param>
    /// <param name="value">键值</param>
    /// <returns>是否成功</returns>
    public bool SetValueBySectionAndKey(string sectionName, string keyName, string value)
    {
        if (data != null) data[sectionName][keyName] = value;
        SaveConfigurationFile();
        return true;
    }
}