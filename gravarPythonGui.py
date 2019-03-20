from tkinter import *
from tkinter import messagebox
import json


class PedidoGUI:
    id = None
    nome = None
    endereco = None
    pedido = None
    restaurante = None
    telefone = None

class Pedido:
    def __init__(self):
        self.id = '0'
        self.nome = ''
        self.endereco = ''
        self.pedido = ''
        self.restaurante = ''
        self.telefone = ''
    def toObject(self):
        tempDict = self.__dict__
        jsonObj = json.dumps(tempDict)
        Obj = json.loads(jsonObj)
        return Obj

top = Tk()

rowIND = 0
for pedidoAtr in vars(PedidoGUI):
    if not pedidoAtr.strip().startswith("__"):
        lbl = Label(top, text=pedidoAtr)
        lbl.grid(column = 1, row = rowIND)
        txt = Entry(top,width=15)
        txt.grid(column = 2, row = rowIND)
        setattr(PedidoGUI, pedidoAtr, txt)
        rowIND = rowIND + 1

listboxPedidos = Listbox(top)


def gravarJson(lista):
    with open('gravando.json', 'w') as f:
        json.dump(lista, f)
def carregarJson():
    with open ('gravando.json', 'r') as f:
        return json.load(f)
def salvarPedido(pedido):
    tempListPedidos = []
    try:
        tempListPedidos = carregarJson()
    except:
        pass
    try:
        pedidoEncontrado = next((x for x in tempListPedidos if x['id'] == pedido.id), None)
        if pedidoEncontrado is not None:
            tempListPedidos.remove(pedidoEncontrado)
        tempListPedidos.append(pedido.toObject())
        gravarJson(tempListPedidos)
    except Exception as e:
        pass
    

def carregaListaPedidos():
    try:
        listboxPedidos.delete(0,'end')
        listaPedidos = carregarJson()
        for pedido in listaPedidos:
            listboxPedidos.insert(pedido['id'], pedido['id'].__str__())
    except Exception as e:
        pass

carregaListaPedidos()

listboxPedidos.grid(column=3, row=0, rowspan=6, columnspan=2, padx=(25,0))

def saveGUI():
    try:
        pedidoEditado = Pedido()
        for pedidoAtr in vars(PedidoGUI):
            if not pedidoAtr.strip().startswith("__"):
                txt = getattr(PedidoGUI, pedidoAtr)
                setattr(pedidoEditado, pedidoAtr, txt.get())

        salvarPedido(pedidoEditado)
        carregaListaPedidos()
        messagebox.showinfo("Aviso", "Pedido salvo!")
    except Exception as e:
        messagebox.showerror("Erro", "Erro ao tentar salvar pedido!")

def limparGUI():
    try:
        for pedidoAtr in vars(PedidoGUI):
            if not pedidoAtr.strip().startswith("__"):
                txt = getattr(PedidoGUI, pedidoAtr)
                txt.delete(0, END)
    except Exception as e:
        pass

btnSalvar = Button(top, text="Salvar", command=saveGUI)
btnSalvar.grid(column=1, row=6)

btnLimpar = Button(top, text="Limpar", command=limparGUI)
btnLimpar.grid(column=2, row=6)


def deletarGUI():
    try:
        selId = listboxPedidos.get(listboxPedidos.curselection())
        tempListPedidos = carregarJson()
        tempListPedidos = [x for x in tempListPedidos if x['id'] != selId]
        gravarJson(tempListPedidos)
        carregaListaPedidos()
    except Exception as e:
        pass

def editarGUI():
    try:
        tempListPedidos = carregarJson()
        selId = listboxPedidos.get(listboxPedidos.curselection())
        pedidoEncontrado = next((x for x in tempListPedidos if x['id'] == selId), None)
        if pedidoEncontrado is not None:
            for pedidoAtr in vars(PedidoGUI):
                if not pedidoAtr.strip().startswith("__"):
                    txt = getattr(PedidoGUI, pedidoAtr)
                    txtValue = pedidoEncontrado[pedidoAtr]
                    txt.delete(0, END)
                    txt.insert(END, txtValue)
    except Exception as e:
        pass

btnDeletar = Button(top, text="Deletar", command=deletarGUI)
btnDeletar.grid(column=3, row=6, padx=(25,0))

btnEditar = Button(top, text="Editar", command=editarGUI)
btnEditar.grid(column=4, row=6)

top.mainloop()