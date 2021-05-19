using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SpeechTrainer.Core.Utills
{
    public class Extensions
    {
        public static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(dependencyObject);

            if (parentObject == null) return null;

            var parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }
    }
}
