
using BussinessLayer.Abstract;
using BussinessLayer.Abstract.AbstractUow;
using BussinessLayer.AbstractValidator;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.UowConcrete;
using DataAccsesLayer.Abstract;
using DataAccsesLayer.EntityFreamework;
using DataAccsesLayer.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.Contianier
{
    public static class ProectStartupExtentions
    {
        public static void ContainerDepencies(this IServiceCollection services)
        {
            services.AddScoped<IDestinationService, DestinationManager>();
            services.AddScoped<IDestinationDal, EFDestinationDal>();

            services.AddScoped<IFeatureService, FeatureManager>();
            services.AddScoped<IFeatureDal, EFFeatureDal>();

            services.AddScoped<ISubAboutService, SubAboutManager>();
            services.AddScoped<ISubAboutDal, EFSubAboutDal>();

            services.AddScoped<ITestimionalService, TestimonialManager>();
            services.AddScoped<ITestimonialDal, EFTestimonialDal>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EFCommentDal>();

            services.AddScoped<IReservationService, ReservationManager>();
            services.AddScoped<IReservationDal, EFReservationDal>();

            services.AddScoped<IGuideService, GuideManager>();
            services.AddScoped<IGuideDal, EFGuideDal>();
            services.AddScoped<IAppUserDal, EFAppUserDal>();
            services.AddScoped<IAppuserService, AppUserManager>();

            services.AddScoped<IExcelService, ExcelManager>();
            services.AddScoped<IMailService, MailManger>();

            services.AddScoped<IContactUsSerivce, ContactUsManager>();
            services.AddScoped<IContactUsDal, EFContactUsDal>();

            services.AddScoped<IAnnounecementService, AnnouncementManager>();
            services.AddScoped<IAnnouncementDal, EFAnnouncementDal>();

            services.AddScoped<IAccountServiceUow, AccountOuwManager>();
            services.AddScoped<IAccountDal, EFAccountDal>();

            services.AddScoped<IUowDal, UowDal>();

        }
    }
}
