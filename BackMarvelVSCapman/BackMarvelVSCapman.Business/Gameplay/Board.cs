using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public enum Token
    {
        EMPTY = 0,
        P1 = 1,
        P2 = 2
    }
    public enum WIN_RESULT
    {
        NOT_WIN,
        P1,
        P2,
        NULL,
    }
    public class Board
    {
        private delegate WIN_RESULT CheckFunc();

        public Token[,] TabBoard { get; }
        public const int NB_COL = 7;
        public const int NB_LIN = 6;

        public Board()
        {
            TabBoard = new Token[NB_COL, NB_LIN];
            for (int i = 0; i < NB_COL; i++)
            {
                for (int j = 0; j < NB_LIN; j++)
                {
                    TabBoard[i, j] = Token.EMPTY;
                }
            }
        }

        public bool Play(int col, bool isPlayer1)
        {
            int i = 0;
            while (TabBoard[col, i] != Token.EMPTY && i < NB_LIN)
            {
                i++;
            }
            if (i < NB_LIN)
            {
                TabBoard[col, i] = isPlayer1 ? Token.P1 : Token.P2;
                return true;
            }
            else
            {
                return false;
            }
        }

        private WIN_RESULT TestGridFull()
        {
            for (int i = 0; i < NB_COL; i++)
            {
                if (TabBoard[i, NB_LIN - 1] == Token.EMPTY)
                {
                    return WIN_RESULT.NOT_WIN;
                }
            }
            return WIN_RESULT.NULL;
        }

        private WIN_RESULT WinCol()
        {
            //count the number of aligned tokens
            int nbAlignedTokens = 0;

            //token actual in count
            Token currentToken = Token.EMPTY;

            var win = WIN_RESULT.NOT_WIN;

            if (NB_LIN >= 4)
            {
                for (int i = 0; i < NB_COL && nbAlignedTokens < 4; i++)
                {
                    nbAlignedTokens = 1;
                    for (int j = 0; j < NB_LIN && nbAlignedTokens < 4; j++)
                    {
                        if (currentToken != Token.EMPTY && TabBoard[i, j] == currentToken)
                        {
                            nbAlignedTokens++;
                        }
                        else
                        {
                            currentToken = TabBoard[i, j];
                            nbAlignedTokens = 1;
                        }
                    }
                }

            }

            if (nbAlignedTokens == 4 && currentToken!=Token.EMPTY)
            {
                win = currentToken == Token.P1 ? WIN_RESULT.P1 : WIN_RESULT.P2;
            }

            return win;
        }

        private WIN_RESULT WinLin()
        {
            //count the number of aligned tokens
            int nbAlignedTokens = 0;

            //token actual in count
            Token currentToken = Token.EMPTY;

            var win = WIN_RESULT.NOT_WIN;

            if (NB_COL >= 4)
            {
                for (int j = 0; j < NB_LIN && nbAlignedTokens < 4; j++)
                {
                    nbAlignedTokens = 1;
                    for (int i = 0; i < NB_COL && nbAlignedTokens < 4; i++)
                    {
                        if (currentToken != Token.EMPTY && TabBoard[i, j] == currentToken)
                        {
                            nbAlignedTokens++;
                        }
                        else
                        {
                            currentToken = TabBoard[i, j];
                            nbAlignedTokens = 1;
                        }
                    }
                }

            }

            if (nbAlignedTokens == 4)
            {
                win = currentToken == Token.P1 ? WIN_RESULT.P1 : WIN_RESULT.P2;
            }

            return win;
        }
        private WIN_RESULT WinDiagR()
        {
            WIN_RESULT win = WIN_RESULT.NOT_WIN;

            for (int i = 0; i < NB_COL - 3; i++)
            {
                for (int j = 0; j < NB_LIN - 3; j++)
                {
                    Token token = TabBoard[i, j];
                    for (int k = 1; k < 4 && TabBoard[i + k, j + k] == token; k++)
                    {
                        if (k == 3 && token != Token.EMPTY)
                        {
                            return token == Token.P1 ? WIN_RESULT.P1 : WIN_RESULT.P2;
                        }
                    }
                }
            }
            return win;
        }

        private WIN_RESULT WinDiagL()
        {
            WIN_RESULT win = WIN_RESULT.NOT_WIN;

            for (int i = 3; i < NB_COL; i++)
            {
                for (int j = 0; j < NB_LIN - 3; j++)
                {
                    Token token = TabBoard[i, j];
                    for (int k = 1; k < 4 && TabBoard[i - k, j + k] == token; k++)
                    {
                        if (k == 3 && token != Token.EMPTY)
                        {
                            return token == Token.P1 ? WIN_RESULT.P1 : WIN_RESULT.P2;
                        }
                    }
                }
            }
            return win;
        }



        public WIN_RESULT CheckWin()
        {
            var win = WIN_RESULT.NOT_WIN;

            CheckFunc[] array =
            {
                WinCol,
                WinLin,
                WinDiagL,
                WinDiagR,
                TestGridFull
            };

            for (int i = 0; i < array.Length && win == WIN_RESULT.NOT_WIN; ++i)
            {
                win = array[i]();
            }

            return win;
        }
    }
}
