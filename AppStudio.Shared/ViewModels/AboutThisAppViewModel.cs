using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return Package.Current.Id.Publisher.Substring(3);
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor);
            }
        }

        public string AboutText
        {
            get
            {
                return "It brings to the latest updates on Sports.\r\nLatest news from reliable sources usi" +
    "ng RSS-Feeds.";
            }
        }
    }
}

