using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;

namespace dapperPath
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static  CultureInfo culture;
        private static List<CultureInfo> cultureInfos = new List<CultureInfo>();
       
        public static  CultureInfo Language
        {
            get
            {
                return culture;

            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == culture) return;

              
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru":
                        dict.Source = new Uri("/Resourses/Language/lang.xaml", UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri(String.Format("/Resourses/Language/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                }

              
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("/Resourses/Language/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
                

             
            }
            
        }
        public static void ChangeTheme(string uriTheme)
        {
       
            ResourceDictionary theme = new ResourceDictionary();
            theme.Source = new Uri(uriTheme, UriKind.Relative);


            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("/Resourses/Theme/")
                                          select d).First();
            if (oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, theme);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(theme);
            }

        }
        public App()
        {
            cultureInfos.Add(new CultureInfo("en"));
            cultureInfos.Add(new CultureInfo("ru"));

        }
    }
}
