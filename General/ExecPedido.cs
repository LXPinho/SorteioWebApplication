﻿using SorteioWebApplication.Models;
using System.Net;
using System.Text.Json;

namespace SorteioWebApplication.Tools
{
    internal class ExecPedido
    {
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }
        public class ListaPedidos
        {
            public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        }
        static public ListaPedidos ListaPedidosItens { get; set; } = new ListaPedidos();
        static public void LimpaListaPedidosItens()
        {
            if (ListaPedidosItens != null && ListaPedidosItens.Pedidos.Count() > 0)
            {
                ListaPedidosItens.Pedidos.Clear();
            }
        }
        static public Pedido PedidoItem { get; set; } = new Pedido();
        static public void LimpaPedidoItem()
        {
            PedidoItem = new Pedido();
        }
        static void ShowPedidos(List<Pedido>? pedidos)
        {
            int count = 0;
            foreach (Pedido pedido in pedidos)
            {
                Console.Write($"[{++count}]\t");
                ShowPedido(pedido);
            }
        }
        static void ShowPedido(Pedido pedido)
        {
            Console.WriteLine(
                $"IdCliente: {pedido.ClientePedido}"
              + $"\tDataDaVenda:{pedido.DataDaVenda}");
        }
        static async Task<Uri> CreatePedidoAsync(Pedido pedido)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().PostAsJsonAsync(
                $"api/Pedido/Post{pedido}", pedido);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<ListaPedidos> GetPedidoAsync(string path)
        {
            ListaPedidos pedidos;
            using (HttpResponseMessage response = await Tools.getInstanceHttpClient().GetAsync(path + $"api/Pedido/"))
            {
                pedidos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaPedidos>(await response.Content.ReadAsAsync<string>()) ?? new ListaPedidos()
                    : new ListaPedidos();
            }
            return pedidos;
        }
        static async Task<Pedido> UpdatePedidoAsync(Pedido pedido)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().PutAsJsonAsync(
                $"api/Pedido/Put{pedido.Id},{pedido}", pedido);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            pedido = await response.Content.ReadAsAsync<Pedido>();
            return pedido;
        }
        static async Task<HttpStatusCode> DeletePedidoAsync(string id)
        {
            HttpResponseMessage response = await Tools.getInstanceHttpClient().DeleteAsync(
                $"api/Pedido/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(enum_opcao opcao = enum_opcao.enumGet)
        {
            ListaPedidosItens = new ListaPedidos();
            try
            {
                switch (opcao)
                {
                    case enum_opcao.enumCreate:
                        // Create
                        Uri url = await CreatePedidoAsync(PedidoItem);
                        break;

                    case enum_opcao.enumGet:

                        // Get 
                        Uri? baseAddress = Tools.getInstanceHttpClient().BaseAddress;
                        if (baseAddress != null)
                        {
                            ListaPedidosItens = await GetPedidoAsync(baseAddress.ToString());
                        }
                        break;

                    case enum_opcao.enumUpdate:

                        // Update
                        Pedido pedido = await UpdatePedidoAsync(PedidoItem);
                        break;

                    case enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeletePedidoAsync(PedidoItem.Id.ToString());
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
