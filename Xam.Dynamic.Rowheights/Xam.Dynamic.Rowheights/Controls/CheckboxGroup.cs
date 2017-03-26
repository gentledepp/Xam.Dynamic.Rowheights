using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xam.Dynamic.Rowheights.ViewModels;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls
{
    internal class CheckboxGroup : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CheckboxGroup), null, propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty VariantProperty = BindableProperty.Create(nameof(Variant), typeof(CheckboxVariant), typeof(CheckboxGroup), CheckboxVariant.Box, propertyChanged: CheckboxVariantPropertyChanged);

        private static void CheckboxVariantPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var group = bindable as CheckboxGroup;

            if (group == null) return;

            foreach (var item in group.Children)
            {
                item.SetValue(Checkbox.VariantProperty, newvalue);
            }
        }

        public CheckboxVariant Variant
        {
            get { return (CheckboxVariant) GetValue(VariantProperty); }
            set { SetValue(VariantProperty, value); }
        }

        public static readonly BindableProperty MultiselectEnabledProperty = BindableProperty.Create(nameof(MultiselectEnabled), typeof(bool), typeof(CheckboxGroup), true, propertyChanged: MultiselectEnabledPropertyChanged);

        private static void MultiselectEnabledPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var group = bindable as CheckboxGroup;

            if (group == null) return;

            if (!(bool) newvalue)
            {
                // uncheck all but first
                foreach (var checkbox in group.Children.OfType<Checkbox>().Where(c => c.IsChecked).Skip(1))
                    checkbox.IsChecked = false;

                // register handles to uncheck other boxes when box was checked
                group.RegisterCheckedChangedEvent();
            }
            else
                group.DisposeRegisteredHandles();
        }

        public bool MultiselectEnabled
        {
            get { return (bool) GetValue(MultiselectEnabledProperty); }
            set
            {
                if (MultiselectEnabled == value) return;
                SetValue(MultiselectEnabledProperty, value);
            }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var group = (CheckboxGroup) bindable;
            group.Children.Clear();

            var answers = newvalue as IEnumerable;
            if (answers == null) return;

            group.DisposeRegisteredHandles();

            foreach (var answer in answers)
            {
                var checkbox = new Checkbox();
                checkbox.SetBinding(Checkbox.LabelTextProperty, nameof(AnswerViewModel.Title));
                checkbox.SetValue(Checkbox.VariantProperty, group.Variant);
                checkbox.SetBinding(Checkbox.IsCheckedProperty, nameof(AnswerViewModel.IsSelected), BindingMode.TwoWay);

                if (!group.MultiselectEnabled)
                    group.RegisterCheckedChangedEvent(checkbox);

                checkbox.BindingContext = answer;

                group.Children.Add(checkbox);
            }

            group.ForceLayout();
        }

        private void RegisterCheckedChangedEvent()
        {
            DisposeRegisteredHandles();
            foreach (var checkbox in Children.OfType<Checkbox>())
            {
                RegisterCheckedChangedEvent(checkbox);
            }
        }

        private void RegisterCheckedChangedEvent(Checkbox checkbox)
        {
            // capture other boxes for unchecking
            var otherBoxes = Children.OfType<Checkbox>().Where(c => c != checkbox);

            // subscribe to CheckedChanged and uncheck other boxes if box was checked
            var handle = Observable.FromEvent<EventHandler<bool>, bool>
            (
                h => (_, b) => h(b),
                h => checkbox.CheckedChanged += h,
                h => checkbox.CheckedChanged -= h
            ).Subscribe(b =>
            {
                if (!b) return;
                // uncheck other boxes when box changes
                foreach (var box in otherBoxes)
                {
                    box.IsChecked = false;
                }
            });

            // add handle to registered handles
            RegisteredHandles.Add(handle);
        }

        private void DisposeRegisteredHandles()
        {
            foreach (var handle in RegisteredHandles)
            {
                handle.Dispose();
            }
            RegisteredHandles.Clear();
        }

        public List<IDisposable> RegisteredHandles { get; } = new List<IDisposable>();
    }
}
