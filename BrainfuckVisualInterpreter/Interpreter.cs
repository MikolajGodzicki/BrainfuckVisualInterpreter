using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal class Interpreter {
        private const uint memorySize = 64;
        List<Cell> cells = new List<Cell>((int)memorySize);
        public string input { get; private set; } = String.Empty;
        Lexer? lexer;
        private List<Tokens> tokens = new List<Tokens>();
        private int currentCellPosition = 0;
        string memory = "";

        public Interpreter() {
            GenerateMemory();
        }

        public void Show() {
            ClearMemory();
            Render();
            input = GetInput();

            lexer = new Lexer(input);
            tokens = lexer.tokens;

            Interpret();
            Render();
            WriteOutput();
        }

        private void GenerateMemory() {
            for (int i = 0; i < memorySize; i++) {
                cells.Add(new Cell(i, 0));
            }
        }

        private void ClearMemory() {
            for (int i = 0; i < memorySize; i++) {
                cells[i].Value = 0;
            }
            currentCellPosition = 0;
            memory = "";
        }

        private void Render() {
            Console.Clear();

            RenderCells();
            
            Console.WriteLine();

            Console.Write($"Current cell: {currentCellPosition}");

            Console.WriteLine();
        }

        private void RenderCells() {
            foreach (Cell cell in cells) {
                Console.Write(cell.ToString());
            }
        }

        private string GetInput() {
            Console.WriteLine("Input: ");
            string? input = Console.ReadLine();
            return input == null ? String.Empty : input;
        }

        private void Interpret() {
            int idx = 0;
            for (int i = 0; i < tokens.Count; ++i) {
                if (tokens[i] == Tokens.MoveRight) {
                    MoveRight();
                }

                else if (tokens[i] == Tokens.MoveLeft) {
                    MoveLeft();
                }

                else if (tokens[i] == Tokens.Add) {
                    Add();
                }

                else if (tokens[i] == Tokens.Subtract) {
                    Substract();
                }

                else if (tokens[i] == Tokens.LeftWhile) {
                    LeftWhile(ref i);
                }

                else if (tokens[i] == Tokens.RightWhile) {
                    RightWhile(ref i);
                }

                else if (tokens[i] == Tokens.Out) {
                    Out();
                }

                else if (tokens[i] == Tokens.In) {
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

        private void LeftWhile(ref int idx) {
            if (cells[currentCellPosition].Value == 0) {
                int skip = 0;
                int ptr = idx + 1;
                while (input[ptr] != ']' || skip > 0) {
                    if (input[ptr] == '[') {
                        skip += 1;
                    }

                    else if (input[ptr] == ']') {
                        skip -= 1;
                    }

                    ptr += 1;
                    idx = ptr;
                }
            }
        }

        private void RightWhile(ref int idx) {
            if (cells[currentCellPosition].Value != 0) {
                int skip = 0;
                int ptr = idx - 1;
                while (input[ptr] != '[' || skip > 0) {
                    if (input[ptr] == ']') {
                        skip += 1;
                    }
                    else if (input[ptr] == '[') {
                        skip -= 1;
                    }
                    ptr -= 1;
                    idx = ptr;
                }
            }
        }

        private void Out() {
            memory += (char)cells[currentCellPosition].Value;
        }

        private void In() {
            Console.Write("Write your character: ");
            char key = Console.ReadKey().KeyChar;
            cells[currentCellPosition].Value = key;
        }

        public void WriteOutput() {
            Console.WriteLine("Output: ");
            Console.WriteLine(memory);
            Console.WriteLine("Click [Enter] to continue");
            Console.ReadKey();
        }
    }
}
