using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal class Cell {
        public Int64 Index { get; set; }
        public Int64 Value { get; set; }

        public Cell(Int64 index, Int64 value) {
            Index = index;
            Value = value;
        }

        public override string ToString() {
            return $"[{Index}:{Value}]";
        }
    }
}
