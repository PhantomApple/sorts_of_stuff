namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //Создает экземпляр построителя веб-приложения.

            builder.Services.AddControllers(); //Регистрирует контроллеры MVC как сервисы.
            builder.Services.AddEndpointsApiExplorer(); // Добавляет службы исследователя API для обнаружения конечных точек.

            builder.Services.AddSwaggerGen(); //Добавляет службы Swagger для генерации документации API.

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            })); //Добавляет службы Cross-Origin Resource Sharing (CORS) для разрешения кросс-доменных запросов.

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) //Проверяет, работает ли приложение в среде разработки.
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection(); //Перенаправляет запросы HTTP на HTTPS.

            app.UseRouting();
            app.UseCors("corsapp"); // Настраивает политику CORS для приложения.

            app.UseAuthorization();
            app.MapControllers(); //Отображает контроллеры для обработки входящих HTTP-запросов.

            app.Run();


        }
    }
}

