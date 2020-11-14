using Autofac;
using Core.Helpers.Auth;
using DataAccess;

namespace Business
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EFUserDao>().As<IUserDao>();

            builder.RegisterType<TokenHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthService>().As<IAuthService>();

            builder.RegisterType<EFQuestionDao>().As<IQuestionDao>();
            builder.RegisterType<QuestionService>().As<IQuestionService>();
        }
    }
}
