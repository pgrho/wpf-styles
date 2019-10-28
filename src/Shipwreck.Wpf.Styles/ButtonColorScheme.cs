using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Shipwreck.Wpf.Styles
{
    public sealed class ButtonColorScheme
    {
        #region BrushConverter

        private sealed class BrushConverter : IMultiValueConverter
        {
            private readonly int _Index;

            internal BrushConverter(int index)
            {
                _Index = index;
            }

            public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var c = values[0] as ButtonColorScheme;
                return (false.Equals(values[3]) ? c?.Disabled?.GetBrush(_Index) : null)
                    ?? (true.Equals(values[2]) ? c?.Pressed?.GetBrush(_Index) : null)
                    ?? (true.Equals(values[1]) ? c?.MouseOver?.GetBrush(_Index) : null)
                    ?? c?.Disabled?.GetBrush(_Index);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
                => new[] { Binding.DoNothing, Binding.DoNothing, Binding.DoNothing };
        }

        private static readonly BrushConverter _Foreground = new BrushConverter(ButtonColorSchemeState.FOREGROUND_INDEX);
        private static readonly BrushConverter _Background = new BrushConverter(ButtonColorSchemeState.BACKGROUND_INDEX);
        private static readonly BrushConverter _BorderBrush = new BrushConverter(ButtonColorSchemeState.BORDER_BRUSH_INDEX);

        #endregion BrushConverter

        [DefaultValue(null)]
        public ButtonColorSchemeState? Default { get; set; }

        [DefaultValue(null)]
        public ButtonColorSchemeState? MouseOver { get; set; }

        [DefaultValue(null)]
        public ButtonColorSchemeState? Pressed { get; set; }

        [DefaultValue(null)]
        public ButtonColorSchemeState? Disabled { get; set; }

        public static ButtonColorScheme GetScheme(ButtonBase obj)
            => (ButtonColorScheme)obj.GetValue(SchemeProperty);

        public static void SetScheme(ButtonBase obj, ButtonColorScheme value)
            => obj.SetValue(SchemeProperty, value);

        public static readonly DependencyProperty SchemeProperty
            = DependencyProperty.RegisterAttached("Scheme", typeof(ButtonColorScheme), typeof(ButtonColorScheme), new FrameworkPropertyMetadata(null, OnSchemeChanged));

        #region Bootstrap Colors

        public static ButtonColorScheme BootstrapPrimary { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffff, 0x0069d9, 0x0062cc),
            Pressed = ButtonColorSchemeState.Create(0xffffff, 0x0062cc, 0x005cbf),
            Disabled = ButtonColorSchemeState.Create(0xffffff, 0x007bff, 0x007bff),
            Default = ButtonColorSchemeState.Create(0xffffff, 0x00fbff, 0x00fbff)
        };

        public static ButtonColorScheme BootstrapSecondary { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffff, 0x5a6268, 0x545b62),
            Pressed = ButtonColorSchemeState.Create(0xffffff, 0x545b62, 0x4e555b),
            Disabled = ButtonColorSchemeState.Create(0xffffff, 0x6c757d, 0x6c757d),
            Default = ButtonColorSchemeState.Create(0xffffff, 0x545b62, 0x4e555b)
        };

        #endregion Bootstrap Colors

        private static void OnSchemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ButtonBase b)
            {
                if (e.NewValue == null)
                {
                    BindingOperations.ClearBinding(b, Control.ForegroundProperty);
                    BindingOperations.ClearBinding(b, Control.BackgroundProperty);
                    BindingOperations.ClearBinding(b, Control.BorderBrushProperty);
                }
                else
                {
                    static MultiBinding getBinding(BrushConverter converter)
                        => new MultiBinding()
                        {
                            Converter = converter,
                            Bindings =
                            {
                                new Binding(){ Path = new PropertyPath(SchemeProperty), RelativeSource = new RelativeSource(RelativeSourceMode.Self) },
                                new Binding(nameof(Button.IsMouseOver)) { RelativeSource = new RelativeSource(RelativeSourceMode.Self) },
                                new Binding(nameof(Button.IsPressed)) { RelativeSource = new RelativeSource(RelativeSourceMode.Self) },
                                new Binding(nameof(Button.IsEnabled)) { RelativeSource = new RelativeSource(RelativeSourceMode.Self) }
                            }
                        };

                    BindingOperations.SetBinding(b, Control.ForegroundProperty, getBinding(_Foreground));
                    BindingOperations.SetBinding(b, Control.BackgroundProperty, getBinding(_Background));
                    BindingOperations.SetBinding(b, Control.BorderBrushProperty, getBinding(_BorderBrush));
                }
            }
        }
    }
}
