using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace XixiLink.Command
{
    public class ImgTypeLinkImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int i = (int)value;
            return new BitmapImage(new Uri("ms-appx:///Images/" + i + ".png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
