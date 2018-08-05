using Android.Content;
using Android.Widget;
using Android.Util;

namespace Admin_App_Coins
{
    class CustomLinearLayout : LinearLayout
    {
        public CustomLinearLayout(Context context, IAttributeSet attribute) : base (context, attribute) { }

        public int Id_Item { get; set; }
    }
}