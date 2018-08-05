using Admin_App_Coins.HelpFunctions;
using Android.App;
using Android.Content;
using FFImageLoading;

namespace Admin_App_Coins.BaseClass
{
    public class BaseMethodPeriod : Activity
    {
        protected FFImageLoading.Views.ImageViewAsync Icon_Emblem;
        protected FFImageLoading.Views.ImageViewAsync Icon_Flag;
        protected byte[] byte_icon_flag, byte_icon_emblem;

        protected void SelectImage(int index)
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), index);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (data != null)
            {
                var image = data.Data;
                ConvertZipImage convert_image;

                if (resultCode == Result.Ok)
                {
                    if (requestCode == 0)
                    {
                        convert_image = new ConvertZipImage(image, Icon_Flag, this);
                        byte_icon_flag = convert_image.ConvertImage();
                    }
                    else if (requestCode == 1)
                    {
                        convert_image = new ConvertZipImage(image, Icon_Emblem, this);
                        byte_icon_emblem = convert_image.ConvertImage();
                    }
                    else
                    {
                        byte_icon_flag = null;
                        byte_icon_emblem = null;
                    }
                }
            }
            else
            {
                if (requestCode == 0)
                {
                    Icon_Flag.SetImageResource(Resource.Drawable.flag);
                    byte_icon_flag = null;
                }
                else if (requestCode == 1)
                {
                    Icon_Emblem.SetImageResource(Resource.Drawable.emblem);
                    byte_icon_emblem = null;
                }
                else
                {
                    byte_icon_flag = null;
                    byte_icon_emblem = null;
                }
            }
        }
    }
}