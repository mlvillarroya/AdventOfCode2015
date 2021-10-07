namespace AdventOfCode2015.Models
{
    public class SantaPosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public SantaPosition()
        {
            x = 0;
            y = 0;
        }
        public override string ToString()
        {
            return $"{x}-{y}";
        }
        public void ChangePosition(char change)
        {
            switch (change)
            {
                case 'v':
                    y--;
                    break;
                case '^':
                    y++;
                    break;
                case '<':
                    x--;
                    break;
                case '>':
                    x++;
                    break;
            }
        }
    }
}