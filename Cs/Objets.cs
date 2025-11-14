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
        adu26p = 0, 
        jeu1825 = 1, 
        enf417 = 2,
        bebe = 3,
        ancomp = 4
    }

    // valeurs correspondent à la ligne dans le .csv
    enum CodeCategorieVehicule {
        trot = 0,
        velo = 1,
        velelec = 2,
        cartand = 3,
        mobil = 4,
        moto = 5,
        cat1 = 6,
        cat2 = 7,
        cat3 = 8,
        cat4 = 9,
        camp = 10
    }
}
