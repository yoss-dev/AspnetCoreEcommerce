using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreEcommerce.WebUI.Areas.Admin.Models.Catalog
{
    public class CategoryCreateOrUpdateModel
    {
        public string ActiveTab { get; set; }
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parent Category")]
        public Guid ParentCategoryId { get; set; }

        public SelectList ParentCategorySelectList { get; set; }

        [Display(Name = "SEO Url")]
        [RegularExpression(@"^[a-zA-Z0-9]+(-[a-zA-Z0-9]+)*$", ErrorMessage =
            "Url must only contain alphanumeric values [a-z A-Z 0-9] and dash [-] e.g. abc-123-D45")]
        public string SeoUrl { get; set; }

        [Display(Name = "Meta Tag Title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Tag Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Meta Tag Description")]
        public string MetaDescription { get; set; }

        public DateTime DateAdded { get; set; }

        public bool Published { get; set; }

        public CategoryCreateOrUpdateModel()
        {
            Published = true;
            ActiveTab = "info";
        }
    }
}
