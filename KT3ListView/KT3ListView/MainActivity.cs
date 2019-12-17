using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Refit;
using EDMTDialog;
using System.Collections.Generic;
using System;

namespace KT3ListView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        MyAPI myAPI;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button button_search = FindViewById<Button>(Resource.Id.btn_search);
            ListView list_teams = FindViewById<ListView>(Resource.Id.lst_teams);
            EditText enter_team = FindViewById<EditText>(Resource.Id.entr_team);

            //Init API
            myAPI = RestService.For<MyAPI>("https://www.thesportsdb.com/api/v1/json/1/searchteams.php?t=");

            button_search.Click += async delegate
            {
                try
                {
                    Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                   .SetContext(this)
                   .SetMessage("Please wait...")
                   .Build();

                    if (!dialog.IsShowing)
                        dialog.Show();

                    List<Team> teams = await myAPI.GetTeams();
                    List<string> team_name = new List<string>();

                    foreach (var team in teams)
                        team_name.Add(team.strTeam);

                    var adapter = new ArrayAdapter<string>(this,
                        Android.Resource.Layout.SimpleListItem1, team_name);

                    list_teams.Adapter = adapter;

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