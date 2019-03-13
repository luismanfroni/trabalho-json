import json

def gravarJson(lista):
    with open('gravando.json', 'w') as f:
        json.dump(lista, f)
def carregarJson():
    with open ('gravando.json', 'r') as f:
        return json.load(f)

pedido = {
    'Endereço': 'Rua iguaçu',
    'Nome' : 'Victor',
    'Pedido': 'Pizza portuguesa'
}
try:
	pedido = carregarJson()
except:
	pass
gravarJson(pedido)
print(carregarJson())