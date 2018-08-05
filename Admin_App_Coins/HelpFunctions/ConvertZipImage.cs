using System.IO;
using Android.Content;
using Android.Graphics;

namespace Admin_App_Coins.HelpFunctions
{
    class ConvertZipImage
    {
        private Android.Net.Uri uri_icon;
        private FFImageLoading.Views.ImageViewAsync Icon;
        private Context context;

        public ConvertZipImage(Android.Net.Uri uri_icon, FFImageLoading.Views.ImageViewAsync Icon, Context context)
        {
            this.uri_icon = uri_icon;
            this.Icon = Icon;
            this.context = context;
        }

        public byte[] ConvertImage()
        {
            Stream stream = context.ContentResolver.OpenInputStream(uri_icon);
            Icon.SetImageBitmap(DecodeBitmapFromStream(uri_icon, 90, 90));

            Bitmap bitmap = BitmapFactory.DecodeStream(stream);
            MemoryStream memStream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 50, memStream);
            return memStream.ToArray();
        }

        private Bitmap DecodeBitmapFromStream(Android.Net.Uri data, int requestedWidth, int requestedHeight)
        {
            Stream stream = context.ContentResolver.OpenInputStream(data);
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeStream(stream);

            options.InSampleSize = CalculateInSampleSize(options, requestedWidth, requestedHeight);

            stream = context.ContentResolver.OpenInputStream(data);
            options.InJustDecodeBounds = false;
            Bitmap bitmap = BitmapFactory.DecodeStream(stream, null, options);
            return bitmap;
        }

        private int CalculateInSampleSize(BitmapFactory.Options options, int requestedWidth, int requestedHeight)
        {
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > requestedHeight || width > requestedWidth)
            {
                int halfHeight = height / 2;
                int halfWidth = width / 2;

                while ((halfHeight / inSampleSize) > requestedHeight && (halfWidth / inSampleSize) > requestedWidth)
                    inSampleSize *= 2;
            }
            return inSampleSize;
        }
    }
}