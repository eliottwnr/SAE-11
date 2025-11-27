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
            saisie = Console.ReadLine(); // peut renvoyer null (d'où string?)
            saisieJuste = uint.TryParse(saisie, out liaison); // essaye de convertir la saisie en unsigned int, renvoie un booléen (succès ou échec)

            if (!saisieJuste || (liaison < 1 || liaison > 4)){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || liaison < 1 || liaison > 4);

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
        } while (!saisieJuste || date[0] <= 0 || date[0] >= 31); // tant que le jour n'est pas entre 1 et 30 (inclus)

        do {
            Console.Write("Saisir le mois de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[1]);

            if (!saisieJuste || date[1] != 11){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || date[1] != 11); // tant que le mois n'est pas en novembre 

        do {
            Console.Write("Saisir l'année de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[2]);

            if (!saisieJuste || date[2] != 2025){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || date[2] != 2025); // tant que le mois n'est pas 2025 

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

            if (!saisieJuste || horaireSaisie <= 0 || horaireSaisie > nbHoraires){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || horaireSaisie <= 0 || horaireSaisie > nbHoraires);

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

            if (!saisieJuste || code < 1 || code > 5){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || code < 1 || code > 5);

        return (CodeCategoriePassager) (code - 1); // car les codes vont de 0 à 4 mais la saisie est de 1 à 5 (voir affichage)
    }

    static bool estLettresOuTiret(string? chaine){
        // renvoie true si chaine contient uniquement des lettres et / ou des tirets 
        bool estJuste = true;
        int i = 0;

        if (chaine == null || chaine.Length == 0){
            estJuste = false;
        }
        else {
            while (estJuste && i < chaine.Length){
                if (!char.IsLetter(chaine[i]) && chaine[i] != '-'){
                    estJuste = false;
                }
                i++;
            }
        }

        return estJuste;
    }

    static string initCap(string? chaine){
        // met la première lettre de la chaine en majuscule, le reste en minuscule
        string nouvelleChaine = "";
        int indiceLettre;
        bool tiret = false; // sert à mettre la lettre d'après en majuscule (s'il y en a une)

        if (chaine != null){
            nouvelleChaine += char.ToUpper(chaine[0]); // ajoute la première lettre de la chaine en majuscules

            // ajoute le reste des lettres de la chaine en minuscule
            for (indiceLettre = 1; indiceLettre < chaine.Length; indiceLettre++){ // pour toutes les lettres en partant de la seconde
                if (char.IsLetter(chaine[indiceLettre])){ // un tiret ne se met pas en majuscules
                    if (tiret){ // si l'élément d'avant est un tiret
                        nouvelleChaine += char.ToUpper(chaine[indiceLettre]);
                        tiret = false;
                    }
                    else {
                        nouvelleChaine += char.ToLower(chaine[indiceLettre]);
                    }
                }
                else { // si c'est un tiret
                    nouvelleChaine += chaine[indiceLettre];
                    tiret = true; 
                }
            }
        }

        return nouvelleChaine;
    }

    static Passager saisirPassager(){
        string nom = "";
        string prenom = "";
        CodeCategoriePassager categorie;

        string? saisie;

        Console.Clear(); // Nettoie la console
        Console.WriteLine("-- Saisie du/des passager(s) --\n\n");

        // saisie du nom
        do {
            Console.Write("Saisir votre nom : ");
            saisie = Console.ReadLine();
            if (!estLettresOuTiret(saisie)){ // si la saisie ne contient pas seulement des lettres (ou un tiret) ou est nulle
                afficherSaisieIncorrecte();
            }
            else { // si la saisie est correcte
                nom = initCap(saisie);
            }
        } while (nom == "");

        // saisie du prénom
        do {
            Console.Write("Saisir votre prénom : ");
            saisie = Console.ReadLine();
            if (!estLettresOuTiret(saisie)){ // si la saisie ne contient pas seulement des lettres ou est nulle
                afficherSaisieIncorrecte();
            }
            else { // si la saisie est correcte
                prenom = initCap(saisie);
            }
        } while (prenom == "");

        categorie = saisirCategoriePassager();

        return new Passager(nom, prenom, categorie);
    }

    static bool validation(string prompt){
        string? saisie;

        do {
            Console.Write(prompt + " [o/n] ");
            saisie = Console.ReadLine();

            if (saisie != null){
                saisie = saisie.ToUpper();
            }
        } while (saisie != "O" && saisie != "N");

        return (saisie == "O");
    }

    static bool autrePassager(){
        return validation("Y aura-t-il d'autres passagers ? ");
    }

    static CodeCategorieVehicule saisirCategorieVehicule(){
        string? saisie;
        bool saisieJuste;
        int code;

        afficherCategoriesVehicule();
        do {
            afficherPrompt();
            saisie = Console.ReadLine();
            saisieJuste = int.TryParse(saisie, out code);

            if (!saisieJuste || code < 1 || code > 11){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || code < 1 || code > 11);

        return (CodeCategorieVehicule) (code - 1); // les codes vont de 0 à 10 mais les saisies de 1 à 11
    }

    static Vehicule saisirVehicule(){
        string? saisie;
        bool saisieJuste;
        uint quantite;

        Console.Clear();
        Console.WriteLine("-- Saisie du/des véhicule(s) --\n\n");

        CodeCategorieVehicule categorie = saisirCategorieVehicule();
        
        do {
            Console.Write("Saisir la quantité : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out quantite);
            if (!saisieJuste || quantite < 1){
                afficherSaisieIncorrecte();
            }
        } while (!saisieJuste || quantite < 1);

        return new Vehicule(quantite, categorie);
    }

    static bool vehicule(){
        return validation("Y aura-t-il un véhicule ? ");
    }

    static bool autreVehicule(){
        return validation("Y aura-t-il d'autres véhicules ? ");
    }

    static bool trajetRetour(){
        return validation("Y aura-t-il un trajet retour ? ");
    }

    static string saisirNomReservation(){
        string? saisie;

        do {
            Console.Write("Saisir le nom de la réservation : ");
            saisie = Console.ReadLine();
        } while (saisie == null);


        return saisie;
    }
}
