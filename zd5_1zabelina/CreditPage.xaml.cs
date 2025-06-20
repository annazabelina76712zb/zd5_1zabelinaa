using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd5_1zabelina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditPage : ContentPage
    {
        public CreditPage()
        {
            InitializeComponent();
            rateSlider.ValueChanged += OnRateSliderValueChanged;
        }
        private void OnRateSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            rateValueLabel.Text = $"Текущая ставка: {e.NewValue:F0}%";
        }
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrEmpty(amountEntry.Text))
                {
                    DisplayAlert("Ошибка", "Введите сумму кредита", "OK");
                    return;
                }

                if (termPicker.SelectedItem == null)
                {
                    DisplayAlert("Ошибка", "Выберите срок кредита", "OK");
                    return;
                }

                // Парсинг данных с проверкой
                if (!double.TryParse(amountEntry.Text, out double amount) || amount <= 0)
                {
                    DisplayAlert("Ошибка", "Некорректная сумма кредита", "OK");
                    return;
                }

                if (!int.TryParse(termPicker.SelectedItem.ToString(), out int term) || term <= 0)
                {
                    DisplayAlert("Ошибка", "Некорректный срок кредита", "OK");
                    return;
                }

                double rate = rateSlider.Value; // Получаем значение из Slider
                bool isAnnuity = paymentTypePicker.SelectedIndex == 0; // 0 = Аннуитетный, 1 = Дифференцированный

                // Расчет платежей
                if (isAnnuity)
                {
                    // Аннуитетный платеж
                    double monthlyRate = rate / 100 / 12; // Учитываем что rate в процентах
                    double coefficient = (monthlyRate * Math.Pow(1 + monthlyRate, term)) /
                                        (Math.Pow(1 + monthlyRate, term) - 1);
                    double monthlyPayment = amount * coefficient;
                    double totalPayment = monthlyPayment * term;
                    double overpayment = totalPayment - amount;

                    monthlyPaymentLabel.Text = $"Ежемесячный платеж: {monthlyPayment:F2} ₽";
                    totalPaymentLabel.Text = $"Общая сумма: {totalPayment:F2} ₽";
                    overpaymentLabel.Text = $"Переплата: {overpayment:F2} ₽";
                }
                else
                {
                    // Дифференцированный платеж
                    double principal = amount / term;
                    double totalPayment = 0;

                    for (int i = 0; i < term; i++)
                    {
                        double remaining = amount - (principal * i);
                        totalPayment += principal + (remaining * rate / 100 / 12); // rate в процентах
                    }

                    double overpayment = totalPayment - amount;

                    monthlyPaymentLabel.Text = "Ежемесячный платеж: изменяется";
                    totalPaymentLabel.Text = $"Общая сумма: {totalPayment:F2} ₽";
                    overpaymentLabel.Text = $"Переплата: {overpayment:F2} ₽";
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }
}