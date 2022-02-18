using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackMarvelVSCapman.Business.Gameplay;

namespace MSTest
{
    [TestClass]
    public class BusinessGameplayBoard
    {
        [TestMethod]
        public void TestPlay()
        {
            Board board = new Board();
            board.Play(1, true);
            for (int i = 0; i < 6; i++)
            {
                Assert.IsTrue(board.Play(2, true));
            }
            //Assert.IsFalse(board.Play(2, true));
        }


        [TestMethod]
        public void TestWinCol()
        {
            Board board = new Board();

            Assert.AreEqual(WIN_RESULT.NOT_WIN, board.CheckWin());

            board.Play(0, true);
            Assert.AreEqual(WIN_RESULT.NOT_WIN, board.CheckWin());
            board.Play(0, true);
            board.Play(0, true);
            board.Play(0, true);

            Assert.AreEqual(WIN_RESULT.P1, board.CheckWin());
        }

        [TestMethod]
        public void TestWinLin()
        {
            Board board = new Board();

            Assert.AreEqual(WIN_RESULT.NOT_WIN, board.CheckWin());

            board.Play(2, true);
            Assert.AreEqual(WIN_RESULT.NOT_WIN, board.CheckWin());
            board.Play(3, true);
            board.Play(4, false);
            board.Play(5, true);

            Assert.AreEqual(WIN_RESULT.NOT_WIN, board.CheckWin());

            board.Play(2, true);
            board.Play(3, true);
            board.Play(4, true);
            board.Play(5, true);

            Assert.AreEqual(WIN_RESULT.P1, board.CheckWin());
        }

        [TestMethod]
        public void TestDiagR()
        {
            Board board = new Board();
            
            board.Play(1, true);
            board.Play(2, true);
            board.Play(2, true);
            board.Play(3, true);
            board.Play(3, true);
            board.Play(3, true);

            board.Play(0, false);
            board.Play(1, false);
            board.Play(2, false);
            board.Play(3, false);

            Assert.AreEqual(WIN_RESULT.P2, board.CheckWin());
        }

        [TestMethod]
        public void TestDiadL()
        {
            Board board = new Board();

            board.Play(0, true);
            board.Play(0, true);
            board.Play(0, true);
            board.Play(1, true);
            board.Play(1, true);
            board.Play(2, true);

            board.Play(0, false);
            board.Play(1, false);
            board.Play(2, false);
            board.Play(3, false);

            Assert.AreEqual(WIN_RESULT.P2, board.CheckWin());
        }

        [TestMethod]
        public void TestGridFull()
        {
            Board board = new Board();

            //fill the board with a patern where no one win
            for (int i = 0; i < Board.NB_COL; i++)
            {
                for (int j = 0; j < Board.NB_LIN; j++)
                {
                    //one ligne P1 the next P2 and so on
                    bool jeton = Convert.ToBoolean(j % 2);
                    if(i == 3)
                    {
                        jeton = !jeton;
                    }
                    board.Play(i, jeton);
                }
            }

            Assert.AreEqual(WIN_RESULT.NULL, board.CheckWin());
        }
    }
}