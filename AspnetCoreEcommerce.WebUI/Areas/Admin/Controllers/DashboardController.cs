using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreEcommerce.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        #region Fields

        #endregion

        #region Constructor

        public DashboardController()
        { }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}