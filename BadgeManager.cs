using AnimeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace AnimeWatcherXYZ.WindowsPhone.WindowsPhoneAPI
{
    public class BadgeManager
    {
		
		// Define all badges here
		public static List<Badge> badges = new List<Badge>()
        {
            new Badge("First Steps", "Add your first anime to the watchlist.", async () => {
                if (Watchlist.watchlist.Count >= 1)
                    return true;
                else return false;
            }, 1),
            new Badge("That's How It All Started", "Watch 24 or more episodes.", async () =>
            {
                var count = (
                    from wl in Watchlist.watchlist
                    from eps in wl.Episodes.Where(o => o.Watched == true)
                    select eps).Count();
                if(count >= 24)
                    return true;
                else return false;
            }, 1),
            new Badge("Part of the Community", "Post your first anime review.", null, 2),
            new Badge("Talkative Kind of Guy", "Visit chat at least once.", null, 2),
        };

        public async static Task CheckForUnlockedBadges()
        {
            var notUnlockedYet = (badges.Where(o => o.ConditionCheck != null && !ApplicationData.Current.RoamingSettings.Values.ContainsKey(o.Name))).ToList();
            foreach(var b in notUnlockedYet)
            {
                var res = await b.ConditionCheck.Invoke();
                if (res == true)
                    b.Unlock();
                await Task.Delay(6);
            }
        }

        public static int GetNumberOfPoints()
        {
            int points = 0;
            foreach (var b in badges)
                if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(b.Name))
                    points += b.Points;
            return points;
        }

        public static int GetAllPoints()
        {
            return badges.Where(o => o.Hidden == false || (o.Hidden == true && o.IsUnlocked == true)).Sum(o => o.Points);
        }

        public static List<Badge> AllBadges()
        {
            return badges.Where(o => o.Hidden == false || (o.Hidden == true && o.IsUnlocked == true)).ToList();
        }

        public static Badge GetBadge(string name)
        {
            return badges.Where(o => o.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();
        }
    }
}