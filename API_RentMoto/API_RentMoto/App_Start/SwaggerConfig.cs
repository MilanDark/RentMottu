using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using SeuProjeto;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SeuProjeto
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Swagger_RentMoto");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                }).EnableSwaggerUi();
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\API_RentMoto.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
