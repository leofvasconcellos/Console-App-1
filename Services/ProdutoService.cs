using Console_App_1.Models;
using System.Drawing;

namespace Console_App_1.Services
{
    public class ProdutoService
    {
        public ProdutoService() { }

        public List<Produto> CadastrarProduto(Produto produto, List<Produto> produtos) {

            //1 - O valor da venda não pode ser menor ou igual ao valor da compra;
            if (produto.ValorVenda <= produto.ValorCompra)
                throw new InvalidOperationException("O valor da venda não pode ser menor ou igual ao valor da compra!");
            //2 - A data de criação do produto não pode ser menor que 01 / 01 / 2024;
            if (produto.DataCriacao < new DateTime(2024, 01, 01))
                throw new InvalidOperationException("A data de criação do produto não pode ser menor que 01 / 01 / 2024!");
            //3 - A descrição do produto deve conter no mínimo 5 caracteres;
            if (produto.Descricao.Length < 5)
                throw new InvalidOperationException("A descrição do produto deve conter no mínimo 5 caracteres!");
            //4 - Os valores de compra e venda não podem ser zero ou abaixo de zero;
            if (produto.ValorVenda <= 0 || produto.ValorCompra <= 0)
                throw new InvalidOperationException("Os valores de compra e venda não podem ser zero ou abaixo de zero!");

            produtos.Add(produto);

            Console.WriteLine("Produto incluído com sucesso!");

            return produtos;
        }
    }
}
