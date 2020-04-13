using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SolverSudoku
{
    public class Fact
    {
        /// <summary>
        /// ligne résolu
        /// </summary>
        public bool Resolu { get { return Row.ToList().All(e => e.HasValue) ? true : false; } }

        /// <summary>
        /// index de la ligne
        /// </summary>
        public int Id = 0;

        /// <summary>
        /// 
        /// </summary>
        List<int> indexMssing = new List<int>();

        public int?[] Row;


        public List<int[]> Permutaions = new List<int[]>();
        public static List<int> POSSIBLE = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> M1 = new List<int>() { 0, 1, 2 };
        List<int> M2 = new List<int>() { 3, 4, 5 };
        List<int> M3 = new List<int>() { 6, 7, 8 };

        /// <summary>
        /// Choix possible pour une case
        /// </summary>
        public List<List<int>> Tirage { get; set; } = new List<List<int>>();

        public Fact(int id, int?[] row)
        {
            Id = id;
            Row = row;
        }
        
        public void PermuteRow2()
        {
            Permutaions.Clear();

            if (Resolu)
            {
                var res = new int[Row.Length];
                for (int i = 0; i < Row.Length; i++)
                {
                    res[i] = Row[i].Value;
                }

                Permutaions.Add(res);
            }
            else
            {
                //regarde dans le tirage les choix possibles
                int i = 0;

                for (int j = 0; j < Tirage[i].Count; j++) //0
                {
                    for (int l = 0; l < Tirage[i + 1].Count; l++) //1
                    {
                        for (int m = 0; m < Tirage[i + 2].Count; m++) //2
                        {
                            for (int n = 0; n < Tirage[i + 3].Count; n++) //3
                            {
                                for (int o = 0; o < Tirage[i + 4].Count; o++) // 4
                                {
                                    for (int p = 0; p < Tirage[i + 5].Count; p++) // 5
                                    {
                                        for (int q = 0; q < Tirage[i + 6].Count; q++) // 6
                                        {
                                            for (int r = 0; r < Tirage[i + 7].Count; r++) // 7
                                            {
                                                for (int s = 0; s < Tirage[i + 8].Count; s++) //8
                                                {
                                                    var permutation = new int[Sudoku.SIZE];

                                                    permutation[0]=Tirage[i][j];
                                                    permutation[1] = Tirage[i + 1][l];
                                                    permutation[2] = Tirage[i + 2][m];
                                                    permutation[3] = Tirage[i + 3][n];
                                                    permutation[4] = Tirage[i + 4][o];
                                                    permutation[5] = Tirage[i + 5][p];
                                                    permutation[6] = Tirage[i + 6][q];
                                                    permutation[7] = Tirage[i + 7][r];
                                                    permutation[8] = Tirage[i + 8][s];

                                                    Permutaions.Add(permutation);

                                                }

                                            }

                                        }

                                    }

                                }

                            }

                        }

                    }

                }
            }
        }


        /// <summary>
        /// recherche les cases a permuter
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetMissingValue()
        {

            List<int> trouve = new List<int>();
            for (int i = 0; i < Row.Length; i++)
            {
                if (Row[i].HasValue)
                {
                    trouve.Add(Row[i].Value);
                }
                else
                {
                    indexMssing.Add(i);
                }
            }

            return POSSIBLE.Where(v => !trouve.Contains(v));
        }

        internal void Exclure(List<Exclure> globalExclure)
        {
            // controle horizontale            
            var rowMatEx = FindExclusMatrice(Id);
            for (int i = 0; i < Row.Length; i++)
            {
                if (Row[i].HasValue)
                {
                    // il y a une valeur on peut exclure des combinanisons
                    Exclure ex = globalExclure.FirstOrDefault(e => e.Valeur == Row[i].Value);
                    if (ex == null)
                    {
                        ex = new Exclure();
                        ex.Valeur = Row[i].Value;
                        globalExclure.Add(ex);
                    }

                    // MATRICE -> A quel sous matrice appartient cette valeur
                    var ColMatEx = FindExclusMatrice(i);
                    foreach (var l in rowMatEx)
                    {
                        foreach (var c in ColMatEx)
                        {
                            ex.AExclure[l, c] = 1;
                        }
                    }

                    // Colonne -> exclu la colonne  a l'excepion de la cellule
                    for (int r = 0; r < Sudoku.SIZE; r++)
                    {
                        if (r != Id)
                        {
                            ex.AExclure[r, i] = 1;
                        }
                    }

                    // Ligne meme si on sait que c'est des permutations en ligne on exclues aussi la ligne a l'exception de la cellule
                    for (int c = 0; c < Sudoku.SIZE; c++)
                    {
                        if (c != i)
                        {
                            ex.AExclure[Id, c] = 1;
                        }
                    }
                }
            }

        }


        /// <summary>
        /// recherche les cases a éliminer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<int> FindExclusMatrice(int i)
        {
            var lst = new List<int>();
            if (M1.Contains(i))
            {
                lst = M1.Where(m => m != i).ToList();
            }
            else if (M2.Contains(i))
            {
                lst = M2.Where(m => m != i).ToList();

            }
            else if (M3.Contains(i))
            {
                lst = M3.Where(m => m != i).ToList();

            }

            return lst;
        }
    }
}
