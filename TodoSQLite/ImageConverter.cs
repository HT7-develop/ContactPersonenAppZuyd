using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;


namespace ContactPersonenApp.Converters
{
    public class ImagePathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string imagePath)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    return new FileImageSource { File = imagePath };
                }
                else
                {
                    // Provide the path to the fallback image here
                    string fallbackImagePath = GetFallbackImagePath();

                    return new FileImageSource { File = fallbackImagePath };
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetFallbackImagePath()
        {
            string fallbackImagePath = "person.png";
            return fallbackImagePath;
        }

    }
}
