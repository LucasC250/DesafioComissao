using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DesafioComissao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lê o arquivo JSON
            string json = File.ReadAllText("vendas.json");

            // Converte o JSON para objetos C#
            DadosVendas dados = JsonConvert.DeserializeObject<DadosVendas>(json);

            // Guarda a comissão total de cada vendedor
            Dictionary<string, decimal> comissoes =
                new Dictionary<string, decimal>();

            // Percorre todas as vendas
            foreach (Venda venda in dados.Vendas)
            {
                decimal percentualComissao;

                // Abaixo de R$ 100,00 não gera comissão
                if (venda.Valor < 100)
                {
                    percentualComissao = 0;
                }
                // Abaixo de R$ 500,00 gera 1%
                else if (venda.Valor < 500)
                {
                    percentualComissao = 0.01m;
                }
                // A partir de R$ 500,00 gera 5%
                else
                {
                    percentualComissao = 0.05m;
                }

                // Calcula a comissão da venda
                decimal comissao = venda.Valor * percentualComissao;

                // Se o vendedor ainda não estiver no dicionário,
                // adiciona com comissão inicial de zero
                if (!comissoes.ContainsKey(venda.Vendedor))
                {
                    comissoes[venda.Vendedor] = 0;
                }

                // Soma a comissão ao total do vendedor
                comissoes[venda.Vendedor] += comissao;
            }

            // Exibe o resultado
            Console.WriteLine("COMISSÃO DOS VENDEDORES");
            Console.WriteLine("-----------------------");

            foreach (KeyValuePair<string, decimal> vendedor in comissoes)
            {
                Console.WriteLine(
                    "{0}: R$ {1:F2}",
                    vendedor.Key,
                    vendedor.Value
                );
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }

    public class Venda
    {
        public string Vendedor { get; set; }
        public decimal Valor { get; set; }
    }

    public class DadosVendas
    {
        public List<Venda> Vendas { get; set; }
    }
}