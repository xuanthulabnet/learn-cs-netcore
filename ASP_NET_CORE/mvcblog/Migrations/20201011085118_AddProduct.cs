using Microsoft.EntityFrameworkCore.Migrations;

namespace mvcblog.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Số 1 cao 40cm rộng 20cm dày 20cm màu xanh lá cây đậm", "Đá phong thuỷ tự nhiên", 1000000m },
                    { 2, "Trang trí trong nhà Chất liệu : • Đá muối", "Đèn đá muối hình tròn", 1500000m },
                    { 3, "Tranh sơn mài loại nhỏ 15x 15 giá 50.000", "Tranh sơn mài", 50000m },
                    { 4, "Nguyên liệu thể hiện :    Sơn dầu", "Tranh sơn dầu - Ngựa", 450000m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
