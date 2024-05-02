using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace dapperPath.ViewModel
{
    public class CustomNavigate : INavigationService
    {
        private Stack<Page> _pageStack = new Stack<Page>();

        public void NavigateTo(Page page)
        {
            _pageStack.Push(page);
            OnPageChanged(page);
        }

        public void NavigateBack()
        {
            if (_pageStack.Count > 1)
            {
                _pageStack.Pop();
                OnPageChanged(_pageStack.Peek());
            }
        }

        public event EventHandler<PageChangedEventArgs> PageChanged;

        protected virtual void OnPageChanged(Page page)
        {
            PageChanged?.Invoke(this, new PageChangedEventArgs(page));
        }
    }

    public class PageChangedEventArgs : EventArgs
    {
        public Page Page { get; }

        public PageChangedEventArgs(Page page)
        {
            Page = page;
        }
    }

    public interface INavigationService
    {
        void NavigateTo(Page page);

        void NavigateBack();
        event EventHandler<PageChangedEventArgs> PageChanged;
    }
}
