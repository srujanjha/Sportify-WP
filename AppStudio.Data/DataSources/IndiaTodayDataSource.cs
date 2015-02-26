using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class IndiaTodayDataSource : IDataSource<RssSchema>
    {
        private const string _url =@"http://indiatoday.feedsportal.com/c/33614/f/589706/index.rss?http://indiatoday.intoday.in/rss/article.jsp?sid=84";

        private IEnumerable<RssSchema> _data = null;

        public IndiaTodayDataSource()
        {
        }

        public async Task<IEnumerable<RssSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var rssDataProvider = new RssDataProvider(_url);
                    _data = await rssDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("IndiaTodayDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<RssSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
