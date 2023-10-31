using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StandardTest.Data;
using StandardTest.Tools;

namespace StandardTest.Controls;

/// <summary>
/// 边框元素
/// </summary>
public class BorderElement
{
    /// <summary>
    /// 边框圆角
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius", typeof(CornerRadius), typeof(BorderElement),
        new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// 设置边框圆角
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) =>
        element.SetValue(CornerRadiusProperty, value);

    /// <summary>
    /// 获取边框圆角
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static CornerRadius GetCornerRadius(DependencyObject element) =>
        (CornerRadius)element.GetValue(CornerRadiusProperty);

    /// <summary>
    /// 边框圆角转换器
    /// </summary>
    public static readonly DependencyProperty CircularProperty = DependencyProperty.RegisterAttached(
        "Circular", typeof(bool), typeof(BorderElement), new PropertyMetadata(ValueBoxes.FalseBox, OnCircularChanged));

    /// <summary>
    /// 边框圆角转换器
    /// </summary>
    /// <param name="d">依赖对象</param>
    /// <param name="e">依赖属性改变事件</param>
    private static void OnCircularChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Border border)
        {
            if ((bool)e.NewValue)
            {
                var binding = new MultiBinding
                {
                    Converter = new BorderCircularConverter()
                };
                binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = border });
                binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = border });
                border.SetBinding(Border.CornerRadiusProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding(border, FrameworkElement.ActualWidthProperty);
                BindingOperations.ClearBinding(border, FrameworkElement.ActualHeightProperty);
                BindingOperations.ClearBinding(border, Border.CornerRadiusProperty);
            }
        }
    }

    /// <summary>
    /// 设置圆形
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetCircular(DependencyObject element, bool value)
        => element.SetValue(CircularProperty, ValueBoxes.BooleanBox(value));

    /// <summary>
    /// 获取圆形
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static bool GetCircular(DependencyObject element)
        => (bool)element.GetValue(CircularProperty);
}