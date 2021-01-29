using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using Sibur;
using Sibur.iOS;
[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace Sibur.iOS
{
    class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && this.Control != null)
            {
                
                    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
                    {
                        UIDatePicker picker = (UIDatePicker)Control.InputView;
                        picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
                    }
                
            }
        }
    }
}