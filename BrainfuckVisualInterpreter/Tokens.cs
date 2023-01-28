using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckVisualInterpreter {
    internal enum Tokens {
        None,

        MoveRight,
        MoveLeft,
        Add,
        Subtract,
        Out,
        In,
        LeftWhile,
        RightWhile,
    }
}
