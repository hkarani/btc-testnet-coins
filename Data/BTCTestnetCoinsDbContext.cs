namespace BTCTestnetCoins.Data
{
    public class BTCTestnetCoinsDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"DataSource ={AppDomain.CurrentDomain.BaseDirectory}BTCTestnetCoinsDB.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
