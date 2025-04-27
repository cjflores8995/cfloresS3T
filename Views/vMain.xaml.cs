using cfloresS2.Helpers;

namespace cfloresS2.Views;

public partial class vMain : ContentPage
{

	public vMain()
	{
		InitializeComponent();

		pkEstudiantes.ItemsSource = ListadoEstudiantes.Nombres;

        ValidarDecimales();

    }

    private async void btnProcesar_Clicked(object sender, EventArgs e)
    {
        var camposTexto = new List<Entry> { txtNotaSeguimiento_1, txtExamen_1, txtNotaSeguimiento_2, txtExamen_2 };
		var campoPicker = new List<Picker> { pkEstudiantes };

		var camposInvalidos = MetodosDeAyuda.ObtenerCamposInvalidos(camposTexto, campoPicker);

		await MostrarProceso(camposInvalidos);
    }

    #region Private Methods

    private async Task MostrarProceso(List<VisualElement> camposInvalidos)
    {
        if (camposInvalidos.Any())
        {
            await DisplayAlert("Error", "Completa todos los campos requeridos", "Aceptar");
        }
        else
        {
            await EjecutarResultado();
        }
    }

    /// <summary>
    /// habilita la validación de decimales
    /// </summary>
    private void ValidarDecimales()
    {
        MetodosDeAyuda.ValidarNumericosDecimales(txtNotaSeguimiento_1);
        MetodosDeAyuda.ValidarNumericosDecimales(txtExamen_1);
        MetodosDeAyuda.ValidarNumericosDecimales(txtNotaSeguimiento_2);
        MetodosDeAyuda.ValidarNumericosDecimales(txtExamen_2);
    }

    private async Task EjecutarResultado()
    {
        try
        {
            string estudianteSeleccionado = pkEstudiantes.SelectedItem?.ToString() ?? string.Empty;
            string fecha = dpFecha.Date.ToString("dd/MM/yyyy");
            decimal notaParcial1 = MetodosDeAyuda.NotaParcial(txtNotaSeguimiento_1.Text, txtExamen_1.Text);
            decimal notaParcial2 = MetodosDeAyuda.NotaParcial(txtNotaSeguimiento_2.Text, txtExamen_2.Text);


            string mensaje =
                $"Nombre: {estudianteSeleccionado}{Environment.NewLine}" +
                $"Fecha: {fecha}{Environment.NewLine}" +
                $"Nota Parcial 1: {notaParcial1}{Environment.NewLine}" +
                $"Nota Parcial 2: {notaParcial2}{Environment.NewLine}" +
                $"Nota Final: {MetodosDeAyuda.SumaNotas(notaParcial1, notaParcial2)}{Environment.NewLine}" +
                $"Estado: {MetodosDeAyuda.ObtenerMensajeNotaFinal(txtNotaSeguimiento_1.Text, txtExamen_1.Text, txtNotaSeguimiento_2.Text, txtExamen_2.Text)}";

            await DisplayAlert("Resultado", mensaje, "Aceptar");
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Algo salio mal!: {ex.Message}", "Aceptar");
        }
    }

    #endregion Private Methods
}