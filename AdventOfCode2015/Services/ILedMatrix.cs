namespace AdventOfCode2015.Services
{
    public interface ILedMatrix
    {
        public void ReadInstruction(string text, int option);
        public int TotalLedOn();
        public int TotalBrightness();

    }
}