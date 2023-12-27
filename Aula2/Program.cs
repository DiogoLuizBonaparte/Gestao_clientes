using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aula2
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
           public string nome;
           public int cpf;
           public string profissão;
        }

       static List<Cliente> clientes = new List<Cliente>();

        enum EMenu { Listagem = 1, Adicionar, Remover, Sair };
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while (!escolheuSair)
            {

                Console.WriteLine("Sistema de clientes - Bem-vindo!");
                Console.WriteLine("Digite uma opção");
                Console.WriteLine("1 - Listagem\n2 - Adicionar\n3 - Remover\n4 - Sair");
                EMenu opcao = (EMenu)int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case EMenu.Listagem:
                        Listagem();
                        break;
                    case EMenu.Adicionar:
                        Adicionar();
                        break;
                    case EMenu.Remover:
                        Remover();
                        break;
                    case EMenu.Sair:
                        Sair();
                        escolheuSair = true;
                        break;

                }
                Console.Clear();
            }

        }

       
        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                int i = 0;
                Console.WriteLine("Lista de clientes");
               
                foreach (Cliente cliente in clientes)
                {

                    Console.WriteLine($"ID do cliente: {i}");
                    Console.WriteLine($"Nome do cliente: {cliente.nome}");
                    Console.WriteLine($"CPD do cliente: {cliente.cpf}");
                    Console.WriteLine($"Profissão do cliente: {cliente.profissão}");
                    i++;
                    Console.WriteLine("===========================================");
                }               
            }
            else
            {
                Console.WriteLine("Não existe clientes cadastrados");
            }
            Console.ReadLine();
        }


        static void Adicionar()
        {
           Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente");
            Console.WriteLine("Digite o nome do cliente");
            cliente.nome = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente");
            cliente.cpf = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a profissão do cliente");
            cliente.profissão = Console.ReadLine();

            clientes.Add(cliente);

            Console.WriteLine("Você cadastrou um cliente. Aperte Enter para sair");
            Salvar();
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Você escolheu remover um cliente");
            Console.WriteLine("Digite o ID do cliente que deseja remover");
            int id = int.Parse(Console.ReadLine());
            

            if (id >= 0 && id < clientes.Count())
            {
                clientes.RemoveAt(id);
                Console.WriteLine($"O cliente: {id} foi removido");
                Salvar();

            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente");
               
            }

            Console.ReadLine();

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("Cadastro.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);
            stream.Close();
        }

        static void Carregar()
        {

            FileStream stream = new FileStream("Cadastro.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

               
            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();

        }

        static void Sair()
        {
            Console.WriteLine("Você escolheu sair. Até a próxima - Aperte Enter para sair");
            Console.ReadLine(); 

        }



    }
}
