using Microsoft.EntityFrameworkCore;
using PlanDiario.Domain;
using PlanDiario.Application.DTOs;

namespace PlanDiario.Infrastructure.Persistence
{
    public class PlanDiarioDbContext : DbContext
    {
        public PlanDiarioDbContext(DbContextOptions<PlanDiarioDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ProductPlan> ProductPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DNI).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.Locality).WithMany().HasForeignKey(e => e.LocalityId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Collector).WithMany(c => c.Customers).HasForeignKey(e => e.CollectorId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Salesman).WithMany(s => s.Customers).HasForeignKey(e => e.SalesmanId).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => e.DNI).IsUnique();
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SellingPrice).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Customer).WithMany(c => c.Orders).HasForeignKey(e => e.CustomerId);
                entity.HasOne(e => e.Period).WithMany(p => p.Orders).HasForeignKey(e => e.PeriodId);
                entity.HasOne(e => e.BusinessType).WithMany(bt => bt.Orders).HasForeignKey(e => e.BusinessTypeId);
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CostPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SalePrice).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Subcategory).WithMany(s => s.Products).HasForeignKey(e => e.idSubcategory);
            });

            // OrderProduct configuration
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Order).WithMany(o => o.OrderProducts).HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Product).WithMany(p => p.OrderProducts).HasForeignKey(e => e.ProductId);
            });

            // Other entity configurations
            modelBuilder.Entity<Locality>().HasOne(l => l.Department).WithMany(d => d.Localities).HasForeignKey(l => l.DepartmentId);
            modelBuilder.Entity<Category>().HasOne(c => c.Line).WithMany(l => l.Categories).HasForeignKey(c => c.LineId);
            modelBuilder.Entity<Subcategory>().HasOne(s => s.Category).WithMany(c => c.Subcategories).HasForeignKey(s => s.CategoryId);
            modelBuilder.Entity<Payment>().HasOne(p => p.Order).WithMany().HasForeignKey(p => p.OrderId);
            modelBuilder.Entity<ProductPlan>().HasOne(pp => pp.Product).WithMany(p => p.ProductPlans).HasForeignKey(pp => pp.ProductId);
            modelBuilder.Entity<ProductPlan>().HasOne(pp => pp.Plan).WithMany(p => p.ProductPlans).HasForeignKey(pp => pp.PlanId);


            modelBuilder.Entity<Department>().HasData(
             new Department { Id = 1, Name = "Capital" },
             new Department { Id = 2, Name = "Godoy Cruz" },
             new Department { Id = 3, Name = "Las Heras" },
             new Department { Id = 4, Name = "Guaymallén" },
             new Department { Id = 5, Name = "Luján de Cuyo" },
             new Department { Id = 6, Name = "Maipú" },
             new Department { Id = 7, Name = "San Martín" },
             new Department { Id = 8, Name = "Rivadavia" },
             new Department { Id = 9, Name = "Junín" },
             new Department { Id = 10, Name = "San Carlos" },
             new Department { Id = 11, Name = "Tunuyán" },
             new Department { Id = 12, Name = "Tupungato" },
             new Department { Id = 13, Name = "San Rafael" },
             new Department { Id = 14, Name = "General Alvear" },
             new Department { Id = 15, Name = "Malargüe" },
             new Department { Id = 16, Name = "La Paz" },
             new Department { Id = 17, Name = "Santa Rosa" },
             new Department { Id = 18, Name = "Lavalle" }
         );

            modelBuilder.Entity<Locality>().HasData(
                // ——— Capital (M5500) ———
                new Locality { Id = 101, Name = "1ª Sección Parque Central", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 102, Name = "2ª Sección Barrio Cívico", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 103, Name = "3ª Sección Parque O'Higgins", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 104, Name = "4ª Sección Área Fundacional", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 105, Name = "5ª Sección Residencial Sur", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 106, Name = "6ª Sección Residencial Norte", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 107, Name = "7ª Sección Residencial Parque", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 108, Name = "8ª Sección Aeroparque", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 109, Name = "9ª Sección Parque Gral. San Martín", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 110, Name = "10ª Sección Residencial Los Cerros", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 111, Name = "11ª Sección San Agustín", PostalCode = "M5500", DepartmentId = 1 },
                new Locality { Id = 112, Name = "12ª Sección Piedemonte", PostalCode = "M5500", DepartmentId = 1 },

                // ——— Godoy Cruz (M5501) ———
                new Locality { Id = 201, Name = "Gobernador Benegas", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 202, Name = "Godoy Cruz", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 203, Name = "Las Tortugas", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 204, Name = "Presidente Sarmiento", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 205, Name = "San Francisco del Monte", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 206, Name = "Trapiche", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 207, Name = "Villa Marini", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 208, Name = "Villa Hipódromo", PostalCode = "M5501", DepartmentId = 2 },
                new Locality { Id = 209, Name = "Villa del Parque", PostalCode = "M5501", DepartmentId = 2 },

                // ——— Las Heras (M5539 cabecera, + CPA propios) ———
                new Locality { Id = 301, Name = "Capdevilla", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 302, Name = "El Algarrobal", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 303, Name = "El Borbollón", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 304, Name = "El Challao", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 305, Name = "El Pastal", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 306, Name = "El Plumerillo", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 307, Name = "El Resguardo", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 308, Name = "El Zapallar", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 309, Name = "La Cieneguita", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 310, Name = "Las Cuevas", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 311, Name = "Las Heras", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 312, Name = "Panquehua", PostalCode = "M5539", DepartmentId = 3 },
                new Locality { Id = 313, Name = "Uspallata", PostalCode = "M5545", DepartmentId = 3 }, // CPA propio
                new Locality { Id = 314, Name = "Puente del Inca", PostalCode = "M5553", DepartmentId = 3 }, // CPA propio
                new Locality { Id = 315, Name = "Los Penitentes", PostalCode = "M5553", DepartmentId = 3 }, // CPA propio

                // ——— Guaymallén (M5521 cabecera, Buena Nueva M5523) ———
                new Locality { Id = 401, Name = "Belgrano", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 402, Name = "El Bermejo", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 403, Name = "Buena Nueva", PostalCode = "M5523", DepartmentId = 4 }, // CPA propio
                new Locality { Id = 404, Name = "Capilla del Rosario", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 405, Name = "Colonia Molina", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 406, Name = "Colonia Segovia", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 407, Name = "Dorrego", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 408, Name = "El Sauce", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 409, Name = "Jesús Nazareno", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 410, Name = "Kilómetro 8", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 411, Name = "Kilómetro 11", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 412, Name = "La Primavera", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 413, Name = "Las Cañas", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 414, Name = "Los Corralitos", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 415, Name = "Nueva Ciudad", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 416, Name = "Pedro Molina", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 417, Name = "Puente de Hierro", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 418, Name = "Rodeo de la Cruz", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 419, Name = "San Francisco del Monte", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 420, Name = "San José", PostalCode = "M5521", DepartmentId = 4 },
                new Locality { Id = 421, Name = "Villa Nueva", PostalCode = "M5521", DepartmentId = 4 },

                // ——— Luján de Cuyo ———
                new Locality { Id = 501, Name = "Agrelo", PostalCode = "M5509", DepartmentId = 5 },
                new Locality { Id = 502, Name = "Cacheuta", PostalCode = "M5569", DepartmentId = 5 },
                new Locality { Id = 503, Name = "Carrodilla", PostalCode = "M5505", DepartmentId = 5 },
                new Locality { Id = 504, Name = "Chacras de Coria", PostalCode = "M5505", DepartmentId = 5 },
                new Locality { Id = 505, Name = "El Carrizal", PostalCode = "M5535", DepartmentId = 5 },
                new Locality { Id = 506, Name = "Industrial", PostalCode = "M5507", DepartmentId = 5 },
                new Locality { Id = 507, Name = "La Puntilla", PostalCode = "M5509", DepartmentId = 5 },
                new Locality { Id = 508, Name = "Las Compuertas", PostalCode = "M5507", DepartmentId = 5 },
                new Locality { Id = 509, Name = "Luján de Cuyo", PostalCode = "M5507", DepartmentId = 5 },
                new Locality { Id = 510, Name = "Mayor Drummond", PostalCode = "M5507", DepartmentId = 5 },
                new Locality { Id = 511, Name = "Perdriel", PostalCode = "M5544", DepartmentId = 5 },
                new Locality { Id = 512, Name = "Potrerillos", PostalCode = "M5552", DepartmentId = 5 },
                new Locality { Id = 513, Name = "Ugarteche", PostalCode = "M5546", DepartmentId = 5 },
                new Locality { Id = 514, Name = "Vistalba", PostalCode = "M5509", DepartmentId = 5 },
                new Locality { Id = 515, Name = "Vertientes del Pedemonte", PostalCode = "M5507", DepartmentId = 5 },

                // ——— Maipú ———
                new Locality { Id = 601, Name = "Colonia Bombal", PostalCode = "M5515", DepartmentId = 6 },
                new Locality { Id = 602, Name = "Coquimbito", PostalCode = "M5511", DepartmentId = 6 },
                new Locality { Id = 603, Name = "Cruz de Piedra", PostalCode = "M5517", DepartmentId = 6 },
                new Locality { Id = 604, Name = "Fray Luis Beltrán", PostalCode = "M5613", DepartmentId = 6 },
                new Locality { Id = 605, Name = "General Gutiérrez", PostalCode = "M5519", DepartmentId = 6 },
                new Locality { Id = 606, Name = "General Ortega", PostalCode = "M5515", DepartmentId = 6 },
                new Locality { Id = 607, Name = "Las Barrancas", PostalCode = "M5513", DepartmentId = 6 },
                new Locality { Id = 608, Name = "Lunlunta", PostalCode = "M5519", DepartmentId = 6 },
                new Locality { Id = 609, Name = "Luzuriaga", PostalCode = "M5519", DepartmentId = 6 },
                new Locality { Id = 610, Name = "Maipú", PostalCode = "M5515", DepartmentId = 6 },
                new Locality { Id = 611, Name = "Rodeo del Medio", PostalCode = "M5513", DepartmentId = 6 },
                new Locality { Id = 612, Name = "Russell", PostalCode = "M5519", DepartmentId = 6 },
                new Locality { Id = 613, Name = "San Roque", PostalCode = "M5519", DepartmentId = 6 },
                new Locality { Id = 614, Name = "Santa Blanca", PostalCode = "M5515", DepartmentId = 6 },

                // ——— San Martín (M5570) ———
                new Locality { Id = 701, Name = "Alto Salvador", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 702, Name = "Alto Verde", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 703, Name = "Buen Orden", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 704, Name = "Chapanay", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 705, Name = "Chivilcoy", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 706, Name = "El Central", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 707, Name = "El Divisadero", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 708, Name = "El Espino", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 709, Name = "El Ramblón", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 710, Name = "Ingeniero Giagnoni", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 711, Name = "Las Chimbas", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 712, Name = "Montecaseros", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 713, Name = "Nueva California", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 714, Name = "Palmira", PostalCode = "M5571", DepartmentId = 7 },
                new Locality { Id = 715, Name = "San Martín", PostalCode = "M5570", DepartmentId = 7 },
                new Locality { Id = 716, Name = "Tres Porteñas", PostalCode = "M5570", DepartmentId = 7 },

                // ——— Rivadavia (M5577) ———
                new Locality { Id = 801, Name = "Andrade", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 802, Name = "El Mirador", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 803, Name = "La Central", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 804, Name = "La Libertad", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 805, Name = "Los Árboles", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 806, Name = "Los Campamentos", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 807, Name = "Los Huarpes", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 808, Name = "Medrano", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 809, Name = "Mundo Nuevo", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 810, Name = "Reducción", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 811, Name = "Rivadavia", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 812, Name = "Santa María de Oro", PostalCode = "M5577", DepartmentId = 8 },
                new Locality { Id = 813, Name = "San Isidro", PostalCode = "M5577", DepartmentId = 8 },

                // ——— Junín (M5573) ———
                new Locality { Id = 901, Name = "Algarrobo Grande", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 902, Name = "Alto Verde", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 903, Name = "Ingeniero Giagnoni", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 904, Name = "Junín", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 905, Name = "La Colonia", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 906, Name = "Los Barriales", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 907, Name = "Medrano", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 908, Name = "Mundo Nuevo", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 909, Name = "Phillips", PostalCode = "M5573", DepartmentId = 9 },
                new Locality { Id = 910, Name = "Rodríguez Peña", PostalCode = "M5573", DepartmentId = 9 },

                // ——— San Carlos (M5567) ———
                new Locality { Id = 1001, Name = "Chilecito", PostalCode = "M5567", DepartmentId = 10 },
                new Locality { Id = 1002, Name = "Eugenio Bustos", PostalCode = "M5567", DepartmentId = 10 },
                new Locality { Id = 1003, Name = "La Consulta", PostalCode = "M5567", DepartmentId = 10 },
                new Locality { Id = 1004, Name = "Pareditas", PostalCode = "M5567", DepartmentId = 10 },
                new Locality { Id = 1005, Name = "Villa San Carlos", PostalCode = "M5567", DepartmentId = 10 },

                // ——— Tunuyán (M5560) ———
                new Locality { Id = 1101, Name = "Campo de los Andes", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1102, Name = "Colonia Las Rosas", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1103, Name = "El Algarrobo", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1104, Name = "El Totoral", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1105, Name = "La Primavera", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1106, Name = "Las Pintadas", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1107, Name = "Los Árboles", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1108, Name = "Los Chacayes", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1109, Name = "Los Sauces", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1110, Name = "Tunuyán", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1111, Name = "Villa Seca", PostalCode = "M5560", DepartmentId = 11 },
                new Locality { Id = 1112, Name = "Vista Flores", PostalCode = "M5560", DepartmentId = 11 },

                // ——— Tupungato (M5561) ———
                new Locality { Id = 1201, Name = "Anchoris", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1202, Name = "Cordón del Plata", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1203, Name = "El Peral", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1204, Name = "El Zampal", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1205, Name = "El Zampalito", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1206, Name = "Gualtallary", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1207, Name = "La Arboleda", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1208, Name = "La Carrera", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1209, Name = "San José", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1210, Name = "Santa Clara", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1211, Name = "Tupungato", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1212, Name = "Villa Bastías", PostalCode = "M5561", DepartmentId = 12 },
                new Locality { Id = 1213, Name = "Zapata", PostalCode = "M5561", DepartmentId = 12 },

                // ——— San Rafael (M5600 cabecera, + CPA propios) ———
                new Locality { Id = 1301, Name = "Cañada Seca", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1302, Name = "Cuadro Benegas", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1303, Name = "Cuadro Nacional", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1304, Name = "El Cerrito", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1305, Name = "El Nihuil", PostalCode = "M5603", DepartmentId = 13 },
                new Locality { Id = 1306, Name = "El Sosneado", PostalCode = "M5611", DepartmentId = 13 },
                new Locality { Id = 1307, Name = "Goudge", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1308, Name = "Jaime Prats", PostalCode = "M5622", DepartmentId = 13 },
                new Locality { Id = 1309, Name = "La Llave", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1310, Name = "Las Malvinas", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1311, Name = "Las Paredes", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1312, Name = "Monte Comán", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1313, Name = "Punta del Agua", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1314, Name = "Rama Caída", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1315, Name = "Real del Padre", PostalCode = "M5624", DepartmentId = 13 },
                new Locality { Id = 1316, Name = "San Rafael", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1317, Name = "Veinticinco de Mayo", PostalCode = "M5600", DepartmentId = 13 },
                new Locality { Id = 1318, Name = "Villa Atuel", PostalCode = "M5624", DepartmentId = 13 },

                // ——— General Alvear ———
                new Locality { Id = 1401, Name = "Bowen", PostalCode = "M5634", DepartmentId = 14 },
                new Locality { Id = 1402, Name = "General Alvear", PostalCode = "M5620", DepartmentId = 14 },
                new Locality { Id = 1403, Name = "San Pedro del Atuel", PostalCode = "M5621", DepartmentId = 14 },
                new Locality { Id = 1404, Name = "Colonia Alvear Oeste", PostalCode = "M5620", DepartmentId = 14 },

                // ——— Malargüe ———
                new Locality { Id = 1501, Name = "Agua Escondida", PostalCode = "M5611", DepartmentId = 15 },
                new Locality { Id = 1502, Name = "Malargüe", PostalCode = "M5613", DepartmentId = 15 },
                new Locality { Id = 1503, Name = "Río Barrancas", PostalCode = "M5611", DepartmentId = 15 },
                new Locality { Id = 1504, Name = "Río Grande", PostalCode = "M5611", DepartmentId = 15 },

                // ——— La Paz (M5590) ———
                new Locality { Id = 1601, Name = "La Paz Norte", PostalCode = "M5590", DepartmentId = 16 },
                new Locality { Id = 1602, Name = "La Paz Sur", PostalCode = "M5590", DepartmentId = 16 },
                new Locality { Id = 1603, Name = "Desaguadero", PostalCode = "M5590", DepartmentId = 16 },
                new Locality { Id = 1604, Name = "Villa Antigua", PostalCode = "M5590", DepartmentId = 16 },
                new Locality { Id = 1605, Name = "Villa Nueva de La Paz", PostalCode = "M5590", DepartmentId = 16 },

                // ——— Santa Rosa (M5596) ———
                new Locality { Id = 1701, Name = "Doce de Octubre", PostalCode = "M5596", DepartmentId = 17 },
                new Locality { Id = 1702, Name = "La Dormida", PostalCode = "M5596", DepartmentId = 17 },
                new Locality { Id = 1703, Name = "Las Catitas", PostalCode = "M5596", DepartmentId = 17 },
                new Locality { Id = 1704, Name = "Ñacuñan", PostalCode = "M5596", DepartmentId = 17 },
                new Locality { Id = 1705, Name = "Ciudad de Santa Rosa", PostalCode = "M5596", DepartmentId = 17 },
                new Locality { Id = 1706, Name = "El Marcado", PostalCode = "M5596", DepartmentId = 17 },

                // ——— Lavalle (M5533) ———
                new Locality { Id = 1801, Name = "Alto del Olvido", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1802, Name = "Colonia Italia", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1803, Name = "Costa de Araujo", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1804, Name = "El Carmen", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1805, Name = "El Chilcal", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1806, Name = "El Plumero", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1807, Name = "El Vergel", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1808, Name = "Gustavo André", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1809, Name = "Jocolí", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1810, Name = "Jocolí Viejo", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1811, Name = "La Bajada", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1812, Name = "La Asunción", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1813, Name = "La Holanda", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1814, Name = "La Palmera", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1815, Name = "La Pega", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1816, Name = "Las Violetas", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1817, Name = "Lagunas del Rosario", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1818, Name = "El Paramillo", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1819, Name = "San Francisco", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1820, Name = "San José", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1821, Name = "San Miguel", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1822, Name = "Tres de Mayo", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1823, Name = "Villa Tulumaya", PostalCode = "M5533", DepartmentId = 18 },
                new Locality { Id = 1824, Name = "Oscar Mendoza", PostalCode = "M5533", DepartmentId = 18 }
            );

            modelBuilder.Entity<Period>().HasData(
                new Period { Id = 1, Name = "Diario" },
                new Period { Id = 2, Name = "Semanal" },
                new Period { Id = 3, Name = "Quincenal" },
                new Period { Id = 4, Name = "Mensual" }
            );

            modelBuilder.Entity<BusinessType>().HasData(
                new BusinessType { Id = 1, Name = "Restaurante" },
                new BusinessType { Id = 2, Name = "Bar" },
                new BusinessType { Id = 3, Name = "Cafetería" },
                new BusinessType { Id = 4, Name = "Pizzería" },
                new BusinessType { Id = 5, Name = "Heladería" },
                new BusinessType { Id = 6, Name = "Kiosco" },
                new BusinessType { Id = 7, Name = "Almacén" },
                new BusinessType { Id = 8, Name = "Farmacia" },
                new BusinessType { Id = 9, Name = "Taller Mecánico" },
                new BusinessType { Id = 10, Name = "Ferretería" },
                new BusinessType { Id = 11, Name = "Librería" },
                new BusinessType { Id = 12, Name = "Panadería" }
            );

            modelBuilder.Entity<Line>().HasData(
                 new Line { Id = 1, Name = "Equipamiento Comercial" },
                 new Line { Id = 2, Name = "Hogar" },
                 new Line { Id = 3, Name = "Bazar" },
                 new Line { Id = 4, Name = "Rodados" },
                 new Line { Id = 5, Name = "Ferreteria" },
                 new Line { Id = 6, Name = "Muebles de interior" }
             );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "COMERCIOS", LineId = 1 },
                new Category { Id = 2, Name = "EQUIPOS DE FRIO", LineId = 1 },
                new Category { Id = 3, Name = "GASTRONOMIA", LineId = 1 },
                new Category { Id = 4, Name = "CARNICERIA", LineId = 1 },
                new Category { Id = 5, Name = "PANADERIA", LineId = 1 },
                new Category { Id = 6, Name = "ELECTRO HOGAR", LineId = 2 },
                new Category { Id = 7, Name = "ELECTRO COCINA", LineId = 2 },
                new Category { Id = 8, Name = "CUIDADO PERSONAL", LineId = 2 },
                new Category { Id = 9, Name = "AUDIO & TV", LineId = 2 },
                new Category { Id = 10, Name = "CALEFACCION", LineId = 2 },
                new Category { Id = 11, Name = "CLIMATIZACION", LineId = 2 },
                new Category { Id = 12, Name = "PEQUEÑOS ELECTRO", LineId = 2 },
                new Category { Id = 13, Name = "BAZAR EN ACERO INOXIDABLE", LineId = 3 },
                new Category { Id = 14, Name = "CRISTALERIA", LineId = 3 },
                new Category { Id = 15, Name = "PLASTICOS", LineId = 3 },
                new Category { Id = 16, Name = "ALUMINIO", LineId = 3 },
                new Category { Id = 17, Name = "COCINA", LineId = 3 },
                new Category { Id = 18, Name = "MENAJE", LineId = 3 },
                new Category { Id = 19, Name = "CICLISMO", LineId = 4 },
                new Category { Id = 20, Name = "MOTOVEHICULOS", LineId = 4 },
                new Category { Id = 21, Name = "BATERIAS", LineId = 4 },
                new Category { Id = 22, Name = "HERRAMIENTAS", LineId = 5 },
                new Category { Id = 23, Name = "ELECTRICIDAD", LineId = 5 },
                new Category { Id = 24, Name = "JARDIN", LineId = 5 },
                new Category { Id = 25, Name = "PLOMERIA", LineId = 5 },
                new Category { Id = 26, Name = "MUEBLES DE COCINA", LineId = 6 },
                new Category { Id = 27, Name = "MUEBLES DE HABITACION", LineId = 6 },
                new Category { Id = 28, Name = "MUEBLES DE LIVING / COMEDOR", LineId = 6 }
            );

            // -------  SUBRUBRO  -------------------------------------------------------
            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory { Id = 1, Name = "Cortadoras De Fiambres", CategoryId = 1 },
                new Subcategory { Id = 2, Name = "Balanzas Electronicas", CategoryId = 1 },
                new Subcategory { Id = 3, Name = "Gavetas Monedero", CategoryId = 1 },
                new Subcategory { Id = 4, Name = "Envasadoras, Selladoras Y Termo Selladoras", CategoryId = 1 },
                new Subcategory { Id = 5, Name = "Báscula", CategoryId = 1 },
                new Subcategory { Id = 6, Name = "Impresor", CategoryId = 1 },
                new Subcategory { Id = 7, Name = "Góndolas Y Estanterías", CategoryId = 1 },
                new Subcategory { Id = 8, Name = "Mostradores De Panadería", CategoryId = 1 },
                new Subcategory { Id = 9, Name = "Cafeteras", CategoryId = 1 },
                new Subcategory { Id = 10, Name = "Carameleras", CategoryId = 1 },
                new Subcategory { Id = 11, Name = "Exhibidores Verticales", CategoryId = 2 },
                new Subcategory { Id = 12, Name = "Freezer", CategoryId = 2 },
                new Subcategory { Id = 13, Name = "Heladeras Vitrinas", CategoryId = 2 },
                new Subcategory { Id = 14, Name = "Heladeras Para Hielo", CategoryId = 2 },
                new Subcategory { Id = 15, Name = "Heladera Bajo Mesada", CategoryId = 2 },
                new Subcategory { Id = 16, Name = "Kiosqueras", CategoryId = 2 },
                new Subcategory { Id = 17, Name = "Auto Servicio", CategoryId = 2 },
                new Subcategory { Id = 18, Name = "Maquina De Helado Soft", CategoryId = 2 },
                new Subcategory { Id = 19, Name = "Heladeras Exhibidoras", CategoryId = 2 },
                new Subcategory { Id = 20, Name = "Heladeras Horizontales", CategoryId = 2 },
                new Subcategory { Id = 21, Name = "Anafes", CategoryId = 3 },
                new Subcategory { Id = 22, Name = "Bañomaria", CategoryId = 3 },
                new Subcategory { Id = 23, Name = "Batidoras", CategoryId = 3 },
                new Subcategory { Id = 24, Name = "Cocinas Industriales", CategoryId = 3 },
                new Subcategory { Id = 25, Name = "Exprimidores", CategoryId = 3 },
                new Subcategory { Id = 26, Name = "Fritadoras", CategoryId = 3 },
                new Subcategory { Id = 27, Name = "Hornos Convector", CategoryId = 3 },
                new Subcategory { Id = 28, Name = "Hornos Pizzero", CategoryId = 3 },
                new Subcategory { Id = 29, Name = "Licuadoras", CategoryId = 3 },
                new Subcategory { Id = 30, Name = "Laminadoras De Masa", CategoryId = 3 },
                new Subcategory { Id = 31, Name = "Máquinas De Pastas", CategoryId = 3 },
                new Subcategory { Id = 32, Name = "Mesadas Acero Inoxidable", CategoryId = 3 },
                new Subcategory { Id = 33, Name = "Molinillos De Café", CategoryId = 3 },
                new Subcategory { Id = 34, Name = "Plancha", CategoryId = 3 },
                new Subcategory { Id = 35, Name = "Procesadoras De Alimentos", CategoryId = 3 },
                new Subcategory { Id = 36, Name = "Sandwicheras", CategoryId = 3 },
                new Subcategory { Id = 37, Name = "Termos", CategoryId = 3 },
                new Subcategory { Id = 38, Name = "Vajilla Melamina", CategoryId = 3 },
                new Subcategory { Id = 39, Name = "Cuchillos Profesionales", CategoryId = 4 },
                new Subcategory { Id = 40, Name = "Cutters", CategoryId = 4 },
                new Subcategory { Id = 41, Name = "Gancho Carbono", CategoryId = 4 },
                new Subcategory { Id = 42, Name = "Mesadas De Corte", CategoryId = 4 },
                new Subcategory { Id = 43, Name = "Picadoras De Carne", CategoryId = 4 },
                new Subcategory { Id = 44, Name = "Sierra De Carne", CategoryId = 4 },
                new Subcategory { Id = 45, Name = "Amasadora", CategoryId = 5 },
                new Subcategory { Id = 46, Name = "Cámaras De Fermentación", CategoryId = 5 },
                new Subcategory { Id = 47, Name = "Cortadora De Pan", CategoryId = 5 },
                new Subcategory { Id = 48, Name = "Divisora Boleadora", CategoryId = 5 },
                new Subcategory { Id = 49, Name = "Hornos Rotativos", CategoryId = 5 },
                new Subcategory { Id = 50, Name = "Hornos De Piso", CategoryId = 5 },
                new Subcategory { Id = 51, Name = "Balanzas", CategoryId = 5 },
                new Subcategory { Id = 52, Name = "Heladeras Exhibidoras Pasteleras", CategoryId = 5 },
                new Subcategory { Id = 53, Name = "Filmadoras", CategoryId = 5 },
                new Subcategory { Id = 54, Name = "Anafe Eléctrico", CategoryId = 6 },
                new Subcategory { Id = 55, Name = "Cocinas Eléctricas", CategoryId = 6 },
                new Subcategory { Id = 56, Name = "Cocinas A Gas", CategoryId = 6 },
                new Subcategory { Id = 57, Name = "Hornos Eléctricos", CategoryId = 6 },
                new Subcategory { Id = 58, Name = "Hornallas", CategoryId = 6 },
                new Subcategory { Id = 59, Name = "Lavarropas", CategoryId = 6 },
                new Subcategory { Id = 60, Name = "Lavasecarropas", CategoryId = 6 },
                new Subcategory { Id = 61, Name = "Lavavajillas", CategoryId = 6 },
                new Subcategory { Id = 62, Name = "Secarropas", CategoryId = 6 },
                new Subcategory { Id = 63, Name = "Ventiladores", CategoryId = 6 },
                new Subcategory { Id = 64, Name = "Extractores", CategoryId = 7 },
                new Subcategory { Id = 65, Name = "Microondas", CategoryId = 7 },
                new Subcategory { Id = 66, Name = "Pavas Eléctricas", CategoryId = 7 },
                new Subcategory { Id = 67, Name = "Procesadoras", CategoryId = 7 },
                new Subcategory { Id = 68, Name = "Sandwicheras", CategoryId = 7 },
                new Subcategory { Id = 69, Name = "Termotanques", CategoryId = 7 },
                new Subcategory { Id = 70, Name = "Termos Eléctricos", CategoryId = 7 },
                new Subcategory { Id = 71, Name = "Afeitadoras", CategoryId = 8 },
                new Subcategory { Id = 72, Name = "Afeitadoras Corporales", CategoryId = 8 },
                new Subcategory { Id = 73, Name = "Corta Pelo", CategoryId = 8 },
                new Subcategory { Id = 74, Name = "Depiladoras", CategoryId = 8 },
                new Subcategory { Id = 75, Name = "Planchitas De Pelo", CategoryId = 8 },
                new Subcategory { Id = 76, Name = "Secadores De Pelo", CategoryId = 8 },
                new Subcategory { Id = 77, Name = "Termómetros", CategoryId = 8 },
                new Subcategory { Id = 78, Name = "Tensiómetros", CategoryId = 8 },
                new Subcategory { Id = 79, Name = "Auriculares", CategoryId = 9 },
                new Subcategory { Id = 80, Name = "Parlantes", CategoryId = 9 },
                new Subcategory { Id = 81, Name = "Home Theatre", CategoryId = 9 },
                new Subcategory { Id = 82, Name = "Minicomponentes", CategoryId = 9 },
                new Subcategory { Id = 83, Name = "Parlantes Portátiles", CategoryId = 9 },
                new Subcategory { Id = 84, Name = "Smart TV", CategoryId = 9 },
                new Subcategory { Id = 85, Name = "Estufas", CategoryId = 10 },
                new Subcategory { Id = 86, Name = "Estufas A Cuarzo", CategoryId = 10 },
                new Subcategory { Id = 87, Name = "Estufas Eléctricas", CategoryId = 10 },
                new Subcategory { Id = 88, Name = "Radiadores", CategoryId = 10 },
                new Subcategory { Id = 89, Name = "Caloventores", CategoryId = 10 },
                new Subcategory { Id = 90, Name = "Estufas Hogar", CategoryId = 10 },
                new Subcategory { Id = 91, Name = "Aires Acondicionados Portátiles", CategoryId = 11 },
                new Subcategory { Id = 92, Name = "Split", CategoryId = 11 },
                new Subcategory { Id = 93, Name = "Turboventiladores", CategoryId = 11 },
                new Subcategory { Id = 94, Name = "Ventiladores De Pared", CategoryId = 11 },
                new Subcategory { Id = 95, Name = "Aspiradoras", CategoryId = 12 },
                new Subcategory { Id = 96, Name = "Planchas", CategoryId = 12 },
                new Subcategory { Id = 97, Name = "Cuchillería", CategoryId = 13 },
                new Subcategory { Id = 98, Name = "Cubiertos", CategoryId = 13 },
                new Subcategory { Id = 99, Name = "Utensilios De Cocina", CategoryId = 17 },
                new Subcategory { Id = 100, Name = "Tablas De Picar", CategoryId = 17 },
                new Subcategory { Id = 101, Name = "Cacerolas", CategoryId = 17 },
                new Subcategory { Id = 102, Name = "Camas y Bases de Sommier", CategoryId = 27 },
                new Subcategory { Id = 103, Name = "Juegos de Sommier", CategoryId = 27 },
                new Subcategory { Id = 104, Name = "Cómodas, Botineros y Cajoneras", CategoryId = 27 },
                new Subcategory { Id = 105, Name = "Mesas de Luz", CategoryId = 27 },
                new Subcategory { Id = 106, Name = "Placares", CategoryId = 27 },
                new Subcategory { Id = 107, Name = "Futones y Sillones", CategoryId = 28 },
                new Subcategory { Id = 108, Name = "Mesas y Centros de TV", CategoryId = 28 },
                new Subcategory { Id = 109, Name = "Mesas de Living", CategoryId = 28 },
                new Subcategory { Id = 110, Name = "Soportes de TV", CategoryId = 28 },
                new Subcategory { Id = 111, Name = "Bicicletas MTB", CategoryId = 20 }
        );

            modelBuilder.Entity<Salesman>().HasData(
                 new Salesman { Id = 1, Name = "Cabrera, Sebastian", CommissionPercentage = 0 },
                 new Salesman { Id = 2, Name = "Vitteli, Victor", CommissionPercentage = 5 },
                 new Salesman { Id = 3, Name = "Carletto, Pablo", CommissionPercentage = 5 },
                 new Salesman { Id = 4, Name = "Alfonso, Miguel", CommissionPercentage = 7 },
                 new Salesman { Id = 5, Name = "Rivero, Karen", CommissionPercentage = 5 },
                 new Salesman { Id = 6, Name = "Vitelli, Joaquin", CommissionPercentage = 5 }
             );

            modelBuilder.Entity<Collector>().HasData(
                 new Collector { Id = 1, Name = "Cabrera, Sebastian", CommisionPercentage = 0 },
                 new Collector { Id = 2, Name = "Vitteli, Victor", CommisionPercentage = 0 },
                 new Collector { Id = 3, Name = "Perez, Martin", CommisionPercentage = 0 },
                 new Collector { Id = 4, Name = "Alfonso, Miguel", CommisionPercentage = 0 }
             );

            modelBuilder.Entity<Plan>().HasData(
                 new Plan { Id = 1, Name = "Plan Básico 2025", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), InterestRate = 0.050m },  // 5 %
                 new Plan { Id = 2, Name = "Plan Plus 24 meses", StartDate = new DateTime(2025, 4, 1), EndDate = new DateTime(2027, 3, 31), InterestRate = 0.065m }, // 6.5 %
                 new Plan { Id = 3, Name = "Plan Premium Anual", StartDate = new DateTime(2025, 7, 1), EndDate = new DateTime(2026, 6, 30), InterestRate = 0.080m },// 8 %
                 new Plan { Id = 4, Name = "Plan Estacional Verano 2025-26", StartDate = new DateTime(2025, 11, 1), EndDate = new DateTime(2026, 2, 28), InterestRate = 0.055m }, // 5.5 %
                 new Plan { Id = 5, Name = "Plan Especial 12 Pagos", StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31), InterestRate = 0.070m } // 7 %
            );

            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Name = "Silla Plástica Apilable",
                   Description = "Silla liviana de polipropileno, apta uso interior/exterior.",
                   Characteristics = "{\"Color\":\"Blanco\",\"Altura\":\"80 cm\"}",
                   CostPrice = 1000m,
                   SalePrice = 1999m,
                   Image = null,
                   idSubcategory = 1,
                   ProviderUrl = "https://proveedor.example/silla-plastica"
               },
               new Product
               {
                   Id = 2,
                   Name = "Mesa Plegable 1,80 m",
                   Description = "Mesa plegable rectangular con estructura metálica.",
                   Characteristics = "{\"Color\":\"Gris\",\"Largo\":\"180 cm\"}",
                   CostPrice = 5200m,
                   SalePrice = 8490m,
                   Image = null,
                   idSubcategory = 1,
                   ProviderUrl = "https://proveedor.example/mesa-plegable"
               },
               new Product
               {
                   Id = 3,
                   Name = "Heladera Comercial 400 L",
                   Description = "Heladera exhibidora vertical, puerta de vidrio.",
                   Characteristics = "{\"Color\":\"Blanco\",\"Capacidad\":\"400 L\"}",
                   CostPrice = 148000m,
                   SalePrice = 214900m,
                   Image = null,
                   idSubcategory = 2,
                   ProviderUrl = "https://proveedor.example/heladera-400l"
               },
               new Product
               {
                   Id = 4,
                   Name = "Microondas 23 L",
                   Description = "Microondas digital con grill y 8 programas automáticos.",
                   Characteristics = "{\"Color\":\"Negro\",\"Potencia\":\"800 W\"}",
                   CostPrice = 42000m,
                   SalePrice = 62990m,
                   Image = null,
                   idSubcategory = 2,
                   ProviderUrl = "https://proveedor.example/microondas-23l"
               },
               new Product
               {
                   Id = 5,
                   Name = "Lámpara LED 20 W",
                   Description = "Lámpara LED industrial, 6500 K, 220 V.",
                   Characteristics = "{\"Color\":\"Blanco Frío\",\"Lumens\":\"1900\"}",
                   CostPrice = 1100m,
                   SalePrice = 1990m,
                   Image = null,
                   idSubcategory = 3,
                   ProviderUrl = "https://proveedor.example/lampara-led"
               }
            );

            modelBuilder.Entity<ProductPlan>().HasData(
                new ProductPlan { Id = 1, ProductId = 1, PlanId = 1 },
                new ProductPlan { Id = 2, ProductId = 1, PlanId = 2 },
                new ProductPlan { Id = 3, ProductId = 2, PlanId = 1 },
                new ProductPlan { Id = 4, ProductId = 3, PlanId = 3 },
                new ProductPlan { Id = 5, ProductId = 4, PlanId = 4 },
                new ProductPlan { Id = 6, ProductId = 5, PlanId = 5 }
            );

            modelBuilder.Entity<Customer>().HasData(
                  new Customer
                  {
                      Id = Guid.Parse("d4b5f092-1790-4f9c-a640-0bef3d1cf7a1"),
                      DNI = "23.456.78",
                      Name = "María",
                      LastName = "Pérez",
                      Address1 = "Av. San Martín 123",
                      Address2 = string.Empty,
                      Phone1 = "2615550123",
                      Phone2 = string.Empty,
                      Phone3 = string.Empty,
                      Email = "maria.perez@gmail.com",
                      LocalityId = 1,   // ← adjust to an existing Locality
                      CollectorId = 1,   // ← adjust to an existing Collector
                      SalesmanId = 1    // ← adjust to an existing Salesman
                  },
                  new Customer
                  {
                      Id = Guid.Parse("a6c7e29b-f6f1-4a35-868e-7c9e9e019051"),
                      DNI = "34.567.89",
                      Name = "Juan",
                      LastName = "Gómez",
                      Address1 = "Calle Sarmiento 456",
                      Address2 = "Depto 2B",
                      Phone1 = "2615550456",
                      Phone2 = "2615550457",
                      Phone3 = string.Empty,
                      Email = "juan.gomez@gmail.com",
                      LocalityId = 2,
                      CollectorId = 1,
                      SalesmanId = 2
                  },
                  new Customer
                  {
                      Id = Guid.Parse("b9e37665-2e7c-4d31-8de9-1a59efc2d7d1"),
                      DNI = "45.678.90",
                      Name = "Carla",
                      LastName = "López",
                      Address1 = "Ruta 40 Km 10",
                      Address2 = string.Empty,
                      Phone1 = "2626550123",
                      Phone2 = string.Empty,
                      Phone3 = string.Empty,
                      Email = "carla.lopez@gmail.com",
                      LocalityId = 3,
                      CollectorId = 2,
                      SalesmanId = 1
                  },
                  new Customer
                  {
                      Id = Guid.Parse("c8f2cd1e-5b4c-4396-8d22-b3406f77c42a"),
                      DNI = "56.789.01",
                      Name = "Pedro",
                      LastName = "Martínez",
                      Address1 = "B° Las Rosas Mz A Casa 5",
                      Address2 = string.Empty,
                      Phone1 = "2635550111",
                      Phone2 = "2635550112",
                      Phone3 = string.Empty,
                      Email = "pedro.martinez@gmail.com",
                      LocalityId = 4,
                      CollectorId = 2,
                      SalesmanId = 2
                  },
                  new Customer
                  {
                      Id = Guid.Parse("e2d0084a-df5f-4188-8e7d-6f1f055e63dc"),
                      DNI = "67.890.12",
                      Name = "Laura",
                      LastName = "Fernández",
                      Address1 = "Calle Belgrano 789",
                      Address2 = "Piso 3",
                      Phone1 = "2615550789",
                      Phone2 = string.Empty,
                      Phone3 = string.Empty,
                      Email = "laura.fernandez@gmail.com",
                      LocalityId = 5,
                      CollectorId = 1,
                      SalesmanId = 1
                  }
            );

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SellingPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.WeekDay).HasConversion<string>();
                entity.Property(e => e.OrderType).HasConversion<string>();

                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Period)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(e => e.PeriodId);

                entity.HasOne(e => e.BusinessType)
                      .WithMany(bt => bt.Orders)
                      .HasForeignKey(e => e.BusinessTypeId);
            });

            // Plan configuration
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.InterestRate).HasColumnType("decimal(5,2)");
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Characteristics).HasColumnType("nvarchar(max)");
                entity.Property(e => e.CostPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SalePrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ProviderUrl).HasMaxLength(500);

                entity.HasOne(e => e.Subcategory)
                      .WithMany()
                      .HasForeignKey(e => e.idSubcategory)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ProductPlan configuration (Many-to-many)
            modelBuilder.Entity<ProductPlan>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Product)
                      .WithMany(e => e.ProductPlans)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Plan)
                      .WithMany(e => e.ProductPlans)
                      .HasForeignKey(e => e.PlanId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.ProductId, e.PlanId }).IsUnique();
            });

            // Lookup entities configuration
            modelBuilder.Entity<Locality>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Collector>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Salesman>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
