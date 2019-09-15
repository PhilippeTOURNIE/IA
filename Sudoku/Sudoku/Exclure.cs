using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverSudoku
{
    public class Exclure
    {
        public int Valeur;

        /// <summary>
        /// un 1 dans la matrice correspond à une exclsion pour cette case
        /// </summary>
        public int[,] AExclure = new int[Sudoku.SIZE, Sudoku.SIZE];

        /// <summary>
        /// il ne reste q'une seule case possible
        /// </summary>
        /// <returns></returns>
        internal List<Tuple<int, int>> SearchValues()
        {
            var findCells = new List<Tuple<int, int>>();

            // tester si c'est la seule possible sur une ligne
            for (int i = 0; i < Sudoku.SIZE; i++)
            {
                int nbPossible = 0, line = -1, column = -1;

                for (int c = 0; c < Sudoku.SIZE; c++)
                {
                    if (AExclure[i, c] == 0)
                    {
                        line = i;
                        column = c;
                        nbPossible++;
                    }
                }

                if (nbPossible == 1)
                {
                    findCells.Add(new Tuple<int, int>(line, column));
                }
            }


            // tester si c'est la seule possible sur une Colonne
            for (int c = 0; c < Sudoku.SIZE; c++)
            {
                int nbPossible = 0, line = -1, column = -1;

                for (int i = 0; i < Sudoku.SIZE; i++)
                {
                    if (AExclure[i, c] == 0)
                    {
                        line = i;
                        column = c;
                        nbPossible++;
                    }
                }

                if (nbPossible == 1)
                {
                    findCells.Add(new Tuple<int, int>(line, column));
                }
            }

            // seule valeur possible dans une sous matrice
            for (int i = 0; i < Sudoku.SIZE; i++)
            {
                var underMatrix = RowUtil.GetRowsbMatrixFlate(i, AExclure);

                // parcours cette sous matrice 3 X 3
                int nbPossible = 0, line = -1;

                for (int l = 0; l < underMatrix.Length; l++)
                {
                    if (underMatrix[l] == 0)
                    {
                        line = l;
                        nbPossible++;
                    }
                }

                if (nbPossible == 1)
                {
                    // recherche des coordonnées dans la matrice principale
                    int mainL = -1;
                    int MainC = -1;
                    RowUtil.getCoordMainMatrix(i, line, out mainL, out MainC);
                    findCells.Add(new Tuple<int, int>(mainL, MainC));
                }

            }

            return findCells;
        }
    }
}
