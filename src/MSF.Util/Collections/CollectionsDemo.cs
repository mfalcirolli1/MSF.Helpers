using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.Collections
{
    public static class CollectionsDemo
    {
        public static void FastestsCollections()
        {
            // HashSet é muito similar ao List<T>
            // A única diferença é que não podem haver registros duplicados nessa collection
            HashSet<string> hs = new HashSet<string>();

            // Como no HashSet, a Key é única e só aparece uma vez no dicionário.
            Dictionary<int, string> dict = new Dictionary<int, string>();

            // Esta é a única lista que não armazena dados em uma matriz interna.
            // Como nenhuma matriz precisa ser criada ou expandida em caso de estouro de capacidade,
            // LinkedList<T> armazena grandes quantidades de dados com muita eficiência.
            // Esta lista é imbatível em termos de requisitos de memória e esforço ao adicionar um elemento.
            LinkedList<string> linked = new LinkedList<string>();
            linked.AddLast("1");
            linked.AddLast("2");
            linked.AddLast("3");
            linked.AddFirst("4");
            linked.AddBefore(linked.AddLast("6"), "5");
            linked.AddAfter(linked.AddLast("7"), "8");

            // Queue usa o princípio FIFO. O valores de entrada são retirados na ordem em que foram adicionados
            // A vantagem é que é fácil remover a primeira entrada da fila.
            // Em comparação com a List<T>, quando remove-se a primeira entrada de uma lista, deve mover todas as entradas restantes, oque exige muito desempenho
            Queue<int> q = new Queue<int>();
            q.Enqueue(99);
            q.Enqueue(4);
            q.Enqueue(31);
            q.Dequeue();
            var item = q.FirstOrDefault(x => x == 4);

            // Stack usa o princípio FILO. O último valor adicionado é o primeiro a ser removido/selecionado.
            // Como na List<T>, também é armazenado em um array
            Stack<int> stack = new Stack<int>();
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Pop();
            stack.Peek();
        }
    }
}
