using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverSudoku
{
    public static class RuleMethod
    {
        // sudoku
        // Les faits ce sont des valeurs dans le matrice
        
        /// <summary>
        /// Recherche si ligne Horizontal dupliqué
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns></returns>
        public static bool NoDuplicateRowHori(Sudoku sudoku)
        {
            HashSet< int[]> hashSet = new HashSet<int[]>();

            // premier élément
            hashSet.Add(sudoku.GetRowsHorizontal(0));

            for (int i = 1; i < Sudoku.SIZE-1; i++)
            {
                var r = sudoku.GetRowsHorizontal(i);

                foreach (var hash in hashSet)
                {
                    // regarde si cette ligne existe déjà
                    if (RowUtil.RowEqual(r, hash))
                    {
                        // existe                         
                        return false;
                    }
                }               
            }

            hashSet = null;
            return true;
        }


        /// <summary>
        /// Recherche si ligne Vertical dupliqué
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns></returns>
        public static bool NoDuplicateRowVerti(Sudoku sudoku)
        {
            HashSet<int[]> hashSet = new HashSet<int[]>();

            // premier élément
            hashSet.Add(sudoku.GetRowVertical(0));

            for (int i = 1; i < Sudoku.SIZE - 1; i++)
            {
                var r = sudoku.GetRowVertical(i);

                foreach (var hash in hashSet)
                {
                    // regarde si cette ligne existe déjà
                    if (RowUtil.RowEqual(r, hash))
                    {
                        // existe                         
                        return false;
                    }
                }
            }

            hashSet = null;
            return true;
        }


        public static bool IsRowMatrixIsPermutation(Sudoku sudoku)
        {   
            for (int i = 0; i < Sudoku.SIZE; i++)
            {
                if (RowUtil.IsPermutation(RowUtil.GetRowsbMatrixFlate(i,sudoku.Matrix))== false)
                {
                    return false;
                }
            }

            return true;
        }


        public static bool IsRowVerticalIsPermutation(Sudoku sudoku)
        {
            for (int i = 0; i < Sudoku.SIZE; i++)
            {
                if (RowUtil.IsPermutation(sudoku.GetRowVertical(i)) == false)
                {
                    return false;
                }
            }

            return true;
        }

      
    }
}
