
namespace Acme.Helpers
{
    public enum ShieldStyle { Flat, Plastic, FlatSquare, }
    public enum ShieldColor { BrightGreen, Green, YellowGreen, Yellow, Orange, Red, LightGrey, Blue, }
    public enum ShieldImage { Png, Svg, }
    public enum ShieldType
    {
        BowerVersion, NodeVersion, GithubTag, GithubRelease, NugetVersion, NugetPreRelease,
        GithubIssues, GithubForks, GithubStars, GithubFollowers,
    };
}
