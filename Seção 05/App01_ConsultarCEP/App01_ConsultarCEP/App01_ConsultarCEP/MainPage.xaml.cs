﻿using System;
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

            BOTAO.Clicked += buscarCEP;
        }

        private void buscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0}, {1}, {2}, {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrato para o CEP informado: " + cep, "OK");
                    }                    

                }catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
                
            }
            else
            {

            }
            
        }
        
        private bool isValidCEP(string cep)
        {
            bool valido = true;
            int novoCEP = 0;

            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres!", "OK");

                valido = false;
            }

            if(!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter somente números!", "OK");

                valido = false;
            }

            return valido;

        }
    }
}
