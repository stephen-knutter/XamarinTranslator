using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Core;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            EditText phoneNumberText = 
                FindViewById<EditText>(Resource.Id.PhoneNumberText);
            
            TextView translatedPhoneWord = 
                FindViewById<TextView>(Resource.Id.TranslatePhoneWord);
            
            Button translateButton = 
                FindViewById<Button>(Resource.Id.TranslateButton);

            Button translateHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);

            // Disable the "Call" button
            //callButton.Enabled = false;
            string translatedNumber = string.Empty;

            // Translate # Btn Click
            translateButton.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                translatedNumber = 
                    PhonewordTranslator.ToNumber(phoneNumberText.Text);
                
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }
                else 
                {
                    translatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translateHistoryButton.Enabled = true;
                }
            };

            // # History Btn Click
            translateHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }
    }
}

