using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Sibur;
using Sibur.Droid;
using Android.Content;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace Sibur.Droid
{
    class CustomDatePickerRenderer:DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
        }
    }
}