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
            builder.RegisterType<EFUserActivationDao>().As<IUserActivationDao>();

            builder.RegisterType<TokenHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthService>().As<IAuthService>();

            builder.RegisterType<EFQuestionDao>().As<IQuestionDao>();
            builder.RegisterType<QuestionService>().As<IQuestionService>();

            builder.RegisterType<EFAnswerDao>().As<IAnswerDao>();
            builder.RegisterType<AnswerService>().As<IAnswerService>();
            builder.RegisterType<EFAnswerLikeDao>().As<IAnswerLikeDao>();
        }
    }
}
