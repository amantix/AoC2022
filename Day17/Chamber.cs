namespace Day17;

public class Chamber
{
    private List<int> heightLog = new();
    public IReadOnlyList<int> HeightLog => heightLog;
    public override string ToString()
    {
        var printLines = lines.Select(line => $"|{new string(line)}|").Append("|.......|").Reverse().ToArray();
        return string.Join("\n", printLines);
    }

    public int Height => lines.Count;

    public static Chamber Simulate(string jetPattern, int rocksCount)
    {
        var chamber = new Chamber();

        var rocks = GetRocks(rocksCount);

        var jetIndex = 0;
        {
            foreach (var rock in rocks)
            {
                //Debug.WriteLine(chamber);
                //Debug.WriteLine("-----------------------------------------------------");
                //allocate space
                chamber.lines.AddRange(Enumerable.Repeat(".......", 3 + rock.Height).Select(line => line.ToArray()));
                //place rock at the top
                rock.Column = 2;
                rock.Row = chamber.lines.Count - 1;

                //place rock
                var isPlaced = false;
                while (!isPlaced)
                {
                    //move horizontally
                    var direction = jetPattern[jetIndex % jetPattern.Length];
                    var shift = direction == '<' ? -1 : 1;
                    jetIndex++;
                    var canMoveHorizontally = true;
                    for (var row = 0; canMoveHorizontally && row < rock.Height; row++)
                    {
                        var chamberRow = rock.Row - row;
                        for (var column = 0; canMoveHorizontally && column < rock.Width; column++)
                        {
                            if (rock.Lines[row][column] != '#')
                            {
                                continue;
                            }

                            var chamberColumn = rock.Column + column + shift;
                            if (chamberColumn is >= 7 or < 0
                                || chamber.lines[chamberRow][chamberColumn] == '#')
                            {
                                canMoveHorizontally = false;
                            }
                        }
                    }

                    if (canMoveHorizontally)
                    {
                        rock.Column += shift;
                    }

                    //move vertically
                    for (var row = 0; !isPlaced && row < rock.Height; row++)
                    {
                        var chamberRow = rock.Row - row - 1;
                        for (var column = 0; !isPlaced && column < rock.Width; column++)
                        {
                            var chamberColumn = rock.Column + column;
                            if (chamberRow < 0 || rock.Lines[row][column] == '#' &&
                                chamber.lines[chamberRow][chamberColumn] == '#')
                            {
                                isPlaced = true;
                            }
                        }
                    }

                    if (!isPlaced)
                    {
                        rock.Row--;
                    }
                }

                //draw rock
                for (var row = 0; row < rock.Height; row++)
                {
                    var chamberRow = rock.Row - row;
                    for (var column = 0; column < rock.Width; column++)
                    {
                        var chamberColumn = rock.Column + column;
                        if (rock.Lines[row][column] == '#')
                        {
                            chamber.lines[chamberRow][chamberColumn] = '#';
                        }
                    }
                }


                chamber.RemoveEmptyLines();
                chamber.heightLog.Add(chamber.Height);
            }
        }

        return chamber;

        IEnumerable<char> GetJetSequence()
        {
            var i = 0;
            while (true)
            {
                yield return jetPattern[i];
                i = (i + 1) % jetPattern.Length;
            }
        }

        IEnumerable<Rock> GetRocks(int rocksCount)
        {
            var shapes = Enum.GetValues<Shape>().ToArray();
            return Enumerable.Range(0, rocksCount).Select(i => new Rock(shapes[i % shapes.Length]));
        }
    }

    private void RemoveEmptyLines()
    {
        while (lines.Count > 0 && lines[^1].All(item => item == '.'))
        {
            lines.RemoveAt(lines.Count - 1);
        }
    }

    private List<char[]> lines = new();

    internal enum Shape
    {
        HLine,
        Cross,
        Angle,
        VLine,
        Square
    };

    internal class Rock
    {
        internal Rock(Shape shape)
        {
            var lines = shape switch
            {
                Shape.HLine => new[] { "####" },
                Shape.Cross => new[] { ".#.", "###", ".#." },
                Shape.Angle => new[] { "..#", "..#", "###" },
                Shape.VLine => new[] { "#", "#", "#", "#" },
                Shape.Square => new[] { "##", "##" },
                _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, "Unsupported rock shape")
            };
            parts = lines.Select(line => line.ToArray()).ToArray();
            Column = 2;
            Shape = shape;
        }

        private Shape Shape { get; }

        public char[][] Lines => parts;

        public int Row;
        public int Column;
        public int Width => parts[0].Length;
        public int Height => parts.Length;
        private char[][] parts;
    }
}