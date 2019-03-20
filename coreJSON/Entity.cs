using Newtonsoft.Json;
namespace coreJSON
{
	public class Pedido 
	{
		public int id;
		public string nome;
		public string endereco;
		public string pedido;
		public string restaurante;
		public string telefone;

		public bool Compare(Pedido compare){
			if(this.id != compare.id)
				return false;

			if(this.nome != compare.nome)
				return false;
			
			if(this.endereco != compare.endereco)
				return false;
			
			if(this.pedido != compare.pedido)
				return false;

			if(this.restaurante != compare.restaurante)
				return false;

			if(this.telefone != compare.telefone)
				return false;

			return true;
		}

		public override string ToString(){
			return JsonConvert.SerializeObject(this);
		}
	}
}