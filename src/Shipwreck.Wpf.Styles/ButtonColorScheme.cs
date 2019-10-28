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
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff0069d9, 0xff0062cc),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff0062cc, 0xff005cbf),
            Disabled = ButtonColorSchemeState.Create(0xa6ffffff, 0xa6007bff, 0xa6007bff),
            Default = ButtonColorSchemeState.Create(0xffffffff, 0xff007bff, 0xff007bff),
        };

        public static ButtonColorScheme BootstrapOutlinePrimary { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff007bff, 0xff007bff),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff007bff, 0xff007bff),
            Disabled = ButtonColorSchemeState.Create(0xa6007bff, 0xa6000000, 0xa6007bff),
            Default = ButtonColorSchemeState.Create(0xff007bff, 0xff000000, 0xff007bff),
        };

        public static ButtonColorScheme BootstrapSecondary { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff5a6268, 0xff545b62),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff545b62, 0xff4e555b),
            Disabled = ButtonColorSchemeState.Create(0xa6ffffff, 0xa66c757d, 0xa66c757d),
            Default = ButtonColorSchemeState.Create(0xffffffff, 0xff6c757d, 0xff6c757d),
        };

        public static ButtonColorScheme BootstrapOutlineSecondary { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff6c757d, 0xff6c757d),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff6c757d, 0xff6c757d),
            Disabled = ButtonColorSchemeState.Create(0xa66c757d, 0xa6000000, 0xa66c757d),
            Default = ButtonColorSchemeState.Create(0xff6c757d, 0xff000000, 0xff6c757d),
        };

        public static ButtonColorScheme BootstrapSuccess { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff218838, 0xff1e7e34),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff1e7e34, 0xff1c7430),
            Disabled = ButtonColorSchemeState.Create(0xa6ffffff, 0xa628a745, 0xa628a745),
            Default = ButtonColorSchemeState.Create(0xffffffff, 0xff28a745, 0xff28a745),
        };

        public static ButtonColorScheme BootstrapOutlineSuccess { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff28a745, 0xff28a745),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff28a745, 0xff28a745),
            Disabled = ButtonColorSchemeState.Create(0xa628a745, 0xa6000000, 0xa628a745),
            Default = ButtonColorSchemeState.Create(0xff28a745, 0xff000000, 0xff28a745),
        };

        public static ButtonColorScheme BootstrapDanger { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xffc82333, 0xffbd2130),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xffbd2130, 0xffb21f2d),
            Disabled = ButtonColorSchemeState.Create(0xa6ffffff, 0xa6dc3545, 0xa6dc3545),
            Default = ButtonColorSchemeState.Create(0xffffffff, 0xffdc3545, 0xffdc3545),
        };

        public static ButtonColorScheme BootstrapOutlineDanger { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xffdc3545, 0xffdc3545),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xffdc3545, 0xffdc3545),
            Disabled = ButtonColorSchemeState.Create(0xa6dc3545, 0xa6000000, 0xa6dc3545),
            Default = ButtonColorSchemeState.Create(0xffdc3545, 0xff000000, 0xffdc3545),
        };

        public static ButtonColorScheme BootstrapWarning { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xff212529, 0xffe0a800, 0xffd39e00),
            Pressed = ButtonColorSchemeState.Create(0xff212529, 0xffd39e00, 0xffc69500),
            Disabled = ButtonColorSchemeState.Create(0xa6212529, 0xa6ffc107, 0xa6ffc107),
            Default = ButtonColorSchemeState.Create(0xff212529, 0xffffc107, 0xffffc107),
        };

        public static ButtonColorScheme BootstrapOutlineWarning { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xff212529, 0xffffc107, 0xffffc107),
            Pressed = ButtonColorSchemeState.Create(0xff212529, 0xffffc107, 0xffffc107),
            Disabled = ButtonColorSchemeState.Create(0xa6ffc107, 0xa6000000, 0xa6ffc107),
            Default = ButtonColorSchemeState.Create(0xffffc107, 0xff000000, 0xffffc107),
        };

        public static ButtonColorScheme BootstrapLight { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xff212529, 0xffe2e6ea, 0xffdae0e5),
            Pressed = ButtonColorSchemeState.Create(0xff212529, 0xffdae0e5, 0xffd3d9df),
            Disabled = ButtonColorSchemeState.Create(0xa6212529, 0xa6f8f9fa, 0xa6f8f9fa),
            Default = ButtonColorSchemeState.Create(0xff212529, 0xfff8f9fa, 0xfff8f9fa),
        };

        public static ButtonColorScheme BootstrapOutlineLight { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xff212529, 0xfff8f9fa, 0xfff8f9fa),
            Pressed = ButtonColorSchemeState.Create(0xff212529, 0xfff8f9fa, 0xfff8f9fa),
            Disabled = ButtonColorSchemeState.Create(0xa6f8f9fa, 0xa6000000, 0xa6f8f9fa),
            Default = ButtonColorSchemeState.Create(0xfff8f9fa, 0xff000000, 0xfff8f9fa),
        };

        public static ButtonColorScheme BootstrapDark { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff23272b, 0xff1d2124),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff1d2124, 0xff171a1d),
            Disabled = ButtonColorSchemeState.Create(0xa6ffffff, 0xa6343a40, 0xa6343a40),
            Default = ButtonColorSchemeState.Create(0xffffffff, 0xff343a40, 0xff343a40),
        };

        public static ButtonColorScheme BootstrapOutlineDark { get; } = new ButtonColorScheme
        {
            MouseOver = ButtonColorSchemeState.Create(0xffffffff, 0xff343a40, 0xff343a40),
            Pressed = ButtonColorSchemeState.Create(0xffffffff, 0xff343a40, 0xff343a40),
            Disabled = ButtonColorSchemeState.Create(0xa6343a40, 0xa6000000, 0xa6343a40),
            Default = ButtonColorSchemeState.Create(0xff343a40, 0xff000000, 0xff343a40),
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
