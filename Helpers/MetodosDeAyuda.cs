using System.Globalization;

namespace cfloresS2.Helpers
{
    public static class MetodosDeAyuda
    {
        /// <summary>
        /// Obtiene un listado de los campos invalidos
        /// </summary>
        /// <param name="camposTexto"></param>
        /// <param name="camposPicker"></param>
        /// <returns></returns>
        public static List<VisualElement> ObtenerCamposInvalidos(List<Entry> camposTexto, List<Picker> camposPicker)
        {
            var camposInvalidos = new List<VisualElement>();

            camposInvalidos.AddRange(camposTexto.Where(c => string.IsNullOrEmpty(c.Text)));
            camposInvalidos.AddRange(camposPicker.Where(p => p.SelectedItem == null));

            return camposInvalidos;
        }

        /// <summary>
        /// Valida que se púedan ingresar numeros entre 0 y 10
        /// </summary>
        /// <param name="entry"></param>
        public static void ValidarNumericosDecimales(Entry entry)
        {
            string separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            entry.Keyboard = Keyboard.Numeric;

            entry.TextChanged += (s, e) =>
            {
                string nuevoTexto = entry.Text;

                if (nuevoTexto == e.OldTextValue)
                    return;

                if (string.IsNullOrWhiteSpace(nuevoTexto))
                    return;

                string textoNormalizado = separadorDecimal == "," ?
                    nuevoTexto.Replace(".", ",") :
                    nuevoTexto.Replace(",", ".");

                if (!double.TryParse(textoNormalizado, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double valorNumerico))
                {
                    entry.Text = e.OldTextValue; 
                    return;
                }

                if (valorNumerico == 10 && nuevoTexto.EndsWith(separadorDecimal))
                {
                    entry.Text = nuevoTexto.Substring(0, nuevoTexto.Length - 1); 
                    return;
                }

                if (valorNumerico < 0 || valorNumerico > 10)
                {
                    entry.Text = e.OldTextValue;
                    return;
                }
            };
        }

        public static string ObtenerMensajeNotaFinal(string txtNotaSeguimiento_1, string txtExamen_1, string txtNotaSeguimiento_2, string txtExamen_2)
        {
            decimal notaSeguimiento1 = NotaParcial(txtNotaSeguimiento_1, txtExamen_1);
            decimal notaSeguimiento2 = NotaParcial(txtNotaSeguimiento_2, txtExamen_2);


            decimal sumaNotas = SumaNotas(notaSeguimiento1, notaSeguimiento2);

            return ObtenerMensajeNota(sumaNotas);
        }

        public static decimal NotaParcial(string txtNotaSeguimiento, string txtExamen)
        {
            decimal notaSeguimiento = (ConvertirAValorDecimal(txtNotaSeguimiento) * 0.3m);
            decimal notaExamen = (ConvertirAValorDecimal(txtExamen) * 0.2m);

            return Math.Round(notaSeguimiento + notaExamen, 2);
        }

        public static decimal SumaNotas(decimal nota1, decimal nota2)
        {
            return Math.Round((nota1 + nota2), 2);
        }

        private static string ObtenerMensajeNota(decimal sumaNotas)
        {
            return sumaNotas switch
            {
                <= 4.9m => "Reprobado", 
                <= 6.9m => "Complementaro",
                >= 7.0m => "Aprobado"
            };
        }

        public static decimal ConvertirAValorDecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            string separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            string textoNormalizado = separadorDecimal == "," ?
                texto.Replace(".", ",") :
                texto.Replace(",", ".");

            if (decimal.TryParse(textoNormalizado, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal resultado))
            {
                return resultado;
            }

            return 0;
        }

    }
}
