using System.Web.Mvc;

namespace XSLTViewEngine
{
    public class XsltViewEngine : VirtualPathProviderViewEngine
    {
        public XsltViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.xsl", "~/Views/Shared/{0}.xsl",
                "~/Views/{1}/{0}.xslt", "~/Views/Shared/{0}.xslt"
            };
            PartialViewLocationFormats = ViewLocationFormats;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new XsltView(partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new XsltView(viewPath);
        }
    }
}