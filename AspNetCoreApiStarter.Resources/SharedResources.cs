using Microsoft.Extensions.Localization;

namespace AspNetCoreApiStarter.Resources
{
    public class SharedResources
    {
        private readonly IStringLocalizer _localizer;

        public SharedResources(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        public string this[string name]
        {
            get
            {
                return _localizer[name];
            }
        }
    }
}
