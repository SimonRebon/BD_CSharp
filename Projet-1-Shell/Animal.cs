using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_1_Shell {
    public abstract class Animal {
        private Int32 id;
        private string type;
        private string nom;
        private Int32 age;
        private Int32 poids;
        private string couleur;
        private string nomProprio;

        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public string Nom { get => nom; set => nom = value; }
        public int Age { get => age; set => age = value; }
        public int Poids { get => poids; set => poids = value; }
        public string Couleur { get => couleur; set => couleur = value; }
        public string NomProprio { get => nomProprio; set => nomProprio = value; }
    }
}
