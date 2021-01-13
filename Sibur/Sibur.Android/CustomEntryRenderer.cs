using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Sibur;
using Sibur.Droid;
using Android.Content;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(MyEntryRenderer))]
namespace Sibur.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}