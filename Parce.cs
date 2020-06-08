using System;
using System.Collections.Generic;
using System.Text;

namespace Interpretator
{
 public   class Parse
    {
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
        public enum Actions
        {
            Plus,
                Minus,
                Multiply,
                Divide,
                Number, None, LeftParenthesis, RightParenthesis


        }
    }
}
