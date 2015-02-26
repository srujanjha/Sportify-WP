using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class TheHinduDataSource : IDataSource<RssSchema>
    {
        private const string _url =@"http://www.thehindu.com/sport/?service=rss";

        private IEnumerable<RssSchema> _data = null;

        public TheHinduDataSource()
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
                    AppLogs.WriteError("TheHinduDataSourceDataSource.LoadData", ex.ToString());
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
