using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Concurrent;

namespace Utilities
{
    public enum UnityObjects
    {
        [Description("Hud")]
        Hud,

        [Description("PauseFrame")]
        PauseFrame,

        [Description("VictoryFrame")]
        VictoryFrame
    }

    public enum UnityScenes
    {
        [Description("MainMenu")]
        MainMenu,

        [Description("LoadGameAsync")]
        LoadGameAsync,

        [Description("SampleScene")]
        SampleScene,

        [Description("Defeat")]
        Defeat
    }

    public static class EnumExtensions
    {
        // Note that we never need to expire these cache items, so we just use ConcurrentDictionary rather than MemoryCache
        private static readonly 
            ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";

            var displayName = DisplayNameCache.GetOrAdd(key, x =>
            {
                var name = (DescriptionAttribute[])value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                return name.Length > 0 ? name[0].Description : value.ToString();
            });

            return displayName;
        }
    }
}