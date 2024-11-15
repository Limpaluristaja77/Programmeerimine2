using KooliProjekt.Data;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void GenerateClients(ApplicationDbContext context)
        {
            // Kontrollime, kas andmebaasis on juba andmeid tabelis "Clients"
            if (context.Clients.Any())
            {
                Console.WriteLine("Kliendid on juba olemas.");
                return;
            }

            // Loodud kliendid
            var clients = new List<Client>
    {
        new Client { Name = "Leonardo DiCaprio", PhoneNumber = "56488344"},
        new Client { Name = "Emma Watson", PhoneNumber = "56482954" },
        new Client { Name = "Will Smith", PhoneNumber = "56492742" },
        new Client { Name = "Scarlett Johansson", PhoneNumber = "58276433" },
        new Client { Name = "Tom Hanks", PhoneNumber = "59232324" },
        new Client { Name = "Meryl Streep", PhoneNumber = "56583344" },
        new Client { Name = "Dwayne Johnson", PhoneNumber = "56445822" },
        new Client { Name = "Ariana Grande", PhoneNumber = "56434384" },
        new Client { Name = "Chris Hemsworth", PhoneNumber = "58624334" },
        new Client { Name = "Rihanna", PhoneNumber = "56993252" }
    };

            // Lisame loodud kliendid andmebaasi
            context.Clients.AddRange(clients);

            // Salvesta muudatused andmebaasi
            context.SaveChanges();


        }


        public static void GeneratePanels(ApplicationDbContext context)
        {


            if (context.Panels.Any())
            {
                Console.WriteLine("Paneelid on juba olemas.");
                return;
            }


            var panels = new List<Panel>
    {
        new Panel { Name = "LePanel", Unit = "30", UnitCost = 200, Manufacturer = "Panhop White"},
        new Panel { Name = "LePanel (Black)", Unit = "30", UnitCost = 220, Manufacturer = "Panhop White"},
        new Panel { Name = "LePanel (White)", Unit = "30", UnitCost = 240, Manufacturer = "Panhop White"},
        new Panel { Name = "PanelTwitch", Unit = "15", UnitCost = 330, Manufacturer = "Panhop Black"},
        new Panel { Name = "PanelTwitch (Black)", Unit = "15", UnitCost = 290, Manufacturer = "Panhop Black"},
        new Panel { Name = "PanelTwitch (White)", Unit = "15", UnitCost = 340, Manufacturer = "Panhop Black"},
        new Panel { Name = "ThePanel", Unit = "30", UnitCost = 390, Manufacturer = "Tahiti White"},
        new Panel { Name = "ThePanel (Black)", Unit = "30", UnitCost = 400, Manufacturer = "Tahiti White"},
        new Panel { Name = "ThePanel (White)", Unit = "30", UnitCost = 290, Manufacturer = "Tahiti White"},
        new Panel { Name = "Panel", Unit = "30", UnitCost = 590, Manufacturer = "Tahiti White"},

    };

            context.Panels.AddRange(panels);


            context.SaveChanges();


        }

        public static void GenerateService(ApplicationDbContext context)
        {


            if (context.Service.Any())
            {
                Console.WriteLine("Teenused on juba olemas.");
                return;
            }


            var service = new List<Service>
    {
        new Service { Name = "Digging", Unit = "1", UnitCost = 800, Provider = "Digging.CO"},
        new Service { Name = "Landscaping", Unit = "1", UnitCost = 500, Provider = "GreenThumb Landscaping" },
        new Service { Name = "Demolition", Unit = "1", UnitCost = 2000, Provider = "DemoMasters" },
        new Service { Name = "Paving", Unit = "1", UnitCost = 1500, Provider = "PaveWay" },
        new Service { Name = "Concrete Pouring", Unit = "1", UnitCost = 1000, Provider = "SolidPour" },
        new Service { Name = "Hauling", Unit = "1", UnitCost = 600, Provider = "HaulAway" },
        new Service { Name = "Site Preparation", Unit = "1", UnitCost = 1300, Provider = "PrepRight" },
        new Service { Name = "Grading", Unit = "1", UnitCost = 1100, Provider = "GradeMaster" },
        new Service { Name = "Excavation", Unit = "1", UnitCost = 1200, Provider = "ExcavationPro" },
        new Service { Name = "Trenching", Unit = "1", UnitCost = 950, Provider = "TrenchPro" },

    };

            context.Service.AddRange(service);


            context.SaveChanges();


        }

        public static void GenerateMaterials(ApplicationDbContext context)
        {


            if (context.Materials.Any())
            {
                Console.WriteLine("Materialid on juba olemas.");
                return;
            }


            var materials = new List<Material>
    {
        new Material { Name = "Nails", Unit = "500", UnitCost = 300, Manufacturer = "Nails.CO"},
        new Material { Name = "Cement", Unit = "50kg", UnitCost = 1200, Manufacturer = "CemTech" },
        new Material { Name = "Sand", Unit = "1 ton", UnitCost = 500, Manufacturer = "SandWorks" },
        new Material { Name = "Bricks", Unit = "1000", UnitCost = 3500, Manufacturer = "BrickMaster" },
        new Material { Name = "Concrete Blocks", Unit = "500", UnitCost = 5000, Manufacturer = "BlockBuilders" },
        new Material { Name = "Steel Rebar", Unit = "1 ton", UnitCost = 15000, Manufacturer = "RebarPro" },
        new Material { Name = "Wood Planks", Unit = "1000", UnitCost = 2500, Manufacturer = "WoodWorks" },
        new Material { Name = "Roofing Shingles", Unit = "100", UnitCost = 7000, Manufacturer = "RoofGuard" },
        new Material { Name = "Insulation", Unit = "100 sq ft", UnitCost = 800, Manufacturer = "InsulaTech" },
        new Material { Name = "Paint", Unit = "1 gallon", UnitCost = 300, Manufacturer = "ColorTech" },

    };

            context.Materials.AddRange(materials);


            context.SaveChanges();


        }

        public static void GenerateBuildings(ApplicationDbContext context)
        {


            if (context.Buildings.Any())
            {
                Console.WriteLine("Ehitised on juba olemas.");
                return;
            }


            var buildings = new List<Buildings>
    {
        new Buildings { Name = "One Story House", PanelId = "LePanel", MaterialId = "Nails"},
        new Buildings { Name = "Two Story House", PanelId = "HighPanel", MaterialId = "Cement" },
        new Buildings { Name = "Wooden Cottage", PanelId = "WoodPanel", MaterialId = "Wood Planks" },
        new Buildings { Name = "Modern Apartment", PanelId = "GlassPanel", MaterialId = "Steel Rebar" },
        new Buildings { Name = "Brick House", PanelId = "BrickPanel", MaterialId = "Bricks" },
        new Buildings { Name = "Office Building", PanelId = "MetalPanel", MaterialId = "Steel Sheets" },
        new Buildings { Name = "Ranch Style House", PanelId = "SolarPanel", MaterialId = "Roofing Shingles" },
        new Buildings { Name = "Luxury Villa", PanelId = "LuxuryPanel", MaterialId = "Glass Panels" },
        new Buildings { Name = "Garage", PanelId = "UtilityPanel", MaterialId = "Concrete Blocks" },
        new Buildings { Name = "Farmhouse", PanelId = "RusticPanel", MaterialId = "Wood Planks" },

    };

            context.Buildings.AddRange(buildings);


            context.SaveChanges();


        }

        public static void GenerateBudgets(ApplicationDbContext context)
        {


            if (context.Budgets.Any())
            {
                Console.WriteLine("Eelarved on juba olemas.");
                return;
            }


            var budgets = new List<Budget>
    {
        new Budget { ClientId = 1, BuildingsId = 1, ServicesId = 1, Date = new DateTime(2024, 10, 23) },
        new Budget { ClientId = 2, BuildingsId = 2, ServicesId = 2, Date = new DateTime(2024, 11, 30) },
        new Budget { ClientId = 3, BuildingsId = 3, ServicesId = 3, Date = new DateTime(2024, 9, 12) },
        new Budget { ClientId = 4, BuildingsId = 4, ServicesId = 4, Date = new DateTime(2024, 10, 12) },
        new Budget { ClientId = 5, BuildingsId = 5, ServicesId = 5, Date = new DateTime(2024, 5, 13) },
        new Budget { ClientId = 6, BuildingsId = 6, ServicesId = 6, Date = new DateTime(2024, 2, 24) },
        new Budget { ClientId = 7, BuildingsId = 7, ServicesId = 7, Date = new DateTime(2024, 3, 5) },
        new Budget { ClientId = 8, BuildingsId = 8, ServicesId = 8, Date = new DateTime(2024, 10, 9) },
        new Budget { ClientId = 9, BuildingsId = 9, ServicesId = 9, Date = new DateTime(2024, 7, 19) },
        new Budget { ClientId = 10, BuildingsId = 10, ServicesId = 10, Date = new DateTime(2024, 1, 28) },

    };

            context.Budgets.AddRange(budgets);


            context.SaveChanges();


        }
    }
}

