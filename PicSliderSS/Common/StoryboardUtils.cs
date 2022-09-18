using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PicSliderSS.Common
{
    class StoryboardUtils
    {
        /// <summary>
        /// スライドアニメーションの生成
        /// </summary>
        /// <param name="target"></param>
        /// <param name="direction"></param>
        /// <param name="start"></param>
        /// <param name="middle"></param>
        /// <returns></returns>
        public static Storyboard SetSlideAnimation(Panel target, SlideDirection direction, double start, double middle)
        {
            double preWait = 1.5;
            double wait = 6.0;
            double afterWait = 1.5;
            var story = new Storyboard();
            var pt1 = new ParallelTimeline(TimeSpan.FromSeconds(preWait));
            var pt2 = new ParallelTimeline(TimeSpan.FromSeconds(preWait + wait));
            var pt3 = new ParallelTimeline(TimeSpan.FromSeconds(preWait + wait + afterWait));
            var show = new DoubleAnimation();
            var hide = new DoubleAnimation();
            show.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            hide.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            show.DecelerationRatio = 0.8;
            hide.AccelerationRatio = 0.8;
            show.To = middle;
            hide.To = start;
            switch (direction)
            {
                case SlideDirection.BottomToTop:
                case SlideDirection.TopToBottom:
                    Storyboard.SetTarget(show, target);
                    Storyboard.SetTargetProperty(show,  new PropertyPath(Canvas.TopProperty));
                    Storyboard.SetTarget(hide, target);
                    Storyboard.SetTargetProperty(hide,  new PropertyPath(Canvas.TopProperty));
                    break;
                case SlideDirection.LeftToRight:
                case SlideDirection.RightToLeft:
                default:
                    Storyboard.SetTarget(show, target);
                    Storyboard.SetTargetProperty(show,  new PropertyPath(Canvas.LeftProperty));
                    Storyboard.SetTarget(hide, target);
                    Storyboard.SetTargetProperty(hide,  new PropertyPath(Canvas.LeftProperty));
                    break;
            }
            pt1.Children.Add(show);
            pt2.Children.Add(hide);
            story.Children.Add(pt1);
            story.Children.Add(pt2);
            story.Children.Add(pt3);
            
            return story;
        }
    }
}
