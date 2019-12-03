using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class AddProductDataBobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4b1ac532-98cb-4adf-ab7e-7f10a4573ca0", "AQAAAAEAACcQAAAAECTKSq/o3JGQoSumLeoBXGaDIxApbR7BRh96ikfeF/Ev9xS72H9siMOtPMCnGhdXRw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88c6a179-d8d5-4725-a68a-c9ea05ce1342", "AQAAAAEAACcQAAAAEDRe/49jv1JNx/Svm9jaOKHTk+/K/F1ZsN44TXSIkqWY5FfERsjhx0AS+zFR5jci1A==" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 6, true, null, "They are environmentally friendly wheels", null, 69.989999999999995, 10, 100, "Prius Wheels", "00000000-ffff-ffff-ffff-fffffffff123" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 7, true, null, "They are environmentally friendly mats for the winter", null, 99.989999999999995, 10, 20, "Prius Winter Mats", "00000000-ffff-ffff-ffff-fffffffff123" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 8, true, null, "Environmentally friendly oil filters", null, 12.99, 10, 200, "Prius Oil Filter", "00000000-ffff-ffff-ffff-fffffffff123" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 9, true, null, "I thought this game is free???", null, 59.990000000000002, 4, 80, "Team Fight Tactics", "00000000-ffff-ffff-ffff-fffffffff123" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 10, true, null, "One of the best games ever made...", null, 59.990000000000002, 4, 100, "God of War 3", "00000000-ffff-ffff-ffff-fffffffff123" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "City", "Description", "ImagePath", "Price", "ProductTypeId", "Quantity", "Title", "UserId" },
                values: new object[] { 11, true, null, "For people who play hockey", null, 270.0, 9, 120, "Ice Skates", "00000000-ffff-ffff-ffff-fffffffff123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cbc856c2-ff09-452a-ac32-867360db414b", "AQAAAAEAACcQAAAAEM9WryXqreeNjgujEMOkn7lL2vaedhC/ZjSJv21ivjTgsyO4r53JTMQ13cQTVCs9RA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ceb9897-b73c-475a-8beb-0ad6ad848ab2", "AQAAAAEAACcQAAAAEP9OFfj7/kJOrv87Bbe96F5PtTyZb0y1U6m/XEN1peHUZVujI1h5R6bDU0Tl0MEvYQ==" });
        }
    }
}
