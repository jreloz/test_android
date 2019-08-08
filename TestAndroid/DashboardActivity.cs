using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using Android.Content;
using TestAndroid.DataHelper;

namespace TestAndroid
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        DataModel DataModel = new DataModel();

        EditText loggeduser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_dashboard);
            // Create your application here

            loggeduser = (EditText)FindViewById<EditText>(Resource.Id.loguser);
            Activity_OnLoad();
        }


        private void Activity_OnLoad()
        {
            loggeduser.Text = DataModel.username;
        }
    }
}