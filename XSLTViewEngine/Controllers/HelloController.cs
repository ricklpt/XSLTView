using System.Dynamic;
using System.IO;
using System.Web.Mvc;

namespace XSLTViewEngine.Controllers
{
    public class HelloController : Controller
    {
        //
        // GET: /Hello/

        public ActionResult Index()
        {
            var fileName = Path.Combine(HttpContext.ApplicationInstance.Server.MapPath("~/App_Data"),
                "data.xml");
            dynamic model = new ExpandoObject();

            using (var sr = new StreamReader(fileName))
            {
                model.data = sr.ReadToEnd();
                ;
            }


            model.channel = "TSI";
            model.market = "Market";

            return View(model);
        }
    }
}