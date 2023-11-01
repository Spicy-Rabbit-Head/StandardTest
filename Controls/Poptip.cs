using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using StandardTest.Data;
using StandardTest.Data.Enum;
using StandardTest.Tools;

namespace StandardTest.Controls;

/// <summary>
/// 弹出提示
/// </summary>
public class Poptip : AdornerElement
{
    /// <summary>
    /// 弹出提示的模式
    /// </summary>
    private readonly Popup popup;

    /// <summary>
    /// 打开的定时器
    /// </summary>
    private DispatcherTimer openTimer;

    /// <summary>
    /// 初始化 <see cref="Poptip"/> 类的新实例。
    /// </summary>
    public Poptip()
    {
        popup = new Popup
        {
            AllowsTransparency = true,
            Child = this,
            Placement = PlacementMode.Relative
        };

        popup.SetBinding(DataContextProperty, new Binding(DataContextProperty.Name) { Source = this });
    }

    /// <summary>
    /// 获取或设置弹出提示的内容
    /// </summary>
    public static readonly DependencyProperty HitModeProperty = DependencyProperty.RegisterAttached(
        "HitMode", typeof(HitMode), typeof(Poptip), new PropertyMetadata(HitMode.HOVER));

    /// <summary>
    /// 设置弹出提示的内容
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetHitMode(DependencyObject element, HitMode value)
        => element.SetValue(HitModeProperty, value);

    /// <summary>
    /// 获取弹出提示的内容
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static HitMode GetHitMode(DependencyObject element)
        => (HitMode)element.GetValue(HitModeProperty);

    /// <summary>
    /// 获取或设置弹出提示的内容
    /// </summary>
    public HitMode HitMode
    {
        get => (HitMode)GetValue(HitModeProperty);
        set => SetValue(HitModeProperty, value);
    }

    /// <summary>
    /// 获取或设置弹出提示的内容
    /// </summary>
    /// <remarks>如果是实例化的弹出提示，那么这个值将会被忽略</remarks>
    public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached(
        "Content", typeof(object), typeof(Poptip), new PropertyMetadata(default, OnContentChanged));

