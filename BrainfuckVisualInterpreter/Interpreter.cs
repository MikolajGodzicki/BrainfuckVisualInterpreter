using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal class Interpreter {
        private const uint memorySize = 16;
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

            Interpret(tokens);
            Console.WriteLine("Output: ");
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

            Console.Write($"Current cell: {currentCellPosition}");

            Console.WriteLine();
        }

        private string GetInput() {
            Console.WriteLine("Input: ");
            string? input = Console.ReadLine();
            return input == null ? String.Empty : input;
        }

        private void Interpret(IEnumerable<Tokens> tokens) {
            int idx = 0;
            foreach (Tokens token in tokens) {
                if (token == Tokens.MoveRight) {
                    MoveRight();
                }

                else if (token == Tokens.MoveLeft) {
                    MoveLeft();
                }

                else if (token == Tokens.Add) {
                    Add();
                }

                else if (token == Tokens.Subtract) {
                    Substract();
                }

                else if (token == Tokens.LeftWhile) {
                    While(idx);
                }

                else if (token == Tokens.Out) {
                    Out();
                }

                else if (token == Tokens.In) {
                    In();
                }

                idx++;
            }
        }

        private void MoveRight() {
            if (currentCellPosition < memorySize - 1)
                currentCellPosition++;
        }

        private void MoveLeft() {
            if (currentCellPosition > 0)
                currentCellPosition--;
        }

        private void Add() {
            cells[currentCellPosition].Value++;
        }

        private void Substract() {
            cells[currentCellPosition].Value--;
        }

        private void While(int idx) {
            List<Tokens> tokensToDo = new List<Tokens>();
            int startIdx = idx;
            int endIdx = startIdx;

            int tokensCount = tokens.Count;

            //Get Index of right parenthesis
            while (tokens[endIdx] != Tokens.RightWhile && endIdx < tokensCount - 1) {
                endIdx++;
            }

            //Add Instructions
            for (int i = startIdx + 1; i < endIdx; i++) {
                tokensToDo.Add(tokens[i]);
            }
            
            //Do instructions while value of selected cell is greater than 0
            while (cells[currentCellPosition].Value > 1) {
                Interpret(tokensToDo);
            }
        }

        private void Out() {
            Console.WriteLine();
            Console.WriteLine((char)cells[currentCellPosition].Value);
            Console.Write("Click [Enter] to continue.");
            Console.ReadLine();
        }

        private void In() {
            Console.Write("Write your character: ");
            char key = Console.ReadKey().KeyChar;
            cells[currentCellPosition].Value = key;
        }
    }
}
