using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DryCleaner.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    recommended_cleaning_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cleaning_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    material = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    acceptance_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    completion_days = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "cleaning_price", "name", "recommended_cleaning_type" },
                values: new object[,]
                {
                    { 1, 1600m, "Верхняя одежда", "Аквачистка" },
                    { 2, 1400m, "Деловые костюмы", "Сухая чистка" },
                    { 3, 1100m, "Вечерние платья", "Ручная стирка" },
                    { 4, 450m, "Сорочки", "Машинная стирка" },
                    { 5, 750m, "Брюки и джинсы", "Аквачистка" },
                    { 6, 2200m, "Пальто и шубы", "Сухая чистка" },
                    { 7, 1350m, "Куртки и ветровки", "Аквачистка" },
                    { 8, 1900m, "Домашний текстиль", "Ручная стирка" },
                    { 9, 1500m, "Постельные принадлежности", "Машинная стирка" },
                    { 10, 950m, "Обувь", "Специальный уход" }
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "full_name", "phone_number" },
                values: new object[,]
                {
                    { 1, "Романов Денис Олегович", "+79161110001" },
                    { 2, "Ковалёва Марина Игоревна", "+79161110002" },
                    { 3, "Егоров Павел Андреевич", "+79161110003" },
                    { 4, "Тихонова Светлана Юрьевна", "+79161110004" },
                    { 5, "Крылов Артём Витальевич", "+79161110005" },
                    { 6, "Зайцева Полина Дмитриевна", "+79161110006" },
                    { 7, "Медведев Кирилл Романович", "+79161110007" },
                    { 8, "Орлова Вероника Павловна", "+79161110008" },
                    { 9, "Гусев Станислав Игоревич", "+79161110009" },
                    { 10, "Ларина Алина Максимовна", "+79161110010" },
                    { 11, "Белов Егор Сергеевич", "+79161110011" },
                    { 12, "Панова Дарья Алексеевна", "+79161110012" },
                    { 13, "Фомин Никита Владимирович", "+79161110013" },
                    { 14, "Киселёва Софья Романовна", "+79161110014" },
                    { 15, "Шестаков Максим Олегович", "+79161110015" }
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "id", "category_id", "material", "name" },
                values: new object[,]
                {
                    { 1, 1, "Шерсть", "Пальто шерстяное" },
                    { 2, 1, "Полиэстер", "Пуховик зимний" },
                    { 3, 2, "Шерсть", "Костюм шерстяной" },
                    { 4, 2, "Лён", "Костюм льняной" },
                    { 5, 3, "Шёлк", "Платье шёлковое" },
                    { 6, 3, "Хлопок", "Платье хлопковое" },
                    { 7, 4, "Хлопок", "Сорочка хлопковая" },
                    { 8, 4, "Лён", "Сорочка льняная" },
                    { 9, 5, "Шерсть", "Брюки шерстяные" },
                    { 10, 5, "Деним", "Джинсы классические" },
                    { 11, 6, "Кашемир", "Пальто кашемировое" },
                    { 12, 6, "Полиэстер", "Шуба искусственная" },
                    { 13, 7, "Кожа", "Куртка кожаная" },
                    { 14, 7, "Полиэстер", "Ветровка спортивная" },
                    { 15, 8, "Хлопок", "Покрывало хлопковое" },
                    { 16, 8, "Лён", "Скатерть льняная" },
                    { 17, 9, "Шерсть", "Одеяло шерстяное" },
                    { 18, 9, "Полиэстер", "Плед флисовый" },
                    { 19, 10, "Кожа", "Ботинки кожаные" },
                    { 20, 10, "Замша", "Сапоги замшевые" }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "acceptance_date", "client_id", "completion_days", "item_id", "status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7, 1, 3 },
                    { 2, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 5, 3 },
                    { 3, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12, 9, 1 },
                    { 4, new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, 15, 1 },
                    { 5, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 3, 3 },
                    { 6, new DateTime(2025, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8, 7, 3 },
                    { 7, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10, 11, 2 },
                    { 8, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, 17, 1 },
                    { 9, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, 2, 3 },
                    { 10, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 7, 3 },
                    { 11, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, 11, 2 },
                    { 12, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, 4, 3 },
                    { 13, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11, 13, 3 },
                    { 14, new DateTime(2026, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 18, 2 },
                    { 15, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, 6, 1 },
                    { 16, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, 6, 3 },
                    { 17, new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 14, 13, 3 },
                    { 18, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 16, 17, 1 },
                    { 19, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 9, 3 },
                    { 20, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, 19, 2 },
                    { 21, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, 15, 1 },
                    { 22, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6, 3, 3 },
                    { 23, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 18, 10, 3 },
                    { 24, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 15, 19, 2 },
                    { 25, new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7, 14, 3 },
                    { 26, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12, 1, 1 },
                    { 27, new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 8, 3 },
                    { 28, new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 7, 14, 3 },
                    { 29, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 11, 18, 2 },
                    { 30, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 6, 20, 1 },
                    { 31, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 8, 4, 3 },
                    { 32, new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 10, 12, 3 },
                    { 33, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 12, 20, 2 },
                    { 34, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4, 5, 3 },
                    { 35, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 6, 16, 3 },
                    { 36, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 10, 6, 1 },
                    { 37, new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 9, 1, 3 },
                    { 38, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 13, 15, 3 },
                    { 39, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 17, 7, 2 },
                    { 40, new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 6, 2, 3 },
                    { 41, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 11, 8, 1 },
                    { 42, new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 7, 3, 3 },
                    { 43, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 9, 9, 2 },
                    { 44, new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 10, 4, 3 },
                    { 45, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 15, 10, 1 },
                    { 46, new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 14, 11, 3 },
                    { 47, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 18, 16, 1 },
                    { 48, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 16, 12, 1 },
                    { 49, new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 5, 14, 2 },
                    { 50, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 10, 20, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_category_id",
                table: "items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_item_id",
                table: "orders",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
