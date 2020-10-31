using Autofac;
using DataAccess;

namespace Business
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EFUserDao>().As<IUserDao>();
        }
    }
}
