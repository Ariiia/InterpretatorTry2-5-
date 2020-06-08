using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator
{
 public   class Parse
    {
        public enum TokenType
        {
            Plus,
            Minus,
            Multiply,
            Divide,
            Number, None, LeftParenthesis, RightParenthesis


        }
        private Token currentToken;
        private int currentPos;
        private int charCount;
        private char currentChar;
        public string Text { get; private set; }
        public Parse(string text)
        {
            this.Text = string.IsNullOrEmpty(text) ? string.Empty : text;
            this.charCount = this.Text.Length;
            this.currentToken = Token.None();

            this.currentPos = -1;
            this.Advance();
        }
        internal Expression Parce()
        {
            this.NextToken();
            Expression node = this.GrabExpr();
            this.ExpectToken(TokenType.None);
            return node;
        }

        private Token ExpectToken(TokenType tokenType)
        {
            if (this.currentToken.Type == tokenType)
            {
                return this.currentToken;
            }
         
        }

    
    }
}
