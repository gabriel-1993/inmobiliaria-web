namespace InmobiliariaVargasHuancaTorrez.Models;

public abstract class RepositorioBase
{
  protected readonly string? connectionString;
  protected RepositorioBase(IConfiguration configuration)
  {
    connectionString = configuration.GetConnectionString("DefaultConnection");
    // connectionString = configuration["ConnectionStrings:SqlServer"];
  }
}