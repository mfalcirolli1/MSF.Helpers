using HtmlAgilityPack;

namespace MSF.Util.Crawler
{
    public struct RequestContext
    {
        public string EventTarget { get; set; }
        public string EventArgument { get; set; }
        public string ViewState { get; set; }
        public string ViewStateGenerator { get; set; }
        public string ScrollPositionX { get; set; }
        public string ScrollPositionY { get; set; }
        public string ViewStateEncrypted { get; set; }
        public string EventValidation { get; set; }
        public string LastFocus { get; set; }


        public string SHATU { get; set; }
        public string IP { get; set; }
        public string URL { get; set; }
        public string TXTCampo { get; set; }
        public string Ambiente { get; set; }
        public string FullName { get; set; }
        public string SHATU2 { get; set; }
        public string NivelAc { get; set; }
        public string LastLogin { get; set; }
        public string LastChange { get; set; }


        public string PaginaHtml { get; set; }
        public HtmlDocument HtmlDocument { get; set; }
    }
}
