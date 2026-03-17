using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace SfDataGrid_Sample
{
    public class CellValueFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return string.Empty;

            var primaryValue = values[0]?.ToString() ?? "";
            var secondaryValue = values[1];
            var parameterValue = parameter?.ToString() ?? "";

            return parameterValue switch
            {
                // Format Order ID with payment status
                "OrderWithStatus" => 
                    $"{primaryValue} [{(secondaryValue is bool && (bool)secondaryValue ? "Processing" : "On Hold")}]",

                // Format Ship Country with Ship City in brackets
                "CountryWithCity" => 
                    $"{primaryValue} ({secondaryValue?.ToString() ?? ""})",

                // Default: Return primary value
                _ => primaryValue
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
