using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls
{
    public class BindablePicker : Picker
    {
        public BindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(BindablePicker), null, BindingMode.TwoWay, null, OnSelectedItemChanged);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(BindablePicker), null, BindingMode.TwoWay, null, OnItemsSourceChanged);
        public static readonly BindableProperty DisplayPropertyProperty = BindableProperty.Create("DisplayProperty", typeof(string), typeof(BindablePicker), null, BindingMode.OneWay, null, OnDisplayPropertyChanged);
        private bool disableEvents;

        public event EventHandler<object> SelectionChanged; 

        public IList ItemsSource
        {
            get { return (IList) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                SelectionChanged?.Invoke(this, value);

                if (ItemsSource == null || SelectedItem == null) return;

                SelectedIndex = ItemsSource.IndexOf(SelectedItem);
            }
        }

        public string DisplayProperty
        {
            get { return (string) GetValue(DisplayPropertyProperty); }
            set { SetValue(DisplayPropertyProperty, value); }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (disableEvents) return;

            SelectedItem = SelectedIndex == -1 ? null : ItemsSource[SelectedIndex];
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker) bindable;
            picker.SelectedItem = newValue;
            if (picker.ItemsSource != null && picker.SelectedItem != null)
            {
                var count = 0;
                foreach (var obj in picker.ItemsSource)
                {
                    if (obj == picker.SelectedItem)
                    {
                        picker.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
            }
        }

        private static void OnDisplayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker) bindable;
            picker.DisplayProperty = (string) newValue;
            LoadItemsAndSetSelected(bindable);

        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker) bindable;

            picker.ItemsSource = (IList) newValue;

            LoadItemsAndSetSelected(bindable);
        }

        private static void LoadItemsAndSetSelected(BindableObject bindable)
        {
            var picker = (BindablePicker) bindable;
            if (picker.ItemsSource != null)
            {
                picker.disableEvents = true;
                picker.SelectedIndex = -1;
                picker.Items.Clear();
                var count = 0;
                foreach (var obj in picker.ItemsSource)
                {
                    if (obj == null) continue;

                    string value;
                    if (picker.DisplayProperty != null)
                    {
                        var prop = obj.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, picker.DisplayProperty, StringComparison.OrdinalIgnoreCase));
                        value = prop != null ? prop.GetValue(obj)?.ToString() : obj.ToString();
                    }
                    else
                    {
                        value = obj.ToString();
                    }
                    picker.Items.Add(value ?? string.Empty);
                    if (picker.SelectedItem != null)
                    {
                        if (picker.SelectedItem == obj)
                        {
                            picker.SelectedIndex = count;
                        }
                    }
                    count++;
                }
                picker.disableEvents = false;
            }
            else
            {
                picker.Items.Clear();
            }
        }
    }
}

