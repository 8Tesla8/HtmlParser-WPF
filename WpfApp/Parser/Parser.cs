using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Http;
using WpfApp.Model;
using WpfApp.Parser;
using WpfApp.Repository;
using WpfApp.ViewModel;

namespace WpfApp.Parser
{
    public class Parser
    {
        HttpHandler _httpHandler = new HttpHandler();
        HtmlPageParser _htmlPageParser = new HtmlPageParser();
        IRepository<HtmlElements> _repository = new HtmlElementRepository();

        public List<HtmlElements> FoundElements { get; private set; }
        public Dictionary<string, bool> FilterTypes { get; private set; } = new Dictionary<string, bool>();
        public HttpResponse HttpResponse { get; private set; }

        public Parser()
        {
            FilterTypes.Add("Title", true);
            FilterTypes.Add("Description", true);
            FilterTypes.Add("Ahref", true);
            FilterTypes.Add("Img", true);
            FilterTypes.Add("H1", true);
            FilterTypes.Add("Link", true);

            FoundElements =  new List<HtmlElements>(_repository.GetList());
        }
        public List<HtmlElementVM> GetHtmlAndParse(string url)
        {
            HttpResponse = _httpHandler.GetResponse(url);

            if (HttpResponse.StatusCode == HttpStatusCode.OK)
            {
                FoundElements = _htmlPageParser.Parse(HttpResponse.Context, url);

                if (FoundElements != null && FoundElements.Count > 0)
                {                   
                    return FilterElements();
                }
            }
 
            return new List<HtmlElementVM>();
        }

        public void SaveHtmlElement()
        {
            _repository.Create(FoundElements);
        }

        public List<HtmlElementVM> FilterElements()
        {
            List<HtmlElementVM> filterList = new List<HtmlElementVM>(); 

            foreach (var htmlElement in FoundElements)
            {
                string typeName = TypeHtml.GetType(htmlElement.TypeId);

                if (FilterTypes[typeName])
                {
                    filterList.Add(new HtmlElementVM()
                    {
                        Id = htmlElement.Id,
                        Content = htmlElement.Content,
                        Html = htmlElement.Html,
                        Type = TypeHtml.GetType(htmlElement.TypeId),
                        Url = htmlElement.Url,
                    });
                }
            }
            return filterList;
        }
    }
}
