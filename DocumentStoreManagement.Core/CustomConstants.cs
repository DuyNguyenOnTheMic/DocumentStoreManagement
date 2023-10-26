using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Core
{
    /// <summary>
    /// Constants class for reusable variables
    /// </summary>
    public class CustomConstants
    {
        // Document types
        public static readonly int DocumentBookType = 1;
        public static readonly int DocumentMagazineType = 2;
        public static readonly int DocumentNewsPaperType = 3;
        public static readonly Dictionary<int, string> DocumentTypes = new()
        {
            {DocumentBookType, nameof(Book) },
            {DocumentMagazineType, nameof(Magazine) },
            {DocumentNewsPaperType, nameof(Newspaper) }
        };

        // Table name
        public static readonly string DocumentsTable = "\"Documents\"";
        public static readonly string OrdersTable = "\"Orders\"";
        public static readonly string OrderDetailsTable = "\"OrderDetails\"";

        // Materialized Views
        public static readonly string MaterializedViewOrdersInclude = "orders_include";
    }
}
