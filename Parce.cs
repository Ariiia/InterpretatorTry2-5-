using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Interpretator
{

    internal enum TokenType
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Number, None, LeftParenthesis, RightParenthesis


    }
    internal class Parser
    {

        public string Text { get; private set; }

        private Token currentToken;
        private int currentPos;
        private int charCount;
        private char currentChar;
        private void Advance()
        {
            this.currentPos += 1;

            if (this.currentPos < this.charCount)
            {
                this.currentChar = this.Text[this.currentPos];
            }
            else
            {
                this.currentChar = char.MinValue;
            }
        }

        public Parser(string text)
        {
            this.Text = string.IsNullOrEmpty(text) ? string.Empty : text;
            this.charCount = this.Text.Length;
            this.currentToken = Token.None();

            this.currentPos = -1;
            this.Advance();
        }
        internal Expression Parse()
        {
            this.NextToken();
            Expression node = this.GrabExpr();
            this.ExpectToken(TokenType.None);
            return node;
        }
        internal Expression Parse()
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
            else
            {
                throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.", this.currentPos, tokenType, this.currentToken.Type.ToString()));
            }
        }

        internal class UnaryOp : Expression
        {
            internal Token Op { get; private set; }
            internal Expression Node { get; private set; }

            public UnaryOp(Token op, Expression node)
            {
                this.Op = op;
                this.Node = node;
            }

     

        internal class BinOp : Expression
        {
            internal Token Op { get; private set; }
            internal Expression Left { get; private set; }
            internal Expression Right { get; private set; }

            public BinOp(Token op, Expression left, Expression right)
            {
                this.Op = op;
                this.Left = left;
                this.Right = right;
            }

            override public object Accept(INodeVisitor visitor)
            {
                return visitor.VisitBinOp(this.Op, this.Left, this.Right);
            }
        }

        public Expression GrabExpr()
        {
            Expression left = this.GrabTerm();

            while (this.currentToken.Type == TokenType.Plus
                || this.currentToken.Type == TokenType.Minus)
            {
                Token op = this.currentToken;
                this.NextToken();
                Expression right = this.GrabTerm();
                left = new BinOp(op, left, right);
            }

            return left;
        }

        private Expression GrabTerm()
        {
            Expression left = this.GrabFactor();

            while (this.currentToken.Type == TokenType.Multiply
                || this.currentToken.Type == TokenType.Divide)
            {
                Token op = this.currentToken;
                this.NextToken();
                Expression right = this.GrabFactor();
                left = new BinOp(op, left, right);
            }

            return left;
        }

        private Expression GrabFactor()
        {
            if (this.currentToken.Type == TokenType.Plus
                || this.currentToken.Type == TokenType.Minus)
            {
                Expression node = this.GrabUnaryExpr();
                return node;
            }
            else if (this.currentToken.Type == TokenType.LeftParenthesis)
            {
                Expression node = this.GrabBracketExpr();
                return node;
            }
            else
            {
                Token token = this.ExpectToken(TokenType.Number);
                this.NextToken();
                return new Num(token);
            }
        }

        private Expression GrabUnaryExpr()
        {
            Token op;

            if (this.currentToken.Type == TokenType.Plus)
            {
                op = this.ExpectToken(TokenType.Plus);
            }
            else
            {
                op = this.ExpectToken(TokenType.Minus);
            }

            this.NextToken();

            if (this.currentToken.Type == TokenType.Plus
                || this.currentToken.Type == TokenType.Minus)
            {
                Expression expr = this.GrabUnaryExpr();
                return new UnaryOp(op, expr);
            }
            else
            {
                Expression expr = this.GrabFactor();
                return new UnaryOp(op, expr);
            }
        }
        private void NextToken()
        {
            if (this.currentChar == char.MinValue)
            {
                this.currentToken = Token.None();
                return;
            }

            if (this.currentChar == ' ')
            {
                while (this.currentChar != char.MinValue && this.currentChar == ' ')
                {
                    this.Advance();
                }

                if (this.currentChar == char.MinValue)
                {
                    this.currentToken = Token.None();
                    return;
                }
            }

            if (this.currentChar == '+')
            {
                this.currentChar = new Token(TokenType.Plus, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar == '-')
            {
                this.currentChar = new Token(TokenType.Minus, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar == '*')
            {
                this.currentToken = new Token(TokenType.Multiply, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar == '/')
            {
                this.currentChar = new Token(TokenType.Divide, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar == '(')
            {
                this.currentChar = new Token(TokenType.LeftParenthesis, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar == ')')
            {
                this.currentToken = new Token(TokenType.RightParenthesis, this.currentChar.ToString());
                this.Advance();
                return;
            }

            if (this.currentChar >= '0' && this.currentChar <= '9')
            {
                string num = string.Empty;
                while (this.currentChar >= '0' && this.currentChar <= '9')
                {
                    num += this.currentChar.ToString();
                    this.Advance();
                }

                if (this.currentChar == '.')
                {
                    num += this.currentChar.ToString();
                    this.Advance();

                    if (this.currentChar >= '0' && this.currentChar <= '9')
                    {
                        while (this.currentChar >= '0' && this.currentChar <= '9')
                        {
                            num += this.currentChar.ToString();
                            this.Advance();
                        }
                    }
                    else
                    {
                        throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Unexpected symbol {1}.", this.currentPos, this.currentChar));
                    }
                }

                this.currentToken = new Token(TokenType.Number, num);
                return;
            }

            throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Unexpected symbol {1}.", this.currentPos, this.currentChar));
        }
        private  Token ExpectToken(TokenType tokenType)
        {
            if (this.currentToken.Type == tokenType)
            {
                return this.currentToken;
            }
            else
            {
                throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.", this.currentPos, tokenType, this.currentToken.Type.ToString()));
            }
        }

        
    } }

