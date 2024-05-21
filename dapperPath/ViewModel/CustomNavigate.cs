using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace dapperPath.ViewModel
{
    public static class CustomNavigate
    {
        private static Stack<Page> pageStack = new Stack<Page>();
        private static int currentPageIndex = -1;

        public static void NavigateTo(Page page)
        {
            if (currentPageIndex >= 0 && currentPageIndex < pageStack.Count)
            {
                pageStack.Push(pageStack.ElementAt(currentPageIndex));
            }

            pageStack.Push(page);
            currentPageIndex = pageStack.Count - 1;
            OnCurrentPageChanged(page);
        }
        public static void NavigateShoe(Page page)
        {
            pageStack.Push(page);

            OnCurrnetShoeChanged(page);
        }

        public static void GoBack()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                OnCurrentPageChanged(pageStack.ElementAt(currentPageIndex));
            }
        }

        public static void GoForward()
        {
            try
            {
                if (currentPageIndex < pageStack.Count - 1)
                {
                    currentPageIndex++;
                    OnCurrentPageChanged(pageStack.ElementAt(currentPageIndex - 2));
                }
            }
            catch
            {
                MessageBox.Show("Произошла непредвиденная ошибка при переходе страниц");
            }
        }

        public static void RefreshPeak(Page page)
        {
            pageStack.Pop();
            NavigateTo(page);
        }

        public static Page GetCurrentPage()
        {
            return pageStack.Count > 0 ? pageStack.ElementAt(currentPageIndex) : null;
        }

        public static event EventHandler<PageChangedEventArgs> CurrentPageChanged;
        public static event EventHandler<PageChangedEventArgs> CurrentShoeChanged;

        private static void OnCurrentPageChanged(Page newPage)
        {
            CurrentPageChanged?.Invoke(null, new PageChangedEventArgs(newPage));
        }
        private static void OnCurrnetShoeChanged(Page newPage)
        {
            CurrentShoeChanged?.Invoke(null, new PageChangedEventArgs(newPage));
        }
    }
    public class PageChangedEventArgs : EventArgs
    {
        public Page NewPage { get; }

        public PageChangedEventArgs(Page newPage)
        {
            NewPage = newPage;
        }
    }
}
