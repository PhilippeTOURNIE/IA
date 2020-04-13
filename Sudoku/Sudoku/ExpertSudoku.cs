using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverSudoku
{
    public class ExpertSudoku
    {
        public Sudoku Solution = null;
        public ExpertSudoku(List<Fact> facts)
        {
            //-------------------------------------
            // apply chaining forward
            //-------------------------------------
            Console.WriteLine("-----------------");
            Console.WriteLine("Chainage avant");
            Console.WriteLine("-----------------");
            List<Exclure> exclures = null;

            // recherche les cases exclues.
            var flagValeurTrouve = true;
            while (flagValeurTrouve)
            {
                flagValeurTrouve = false;

                //---------------------
                // exclusions
                //---------------------
                exclures = new List<Exclure>();
                // exclu a cause de la permetutaion
                facts.ForEach(f => f.Exclure(exclures));
                // exclu les existantes differentes
                facts.ForEach(f =>
                {
                    for (int i = 0; i < f.Row.Count(); i++)
                    {
                        if (f.Row[i].HasValue)
                        {
                            foreach (var ee in exclures.Where(ex => ex.Valeur != f.Row[i].Value))
                            {
                                ee.AExclure[f.Id, i] = 1;
                            }
                        }
                    }
                });

                //---------------------
                // regle 1
                // si une valeur est autorisée que sur une case alors on a une solution
                //---------------------
                foreach (var exclure in exclures)
                {
                    var valeurTrouvee = exclure.SearchValues();
                    // retirer

                    if (valeurTrouvee.Count() > 0)
                    {
                        foreach (var vt in valeurTrouvee)
                        {
                            if (!facts[vt.Item1].Row[vt.Item2].HasValue)
                            {
                                flagValeurTrouve = true;
                                // met à jour la matrice
                                facts[vt.Item1].Row[vt.Item2] = exclure.Valeur;
                                Console.WriteLine($"Ligne : {vt.Item1 + 1}, colonne {vt.Item2 + 1}, valeur : {exclure.Valeur}");
                            }
                        }
                    }
                }

                //---------------------
                // regle 2
                // test les exclusions si interdit a tous les chiffres sauf un chiffre alors 
                // on a trouvé une solution ( cellule ne convient pas 1,3,4,5,6,7,8,9 => reponse =2)
                // en meme temps prepare la liste des permutations possbiles.
                //---------------------
                foreach (var f in facts)
                {
                    f.Tirage = new List<List<int>>();

                    for (int i = 0; i < Sudoku.SIZE; i++)
                    {
                        if (!f.Row[i].HasValue)
                        {
                            List<int> choixInterdit = new List<int>();   //= Fact.POSSIBLE;
                            foreach (var ex in exclures)
                            {
                                if (ex.AExclure[f.Id, i] > 0)
                                {
                                    // exclure 
                                    choixInterdit.Add(ex.Valeur);
                                }
                            }

                            var possible = Fact.POSSIBLE.Except(choixInterdit);

                            if (possible.Count() == 1)
                            {
                                // on a trouvé une solution
                                f.Row[i] = possible.First();
                                flagValeurTrouve = true;
                                f.Tirage.Add(new List<int>() { possible.First() });
                                Console.WriteLine($"Ligne : {f.Id + 1}, colonne {i + 1}, valeur : {possible.First()}");

                            }
                            else
                            {
                                //pas trouvé on mets les choix possible
                                f.Tirage.Add(possible.ToList());
                            }
                        }
                        else
                        {
                            f.Tirage.Add(new List<int>() { f.Row[i].Value });
                        }
                    }
                }

            }
            
            
            if (facts.All(f => f.Resolu)){
                // Si chainage avant gagné
                List<int[]> tmp = new List<int[]>();
                GetRowNonNullable(facts, tmp);

                RowUtil.PrintConsole(tmp);
                Solution =new Sudoku(tmp);
                return;
            }

            //-------------------------------------
            // apply chaining backward
            //-------------------------------------
            Console.WriteLine("-----------------");
            Console.WriteLine("Chainage arriere");
            Console.WriteLine("-----------------");


            facts.ForEach(f => f.PermuteRow2());


            // si apres permutation il y a une permutations ==1 une ligne a été résolu . on peut recommencer
            // les calculs avec les nouveaux fait inférés


            // iteration

            foreach (var t0 in facts[0].Permutaions)
            {
                foreach (var t1 in facts[1].Permutaions)
                {
                    foreach (var t2 in facts[2].Permutaions)
                    {
                        foreach (var t3 in facts[3].Permutaions)
                        {
                            foreach (var t4 in facts[4].Permutaions)
                            {
                                foreach (var t5 in facts[5].Permutaions)
                                {
                                    foreach (var t6 in facts[6].Permutaions)
                                    {
                                        foreach (var t7 in facts[7].Permutaions)
                                        {
                                            foreach (var t8 in facts[8].Permutaions)
                                            {
                                                List<int[]> rows = new List<int[]>();
                                                rows.Add(t0);
                                                rows.Add(t1);
                                                rows.Add(t2);
                                                rows.Add(t3);
                                                rows.Add(t4);
                                                rows.Add(t5);
                                                rows.Add(t6);
                                                rows.Add(t7);
                                                rows.Add(t8);


                                                var sudoku = new Sudoku(rows);

                                                if (ApplyRule(sudoku))
                                                {
                                                    Solution = sudoku;
                                                    RowUtil.PrintConsole(rows);

                                                    return;
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

            Console.WriteLine("Aucunesolution !");
        }

        private static void GetRowNonNullable(List<Fact> facts, List<int[]> tmp)
        {
            facts.ForEach(t =>
            {
                var tl = new int[9];
                for (int i = 0; i < t.Row.Count(); i++)
                {
                    tl[i] = t.Row[i].HasValue ? t.Row[i].Value : 0;
                }
                tmp.Add(tl);
            });
        }

        private bool ApplyRule(Sudoku sudoku)
        {
            if (!RuleMethod.IsRowMatrixIsPermutation(sudoku))
            {
                return false;
            }
            if (!RuleMethod.IsRowVerticalIsPermutation(sudoku))
            {
                return false;
            }

            return true;
        }


    }
}

