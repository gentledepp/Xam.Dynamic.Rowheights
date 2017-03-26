using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls
{
    public enum CheckboxVariant { Box, Radio, Custom }

    public partial class Checkbox
    {
        private const string DefaultBoxCheckedText = "fa-check-square-o";
        private const string DefaultBoxUncheckedText = "fa-square-o";
        private const string DefaultRadioCheckedText = "fa-dot-circle-o";
        private const string DefaultRadioUncheckedText = "fa-circle-o";
        private const double DefaultFontSize = 20;

        public CheckboxVariant Variant
        {
            get { return (CheckboxVariant) GetValue(VariantProperty); }
            set
            {
                SetValue(VariantProperty, value);
                UpdateButtonText();
            }
        }

        public string CheckedText
        {
            get { return (string) GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }

        public string UncheckedText
        {
            get { return (string) GetValue(UncheckedTextProperty); }
            set { SetValue(UncheckedTextProperty, value); }
        }

        public string LabelText
        {
            get { return (string) GetValue(LabelTextProperty); }
            set
            {
                SetValue(LabelTextProperty, value);
                OnPropertyChanged();
            }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public Command ButtonCommand
        {
            get { return _buttonCommand; }
            set
            {
                if (_buttonCommand == value) return;
                _buttonCommand = value;
                OnPropertyChanged();
            }
        }

        public bool IsChecked
        {
            get { return (bool) GetValue(IsCheckedProperty); }
            set
            {
                if (IsChecked == value) return;
                SetValue(IsCheckedProperty, value);
                OnPropertyChanged();
                CheckedChanged?.Invoke(this, IsChecked);
            }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (Math.Abs(_fontSize - value) < 0.1) return;
                _fontSize = value;
                OnPropertyChanged();

                // good width for font size 20: 32 = 20 * 1.6
                //             -"-          30: 48
                CheckboxWidth = value * 1.6;
            }
        }

        public double CheckboxWidth
        {
            get { return _checkboxWidth; }
            set
            {
                if (Math.Abs(_checkboxWidth - value) < 0.1) return;
                _checkboxWidth = value;
                OnPropertyChanged();
            }
        }

        #region Bindable properties

        public static readonly BindableProperty VariantProperty = BindableProperty.Create(nameof(Variant), typeof(CheckboxVariant), typeof(Checkbox), CheckboxVariant.Box, propertyChanged: CheckboxVariantPropertyChanged);

        private static void CheckboxVariantPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            if (checkbox == null) return;

            switch (checkbox.Variant)
            {
                case CheckboxVariant.Box:
                    checkbox.CheckedText = DefaultBoxCheckedText;
                    checkbox.UncheckedText = DefaultBoxUncheckedText;
                    break;
                case CheckboxVariant.Radio:
                    checkbox.CheckedText = DefaultRadioCheckedText;
                    checkbox.UncheckedText = DefaultRadioUncheckedText;
                    break;
            }

            checkbox.UpdateButtonText();
        }

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(Checkbox), propertyChanged: LabelTextPropertyChanged);

        private static void LabelTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            if (checkbox == null) return;

            checkbox.LabelText = newvalue as string;
        }

        public static readonly BindableProperty CheckedTextProperty = BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(Checkbox), propertyChanged: CheckedTextPropertyChanged);

        private static void CheckedTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            if (checkbox == null) return;

            checkbox.CheckedText = newvalue as string;
            checkbox.UpdateVariant();
        }

        public static readonly BindableProperty UncheckedTextProperty = BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(Checkbox), propertyChanged: UncheckedTextPropertyChanged);

        private static void UncheckedTextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            if (checkbox == null) return;

            checkbox.UncheckedText = newvalue as string;
            checkbox.UpdateVariant();
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(Checkbox), false, propertyChanged: IsCheckedPropertyChanged, defaultBindingMode: BindingMode.TwoWay);

        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            checkbox?.UpdateButtonText();
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
            typeof(double), typeof(Checkbox), 0d, propertyChanged: FontSizePropertyChanged);

        private string _buttonText;
        private double _checkboxWidth;
        private double _fontSize;
        private Command _buttonCommand;
        private IDisposable _propertyChangedHandler;

        private static void FontSizePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as Checkbox;

            if (checkbox == null) return;

            checkbox.FontSize = (double) newvalue;
        }

        #endregion

        public event EventHandler<bool> CheckedChanged;

        public Checkbox()
        {

            _propertyChangedHandler = Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>
            (
                h => (_, args) => h(args),
                h => PropertyChanged += h,
                h => PropertyChanged -= h
            ).Subscribe(args =>
            {
                if (args.PropertyName == nameof(IsEnabled))
                {
                    var isEnabled = (bool) GetValue(IsEnabledProperty);
                    SetValue(OpacityProperty, isEnabled ? 1 : 0.5);
                }
            });

            CheckedText = DefaultBoxCheckedText;
            UncheckedText = DefaultBoxUncheckedText;
            ButtonText = UncheckedText;
            ButtonCommand = new Command(ToggleChecked);
            FontSize = DefaultFontSize;

            InitializeComponent();
        }

        ~Checkbox()
        {
            _propertyChangedHandler?.Dispose();
        }

        #region Private helpers

        private void ToggleChecked()
        {
            IsChecked = !IsChecked;
        }

        private void UpdateVariant()
        {
            CheckboxVariant checkedVariant;
            CheckboxVariant uncheckedVariant;

            // get the variant of checked text

            switch (CheckedText)
            {
                case DefaultBoxCheckedText:
                    checkedVariant = CheckboxVariant.Box;
                    break;
                case DefaultRadioCheckedText:
                    checkedVariant = CheckboxVariant.Radio;
                    break;
                default:
                    checkedVariant = CheckboxVariant.Custom;
                    break;
            }

            // get the variant of unchecked text

            switch (UncheckedText)
            {
                case DefaultBoxUncheckedText:
                    uncheckedVariant = CheckboxVariant.Box;
                    break;
                case DefaultRadioUncheckedText:
                    uncheckedVariant = CheckboxVariant.Radio;
                    break;
                default:
                    uncheckedVariant = CheckboxVariant.Custom;
                    break;
            }

            // check if both texts are the same variant and update accordingly
            Variant = checkedVariant == uncheckedVariant ? checkedVariant : CheckboxVariant.Custom;
        }

        private void UpdateButtonText()
        {
            ButtonText = IsChecked ? CheckedText : UncheckedText;
            // Force redraw (hack for UWP)
            Grid?.ForceLayout();
        }

        #endregion
    }
}
