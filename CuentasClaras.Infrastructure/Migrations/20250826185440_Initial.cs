using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CuentasClaras.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CommisionPercentage = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CommissionPercentage = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localities_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: false),
                    CollectorId = table.Column<int>(type: "int", nullable: false),
                    SalesmanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Collectors_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    BusinessTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OrderStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_BusinessTypes_BusinessTypeId",
                        column: x => x.BusinessTypeId,
                        principalTable: "BusinessTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    ProviderUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubcategoryId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Subcategories_SubcategoryId1",
                        column: x => x.SubcategoryId1,
                        principalTable: "Subcategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountTransference = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPlans_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BusinessTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Restaurante" },
                    { 2, "Bar" },
                    { 3, "Cafetería" },
                    { 4, "Pizzería" },
                    { 5, "Heladería" },
                    { 6, "Kiosco" },
                    { 7, "Almacén" },
                    { 8, "Farmacia" },
                    { 9, "Taller Mecánico" },
                    { 10, "Ferretería" },
                    { 11, "Librería" },
                    { 12, "Panadería" }
                });

            migrationBuilder.InsertData(
                table: "Collectors",
                columns: new[] { "Id", "CommisionPercentage", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 0, "Cabrera, Sebastian", "" },
                    { 2, 0, "Vitteli, Victor", "" },
                    { 3, 0, "Perez, Martin", "" },
                    { 4, 0, "Alfonso, Miguel", "" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Capital" },
                    { 2, "Godoy Cruz" },
                    { 3, "Las Heras" },
                    { 4, "Guaymallén" },
                    { 5, "Luján de Cuyo" },
                    { 6, "Maipú" },
                    { 7, "San Martín" },
                    { 8, "Rivadavia" },
                    { 9, "Junín" },
                    { 10, "San Carlos" },
                    { 11, "Tunuyán" },
                    { 12, "Tupungato" },
                    { 13, "San Rafael" },
                    { 14, "General Alvear" },
                    { 15, "Malargüe" },
                    { 16, "La Paz" },
                    { 17, "Santa Rosa" },
                    { 18, "Lavalle" }
                });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Equipamiento Comercial" },
                    { 2, "Hogar" },
                    { 3, "Bazar" },
                    { 4, "Rodados" },
                    { 5, "Ferreteria" },
                    { 6, "Muebles de interior" }
                });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Diario" },
                    { 2, "Semanal" },
                    { 3, "Quincenal" },
                    { 4, "Mensual" }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "EndDate", "InterestRate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.050m, "Plan Básico 2025", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2027, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.065m, "Plan Plus 24 meses", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.080m, "Plan Premium Anual", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.055m, "Plan Estacional Verano 2025-26", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.070m, "Plan Especial 12 Pagos", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Salesmen",
                columns: new[] { "Id", "CommissionPercentage", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 0, "Cabrera, Sebastian", "" },
                    { 2, 5, "Vitteli, Victor", "" },
                    { 3, 5, "Carletto, Pablo", "" },
                    { 4, 7, "Alfonso, Miguel", "" },
                    { 5, 5, "Rivero, Karen", "" },
                    { 6, 5, "Vitelli, Joaquin", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "LineId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "COMERCIOS" },
                    { 2, 1, "EQUIPOS DE FRIO" },
                    { 3, 1, "GASTRONOMIA" },
                    { 4, 1, "CARNICERIA" },
                    { 5, 1, "PANADERIA" },
                    { 6, 2, "ELECTRO HOGAR" },
                    { 7, 2, "ELECTRO COCINA" },
                    { 8, 2, "CUIDADO PERSONAL" },
                    { 9, 2, "AUDIO & TV" },
                    { 10, 2, "CALEFACCION" },
                    { 11, 2, "CLIMATIZACION" },
                    { 12, 2, "PEQUEÑOS ELECTRO" },
                    { 13, 3, "BAZAR EN ACERO INOXIDABLE" },
                    { 14, 3, "CRISTALERIA" },
                    { 15, 3, "PLASTICOS" },
                    { 16, 3, "ALUMINIO" },
                    { 17, 3, "COCINA" },
                    { 18, 3, "MENAJE" },
                    { 19, 4, "CICLISMO" },
                    { 20, 4, "MOTOVEHICULOS" },
                    { 21, 4, "BATERIAS" },
                    { 22, 5, "HERRAMIENTAS" },
                    { 23, 5, "ELECTRICIDAD" },
                    { 24, 5, "JARDIN" },
                    { 25, 5, "PLOMERIA" },
                    { 26, 6, "MUEBLES DE COCINA" },
                    { 27, 6, "MUEBLES DE HABITACION" },
                    { 28, 6, "MUEBLES DE LIVING / COMEDOR" }
                });

            migrationBuilder.InsertData(
                table: "Localities",
                columns: new[] { "Id", "DepartmentId", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 101, 1, "1ª Sección Parque Central", "M5500" },
                    { 102, 1, "2ª Sección Barrio Cívico", "M5500" },
                    { 103, 1, "3ª Sección Parque O'Higgins", "M5500" },
                    { 104, 1, "4ª Sección Área Fundacional", "M5500" },
                    { 105, 1, "5ª Sección Residencial Sur", "M5500" },
                    { 106, 1, "6ª Sección Residencial Norte", "M5500" },
                    { 107, 1, "7ª Sección Residencial Parque", "M5500" },
                    { 108, 1, "8ª Sección Aeroparque", "M5500" },
                    { 109, 1, "9ª Sección Parque Gral. San Martín", "M5500" },
                    { 110, 1, "10ª Sección Residencial Los Cerros", "M5500" },
                    { 111, 1, "11ª Sección San Agustín", "M5500" },
                    { 112, 1, "12ª Sección Piedemonte", "M5500" },
                    { 201, 2, "Gobernador Benegas", "M5501" },
                    { 202, 2, "Godoy Cruz", "M5501" },
                    { 203, 2, "Las Tortugas", "M5501" },
                    { 204, 2, "Presidente Sarmiento", "M5501" },
                    { 205, 2, "San Francisco del Monte", "M5501" },
                    { 206, 2, "Trapiche", "M5501" },
                    { 207, 2, "Villa Marini", "M5501" },
                    { 208, 2, "Villa Hipódromo", "M5501" },
                    { 209, 2, "Villa del Parque", "M5501" },
                    { 301, 3, "Capdevilla", "M5539" },
                    { 302, 3, "El Algarrobal", "M5539" },
                    { 303, 3, "El Borbollón", "M5539" },
                    { 304, 3, "El Challao", "M5539" },
                    { 305, 3, "El Pastal", "M5539" },
                    { 306, 3, "El Plumerillo", "M5539" },
                    { 307, 3, "El Resguardo", "M5539" },
                    { 308, 3, "El Zapallar", "M5539" },
                    { 309, 3, "La Cieneguita", "M5539" },
                    { 310, 3, "Las Cuevas", "M5539" },
                    { 311, 3, "Las Heras", "M5539" },
                    { 312, 3, "Panquehua", "M5539" },
                    { 313, 3, "Uspallata", "M5545" },
                    { 314, 3, "Puente del Inca", "M5553" },
                    { 315, 3, "Los Penitentes", "M5553" },
                    { 401, 4, "Belgrano", "M5521" },
                    { 402, 4, "El Bermejo", "M5521" },
                    { 403, 4, "Buena Nueva", "M5523" },
                    { 404, 4, "Capilla del Rosario", "M5521" },
                    { 405, 4, "Colonia Molina", "M5521" },
                    { 406, 4, "Colonia Segovia", "M5521" },
                    { 407, 4, "Dorrego", "M5521" },
                    { 408, 4, "El Sauce", "M5521" },
                    { 409, 4, "Jesús Nazareno", "M5521" },
                    { 410, 4, "Kilómetro 8", "M5521" },
                    { 411, 4, "Kilómetro 11", "M5521" },
                    { 412, 4, "La Primavera", "M5521" },
                    { 413, 4, "Las Cañas", "M5521" },
                    { 414, 4, "Los Corralitos", "M5521" },
                    { 415, 4, "Nueva Ciudad", "M5521" },
                    { 416, 4, "Pedro Molina", "M5521" },
                    { 417, 4, "Puente de Hierro", "M5521" },
                    { 418, 4, "Rodeo de la Cruz", "M5521" },
                    { 419, 4, "San Francisco del Monte", "M5521" },
                    { 420, 4, "San José", "M5521" },
                    { 421, 4, "Villa Nueva", "M5521" },
                    { 501, 5, "Agrelo", "M5509" },
                    { 502, 5, "Cacheuta", "M5569" },
                    { 503, 5, "Carrodilla", "M5505" },
                    { 504, 5, "Chacras de Coria", "M5505" },
                    { 505, 5, "El Carrizal", "M5535" },
                    { 506, 5, "Industrial", "M5507" },
                    { 507, 5, "La Puntilla", "M5509" },
                    { 508, 5, "Las Compuertas", "M5507" },
                    { 509, 5, "Luján de Cuyo", "M5507" },
                    { 510, 5, "Mayor Drummond", "M5507" },
                    { 511, 5, "Perdriel", "M5544" },
                    { 512, 5, "Potrerillos", "M5552" },
                    { 513, 5, "Ugarteche", "M5546" },
                    { 514, 5, "Vistalba", "M5509" },
                    { 515, 5, "Vertientes del Pedemonte", "M5507" },
                    { 601, 6, "Colonia Bombal", "M5515" },
                    { 602, 6, "Coquimbito", "M5511" },
                    { 603, 6, "Cruz de Piedra", "M5517" },
                    { 604, 6, "Fray Luis Beltrán", "M5613" },
                    { 605, 6, "General Gutiérrez", "M5519" },
                    { 606, 6, "General Ortega", "M5515" },
                    { 607, 6, "Las Barrancas", "M5513" },
                    { 608, 6, "Lunlunta", "M5519" },
                    { 609, 6, "Luzuriaga", "M5519" },
                    { 610, 6, "Maipú", "M5515" },
                    { 611, 6, "Rodeo del Medio", "M5513" },
                    { 612, 6, "Russell", "M5519" },
                    { 613, 6, "San Roque", "M5519" },
                    { 614, 6, "Santa Blanca", "M5515" },
                    { 701, 7, "Alto Salvador", "M5570" },
                    { 702, 7, "Alto Verde", "M5570" },
                    { 703, 7, "Buen Orden", "M5570" },
                    { 704, 7, "Chapanay", "M5570" },
                    { 705, 7, "Chivilcoy", "M5570" },
                    { 706, 7, "El Central", "M5570" },
                    { 707, 7, "El Divisadero", "M5570" },
                    { 708, 7, "El Espino", "M5570" },
                    { 709, 7, "El Ramblón", "M5570" },
                    { 710, 7, "Ingeniero Giagnoni", "M5570" },
                    { 711, 7, "Las Chimbas", "M5570" },
                    { 712, 7, "Montecaseros", "M5570" },
                    { 713, 7, "Nueva California", "M5570" },
                    { 714, 7, "Palmira", "M5571" },
                    { 715, 7, "San Martín", "M5570" },
                    { 716, 7, "Tres Porteñas", "M5570" },
                    { 801, 8, "Andrade", "M5577" },
                    { 802, 8, "El Mirador", "M5577" },
                    { 803, 8, "La Central", "M5577" },
                    { 804, 8, "La Libertad", "M5577" },
                    { 805, 8, "Los Árboles", "M5577" },
                    { 806, 8, "Los Campamentos", "M5577" },
                    { 807, 8, "Los Huarpes", "M5577" },
                    { 808, 8, "Medrano", "M5577" },
                    { 809, 8, "Mundo Nuevo", "M5577" },
                    { 810, 8, "Reducción", "M5577" },
                    { 811, 8, "Rivadavia", "M5577" },
                    { 812, 8, "Santa María de Oro", "M5577" },
                    { 813, 8, "San Isidro", "M5577" },
                    { 901, 9, "Algarrobo Grande", "M5573" },
                    { 902, 9, "Alto Verde", "M5573" },
                    { 903, 9, "Ingeniero Giagnoni", "M5573" },
                    { 904, 9, "Junín", "M5573" },
                    { 905, 9, "La Colonia", "M5573" },
                    { 906, 9, "Los Barriales", "M5573" },
                    { 907, 9, "Medrano", "M5573" },
                    { 908, 9, "Mundo Nuevo", "M5573" },
                    { 909, 9, "Phillips", "M5573" },
                    { 910, 9, "Rodríguez Peña", "M5573" },
                    { 1001, 10, "Chilecito", "M5567" },
                    { 1002, 10, "Eugenio Bustos", "M5567" },
                    { 1003, 10, "La Consulta", "M5567" },
                    { 1004, 10, "Pareditas", "M5567" },
                    { 1005, 10, "Villa San Carlos", "M5567" },
                    { 1101, 11, "Campo de los Andes", "M5560" },
                    { 1102, 11, "Colonia Las Rosas", "M5560" },
                    { 1103, 11, "El Algarrobo", "M5560" },
                    { 1104, 11, "El Totoral", "M5560" },
                    { 1105, 11, "La Primavera", "M5560" },
                    { 1106, 11, "Las Pintadas", "M5560" },
                    { 1107, 11, "Los Árboles", "M5560" },
                    { 1108, 11, "Los Chacayes", "M5560" },
                    { 1109, 11, "Los Sauces", "M5560" },
                    { 1110, 11, "Tunuyán", "M5560" },
                    { 1111, 11, "Villa Seca", "M5560" },
                    { 1112, 11, "Vista Flores", "M5560" },
                    { 1201, 12, "Anchoris", "M5561" },
                    { 1202, 12, "Cordón del Plata", "M5561" },
                    { 1203, 12, "El Peral", "M5561" },
                    { 1204, 12, "El Zampal", "M5561" },
                    { 1205, 12, "El Zampalito", "M5561" },
                    { 1206, 12, "Gualtallary", "M5561" },
                    { 1207, 12, "La Arboleda", "M5561" },
                    { 1208, 12, "La Carrera", "M5561" },
                    { 1209, 12, "San José", "M5561" },
                    { 1210, 12, "Santa Clara", "M5561" },
                    { 1211, 12, "Tupungato", "M5561" },
                    { 1212, 12, "Villa Bastías", "M5561" },
                    { 1213, 12, "Zapata", "M5561" },
                    { 1301, 13, "Cañada Seca", "M5600" },
                    { 1302, 13, "Cuadro Benegas", "M5600" },
                    { 1303, 13, "Cuadro Nacional", "M5600" },
                    { 1304, 13, "El Cerrito", "M5600" },
                    { 1305, 13, "El Nihuil", "M5603" },
                    { 1306, 13, "El Sosneado", "M5611" },
                    { 1307, 13, "Goudge", "M5600" },
                    { 1308, 13, "Jaime Prats", "M5622" },
                    { 1309, 13, "La Llave", "M5600" },
                    { 1310, 13, "Las Malvinas", "M5600" },
                    { 1311, 13, "Las Paredes", "M5600" },
                    { 1312, 13, "Monte Comán", "M5600" },
                    { 1313, 13, "Punta del Agua", "M5600" },
                    { 1314, 13, "Rama Caída", "M5600" },
                    { 1315, 13, "Real del Padre", "M5624" },
                    { 1316, 13, "San Rafael", "M5600" },
                    { 1317, 13, "Veinticinco de Mayo", "M5600" },
                    { 1318, 13, "Villa Atuel", "M5624" },
                    { 1401, 14, "Bowen", "M5634" },
                    { 1402, 14, "General Alvear", "M5620" },
                    { 1403, 14, "San Pedro del Atuel", "M5621" },
                    { 1404, 14, "Colonia Alvear Oeste", "M5620" },
                    { 1501, 15, "Agua Escondida", "M5611" },
                    { 1502, 15, "Malargüe", "M5613" },
                    { 1503, 15, "Río Barrancas", "M5611" },
                    { 1504, 15, "Río Grande", "M5611" },
                    { 1601, 16, "La Paz Norte", "M5590" },
                    { 1602, 16, "La Paz Sur", "M5590" },
                    { 1603, 16, "Desaguadero", "M5590" },
                    { 1604, 16, "Villa Antigua", "M5590" },
                    { 1605, 16, "Villa Nueva de La Paz", "M5590" },
                    { 1701, 17, "Doce de Octubre", "M5596" },
                    { 1702, 17, "La Dormida", "M5596" },
                    { 1703, 17, "Las Catitas", "M5596" },
                    { 1704, 17, "Ñacuñan", "M5596" },
                    { 1705, 17, "Ciudad de Santa Rosa", "M5596" },
                    { 1706, 17, "El Marcado", "M5596" },
                    { 1801, 18, "Alto del Olvido", "M5533" },
                    { 1802, 18, "Colonia Italia", "M5533" },
                    { 1803, 18, "Costa de Araujo", "M5533" },
                    { 1804, 18, "El Carmen", "M5533" },
                    { 1805, 18, "El Chilcal", "M5533" },
                    { 1806, 18, "El Plumero", "M5533" },
                    { 1807, 18, "El Vergel", "M5533" },
                    { 1808, 18, "Gustavo André", "M5533" },
                    { 1809, 18, "Jocolí", "M5533" },
                    { 1810, 18, "Jocolí Viejo", "M5533" },
                    { 1811, 18, "La Bajada", "M5533" },
                    { 1812, 18, "La Asunción", "M5533" },
                    { 1813, 18, "La Holanda", "M5533" },
                    { 1814, 18, "La Palmera", "M5533" },
                    { 1815, 18, "La Pega", "M5533" },
                    { 1816, 18, "Las Violetas", "M5533" },
                    { 1817, 18, "Lagunas del Rosario", "M5533" },
                    { 1818, 18, "El Paramillo", "M5533" },
                    { 1819, 18, "San Francisco", "M5533" },
                    { 1820, 18, "San José", "M5533" },
                    { 1821, 18, "San Miguel", "M5533" },
                    { 1822, 18, "Tres de Mayo", "M5533" },
                    { 1823, 18, "Villa Tulumaya", "M5533" },
                    { 1824, 18, "Oscar Mendoza", "M5533" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address1", "Address2", "CollectorId", "DNI", "Email", "LastName", "LocalityId", "Name", "Phone1", "Phone2", "Phone3", "SalesmanId" },
                values: new object[,]
                {
                    { new Guid("a6c7e29b-f6f1-4a35-868e-7c9e9e019051"), "Calle Sarmiento 456", "Depto 2B", 1, "34.567.89", "juan.gomez@gmail.com", "Gómez", 102, "Juan", "2615550456", "2615550457", "", 2 },
                    { new Guid("b9e37665-2e7c-4d31-8de9-1a59efc2d7d1"), "Ruta 40 Km 10", "", 2, "45.678.90", "carla.lopez@gmail.com", "López", 103, "Carla", "2626550123", "", "", 1 },
                    { new Guid("c8f2cd1e-5b4c-4396-8d22-b3406f77c42a"), "B° Las Rosas Mz A Casa 5", "", 2, "56.789.01", "pedro.martinez@gmail.com", "Martínez", 104, "Pedro", "2635550111", "2635550112", "", 2 },
                    { new Guid("d4b5f092-1790-4f9c-a640-0bef3d1cf7a1"), "Av. San Martín 123", "", 1, "23.456.78", "maria.perez@gmail.com", "Pérez", 101, "María", "2615550123", "", "", 1 },
                    { new Guid("e2d0084a-df5f-4188-8e7d-6f1f055e63dc"), "Calle Belgrano 789", "Piso 3", 1, "67.890.12", "laura.fernandez@gmail.com", "Fernández", 105, "Laura", "2615550789", "", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Cortadoras De Fiambres" },
                    { 2, 1, "Balanzas Electronicas" },
                    { 3, 1, "Gavetas Monedero" },
                    { 4, 1, "Envasadoras, Selladoras Y Termo Selladoras" },
                    { 5, 1, "Báscula" },
                    { 6, 1, "Impresor" },
                    { 7, 1, "Góndolas Y Estanterías" },
                    { 8, 1, "Mostradores De Panadería" },
                    { 9, 1, "Cafeteras" },
                    { 10, 1, "Carameleras" },
                    { 11, 2, "Exhibidores Verticales" },
                    { 12, 2, "Freezer" },
                    { 13, 2, "Heladeras Vitrinas" },
                    { 14, 2, "Heladeras Para Hielo" },
                    { 15, 2, "Heladera Bajo Mesada" },
                    { 16, 2, "Kiosqueras" },
                    { 17, 2, "Auto Servicio" },
                    { 18, 2, "Maquina De Helado Soft" },
                    { 19, 2, "Heladeras Exhibidoras" },
                    { 20, 2, "Heladeras Horizontales" },
                    { 21, 3, "Anafes" },
                    { 22, 3, "Bañomaria" },
                    { 23, 3, "Batidoras" },
                    { 24, 3, "Cocinas Industriales" },
                    { 25, 3, "Exprimidores" },
                    { 26, 3, "Fritadoras" },
                    { 27, 3, "Hornos Convector" },
                    { 28, 3, "Hornos Pizzero" },
                    { 29, 3, "Licuadoras" },
                    { 30, 3, "Laminadoras De Masa" },
                    { 31, 3, "Máquinas De Pastas" },
                    { 32, 3, "Mesadas Acero Inoxidable" },
                    { 33, 3, "Molinillos De Café" },
                    { 34, 3, "Plancha" },
                    { 35, 3, "Procesadoras De Alimentos" },
                    { 36, 3, "Sandwicheras" },
                    { 37, 3, "Termos" },
                    { 38, 3, "Vajilla Melamina" },
                    { 39, 4, "Cuchillos Profesionales" },
                    { 40, 4, "Cutters" },
                    { 41, 4, "Gancho Carbono" },
                    { 42, 4, "Mesadas De Corte" },
                    { 43, 4, "Picadoras De Carne" },
                    { 44, 4, "Sierra De Carne" },
                    { 45, 5, "Amasadora" },
                    { 46, 5, "Cámaras De Fermentación" },
                    { 47, 5, "Cortadora De Pan" },
                    { 48, 5, "Divisora Boleadora" },
                    { 49, 5, "Hornos Rotativos" },
                    { 50, 5, "Hornos De Piso" },
                    { 51, 5, "Balanzas" },
                    { 52, 5, "Heladeras Exhibidoras Pasteleras" },
                    { 53, 5, "Filmadoras" },
                    { 54, 6, "Anafe Eléctrico" },
                    { 55, 6, "Cocinas Eléctricas" },
                    { 56, 6, "Cocinas A Gas" },
                    { 57, 6, "Hornos Eléctricos" },
                    { 58, 6, "Hornallas" },
                    { 59, 6, "Lavarropas" },
                    { 60, 6, "Lavasecarropas" },
                    { 61, 6, "Lavavajillas" },
                    { 62, 6, "Secarropas" },
                    { 63, 6, "Ventiladores" },
                    { 64, 7, "Extractores" },
                    { 65, 7, "Microondas" },
                    { 66, 7, "Pavas Eléctricas" },
                    { 67, 7, "Procesadoras" },
                    { 68, 7, "Sandwicheras" },
                    { 69, 7, "Termotanques" },
                    { 70, 7, "Termos Eléctricos" },
                    { 71, 8, "Afeitadoras" },
                    { 72, 8, "Afeitadoras Corporales" },
                    { 73, 8, "Corta Pelo" },
                    { 74, 8, "Depiladoras" },
                    { 75, 8, "Planchitas De Pelo" },
                    { 76, 8, "Secadores De Pelo" },
                    { 77, 8, "Termómetros" },
                    { 78, 8, "Tensiómetros" },
                    { 79, 9, "Auriculares" },
                    { 80, 9, "Parlantes" },
                    { 81, 9, "Home Theatre" },
                    { 82, 9, "Minicomponentes" },
                    { 83, 9, "Parlantes Portátiles" },
                    { 84, 9, "Smart TV" },
                    { 85, 10, "Estufas" },
                    { 86, 10, "Estufas A Cuarzo" },
                    { 87, 10, "Estufas Eléctricas" },
                    { 88, 10, "Radiadores" },
                    { 89, 10, "Caloventores" },
                    { 90, 10, "Estufas Hogar" },
                    { 91, 11, "Aires Acondicionados Portátiles" },
                    { 92, 11, "Split" },
                    { 93, 11, "Turboventiladores" },
                    { 94, 11, "Ventiladores De Pared" },
                    { 95, 12, "Aspiradoras" },
                    { 96, 12, "Planchas" },
                    { 97, 13, "Cuchillería" },
                    { 98, 13, "Cubiertos" },
                    { 99, 17, "Utensilios De Cocina" },
                    { 100, 17, "Tablas De Picar" },
                    { 101, 17, "Cacerolas" },
                    { 102, 27, "Camas y Bases de Sommier" },
                    { 103, 27, "Juegos de Sommier" },
                    { 104, 27, "Cómodas, Botineros y Cajoneras" },
                    { 105, 27, "Mesas de Luz" },
                    { 106, 27, "Placares" },
                    { 107, 28, "Futones y Sillones" },
                    { 108, 28, "Mesas y Centros de TV" },
                    { 109, 28, "Mesas de Living" },
                    { 110, 28, "Soportes de TV" },
                    { 111, 20, "Bicicletas MTB" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Characteristics", "CostPrice", "Description", "Image", "Name", "ProviderUrl", "SalePrice", "SubcategoryId", "SubcategoryId1" },
                values: new object[,]
                {
                    { 1, "{\"Color\":\"Blanco\",\"Altura\":\"80 cm\"}", 1000m, "Silla liviana de polipropileno, apta uso interior/exterior.", null, "Silla Plástica Apilable", "https://proveedor.example/silla-plastica", 1999m, 1, null },
                    { 2, "{\"Color\":\"Gris\",\"Largo\":\"180 cm\"}", 5200m, "Mesa plegable rectangular con estructura metálica.", null, "Mesa Plegable 1,80 m", "https://proveedor.example/mesa-plegable", 8490m, 1, null },
                    { 3, "{\"Color\":\"Blanco\",\"Capacidad\":\"400 L\"}", 148000m, "Heladera exhibidora vertical, puerta de vidrio.", null, "Heladera Comercial 400 L", "https://proveedor.example/heladera-400l", 214900m, 2, null },
                    { 4, "{\"Color\":\"Negro\",\"Potencia\":\"800 W\"}", 42000m, "Microondas digital con grill y 8 programas automáticos.", null, "Microondas 23 L", "https://proveedor.example/microondas-23l", 62990m, 2, null },
                    { 5, "{\"Color\":\"Blanco Frío\",\"Lumens\":\"1900\"}", 1100m, "Lámpara LED industrial, 6500 K, 220 V.", null, "Lámpara LED 20 W", "https://proveedor.example/lampara-led", 1990m, 3, null }
                });

            migrationBuilder.InsertData(
                table: "ProductPlans",
                columns: new[] { "Id", "PlanId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 3, 3 },
                    { 5, 4, 4 },
                    { 6, 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LineId",
                table: "Categories",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CollectorId",
                table: "Customers",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DNI",
                table: "Customers",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LocalityId",
                table: "Customers",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SalesmanId",
                table: "Customers",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_DepartmentId",
                table: "Localities",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId_ProductId",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BusinessTypeId",
                table: "Orders",
                column: "BusinessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PeriodId",
                table: "Orders",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPlans_PlanId",
                table: "ProductPlans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPlans_ProductId_PlanId",
                table: "ProductPlans",
                columns: new[] { "ProductId", "PlanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId1",
                table: "Products",
                column: "SubcategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductPlans");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BusinessTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Collectors");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Salesmen");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Lines");
        }
    }
}
