using System;

using System.IO;
using System.Collections.Generic;

namespace coreJSON
{
    class Program
    {
        const string arquivo = "../gravando.json";
        static void Main(string[] args)
        {
            PedidoManager pm = new PedidoManager(arquivo);
            if(args != null && args.Length > 0){
                foreach(string arg in args){
                    string param = arg.Trim().ToUpper();
                    switch(param){
                        case "V":
                        case "VERBOSE":
                            pm.verbose = true;
                            break;
                        case "D":
                        case "DEBUG":
                            pm.debug = true;
                            break;
                    }
                    
                }
                
            }
            Console.WriteLine("Checando por atualizações nos pedidos em " + arquivo);
            while(true) { }
        }

        
    }
}
