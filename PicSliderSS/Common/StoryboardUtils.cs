using PicSliderSS.PicSliderGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PicSliderSS.Common
{
    class StoryboardUtils
    {
        public static void LinkingShowWaitHidden(Storyboard show, Storyboard hidden, Storyboard wait)
        {
            show.Completed += (o, e) => { wait.Begin(); };
            wait.Completed += (o, e) => { hidden.Begin(); };
        }

        public static Storyboard CreateSlideStoryboard(IEnumerable<Grid> grids)
        {
            Storyboard sb = new Storyboard();

            ParallelTimeline pt1 = new ParallelTimeline(TimeSpan.FromSeconds(1.3));
            ParallelTimeline pt2 = new ParallelTimeline(TimeSpan.FromSeconds(4.0));
            ParallelTimeline pt3 = new ParallelTimeline(TimeSpan.FromSeconds(5.0));

            foreach (Grid grid in grids)
            {
                pt1.Children.Add(CreateShowAnimation(grid));
                pt2.Children.Add(CreateHiddenAnimation(grid));
            }

            sb.Children.Add(pt1);
            sb.Children.Add(pt2);
            sb.Children.Add(pt3);

            return sb;
        }

        public static Storyboard CreateShowStoryboard()
        {
            Storyboard sb = new Storyboard();
            sb.BeginTime = TimeSpan.FromSeconds(0);
            sb.FillBehavior = FillBehavior.HoldEnd;
            return sb;
        }
        public static Storyboard CreateHiddenStoryboard()
        {
            Storyboard sb = new Storyboard();
            sb.BeginTime = TimeSpan.FromSeconds(0);
            sb.FillBehavior = FillBehavior.HoldEnd;
            return sb;
        }
        public static Storyboard CreateWaitStoryboard()
        {
            Storyboard sb = new Storyboard();
            sb.BeginTime = TimeSpan.FromSeconds(0);
            sb.FillBehavior = FillBehavior.HoldEnd;
            sb.SlipBehavior = SlipBehavior.Grow;
            return sb;
        }

        public static ThicknessAnimation CreateShowAnimation(Grid imageGrid)
        {
            ThicknessAnimation ta = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(0.3)), FillBehavior.HoldEnd);
            ta.DecelerationRatio = 0.8;
            Storyboard.SetTarget(ta, imageGrid);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Grid.MarginProperty));

            return ta;
        }

        public static ThicknessAnimation CreateHiddenAnimation(Grid imageGrid)
        {
            ThicknessAnimation ta = new ThicknessAnimation(imageGrid.Margin, new Duration(TimeSpan.FromSeconds(0.3)), FillBehavior.HoldEnd);
            ta.AccelerationRatio = 0.8;
            Storyboard.SetTarget(ta, imageGrid);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Grid.MarginProperty));
            ta.FillBehavior = FillBehavior.HoldEnd;

            return ta;
        }

        public static ThicknessAnimation CreateWaitAnimation(Grid imageGrid)
        {
            ThicknessAnimation ta = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(3)), FillBehavior.HoldEnd);
            Storyboard.SetTarget(ta, imageGrid);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Grid.MarginProperty));
            ta.FillBehavior = FillBehavior.HoldEnd;

            return ta;
        }


    }
}
