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
    public partial class CurrencyPage : ContentPage
    {
        public CurrencyPage()
        {
            InitializeComponent();
        }
        private void LoadRates()
        {
            dateLabel.Text = DateTime.Now.ToString("dd.MM.yyyy");
            usdLabel.Text = "75.50 ₽";
            eurLabel.Text = "85.30 ₽";
        }

        private void OnUpdateRatesClicked(object sender, EventArgs e)
        {
            LoadRates();
            DisplayAlert("Обновлено", "Курсы валют обновлены", "OK");
        }
    }
}