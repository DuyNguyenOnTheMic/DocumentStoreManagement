using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Core
{
    /// <summary>
    /// Constants class for reusable variables
    /// </summary>
    public class CustomConstants
    {
        public static readonly int DocumentBookType = 1;
        public static readonly int DocumentMagazineType = 2;
        public static readonly int DocumentNewsPaperType = 3;

        public static readonly Dictionary<int, string> DocumentTypes = new()
        {
            {DocumentBookType, nameof(Book) },
            {DocumentMagazineType, nameof(Magazine) },
            {DocumentNewsPaperType, nameof(Newspaper) }
        };
    }
}
