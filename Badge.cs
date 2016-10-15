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
    public class Badge
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private BadgeType type;

        public BadgeType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private bool hidden;

        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public bool IsUnlocked { get
            {
                if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(Name))
                    return true;
                return false;
            }
        }

        public Func<Task<bool>> ConditionCheck;
        public Badge(string n, string d, Func<Task<bool>> cc, int pt, BadgeType tp=BadgeType.General, bool hid=false)
        {
            name = n;
            description = d;
            ConditionCheck = cc;
            points = pt;
            type = tp;
            hidden = hid;
        }

        public void Unlock()
        {
            if(!ApplicationData.Current.RoamingSettings.Values.ContainsKey(this.name))
            {
                ApplicationData.Current.RoamingSettings.Values[this.name] = true;

                string toastXml = "<toast>"
                        + "<visual version='1'>"
                        + "<binding template='ToastText02'>"
                        + "<text id='1'>Badge Unlocked!</text>"
                        + "<text id='2'>"+ name +"</text>"
                        + "</binding>"
                        + "</visual>"
                        + "</toast>";
                XmlDocument toast = new XmlDocument();
                toast.LoadXml(toastXml);
                ToastNotification updatedToast = new ToastNotification(toast);
                ToastNotificationManager.CreateToastNotifier().Show(updatedToast);
            }
        }

        public override string ToString()
        {
            return name + " (" + Points + "p)";
        }
    }
}
