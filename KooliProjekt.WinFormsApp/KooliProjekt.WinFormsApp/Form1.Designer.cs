namespace KooliProjekt.WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PanelsGrid = new DataGridView();
            IdLabel = new Label();
            IdField = new TextBox();
            NameLabel = new Label();
            NameField = new TextBox();
            UnitLabel = new Label();
            UnitField = new TextBox();
            UnitCostLabel = new Label();
            UnitCostField = new TextBox();
            ManufacturerLabel = new Label();
            ManufacturerField = new TextBox();
            NewButton = new Button();
            SaveButton = new Button();
            DeleteButton = new Button();

            ((System.ComponentModel.ISupportInitialize)PanelsGrid).BeginInit();
            SuspendLayout();

            // 
            // PanelsGrid
            // 
            PanelsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PanelsGrid.Location = new Point(5, 6);
            PanelsGrid.MultiSelect = false;
            PanelsGrid.Name = "PanelsGrid";
            PanelsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PanelsGrid.Size = new Size(419, 432);
            PanelsGrid.TabIndex = 0;

            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.Location = new Point(460, 16);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new Size(21, 15);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "ID:";

            // 
            // IdField
            // 
            IdField.Location = new Point(507, 13);
            IdField.Name = "IdField";
            IdField.ReadOnly = true;
            IdField.Size = new Size(281, 23);
            IdField.TabIndex = 2;

            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(460, 56);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(33, 15);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "Name:";

            // 
            // NameField
            // 
            NameField.Location = new Point(507, 53);
            NameField.Name = "NameField";
            NameField.Size = new Size(281, 23);
            NameField.TabIndex = 4;

            // 
            // UnitLabel
            // 
            UnitLabel.AutoSize = true;
            UnitLabel.Location = new Point(460, 96);
            UnitLabel.Name = "UnitLabel";
            UnitLabel.Size = new Size(33, 15);
            UnitLabel.TabIndex = 8;
            UnitLabel.Text = "Unit:";

            // 
            // UnitField
            // 
            UnitField.Location = new Point(507, 93);
            UnitField.Name = "UnitField";
            UnitField.Size = new Size(281, 23);
            UnitField.TabIndex = 9;

            // 
            // UnitCostLabel
            // 
            UnitCostLabel.AutoSize = true;
            UnitCostLabel.Location = new Point(460, 136);
            UnitCostLabel.Name = "UnitCostLabel";
            UnitCostLabel.Size = new Size(58, 15);
            UnitCostLabel.TabIndex = 10;
            UnitCostLabel.Text = "Unit Cost:";

            // 
            // UnitCostField
            // 
            UnitCostField.Location = new Point(507, 133);
            UnitCostField.Name = "UnitCostField";
            UnitCostField.Size = new Size(281, 23);
            UnitCostField.TabIndex = 11;

            // 
            // ManufacturerLabel
            // 
            ManufacturerLabel.AutoSize = true;
            ManufacturerLabel.Location = new Point(460, 176);
            ManufacturerLabel.Name = "ManufacturerLabel";
            ManufacturerLabel.Size = new Size(87, 15);
            ManufacturerLabel.TabIndex = 12;
            ManufacturerLabel.Text = "Manufacturer:";

            // 
            // ManufacturerField
            // 
            ManufacturerField.Location = new Point(507, 173);
            ManufacturerField.Name = "ManufacturerField";
            ManufacturerField.Size = new Size(281, 23);
            ManufacturerField.TabIndex = 13;

            // 
            // NewButton
            // 
            NewButton.Location = new Point(522, 216);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 5;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;

            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(603, 216);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;

            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(684, 216);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 7;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(NewButton);
            Controls.Add(ManufacturerField);
            Controls.Add(ManufacturerLabel);
            Controls.Add(UnitCostField);
            Controls.Add(UnitCostLabel);
            Controls.Add(UnitField);
            Controls.Add(UnitLabel);
            Controls.Add(NameField);
            Controls.Add(NameLabel);
            Controls.Add(IdField);
            Controls.Add(IdLabel);
            Controls.Add(PanelsGrid);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)PanelsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView PanelsGrid;
        private Label IdLabel;
        private TextBox IdField;
        private Label NameLabel;
        private TextBox NameField;
        private Label UnitLabel;
        private TextBox UnitField;
        private Label UnitCostLabel;
        private TextBox UnitCostField;
        private Label ManufacturerLabel;
        private TextBox ManufacturerField;
        private Button NewButton;
        private Button SaveButton;
        private Button DeleteButton;
    }
}
