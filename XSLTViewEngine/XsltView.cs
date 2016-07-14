using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace XSLTViewEngine
{
    public class XsltView : IView
    {
        private readonly string _path;

        public XsltView(string partialPath)
        {
            _path = partialPath;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var xsltFile = viewContext.HttpContext.Server.MapPath(_path);
            var argsList = new XsltArgumentList();

            var xmlData = string.Empty;
            dynamic model = viewContext.ViewData.Model;

            if (model is ExpandoObject)
            {
                foreach (var property in (IDictionary<string, object>) model)
                {
                    if (string.Compare(property.Key, "data", StringComparison.InvariantCultureIgnoreCase) == 0)
                        xmlData = property.Value as string;
                    else
                        argsList.AddParam(property.Key, "", property.Value);
                }
            }
            else
            {
                xmlData = model != null ? model.ToString() : string.Empty;
            }


            var xmlTree = XDocument.Parse(xmlData);
            var xslt = new XslCompiledTransform();


            xslt.Load(xsltFile);

            xslt.Transform(xmlTree.CreateReader(), argsList, writer);
        }
    }
}