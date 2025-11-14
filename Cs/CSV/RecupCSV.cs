using System;
using System.IO;

// Gère la récupération des données dans le csv correspondant
partial class Programme {
    // renvoie true si la lecture s'est bien passée, false sinon
    static bool lireFichier(string cheminAccess, out string[] lignes){
        bool estLue = false;

        long nbLignes = 0; // car newFileInfo.length renvoie un long
        lignes = new string[nbLignes];

        if (File.Exists(cheminAccess)){
            nbLignes = new System.IO.FileInfo(cheminAccess).Length; // récupère le nombre de lignes du fichier
            lignes = new string[nbLignes]; // chaque élément du tableau est une ligne du fichier

            lignes = File.ReadAllLines(cheminAccess);

            estLue = true;
        }

        return estLue;
    }

    static bool horairesJour(Liaison liaison, uint jour, out string[] horaires){ // jour étant le jour du mois
        bool succes = false;
        string cheminAccess = "CSV/Horaires/";

        horaires = new string[0]; // initialise un tableau vide au cas où la fonction lireFichier n'arrive pas à lire

        switch (liaison){
            case Liaison.lorient_groix:
                cheminAccess += "lorient_groix.csv";
                break;
            case Liaison.groix_lorient:
                cheminAccess += "groix_lorient.csv";
                break;
            case Liaison.quiberon_lepalais:
                cheminAccess += "quiberon_lepalais.csv";
                break;
            case Liaison.lepalais_quiberon:
                cheminAccess += "lepalais_quiberon.csv";
                break;
            default: // liaison est une énumération dont tous les cas sont traités ci-dessus
                break;
        }

        succes = lireFichier(cheminAccess, out horaires);
        horaires = horaires[jour - 1].Split(";"); // sélectionne l'indice du jour renseigné (jour 1 = indice 0)

        return succes;
    }

    static bool tarifsPassagers(CodeCategoriePassager passager, Liaison liaison, out double tarif){
        bool succes = false;

        string cheminAccess = "CSV/Tarifs/passagers.csv"; // de forme numéro du CodeCategoriePassager;tarifGroix-Lorient;tarifLePalais-Quiberon
        int nbLignesFichier = 0;

        string[] tarifs;
        string[] ligne;

        uint indiceLiaison = 0; // 0 pour groix / lorient et 1 pour le palais / quiberon

        switch (liaison){
            case Liaison.lorient_groix:
                indiceLiaison = 0;
                break;
            case Liaison.groix_lorient:
                indiceLiaison = 0;
                break;
            case Liaison.quiberon_lepalais:
                indiceLiaison = 1;
                break;
            case Liaison.lepalais_quiberon:
                indiceLiaison = 1;
                break;
            default: // liaison est une énumération dont tous les cas sont traités ci-dessus
                break;
        }

        succes = lireFichier(cheminAccess, out tarifs);
        nbLignesFichier = tarifs.Length;

        ligne = tarifs[(int) passager].Split(';'); 

        tarif = double.Parse(ligne[indiceLiaison]); // sélectionne le bon tarif de la ligne 

        return succes;
    }

    static bool tarifsVehicules(CodeCategorieVehicule vehicule, Liaison liaison, out double tarif){
        bool succes = false;

        string cheminAccess = "CSV/Tarifs/vehicules.csv"; // de forme numéro du CodeCategorieVehicule;tarifGroix-Lorient;tarifLePalais-Quiberon
        int nbLignesFichier = 0;

        string[] tarifs;
        string[] ligne;

        uint indiceLiaison = 0; // 0 pour groix / lorient et 1 pour le palais / quiberon

        switch (liaison){
            case Liaison.lorient_groix:
                indiceLiaison = 0;
                break;
            case Liaison.groix_lorient:
                indiceLiaison = 0;
                break;
            case Liaison.quiberon_lepalais:
                indiceLiaison = 1;
                break;
            case Liaison.lepalais_quiberon:
                indiceLiaison = 1;
                break;
            default: // liaison est une énumération dont tous les cas sont traités ci-dessus
                break;
        }

        succes = lireFichier(cheminAccess, out tarifs);
        nbLignesFichier = tarifs.Length;

        ligne = tarifs[(int) vehicule].Split(';'); 

        tarif = double.Parse(ligne[indiceLiaison]); // sélectionne le bon tarif de la ligne 

        return succes;
    }
}
