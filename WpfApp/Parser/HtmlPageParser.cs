using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using WpfApp.Model;

namespace WpfApp.Parser
{
    public class HtmlPageParser
    {
        public List<HtmlElements> FoundElements { get; private set; }
        private string _url;

        public List<HtmlElements> Parse(string htmlPage, string url)
        {
            _url = url;

            FoundElements = new List<HtmlElements>();

            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(htmlPage);

            FindElementsByType(doc, HtmlType.Title);
            FindElementsByType(doc, HtmlType.Description);
            FindElementsByType(doc, HtmlType.H1);
            FindElementsByType(doc, HtmlType.Img);
            FindElementsByType(doc, HtmlType.Ahref);
            FindElementsByType(doc, HtmlType.Link);

            return FoundElements;
        }

        private void FindElementsByType(IHtmlDocument document, HtmlType serchType)
        {
            string htmlSerchElement = null;


            if (serchType == HtmlType.Title)
            {
                htmlSerchElement = "title";
            }
            else if (serchType == HtmlType.Description)
            {
                htmlSerchElement = "meta";
            }
            else if (serchType == HtmlType.H1)
            {
                htmlSerchElement = "h1";
            }
            else if (serchType == HtmlType.Img)
            {
                htmlSerchElement = "img";
            }
            else if (serchType == HtmlType.Ahref)
            {
                htmlSerchElement = "a";
            }
            else if (serchType == HtmlType.Link)
            {
                htmlSerchElement = "link";
            }

            var foundHtmlElements = document.All.Where(d => d.LocalName == htmlSerchElement);
            foreach (var item in foundHtmlElements)
            {
                HtmlElements foundHtml = null;

                if (serchType == HtmlType.Description)
                {
                    if (item.OuterHtml.Contains("name=\"description\""))
                    {
                        foundHtml = new HtmlElements()
                        {
                            Content = item.GetAttribute("content"),
                        };
                    }
                }
                else if (serchType == HtmlType.Img)
                {
                    foundHtml = new HtmlElements()
                    {
                        Content = item.GetAttribute("src"),
                    };
                }
                else if (serchType == HtmlType.Ahref ||
                    serchType == HtmlType.Link)
                {
                    foundHtml = new HtmlElements()
                    {
                        Content = item.GetAttribute("href"),
                    };
                }
                else
                {
                    foundHtml = new HtmlElements()
                    {
                        Content = item.TextContent,
                    };
                }


                if (foundHtml != null)
                {
                    foundHtml.TypeId = (int)serchType;
                    foundHtml.Html = item.OuterHtml;
                    foundHtml.Url = _url;

                    FoundElements.Add(foundHtml);
                }
            }
        }
    }
}

