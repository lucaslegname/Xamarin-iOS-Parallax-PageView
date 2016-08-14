using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using UIKit;

namespace ParallaxPageView
{
    public partial class MainPageViewController : UIPageViewController
    {
        private MainPageViewSource m_Source;
        private UIScrollView m_ScrollView;
        private UIViewController m_NextView;
        private UIViewController m_PreviousView;

        public MainPageViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            m_Source = new MainPageViewSource (this);

            WeakDelegate = this;
            WeakDataSource = m_Source;

            var viewControllers = new UIViewController [] { m_Source.PagesViews [0] };
            SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, false, null);

            DidFinishAnimating += OnDidFinishAnimating;
            WillTransition += OnWillTransition;

            m_ScrollView = GetScrollView ();
            m_ScrollView.Scrolled += OnScrolled;
        }

        private void OnDidFinishAnimating (object sender, UIPageViewFinishedAnimationEventArgs e)
        {
            if (e.Completed) {
                m_PreviousView = m_Source.GetPreviousViewController (this, m_NextView);
                if (m_PreviousView is DataViewController)
                    ((DataViewController)m_PreviousView).BackwardParallax (0f, m_PreviousView.View.Frame.Width, false);

                m_PreviousView = m_Source.GetNextViewController (this, m_NextView);
                if (m_PreviousView is DataViewController)
                    ((DataViewController)m_PreviousView).ForwardParallax (0f, m_PreviousView.View.Frame.Width, false);
            }

            if (m_NextView is DataViewController)
                ((DataViewController)m_NextView).ResetBackgroundPosition ();

            m_NextView = null;
            m_PreviousView = null;
        }

        private void OnWillTransition (object sender, UIPageViewControllerTransitionEventArgs e)
        {
            m_NextView = null;
            m_PreviousView = null;

            if (e.PendingViewControllers.Length > 0)
                m_NextView = e.PendingViewControllers [0];
        }

        private UIScrollView GetScrollView ()
        {
            foreach (UIView view in View.Subviews) {
                if (view is UIScrollView)
                    return (UIScrollView)view;
            }
            return null;
        }

        private void OnScrolled (object sender, EventArgs e)
        {
            nfloat offset = m_ScrollView.ContentOffset.X - m_ScrollView.Frame.Width;
            AnimateOnScroll (offset, m_ScrollView.Frame.Width);
        }

        private void AnimateOnScroll (nfloat offset, nfloat frameWidth)
        {
            if (offset > 0f) {
                m_PreviousView = m_Source.GetPreviousViewController (this, m_NextView);

                // We go forward
                // Current View Backward Parallax + Next View Forward Parallax
                if (m_NextView is DataViewController)
                    ((DataViewController)m_NextView).ForwardParallax (offset, frameWidth, false);
                if (m_PreviousView is DataViewController)
                    ((DataViewController)m_PreviousView).ForwardParallax (offset, frameWidth, true);
            } else {
                m_PreviousView = m_Source.GetNextViewController (this, m_NextView);

                // We go backward
                // Current View Forward Parallax + Previous View Backward Parallax
                if (m_NextView is DataViewController)
                    ((DataViewController)m_NextView).BackwardParallax (offset, frameWidth, false);
                if (m_PreviousView is DataViewController)
                    ((DataViewController)m_PreviousView).BackwardParallax (offset, frameWidth, true);
            }
        }
    }
}
