using System;
using System.Linq;
using System.Reactive.Linq;
using Xam.Dynamic.Rowheights.ViewModels;

namespace Xam.Dynamic.Rowheights.Controls.Cells
{
    public partial class QuestionPickerViewCell
    {
        private IDisposable _selectionChangedSubscription;

        public QuestionPickerViewCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as QuestionViewModel;

            if (viewModel == null) return;

            Picker.SelectedItem = viewModel.Children.OfType<AnswerViewModel>().FirstOrDefault(a => a.IsSelected);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _selectionChangedSubscription = Observable.FromEvent<EventHandler<object>, object>
            (
                h => (_, o) => h(o),
                h => Picker.SelectionChanged += h,
                h => Picker.SelectionChanged -= h
            ).Subscribe(i =>
            {
                var selectedAnswer = i as AnswerViewModel;
                if (selectedAnswer == null) return;
                // reset selected answers
                foreach (var answer in Picker.ItemsSource.OfType<AnswerViewModel>())
                    answer.IsSelected = false;
                // set selected answer
                selectedAnswer.IsSelected = true;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _selectionChangedSubscription?.Dispose();
        }
    }
}
