using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace DemoApp1
{
    public class MainPage: ContentPage
    {
        Entry phoneNumberText;
        Button btnTranslate;
        Button btnCall;
        string translatedNumber = string.Empty;

        public MainPage()
        {
            this.Padding = new Thickness(20, 20, 20, 20);

            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN"
            });

            panel.Children.Add(btnTranslate = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(btnCall = new Button
            {
                Text = "Call",
                IsEnabled = false
            });

            btnTranslate.Clicked += BtnTranslate_Clicked;
            btnCall.Clicked += BtnCall_Clicked;
            this.Content = panel;
        }

        private async void BtnCall_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"
                ))
            {
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid", "OK");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing is not supported", "OK");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed", "OK");
                }

            }
        }

        private void BtnTranslate_Clicked(object sender, EventArgs e)
        {
            translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                btnCall.IsEnabled = true;
                btnCall.Text = "Call " + translatedNumber;
            }
            else
            {
                btnCall.IsEnabled = false;
                btnCall.Text = "Call";
            }
        }
    }
}
