using KooliProjekt.PublicApi.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panel = KooliProjekt.PublicApi.Api.Panel;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IPanelView
    {
        private PanelPresenter _presenter;

        public Form1()
        {
            InitializeComponent();

            var apiClient = new ApiClient();
            _presenter = new PanelPresenter(this, apiClient);
            Presenter = _presenter;

            PanelsGrid.SelectionChanged += PanelsGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        public IList<Panel> Panels
        {
            get => (IList<Panel>)PanelsGrid.DataSource;
            set => PanelsGrid.DataSource = value;
        }

        private Panel _selectedItem;
        public Panel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                _presenter.UpdateView(value);
            }
        }

        public string Manufacturer
        {
            get => ManufacturerField.Text;
            set => ManufacturerField.Text = value;
        }

        public decimal UnitCost
        {
            get
            {
                if (decimal.TryParse(UnitCostField.Text, out var result))
                    return result;
                return 0;
            }
            set => UnitCostField.Text = value.ToString();
        }

        public string Unit
        {
            get => UnitField.Text;
            set => UnitField.Text = value;
        }

        public string Name
        {
            get => NameField.Text;
            set => NameField.Text = value;
        }

        public int Id
        {
            get
            {
                if (int.TryParse(IdField.Text, out var result))
                    return result;
                return 0;
            }
            set => IdField.Text = value.ToString();
        }

        public PanelPresenter Presenter { get; set; }
        System.Windows.Forms.Panel IPanelView.SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private async Task LoadDataAsync()
        {
            await _presenter.Load();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDataAsync();
        }

        private void PanelsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (PanelsGrid.SelectedRows.Count == 0)
            {
                SelectedItem = null;
                return;
            }

            var panel = PanelsGrid.SelectedRows[0].DataBoundItem as Panel;
            SelectedItem = panel;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Id = 0;
            Name = string.Empty;
            Unit = string.Empty;
            UnitCost = 0;
            Manufacturer = string.Empty;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var panel = new Panel
                {
                    Id = Id,
                    Name = Name,
                    Unit = Unit,
                    UnitCost = UnitCost,
                    Manufacturer = Manufacturer
                };

                await _presenter.Save(panel);
                await LoadDataAsync();

                MessageBox.Show("Saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving: " + ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("No item selected to delete.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this item?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                await _presenter.Delete(SelectedItem.Id);
                await LoadDataAsync();
            }
        }

        public void Delete(int panelId)
        {
            throw new NotImplementedException();
        }

        public void Save(Panel panel)
        {
            throw new NotImplementedException();
        }
    }
}
