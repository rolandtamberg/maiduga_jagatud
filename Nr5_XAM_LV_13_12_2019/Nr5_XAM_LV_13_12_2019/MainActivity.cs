using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Refit;
using EDMTDialog;
using System.Collections.Generic;
using System;

namespace Nr5_XAM_LV_13_12_2019
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btn_get_data;
        ListView lst_users;

        IMyAPI myAPI;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btn_get_data = FindViewById<Button>(Resource.Id.btn_get_data);
            lst_users = FindViewById<ListView>(Resource.Id.list_user);

            //Init API
            myAPI = RestService.For<IMyAPI>("http://jsonplaceholder.typicode.com");

            btn_get_data.Click += async delegate
            {
                try
                {
                    Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                   .SetContext(this)
                   .SetMessage("Please wait...")
                   .Build();

                    if (!dialog.IsShowing)
                        dialog.Show();

                    List<MyUser> users = await myAPI.GetUsers();
                    List<string> user_name = new List<string>();

                    foreach (var user in users)
                        user_name.Add(user.name);

                    var adapter = new ArrayAdapter<string>(this,
                        Android.Resource.Layout.SimpleListItem1, user_name);

                    lst_users.Adapter = adapter;

                    if (dialog.IsShowing)
                        dialog.Dismiss();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}