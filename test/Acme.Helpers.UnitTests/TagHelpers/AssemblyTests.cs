using Acme.Helpers.TagHelpers;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Acme.Helpers.UnitTests
{
    public class AssemblyTests
    {
        private const string NAMESPACE = "Acme.Helpers.TagHelpers";

        [Fact]
        public void AcmeHelpers_ControlsAreDefinedInNamespace_TagHelpers()
        {
            var assembly = Assembly.GetAssembly(typeof(DisplayTagHelper));
            var helpers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TagHelper)));
            foreach (var helper in helpers)
            {
                Assert.Equal(helper.Namespace, NAMESPACE);
            }
        }

        [Fact]
        public void AcmeHelpersDemo_ControlsAreDefinedInNamespace_TagHelpers()
        {
            var assembly = Assembly.GetAssembly(typeof(GithubForkmeTagHelper));
            var helpers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TagHelper)));
            foreach (var helper in helpers)
            {
                Assert.Equal(helper.Namespace, NAMESPACE);
            }

        }

        [Fact]
        public void AcmeHelpersPager_ControlsAreDefinedInNamespace_TagHelpers()
        {
            var assembly = Assembly.GetAssembly(typeof(PagerTagHelper));
            var helpers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TagHelper)));
            foreach (var helper in helpers)
            {
                Assert.Equal(helper.Namespace, NAMESPACE);
            }
        }

        [Fact]
        public void AcmeHelpersTable_ControlsAreDefinedInNamespace_TagHelpers()
        {
            var assembly = Assembly.GetAssembly(typeof(TableTagHelper));
            var helpers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TagHelper)));
            foreach (var helper in helpers)
            {
                Assert.Equal(helper.Namespace, NAMESPACE);
            }
        }
    }
}
