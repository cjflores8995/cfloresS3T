namespace cfloresS2.Views;

public partial class vLogin : ContentPage
{
	private readonly string[] usuarios = { "Carlos", "Ana", "Jose" };
	private readonly string[] passwords = { "carlos123", "ana123", "jose123" };

    public vLogin()
	{
		InitializeComponent();
	}

    private async void LoginClicked(object sender, EventArgs e)
	{
		string usuario = txtUsuario.Text?.Trim() ?? string.Empty;
		string password = txtPassword.Text?.Trim() ?? string.Empty;

		if(EsUsuarioValido(usuario, password))
		{
			await Navigation.PushAsync(new vMain());
		}
		else
		{
			await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Aceptar");
		}
	}

	/// <summary>
	/// Valida el usuario contra el listado de usuarios del array
	/// </summary>
	/// <param name="usuario"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	private bool EsUsuarioValido(string usuario, string password)
	{
		for(int i=0; i< usuarios.Length; i++)
		{
			if (usuarios[i].Equals(usuario, StringComparison.OrdinalIgnoreCase) && passwords[i].Equals(password, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}
}