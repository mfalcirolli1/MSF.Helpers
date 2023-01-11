using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.AccessModifiers
{
    public  class AccessDemo
    {
        // Acesso em qualquer assembly e/ou classe
        public void PublicDemo()
        {

        }

        // Acesso apenas na classe em questão
        private void PrivateDemo()
        {

        }

        // Acesso apenas no projeto (assembly) em questão
        internal void InternalDemo()
        {

        }

        // Permite ser chamada da classe onde é declarado (AccessDemo) OU
        // De qualquer outra classe de QUALQUER assembly que herde dela. 
        protected void ProtectedDemo()
        {

        }

        // Permite ser chamada da classe onde é declarado (AccessDemo) OU
        // Apenas de classes do MESMO assembly que herde dela.
        private protected void PrivateProtectedDemo()
        {

        }

        // Permite ser chamada da classe onde é declarado (AccessDemo) OU
        // De qualquer outra classe de QUALQUER assembly que herde dela.
        protected internal void ProtectedInternal()
        {

        }
    }
}
