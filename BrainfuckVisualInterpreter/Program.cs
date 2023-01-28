namespace BrainfuckVisualInterpreter {
    internal class Program {
        static void Main(string[] args) {
            Interpreter interpreter = new Interpreter();
            while (true) {
                interpreter.Show();
            }
        }
    }
}