using Accord.Fuzzy;
using System;

namespace ZoomGps
{
    public class ZoomGps
    {

        private InferenceSystem m_IS;

        public ZoomGps()
        {
            // création du langage
            var ld = LinguistictDistance();
            var lv = LinguisticVitesse();
            var lz = LinguisticZoom();

            // création des règles

            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(ld);
            fuzzyDB.AddVariable(lv);
            fuzzyDB.AddVariable(lz);

            var IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(100));

            IS.NewRule("Rule 1", "IF Distance IS Grande THEN Zoom IS Petit");
            IS.NewRule("Rule 2", "IF Distance IS Faible AND Vitesse IS Lente THEN Zoom IS Normal");
            IS.NewRule("Rule 3", "IF Distance IS Faible AND Vitesse IS PeuRapide THEN Zoom IS Normal");
            IS.NewRule("Rule 4", "IF Distance IS Faible AND Vitesse IS Rapide THEN Zoom IS Gros");
            IS.NewRule("Rule 5", "IF Distance IS Faible AND Vitesse IS TresRapide THEN Zoom IS Gros");
            
            IS.NewRule("Rule 6", "IF Distance IS Moyenne AND Vitesse IS Lente THEN Zoom IS Petit");
            IS.NewRule("Rule 7", "IF Distance IS Moyenne AND Vitesse IS PeuRapide THEN Zoom IS Normal");
            IS.NewRule("Rule 8", "IF Distance IS Moyenne AND Vitesse IS Rapide THEN Zoom IS Normal");
            IS.NewRule("Rule 9", "IF Distance IS Moyenne AND Vitesse IS TresRapide THEN Zoom IS Gros");

            m_IS = IS;
        }

        public float DoInference(float distance, float vitesse)
        {
            m_IS.SetInput("Distance", distance);
            m_IS.SetInput("Vitesse", vitesse);
            return m_IS.Evaluate("Zoom");
        }

        public LinguisticVariable LinguistictDistance()
        {
            LinguisticVariable lDistance = new LinguisticVariable("Distance", 0, 500000);
            FuzzySet fsFaible = new FuzzySet("Faible", new TrapezoidalFunction(30, 50, 500000,0, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsMoyenne = new FuzzySet("Moyenne", new TrapezoidalFunction(40, 50, 100, 150));
            FuzzySet fsGrande = new FuzzySet("Grande", new TrapezoidalFunction(100, 150, 500000, 0, TrapezoidalFunction.EdgeType.Left));

            lDistance.AddLabel(fsFaible);
            lDistance.AddLabel(fsMoyenne);
            lDistance.AddLabel(fsGrande);
            
            return lDistance;
        }

        public LinguisticVariable LinguisticVitesse()
        {
            LinguisticVariable lV = new LinguisticVariable("Vitesse", 0, 200);
            FuzzySet fsLente = new FuzzySet("Lente", new TrapezoidalFunction(20, 30,200,0, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsPeuRapide = new FuzzySet("PeuRapide", new TrapezoidalFunction(20, 30, 70, 80));
            FuzzySet fsRapide = new FuzzySet("Rapide", new TrapezoidalFunction(70, 80, 90, 110));
            FuzzySet fsTresRapide = new FuzzySet("TresRapide", new TrapezoidalFunction(90, 110,200,0, TrapezoidalFunction.EdgeType.Left));

            lV.AddLabel(fsLente);
            lV.AddLabel(fsPeuRapide);
            lV.AddLabel(fsRapide);
            lV.AddLabel(fsTresRapide);
            
            return lV;
        }

        private LinguisticVariable LinguisticZoom()
        {
            LinguisticVariable lZ = new LinguisticVariable("Zoom", 1, 5);
            FuzzySet fsPetit = new FuzzySet("Petit", new TrapezoidalFunction(1, 2, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsNormal = new FuzzySet("Normal", new TrapezoidalFunction(1, 2, 3,4));
            FuzzySet fsGros = new FuzzySet("Gros", new TrapezoidalFunction(3,4, TrapezoidalFunction.EdgeType.Left));

            lZ.AddLabel(fsPetit);
            lZ.AddLabel(fsNormal);
            lZ.AddLabel(fsGros);

            return lZ;
        }

    }
}
