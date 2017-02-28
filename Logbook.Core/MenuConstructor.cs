using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Logbook.Core.DTO;

namespace Logbook.Core
{
    public static class MenuConstructor
    {
        public static string ConstructHtmlBackLink(string page, Guid id)
        {
            if (page == "EditActivity")
            {
                var activity = DataAccess.GetActivity(id);
                return $"<b>{Home()} > {Settings()} > {activity.Name}";
            }
            if (page == "EditField")
            {
                var field = DataAccess.GetField(id);
                var activity = DataAccess.GetActivity(field.ActivityId);
                return $"<b>{Home()} > {Settings()} > {EditActivityURL(activity)} > {field.Name}</b>";
            }
            if (page == "EditFieldOption")
            {
                var fieldOption = DataAccess.GetFieldOption(id);
                var field = DataAccess.GetField(fieldOption.FieldId);
                var activity = DataAccess.GetActivity(field.ActivityId);
                return $"<b>{Home()} > {Settings()} > {EditActivityURL(activity)} > {EditFieldURL(field)} > {fieldOption.Text}</b>";
            }
            return "test";
        }

        private static string Home()
        {
            return $"<a href=\"/\">Home</a>";
        }

        private static string Settings()
        {
            return $"<a href=\"/Settings\">Customise</a>";
        }

        private static string EditActivityURL(ActivityDTO activity)
        {
            return $"<a href =\"/Settings/EditActivity/{activity.ActivityId}\">{activity.Name}</a>";
        }

        private static string EditFieldURL(FieldDTO field)
        {
            return $"<a href =\"/Settings/EditField/{field.FieldId}\">{field.Name}</a>";
        }
    }
}
