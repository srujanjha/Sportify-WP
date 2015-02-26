using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class SifySportsViewModel : ViewModelBase<RssSchema>
    {
        override protected string CacheKey
        {
            get { return "SifySportsDataSource"; }
        }

        override protected IDataSource<RssSchema> CreateDataSource()
        {
            return new SifySportsDataSource(); // RssDataSource
        }

        override public Visibility GoToSourceVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void GoToSource()
        {
            base.GoToSource("{FeedUrl}");
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("SifySportsList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("SifySportsDetail");
        }
    }
}
