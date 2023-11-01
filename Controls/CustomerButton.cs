using System;
using System.Windows;
using System.Windows.Controls;

namespace StandardTest.Controls;

/// <summary>
/// 客户区按钮
/// </summary>
public class CustomerButton : Button
{
    /// <summary>
    /// 模态窗口类型
    /// </summary>
    public Type? ModalWindow { get; set; }

    /// <summary>
    /// 重写OnClick方法
    /// </summary>
    protected override void OnClick()
    {
        // 调用父类的OnClick方法
        base.OnClick();

        // 通过反射创建窗口实例
        if (ModalWindow == null || Activator.CreateInstance(ModalWindow) is not Window window) return;
        window.Owner = Window.GetWindow(this);
        // 显示窗口
        window.ShowDialog();
    }
}