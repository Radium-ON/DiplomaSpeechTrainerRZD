using System;

namespace SpeechTrainer.Core.Utills
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }
            return (bValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            else
            {
                return false;
            }
        }
    }
}
