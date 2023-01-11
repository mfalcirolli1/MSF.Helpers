using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Dynamic
{
    public class Dynamic
    {
        // Dynamic apresenta o benefício de poder se transformar em qualquer tipo de variável a qualquer momento durante a execução da aplicação.
        // Entretando, não tem intellisense (não difere letras maiúsculas e minículas para atributos).
        // Tipos dinamicos aceitam quaisquer atributos (se foram setados como objetos) mesmo que esses atributos nao existam.

        // O VS 'desiste' da variável e deixa a aplicação fazer o que bem entender com a tipagem dela.
        // Camufla bugs que só poderão ser encontrados em tempo de execução.

        // DYNAMIC = NON STATIC TYPED & WEAK TYPED
        // VAR = STATIC TYPED & STRONG TYPED

        // Quando Dynamic é preciso?
        // Quando se está usando coisas fora do C# e conversando com eles (Ex: outras linguagem de programação)

        public static void DynamicMethod()
        {
            dynamic testDynamic = string.Empty;
            var testVar = "Teste";
            Console.WriteLine(testVar);
        }
    }
}
