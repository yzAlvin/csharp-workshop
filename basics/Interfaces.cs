public interface IChessPiece
{
	void Move();
	void Capture();
}

public class Queen : IChessPiece
{
	public void Capture()
	{
		Console.WriteLine("Capture anything it can move to");
	}

	public void Move()
	{
		Console.WriteLine("Move horizontally, vertically, diagonally, any number of spaces");
	}
}

public class Pawn : IChessPiece
{
	public void Capture()
	{
		Console.WriteLine("Can only capture pieces diagonally, 1 space in front of it");
	}

	public void Move()
	{
		Console.WriteLine("Can only move forward on space");
	}

	public void Promote()
	{
		Console.WriteLine("Turn into any piece at the end");
	}
}

public class ChessGame
{
	void MovePiece(IChessPiece chessPiece)
	{
		chessPiece.Move();
	}
}