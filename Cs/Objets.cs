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

    // valeurs correspondent à la ligne dans le .csv
    enum CodeCategoriePassager {
        adu26p = 1, 
        jeu1825 = 2, 
        enf417 = 3,
        bebe = 4,
        ancomp = 5
    }

    // valeurs correspondent à la ligne dans le .csv (commme un indice)
    enum CodeCategorieVehicule {
        trot = 1,
        velo = 2,
        velelec = 3,
        cartand = 4,
        mobil = 5,
        moto = 6,
        cat1 = 7,
        cat2 = 8,
        cat3 = 9,
        cat4 = 10,
        camp = 11
    }
}
