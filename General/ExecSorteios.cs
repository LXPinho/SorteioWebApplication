using SorteioWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using static SorteioWebApplication.Tools.ExecSorteios;

namespace SorteioWebApplication.Tools
{
    internal class ExecSorteios
    {
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }
        public int Id { get; internal set; }
        public class ListaSorteios
        {
            public List<Sorteios> Sorteios { get; set; } = new List<Sorteios>();
        }
        static public List<Sorteios> ListaSorteiosItens { get; set; } = new List<Sorteios>();
        static public void LimpaListaSorteios()
        {
            if (ListaSorteiosItens != null && ListaSorteiosItens.Count > 0)
            {
                ListaSorteiosItens.Clear();
            }
        }
        static public Sorteios SorteiosItem { get; set; } = new Sorteios();
        static public void LimpaClienteItem()
        {
            SorteiosItem.NumeroDoSorteio = 0;
            SorteiosItem.QtdeNumerosSorteados = 0;
            SorteiosItem.VibesAculumadas = 0;
            foreach(ListaNumerosSorteados item in SorteiosItem.ListaSorteios) item.listaNumerosSorteados.Clear();
            SorteiosItem.ListaSorteios.Clear();
            SorteiosItem.Texto = string.Empty;
        }

        static void ShowSorteios(List<Sorteios>? clientes)
        {
            int count = 0;
            foreach (Sorteios sorteios in clientes)
            {
                Console.Write($"[{++count}]\t");
                ShowSorteios(sorteios);
            }
        }
        static void ShowSorteios(Sorteios sorteios)
        {
            Console.WriteLine($"Sorteios: {sorteios.ToString()}");
        }
        static async Task<Uri> CreateSorteiosAsync(Sorteios sorteios)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().PostAsJsonAsync(
                $"api/Sorteio/Post{sorteios}", sorteios);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<List<Sorteios>> GetClienteAsync(string path)
        {
            List<Sorteios> sorteios;
            using (HttpResponseMessage response = await Tools.getInstanceHttpClient().GetAsync(path + $"api/Sorteio/"))
            {
                sorteios = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<List<Sorteios>>(await response.Content.ReadAsAsync<string>()) ?? new List<Sorteios>()
                    : new List<Sorteios>();
            }
            return sorteios;
        }
        static async Task<Sorteios> UpdateSorteiosAsync(Sorteios sorteios)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().PutAsJsonAsync(
                $"api/Sorteio/Put{sorteios.NumeroDoSorteio},{sorteios}", sorteios);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            sorteios = await response.Content.ReadAsAsync<Sorteios>();
            return sorteios;
        }
        static async Task<HttpStatusCode> DeleteSorteiosAsync(string id)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().DeleteAsync(
                $"api/Sorteio/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(enum_opcao opcao = enum_opcao.enumGet)
        {
            ListaSorteiosItens = new List<Sorteios>();
            try
            {
                switch (opcao)
                {
                    case enum_opcao.enumCreate:
                        // Create
                        Uri url = await CreateSorteiosAsync(SorteiosItem);
                        break;

                    case enum_opcao.enumGet:

                        // Get 
                        Uri? baseAddress = Tools.getInstanceHttpClient().BaseAddress;
                        if (baseAddress != null)
                        {
                            ListaSorteiosItens = await GetClienteAsync(baseAddress.ToString());
                        }
                        break;

                    case enum_opcao.enumUpdate:
                        // Update
                        Sorteios cliente = await UpdateSorteiosAsync(SorteiosItem);
                        break;

                    case enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeleteSorteiosAsync(SorteiosItem.NumeroDoSorteio.ToString());
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
