using SharpAITest.DataAccessLibrary.DTOs;
using System.Data;

namespace SharpAITest.DataAccessLibrary.Extensions;

public static class DataTableExtensions
{
    public static DataTable ToCreateDataTable(this IEnumerable<OrderProductDto> orderProducts)
    {
        DataTable output = new();
        output.Columns.Add($"OrderId");
        output.Columns.Add($"ProductId");
        output.Columns.Add($"Quantity");

        foreach (var orderProduct in orderProducts)
        {
            output.Rows.Add(
                orderProduct.OrderId,
                orderProduct.ProductId,
                orderProduct.Quantity
                );
        }

        return output;
    }

    public static DataTable ToUpdateDataTable(this IEnumerable<OrderProductDto> orderProducts)
    {
        DataTable output = new();
        output.Columns.Add($"Id");
        output.Columns.Add($"Quantity");

        foreach (var orderProduct in orderProducts)
        {
            output.Rows.Add(
                orderProduct.Id,
                orderProduct.Quantity
                );
        }

        return output;
    }
}
