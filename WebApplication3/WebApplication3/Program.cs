namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //������� ��������� ����������� ���-����������.

            builder.Services.AddControllers(); //������������ ����������� MVC ��� �������.
            builder.Services.AddEndpointsApiExplorer(); // ��������� ������ ������������� API ��� ����������� �������� �����.

            builder.Services.AddSwaggerGen(); //��������� ������ Swagger ��� ��������� ������������ API.

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            })); //��������� ������ Cross-Origin Resource Sharing (CORS) ��� ���������� �����-�������� ��������.

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) //���������, �������� �� ���������� � ����� ����������.
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection(); //�������������� ������� HTTP �� HTTPS.

            app.UseRouting();
            app.UseCors("corsapp"); // ����������� �������� CORS ��� ����������.

            app.UseAuthorization();
            app.MapControllers(); //���������� ����������� ��� ��������� �������� HTTP-��������.

            app.Run();


        }
    }
}

