using SharpAITest.API.Extensions;

namespace SharpAITest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();
            builder.Services.AddLocalDependencies();
            var app = builder.Build();
            app.AddDevTools();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
