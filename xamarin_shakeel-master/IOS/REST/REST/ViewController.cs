using System;
using System.Net.Http;
using System.Json;
using UIKit;

namespace REST
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            btnForecast.TouchUpInside += async (sender, e) =>
            {
                HttpClient client = new HttpClient();
                string jsonResponse = await client.GetStringAsync("http://api.ometfn.net/0.1/forecast/eu12/46.5,6.32/now.json");
                JsonValue jsonObj = JsonValue.Parse(jsonResponse);  //对json回应作解析,
                string temp = (string)jsonObj["run"];
                JsonArray windSpeedArray = (JsonArray)jsonObj["wind_10m_ground_speed"];
                double windSpeed = (double)windSpeedArray[0];
                lblOutput.Text = string.Format("run:{0}\nWind Speed:{1}", temp, windSpeed);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}