using System;
using Newtonsoft.Json;
using System.IO;

namespace coreJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            Pedido pedidoImportado;
            try 
            {
                pedidoImportado = importPedido("gravando.json");
            } catch(FileNotFoundException ex){
                pedidoImportado = new Pedido(){
                    nome = "Usuario Teste",
                    endereco = "Joinville",
                    pedido = "Pizza Grande",
                    restaurante = "Baggio",
                    telefone = "1234 5678"
                };
            }
			exportPedido(pedidoImportado, "gravando.json");
        }

        private static Pedido importPedido(string arquivo)
        {
        	string json = File.ReadAllText(arquivo);
        	return JsonConvert.DeserializeObject<Pedido>(json);
        }

        private static void exportPedido(Pedido pedido, string arquivo)
        {
        	string json = JsonConvert.SerializeObject(pedido);
        	File.WriteAllText(arquivo, json);
        }
    }
}
