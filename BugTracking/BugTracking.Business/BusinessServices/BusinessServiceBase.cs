using AutoMapper;
using Common.Logging;
using BugTracking.Business.Mappings.Profiles;
using System.Runtime.CompilerServices;
using Common.Extensions;

namespace BugTracking.Business
{
    public abstract class BusinessServiceBase
    {
        protected static readonly ILogger Logger = LoggerFactory.GetLogger(LoggingComponent.Bll);
        private readonly string ErrorOccuredInMethodMessageStringFormat = "Error occurred in method {0}.";

        protected static readonly IMapper MapperAllLevels;
        protected static readonly IMapper MapperTopLevel;
        protected static readonly IMapper MapperMonitoringData;

        static BusinessServiceBase()
        {
            var mapperConfigurationAllLevels = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomPropertiesMappingProfile());
            });
            MapperAllLevels = mapperConfigurationAllLevels.CreateMapper();
            
        }

        protected string GetErrorOccurredInMethodMessage([CallerMemberName] string methodName = "") => ErrorOccuredInMethodMessageStringFormat.FormatWith(methodName);

    }
}
