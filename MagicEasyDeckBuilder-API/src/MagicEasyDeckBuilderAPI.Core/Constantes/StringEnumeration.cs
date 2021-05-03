using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core.Constantes
{
    public abstract class StringEnumeration : IComparable
    {
        private readonly string _valor;

        protected StringEnumeration(string valor) => _valor = valor;

        public override string ToString()
        {
            return _valor;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StringEnumeration outroObj))
                return false;

            var typeMatch = GetType().Equals(obj.GetType());
            var valueMatch = _valor == outroObj._valor;

            return typeMatch && valueMatch;
        }

        public override int GetHashCode()
        {
            return _valor.GetHashCode();
        }
        public int CompareTo(object obj)
        {
            return _valor.CompareTo(((StringEnumeration)obj)._valor);
        }

        public static implicit operator string(StringEnumeration a) => a._valor;
    }
}
