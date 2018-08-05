using Android.Content;
using Android.Widget;
using Android.Util;

namespace Admin_App_Coins
{
    class CustomRelativeLayout : RelativeLayout
    {
        public CustomRelativeLayout(Context context, IAttributeSet attribute) : base (context, attribute) { }

        public int Id_Item { get; set; }
    }
}