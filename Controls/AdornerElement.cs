using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using StandardTest.Data;

namespace StandardTest.Controls;

/// <summary>
/// 装饰器元素
/// </summary>
public abstract class AdornerElement : Control, IDisposable
{
    /// <summary>
    /// 目标元素
    /// </summary>
    protected FrameworkElement ElementTarget { get; set; }

    /// <summary>
    /// 目标属性
    /// </summary>
    public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
        nameof(Target), typeof(FrameworkElement), typeof(AdornerElement),
        new PropertyMetadata(default(FrameworkElement), OnTargetChanged));

    /// <summary>
    /// 目标属性改变事件
    /// </summary>
    /// <param name="d">依赖对象</param>
    /// <param name="e">属性改变事件参数</param>
    private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (AdornerElement)d;
        ctl.OnTargetChanged(ctl.ElementTarget, false);
        ctl.OnTargetChanged((FrameworkElement)e.NewValue, true);
    }

    /// <summary>
    /// 目标元素
    /// </summary>
    [Bindable(true), Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FrameworkElement Target
    {
        get => (FrameworkElement)GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    /// <summary>
    /// 实例属性
    /// </summary>
    public static readonly DependencyProperty InstanceProperty = DependencyProperty.RegisterAttached(
        "Instance", typeof(AdornerElement), typeof(AdornerElement),
        new PropertyMetadata(default(AdornerElement), OnInstanceChanged));

    /// <summary>
    /// 实例属性改变事件
    /// </summary>
    /// <param name="d">依赖对象</param>
    /// <param name="e">属性改变事件参数</param>
    private static void OnInstanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement target) return;
        var element = (AdornerElement)e.NewValue;
        element.OnInstanceChanged(target);
    }

    /// <summary>
    /// 实例属性改变事件
    /// </summary>
    /// <param name="target">目标元素</param>
    protected virtual void OnInstanceChanged(FrameworkElement target) => Target = target;

    /// <summary>
    /// 设置实例
    /// </summary>
    /// <param name="element">依赖对象</param>
    /// <param name="value">实例</param>
    public static void SetInstance(DependencyObject element, AdornerElement value)
        => element.SetValue(InstanceProperty, value);

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <param name="element">依赖对象</param>
    /// <returns>实例</returns>
    public static AdornerElement GetInstance(DependencyObject element)
        => (AdornerElement)element.GetValue(InstanceProperty);

    /// <summary>
    /// 是否实例
    /// </summary>
    /// <remarks>用于标记是否为实例，用于区分是否为模板</remarks>
    public static readonly DependencyProperty IsInstanceProperty = DependencyProperty.RegisterAttached(
        "IsInstance", typeof(bool), typeof(AdornerElement), new PropertyMetadata(ValueBoxes.TrueBox));

    /// <summary>
    /// 设置是否实例
    /// </summary>
    /// <param name="element">依赖对象</param>
    /// <param name="value">是否实例</param>
    /// <remarks>用于标记是否为实例，用于区分是否为模板</remarks>
    public static void SetIsInstance(DependencyObject element, bool value)
        => element.SetValue(IsInstanceProperty, ValueBoxes.BooleanBox(value));

    /// <summary>
    /// 获取是否实例
    /// </summary>
    /// <param name="element">依赖对象</param>
    /// <returns>是否实例</returns>
    public static bool GetIsInstance(DependencyObject element)
        => (bool)element.GetValue(IsInstanceProperty);

    /// <summary>
    /// 是否模板
    /// </summary>
    /// <remarks>用于标记是否为模板，用于区分是否为实例</remarks>
    protected virtual void OnTargetChanged(FrameworkElement element, bool isNew)
    {
        if (element == null) return;

        if (!isNew)
        {
            element.Unloaded -= TargetElement_Unloaded;
            ElementTarget = null;
        }
        else
        {
            element.Unloaded += TargetElement_Unloaded;
            ElementTarget = element;
        }
    }

    /// <summary>
    /// 目标元素卸载事件
    /// </summary>
    /// <param name="sender">目标元素</param>
    /// <param name="e">路由事件参数</param>
    /// <remarks>用于卸载实例</remarks>
    private void TargetElement_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element)
        {
            element.Unloaded -= TargetElement_Unloaded;
            Dispose();
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <remarks>用于释放资源</remarks>
    protected abstract void Dispose();

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <remarks>用于释放资源</remarks>
    void IDisposable.Dispose() => Dispose();
}