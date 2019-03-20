using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace coreJSON
{
    public class PedidoManager
    {
        const string separador = "-------------------------------------------";
        List<Pedido> pedidos = new List<Pedido>();
        Watcher pedidosWatcher;
        string filePath;

        bool _verbose = false;
        public bool verbose 
        { get { return _verbose; } set { this.pedidosWatcher.verbose = this._verbose = value; } }
        bool _debug = false;
        public bool debug 
        { get { return _debug; } set { this.pedidosWatcher.debug = this._debug = value; } }

        public PedidoManager(string arquivo)
        {
            filePath = arquivo;
            AtualizarPedidos();
            pedidosWatcher = new Watcher("../", arquivo);
            pedidosWatcher.handler += UpdateFile;
        }


        public void UpdateFile(FileSystemEventArgs args) 
        {
            List<Pedido> tempPedidos = importPedido(args.FullPath);
            if (tempPedidos != null && tempPedidos.Count > 0){
                foreach(Pedido tempPedido in tempPedidos){
                    Pedido achado = pedidos.FirstOrDefault( x => x.id == tempPedido.id);
                    if (achado != null && !achado.Compare(tempPedido)) {
                        if(verbose) Console.WriteLine(separador);
                        Console.WriteLine($"Pedido { achado.id } Alterado");
                        if(verbose){
                            Console.WriteLine($"DE: { achado }");
                            Console.WriteLine($"PARA: {tempPedido}");
                            Console.WriteLine(separador);
                        }
                    } else if (achado == null) {
                        if(verbose) Console.WriteLine(separador);
                        Console.WriteLine($"Pedido { tempPedido.id } Criado");
                        if(verbose) {
                            Console.WriteLine(tempPedido);
                            Console.WriteLine(separador);
                        }
                    }
                }
                foreach(Pedido pedido in pedidos){
                    Pedido achado = tempPedidos.FirstOrDefault( x => x.id == pedido.id);
                    if (achado == null){
                        if(verbose) Console.WriteLine(separador);
                        Console.WriteLine($"Pedido { pedido.id } Deletado");
                        if(verbose) {
                            Console.WriteLine(pedido);
                            Console.WriteLine(separador);
                        }
                    }
                }
                pedidos = tempPedidos;
            }
        }

        public void AtualizarPedidos()
        {
            try 
            {
                pedidos = importPedido(filePath);
            } catch(Exception ex){
                if (debug) Console.WriteLine($"AtualizarPedidos ERROR: { ex.Message } { ex.Source } { ex.StackTrace }");
            }
        }

        private List<Pedido> importPedido(string arquivo)
        {
        	string json;
            try {
                json = File.ReadAllText(arquivo);
            } catch(Exception e) 
            {
                json = ""; 
                if (debug) 
                    Console.WriteLine($"Error Reading '{ arquivo }': { e.Message }");
            }
        	List<Pedido> pedidoImportado = new List<Pedido>();
            try {
                pedidoImportado = JsonConvert.DeserializeObject<List<Pedido>>(json);
            } catch(Exception e)
            {
                if (debug) 
                    Console.WriteLine($"Error deserializing '{ arquivo }': { e.Message }"); 
            }
            
            return pedidoImportado;
        }
    }
}