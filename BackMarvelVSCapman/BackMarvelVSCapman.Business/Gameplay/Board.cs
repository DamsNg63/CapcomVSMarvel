using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public enum Jeton
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
        public Jeton[] TabBoard { get; private set; }
        public const int NB_COL = 7;
        public const int NB_LIN = 6;

        public Board()
        {
            TabBoard = new Jeton[NB_COL * NB_LIN];
            for(int i = 0; i < NB_COL*NB_LIN; i++)
            {
                TabBoard[i] = Jeton.EMPTY;
            }
        }

        public bool Play(int col, bool isPlayer1)
        {
            int i = 0;
            while(TabBoard[i * NB_COL + col] != Jeton.EMPTY && i < NB_LIN)
            {
                i++;
            }
            if(TabBoard[i * NB_COL + col] == Jeton.EMPTY)
            {
                TabBoard[i * NB_COL + col] = isPlayer1 ? Jeton.P1 : Jeton.P2;
                return true;
            }
            else
            {
                return false;
            }
        }

        private WIN_RESULT WinResColLin(int line, int col)
        {
            int nbPlay = 0;
            int cptLP1;
            int cptLP2;
            for (int i = 0; i < line; i++)
            {
                cptLP1 = 0;
                cptLP2 = 0;
                for (int j = 0; j < col; j++)
                {
                    if (TabBoard[i * NB_COL + j] != Jeton.EMPTY)
                    {
                        nbPlay++;
                        if (TabBoard[i * NB_COL + j] == Jeton.P1)
                        {
                            cptLP1++;
                            cptLP2 = 0;
                        }
                        else
                        {
                            cptLP2++;
                            cptLP1=0;
                        }
                    }
                    if (cptLP1 > 3)
                    {
                        return WIN_RESULT.P1;
                    }
                    if (cptLP2 > 3)
                    {
                        return WIN_RESULT.P2;
                    }
                }
            }
            if(nbPlay == TabBoard.Length - 1)
            {
                return WIN_RESULT.NULL;
            }
            return WIN_RESULT.NOT_WIN;
        }

        private WIN_RESULT WinDiagL()
        {
            int cptLP1;
            int cptLP2;
            for (int a = -2; a < 4; a++)
            {
                cptLP1 = 0;
                cptLP2 = 0;
                int maxL = a < 0 ? NB_LIN-1 + a+1 : NB_LIN-1;
                int minL = a < 0 ? 0 : a;

                for (int i = minL; i<= maxL; i++)
                {
                    var elem = TabBoard[i * NB_COL + i - a];
                    if(elem != Jeton.EMPTY)
                    {
                        if(elem == Jeton.P1)
                        {
                            cptLP1++;
                            cptLP2 = 0;
                        }
                        else
                        {
                            cptLP2++;
                            cptLP1 = 0;
                        }
                    }
                    if (cptLP1 > 3)
                    {
                        return WIN_RESULT.P1;
                    }
                    if (cptLP2 > 3)
                    {
                        return WIN_RESULT.P2;
                    }
                }
            }
            return WIN_RESULT.NOT_WIN;
        }

        private WIN_RESULT WinDiagR()
        {
            int cptLP1;
            int cptLP2;
            for (int a = -2; a < 4; a++)
            {
                cptLP1 = 0;
                cptLP2 = 0;
                int maxL = a < 2 ? NB_LIN-1 : NB_LIN - a;
                int minL = a < 0 ? -a : 0;
                for (int i = minL; i <= maxL; i++)
                {

                    var elem = TabBoard[i * NB_COL - i - a+NB_LIN];
                    if (elem != Jeton.EMPTY)
                    {
                        if (elem == Jeton.P1)
                        {
                            cptLP1++;
                            cptLP2 = 0;
                        }
                        else
                        {
                            cptLP2++;
                            cptLP1 = 0;
                        }
                    }
                    if (cptLP1 > 3)
                    {
                        return WIN_RESULT.P1;
                    }
                    if (cptLP2 > 3)
                    {
                        return WIN_RESULT.P2;
                    }
                }
            }
            return WIN_RESULT.NOT_WIN;
        }

        public WIN_RESULT CheckWin()
        {
            //line
            var win = WinResColLin(NB_LIN, NB_COL);
            //col
            if(win == WIN_RESULT.NOT_WIN)
            {
                win = WinResColLin(NB_LIN, NB_COL);
            }
            if (win == WIN_RESULT.NOT_WIN)
            {
                win = WinDiagL();
            }
            if (win == WIN_RESULT.NOT_WIN)
            {
                win = WinDiagR();
            }

            return win;
            //diagL todo 
            //diagR todo
        }
    }
}
