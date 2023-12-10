using OutputCaching.Enums;

namespace OutputCaching.Models.Options;

public class CacheSettings
{
    public static string SectionName => "CacheSettings";
    public OutputCacheType OutputCacheType { get; set; }
}