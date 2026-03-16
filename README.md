# How to customize the cell value using multi-bindings in .NET MAUI DataGrid?

This sample demonstrates how to customize cell values using **MultiBinding** in a [.NET MAUI SfDataGrid](https://help.syncfusion.com/maui/datagrid/overview). It shows how to combine multiple data sources within a single cell to create enhanced, contextual displays. By using IMultiValueConverter, you can transform and combine cell data with other properties to modify the cell value dynamically.

## XAML

```xml
<ContentPage.Resources>
    <local:CellValueFormatConverter x:Key="CellValueFormatConverter" />
</ContentPage.Resources>

<syncfusion:SfDataGrid x:Name="dataGrid"
                       ItemsSource="{Binding OrderInfoCollection}">

    <syncfusion:SfDataGrid.Columns>
        <!-- Order ID with Status using MultiBinding -->
        <syncfusion:DataGridNumericColumn HeaderText="Order ID" 
                                          Format="0"
                                          MappingName="OrderID" 
                                          Width="150">
            <syncfusion:DataGridNumericColumn.DisplayBinding>
                <MultiBinding Converter="{StaticResource CellValueFormatConverter}"
                              ConverterParameter="OrderWithStatus">
                    <Binding Path="OrderID" />
                    <Binding Path="IsPaid" />
                </MultiBinding>
            </syncfusion:DataGridNumericColumn.DisplayBinding>
        </syncfusion:DataGridNumericColumn>

        <!-- Ship Country with City using MultiBinding -->
        <syncfusion:DataGridTextColumn HeaderText="Ship Country"
                                        MappingName="ShipCountry"
                                        Width="200">
            <syncfusion:DataGridTextColumn.DisplayBinding>
                <MultiBinding Converter="{StaticResource CellValueFormatConverter}"
                              ConverterParameter="CountryWithCity">
                    <Binding Path="ShipCountry" />
                    <Binding Path="ShipCity" />
                </MultiBinding>
            </syncfusion:DataGridTextColumn.DisplayBinding>
        </syncfusion:DataGridTextColumn>

        <syncfusion:DataGridTextColumn HeaderText="Customer ID"
                                        MappingName="CustomerID"
                                        Width="150" />

        <syncfusion:DataGridTextColumn HeaderText="Customer Name"
                                        MappingName="Customer"
                                        Width="150" />

        <syncfusion:DataGridTextColumn HeaderText="Ship City"
                                        MappingName="ShipCity"
                                        Width="150" />
    </syncfusion:SfDataGrid.Columns>
</syncfusion:SfDataGrid>
```

## Converter Implementation

```csharp
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
```

Download the complete sample from [GitHub](https://github.com/SyncfusionExamples/How-to-customize-the-cell-value-using-multi-bindings-in-MAUI-DataGrid-SfDataGrid)

