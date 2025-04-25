using KooliProjekt.PublicApi.Api;
using System;
using System.Windows.Forms;


namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IPanelView
    {
        public IList<KooliProjekt.PublicApi.Api.Panel> Panels 
        {
            get
            {
                return (IList<KooliProjekt.PublicApi.Api.Panel>)PanelsGrid.DataSource;
            }
            set
            {
                PanelsGrid.DataSource = value;
            }
        }

        public KooliProjekt.PublicApi.Api.Panel SelectedItem { get; set; }

        public PanelPresenter Presenter { get; set; }

        public string Manufacturer
        {
            get
            {
                return ManufacturerField.Text; ;
            }
            set
            {
                ManufacturerField.Text = value;
            }
        }
        public decimal UnitCost
        {
            get
            {

                decimal result;
                if (decimal.TryParse(UnitCostField.Text, out result))
                {
                    return result;
                }
                else
                {

                    return 0;
                }
            }
            set
            {

                UnitCostField.Text = value.ToString();
            }
        }
        public string Unit
        {
            get
            {
                return UnitField.Text; ;
            }
            set
            {
                UnitField.Text = value;
            }
        }

        public string Name
        {
            get
            {
                return NameField.Text; ;
            }
            set
            {
                NameField.Text = value;
            }
        }

        public int Id
        {
            get
            {
                return int.Parse(IdField.Text);
            }
            set
            {
                IdField.Text = value.ToString();
            }
        }

        System.Windows.Forms.Panel IPanelView.SelectedItem 
        {
            get; set;
        }

        public Form1()
        {
            InitializeComponent();

            PanelsGrid.SelectionChanged += PanelsGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
            

        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            // Küsi kustutamise kinnitust
            // Kui vastus oli "Yes", siis kustuta element ja lae andmed uuesti

            var confirmationResult = MessageBox.Show("Are you sure?","Wish to delete?",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

           
            if (confirmationResult == DialogResult.Yes)
            {
                
            }
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

            var panel = (KooliProjekt.PublicApi.Api.Panel)PanelsGrid.SelectedRows[0].DataBoundItem;

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
