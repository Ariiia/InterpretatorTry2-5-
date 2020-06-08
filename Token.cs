using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator
{
  internal  class Token
    {
         TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
        internal static Token None()
        {
            return new Token(TokenType.None, "");
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
