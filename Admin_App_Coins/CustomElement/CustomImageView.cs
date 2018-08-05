using Android.Content;
using Android.Util;
using Android.Widget;

namespace Admin_App_Coins
{
    public class CustomImageView: ImageView
    {
        public CustomImageView(Context context, IAttributeSet attribute) : base (context, attribute) { }

        public int Id_Item { get; set; }
    }
}
