using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace dapperPath.Resourses
{
    public class NotZeroConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is decimal price && values[1] is decimal discount && discount != 0)
            {
                decimal discountedPrice = (decimal)values[1];
                var textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run($"{price} BYN") { TextDecorations = TextDecorations.Strikethrough });
                textBlock.Inlines.Add(new Run($" {discountedPrice} BYN") { Foreground = Brushes.Red });
                return textBlock;
            }

            if (values[0] is decimal price1 && values[1] is decimal discount1 && discount1 == 0)
            {
                return new Run($"{price1} BYN");
            }

            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
