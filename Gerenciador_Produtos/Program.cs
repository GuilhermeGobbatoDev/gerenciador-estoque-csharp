
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gerenciador_Produtos
{
    public class Produto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }

    internal class Program
    {
        public static List<Produto> produtos = new List<Produto>();

        static void CadastrarProdutos()
        {
            Console.Clear();
            int quantidade_cadastrar;

            Console.WriteLine("========== CADASTRO DE PRODUTOS ==========");
            Console.Write("\nQuantos produtos deseja cadastrar? ");

            while (!int.TryParse(Console.ReadLine(), out quantidade_cadastrar)
                   || quantidade_cadastrar <= 0)
            {
                Console.Write("Quantidade inválida. Digite um número maior que zero: ");
            }

            for (int i = 0; i < quantidade_cadastrar; i++)
            {
                Console.Clear();
                Produto produto = new Produto();

                Console.WriteLine($"========== PRODUTO {i + 1} DE {quantidade_cadastrar} ==========");

                Console.Write("\nNome do produto: ");
                produto.Nome = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(produto.Nome) || int.TryParse(produto.Nome, out _))
                {
                    Console.Write("Nome inválido. Digite novamente: ");
                    produto.Nome = Console.ReadLine();
                }

                produto.Nome = produto.Nome.Trim();

                int quantidade;

                Console.Write("Quantidade inicial: ");

                while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade < 0)
                {
                    Console.Write("Quantidade inválida. Digite zero ou um número positivo: ");
                }

                produto.Quantidade = quantidade;

                decimal preco;

                Console.Write("Preço unitário: R$ ");

                while (!decimal.TryParse(Console.ReadLine(), out preco) || preco < 0)
                {
                    Console.Write("Preço inválido. Digite um valor maior ou igual a zero: ");
                }

                produto.Preco = preco;

                produtos.Add(produto);
            }

            Console.WriteLine("\n[OK] Cadastro concluído com sucesso!");
            Console.Write("Pressione Enter para voltar ao menu...");
            Console.ReadLine();
        }

        static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("========== PRODUTOS CADASTRADOS ==========");

            foreach (Produto produto in produtos)
            {
                Console.WriteLine($"\nProduto: {produto.Nome}");
                Console.WriteLine($"Quantidade: {produto.Quantidade} | Preço unitário: {produto.Preco:C2}");

                decimal estoque_produto = produto.Quantidade * produto.Preco;

                Console.WriteLine($"Valor em estoque: {estoque_produto:C2}");
                Console.WriteLine("-----------------------------------------");
            }

            Console.Write("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }

        static void ProcurarProduto()
        {
            bool invalido = true;

            Console.Clear();
            Console.WriteLine("========== PESQUISAR PRODUTO ==========");
            Console.Write("\nDigite o nome do produto: ");

            while (invalido)
            {
                string produto_pesquisado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(produto_pesquisado))
                {
                    Console.Write("Nome inválido. Digite novamente: ");
                }
                else
                {
                    var encontrados = produtos.Where(p => p.Nome != null && p.Nome.ToLower().Contains(produto_pesquisado.ToLower())).ToList();

                    if (encontrados.Count == 0)
                    {
                        Console.Write("Nenhum produto encontrado. Tente novamente: ");
                    }
                    else
                    {
                        foreach (var produto in encontrados)
                        {
                            Console.WriteLine($"\nProduto: {produto.Nome}");
                            Console.WriteLine($"Quantidade: {produto.Quantidade} | Preço unitário: {produto.Preco:C2}");
                            Console.WriteLine("-----------------------------------------");
                        }

                        invalido = false;
                    }
                }
            }

            Console.Write("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }

        static void AtualizarQuantidade()
        {
            bool encontrado = true;

            Console.Clear();

            while (encontrado)
            {
                Console.WriteLine("========== ATUALIZAR ESTOQUE ==========");
                Console.Write("\nNome do produto: ");

                string produtoAtualizar = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(produtoAtualizar))
                {
                    Console.WriteLine("Nome inválido. Tente novamente.");
                }
                else
                {
                    var produtoEncontrado = produtos.Where(p => p != null && p.Nome != null && p.Nome.ToLower().Contains(produtoAtualizar.ToLower())).ToList();

                    if (produtoEncontrado.Count == 0)
                    {
                        Console.WriteLine($"\nNenhum produto encontrado para: {produtoAtualizar}");
                    }
                    else
                    {
                        foreach (var produto in produtoEncontrado)
                        {
                            bool valido = true;
                            int quantidade_atualizada;

                            Console.Clear();

                            Console.WriteLine("========== PRODUTO ENCONTRADO ==========");

                            Console.WriteLine($"\nProduto: {produto.Nome}");
                            Console.WriteLine($"Estoque atual: {produto.Quantidade} unidade(s)");

                            encontrado = false;

                            while (valido)
                            {
                                Console.Write("\nO que deseja fazer? (adicionar/subtrair): ");
                                string resposta = Console.ReadLine();

                                if (resposta?.Trim().ToLower() == "adicionar")
                                {
                                    bool menorZero = true;

                                    while (menorZero)
                                    {
                                        Console.Write("\nQuantidade a adicionar: ");

                                        while (!int.TryParse(Console.ReadLine(), out quantidade_atualizada))
                                        {
                                            Console.Write("Entrada inválida. Digite um número inteiro: ");
                                        }

                                        if (quantidade_atualizada <= 0)
                                        {
                                            Console.WriteLine("[ERRO] A quantidade deve ser maior que zero.");
                                        }
                                        else
                                        {
                                            produto.Quantidade += quantidade_atualizada;

                                            Console.Clear();

                                            Console.WriteLine("========== ESTOQUE ATUALIZADO ==========");
                                            Console.WriteLine($"Produto: {produto.Nome}");
                                            Console.WriteLine($"Nova quantidade: {produto.Quantidade} unidade(s)");

                                            menorZero = false;
                                            valido = false;
                                        }
                                    }
                                }
                                else if (resposta?.Trim().ToLower() == "subtrair")
                                {
                                    bool maiorZero = true;

                                    while (maiorZero)
                                    {
                                        Console.Write("\nQuantidade a retirar: ");

                                        while (!int.TryParse(Console.ReadLine(), out quantidade_atualizada))
                                        {
                                            Console.Write("Entrada inválida. Digite um número inteiro: ");
                                        }

                                        if (quantidade_atualizada > produto.Quantidade)
                                        {
                                            Console.WriteLine($"[ERRO] Estoque insuficiente. Disponível: {produto.Quantidade} unidade(s).");
                                        }
                                        else if (quantidade_atualizada <= 0)
                                        {
                                            Console.WriteLine("[ERRO] A quantidade deve ser maior que zero.");
                                        }
                                        else
                                        {
                                            produto.Quantidade -= quantidade_atualizada;

                                            Console.WriteLine("========== ESTOQUE ATUALIZADO ==========");
                                            Console.WriteLine($"Produto: {produto.Nome}");
                                            Console.WriteLine($"Nova quantidade: {produto.Quantidade} unidade(s)");

                                            maiorZero = false;
                                            valido = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Opção inválida. Digite adicionar ou subtrair.");
                                }
                            }
                        }
                    }
                }
            }

            Console.Write("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }

        static decimal ValorTotal(List<Produto> produtos)
        {
            decimal total = 0;

            foreach (Produto produto in produtos)
            {
                total += produto.Preco * produto.Quantidade;
            }

            return total;
        }

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                int opcao;

                Console.Clear();

                Console.WriteLine("========== SISTEMA DE ESTOQUE ==========");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1 - Cadastrar produto");
                Console.WriteLine("2 - Listar produtos");
                Console.WriteLine("3 - Pesquisar produto");
                Console.WriteLine("4 - Consultar valor total do estoque");
                Console.WriteLine("5 - Atualizar estoque");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("----------------------------------------");

                Console.Write("\nOpção: ");

                while (!int.TryParse(Console.ReadLine(), out opcao)
                       || opcao < 0 || opcao > 5)
                {
                    Console.Write("Opção inválida. Digite um número de 0 a 5: ");
                }

                switch (opcao)
                {
                    case 1:
                        CadastrarProdutos();
                        break;

                    case 2:
                        ListarProdutos();
                        break;

                    case 3:
                        ProcurarProduto();
                        break;

                    case 4:
                        Console.Clear();

                        Console.WriteLine("========== VALOR TOTAL DO ESTOQUE ==========");
                        Console.WriteLine($"\nTotal: {ValorTotal(produtos):C2}");

                        Console.Write("\nPressione Enter para voltar ao menu...");
                        Console.ReadLine();
                        break;

                    case 5:
                        AtualizarQuantidade();
                        break;

                    case 0:
                        executando = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