    /// <summary>
    /// 获取或设置弹出提示的内容
    /// </summary>
    /// <param name="d">元素</param>
    /// <param name="e">参数</param>
    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Poptip) return;
        if (GetInstance(d) == null)
        {
            SetInstance(d, Default);
            SetIsInstance(d, false);
        }
    }

    /// <summary>
    /// 设置弹出提示的内容
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    /// <remarks>如果是实例化的弹出提示，那么这个值将会被忽略</remarks>
    public static void SetContent(DependencyObject element, object value)
        => element.SetValue(ContentProperty, value);

    /// <summary>
    /// 获取弹出提示的内容
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static object GetContent(DependencyObject element)
        => element.GetValue(ContentProperty);

    /// <summary>
    /// 内容
    /// </summary>
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// 内容模板属性
    /// </summary>
    public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(
        nameof(ContentTemplate), typeof(DataTemplate), typeof(Poptip), new PropertyMetadata(default(DataTemplate)));

    /// <summary>
    /// 内容模板
    /// </summary>
    public DataTemplate ContentTemplate
    {
        get => (DataTemplate)GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    /// <summary>
    /// 内容字符串格式化属性
    /// </summary>
    public static readonly DependencyProperty ContentStringFormatProperty = DependencyProperty.Register(
        nameof(ContentStringFormat), typeof(string), typeof(Poptip), new PropertyMetadata(default(string)));

    /// <summary>
    /// 内容字符串格式化
    /// </summary>
    public string ContentStringFormat
    {
        get => (string)GetValue(ContentStringFormatProperty);
        set => SetValue(ContentStringFormatProperty, value);
    }

    /// <summary>
    /// 内容模板选择器属性
    /// </summary>
    public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(
        nameof(ContentTemplateSelector), typeof(DataTemplateSelector), typeof(Poptip),
        new PropertyMetadata(default(DataTemplateSelector)));

    /// <summary>
    /// 内容模板选择器
    /// </summary>
    public DataTemplateSelector ContentTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty);
        set => SetValue(ContentTemplateSelectorProperty, value);
    }

    /// <summary>
    /// 内容字符串格式化属性
    /// </summary>
    public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
        "VerticalOffset", typeof(double), typeof(Poptip), new PropertyMetadata(ValueBoxes.Double0Box));

    /// <summary>
    /// 设置垂直偏移
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetVerticalOffset(DependencyObject element, double value)
    {
        element.SetValue(VerticalOffsetProperty, value);
    }

    /// <summary>
    /// 获取垂直偏移
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static double GetVerticalOffset(DependencyObject element)
    {
        return (double)element.GetValue(VerticalOffsetProperty);
    }

    /// <summary>
    /// 垂直偏移
    /// </summary>
    public double VerticalOffset
    {
        get => (double)GetValue(VerticalOffsetProperty);
        set => SetValue(VerticalOffsetProperty, value);
    }

    /// <summary>
    /// 水平偏移属性
    /// </summary>
    public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached(
        "HorizontalOffset", typeof(double), typeof(Poptip), new PropertyMetadata(ValueBoxes.Double0Box));

    /// <summary>
    /// 设置水平偏移
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetHorizontalOffset(DependencyObject element, double value)
    {
        element.SetValue(HorizontalOffsetProperty, value);
    }

    /// <summary>
    /// 获取水平偏移
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static double GetHorizontalOffset(DependencyObject element)
    {
        return (double)element.GetValue(HorizontalOffsetProperty);
    }

    /// <summary>
    /// 水平偏移
    /// </summary>
    public double HorizontalOffset
    {
        get => (double)GetValue(HorizontalOffsetProperty);
        set => SetValue(HorizontalOffsetProperty, value);
    }

    /// <summary>
    /// 弹出位置属性
    /// </summary>
    public static readonly DependencyProperty PlacementTypeProperty = DependencyProperty.RegisterAttached(
        "PlacementType", typeof(PlacementType), typeof(Poptip), new PropertyMetadata(PlacementType.TOP));

    /// <summary>
    /// 设置弹出位置
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetPlacement(DependencyObject element, PlacementType value)
        => element.SetValue(PlacementTypeProperty, value);

    /// <summary>
    /// 获取弹出位置
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static PlacementType GetPlacement(DependencyObject element)
        => (PlacementType)element.GetValue(PlacementTypeProperty);

    /// <summary>
    /// 弹出位置
    /// </summary>
    public PlacementType PlacementType
    {
        get => (PlacementType)GetValue(PlacementTypeProperty);
        set => SetValue(PlacementTypeProperty, value);
    }

    /// <summary>
    /// 是否打开属性
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.RegisterAttached(
        "IsOpen", typeof(bool), typeof(Poptip), new PropertyMetadata(ValueBoxes.FalseBox, OnIsOpenChanged));

    /// <summary>
    /// 是否打开
    /// </summary>
    /// <param name="d">依赖对象</param>
    /// <param name="e">参数</param>
    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Poptip poptip)
        {
            poptip.SwitchPoptip((bool)e.NewValue);
        }
        else
        {
            ((Poptip)GetInstance(d))?.SwitchPoptip((bool)e.NewValue);
        }
    }

    /// <summary>
    /// 设置是否打开
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="value">值</param>
    public static void SetIsOpen(DependencyObject element, bool value)
        => element.SetValue(IsOpenProperty, ValueBoxes.BooleanBox(value));

    /// <summary>
    /// 获取是否打开
    /// </summary>
    /// <param name="element">元素</param>
    /// <returns>值</returns>
    public static bool GetIsOpen(DependencyObject element)
        => (bool)element.GetValue(IsOpenProperty);

    /// <summary>
    /// 是否打开
    /// </summary>
    /// <returns>值</returns>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, ValueBoxes.BooleanBox(value));
    }

    /// <summary>
    /// 延时属性
    /// </summary>
    public static readonly DependencyProperty DelayProperty = DependencyProperty.Register(
        nameof(Delay), typeof(double), typeof(Poptip), new PropertyMetadata(1000.0),
        ValidateHelper.IsInRangeOfPosDoubleIncludeZero);

    /// <summary>
    /// 延时
    /// </summary>
    public double Delay
    {
        get => (double)GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    /// <summary>
    /// 是否实例化属性
    /// </summary>
    public static Poptip Default => new();

    /// <summary>
    /// 目标更改时调用
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="isNew">是否是新的</param>
    protected sealed override void OnTargetChanged(FrameworkElement element, bool isNew)
    {
        base.OnTargetChanged(element, isNew);

        if (element == null) return;

        if (!isNew)
        {
            element.MouseEnter -= Element_MouseEnter;
            element.MouseLeave -= Element_MouseLeave;
            element.GotFocus -= Element_GotFocus;
            element.LostFocus -= Element_LostFocus;
            ElementTarget = null;
        }
        else
        {
            element.MouseEnter += Element_MouseEnter;
            element.MouseLeave += Element_MouseLeave;
            element.GotFocus += Element_GotFocus;
            element.LostFocus += Element_LostFocus;
            ElementTarget = element;
            popup.PlacementTarget = ElementTarget;
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    protected override void Dispose() => SwitchPoptip(false);

    /// <summary>
    /// 更新位置
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">不支持的弹出位置</exception>
    private void UpdateLocation()
    {
        var targetWidth = Target.RenderSize.Width;
        var targetHeight = Target.RenderSize.Height;

        Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        var size = DesiredSize;

        var width = size.Width;
        var height = size.Height;

        var offsetX = .0;
        var offsetY = .0;

        var poptip = (Poptip)GetInstance(Target);
        var popupPlacement = poptip.PlacementType;
        var popupOffsetX = poptip.HorizontalOffset;
        var popupOffsetY = poptip.VerticalOffset;

        switch (popupPlacement)
        {
            case PlacementType.LEFT_TOP:
                break;
            case PlacementType.LEFT:
                offsetY = -(height - targetHeight) * 0.5;
                break;
            case PlacementType.LEFT_BOTTOM:
                offsetY = -(height - targetHeight);
                break;
            case PlacementType.TOP_LEFT:
                offsetX = width;
                offsetY = -height;
                break;
            case PlacementType.TOP:
                offsetX = (width + targetWidth) * 0.5;
                offsetY = -height;
                break;
            case PlacementType.TOP_RIGHT:
                offsetX = targetWidth;
                offsetY = -height;
                break;
            case PlacementType.RIGHT_TOP:
                offsetX = width + targetWidth;
                break;
            case PlacementType.RIGHT:
                offsetX = width + targetWidth;
                offsetY = -(height - targetHeight) * 0.5;
                break;
            case PlacementType.RIGHT_BOTTOM:
                offsetX = width + targetWidth;
                offsetY = -(height - targetHeight);
                break;
            case PlacementType.BOTTOM_LEFT:
                offsetX = width;
                offsetY = targetHeight;
                break;
            case PlacementType.BOTTOM:
                offsetX = (width + targetWidth) * 0.5;
                offsetY = targetHeight;
                break;
            case PlacementType.BOTTOM_RIGHT:
                offsetX = targetWidth;
                offsetY = targetHeight;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        popup.HorizontalOffset = offsetX + popupOffsetX;
        popup.VerticalOffset = offsetY + popupOffsetY;
    }
    
    /// <summary>
    /// 切换弹出提示
    /// </summary>
    /// <param name="isShow">是否显示</param>
    private void SwitchPoptip(bool isShow)
    {
        if (isShow)
        {
            if (!GetIsInstance(Target))
            {
                SetCurrentValue(ContentProperty, GetContent(Target));
                SetCurrentValue(PlacementTypeProperty, GetPlacement(Target));
                SetCurrentValue(HitModeProperty, GetHitMode(Target));
                SetCurrentValue(HorizontalOffsetProperty, GetHorizontalOffset(Target));
                SetCurrentValue(VerticalOffsetProperty, GetVerticalOffset(Target));
                SetCurrentValue(IsOpenProperty, GetIsOpen(Target));
            }

            popup.PlacementTarget = Target;
            UpdateLocation();
        }

        ResetTimer();

        var delay = Delay;
        if (!isShow || HitMode != HitMode.HOVER || MathHelper.IsVerySmall(delay))
        {
            popup.IsOpen = isShow;
            Target.SetCurrentValue(IsOpenProperty, isShow);
        }
        else
        {
            openTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(delay)
            };

            openTimer.Tick += OpenTimer_Tick;
            openTimer.Start();
        }
    }
    
    /// <summary>
    /// 重置定时器
    /// </summary>
    private void ResetTimer()
    {
        if (openTimer != null)
        {
            openTimer.Stop();
            openTimer = null;
        }
    }

    /// <summary>
    /// 打开定时器
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">参数</param>
    private void OpenTimer_Tick(object sender, EventArgs e)
    {
        popup.IsOpen = true;
        Target.SetCurrentValue(IsOpenProperty, true);

        ResetTimer();
    }
    
    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">参数</param>
    private void Element_MouseEnter(object sender, MouseEventArgs e)
    {
        var hitMode = GetIsInstance(Target) ? HitMode : GetHitMode(Target);
        if (hitMode != HitMode.HOVER) return;

        SwitchPoptip(true);
    }
    
    /// <summary>
    /// 鼠标离开
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">参数</param>
    private void Element_MouseLeave(object sender, MouseEventArgs e)
    {
        var hitMode = GetIsInstance(Target) ? HitMode : GetHitMode(Target);
        if (hitMode != HitMode.HOVER) return;

        SwitchPoptip(false);
    }
    
    /// <summary>
    /// 元素获得焦点
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">参数</param>
    private void Element_GotFocus(object sender, RoutedEventArgs e)
    {
        var hitMode = GetIsInstance(Target) ? HitMode : GetHitMode(Target);
        if (hitMode != HitMode.FOCUS) return;

        SwitchPoptip(true);
    }

    /// <summary>
    /// 元素失去焦点
    /// </summary>
    /// <param name="sender">寄件人</param>
    /// <param name="e">参数</param>
    private void Element_LostFocus(object sender, RoutedEventArgs e)
    {
        var hitMode = GetIsInstance(Target) ? HitMode : GetHitMode(Target);
        if (hitMode != HitMode.FOCUS) return;

        SwitchPoptip(false);
    }
}