using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            botao.Clicked += buscarCep;         
        }

        private void buscarCep(object sender, EventArgs args)
        {            
            string ceplido = cep.Text.Trim();

            if (isValidCep(ceplido))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(ceplido);
                    if (end != null)
                    {
                        resultado.Text = string.Format("Endereço: {2}, {3} - {0}, {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não encontrado para o CEP informado: " + ceplido, "OK");
                    }
                    
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
                
            }
          
            //TODO - validações

        }
        private bool isValidCep(string ceplido)
        {
            bool valid = true;
            if (ceplido.Length != 8)
            {
                DisplayAlert("ERRO", "CEP INVÁLIDO! O CEP deve conter 8 caracteres", "OK");
                valid = false;
            }

            int novoCep = 0;
            if (!int.TryParse(ceplido, out novoCep))
            {
                DisplayAlert("ERRO", "CEP INVÁLIDO! O CEP deve conter somente números", "OK");
                valid = false;
            }
            return valid;
        }
    }
}
