using System;
using System.IO;

// Gère la récupération des horaires dans le csv correspondant
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
        string cheminAccess = "Horaires/";

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
}
