using AutoMapper;
using BusinessObjects = BugTracking.Models;
using EntityModel = DAL.EntityModel;


namespace BugTracking.Business.Mappings.Profiles
{
    class CustomPropertiesMappingProfile : Profile
    {
        public override string ProfileName => nameof(CustomPropertiesMappingProfile);

        protected override void Configure()
        {
            CreateMap<BusinessObjects.AspNetRole, EntityModel.AspNetRole>().ReverseMap();
            CreateMap<BusinessObjects.AspNetUser, EntityModel.AspNetUser>().ReverseMap();
            CreateMap<BusinessObjects.AspNetUserRole, EntityModel.AspNetUserRole>().ReverseMap();
            CreateMap<BusinessObjects.Organization, EntityModel.Organization>().ReverseMap();
            CreateMap<BusinessObjects.Priority, EntityModel.Priority>().ReverseMap();
            CreateMap<BusinessObjects.Product, EntityModel.Product>().ReverseMap();
            CreateMap<BusinessObjects.ProductOrganisation, EntityModel.ProductOrganisation>().ReverseMap();
            CreateMap<BusinessObjects.Responsible, EntityModel.Responsible>().ReverseMap();
            CreateMap<BusinessObjects.Status, EntityModel.Status>().ReverseMap();
            CreateMap<BusinessObjects.Ticket, EntityModel.Ticket>().ReverseMap();
            CreateMap<BusinessObjects.User, EntityModel.User>().ReverseMap();
            CreateMap<BusinessObjects.Category, EntityModel.Category>().ReverseMap();
            CreateMap<BusinessObjects.Ticket, EntityModel.User>().ReverseMap();

            //CreateMap<EntityModel.AspNetUser, BusinessObjects.DropDownAddInf>().ReverseMap();
            //CreateMap<EntityModel.User, BusinessObjects.AddInformation>().ReverseMap();

            CreateMap<EntityModel.User, BusinessObjects.RegisterViewModel>().ReverseMap();
            CreateMap<EntityModel.AspNetUserRole, BusinessObjects.RegisterViewModel>().ReverseMap();
        }
    }
}
