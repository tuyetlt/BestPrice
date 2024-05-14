using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
/// <summary>
/// Summary description for CacheUtitl
/// </summary>
public static class CacheUtitl
{
    private static DateTime cTime = DateTime.Now.AddHours(2);

    public static void Set<T>(string key, T obj, CacheItemPriority priortity = CacheItemPriority.Normal) where T : class
    {
        HttpContext.Current.Cache.Remove(key);
        HttpContext.Current.Cache.Add(key, obj, null, cTime, System.Web.Caching.Cache.NoSlidingExpiration, priortity, null);
    }
    public static T Get<T>(string key) where T : class
    {
        try
        {
            T obj = (T)HttpContext.Current.Cache.Get(key);
            return obj;
        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }

	public static string  GetCacheUtcExpiryDateTime(string cacheKey)
	{
		object cacheEntry = HttpContext.Current.Cache.GetType().GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(HttpContext.Current.Cache, new object[] { cacheKey, 1 });
		PropertyInfo utcExpiresProperty = cacheEntry.GetType().GetProperty("UtcExpires", BindingFlags.NonPublic | BindingFlags.Instance);
		DateTime utcExpiresValue = (DateTime)utcExpiresProperty.GetValue(cacheEntry, null);

		return utcExpiresValue.ToShortTimeString();
	}

    public static void Clear(string key)
    {
        HttpContext.Current.Cache.Remove(key);
    }
    public static void ClearAll(string refix = "")
    {
        foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
        {
            if (Utils.IsNullOrEmpty(refix))
                HttpContext.Current.Cache.Remove(entry.Key.ToString());
            else
            {
                if (entry.Key.ToString().ToLower().Contains(refix.ToLower()))
                    HttpContext.Current.Cache.Remove(entry.Key.ToString());
            }
        }
    }
    public static bool CheckCache(string key)
    {
        try
        {
            if (HttpContext.Current.Cache.Get(key) != null)
                return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }
}
