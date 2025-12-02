using System;

// Contient tout ce qui est en rapport avec le stockage de données (temporaires / au moment de l'exécution)
// structures, énumérations, etc... 
partial class Programme {
    struct Reservation {
        public string nom;
        public uint idLiaison;
        public string date;
        public string heure;
        public string horodatage;

        public Reservation(string n, Liaison l){
            nom = n;
            idLiaison = (uint)l;

            date = ""; // format YYYY-MM-DD
            heure = ""; // format HH:mm
            horodatage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    struct Trajet {
        public Reservation traversee;
        public List<Passager> passagers;
        public List<Vehicule> vehicules;

        public Trajet(Reservation t, List<Passager> p, List<Vehicule> v){
            traversee = t;
            passagers = p;
            vehicules = v;
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

    // valeurs correspondent à la ligne dans le .csv (commme un indice)
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

    struct Passager {
        public string nom, prenom;
        public CodeCategoriePassager categorie;

        public Passager(string n, string p, CodeCategoriePassager c){
            nom = n;
            prenom = p;
            categorie = c;
        }
    }

    struct Vehicule {
        public CodeCategorieVehicule categorie;
        public uint quantite;

        public Vehicule(uint q, CodeCategorieVehicule c){
            quantite = q;
            categorie = c;
        }
    }
}
