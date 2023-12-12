namespace Bogus.Models.Options;

public class FakeDataOptions
{
    public static string SectionName => "FakeData";
    public int? Seed { get; set; }
    public string Locale { get; set; }
}