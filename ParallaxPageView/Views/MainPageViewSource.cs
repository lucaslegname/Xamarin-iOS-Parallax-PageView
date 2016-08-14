using System;
using UIKit;
using System.Collections.Generic;

namespace ParallaxPageView
{
    public class MainPageViewSource : UIPageViewControllerDataSource
    {
        private List<string> pageData;
        private List<DataViewController> pageViews;

        public List<string> PageData { get { return pageData; } }
        public List<DataViewController> PagesViews { get { return pageViews; } }

        public MainPageViewSource (UIViewController rootNav)
        {
            pageData = new List<string> ();
            pageViews = new List<DataViewController> ();

            for (int i = 1;i <= 3; i++) {
                string viewName = i.ToString ("00");
                pageData.Add(viewName);

                var dataViewController = (DataViewController)rootNav.Storyboard.InstantiateViewController ("DataViewController");
                dataViewController.DataObject = viewName;
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
    }
}
