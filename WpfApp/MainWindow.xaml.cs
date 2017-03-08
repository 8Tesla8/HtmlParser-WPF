using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp.Http;
using WpfApp.Model;
using WpfApp.Repository;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        object _lock = new object();
        bool _appLoadComplete = false;
        Parser.Parser _parse = new Parser.Parser();
        
        public MainWindow()
        {
            InitializeComponent();

            _appLoadComplete = true;
            dataGrid.ItemsSource = _parse.FilterElements();
        }
        
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                string key = checkBox.Name.Substring(8, checkBox.Name.Length - 8);

                if (checkBox.IsChecked ?? false)
                {
                    _parse.FilterTypes[key] = true;
                }
                else
                {
                    _parse.FilterTypes[key] = false;
                }

                if (_appLoadComplete)
                {
                    dataGrid.ItemsSource = _parse.FilterElements();
                }
            }
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                _parse.SaveHtmlElement();
            }
        }

        private void ParseBtnClick(object sender, RoutedEventArgs e)
        {
            string url = textBoxUrl.Text;

            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Url can't be empty");
                return;
            }

            lock (_lock)
            {
                Task.Run(() =>
                {                  
                    var listOfFoundHtml = _parse.GetHtmlAndParse(url);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        textBlockStatus.Text = string.Format
                        ("Url: \n{0}\nCode: {1} {2}\nTime: {3}\n",
                        _parse.HttpResponse.Url,
                        (int)_parse.HttpResponse.StatusCode,
                        _parse.HttpResponse.StatusCode,      
                        _parse.HttpResponse.ResponseTime);


                        if (listOfFoundHtml != null && listOfFoundHtml.Count > 0)
                        {
                            dataGrid.ItemsSource = listOfFoundHtml;
                        }

                    }), DispatcherPriority.ContextIdle);
                });
            }
        }
    }
}
