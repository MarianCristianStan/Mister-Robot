using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mister_Robot.Migrations
{
    /// <inheritdoc />
    public partial class v12productreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_Products_ProductId",
                table: "WishlistProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProduct_Wishlists_WishlistId",
                table: "WishlistProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFeatures",
                table: "ProductFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct");

            migrationBuilder.DropColumn(
                name: "FeatureName",
                table: "ProductFeatures");

            migrationBuilder.RenameTable(
                name: "WishlistProduct",
                newName: "WishlistProducts");

            migrationBuilder.RenameTable(
                name: "CartProduct",
                newName: "CartProducts");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistProduct_ProductId",
                table: "WishlistProducts",
                newName: "IX_WishlistProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_ProductId",
                table: "CartProducts",
                newName: "IX_CartProducts_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductFeatures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "FeatureId",
                table: "ProductFeatures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFeatures",
                table: "ProductFeatures",
                columns: new[] { "ProductId", "FeatureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistProducts",
                table: "WishlistProducts",
                columns: new[] { "WishlistId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductCategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                    table.ForeignKey(
                        name: "FK_Features_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_FeatureId",
                table: "ProductFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductCategoryId",
                table: "Features",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_Features_FeatureId",
                table: "ProductFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProducts_Products_ProductId",
                table: "WishlistProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProducts_Wishlists_WishlistId",
                table: "WishlistProducts",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_Features_FeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProducts_Products_ProductId",
                table: "WishlistProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistProducts_Wishlists_WishlistId",
                table: "WishlistProducts");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFeatures",
                table: "ProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_FeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistProducts",
                table: "WishlistProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "ProductFeatures");

            migrationBuilder.RenameTable(
                name: "WishlistProducts",
                newName: "WishlistProduct");

            migrationBuilder.RenameTable(
                name: "CartProducts",
                newName: "CartProduct");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistProducts_ProductId",
                table: "WishlistProduct",
                newName: "IX_WishlistProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProduct",
                newName: "IX_CartProduct_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductFeatures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                table: "ProductFeatures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFeatures",
                table: "ProductFeatures",
                columns: new[] { "ProductId", "FeatureName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistProduct",
                table: "WishlistProduct",
                columns: new[] { "WishlistId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_Products_ProductId",
                table: "WishlistProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistProduct_Wishlists_WishlistId",
                table: "WishlistProduct",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "WishlistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
