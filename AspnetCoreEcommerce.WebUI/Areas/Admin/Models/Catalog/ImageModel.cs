using System;

namespace AspnetCoreEcommerce.WebUI.Areas.Admin.Models.Catalog
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int SortOrder { get; set; }
    }
}
