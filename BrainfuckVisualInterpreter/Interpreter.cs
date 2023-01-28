using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal class Interpreter {
        private const uint memorySize = 8;
        List<Cell> cells = new List<Cell>((int)memorySize);
        public string input { get; private set; } = String.Empty;
        Lexer? lexer;
        private List<Tokens> tokens = new List<Tokens>();
        private int currentCellPosition = 0;

        public Interpreter() {
            Generate();
        }

        public void Show() {
            Render();
            input = GetInput();

            lexer = new Lexer(input);
            tokens = lexer.tokens;

            Interpret();
        }

        private void Generate() {
            for (int i = 0; i < memorySize; i++) {
                cells.Add(new Cell(i, 0));
            }
        }

        private void Render() {
            Console.Clear();

            foreach (Cell cell in cells) {
                Console.Write(cell.ToString());
            }

            Console.WriteLine();
        }

        private string GetInput() {
            Console.WriteLine("Insert your code: ");
            string? input = Console.ReadLine();
            return input == null ? String.Empty : input;
        }

        private void Interpret() {
            foreach (Tokens token in tokens) {
                if (token == Tokens.MoveRight) {
                    if (currentCellPosition < memorySize)
                        currentCellPosition++;
                }

                else if (token == Tokens.MoveLeft) {
                    if (currentCellPosition > 0)
                        currentCellPosition--;
                }

                else if (token == Tokens.Add) {
                    cells[currentCellPosition].Value++;
                }

                else if (token == Tokens.Subtract) {
                    cells[currentCellPosition].Value--;
                }

                else if (token == Tokens.Out) {
                    Console.WriteLine((char) cells[currentCellPosition].Value); 
                }
            }
        }
    }
}
