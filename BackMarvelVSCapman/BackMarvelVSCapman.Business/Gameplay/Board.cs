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
        public Jeton[,] TabBoard { get; }
        public const int NB_COL = 7;
        public const int NB_LIN = 6;

        public Board()
        {
            TabBoard = new Jeton[NB_COL, NB_LIN];
            for(int i = 0; i < NB_COL; i++)
            {
                for(int j = 0; j < NB_LIN; j++)
                {
                    TabBoard[i, j] = Jeton.EMPTY;
                }
            }
        }

        public bool Play(int col, bool isPlayer1)
        {
            int i = 0;
            while(TabBoard[col, i] != Jeton.EMPTY && i < NB_LIN)
            {
                i++;
            }
            if(i < NB_LIN)
            {
                TabBoard[col, i] = isPlayer1 ? Jeton.P1 : Jeton.P2;
                return true;
            }
            else
            {
                return false;
            }
        }

        private WIN_RESULT TestGridFull()
        {
            for(int i = 0; i < NB_COL; i++)
            {
                if(TabBoard[i, NB_LIN - 1] == Jeton.EMPTY)
                {
                    return WIN_RESULT.NOT_WIN;
                }
            }
            return WIN_RESULT.NULL;
        }

        private WIN_RESULT WinCol()
        {
            //compte le nombre de jetons alignes
            int nbJetonsAligne = 0;
            //jeton aligne actuel
            Jeton jetonActuel = Jeton.EMPTY;

            var win = WIN_RESULT.NOT_WIN;

            if (NB_LIN >= 4)
            {
                for(int i = 0; i<NB_COL && nbJetonsAligne<4; i++)
                {
                    nbJetonsAligne = 1;
                    for(int j = 0; j < NB_LIN && nbJetonsAligne < 4; j++)
                    {
                        if (jetonActuel != Jeton.EMPTY && TabBoard[i, j] == jetonActuel)
                        {
                            nbJetonsAligne++;
                        }
                        else
                        {
                            jetonActuel = TabBoard[i, j];
                            nbJetonsAligne = 1;
                        }
                    }
                }
                
            }

            if(nbJetonsAligne == 4)
            {
                if (jetonActuel == Jeton.P1)
                {
                    win = WIN_RESULT.P1;
                }
                if (jetonActuel == Jeton.P2)
                {
                    win = WIN_RESULT.P2;
                }

            }

            return win;
        }

        private WIN_RESULT WinLin()
        {
            //compte le nombre de jetons alignes
            int nbJetonsAligne = 0;
            //jeton aligne actuel
            Jeton jetonActuel = Jeton.EMPTY;

            var win = WIN_RESULT.NOT_WIN;

            if (NB_COL >= 4)
            {
                for (int j = 0; j < NB_LIN && nbJetonsAligne < 4; j++)
                {
                    nbJetonsAligne = 1;
                    for (int i = 0; i < NB_COL && nbJetonsAligne < 4; i++)
                    {
                        if (jetonActuel != Jeton.EMPTY && TabBoard[i, j] == jetonActuel)
                        {
                            nbJetonsAligne++;
                        }
                        else
                        {
                            jetonActuel = TabBoard[i, j];
                            nbJetonsAligne = 1;
                        }
                    }
                }

            }

            if (nbJetonsAligne == 4)
            {
                if (jetonActuel == Jeton.P1)
                {
                    win = WIN_RESULT.P1;
                }
                if (jetonActuel == Jeton.P2)
                {
                    win =  WIN_RESULT.P2;
                }

            }

            return win;
        }
        private WIN_RESULT WinDiagR()
        {
            WIN_RESULT win= WIN_RESULT.NOT_WIN;

            for(int i = 0; i < NB_COL-3; i++)
            {
                for(int j=0; j < NB_LIN-3; j++)
                {
                    Jeton jeton = TabBoard[i, j];
                    for(int k = 1; k < 4 && TabBoard[i+k, j+k] == jeton ; k++){
                        if(k == 3)
                        {
                            if (jeton == Jeton.P1)
                            {
                                win = WIN_RESULT.P1;
                            }
                            if (jeton == Jeton.P2)
                            {
                                win = WIN_RESULT.P2;
                            }
                            return win;
                        }
                    }
                }
            }
            return win;
        }
        
        private WIN_RESULT WinDiagL() {
            WIN_RESULT win = WIN_RESULT.NOT_WIN;

            for (int i = 3; i < NB_COL; i++)
            {
                for (int j = 0; j < NB_LIN - 3; j++)
                {
                    Jeton jeton = TabBoard[i, j];
                    for (int k = 1; k < 4 && TabBoard[i - k, j + k] == jeton; k++)
                    {
                        if (k == 3)
                        {
                            if (jeton == Jeton.P1)
                            {
                                win = WIN_RESULT.P1;
                            }
                            if (jeton == Jeton.P2)
                            {
                                win = WIN_RESULT.P2;
                            }
                            return win;
                        }
                    }
                }
            }
            return win;
        }

        

        public WIN_RESULT CheckWin()
        {

            var win = WinCol();
            Console.WriteLine(win);

            if (win == WIN_RESULT.NOT_WIN)
            {
                win = WinLin();
            }
            if(win == WIN_RESULT.NOT_WIN)
            {
                win = WinDiagL();
            }
            if (win == WIN_RESULT.NOT_WIN)
            {
                win = WinDiagR();
            }
            if (win == WIN_RESULT.NOT_WIN)
            {
                win = TestGridFull();
            }

            return win;
        }
    }
}
