using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.Write("Digite a quanidade de produtos a ser cadastrado: ");
            quantidade_cadastrar = int.Parse(Console.ReadLine());

            for (int i = 0; i < quantidade_cadastrar; i++)
            {
                Console.Clear();
                Produto produto = new Produto();

                Console.WriteLine($"Produto {i + 1}: ");

                Console.Write("\nProduto: ");
                produto.Nome = Console.ReadLine();

                Console.Write("Quantidade: ");
                produto.Quantidade = int.Parse(Console.ReadLine());

                Console.Write("Preço: ");
                produto.Preco = decimal.Parse(Console.ReadLine());

                produtos.Add(produto);
            }

            Console.WriteLine("\nCadastro(s) Concluído! Pressione qualquer tecla para voltar ao menu!");
            Console.ReadLine();
        }

        static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("Lista dos produtos: ");

            foreach (Produto produto in produtos)
            {
                Console.WriteLine($"\nProduto: {produto.Nome} | Quantidade: {produto.Quantidade} | Preço: {produto.Preco}");
                decimal estoque_produto = produto.Quantidade * produto.Preco;
                Console.WriteLine($"Valor em estoque: {estoque_produto}");
            }

            Console.Write("\nPressione qualquer tecla para voltar ao menu!");
            Console.ReadLine();
        }

        static void ProcurarProduto()
        {
            bool invalido = true;
            Console.Clear();
            Console.Write("Digite o nome do produto: ");

            while (invalido)
            {
                string produto_pesquisado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(produto_pesquisado))
                {
                    Console.WriteLine("Por favor, digite um produto válido!");
                }
                else
                {
                    // Procurar produto na lista
                    var encontrados = produtos.Where(p => p.Nome != null && p.Nome.ToLower().Contains(produto_pesquisado.ToLower())).ToList();
    
                    if (encontrados.Count == 0)
                    {
                        Console.WriteLine("Produto não encontrado. Tente novamente:");
                    }
                    else
                    {
                        foreach (var produto in encontrados)
                        {
                            Console.WriteLine($"\nProduto: {produto.Nome} | Quantidade: {produto.Quantidade} | Preço: {produto.Preco}");
                        }
                        invalido = false;
                    }
                }

            }

            Console.Write("\nPressione qualquer tecla para voltar ao menu!");
            Console.ReadLine();

        }

        static void AtualizarQuantidade()
        {
            bool encontrado = true;

            Console.Clear();

            while (encontrado)
            {

                Console.Write("Digite o produto que deseja: ");
                string produtoAtualizar = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(produtoAtualizar))
                {
                    Console.WriteLine("Por favor, digite um produto válido!");
                }
                else
                {
                    var produtoEncontrado = produtos.Where(p => p != null && p.Nome != null && p.Nome.ToLower().Contains(produtoAtualizar.ToLower())).ToList();

                    if (produtoEncontrado.Count == 0)
                    {
                        Console.WriteLine($"Nenhum produto ({produtoAtualizar}) encontrado!");
                    }
                    else
                    {
                        foreach (var produto in produtoEncontrado)
                        {
                            bool valido = true;
                            int quantidade_atualizada;

                            Console.Clear();

                            Console.WriteLine("Produto encontrado!");

                            Console.WriteLine($"\nProduto: {produto.Nome} | Quantidade Atual: {produto.Quantidade}.");

                            encontrado = false;

                            while (valido)
                            {
                                Console.Write("\nDeseja adicionar ou subtrair a quantidade? ");
                                string resposta = Console.ReadLine();


                                if (resposta.ToLower().Contains("adi"))
                                {
                                    bool menorZero = true;


                                    while (menorZero)
                                    {
                                        Console.Write("\nDigite a quantidade ser adicionada produto: ");
                                        quantidade_atualizada = int.Parse(Console.ReadLine());
                                        
                                        if (quantidade_atualizada <= 0)
                                        {
                                            Console.WriteLine("Digite um valor maior que 0!");
                                        }
                                        else
                                        {
                                            produto.Quantidade += quantidade_atualizada;

                                            Console.Clear();
                                            Console.WriteLine("Quantidade Atualizada!");
                                            Console.WriteLine($"Produto: {produto.Nome} | Quantidade Atual: {produto.Quantidade}.");

                                            menorZero = false;
                                            valido = false;
                                        }
                                    }
              
                                }
                                else if (resposta.ToLower().Contains("sub"))
                                {
                                    bool maiorZero = true;

                                    while (maiorZero)
                                    {
                                        Console.WriteLine("\nDigite a quantidade a ser retirada produto: ");
                                        quantidade_atualizada = int.Parse(Console.ReadLine());

                                        if (quantidade_atualizada > produto.Quantidade)
                                        {
                                            Console.WriteLine("Estoque insuficiente!");
                                        }
                                        else if (quantidade_atualizada <= 0)
                                        {
                                            Console.WriteLine("Digite uma quantidade maior que zero!");
                                        }
                                        else
                                        {
                                            produto.Quantidade -= quantidade_atualizada;

                                            Console.WriteLine("Quantidade Atualizada!");
                                            Console.WriteLine($"Produto: {produto.Nome} | Quantidade Atual: {produto.Quantidade}.");

                                            maiorZero = false;
                                            valido = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Resposta inválida!");

                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nPressione Enter pra voltar Menu");
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
                Console.Clear();

                Console.WriteLine("===== Sistema de Estoque =====");
                Console.WriteLine("\n1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Pesquisar Produto");
                Console.WriteLine("4 - Valor total do estoque");
                Console.WriteLine("5 - Atualizar estoque");
                Console.WriteLine("0 - Sair");

                Console.Write("\nOpção: ");

                var opcao = int.Parse(Console.ReadLine());

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
                        
                        Console.Write($"Valor total em estoque {ValorTotal(produtos)}");

                        Console.Write("\nPressione qualquer tecla para voltar ao menu!");
                        Console.ReadLine();
                        break;

                    case 5:
                        AtualizarQuantidade();
                        break;

                    case 0:
                        executando = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }

            }

            

        }
    }
}
