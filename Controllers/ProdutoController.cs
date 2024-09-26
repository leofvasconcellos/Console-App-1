using Console_App_1.Models;
using Console_App_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Console_App_1.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoService _produtoService = new ProdutoService();

        private static List<Produto> produtos = new List<Produto>
        {
            new Produto { Descricao = "Papel", ValorVenda = 2.0, ValorCompra = 0.5, Tipo = TipoProduto.MateriaPrima, DataCriacao = DateTime.Now },
            new Produto { Descricao = "Lápis", ValorVenda = 4.0, ValorCompra = 2.0, Tipo = TipoProduto.Consumo, DataCriacao = DateTime.Now },
            new Produto { Descricao = "Caneta", ValorVenda = 7.0, ValorCompra = 3.0, Tipo = TipoProduto.Intermediario, DataCriacao = DateTime.Now },
            new Produto { Descricao = "Borracha", ValorVenda = 3.5, ValorCompra = 1.0, Tipo = TipoProduto.Final, DataCriacao = DateTime.Now },
        };

        public IActionResult Index(string periodo, string tipo)
        {
            var produtosFiltrados = produtos.AsQueryable();

            switch (periodo)
            {
                case "01":
                    produtosFiltrados = produtosFiltrados.Where(p => p.DataCriacao.Date.Month >= 1 && p.DataCriacao.Date.Month <= 3);
                    break;
                case "02":
                    produtosFiltrados = produtosFiltrados.Where(p => p.DataCriacao.Date.Month >= 4 && p.DataCriacao.Date.Month <=6);
                    break;
                case "03":
                    produtosFiltrados = produtosFiltrados.Where(p => p.DataCriacao.Date.Month >= 7 && p.DataCriacao.Date.Month <= 9);
                    break;
                case "04":
                    produtosFiltrados = produtosFiltrados.Where(p => p.DataCriacao.Date.Month >= 10 && p.DataCriacao.Date.Month <= 12);
                    break;
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                produtosFiltrados = produtosFiltrados.Where(p => p.Tipo.ToString() == tipo);
            }

            ViewBag.ProdutosComMaiorMargem = produtos.OrderByDescending(p => (p.ValorVenda - p.ValorCompra) / p.ValorCompra).Take(3).ToList();
            ViewBag.ProdutosComMenorMargem = produtos.OrderBy(p => (p.ValorVenda - p.ValorCompra) / p.ValorCompra).Take(3).ToList();

            return View(produtosFiltrados.ToList());
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtos = _produtoService.CadastrarProduto(produto, produtos);
                return RedirectToAction("Index");
            }
            return View(produto);
        }
    }
}
