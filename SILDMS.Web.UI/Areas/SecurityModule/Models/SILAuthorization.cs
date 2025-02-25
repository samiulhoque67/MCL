using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SILDMS.Web.UI.Areas.SecurityModule.Models
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public static class SILAuthorization
    {
        //public static string GetUserID()
        //{
        //    try { return Convert.ToString(HttpContext.Current.Session["UserID"]); }
        //    catch (Exception) { return ""; }
        //}


        public static string GetUserID()
        {
            try
            {
                if (HttpContext.Current?.Session == null)
                    return "Session is null";

                if (HttpContext.Current.Session["UserID"] == null)
                    return "UserID not found in session";

                return Convert.ToString(HttpContext.Current.Session["UserID"]);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public static string GetUserFullName()
        {
            try { return Convert.ToString(HttpContext.Current.Session["UserFullName"]); }
            catch (Exception) { return ""; }
        }

        public static string GetUserDesignation()
        {
            try { return Convert.ToString(HttpContext.Current.Session["UserDesignation"]); }
            catch (Exception) { return ""; }
        }


        public static string GetOwnerLevelID()
        {
            if (HttpContext.Current.Session["OwnerLevelID"] != null)
            {
                return Convert.ToString(HttpContext.Current.Session["OwnerLevelID"]);
            }
            return "";
        }

        public static string GetOwnerID()
        {
            if (HttpContext.Current.Session["OwnerID"] != null)
            {
                return Convert.ToString(HttpContext.Current.Session["OwnerID"]);
            }
            return "";
        }
    }
}