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
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
        }
        private async void OnSignInClicked(object sender, System.EventArgs e)
        {
            // Здесь можно добавить проверку логина/пароля
            // Пока просто переходим на главную страницу

            var mainTabbedPage = new MainTabbedPage();
            await Navigation.PushAsync(mainTabbedPage);

            // Если хотим сделать TabbedPage корневой страницей (без возможности вернуться назад)
            // Application.Current.MainPage = new MainTabbedPage();
        }
        private void OnForgotPasswordTapped(object sender, System.EventArgs e)
        {
           
            DisplayAlert("Forgot Password", "Password reset instructions will be sent to your email.", "OK");
        }
    }
}