using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InfoPan.UW
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        List<SyndicationItem> izsuList = new List<SyndicationItem>(); 
        private async void Layout_Loaded(object sender, RoutedEventArgs e)
        {
            SyndicationClient client = new SyndicationClient();
            SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri("http://www.izsu.gov.tr/pages/rss.aspx?rssId=2"));
            if (feed != null)
            {
                foreach (SyndicationItem item in feed.Items)
                {
                    izsuList.Add(item);
                }
            }
            else
            {
                webView.NavigateToString("<h3>Bağlantı Sorunu!</h3>");
            }
        }

        private async void feedBtn_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                foreach (var item in izsuList)
                {
                    izsuListBox.Items.Add(item.Title.Text);
                }

                int counter = 0;
                foreach (var item in izsuList)
                {
                    izsuListBox.SelectedItem = izsuListBox.Items[counter];
                    izsuListBox.ScrollIntoView(item.Title.Text);
                    webView.NavigateToString(item.Summary.Text);
                    await Task.Delay(5000);
                    counter++;
                }
            }
        }
    }
}
