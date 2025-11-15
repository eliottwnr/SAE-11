using System;

// S'occupe de toutes les saisies utilisateur
partial class Programme {
    // Plus simple de saisir un entier non signé mais plus explicite de renvoyer une énumération
    static Liaison saisirLiaison(){
        uint liaison;
        
        string? saisie; // string? : string qui peut être null (au cas où aucune saisie)
        bool saisieJuste;

        afficherLiaisons();

        do {
            afficherPrompt();
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out liaison);

            if (!saisieJuste || (liaison < 1 || liaison > 4)){
                afficherSaisieIncorrecte();
            }
        } while (liaison < 1 || liaison > 4);

        return (Liaison) liaison; // cast (conversion) vers l'énumération
    }

    // permet de saisir des dates en novembre 2025 (mais modifiable) 
    // prompt différent à chaque fois donc pas d'appel d'une fonction d'affichage
    static uint[] saisirDate(){
        uint[] date = new uint[3]; // jour, mois, année

        string? saisie;
        bool saisieJuste;

        Console.Clear(); // nettoie la console
        Console.WriteLine("-- Saisie de la date --\n\n");
        do {
            Console.Write("Saisir le jour de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[0]);

            if (!saisieJuste || date[0] <= 0 || date[0] >= 31){
                afficherSaisieIncorrecte();
            }
        } while (date[0] <= 0 || date[0] >= 31); // tant que le jour n'est pas entre 1 et 30 (inclus)

        do {
            Console.Write("Saisir le mois de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[1]);

            if (!saisieJuste || date[1] != 11){
                afficherSaisieIncorrecte();
            }
        } while (date[1] != 11); // tant que le mois n'est pas en novembre 

        do {
            Console.Write("Saisir l'année de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[2]);

            if (!saisieJuste || date[2] != 2025){
                afficherSaisieIncorrecte();
            }
        } while (date[2] != 2025); // tant que le mois n'est pas 2025 

        return date;
    }

    static uint[] saisirHoraire(string[] horaires){
        uint[] heure = new uint[2]; // heures, minutes
        int nbHoraires;

        bool saisieJuste;
        string? saisie;
        uint horaireSaisie;

        afficherHoraires(horaires);
        nbHoraires = horaires.Length;

        do {
            afficherPrompt();
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out horaireSaisie);

            if (horaireSaisie <= 0 || horaireSaisie > nbHoraires){
                afficherSaisieIncorrecte();
            }
        } while (horaireSaisie <= 0 || horaireSaisie > nbHoraires);

        heure[0] = uint.Parse(horaires[horaireSaisie - 1].Split(':')[0]); // sélectionne la première partie du bon horaire 
        heure[1] = uint.Parse(horaires[horaireSaisie - 1].Split(':')[1]); // sélectionne la seconde partie du bon horaire 

        return heure;
    }

    static CodeCategoriePassager saisirCategoriePassager(){
        string? saisie;
        bool saisieJuste;
        uint code;

        afficherCategoriesPassager();
        do {
            afficherPrompt();
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out code);

            if (code < 1 || code > 5){
                afficherSaisieIncorrecte();
            }
        } while (code < 1 || code > 5);

        return (CodeCategoriePassager) (code - 1); // car les codes vont de 0 à 4 mais la saisie est de 1 à 5 (voir affichage)
    }

    static Passager saisirPassager(){
        string nom = "";
        string prenom = "";
        CodeCategoriePassager categorie;

        string? saisie;
        int indiceLettre;

        Console.Clear(); // Nettoie la console
        Console.WriteLine("-- Saisie du/des passager(s) --\n\n");

        // saisie du nom
        do {
            Console.Write("Saisir votre nom : ");
            saisie = Console.ReadLine();
            if (saisie == null || !saisie.All(char.IsLetter)){ // si la saisie ne contient pas seulement des lettres ou est nulle
                afficherSaisieIncorrecte();
            }
            else { // si la saisie est correcte
                nom += char.ToUpper(saisie[0]); // ajoute la première lettre de la saisie en majuscules

                // ajoute le reste des lettres de la saisie en minuscule
                for (indiceLettre = 1; indiceLettre < saisie.Length; indiceLettre++){ // pour toutes les lettres en partant de la seconde
                    nom += char.ToLower(saisie[indiceLettre]);
                }
            }
        } while (nom == "");

        // saisie du prénom
        do {
            Console.Write("Saisir votre prénom : ");
            saisie = Console.ReadLine();
            if (saisie == null || !saisie.All(char.IsLetter)){ // si la saisie ne contient pas seulement des lettres ou est nulle
                afficherSaisieIncorrecte();
            }
            else { // si la saisie est correcte
                prenom += char.ToUpper(saisie[0]); // ajoute la première lettre de la saisie en majuscules

                // ajoute le reste des lettres de la saisie en minuscule
                for (indiceLettre = 1; indiceLettre < saisie.Length; indiceLettre++){ // pour toutes les lettres en partant de la seconde
                    prenom += char.ToLower(saisie[indiceLettre]);
                }
            }
        } while (prenom == "");

        categorie = saisirCategoriePassager();

        return new Passager(nom, prenom, categorie);
    }
}
