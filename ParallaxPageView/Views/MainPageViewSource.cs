using System;
using UIKit;
using System.Collections.Generic;

namespace ParallaxPageView
{
    public class MainPageViewSource : UIPageViewControllerDataSource
    {

        private List<string> pageData;
        private List<DataViewController> pageViews;

        private UIViewController m_RootNav;

        public List<string> PageData { get { return pageData; } }
        public List<DataViewController> PagesViews { get { return pageViews; } }

        public MainPageViewSource (UIViewController rootNav)
        {
            m_RootNav = rootNav;

            pageData = new List<string> ();
            pageViews = new List<DataViewController> ();

            for (int i = 1;i <= 3; i++) {
                pageData.Add(i.ToString ("00"));

                var dataViewController = (DataViewController)m_RootNav.Storyboard.InstantiateViewController ("DataViewController");
                dataViewController.DataObject = i.ToString ("00");
                pageViews.Add (dataViewController);
            }
        }

        public DataViewController GetViewController (int index, UIStoryboard storyboard)
        {
            if (index >= pageData.Count)
                return null;

            return pageViews [index];
        }

        public int IndexOf (DataViewController viewController)
        {
            if (viewController == null)
                return -1;
            
            return pageData.IndexOf (viewController.DataObject);
        }

        #region Page View Controller Data Source

        public override UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            int index = IndexOf ((DataViewController)referenceViewController);

            if (index == -1 || index == pageData.Count - 1)
                return null;

            return GetViewController (index + 1, referenceViewController.Storyboard);
        }

        public override UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            int index = IndexOf ((DataViewController)referenceViewController);

            if (index == -1 || index == 0)
                return null;

            return GetViewController (index - 1, referenceViewController.Storyboard);
        }

        #endregion
    }
}

