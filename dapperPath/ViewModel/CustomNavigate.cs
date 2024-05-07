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

        public static void NavigateTo(Page page)
        {
            // Добавляем текущую страницу на стек
            if (pageStack.Count > 0)
            {
                pageStack.Push(pageStack.Peek());
            }

            // Переходим на новую страницу
            pageStack.Push(page);

            // Вызываем событие об изменении текущей страницы
            OnCurrentPageChanged(page);
        }

        public static void GoBack()
        {
            // Убираем текущую страницу со стека
            pageStack.Pop();

            // Вызываем событие об изменении текущей страницы
            OnCurrentPageChanged(pageStack.Peek());
        }

        public static Page GetCurrentPage()
        {
            return pageStack.Peek();
        }

        public static event EventHandler<PageChangedEventArgs> CurrentPageChanged;

        private static void OnCurrentPageChanged(Page newPage)
        {
            CurrentPageChanged?.Invoke(null, new PageChangedEventArgs(newPage));
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
