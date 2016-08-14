using System;

using UIKit;

namespace ParallaxPageView
{
    public partial class DataViewController : UIViewController
    {
        private UIImageView m_BackgroundImageView;

        public string DataObject {
            get; set;
        }

        protected DataViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            m_BackgroundImageView = new UIImageView (UIScreen.MainScreen.Bounds);
            m_BackgroundImageView.Image = UIImage.FromBundle (string.Format("Images/{0}.jpg", DataObject));

            UIView backgroundView = new UIView (UIScreen.MainScreen.Bounds);
            backgroundView.BackgroundColor = UIColor.Black;
            backgroundView.Alpha = 1f;

            backgroundView.AddSubview (m_BackgroundImageView);
            this.View.AddSubview (backgroundView);
            backgroundView.Layer.ZPosition = -1;
            backgroundView.ClipsToBounds = true;

            dataLabel.Text = DataObject;
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        public void ForwardParallax (nfloat offset, nfloat viewWidth, bool previousScreen)
        {
            nfloat percent = ((nfloat)Math.Abs (offset)) / viewWidth;
            nfloat parallaxSpan = viewWidth * 0.35f;
            nfloat startPosition = previousScreen ? 0f : parallaxSpan;
            nfloat value = startPosition - (parallaxSpan * percent);

            if (m_BackgroundImageView != null)
                m_BackgroundImageView.Frame = new CoreGraphics.CGRect (-value, m_BackgroundImageView.Frame.Y, m_BackgroundImageView.Frame.Width, m_BackgroundImageView.Frame.Height);
        }

        public void BackwardParallax (nfloat offset, nfloat viewWidth, bool previousScreen)
        {
            nfloat percent = 1 - (((nfloat)Math.Abs (offset)) / viewWidth);
            nfloat parallaxSpan = viewWidth * 0.35f;
            nfloat startPosition = previousScreen ? -parallaxSpan : 0f;
            nfloat value = startPosition + (parallaxSpan * percent);

            if (m_BackgroundImageView!= null)
                m_BackgroundImageView.Frame = new CoreGraphics.CGRect (value, m_BackgroundImageView.Frame.Y, m_BackgroundImageView.Frame.Width, m_BackgroundImageView.Frame.Height);
        }

        public void ResetBackgroundPosition ()
        {
            m_BackgroundImageView.Frame = new CoreGraphics.CGRect (0, m_BackgroundImageView.Frame.Y, m_BackgroundImageView.Frame.Width, m_BackgroundImageView.Frame.Height);
        }
    }
}
