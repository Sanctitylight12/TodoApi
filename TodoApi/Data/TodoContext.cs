using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{

    //透過 DbContext，不必手動打 SQL、管理連線或交易，只要專注在 C# 物件操作上，EF Core 幫你完成底層的細節
    // 定義一個繼承 DbContext 的類別
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options)
        {
        
        }

        // 這裡告訴 EF Core：TodoItems 實體類別要對應資料庫裡的 TodoItems 資料表
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
