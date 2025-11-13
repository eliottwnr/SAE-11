using System;

// Contient tout ce qui est en rapport avec le stockage de données (temporaires / au moment de l'exécution)
// structures, énumérations, etc... 
partial class Programme {
    struct Traversee {
        public Liaison liaison;
        public uint[] date;
        public uint[] heure;

        public Traversee(Liaison l){
            liaison = l;

            date = new uint[3]; // Jour, Mois, Année 
            heure = new uint[2]; // Heures, Minutes
        }
    }

    // permet de représenter les Liaisons sous une forme plus explicite que des entiers
    enum Liaison {
        groix_lorient = 1, 
        lorient_groix = 2, 
        lepalais_quiberon = 3,
        quiberon_lepalais = 4
    }
}
