using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>(); 
        private static int opcao = 0;

        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                                                        ");
            Console.WriteLine("               Digite a Opção desejada :                ");
            Console.WriteLine("             =============================              ");
            Console.WriteLine("               1 - Criar Conta                          ");
            Console.WriteLine("             =============================              ");
            Console.WriteLine("               2 - Entrar com CPF e Senha               ");
            Console.WriteLine("             =============================              "); 
            opcao = int.Parse(Console.ReadLine());



            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                default:
                    Console.WriteLine("Valor digitado Invalido!");
                    break;

            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                        ");
            Console.WriteLine("               Digite seu Nome :                        ");
            string nome = Console.ReadLine();
            Console.WriteLine("             =============================              ");
            Console.WriteLine("               Digite se CPF:                           ");
            string cfp = Console.ReadLine();
            Console.WriteLine("             =============================              ");
            Console.WriteLine("               Digite sua senha:                        ");
            string senha = Console.ReadLine();
            Console.WriteLine("             =============================              ");

            // Criar Conta

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();
            pessoa.SetNome(nome);
            pessoa.SetCPF(cfp);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            

            Console.WriteLine("             Conta cadastrada com sucesso.              ");
            Console.WriteLine("             =============================              ");

            Thread.Sleep(2000); // espera 1 segundo para aparecer a tela de conta logada.

            TelaContaLogada(pessoa);
           
            

        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                        ");
            Console.WriteLine("               Digite o CPF:                            ");
            string cpf = Console.ReadLine();
            Console.WriteLine("             =============================              ");
            Console.WriteLine("               Digite sua Senha:                        ");
            string senha = Console.ReadLine();
            Console.WriteLine("             =============================              ");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);
            if (pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("             Pessoa não cadastrada. Tente novamente...                           ");
                Console.WriteLine("             =============================                      ");

                Console.WriteLine();
                Console.WriteLine();
                Thread.Sleep(2000);
                TelaPrincipal();
            }


        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string mensagemTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} | Agencia: {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()} ";
            Console.WriteLine("");
            Console.WriteLine($"                 Seja bem vindo(a), {mensagemTelaBemVindo}");
            Console.WriteLine("");

        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("              Digite a Opção desejada:                          ");
            Console.WriteLine("             =============================                      ");
            Console.WriteLine("              1 - Deposito.                                     ");
            Console.WriteLine("              2 - Saque.                                        ");
            Console.WriteLine("              3 - Saldo.                                        ");
            Console.WriteLine("              4 - Extrato.                                      ");
            Console.WriteLine("             =============================                      ");
            Console.WriteLine("              5 - Sair.                                         ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("              Opcão Invalida.                                   ");
                    Console.WriteLine("             =============================                      ");
                    break;
            }
        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                     Digite o valor do Deposito:                          ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("                   ==============================                          ");

            pessoa.Conta.Depositar(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                            ");
            Console.WriteLine("                                                                            ");
            Console.WriteLine("                     Deposito Realizado com sucesso.                        ");
            Console.WriteLine("                    ================================                        ");
            Console.WriteLine("                                                                            ");

            OpcaoVoltarLogado(pessoa);



        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                     Entre com uma opcao abaixo.                            ");
            Console.WriteLine("                    ================================                        ");
            Console.WriteLine("                     1 - Voltar para minha conta.                           ");
            Console.WriteLine("                     2 - Sair.                                              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                TelaContaLogada(pessoa);

            }
            else
            {
                TelaPrincipal();
            }
        }

        private static void OpcaoVoltarDeslogado(Pessoa pessoa)
        {
            Console.WriteLine("                     Entre com uma opcao abaixo.                            ");
            Console.WriteLine("                    ================================                        ");
            Console.WriteLine("                     1 - Voltar para o menu principal.                      ");
            Console.WriteLine("                     2 - Sair.                                              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                TelaPrincipal();

            }
            else
            {
                Console.WriteLine("                  Opcao Invalida.                                        ");
                Console.WriteLine("                  ===========================                            ");
            }
        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                     Digite o valor do Saque :                             ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("                   ==============================                          ");

            bool okSaque = pessoa.Conta.Sacar(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                            ");
            Console.WriteLine("                                                                            ");

            if (okSaque)
            {
                Console.WriteLine("                     Saque Realizado com sucesso.                           ");
                Console.WriteLine("                    ================================                        ");
            }
            else
            {
                Console.WriteLine("                     Saldo insuficiente.                                    ");
                Console.WriteLine("                    ================================                        ");
            }

            Console.WriteLine("                                                                            ");

            OpcaoVoltarLogado(pessoa);



        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine($"            Seu Saldo é :  {pessoa.Conta.ConsultarSaldo()} ");
            Console.WriteLine("             =============================                  ");
            Console.WriteLine("                                                            ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any())
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                            ");
                    Console.WriteLine($"                       Data da Movimentacao: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"                       Tipo de Movimentacao: {extrato.Descricao}");
                    Console.WriteLine($"                       VALOR: {extrato.Valor}                 ");
                    Console.WriteLine("                         ===================                ");
                }


                Console.WriteLine("                                                            ");
                Console.WriteLine("                                                            ");
                Console.WriteLine($"                        SUB VALOR: {total}                 ");
                Console.WriteLine("                         ===================                ");


            }
            else
            {
                Console.WriteLine("                         Voce nao possui movimentacoes para exibir.");
            }

            Console.WriteLine("");
            Console.WriteLine(""); 
            Console.WriteLine("");
            OpcaoVoltarLogado(pessoa);
        }
    }
}
