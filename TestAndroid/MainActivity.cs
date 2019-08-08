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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DataModel DataModel = new DataModel();

        EditText uname, pword;
        Button login, create;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            uname = FindViewById<EditText>(Resource.Id.uname);
            pword = FindViewById<EditText>(Resource.Id.pword);

            login = FindViewById<Button>(Resource.Id.login);
            create = FindViewById<Button>(Resource.Id.create);

            login.Click += Login_Click;
        }

        private async void Login_Click(object sender, System.EventArgs e)
        {
            try
            {
                var result = await CheckAuth();
                
                if (result == "has user")
                {
                    DataModel.username = uname.Text;
                    StartActivity(typeof(DashboardActivity));
                }
                else
                {
                    Toast.MakeText(this.ApplicationContext, $"{result}", ToastLength.Long).Show();
                }
            }
            catch (System.Exception)
            {

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        


        //Post Async
        private async Task<string> CheckAuth()
        {
            var username = uname.Text;
            var password = pword.Text;


            // You need to post the data as key value pairs:
            string postData = $"CheckAuth=CheckAuth&uname={username}&pword={password}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);



            // Post the data to the right place.
            Uri target = new Uri("http://192.168.1.5/test/");

            WebRequest request = WebRequest.Create(target);


            request.Method = "POST";
            request.ConnectionGroupName = "sendSMS";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }



            using (var response = (HttpWebResponse)request.GetResponse())
            {
                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.ASCII;

                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    var res = await reader.ReadToEndAsync();
                    return res.Trim('"');
                }
            }

        }
    }
}