using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_1_Shell {
    public class Cheval : Animal {
        public Cheval(Int32 _id, string _type, string _nom, Int32 _age, Int32 _poids, string _couleur, string _nomProprio) {
            this.Id = _id;
            this.Type = _type;
            this.Nom = _nom;
            this.Age = _age;
            this.Poids = _poids;
            this.Couleur = _couleur;
            this.NomProprio = _nomProprio;
        }
    }
}
