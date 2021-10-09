namespace AdventOfCode2015.Models
{
    public class Wire
    {
        private readonly string _code;
        private int _signal;

        public Wire(string code, int signal)
        {
            _code = code;
            _signal = signal;
        }

        public void SetValue(int value)
        {
            _signal = value;
        }

        public int GetValue()
        {
            return _signal;
        }

        public string GetCode()
        {
            return _code;
        }
        
    }
}