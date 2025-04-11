
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PanelsGrid.SelectionChanged += PanelsGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            // Küsi kustutamise kinnitust
            // Kui vastus oli "Yes", siis kustuta element ja lae andmed uuesti
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            // Kontrolli ID-d:
            //  - kui IDField on tühi või 0, siis tee uus objekt
            //  - kui IDField ei ole tühi, siis küsi käesolev objekt gridi käest
            // Salvesta API kaudu
            // Lae andmed API-st uuesti
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            IdField.Text = string.Empty;
            NameField.Text = string.Empty;
            UnitField.Text = string.Empty;
            UnitCostField.Text = string.Empty;
            ManufacturerField.Text = string.Empty;

        }

        private void PanelsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (PanelsGrid.SelectedRows.Count == 0)
            {
                return;
            }

            var panel = (Api.Panel)PanelsGrid.SelectedRows[0].DataBoundItem;

            if(panel == null)
            {
                IdField.Text = string.Empty;
                NameField.Text = string.Empty;
                UnitField.Text = string.Empty;
                UnitCostField.Text = string.Empty;
                ManufacturerField.Text = string.Empty;
            }
            else
            {
                IdField.Text = panel.Id.ToString();
                NameField.Text = panel.Name;
                UnitField.Text = panel.Unit;
                UnitCostField.Text = panel.UnitCost.ToString();
                ManufacturerField.Text = panel.Manufacturer;
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var apiClient = new ApiClient();
            var response = await apiClient.List();

            PanelsGrid.AutoGenerateColumns = true;
            PanelsGrid.DataSource = response.Value;
            
        }
    }
}
