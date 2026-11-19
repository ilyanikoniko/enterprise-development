using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DryCleaner.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class RenameToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_ItemId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "items");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "clients");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "orders",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "CompletionDays",
                table: "orders",
                newName: "completion_days");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "orders",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "AcceptanceDate",
                table: "orders",
                newName: "acceptance_date");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ItemId",
                table: "orders",
                newName: "IX_orders_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ClientId",
                table: "orders",
                newName: "IX_orders_client_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "items",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "items",
                newName: "material");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "items",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "items",
                newName: "IX_items_category_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "clients",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "clients",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RecommendedCleaningType",
                table: "categories",
                newName: "recommended_cleaning_type");

            migrationBuilder.RenameColumn(
                name: "CleaningPrice",
                table: "categories",
                newName: "cleaning_price");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_categories_category_id",
                table: "items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_clients_client_id",
                table: "orders",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_items_item_id",
                table: "orders",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_categories_category_id",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_clients_client_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_items_item_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "Orders",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "completion_days",
                table: "Orders",
                newName: "CompletionDays");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Orders",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "acceptance_date",
                table: "Orders",
                newName: "AcceptanceDate");

            migrationBuilder.RenameIndex(
                name: "IX_orders_item_id",
                table: "Orders",
                newName: "IX_Orders_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_client_id",
                table: "Orders",
                newName: "IX_Orders_ClientId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Items",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "material",
                table: "Items",
                newName: "Material");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Items",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Items",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_items_category_id",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Clients",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "recommended_cleaning_type",
                table: "Categories",
                newName: "RecommendedCleaningType");

            migrationBuilder.RenameColumn(
                name: "cleaning_price",
                table: "Categories",
                newName: "CleaningPrice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_ItemId",
                table: "Orders",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
