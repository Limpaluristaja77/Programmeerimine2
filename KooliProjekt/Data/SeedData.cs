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

        public static void GenerateServices(ApplicationDbContext context)
        {


            if (context.Services.Any())
            {
                Console.WriteLine("Teenused on juba olemas.");
                return;
            }


            var services = new List<Service>
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

            context.Services.AddRange(services);


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
            var panels1 = context.Panels.FirstOrDefault();
            var panels2 = context.Panels.Skip(1).Take(1).FirstOrDefault();
            var panels3 = context.Panels.Skip(2).Take(1).FirstOrDefault();
            var panels4 = context.Panels.Skip(3).Take(1).FirstOrDefault();
            var panels5 = context.Panels.Skip(4).Take(1).FirstOrDefault();
            var panels6 = context.Panels.Skip(5).Take(1).FirstOrDefault();
            var panels7 = context.Panels.Skip(6).Take(1).FirstOrDefault();
            var panels8 = context.Panels.Skip(7).Take(1).FirstOrDefault();
            var panels9 = context.Panels.Skip(8).Take(1).FirstOrDefault();
            var panels10 = context.Panels.Skip(9).Take(1).FirstOrDefault();

            var materials1 = context.Materials.FirstOrDefault();
            var materials2 = context.Materials.Skip(1).Take(1).FirstOrDefault();
            var materials3 = context.Materials.Skip(2).Take(1).FirstOrDefault();
            var materials4 = context.Materials.Skip(3).Take(1).FirstOrDefault();
            var materials5 = context.Materials.Skip(4).Take(1).FirstOrDefault();
            var materials6 = context.Materials.Skip(5).Take(1).FirstOrDefault();
            var materials7 = context.Materials.Skip(6).Take(1).FirstOrDefault();
            var materials8 = context.Materials.Skip(7).Take(1).FirstOrDefault();
            var materials9 = context.Materials.Skip(8).Take(1).FirstOrDefault();
            var materials10 = context.Materials.Skip(9).Take(1).FirstOrDefault();


            var buildings = new List<Buildings>
    {
                new Buildings { Name = "One Story House", PanelId = panels1.Id, MaterialId = materials1.Id},
                new Buildings { Name = "Two Story House", PanelId = panels2.Id, MaterialId = materials2.Id },
                new Buildings { Name = "Wooden Cottage", PanelId = panels3.Id, MaterialId = materials3.Id },
                new Buildings { Name = "Modern Apartment", PanelId = panels4.Id, MaterialId = materials4.Id },
                new Buildings { Name = "Brick House", PanelId = panels5.Id, MaterialId = materials5.Id },
                new Buildings { Name = "Office Building", PanelId = panels6.Id, MaterialId = materials6.Id },
                new Buildings { Name = "Ranch Style House", PanelId = panels7.Id, MaterialId = materials7.Id },
                new Buildings { Name = "Luxury Villa", PanelId = panels8.Id, MaterialId = materials8.Id },
                new Buildings { Name = "Garage", PanelId = panels9.Id, MaterialId = materials9.Id, },
                new Buildings { Name = "Farmhouse", PanelId = panels10.Id, MaterialId = materials10.Id },

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
            

            var client1 = context.Clients.FirstOrDefault();
            var client2 = context.Clients.Skip(1).Take(1).FirstOrDefault();
            var client3 = context.Clients.Skip(2).Take(1).FirstOrDefault();
            var client4 = context.Clients.Skip(3).Take(1).FirstOrDefault();
            var client5 = context.Clients.Skip(4).Take(1).FirstOrDefault();
            var client6 = context.Clients.Skip(5).Take(1).FirstOrDefault();
            var client7 = context.Clients.Skip(6).Take(1).FirstOrDefault();
            var client8 = context.Clients.Skip(7).Take(1).FirstOrDefault();
            var client9 = context.Clients.Skip(8).Take(1).FirstOrDefault();
            var client10 = context.Clients.Skip(9).Take(1).FirstOrDefault();

            var buildings1 = context.Buildings.FirstOrDefault();
            var buildings2 = context.Buildings.Skip(1).Take(1).FirstOrDefault();
            var buildings3 = context.Buildings.Skip(2).Take(1).FirstOrDefault();
            var buildings4 = context.Buildings.Skip(3).Take(1).FirstOrDefault();
            var buildings5 = context.Buildings.Skip(4).Take(1).FirstOrDefault();
            var buildings6 = context.Buildings.Skip(5).Take(1).FirstOrDefault();
            var buildings7 = context.Buildings.Skip(6).Take(1).FirstOrDefault();
            var buildings8 = context.Buildings.Skip(7).Take(1).FirstOrDefault();
            var buildings9 = context.Buildings.Skip(8).Take(1).FirstOrDefault();
            var buildings10 = context.Buildings.Skip(9).Take(1).FirstOrDefault();

            var service1 = context.Services.FirstOrDefault();
            var service2 = context.Services.Skip(1).Take(1).FirstOrDefault();
            var service3 = context.Services.Skip(2).Take(1).FirstOrDefault();
            var service4 = context.Services.Skip(3).Take(1).FirstOrDefault();
            var service5 = context.Services.Skip(4).Take(1).FirstOrDefault();
            var service6 = context.Services.Skip(5).Take(1).FirstOrDefault();
            var service7 = context.Services.Skip(6).Take(1).FirstOrDefault();
            var service8 = context.Services.Skip(7).Take(1).FirstOrDefault();
            var service9 = context.Services.Skip(8).Take(1).FirstOrDefault();
            var service10 = context.Services.Skip(9).Take(1).FirstOrDefault();

            var budgets = new List<Budget>
            {
                new Budget { ClientId = client1.Id, BuildingsId = buildings1.Id, ServicesId = service1.Id, Date = new DateTime(2024, 10, 23) },
                new Budget { ClientId = client2.Id, BuildingsId = buildings2.Id, ServicesId = service2.Id, Date = new DateTime(2024, 11, 30) },
                new Budget { ClientId = client3.Id, BuildingsId = buildings3.Id, ServicesId = service3.Id, Date = new DateTime(2024, 9, 12) },
                new Budget { ClientId = client4.Id, BuildingsId = buildings4.Id, ServicesId = service4.Id, Date = new DateTime(2024, 10, 12) },
                new Budget { ClientId = client5.Id, BuildingsId = buildings5.Id, ServicesId = service5.Id, Date = new DateTime(2024, 5, 13) },
                new Budget { ClientId = client6.Id, BuildingsId = buildings6.Id, ServicesId = service6.Id, Date = new DateTime(2024, 2, 24) },
                new Budget { ClientId = client7.Id, BuildingsId = buildings7.Id, ServicesId = service7.Id, Date = new DateTime(2024, 3, 5) },
                new Budget { ClientId = client8.Id, BuildingsId = buildings8.Id, ServicesId = service8.Id, Date = new DateTime(2024, 10, 9) },
                new Budget { ClientId = client9.Id, BuildingsId = buildings9.Id, ServicesId = service9.Id, Date = new DateTime(2024, 7, 19) },
                new Budget { ClientId = client10.Id, BuildingsId = buildings10.Id, ServicesId = service10.Id, Date = new DateTime(2024, 1, 28) },

            };

            context.Budgets.AddRange(budgets);


            context.SaveChanges();


        }
    }
}

