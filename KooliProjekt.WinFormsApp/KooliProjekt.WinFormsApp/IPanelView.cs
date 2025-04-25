
namespace KooliProjekt.WinFormsApp
{
    public interface IPanelView
    {
        IList<KooliProjekt.PublicApi.Api.Panel> Panels { get; set; }
        Panel SelectedItem { get; set; }
        string Manufacturer { get; set; }
        decimal UnitCost { get; set; }
        string Unit {  get; set; }
        string Name { get; set; }
        int Id { get; set; }
        PanelPresenter Presenter { get; set; }
    }
}
