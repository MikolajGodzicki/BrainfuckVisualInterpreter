using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal class Lexer {
        string _input;
        char _currentChar;

        public List<Tokens> tokens { get; private set; }

        public Lexer(string input) {
            _input = input;

            tokens = new List<Tokens>();

            GetTokens();
        }

        void GetTokens() {
            for (int i = 0; i < _input.Length; i++) {
                _currentChar = _input[i];
                Transform();
            }
        }

        private void Transform() {
            switch (_currentChar) {
                case '>':
                    tokens.Add(Tokens.MoveRight);
                    break;
                case '<':
                    tokens.Add(Tokens.MoveLeft);
                    break;
                case '+':
                    tokens.Add(Tokens.Add);
                    break;
                case '-':
                    tokens.Add(Tokens.Subtract);
                    break;
                case '.':
                    tokens.Add(Tokens.Out);
                    break;
                case ',':
                    tokens.Add(Tokens.In);
                    break;
                case '[':
                    tokens.Add(Tokens.LeftWhile);
                    break;
                case ']':
                    tokens.Add(Tokens.RightWhile);
                    break;
                default:
                    tokens.Add(Tokens.None);
                    break;
            }
        }
    }
}
