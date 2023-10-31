using System;
using System.Windows;
using System.Windows.Controls;

namespace StandardTest.Controls;

public class CustomerButton : Button
{
    public Type? ModalWindow { get; set; }

    // 创建激发事件
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