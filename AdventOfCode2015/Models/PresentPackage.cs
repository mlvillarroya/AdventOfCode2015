using System;
using System.Buffers;
using System.Linq;

namespace AdventOfCode2015.Models
{
    public class PresentPackage
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private int Surface1 => Length * Width;
        private int Surface2 => Width * Height;
        private int Surface3 => Length * Height;
        private int MinSize
        {
            get
            {
                var sizes = new[] {this.Width, this.Height, this.Length};
                Array.Sort(sizes);
                return sizes[0];
            }
        }
        private int MediumSize
        {
            get
            {
                var sizes = new[] {this.Width, this.Height, this.Length};
                Array.Sort(sizes);
                return sizes[1];
            }
        }
        private int MinSurface => Math.Min(Surface1, Math.Min(Surface2, Surface3));
        public int ComputeNeededPaper()
        {
            return 2 * Surface1 + 2 * Surface2 + 2 * Surface3 + MinSurface;
        }

        public int ComputeNeededRibbon()
        {
            return 2 * MinSize + 2 * MediumSize + Width * Length * Height;
        }
    }
}