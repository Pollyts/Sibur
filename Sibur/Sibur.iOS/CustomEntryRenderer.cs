using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using Sibur;
using Sibur.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace Sibur.iOS
{
    class CustomEntryRenderer: EntryRenderer
    {
		//public CustomEntryRenderer(Context context) : base(context)
		//{
		//}
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// do whatever you want to the UITextField here!
				Control.BackgroundColor = UIColor.FromRGBA(255,255,255,0);
				//Control.BorderStyle = UITextBorderStyle.Line;
			}
		}
	}
}