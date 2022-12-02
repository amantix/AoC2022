namespace Day2;

public static class RockPaperScissors
{
    public enum Moves
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public enum Outcomes
    {
        Lose = 1,
        Draw = 2,
        Win = 3
    }

    public readonly record struct Round(Moves OpponentMove, Moves PlayerMove)
    {
        private Outcomes Outcome =>
            (OpponentMove, PlayerMove) switch
            {
                _ when OpponentMove == PlayerMove => Outcomes.Draw,
                (Moves.Rock, Moves.Scissors) => Outcomes.Lose,
                _ when OpponentMove < PlayerMove => Outcomes.Win,
                (Moves.Scissors, Moves.Rock) => Outcomes.Win,
                _ => Outcomes.Lose
            };

        public int Score => (int)PlayerMove + Outcome.CountScore();
    }

    public static Round ParseRound<T>(string line) where T : struct
    {
        var opponentMove = (Moves)(line[0] - 'A' + 1);
        return typeof(T) switch
        {
            { } move when move == typeof(Moves)
                => new Round(opponentMove, (Moves)(line[2] - 'X' + 1)),
            { } outcome when outcome == typeof(Outcomes)
                => new Round(opponentMove, GetMove(opponentMove, (Outcomes)(line[2] - 'X' + 1))),
            _ => throw new ArgumentOutOfRangeException($"Unsupported input type {typeof(T)}")
        };
    }

    private static int CountScore(this Outcomes outcome) =>
        outcome switch
        {
            Outcomes.Draw => 3,
            Outcomes.Win => 6,
            Outcomes.Lose => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome,
                $"Outcome \"{outcome}\" is not supported")
        };

    private static Moves GetMove(this Moves opponentMove, Outcomes desiredOutcome) =>
        (opponentMove, desiredOutcome) switch
        {
            (_, Outcomes.Draw) => opponentMove,
            (Moves.Rock, Outcomes.Lose) => Moves.Scissors,
            (_, Outcomes.Lose) => opponentMove - 1,
            (Moves.Scissors, Outcomes.Win) => Moves.Rock,
            (_, Outcomes.Win) => opponentMove + 1,
            _ => throw new ArgumentOutOfRangeException(
                $"\"Unsupported outcome {desiredOutcome} with opponent move {opponentMove}")
        };
}